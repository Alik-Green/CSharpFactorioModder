using System.Globalization;
using System.Text.Json;
using Factorio.ModTool.Api;

namespace Factorio.ModTool.Build;

/// <summary>
/// Validates nested prototype structures, engine collision/fluid-box geometry, and content reachability.
/// </summary>
public static class DataStageRules
{
    private static readonly string[] SingleFluidBoxKeys =
    {
        "fluid_box", "input_fluid_box", "output_fluid_box", "fuel_fluid_box", "oxidizer_fluid_box",
    };

    /// <summary>Recursively checks nested object/array properties against <see cref="PrototypeSchema"/>.</summary>
    public static void CheckNestedProperties(
        PrototypeSchema schema,
        PrototypeDef proto,
        IReadOnlyDictionary<string, PrototypeProperty> props,
        DataStage.Shape shape,
        JsonElement root,
        List<LuaCheck.Finding> findings)
    {
        foreach (var jsonProp in root.EnumerateObject())
        {
            var def = ResolveProperty(props, jsonProp.Name);
            if (def == null) continue;
            ValidateValue(schema, shape, jsonProp.Name, def.Type, enclosingType: null, jsonProp.Value, findings);
        }
    }

    private static void ValidateValue(
        PrototypeSchema schema,
        DataStage.Shape shape,
        string path,
        JsonElement typeExpr,
        TypeDef? enclosingType,
        JsonElement value,
        List<LuaCheck.Finding> findings)
    {
        if (value.ValueKind is JsonValueKind.Null or JsonValueKind.Undefined) return;

        // Follow named type aliases up front when `typeExpr` is a string.
        if (typeExpr.ValueKind == JsonValueKind.String)
        {
            var typeName = typeExpr.GetString()!;
            var target = schema.FindType(typeName);
            if (target == null) return; // primitive (double, string, boolean, ...)
            ValidateValue(schema, shape, path, target.Type, target, value, findings);
            return;
        }

        if (typeExpr.ValueKind != JsonValueKind.Object) return;
        if (!typeExpr.TryGetProperty("complex_type", out var ctEl)) return;
        var kind = ctEl.GetString();

        switch (kind)
        {
            case "type":
                if (typeExpr.TryGetProperty("value", out var inner))
                    ValidateValue(schema, shape, path, inner, enclosingType, value, findings);
                break;

            case "array":
                if (value.ValueKind == JsonValueKind.Array && typeExpr.TryGetProperty("value", out var elemType))
                {
                    var index = 1; // 1-based Lua indexing for prototype diagnostics
                    foreach (var item in value.EnumerateArray())
                    {
                        ValidateValue(schema, shape, $"{path}[{index}]", elemType, null, item, findings);
                        index++;
                    }
                }
                break;

            case "dictionary":
                if (value.ValueKind == JsonValueKind.Object && typeExpr.TryGetProperty("value", out var dictValType))
                {
                    foreach (var entry in value.EnumerateObject())
                        ValidateValue(schema, shape, $"{path}.{entry.Name}", dictValType, null, entry.Value, findings);
                }
                break;

            case "struct":
                if (value.ValueKind == JsonValueKind.Object && enclosingType != null)
                    ValidateStructObject(schema, shape, path, schema.StructOf(enclosingType), value, findings);
                break;

            case "union":
                if (value.ValueKind == JsonValueKind.Object && typeExpr.TryGetProperty("options", out var options))
                    ValidateUnionObject(schema, shape, path, options, enclosingType, value, findings);
                break;
        }
    }

    private static void ValidateUnionObject(
        PrototypeSchema schema,
        DataStage.Shape shape,
        string path,
        JsonElement options,
        TypeDef? enclosingType,
        JsonElement obj,
        List<LuaCheck.Finding> findings)
    {
        var branches = new List<PrototypeSchema.ResolvedStruct>();
        foreach (var option in options.EnumerateArray())
        {
            // Dictionary branch inside a union means arbitrary keys are valid when matched.
            if (ResolvesToDictionary(schema, option, out var dictValType))
            {
                foreach (var entry in obj.EnumerateObject())
                    ValidateValue(schema, shape, $"{path}.{entry.Name}", dictValType, null, entry.Value, findings);
                return;
            }

            var resolved = ResolveStructOption(schema, option, enclosingType);
            if (resolved != null) branches.Add(resolved);
        }

        if (branches.Count == 0) return;

        // 1. Tagged union (`type = "burner"` selects BurnerEnergySource directly).
        if (obj.TryGetProperty("type", out var tagEl) && tagEl.ValueKind == JsonValueKind.String)
        {
            var tag = tagEl.GetString();
            var matched = branches.FirstOrDefault(b => b.TypeTagLiteral == tag);
            if (matched != null)
            {
                ValidateStructObject(schema, shape, path, matched, obj, findings);
                return;
            }
        }

        // 2. Untagged union (e.g. Animation4Way = struct { north, east, ... } | Animation):
        // choose the branch with fewest unknown properties (preferring exact matches).
        var objKeys = obj.EnumerateObject().Select(p => p.Name).ToList();
        var best = branches
            .OrderBy(b => objKeys.Count(k => !b.Properties.ContainsKey(k) && !b.AltNames.Contains(k)))
            .ThenByDescending(b => objKeys.Count(k => b.Properties.ContainsKey(k) || b.AltNames.Contains(k)))
            .First();

        ValidateStructObject(schema, shape, path, best, obj, findings);
    }

    private static void ValidateStructObject(
        PrototypeSchema schema,
        DataStage.Shape shape,
        string path,
        PrototypeSchema.ResolvedStruct resolved,
        JsonElement obj,
        List<LuaCheck.Finding> findings)
    {
        var keys = new HashSet<string>(StringComparer.Ordinal);
        foreach (var jsonProp in obj.EnumerateObject())
        {
            keys.Add(jsonProp.Name);
            var def = ResolveProperty(resolved.Properties, jsonProp.Name);
            if (def == null)
            {
                var suggestion = PrototypeSchema.NearestProperty(jsonProp.Name, resolved.Properties.Keys);
                var suffix = suggestion != null ? $" (did you mean '{suggestion}'?)" : "";
                findings.Add(new LuaCheck.Finding(
                    "data.lua",
                    0,
                    $"{shape.Type} {shape.Name}: {path}.{jsonProp.Name} is not a property here{suffix}"));
                continue;
            }

            ValidateValue(schema, shape, $"{path}.{jsonProp.Name}", def.Type, null, jsonProp.Value, findings);
        }

        foreach (var prop in resolved.Properties.Values)
        {
            if (prop.Optional) continue;
            if (keys.Contains(prop.Name)) continue;
            if (!string.IsNullOrEmpty(prop.AltName) && keys.Contains(prop.AltName)) continue;

            findings.Add(new LuaCheck.Finding(
                "data.lua",
                0,
                $"{shape.Type} {shape.Name}: missing required property {path}.{prop.Name}"));
        }
    }

    private static PrototypeProperty? ResolveProperty(
        IReadOnlyDictionary<string, PrototypeProperty> props,
        string name)
    {
        if (props.TryGetValue(name, out var direct)) return direct;
        return props.Values.FirstOrDefault(p => p.AltName == name);
    }

    private static PrototypeSchema.ResolvedStruct? ResolveStructOption(
        PrototypeSchema schema,
        JsonElement option,
        TypeDef? enclosingType)
    {
        if (option.ValueKind == JsonValueKind.String)
        {
            var td = schema.FindType(option.GetString()!);
            if (td == null) return null;
            return td.Type.ValueKind == JsonValueKind.Object &&
                   td.Type.TryGetProperty("complex_type", out var ct) &&
                   ct.GetString() == "struct"
                ? schema.StructOf(td)
                : null;
        }

        if (option.ValueKind == JsonValueKind.Object && option.TryGetProperty("complex_type", out var optCt))
        {
            var kind = optCt.GetString();
            if (kind == "type" && option.TryGetProperty("value", out var inner))
                return ResolveStructOption(schema, inner, enclosingType);
            if (kind == "struct" && enclosingType != null)
                return schema.StructOf(enclosingType);
        }

        return null;
    }

    private static bool ResolvesToDictionary(PrototypeSchema schema, JsonElement option, out JsonElement valueType)
    {
        valueType = default;
        if (option.ValueKind == JsonValueKind.String)
        {
            var td = schema.FindType(option.GetString()!);
            return td != null && ResolvesToDictionary(schema, td.Type, out valueType);
        }

        if (option.ValueKind == JsonValueKind.Object && option.TryGetProperty("complex_type", out var ct))
        {
            var kind = ct.GetString();
            if (kind == "type" && option.TryGetProperty("value", out var inner))
                return ResolvesToDictionary(schema, inner, out valueType);
            if (kind == "dictionary" && option.TryGetProperty("value", out valueType))
                return true;
        }

        return false;
    }

    // ------------------------------------------------------------------ geometry

    /// <summary>Validates collision box orientation, fluid box volumes, and pipe connection positions.</summary>
    public static void CheckGeometry(DataStage.Shape shape, JsonElement root, List<LuaCheck.Finding> findings)
    {
        (double X1, double Y1, double X2, double Y2)? shrunkBox = null;

        if (root.TryGetProperty("collision_box", out var boxEl) && TryReadBox(boxEl, out var box))
        {
            if (box.X1 >= box.X2 || box.Y1 >= box.Y2)
            {
                findings.Add(new LuaCheck.Finding(
                    "data.lua",
                    0,
                    $"{shape.Type} {shape.Name}: collision_box {{{{{Fmt(box.X1)}, {Fmt(box.Y1)}}}, {{{Fmt(box.X2)}, {Fmt(box.Y2)}}}}} is inside out (left_top must be strictly less than right_bottom)"));
            }
            else
            {
                shrunkBox = (box.X1 + 0.001, box.Y1 + 0.001, box.X2 - 0.001, box.Y2 - 0.001);
            }
        }

        var usedPositions = new HashSet<string>(StringComparer.Ordinal);

        if (root.TryGetProperty("fluid_boxes", out var fluidBoxes) && fluidBoxes.ValueKind == JsonValueKind.Array)
        {
            var index = 1;
            foreach (var fb in fluidBoxes.EnumerateArray())
            {
                if (fb.ValueKind == JsonValueKind.Object)
                    CheckFluidBox(shape, $"fluid_boxes[{index}]", fb, shrunkBox, usedPositions, findings);
                index++;
            }
        }

        foreach (var key in SingleFluidBoxKeys)
        {
            if (root.TryGetProperty(key, out var singleFb) && singleFb.ValueKind == JsonValueKind.Object)
                CheckFluidBox(shape, key, singleFb, shrunkBox, usedPositions, findings);
        }
    }

    private static void CheckFluidBox(
        DataStage.Shape shape,
        string boxPath,
        JsonElement fb,
        (double X1, double Y1, double X2, double Y2)? shrunkBox,
        HashSet<string> usedPositions,
        List<LuaCheck.Finding> findings)
    {
        if (fb.TryGetProperty("volume", out var volEl) && volEl.ValueKind == JsonValueKind.Number)
        {
            var volume = volEl.GetDouble();
            if (volume <= 0)
            {
                findings.Add(new LuaCheck.Finding(
                    "data.lua",
                    0,
                    $"{shape.Type} {shape.Name}: {boxPath}.volume is {Fmt(volume)}; fluid box volume must be greater than 0"));
            }
        }

        if (!fb.TryGetProperty("pipe_connections", out var conns) || conns.ValueKind != JsonValueKind.Array)
            return;

        var index = 1;
        foreach (var conn in conns.EnumerateArray())
        {
            if (conn.ValueKind != JsonValueKind.Object) { index++; continue; }
            var connPath = $"{boxPath}.pipe_connections[{index}]";

            var isLinked = (conn.TryGetProperty("connection_type", out var ct) &&
                            ct.ValueKind == JsonValueKind.String &&
                            ct.GetString() == "linked") ||
                           conn.TryGetProperty("linked_connection_id", out _);

            if (!isLinked && (!conn.TryGetProperty("direction", out var dirEl) || dirEl.ValueKind == JsonValueKind.Null))
            {
                findings.Add(new LuaCheck.Finding(
                    "data.lua",
                    0,
                    $"{shape.Type} {shape.Name}: {connPath} has no direction (every normal pipe connection requires direction)"));
            }

            if (conn.TryGetProperty("position", out var posEl) && TryReadPoint(posEl, out var pt))
            {
                var posKey = $"{Fmt(pt.X)},{Fmt(pt.Y)}";
                if (!usedPositions.Add(posKey))
                {
                    findings.Add(new LuaCheck.Finding(
                        "data.lua",
                        0,
                        $"{shape.Type} {shape.Name}: {connPath} and another pipe connection cannot share a position {{{Fmt(pt.X)}, {Fmt(pt.Y)}}}"));
                }

                if (shrunkBox is { } b &&
                    (pt.X < b.X1 - 1e-9 || pt.X > b.X2 + 1e-9 || pt.Y < b.Y1 - 1e-9 || pt.Y > b.Y2 + 1e-9))
                {
                    findings.Add(new LuaCheck.Finding(
                        "data.lua",
                        0,
                        $"{shape.Type} {shape.Name}: {connPath}.position is {{{Fmt(pt.X)}, {Fmt(pt.Y)}}}, " +
                        $"outside the entity bounding box {{{{{Fmt(b.X1)}, {Fmt(b.Y1)}}}, {{{Fmt(b.X2)}, {Fmt(b.Y2)}}}}}; " +
                        "a pipe connection must sit inside collision_box, or Factorio refuses to load the entity " +
                        "(\"position must be inside of entity bounding box\")"));
                }
            }

            index++;
        }
    }

    // -------------------------------------------------------------- reachability

    /// <summary>Warns on prototypes that load cleanly in Factorio yet can never be crafted or found.</summary>
    public static IReadOnlyList<string> Unreachable(IEnumerable<DataStage.Shape> shapes)
    {
        var list = shapes.ToList();
        var producedItems = new HashSet<string>(StringComparer.Ordinal);
        var unlockedRecipes = new HashSet<string>(StringComparer.Ordinal);

        foreach (var proto in list)
        {
            if (proto.Element is not { ValueKind: JsonValueKind.Object } el) continue;

            if (proto.Type == "recipe")
            {
                if (el.TryGetProperty("result", out var resStr) && resStr.ValueKind == JsonValueKind.String)
                    producedItems.Add(resStr.GetString()!);

                if (el.TryGetProperty("results", out var results) && results.ValueKind == JsonValueKind.Array)
                {
                    foreach (var entry in results.EnumerateArray())
                    {
                        if (entry.ValueKind == JsonValueKind.Object &&
                            entry.TryGetProperty("name", out var n) &&
                            n.ValueKind == JsonValueKind.String)
                        {
                            producedItems.Add(n.GetString()!);
                        }
                        else if (entry.ValueKind == JsonValueKind.Array &&
                                 entry.GetArrayLength() >= 1 &&
                                 entry[0].ValueKind == JsonValueKind.String)
                        {
                            producedItems.Add(entry[0].GetString()!);
                        }
                    }
                }
            }
            else if (proto.Type == "technology")
            {
                if (el.TryGetProperty("effects", out var effects) && effects.ValueKind == JsonValueKind.Array)
                {
                    foreach (var effect in effects.EnumerateArray())
                    {
                        if (effect.ValueKind == JsonValueKind.Object &&
                            effect.TryGetProperty("type", out var et) &&
                            et.GetString() == "unlock-recipe" &&
                            effect.TryGetProperty("recipe", out var rec) &&
                            rec.ValueKind == JsonValueKind.String)
                        {
                            unlockedRecipes.Add(rec.GetString()!);
                        }
                    }
                }
            }
        }

        var warnings = new List<string>();
        foreach (var proto in list)
        {
            if (proto.Element is not { ValueKind: JsonValueKind.Object } el) continue;
            if (el.TryGetProperty("hidden", out var hiddenEl) && hiddenEl.ValueKind == JsonValueKind.True)
                continue;

            if (proto.Type == "item")
            {
                if (!el.TryGetProperty("subgroup", out var subEl) ||
                    subEl.ValueKind != JsonValueKind.String ||
                    string.IsNullOrWhiteSpace(subEl.GetString()))
                {
                    warnings.Add(
                        $"item '{proto.Name}' has no subgroup and will appear under the 'Other' tab in the crafting menu");
                }

                if (el.TryGetProperty("place_result", out var placeEl) &&
                    placeEl.ValueKind == JsonValueKind.String &&
                    !string.IsNullOrEmpty(placeEl.GetString()) &&
                    !producedItems.Contains(proto.Name))
                {
                    warnings.Add(
                        $"item '{proto.Name}' places '{placeEl.GetString()}' but no recipe in this mod produces it, " +
                        "so it cannot be crafted and never appears in the crafting menu");
                }
            }
            else if (proto.Type == "recipe")
            {
                if (el.TryGetProperty("enabled", out var enabledEl) &&
                    enabledEl.ValueKind == JsonValueKind.False &&
                    !unlockedRecipes.Contains(proto.Name))
                {
                    warnings.Add(
                        $"recipe '{proto.Name}' starts disabled and no technology in this mod unlocks it");
                }
            }
        }

        return warnings;
    }

    private static bool TryReadBox(JsonElement el, out (double X1, double Y1, double X2, double Y2) box)
    {
        box = default;
        if (el.ValueKind == JsonValueKind.Array && el.GetArrayLength() >= 2 &&
            TryReadPoint(el[0], out var lt) && TryReadPoint(el[1], out var rb))
        {
            box = (lt.X, lt.Y, rb.X, rb.Y);
            return true;
        }

        if (el.ValueKind == JsonValueKind.Object &&
            el.TryGetProperty("left_top", out var ltObj) &&
            el.TryGetProperty("right_bottom", out var rbObj) &&
            TryReadPoint(ltObj, out var lt2) &&
            TryReadPoint(rbObj, out var rb2))
        {
            box = (lt2.X, lt2.Y, rb2.X, rb2.Y);
            return true;
        }

        return false;
    }

    private static bool TryReadPoint(JsonElement el, out (double X, double Y) point)
    {
        point = default;
        if (el.ValueKind == JsonValueKind.Array && el.GetArrayLength() >= 2 &&
            el[0].ValueKind == JsonValueKind.Number && el[1].ValueKind == JsonValueKind.Number)
        {
            point = (el[0].GetDouble(), el[1].GetDouble());
            return true;
        }

        if (el.ValueKind == JsonValueKind.Object &&
            el.TryGetProperty("x", out var xEl) && xEl.ValueKind == JsonValueKind.Number &&
            el.TryGetProperty("y", out var yEl) && yEl.ValueKind == JsonValueKind.Number)
        {
            point = (xEl.GetDouble(), yEl.GetDouble());
            return true;
        }

        return false;
    }

    private static string Fmt(double value) =>
        Math.Round(value, 6).ToString("0.###", CultureInfo.InvariantCulture);
}
