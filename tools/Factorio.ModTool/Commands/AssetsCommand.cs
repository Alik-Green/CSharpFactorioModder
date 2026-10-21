using Factorio.ModTool.Assets;
using Factorio.ModTool.Build;

namespace Factorio.ModTool.Commands;

/// <summary>
/// `factorio-mod assets [mod-dir...] [--force]`: render the models named by each mod's
/// <c>assets/render.json</c> into sprite PNGs and the Lua sprite table that references them.
/// </summary>
public static class AssetsCommand
{
    public static int Run(ToolchainSettings settings, string[] args)
    {
        var positional = CommandLine.Positional(args);
        var force = args.Contains("--force");

        var directories = positional.Count > 0
            ? positional
            : Directory.Exists(settings.ModsDirectory)
                ? Directory.EnumerateDirectories(settings.ModsDirectory)
                    .Where(d => File.Exists(Path.Combine(d, "assets", "render.json")))
                    .OrderBy(d => d, StringComparer.Ordinal)
                    .ToList()
                : new List<string>();

        if (directories.Count == 0)
            throw new ToolException("nothing to render: no mod has an assets/render.json");

        foreach (var directory in directories)
        {
            var project = ModProject.Load(directory);
            Console.WriteLine($"assets {project.Name} {project.Version}");
            var result = AssetPipeline.Run(settings, project, force, line => Console.WriteLine("  " + line));
            Console.WriteLine($"  {result.Rendered} rendered, {result.Skipped} up to date, {result.Outputs.Count} file(s)");
        }

        return 0;
    }
}
