using Factorio.Analyzers;
using Factorio.ModTool.Build;

namespace Factorio.ModTool.Commands;

/// <summary>
/// `factorio-mod analyze [mod-dir...]`: run the Factorio analyzers over one mod, or over every
/// `mods/*/mod.json`, without building anything. Exits non-zero when a rule of error severity fires.
/// </summary>
public static class AnalyzeCommand
{
    public static int Run(ToolchainSettings settings, string[] args)
    {
        var positional = CommandLine.Positional(args);
        var directories = positional.Count > 0
            ? positional
            : Directory.Exists(settings.ModsDirectory)
                ? Directory.EnumerateDirectories(settings.ModsDirectory)
                    .Where(d => File.Exists(Path.Combine(d, "mod.json")))
                    .OrderBy(d => d, StringComparer.Ordinal)
                    .ToList()
                : new List<string>();

        if (directories.Count == 0) throw new ToolException("nothing to analyse: pass a mod directory");

        var errors = 0;
        var warnings = 0;
        foreach (var directory in directories)
        {
            // A mod directory is analysed through its manifest; any other directory (the bindings,
            // for instance) is analysed as a plain tree of C# files.
            string name;
            string label;
            IReadOnlyList<string> files;
            if (File.Exists(Path.Combine(directory, "mod.json")))
            {
                var project = ModProject.Load(directory);
                name = project.Name;
                label = $"{project.Name} {project.Version}";
                files = project.SourceFiles().ToList();
            }
            else if (Directory.Exists(directory))
            {
                name = Path.GetFileName(Path.TrimEndingDirectorySeparator(Path.GetFullPath(directory)));
                label = directory;
                files = Directory.EnumerateFiles(directory, "*.cs", SearchOption.AllDirectories)
                    .Where(f => !f.Contains($"{Path.DirectorySeparatorChar}obj{Path.DirectorySeparatorChar}") &&
                                !f.Contains($"{Path.DirectorySeparatorChar}bin{Path.DirectorySeparatorChar}"))
                    .OrderBy(f => f, StringComparer.Ordinal)
                    .ToList();
            }
            else
            {
                throw new ToolException($"no such directory: {directory}");
            }

            var findings = Analysis.Run(files);
            Console.WriteLine($"analyze {label}: {files.Count} file(s), {findings.Count} finding(s)");

            foreach (var finding in findings)
            {
                if (finding.IsError) errors++; else warnings++;
                Console.WriteLine($"  {finding}");
            }
        }

        Console.WriteLine($"  {errors} error(s), {warnings} warning(s)");
        return errors == 0 ? 0 : 1;
    }
}
