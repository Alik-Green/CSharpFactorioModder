using System.Text.Json;
using Factorio.ModTool.Commands;
using Xunit;

namespace Factorio.Bindings.Tests;

/// <summary>
/// The <c>dotnet new</c> template in <c>templates/dotnet/factorio-mod/</c>.
///
/// It is what makes "Factorio mod (C#)" appear in Rider's New Solution dialog - that dialog is driven
/// by the .NET template engine - so it has to be a valid template package and it has to produce a
/// mod that builds. Both are checked here: the first by reading the descriptor, the second by
/// running the template and handing the result to the same checks the tool runs.
/// </summary>
public class TemplatePackTests
{
    private static string TemplateDirectory => Path.Combine(TestEnvironment.RepoRoot, TemplateCommand.TemplateDirectory);

    /// <summary>The descriptor is valid JSON and names a template with the mod's short name.</summary>
    [Fact]
    public void The_template_descriptor_is_complete()
    {
        var path = Path.Combine(TemplateDirectory, ".template.config", "template.json");
        using var document = JsonDocument.Parse(File.ReadAllText(path));
        var root = document.RootElement;

        Assert.Equal("factorio-mod", root.GetProperty("shortName").GetString());
        Assert.Equal("FactorioMod", root.GetProperty("sourceName").GetString());
        Assert.Equal("project", root.GetProperty("tags").GetProperty("type").GetString());
        Assert.Equal("C#", root.GetProperty("tags").GetProperty("language").GetString());

        // Every parameter a user can pass, so a rename is caught here rather than in a wizard.
        var symbols = root.GetProperty("symbols");
        Assert.True(symbols.TryGetProperty("title", out _));
        Assert.True(symbols.TryGetProperty("modAuthor", out _));
        Assert.Equal("2.0", symbols.GetProperty("factorio").GetProperty("defaultValue").GetString());
    }

    /// <summary>
    /// The placeholders only exist as placeholders: nothing in the template still says
    /// <c>{{</c>, and the tokens the descriptor replaces really occur in the files.
    /// </summary>
    [Fact]
    public void The_template_sources_use_the_tokens_the_descriptor_declares()
    {
        var files = Directory.EnumerateFiles(TemplateDirectory, "*", SearchOption.AllDirectories)
            .Where(f => !f.Contains(".template.config"))
            .ToList();
        Assert.NotEmpty(files);

        foreach (var file in files)
        {
            // The template engine's own syntax is a bare replacement, so the old {{name}} style
            // from templates/mod/ must not leak into a template package.
            Assert.DoesNotContain("{{", File.ReadAllText(file));
        }

        var modJson = File.ReadAllText(Path.Combine(TemplateDirectory, "mod.json"));
        Assert.Contains("\"name\": \"FactorioMod\"", modJson);
        Assert.Contains("\"title\": \"ModDisplayTitle\"", modJson);
        Assert.Contains("\"author\": \"ModAuthor\"", modJson);
        Assert.Contains("\"factorioVersion\": \"ModFactorioVersion\"", modJson);
    }

    /// <summary>
    /// The project file has to survive the template engine's name replacement: everything it
    /// contains is renamed to the project name, so its own identifiers must not be the mod's name.
    /// </summary>
    [Fact]
    public void The_template_project_has_no_names_that_the_rename_would_break()
    {
        var project = File.ReadAllText(Path.Combine(TemplateDirectory, "FactorioMod.csproj"));

        Assert.DoesNotContain("FactorioMod", project);
        Assert.Contains("Target Name=\"FactorioBuild\"", project);
        Assert.Contains("FACTORIO_MOD_REPO", project);
        Assert.Contains("config/toolchain.json", project);
    }

    /// <summary>
    /// Instantiating it produces a mod the tool accepts: same mod.json keys the tool reads, a
    /// namespace that matches the entry point, and a data stage the build can run.
    /// </summary>
    [Fact]
    public void The_instantiated_template_is_a_mod_that_builds()
    {
        var directory = Path.Combine(Path.GetTempPath(), "factorio-template-" + Guid.NewGuid().ToString("N"));

        try
        {
            // The same substitution `dotnet new` does: the project name replaces the source name.
            Copy(TemplateDirectory, directory, "FactorioMod", "AirFilter");
            Replace(directory, "ModDisplayTitle", "Air Filter");
            Replace(directory, "ModAuthor", "someone");
            Replace(directory, "ModFactorioVersion", "2.0");

            var project = Factorio.ModTool.Build.ModProject.Load(directory);
            Assert.Equal("AirFilter", project.Name);
            Assert.Equal("Air Filter", project.Title);
            Assert.Equal("AirFilter.Main.Init", project.Entry);
            Assert.Equal("AirFilter.ExampleMod", project.DataEntry);
            Assert.True(project.Build.Zip);

            var main = File.ReadAllText(Path.Combine(directory, "src", "Main.cs"));
            Assert.Contains("namespace AirFilter", main);
            Assert.Contains("public static void Init()", main);

            // The data stage compiles and produces prototypes - the build itself would, but this is
            // the part that would fail if a rename broke an identifier.
            var result = Factorio.ModTool.Data.DataEntryGenerator.Generate(
                project.DataEntry, project.DataSourceFiles(), project.FactorioVersion, project.Name);
            Assert.Equal(2, result.Prototypes.Count);
            Assert.Contains("AirFilter-token", result.Lua);
        }
        finally
        {
            try { Directory.Delete(directory, true); } catch { /* best effort */ }
        }
    }

    /// <summary>Copies a folder, renaming the source name in paths and in file contents.</summary>
    private static void Copy(string source, string destination, string sourceName, string newName)
    {
        Directory.CreateDirectory(destination);
        foreach (var file in Directory.EnumerateFiles(source, "*", SearchOption.AllDirectories))
        {
            var relative = Path.GetRelativePath(source, file)
                .Replace(".template.config" + Path.DirectorySeparatorChar, ".template-config-")
                .Replace(sourceName, newName);

            var target = Path.Combine(destination, relative);
            Directory.CreateDirectory(Path.GetDirectoryName(target)!);
            File.WriteAllText(target, File.ReadAllText(file).Replace(sourceName, newName));
        }
    }

    /// <summary>Replaces a token in every file of a folder.</summary>
    private static void Replace(string directory, string token, string value)
    {
        foreach (var file in Directory.EnumerateFiles(directory, "*", SearchOption.AllDirectories))
            File.WriteAllText(file, File.ReadAllText(file).Replace(token, value));
    }
}
