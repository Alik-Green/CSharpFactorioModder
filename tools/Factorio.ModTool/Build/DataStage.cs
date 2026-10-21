using System.Text;
using System.Text.Json;
using Factorio.ModTool.Api;

namespace Factorio.ModTool.Build;

/// <summary>
/// Executes a packaged mod's <c>data.lua</c> under Lua 5.2 with a strict <c>defines</c> table from the
/// official API document and validates every registered prototype against <c>prototype-api.json</c>.
/// </summary>
public static class DataStage
{
    /// <summary>One prototype table handed to <c>data:extend</c>.</summary>
    public sealed record Shape(
        string Type,
        string Name,
        IReadOnlyList<string> Keys,
        JsonElement? Element = null);

    public sealed record RunResult(
        IReadOnlyList<Shape> Shapes,
        IReadOnlyList<LuaCheck.Finding> Findings,
        IReadOnlyList<string> Warnings);

    /// <summary>
    /// Runs <c>data.lua</c> (when present and Lua 5.2 is installed) and runs all prototype, geometry,
    /// cross-reference, resource-path and reachability checks.
    /// </summary>
    public static RunResult Run(
        ToolchainSettings settings,
        ModProject project,
        string modDirectory,
        ApiTarget apiTarget,
        IReadOnlySet<string>? inherited = null)
    {
        var dataLua = Path.Combine(modDirectory, "data.lua");
        if (!File.Exists(dataLua))
            return new RunResult(Array.Empty<Shape>(), Array.Empty<LuaCheck.Finding>(), Array.Empty<string>());

        var lua = Shell.Lua(settings);
        if (lua == null)
            return new RunResult(Array.Empty<Shape>(), Array.Empty<LuaCheck.Finding>(), Array.Empty<string>());

        var runtimeJson = Path.Combine(apiTarget.ApiDirectory, "runtime-api.json");
        var prototypeJson = Path.Combine(apiTarget.ApiDirectory, "prototype-api.json");
        var harnessPath = Path.Combine(settings.BuildDirectory, "obj", project.Name, "data_harness.lua");
        Directory.CreateDirectory(Path.GetDirectoryName(harnessPath)!);
        File.WriteAllText(harnessPath, BuildHarnessLua(runtimeJson));

        var run = Shell.Run(lua, new[] { harnessPath, modDirectory, dataLua }, workingDirectory: modDirectory);
        if (run.ExitCode != 0)
        {
            var err = (run.StdErr + "\n" + run.StdOut).Trim();
            return new RunResult(
                Array.Empty<Shape>(),
                new[] { new LuaCheck.Finding("data.lua", 0, err) },
                Array.Empty<string>());
        }

        var shapes = ParseShapes(run.StdOut);
        var hasForeignDeps = project.Dependencies.Any(dep => !IsVanillaDependency(dep));
        var findings = Validate(
            shapes,
            prototypeJson,
            project.FactorioVersion,
            hasForeignDependencies: hasForeignDeps,
            modName: project.Name,
            inherited: inherited);
        var warnings = DataStageRules.Unreachable(shapes);

        return new RunResult(shapes, findings, warnings);
    }

    /// <summary>Validates extracted prototype shapes against <c>prototype-api.json</c> and engine rules.</summary>
    public static IReadOnlyList<LuaCheck.Finding> Validate(
        IReadOnlyList<Shape> shapes,
        string prototypeJsonPath,
        string apiVersion,
        bool hasForeignDependencies = false,
        string? modName = null,
        IReadOnlySet<string>? inherited = null)
    {
        _ = apiVersion;
        var schema = PrototypeSchema.Load(prototypeJsonPath);
        var findings = new List<LuaCheck.Finding>();

        foreach (var shape in shapes)
        {
            var proto = schema.FindPrototype(shape.Type);
            if (proto == null)
            {
                findings.Add(new LuaCheck.Finding("data.lua", 0, $"'{shape.Type}' is not a prototype type"));
                continue;
            }

            var props = schema.PropertiesOf(proto);
            var altNames = props.Values
                .Where(p => !string.IsNullOrEmpty(p.AltName))
                .Select(p => p.AltName!)
                .ToHashSet(StringComparer.Ordinal);
            var keySet = shape.Keys.ToHashSet(StringComparer.Ordinal);

            // 1. Unknown top-level keys
            foreach (var key in shape.Keys)
            {
                if (key is "__inherit" or "__nil") continue;
                if (props.ContainsKey(key) || altNames.Contains(key)) continue;

                var suggestion = PrototypeSchema.NearestProperty(key, props.Keys);
                var suffix = suggestion != null ? $" (did you mean '{suggestion}'?)" : "";
                findings.Add(new LuaCheck.Finding(
                    "data.lua",
                    0,
                    $"{shape.Type} {shape.Name}: '{key}' is not a property of {proto.Name}{suffix}"));
            }

            // 2. Missing required top-level properties (skipped when inheriting from a vanilla prototype)
            var inheritsVanilla = inherited != null && inherited.Contains($"{shape.Type}/{shape.Name}");
            if (!inheritsVanilla)
            {
                foreach (var prop in props.Values)
                {
                    if (prop.Optional) continue;
                    if (keySet.Contains(prop.Name)) continue;
                    if (!string.IsNullOrEmpty(prop.AltName) && keySet.Contains(prop.AltName)) continue;

                    findings.Add(new LuaCheck.Finding(
                        "data.lua",
                        0,
                        $"{shape.Type} {shape.Name}: missing required property '{prop.Name}' of {proto.Name}"));
                }
            }

            // 3. Nested structures, engine geometry, and resource paths when full JSON is present
            if (shape.Element is { ValueKind: JsonValueKind.Object } root)
            {
                DataStageRules.CheckNestedProperties(schema, proto, props, shape, root, findings);
                DataStageRules.CheckGeometry(shape, root, findings);
                DataStageResources.CheckPrototype(shape, root, modName, findings);
            }
        }

        // 4. Cross-prototype references (recipe categories, item groups, item subgroups)
        DataStageReferences.Check(shapes, hasForeignDependencies, findings);

        return findings;
    }

    private static bool IsVanillaDependency(string dependency)
    {
        var token = dependency.Trim().TrimStart('?', '!', '~', '(').Trim();
        var name = token.Split(' ', StringSplitOptions.RemoveEmptyEntries).FirstOrDefault() ?? "";
        return name is "base" or "core";
    }

    private static List<Shape> ParseShapes(string stdout)
    {
        var shapes = new List<Shape>();
        var inBlock = false;
        foreach (var rawLine in stdout.Replace("\r\n", "\n").Split('\n'))
        {
            var line = rawLine.Trim();
            if (line == "__FACTORIO_PROTOTYPES_START__") { inBlock = true; continue; }
            if (line == "__FACTORIO_PROTOTYPES_END__") { inBlock = false; continue; }
            if (!inBlock || line.Length == 0) continue;

            using var doc = JsonDocument.Parse(line);
            var root = doc.RootElement.Clone();
            if (root.ValueKind != JsonValueKind.Object) continue;
            var type = root.TryGetProperty("type", out var t) && t.ValueKind == JsonValueKind.String ? t.GetString() ?? "" : "";
            var name = root.TryGetProperty("name", out var n) && n.ValueKind == JsonValueKind.String ? n.GetString() ?? "" : "";
            var keys = root.EnumerateObject().Select(p => p.Name).ToList();
            shapes.Add(new Shape(type, name, keys, root));
        }

        return shapes;
    }

    private static string BuildHarnessLua(string runtimeJsonPath)
    {
        var sb = new StringBuilder();
        sb.AppendLine("local mod_dir = arg[1]");
        sb.AppendLine("local data_lua = arg[2]");
        sb.AppendLine("package.path = mod_dir .. '/?.lua;' .. mod_dir .. '/?/init.lua;' .. package.path");
        sb.AppendLine();
        sb.AppendLine("local function strict_defines(name, tbl)");
        sb.AppendLine("  return setmetatable(tbl, {");
        sb.AppendLine("    __index = function(_, key)");
        sb.AppendLine("      error(\"unknown defines member '\" .. name .. \".\" .. tostring(key) .. \"'\", 2)");
        sb.AppendLine("    end");
        sb.AppendLine("  })");
        sb.AppendLine("end");
        sb.AppendLine();

        var doc = JsonSerializer.Deserialize<ApiDocument>(File.ReadAllText(runtimeJsonPath));
        sb.AppendLine("defines = strict_defines('defines', {");
        if (doc?.Defines != null)
        {
            foreach (var def in doc.Defines)
                EmitDefine(sb, "defines", def, "  ");
        }
        sb.AppendLine("})");
        sb.AppendLine();

        sb.AppendLine("""
            local extended = {}
            data = {
              is_demo = false,
              raw = setmetatable({}, {
                __index = function(self, key)
                  local t = {}
                  rawset(self, key, t)
                  return t
                end
              }),
              extend = function(self, list)
                assert(type(list) == "table", "data:extend expects a table of prototypes")
                for _, proto in ipairs(list) do
                  if type(proto) == "table" then
                    if proto.type ~= nil and proto.name ~= nil then
                      self.raw[proto.type][proto.name] = proto
                    end
                    extended[#extended + 1] = proto
                  end
                end
              end
            }

            local function json_escape(s)
              return (s:gsub('\\', '\\\\'):gsub('"', '\\"'):gsub('\n', '\\n'):gsub('\r', '\\r'):gsub('\t', '\\t'))
            end

            local function is_array(t)
              local count = 0
              local max_n = 0
              for k, _ in pairs(t) do
                if type(k) ~= "number" or k < 1 or k % 1 ~= 0 then return false, 0 end
                count = count + 1
                if k > max_n then max_n = k end
              end
              if max_n ~= count then return false, 0 end
              return true, count
            end

            local function to_json(v)
              local tv = type(v)
              if v == nil then return "null" end
              if tv == "boolean" then return v and "true" or "false" end
              if tv == "number" then
                if v ~= v or v == math.huge or v == -math.huge then return "0" end
                if v % 1 == 0 and v >= -1e15 and v <= 1e15 then return string.format("%.0f", v) end
                return tostring(v)
              end
              if tv == "string" then return '"' .. json_escape(v) .. '"' end
              if tv == "table" then
                local arr, n = is_array(v)
                if arr then
                  local items = {}
                  for i = 1, n do items[i] = to_json(v[i]) end
                  return "[" .. table.concat(items, ",") .. "]"
                else
                  local items = {}
                  local keys = {}
                  for k, _ in pairs(v) do keys[#keys + 1] = tostring(k) end
                  table.sort(keys)
                  for i = 1, #keys do
                    local k = keys[i]
                    items[#items + 1] = '"' .. json_escape(k) .. '":' .. to_json(v[k])
                  end
                  return "{" .. table.concat(items, ",") .. "}"
                end
              end
              return "null"
            end

            dofile(data_lua)
            print("__FACTORIO_PROTOTYPES_START__")
            for i = 1, #extended do
              print(to_json(extended[i]))
            end
            print("__FACTORIO_PROTOTYPES_END__")
            """);

        return sb.ToString();
    }

    private static void EmitDefine(StringBuilder sb, string prefix, DefineDef def, string indent)
    {
        var fullName = $"{prefix}.{def.Name}";
        sb.AppendLine($"{indent}[{Quote(def.Name)}] = strict_defines({Quote(fullName)}, {{");
        if (def.Values != null)
        {
            for (var i = 0; i < def.Values.Count; i++)
                sb.AppendLine($"{indent}  [{Quote(def.Values[i].Name)}] = {i},");
        }
        if (def.Subkeys != null)
        {
            foreach (var sub in def.Subkeys)
                EmitDefine(sb, fullName, sub, indent + "  ");
        }
        sb.AppendLine($"{indent}}}),");
    }

    private static string Quote(string value) =>
        "\"" + value.Replace("\\", "\\\\").Replace("\"", "\\\"") + "\"";
}
