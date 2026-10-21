using Factorio.ModTool;
using Factorio.ModTool.Build;
using Factorio.ModTool.Commands;
using Xunit;

namespace Factorio.Bindings.Tests;

/// <summary>
/// Everything that used to have to be remembered as a command-line flag, written in the mod's
/// mod.json instead.
///
/// The point is that an IDE can build a mod with one fixed command - `factorio-mod build &lt;dir&gt;` -
/// and still respect what that mod needs, because the mod says so itself. A flag on the command
/// line still wins, for the one-off `--no-zip` case.
/// </summary>
public class ProjectSettingsTests
{
    /// <summary>A mod.json with a build object is read into the project.</summary>
    [Fact]
    public void The_build_object_is_read_from_mod_json()
    {
        using var mod = Buildable("""
            "build": {
              "output": "out",
              "zip": false,
              "format": "msbuild",
              "lua51": true
            }
            """);

        var project = ModProject.Load(mod.Directory);
        Assert.Equal("out", project.Build.Output);
        Assert.False(project.Build.Zip);
        Assert.Equal("msbuild", project.Build.Format);
        Assert.True(project.Build.Lua51);

        // Anything the object does not mention stays unset, so the default still applies.
        Assert.Null(project.Build.Analyze);
        Assert.Null(project.Build.Validate);
    }

    /// <summary>Without a build object, every setting is the toolchain's default.</summary>
    [Fact]
    public void A_mod_without_a_build_object_uses_the_defaults()
    {
        using var mod = Buildable("");
        var project = ModProject.Load(mod.Directory);

        var options = BuildCommand.OptionsFor(project, Array.Empty<string>());
        Assert.True(options.Zip);
        Assert.True(options.Analyze);
        Assert.True(options.Validate);
        Assert.True(options.Assets);
        Assert.False(options.ClassicLua);
        Assert.Null(options.OutputDirectory);
    }

    /// <summary>mod.json decides, when the command line says nothing.</summary>
    [Fact]
    public void Mod_json_overrides_the_defaults()
    {
        using var mod = Buildable("""
            "build": { "zip": false, "analyze": false, "assets": false, "validate": false, "output": "out" }
            """);

        var options = BuildCommand.OptionsFor(ModProject.Load(mod.Directory), Array.Empty<string>());

        Assert.False(options.Zip);
        Assert.False(options.Analyze);
        Assert.False(options.Assets);
        Assert.False(options.Validate);

        // A relative output in mod.json is relative to the mod, so building it from anywhere writes
        // to the same place.
        Assert.Equal(Path.Combine(mod.Directory, "out"), options.OutputDirectory);
    }

    /// <summary>The command line wins over mod.json, which is what makes a one-off build possible.</summary>
    [Fact]
    public void A_flag_overrides_mod_json()
    {
        using var mod = Buildable(""" "build": { "zip": false, "analyze": false } """);
        var project = ModProject.Load(mod.Directory);

        Assert.True(BuildCommand.OptionsFor(project, new[] { "--zip" }).Zip);
        Assert.True(BuildCommand.OptionsFor(project, new[] { "--analyze" }).Analyze);
        Assert.False(BuildCommand.OptionsFor(project, new[] { "--no-analyze" }).Analyze);
    }

    /// <summary>A misspelled format is caught when the mod is loaded, not halfway through a build.</summary>
    [Fact]
    public void An_unknown_format_is_rejected()
    {
        using var mod = Buildable(""" "build": { "format": "json" } """);

        var error = Assert.Throws<ToolException>(() => ModProject.Load(mod.Directory));
        Assert.Contains("build.format", error.Message);
    }

    // ------------------------------------------------------------------ the scaffold

    /// <summary>
    /// A new mod has a .csproj named after it, so opening the folder in Rider gives a project whose
    /// Build button builds the mod.
    /// </summary>
    [Fact]
    public void The_template_writes_a_csproj_named_after_the_mod()
    {
        var directory = Path.Combine(Path.GetTempPath(), "factorio-csproj-" + Guid.NewGuid().ToString("N"));
        try
        {
            var written = ModTemplate.Create(TestEnvironment.RepoRoot, directory, "air-extractor", "someone", overwrite: false);

            Assert.Contains("air-extractor.csproj", written);

            var project = File.ReadAllText(Path.Combine(directory, "air-extractor.csproj"));
            Assert.Contains("<Target Name=\"FactorioBuild\" AfterTargets=\"Build\"", project);
            Assert.Contains("factorio-mod.dll", project);
            Assert.Contains("factorio-mod.cmd", project);
            Assert.Contains("DesignTimeBuild", project);
            Assert.DoesNotContain("{{", project);

            // New projects must not need a checkout: without one, the tool comes from PATH, which is
            // what the installer sets up. Both paths are in the same file, so assert both.
            Assert.Contains("Exists('$(FactorioRepo)/config/toolchain.json')", project);
            Assert.Contains("Condition=\"'$(FactorioToolCommand)' == ''\">factorio-mod</FactorioToolCommand>", project);
        }
        finally
        {
            try { Directory.Delete(directory, true); } catch { /* best effort */ }
        }
    }

    /// <summary>The title and version given to `new` reach mod.json.</summary>
    [Fact]
    public void The_template_takes_a_title_and_a_version()
    {
        var directory = Path.Combine(Path.GetTempPath(), "factorio-title-" + Guid.NewGuid().ToString("N"));
        try
        {
            ModTemplate.Create(TestEnvironment.RepoRoot, directory, "air-extractor", "someone", true,
                "2.1", "Air Extractor Deluxe", "1.4.2");

            var project = ModProject.Load(directory);
            Assert.Equal("Air Extractor Deluxe", project.Title);
            Assert.Equal("1.4.2", project.Version);
            Assert.Equal("2.1", project.FactorioVersion);
        }
        finally
        {
            try { Directory.Delete(directory, true); } catch { /* best effort */ }
        }
    }

    /// <summary>The shipped mods carry the project that makes the IDE's Build button work.</summary>
    [Theory]
    [InlineData("HelloFactorio")]
    [InlineData("GasExtractor")]
    [InlineData("DemoChemistry")]
    public void Every_shipped_mod_has_a_csproj(string name)
    {
        var directory = Path.Combine(TestEnvironment.RepoRoot, "mods", name);
        var csproj = Path.Combine(directory, name + ".csproj");

        Assert.True(File.Exists(csproj), $"{name}.csproj is missing");
        var project = File.ReadAllText(csproj);
        Assert.Contains("<Target Name=\"FactorioBuild\"", project);
        Assert.Contains("Condition=\"'$(FactorioToolCommand)' == ''\">factorio-mod</FactorioToolCommand>", project);
    }

    /// <summary>A mod.json that builds, with an optional extra "build" property.</summary>
    private static Scratch Buildable(string buildProperty) => new(buildProperty);

    /// <summary>A throwaway mod folder, written by hand so a test controls every byte.</summary>
    private sealed class Scratch : IDisposable
    {
        public Scratch(string buildProperty)
        {
            Directory = Path.Combine(Path.GetTempPath(), "factorio-settings-" + Guid.NewGuid().ToString("N"));
            System.IO.Directory.CreateDirectory(Path.Combine(Directory, "src"));
            File.WriteAllText(Path.Combine(Directory, "src", "Main.cs"),
                "namespace T { public static class Main { public static void Init() { } } }");

            var comma = buildProperty.Trim().Length == 0 ? "" : ",\n  ";
            File.WriteAllText(Path.Combine(Directory, "mod.json"), $$"""
                {
                  "name": "settings-test",
                  "title": "Settings Test",
                  "version": "0.1.0",
                  "author": "tests",
                  "entry": "T.Main.Init",
                  "sources": [ "src" ]{{comma}}{{buildProperty}}
                }
                """);
        }

        public string Directory { get; }

        public void Dispose()
        {
            try { System.IO.Directory.Delete(Directory, true); } catch { /* best effort */ }
        }
    }
}
