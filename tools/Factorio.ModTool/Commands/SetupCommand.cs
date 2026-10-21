namespace Factorio.ModTool.Commands;

/// <summary>
/// `factorio-mod setup`: everything a fresh checkout needs, in one command - the pinned compiler,
/// the API documents and the generated bindings. It is the same three steps as
/// `make bootstrap api generate`, without needing make, and it can be run from any directory
/// (the repository is found from <c>--repo</c>, <c>FACTORIO_REPO</c> or the tool's own location).
///
/// Each step is skipped when its output already exists, so re-running is cheap and safe.
/// </summary>
public static class SetupCommand
{
    public static int Run(ToolchainSettings settings, string[] args)
    {
        var offline = args.Contains("--offline");
        var update = args.Contains("--update");
        var force = args.Contains("--force");

        Console.WriteLine("setup " + settings.Root);
        Console.WriteLine();

        // 1. the compiler ---------------------------------------------------------------
        if (Directory.Exists(settings.CompilerCloneDir) && File.Exists(settings.CompilerDll) && !force)
        {
            Console.WriteLine("compiler   already built (" + Shorten(settings.Root, settings.CompilerDll) + ")");
        }
        else
        {
            if (BootstrapCommand.Run(settings, force ? new[] { "--force" } : Array.Empty<string>()) != 0) return 1;
        }

        // 2. the API documents, per supported Factorio version ---------------------------
        var targets = args.Contains("--all") ? settings.Targets() : new[] { settings.DefaultTarget() }.ToList();
        var fetchArgs = new List<string>();
        if (offline) fetchArgs.Add("--offline");
        if (args.Contains("--all")) fetchArgs.Add("--all");
        if (update || force) fetchArgs.Add("--update");

        var missingDocuments = targets.Where(t => !HasDocuments(t)).ToList();
        if (missingDocuments.Count == 0 && !update && !force)
        {
            Console.WriteLine("api        already present (" +
                              string.Join(", ", targets.Select(t => t.DocsVersion)) + ")");
        }
        else if (FetchApiCommand.Run(settings, fetchArgs.ToArray()) != 0)
        {
            return 1;
        }

        // 3. the bindings ---------------------------------------------------------------
        foreach (var target in targets)
        {
            var runtime = Path.Combine(target.GeneratedDirectory, "Runtime");
            if (Directory.Exists(runtime) && !force && !args.Contains("--regenerate"))
            {
                Console.WriteLine($"bindings   already generated for {target.DocsVersion} (" +
                                  Shorten(settings.Root, target.GeneratedDirectory) + ")");
                continue;
            }

            var generateArgs = new List<string> { "--factorio", target.ModVersion };
            if (GenerateCommand.Run(settings, generateArgs.ToArray()) != 0) return 1;
        }

        if (!args.Contains("--all"))
            Console.WriteLine("note       the other supported versions are one command away: `factorio-mod setup --all`");

        Console.WriteLine();
        Console.WriteLine("ready. next:");
        Console.WriteLine("  factorio-mod new MyMod        scaffold and build a mod");
        Console.WriteLine("  factorio-mod build mods/GasExtractor");
        return 0;
    }

    /// <summary>Whether a version's two API documents are on disk.</summary>
    private static bool HasDocuments(ApiTarget target) =>
        new[] { "runtime-api.json", "prototype-api.json" }
            .All(file => File.Exists(Path.Combine(target.ApiDirectory, file)));

    /// <summary>Paths relative to the repository when inside it, absolute otherwise.</summary>
    private static string Shorten(string root, string path)
    {
        var relative = Path.GetRelativePath(root, path);
        return relative.StartsWith("..", StringComparison.Ordinal) ? path : relative;
    }
}
