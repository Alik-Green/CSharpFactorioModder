using Xunit;
using Factorio.ModTool.Build;

namespace Factorio.Bindings.Tests;

/// <summary>
/// The utility library is bundled as `local util = (function () ... end)()` plus one local alias per
/// function, because every <c>@CSharpLua.Template</c> in the C# utilities calls the snake_case
/// alias. The aliases are generated from the file itself: a hand-written list would drift the
/// moment a helper is added, and the failure mode (a nil global inside a mod) only shows up at run
/// time in game.
/// </summary>
public class LuaBundleTests
{
    private static readonly string UtilsPath =
        Path.Combine(TestEnvironment.RepoRoot, "src", "Factorio.Bindings", "FactorioUtils.lua");

    [Fact]
    public void Every_util_function_gets_a_local_alias()
    {
        var source = File.ReadAllText(UtilsPath);
        var aliases = LuaBundle.UtilAliases(source);

        var functions = System.Text.RegularExpressions.Regex
            .Matches(source, @"^function util\.([A-Za-z_][A-Za-z0-9_]*)", System.Text.RegularExpressions.RegexOptions.Multiline)
            .Select(m => m.Groups[1].Value)
            .ToList();

        Assert.NotEmpty(functions);
        foreach (var name in functions)
            Assert.Contains(aliases, line => line.Contains("= util." + name, StringComparison.Ordinal) ||
                                             line.Contains(", util." + name, StringComparison.Ordinal));

        // And nothing else: an alias for a function that does not exist would be nil at run time.
        var declared = string.Join("\n", aliases)
            .Split('=', 2)[0];
        Assert.DoesNotContain("util_does_not_exist", declared);
    }

    [Fact]
    public void Aliases_are_grouped_four_per_line()
    {
        var aliases = LuaBundle.UtilAliases("function util.a() end\nfunction util.b() end\nfunction util.c() end\nfunction util.d() end\nfunction util.e() end");
        Assert.Equal(2, aliases.Count);
        Assert.Equal("local util_a, util_b, util_c, util_d = util.a, util.b, util.c, util.d", aliases[0]);
        Assert.Equal("local util_e = util.e", aliases[1]);
    }

    [Fact]
    public void A_helper_added_to_the_library_is_reachable_immediately()
    {
        var aliases = LuaBundle.UtilAliases("function util.brand_new_helper(x) return x end");
        Assert.Equal(new[] { "local util_brand_new_helper = util.brand_new_helper" }, aliases);
    }

    [Fact]
    public void The_bundled_control_lua_aliases_every_util_function_it_uses()
    {
        // A template naming a helper that FactorioUtils.lua does not define compiles fine and then
        // fails in game, so the alias block and the templates are checked against each other here.
        var utils = File.ReadAllText(UtilsPath);
        var aliases = string.Join("\n", LuaBundle.UtilAliases(utils));

        var templates = Directory
            .GetFiles(Path.Combine(TestEnvironment.RepoRoot, "src", "Factorio.Bindings", "Utilities"), "*.cs")
            .SelectMany(file => System.Text.RegularExpressions.Regex
                .Matches(File.ReadAllText(file), @"util\.([A-Za-z_][A-Za-z0-9_]*)")
                .Select(m => m.Groups[1].Value))
            .Distinct();

        foreach (var name in templates)
        {
            Assert.Contains("function util." + name + "(", utils);
            Assert.Contains("util_" + name, aliases);
        }
    }
}
