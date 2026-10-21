using System.Text.Json;
using System.Text.RegularExpressions;
using Factorio.ModTool.Api;
using Xunit;

namespace Factorio.Bindings.Tests;

/// <summary>
/// Tests against the *real* official runtime API document (api/&lt;version&gt;/runtime-api.json).
/// The subject under test is the emitted C#, because that is what CSharp.lua consumes.
/// </summary>
public class RuntimeBindingTests
{
    private static readonly Generated G = Generated.Default;
    private static readonly string Cs = G.AllCode;

    private static JsonElement LoadApi() =>
        JsonDocument.Parse(File.ReadAllText(TestEnvironment.RuntimeJson)).RootElement;

    private static JsonElement FindClass(string name) =>
        LoadApi().GetProperty("classes").EnumerateArray().First(c => c.GetProperty("name").GetString() == name);

    /// <summary>Parent class of a C#-named class, in C# naming.</summary>
    private static string? FactorioParent(string name)
    {
        var cls = FindClass(G.ApiName(name));
        return cls.TryGetProperty("parent", out var p) ? G.CsName(p.GetString()!) : null;
    }

    /// <summary>Source with string literals and comments blanked, so name checks see code only.</summary>
    private static string CodeOnly(string text) =>
        Regex.Replace(Regex.Replace(text, @"///[^\n]*", ""), "\"[^\"\n]*\"", "\"\"");

    // ---------------------------------------------------------------- layout

    [Fact]
    public void OneFilePerType_is_written_under_a_stable_path()
    {
        Assert.True(G.Files.Count > 1000, $"only {G.Files.Count} files were written");
        foreach (var name in new[] { "Game", "Player", "Entity", "Surface" })
            Assert.True(G.Files.ContainsKey($"Runtime/Objects/{name}.cs"), $"missing Runtime/Objects/{name}.cs");
        Assert.True(G.Files.ContainsKey("Runtime/Tables/MapPosition.cs"));
        Assert.True(G.Files.ContainsKey("Runtime/Events/OnTickEvent.cs"));
        Assert.True(G.Files.ContainsKey("Runtime/Globals.cs"));
        Assert.True(G.Files.ContainsKey("Runtime/Defines.cs"));
        Assert.True(G.Files.ContainsKey("Runtime/Events.cs"));
        Assert.All(G.Files.Where(f => f.Key.EndsWith(".cs", StringComparison.Ordinal)),
            f => Assert.Contains("namespace Factorio.", f.Value));
    }

    // ---------------------------------------------------------------- globals

    [Fact]
    public void Global_objects_expand_to_the_native_globals()
    {
        foreach (var global in new[] { "game", "script", "prototypes", "settings", "rendering", "remote", "rcon", "helpers", "commands" })
        {
            Assert.Contains($"/// @CSharpLua.Get = \"{global}\"", G.Globals);
        }
        Assert.Contains("public static Game Game { get; }", G.Globals);
        Assert.Contains("public static Script Script { get; }", G.Globals);
        Assert.Contains("/// @CSharpLua.Ignore\n    public static class Globals", G.Globals.Replace("\r\n", "\n"));
    }

    [Fact]
    public void Every_documented_global_object_is_bound()
    {
        var expected = LoadApi().GetProperty("global_objects").EnumerateArray()
            .Select(g => g.GetProperty("name").GetString()!).ToList();
        foreach (var name in expected)
            Assert.Contains($"/// @CSharpLua.Get = \"{name}\"", G.Globals);
    }

    [Fact]
    public void Global_functions_expand_to_bare_calls_not_member_calls()
    {
        // `Globals.Log(x)` must compile to `log(x)`; a `{this}` template would emit the invalid `.log(x)`.
        Assert.Contains("/// @CSharpLua.Template = \"log({0})\"", G.Globals);
        Assert.Contains("/// @CSharpLua.Template = \"localised_print({0})\"", G.Globals);
        Assert.Contains("/// @CSharpLua.Template = \"table_size({0})\"", G.Globals);
        Assert.DoesNotContain("{this}.log(", G.Globals);
    }

    // ----------------------------------------------------------- inheritance

    [Fact]
    public void Class_inheritance_mirrors_the_documented_parent_chain()
    {
        foreach (var child in new[] { "Player", "Entity", "Surface", "GuiElement", "Control" })
        {
            var parent = FactorioParent(child) ?? "LuaObject";
            Assert.Contains($"partial class {child} : {parent}", G.Type(child));
        }
        Assert.Equal("Control", FactorioParent("Player"));
    }

    [Fact]
    public void Every_class_is_emitted_and_ignored()
    {
        var classes = LoadApi().GetProperty("classes").EnumerateArray()
            .Select(c => c.GetProperty("name").GetString()!).ToList();
        foreach (var name in classes)
        {
            var cs = G.CsName(name);
            var file = G.Type(cs);
            Assert.False(string.IsNullOrEmpty(file), $"no file for {name} (as {cs})");
            Assert.Contains($"partial class {cs}", file);
            Assert.Matches(new Regex($@"/// @CSharpLua\.Ignore\s*\n\s*public (abstract )?partial class {cs}\b"),
                file.Replace("\r\n", "\n"));
        }
    }

    [Fact]
    public void Root_object_type_is_the_hand_written_LuaObject_marker()
    {
        // LuaObject is not a class in the API document: it is the marker base in src/Factorio.Bindings/Compat.cs.
        Assert.Contains("public abstract class LuaObject", File.ReadAllText(
            Path.Combine(TestEnvironment.RepoRoot, "src", "Factorio.Bindings", "Compat.cs")));
        Assert.DoesNotContain("partial class LuaObject", Cs);
        Assert.Contains("partial class Commandable : LuaObject", G.Type("Commandable"));
    }

    // ------------------------------------------------------------ properties

    [Fact]
    public void Read_only_attributes_become_get_only_properties()
    {
        FindClass(G.ApiName("Game")).GetProperty("attributes").EnumerateArray()
            .First(a => a.GetProperty("name").GetString() == "tick");
        Assert.Contains("/// @CSharpLua.Get = \"{this}.tick\"", G.Runtime);
        Assert.Contains("public double Tick { get; }", G.Runtime);
    }

    [Fact]
    public void Read_write_attributes_become_get_set_properties()
    {
        Assert.Contains("/// @CSharpLua.Get = \"{this}.allow_debug_settings\"", G.Runtime);
        Assert.Contains("/// @CSharpLua.Set = \"{this}.allow_debug_settings = {0}\"", G.Runtime);
        Assert.Contains("public bool AllowDebugSettings { get; set; }", G.Runtime);
    }

    [Fact]
    public void Properties_keep_the_native_name_in_the_template_only()
    {
        // The native field is `player`, the C# member is `Player`: the template carries the snake_case
        // name, so CSharp.lua emits `player` and never a get_Player() accessor.
        Assert.Contains("/// @CSharpLua.Get = \"{this}.player\"", G.Type("Entity"));
        Assert.Contains("public Player Player { get; }", G.Type("Entity"));
        // ... and the native method `get_player` stays callable.
        Assert.Contains("/// @CSharpLua.Template = \"{this}.get_player({0})\"", G.Runtime);
        Assert.Contains("public Player GetPlayer(object player)", G.Runtime);
    }

    [Fact]
    public void Api_names_are_converted_to_CSharp_conventions()
    {
        var code = CodeOnly(Cs);
        // no snake_case identifier may survive as a *declared* name
        var declared = Regex.Matches(code, @"public\s[^;{(\n]*?\b(?<name>[A-Za-z_]\w*)\s*(?:\{ get|\()");
        var offenders = declared.Select(m => m.Groups["name"].Value)
            .Where(n => n.Contains('_'))
            .Distinct()
            .ToList();
        Assert.True(offenders.Count == 0, "snake_case declarations: " + string.Join(", ", offenders.Take(10)));

        // properties are PascalCase, parameters camelCase
        Assert.Contains("public double Tick { get; }", G.Runtime);
        Assert.Contains("public void Print(object message)", G.Runtime);
        // 2.0 has one parameter here, 2.1 two; the naming rule is what is under test.
        Assert.Matches(new Regex(@"public ItemFilter GetQuickBarSlot\(double \w+(, double \w+)?\)"), G.Type("Player"));
    }

    // --------------------------------------------------------------- methods

    [Fact]
    public void Methods_are_dot_calls_not_colon_calls()
    {
        // The single most important correctness property: Factorio API methods are called with dot
        // syntax, CSharp.lua would emit `obj:method()` (implicit self) without the template.
        Assert.Contains("/// @CSharpLua.Template = \"{this}.print({0})\"", G.Runtime);
        var templates = Regex.Matches(Cs, @"@CSharpLua\.Template = ""\{this\}\.[a-z_0-9]+\(").Count;
        Assert.True(templates > 900, $"expected ~1000 dot-call method templates, found {templates}");
    }

    [Fact]
    public void Every_instance_member_has_a_template_on_the_preceding_line()
    {
        int checkedMembers = 0;
        foreach (var text in G.Files.Where(f => f.Key.StartsWith("Runtime/Objects/", StringComparison.Ordinal)).Select(f => f.Value))
        {
            var lines = text.Replace("\r\n", "\n").Split('\n');
            for (int i = 1; i < lines.Length; i++)
            {
                var line = lines[i].Trim();
                if (!line.StartsWith("public ", StringComparison.Ordinal)) continue;
                if (line.Contains("static ") || line.Contains("partial class") || line.Contains(" const ")) continue;
                Assert.Matches(@"@CSharpLua\.(Template|Get|Set) = ""\{this\}", lines[i - 1]);
                checkedMembers++;
            }
        }
        Assert.True(checkedMembers > 1500, $"checked only {checkedMembers} instance members");
    }

    [Fact]
    public void Script_registration_methods_keep_the_documented_parameter_order()
    {
        Assert.Contains("/// @CSharpLua.Template = \"{this}.on_init({0})\"", G.Type("Script"));
        Assert.Contains("/// @CSharpLua.Template = \"{this}.on_configuration_changed({0})\"", G.Type("Script"));
        Assert.Contains("/// @CSharpLua.Template = \"{this}.on_event({0}, {1})\"", G.Type("Script"));
        Assert.Contains("/// @CSharpLua.Template = \"{this}.on_event({0}, {1}, {2})\"", G.Type("Script"));
        Assert.Matches(new Regex(@"public void OnEvent\(object @event, Action<EventData> handler, LuaArray<object> filters\)"),
            G.Type("Script"));
    }

    [Fact]
    public void Optional_parameters_produce_overloads_that_omit_the_trailing_arguments()
    {
        // No `= null` tail may reach the Lua call, so each arity gets its own template.
        Assert.Contains("/// @CSharpLua.Template = \"{this}.print({0})\"", G.Runtime);
        Assert.Contains("/// @CSharpLua.Template = \"{this}.print({0}, {1})\"", G.Runtime);
        Assert.Contains("public void Print(object message)", G.Runtime);
        Assert.Contains("public void Print(object message, PrintSettings printSettings)", G.Runtime);
    }

    [Fact]
    public void Variadic_parameters_use_params_and_star_expansion()
    {
        Assert.Contains("/// @CSharpLua.Template = \"{this}.call({0}, {1}, {*2})\"", G.Type("Remote"));
        Assert.Contains("public object Call(string @interface, string function, params object[] args)", G.Type("Remote"));
    }

    [Fact]
    public void Multiple_return_values_become_out_parameters_not_tuples()
    {
        // Tuples deconstruct through CoreSystem (`:Deconstruct()`), out parameters do not.
        // The parameters differ between Factorio releases; the shape - out parameters, no tuple -
        // is what must hold.
        Assert.Matches(new Regex(@"public void ApplyUpgrade\(([^)]*, )?out Entity result, out Entity result2\)"),
            G.Type("Entity"));
        Assert.DoesNotContain("ValueTuple", Cs);
        Assert.DoesNotContain("(Entity, Entity)", Cs);
    }

    [Fact]
    public void Reserved_CSharp_keywords_are_escaped_and_keep_their_Lua_name()
    {
        Assert.Contains("string function", G.Type("Remote"));
        Assert.Contains("object @event", G.Type("Script"));
        Assert.Contains("string @interface", G.Type("Remote"));
    }

    // ----------------------------------------------------------- definitions

    [Fact]
    public void Every_public_member_is_documented()
    {
        // Mirrors the generator's own gate: a `<summary>` on the line above every public declaration.
        var undocumented = new List<string>();
        foreach (var (path, text) in G.Files.Where(f => f.Key.EndsWith(".cs", StringComparison.Ordinal)))
        {
            var lines = text.Replace("\r\n", "\n").Split('\n');
            for (int i = 0; i < lines.Length; i++)
            {
                var t = lines[i].TrimStart();
                if (!t.StartsWith("public ", StringComparison.Ordinal)) continue;
                var documented = i > 0 && lines[i - 1].TrimStart().StartsWith("///", StringComparison.Ordinal);
                if (!documented) undocumented.Add($"{path}:{i + 1}");
            }
        }
        Assert.True(undocumented.Count == 0, "undocumented members: " + string.Join(", ", undocumented.Take(10)));
    }

    [Fact]
    public void Documentation_comes_from_the_official_text_where_available()
    {
        // Game::print has a description in the JSON and must carry it verbatim (minus markdown).
        var described = FindClass(G.ApiName("Game")).GetProperty("methods").EnumerateArray()
            .First(m => m.GetProperty("name").GetString() == "print")
            .GetProperty("description").GetString()!;
        var expected = Docs.ToSummary(described).Substring(0, 40);
        Assert.Contains(expected, G.Runtime);

        // Fallbacks name the native member, so a modder can still look it up.
        Assert.Matches(new Regex("/// <summary>[^<\\n]*<c>[a-z_0-9]+</c>"), Cs);
    }

    [Fact]
    public void Documentation_links_to_the_official_reference()
    {
        // The link follows whichever API version the tests generated from, so this does not pin a
        // documentation release in two places.
        Assert.Contains($"https://lua-api.factorio.com/{TestEnvironment.Version}/runtime-api.html#LuaGameScript", G.Runtime);
    }

    [Fact]
    public void DocCheck_detects_a_missing_summary()
    {
        var dir = Path.Combine(Path.GetTempPath(), "doccheck-" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(dir);
        try
        {
            var file = Path.Combine(dir, "Bad.cs");
            File.WriteAllText(file, "public class A\n{\n    public void Bad() { }\n}\n");
            var findings = DocCheck.Check(new[] { file });
            Assert.Contains(findings, f => f.Member == "Bad");
        }
        finally
        {
            Directory.Delete(dir, true);
        }
    }

    // ---------------------------------------------------------------- unions

    [Fact]
    public void Union_and_nullable_types_are_reported()
    {
        Assert.Contains("union collapsed to object", G.ReportMarkdown);
        Assert.Contains("LuaEventType", G.ReportMarkdown);
    }

    // ---------------------------------------------------- structured tables

    [Fact]
    public void Structured_table_arguments_generate_a_table_building_constructor()
    {
        const string table = "Runtime/Tables/LuaSurfaceCreateEntityParameters.cs";
        Assert.True(G.Files.ContainsKey(table));
        Assert.Contains("public partial class LuaSurfaceCreateEntityParameters", G[table]);
        Assert.Contains("/// @CSharpLua.Get = \"{this}.name\"", G[table]);
        Assert.Contains("public object Name { get; set; }", G[table]);
        // the constructor is a real Lua table literal, not a C# object
        Assert.Matches(new Regex(@"@CSharpLua\.Template = ""\{name = \{0\}"), G[table]);
        Assert.Contains("public Entity CreateEntity(LuaSurfaceCreateEntityParameters parameters)", G.Type("Surface"));
    }

    [Fact]
    public void Optional_value_type_constructor_parameters_default_to_default_not_null()
    {
        const string table = "Runtime/Tables/LuaSurfaceCreateEntityParameters.cs";
        Assert.DoesNotContain("double? speed = null", G[table]);
        Assert.Contains("double? speed = default", G[table]);
    }

    [Fact]
    public void Variant_parameter_groups_are_merged_and_reported()
    {
        Assert.Contains("Surface::create_entity", G.ReportMarkdown);
        Assert.Contains("variant parameter groups merged", G.ReportMarkdown);
        Assert.Contains("ammoInventory", G.Files["Runtime/Tables/LuaSurfaceCreateEntityParameters.cs"]);
    }

    // ---------------------------------------------------------------- events

    [Fact]
    public void Events_generate_data_classes_and_typed_registration_helpers()
    {
        Assert.Contains("public partial class OnPlayerCreatedEvent : EventData", G.Type("OnPlayerCreatedEvent"));
        Assert.Contains("public static void OnPlayerCreated(Action<OnPlayerCreatedEvent> handler)", G.Events);
        Assert.Contains("/// @CSharpLua.Template = \"script.on_event(defines.events.on_player_created, {0})\"", G.Events);
        Assert.Contains("public partial class EventData", G.Type("EventData"));

        var helpers = Regex.Matches(G.Events, @"public static void \w+\(Action<\w+Event> handler\)").Count;
        var eventNames = LoadApi().GetProperty("events").EnumerateArray()
            .Select(e => e.GetProperty("name").GetString()!).ToHashSet();
        var defineNames = LoadApi().GetProperty("defines").EnumerateArray()
            .First(d => d.GetProperty("name").GetString() == "events")
            .GetProperty("values").EnumerateArray()
            .Select(v => v.GetProperty("name").GetString()!).ToHashSet();
        Assert.Equal(eventNames.Intersect(defineNames).Count(), helpers);
        Assert.True(eventNames.Count > helpers, "deprecated script_raised_* events have no define and no helper");
    }

    // --------------------------------------------------------------- defines

    [Fact]
    public void Defines_are_native_lookups_not_inlined_constants()
    {
        Assert.Contains("public static class Defines", G.Defines);
        Assert.Contains("/// @CSharpLua.Get = \"defines.events.on_tick\"", G.Defines);
        Assert.Contains("public static double OnTick { get; }", G.Defines);
        Assert.Contains("/// @CSharpLua.Get = \"defines.direction.north\"", G.Defines);
        var defineLeaves = CountLeaves(LoadApi().GetProperty("defines"));
        var emitted = Regex.Matches(G.Defines, "@CSharpLua\\.Get = \"defines").Count;
        Assert.Equal(defineLeaves, emitted);
    }

    private static int CountLeaves(JsonElement defines) =>
        defines.EnumerateArray().Sum(d =>
            (d.TryGetProperty("values", out var v) ? v.GetArrayLength() : 0) +
            (d.TryGetProperty("subkeys", out var s) ? CountLeaves(s) : 0));

    // ------------------------------------------------------------ reporting

    [Fact]
    public void Report_covers_every_stage_and_severity()
    {
        using var doc = JsonDocument.Parse(G.ReportJson);
        var root = doc.RootElement;
        Assert.Equal(6, root.GetProperty("apiVersion").GetInt32());
        var diags = root.GetProperty("diagnostics").EnumerateArray().ToList();
        Assert.NotEmpty(diags);
        Assert.All(diags, d =>
        {
            Assert.Contains(d.GetProperty("severity").GetString(), new[] { "info", "degraded", "skipped", "error" });
            Assert.False(string.IsNullOrWhiteSpace(d.GetProperty("member").GetString()));
            Assert.False(string.IsNullOrWhiteSpace(d.GetProperty("message").GetString()));
        });
        Assert.Contains(diags, d => d.GetProperty("severity").GetString() == "degraded");
        Assert.DoesNotContain(diags, d => d.GetProperty("severity").GetString() == "error");
        Assert.Contains("degraded", G.ReportMarkdown);
    }

    [Fact]
    public void Unsupported_api_version_is_rejected_instead_of_misgenerated()
    {
        // api_version 5 used `type`/`read`/`write` instead of `read_type`/`write_type`; accepting it
        // silently would emit attributes with no types.
        var tmp = Path.Combine(Path.GetTempPath(), "factorio-api-v5-" + Guid.NewGuid().ToString("N") + ".json");
        try
        {
            var text = File.ReadAllText(TestEnvironment.RuntimeJson);
            var patched = Regex.Replace(text, "\"api_version\"\\s*:\\s*6", "\"api_version\": 5", RegexOptions.None, TimeSpan.FromSeconds(5));
            Assert.Contains("\"api_version\": 5", patched);
            File.WriteAllText(tmp, patched);

            var r = TestEnvironment.RunGenerator(runtimeJson: tmp);
            Assert.NotEqual(0, r.ExitCode);
            Assert.Contains("unsupported api_version 5", r.StandardError);
        }
        finally
        {
            File.Delete(tmp);
        }
    }

    [Fact]
    public void Generator_is_deterministic()
    {
        var again = TestEnvironment.RunGenerator();
        Assert.Equal(G.Files.Keys.OrderBy(k => k), again.Files.Keys.OrderBy(k => k));
        foreach (var path in G.Files.Keys)
            Assert.Equal(G.Files[path], again.Files[path]);
    }

    [Fact]
    public void Prototype_stage_is_generated_separately()
    {
        Assert.Contains("namespace Factorio.Prototypes", G.Prototype);
        Assert.Contains("/// @CSharpLua.Get = \"data\"", G.Prototype);
        Assert.Contains("partial class ItemPrototype", G.Prototype);
        Assert.DoesNotContain("class Entity ", G.Prototype);
    }
}
