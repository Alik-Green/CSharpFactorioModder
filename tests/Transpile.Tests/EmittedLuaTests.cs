using System.Text.RegularExpressions;
using Xunit;

namespace Factorio.Bindings.TranspileTests;

/// <summary>
/// End-to-end tests through the *real* pinned CSharp.lua compiler. They assert on the Lua that
/// would actually be shipped, which is the only thing Factorio executes.
/// </summary>
[Collection("transpiler")]
public class EmittedLuaTests
{
    private readonly TranspilerFixture _f;

    public EmittedLuaTests(TranspilerFixture f) => _f = f;

    [Fact]
    public void Compiler_accepts_the_generated_bindings_and_the_fixture()
    {
        Assert.True(_f.Result.ExitCode == 0,
            $"CSharp.lua failed ({_f.Result.ExitCode}):\n{_f.Result.StdOut}\n{_f.Result.StdErr}");
        Assert.Contains("Constructs.lua", _f.Result.Files);
    }

    [Fact]
    public void Compiler_accepts_the_example_mod()
    {
        var r = _f.Run(_f.Bindings.Concat(new[] { _f.Compat, _f.ExampleMod }));
        Assert.True(r.ExitCode == 0, $"CSharp.lua failed:\n{r.StdOut}\n{r.StdErr}");
        var lua = r.Read("HelloFactorio.lua");

        // the natives are used exactly as in hand written Lua
        Assert.Contains("game.print(\"Hello from C#! This mod was compiled to Lua by factorio-mod.\")", lua);
        Assert.Contains("script.on_init(", lua);
        Assert.Contains("script.on_event(defines.events.on_tick, ", lua);
        Assert.Contains("script.on_event(defines.events.on_player_joined_game, ", lua);
        Assert.Contains("game.get_player(1)", lua);
        Assert.Contains("game.speed = 1", lua);
        Assert.Contains("player.character", lua);
    }

    [Fact]
    public void Native_methods_are_called_with_dot_syntax_never_colon_syntax()
    {
        // CSharp.lua's default for an instance method is `obj:method(...)`, which passes `self` as
        // the first argument. Factorio's API takes dot calls, so a single colon call is a bug.
        var lua = _f.Result.Read("Constructs.lua");
        foreach (var native in new[] { "game", "script", "remote", "player", "force", "surface", "entities" })
        {
            Assert.DoesNotContain(native + ":", lua);
            Assert.DoesNotMatch(new Regex($@"\b{native}\s*:\s*\w+\s*\("), lua);
        }
        Assert.Contains("game.print(\"hello\")", lua);
        Assert.Contains("game.get_player(1)", lua);
    }

    [Fact]
    public void Emitted_lua_has_no_core_system_dependencies()
    {
        foreach (var file in _f.Result.Files)
        {
            var bad = LuaChecks.UnsupportedSystemCalls(_f.Result.Read(file));
            Assert.True(bad.Count == 0, $"{file} requires CoreSystem: {string.Join(", ", bad)}");
        }
    }

    [Fact]
    public void Emitted_lua_parses_as_lua_52()
    {
        foreach (var file in _f.Result.Files)
        {
            var (ok, message) = LuaChecks.ParseLua52(Path.Combine(_f.Result.OutDir, file));
            Assert.True(ok, $"{file} is not valid Lua 5.2: {message}");
        }
    }

    [Fact]
    public void Optional_parameters_do_not_emit_a_trailing_nil()
    {
        var lua = _f.Result.Read("Constructs.lua");
        Assert.Contains("player.print(\"hi\")", lua);
        Assert.Contains("game.print(\"hello\")", lua);
        Assert.DoesNotContain("player.print(\"hi\", nil)", lua);
        Assert.DoesNotContain("game.print(\"hello\", nil)", lua);
    }

    [Fact]
    public void Structured_tables_are_lua_table_literals()
    {
        var lua = _f.Result.Read("Constructs.lua");
        // created through the generated ctor template, not through a C# class
        Assert.Matches(new Regex(@"create_entity\(\{"), lua);
        Assert.DoesNotContain("LuaSurfaceCreateEntityParameters", lua);
    }

    [Fact]
    public void Multiple_returns_become_multiple_lua_assignment()
    {
        var lua = _f.Result.Read("Constructs.lua");
        Assert.Matches(new Regex(@"local \w+, \w+ = inventory\.find_empty_stack\("), lua);
        Assert.DoesNotContain(":Deconstruct()", lua);
    }

    [Fact]
    public void Arrays_use_native_indexing_and_length()
    {
        var lua = _f.Result.Read("Constructs.lua");
        Assert.Contains("#entities", lua);
        Assert.Matches(new Regex(@"entities\[\w+\]"), lua);
        Assert.DoesNotContain(":get(", lua);
        Assert.DoesNotContain(":getLength()", lua);
    }

    [Fact]
    public void Variadic_arguments_are_forwarded_flat()
    {
        var lua = _f.Result.Read("Constructs.lua");
        Assert.Contains("remote.call(\"other-mod\", \"ping\", 1, 2, \"three\")", lua);
        Assert.DoesNotContain("remote.call(\"other-mod\", \"ping\", {", lua);
    }

    [Fact]
    public void Defines_are_read_from_the_native_table()
    {
        var lua = _f.Result.Read("Constructs.lua");
        Assert.Contains("defines.events.on_tick", lua);
        Assert.Contains("defines.direction.north", lua);
    }

    [Fact]
    public void Utility_handles_compile_to_plain_names_and_lua_calls()
    {
        // A registry, a value handle and a setting are names, not objects: declaring one must not
        // allocate a table, and every operation must be a call into FactorioUtils.lua.
        var lua = _f.Result.Read("Constructs.lua");
        Assert.Contains("machines = \"fixture.machines\"", lua);
        Assert.Contains("gas = util_registry_value(machines, \"gas\")", lua);
        Assert.Contains("preferred = \"fixture.preferred\"", lua);
        Assert.Contains("util_registry_add(storage, machines, unit)", lua);
        Assert.Contains("util_value_set(storage, gas, unit, \"light-oil\")", lua);
        Assert.Contains("util_value_get(storage, gas, unit)", lua);
        Assert.Contains("util_setting_set(storage, preferred, read)", lua);
    }

    [Fact]
    public void Lifecycle_helpers_subscribe_by_event_name()
    {
        var lua = _f.Result.Read("Constructs.lua");
        Assert.Contains("util_on_init(", lua);
        Assert.Contains("util_on_built(\"gas-extractor\", function (machine, builder)", lua);
        Assert.Contains("util_on_removed(\"gas-extractor\", function (machine)", lua);
        Assert.Contains("util_every_second(", lua);
        Assert.Contains("util_command(\"fixture\", \"fixture.command-help\", function (command)", lua);
    }

    [Fact]
    public void Recipe_helpers_compile_to_the_documented_lua()
    {
        var lua = _f.Result.Read("Constructs.lua");
        Assert.Contains("util_machine_recipe(unit)", lua);
        Assert.Contains("util_machine_is_open(unit)", lua);
        Assert.Contains("(unit.products_finished or 0)", lua);
        Assert.Contains("util_machine_set_recipe(unit, recipe)", lua);
    }

    [Fact]
    public void Generated_types_are_not_emitted_as_lua_classes()
    {
        // The bindings must be compile-time only: no class tables, no constructors, no metatables.
        var lua = _f.Result.Read("Game.lua");
        Assert.DoesNotContain("namespace.class(\"", lua);
        Assert.DoesNotContain("namespace.struct(\"", lua);
        Assert.DoesNotContain("__ctor__", lua);
        Assert.DoesNotContain("setmetatable", lua);
    }

    [Fact]
    public void Golden_output_of_the_example_mod_is_stable()
    {
        var r = _f.Run(_f.Bindings.Concat(new[] { _f.Compat, _f.ExampleMod }));
        Assert.True(r.ExitCode == 0, r.StdOut + r.StdErr);

        var golden = Path.Combine(TranspilerFixture.Root, "tests/golden/HelloFactorio.lua");
        var actual = Normalize(r.Read("HelloFactorio.lua"));
        if (!File.Exists(golden))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(golden)!);
            File.WriteAllText(golden, actual);
            Assert.Fail("golden file did not exist and was created; re-run the test");
        }
        Assert.Equal(File.ReadAllText(golden).Replace("\r\n", "\n"), actual);
    }

    [Fact]
    public void Prototype_stage_transpiles_to_a_data_table_access()
    {
        var r = _f.Run(_f.PrototypeBindings.Concat(new[]
        {
            _f.Compat,
            Path.Combine(TranspilerFixture.Root, "tests/fixtures/DataStage.cs"),
        }));
        Assert.True(r.ExitCode == 0, $"CSharp.lua failed:\n{r.StdOut}\n{r.StdErr}");

        var lua = r.Read("DataStage.lua");
        Assert.Contains("data.raw[\"item\"]", lua);
        Assert.Contains("data.raw[\"recipe\"]", lua);
        Assert.Contains("log(\"iron-plate exists\")", lua);
        // the data stage must not reference the runtime globals
        Assert.DoesNotContain("game.", lua);
        Assert.DoesNotContain("script.", lua);

        var (ok, message) = LuaChecks.ParseLua52(Path.Combine(r.OutDir, "DataStage.lua"));
        Assert.True(ok, "DataStage.lua is not valid Lua 5.2: " + message);
        var bad = LuaChecks.UnsupportedSystemCalls(r.Read("DataStage.lua"));
        Assert.True(bad.Count == 0, "CoreSystem required: " + string.Join(", ", bad));
    }

    /// <summary>Strips the compiler banner so the golden file only pins semantic content.</summary>
    private static string Normalize(string lua)
    {
        var lines = lua.Replace("\r\n", "\n").Split('\n').ToList();
        while (lines.Count > 0 && (lines[0].StartsWith("--", StringComparison.Ordinal) || lines[0].Trim().Length == 0))
            lines.RemoveAt(0);
        return string.Join("\n", lines);
    }
}
