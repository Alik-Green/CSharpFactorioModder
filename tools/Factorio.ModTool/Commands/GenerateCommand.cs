using Factorio.ModTool.Api;

namespace Factorio.ModTool.Commands;

/// <summary>
/// `factorio-mod generate [--factorio 2.0|2.1] [--all] [--out DIR]`: turn the official JSON into C#.
/// Writes the api-report next to the sources and fails if any public member would ship without
/// documentation.
///
/// Each supported Factorio version gets its own tree - `Generated/&lt;docsVersion&gt;/` - because the
/// API differs between them: a 2.0 mod must not compile against 2.1-only members.
/// </summary>
public static class GenerateCommand
{
    public static int Run(ToolchainSettings settings, string[] args)
    {
        var targets = args.Contains("--all")
            ? settings.Targets()
            : new[] { FetchApiCommand.SelectedTarget(settings, args) }.ToList();

        var failures = 0;
        foreach (var target in targets)
        {
            var output = ArgValue(args, "--out") ?? target.GeneratedDirectory;

            Console.WriteLine($"generate Factorio {target.DocsVersion} ({target.ModVersion})");
            var result = BindingsGenerator.Generate(
                target.ApiDirectory, output, target.DocsVersion, target.SchemaVersion);
            Console.WriteLine($"  {result.Stats.Files} files: {result.Stats.Classes} classes, {result.Stats.Methods} methods, " +
                              $"{result.Stats.Attributes} attributes, {result.Stats.Globals} globals, " +
                              $"{result.Stats.Defines} define leaves, {result.Stats.Events} events, " +
                              $"{result.Stats.TableClasses} table classes");
            Console.WriteLine($"  diagnostics: {result.Stats.Errors} error, {result.Stats.Skipped} skipped, " +
                              $"{result.Stats.Degraded} degraded, {result.Stats.Info} info (api-report.md)");
            Console.WriteLine("  wrote " + output);
            if (result.ExitCode != 0) failures++;
        }

        return failures == 0 ? 0 : 1;
    }

    internal static string? ArgValue(string[] args, string name)
    {
        for (var i = 0; i < args.Length - 1; i++)
            if (args[i] == name) return args[i + 1];
        return null;
    }
}
