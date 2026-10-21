using System.Text.Json;

namespace Factorio.ModTool.Commands;

/// <summary>
/// `factorio-mod bootstrap`: clone the pinned CSharp.lua, apply the local patch, build the launcher,
/// and record what was actually used. Deterministic: re-running with an intact checkout and a built
/// launcher only verifies and returns.
/// </summary>
public static class BootstrapCommand
{
    public static int Run(ToolchainSettings settings, string[] args)
    {
        var force = args.Contains("--force");
        Console.WriteLine("bootstrap");

        var dotnet = Shell.Dotnet(settings);
        Console.WriteLine($"  dotnet     {dotnet}");
        Console.WriteLine($"  CSharp.lua {settings.CompilerCommit} in {settings.CompilerCloneDir}");

        // Three states are possible:
        //   * no checkout at all           -> clone it (needs git and network);
        //   * a git checkout               -> verify the commit and apply the patch;
        //   * a plain copy of the sources  -> what a downloaded workspace contains: the vendored
        //                                     tree has no .git, so verify nothing and just build it.
        var hasGit = Directory.Exists(Path.Combine(settings.CompilerCloneDir, ".git"));
        if (!Directory.Exists(settings.CompilerCloneDir))
        {
            Console.WriteLine("  cloning ...");
            var clone = Shell.Run("git", new[] { "clone", "--quiet", settings.CompilerRepo, settings.CompilerCloneDir });
            if (clone.ExitCode != 0)
                throw new ToolException(
                    "git clone failed:\n" + clone.StdErr +
                    "\nClone it yourself into " + settings.CompilerCloneDir + ", or copy the sources there.");
            hasGit = true;
        }
        else if (!hasGit)
        {
            if (force)
                throw new ToolException(
                    settings.CompilerCloneDir + " is a plain copy of the sources, not a git checkout, so it cannot " +
                    "be re-cloned in place. Delete the directory and run this again to clone, or build the copy as is.");

            Console.WriteLine("  no git metadata in " + settings.CompilerCloneDir + ": using the bundled copy");
            Console.WriteLine("             (commit check and patch are skipped; it is presumably already patched)");
        }

        // A fresh clone is at the default branch, not at the pinned commit, and unpatched - so this
        // runs for both cases, not only for an existing checkout.
        if (hasGit) CheckoutAndPatch(settings);

        if (!File.Exists(settings.CompilerProject))
            throw new ToolException(
                "the CSharp.lua sources are missing: " + settings.CompilerProject +
                "\nRun `factorio-mod bootstrap` with git installed and network access to clone them.");

        if (force || !File.Exists(settings.CompilerDll))
        {
            Console.WriteLine("  building the launcher ...");
            var build = Shell.Run(dotnet, new[] { "build", settings.CompilerProject, "-c", "Release", "-v", "quiet", "--nologo" });
            if (build.ExitCode != 0)
                throw new ToolException("building CSharp.lua failed:\n" + build.StdOut + build.StdErr);
        }

        if (!File.Exists(settings.CompilerDll))
            throw new ToolException("the launcher was built but " + settings.CompilerDll + " is missing; check the csproj target framework");

        WriteLock(settings);
        Console.WriteLine("  compiler   " + settings.CompilerDll);
        Console.WriteLine("bootstrap ok");
        return 0;
    }

    /// <summary>
    /// Pins the checkout to the commit the toolchain was tested against and applies the local patch
    /// (which is what makes the compiler target .NET 8). Both steps are idempotent: a good checkout
    /// only gets verified, an already patched tree only gets checked.
    /// </summary>
    private static void CheckoutAndPatch(ToolchainSettings settings)
    {
        // Always ask git what is checked out: a stale checkout silently changes compiler behaviour.
        var head = Shell.Run("git", new[] { "-C", settings.CompilerCloneDir, "rev-parse", "HEAD" });
        if (head.ExitCode != 0) throw new ToolException(settings.CompilerCloneDir + " is not a git checkout");
        if (head.StdOut.Trim() != settings.CompilerCommit)
        {
            Console.WriteLine($"  checking out {settings.CompilerCommit[..8]} (currently {head.StdOut.Trim()[..8]})");
            var checkout = Shell.Run("git", new[] { "-C", settings.CompilerCloneDir, "checkout", "--quiet", settings.CompilerCommit });
            if (checkout.ExitCode != 0) throw new ToolException("git checkout failed:\n" + checkout.StdErr);
        }

        if (!File.Exists(settings.CompilerPatch)) return;

        var applied = Shell.Run("git", new[] { "-C", settings.CompilerCloneDir, "apply", "--check", "--reverse", settings.CompilerPatch });
        if (applied.ExitCode != 0)
        {
            var apply = Shell.Run("git", new[] { "-C", settings.CompilerCloneDir, "apply", settings.CompilerPatch });
            if (apply.ExitCode != 0) throw new ToolException("failed to apply " + settings.CompilerPatch + ":\n" + apply.StdErr);
            Console.WriteLine("  applied " + Path.GetFileName(settings.CompilerPatch));
        }
    }

    /// <summary>Records the resolved versions so a failure can be reported against exact inputs.</summary>
    private static void WriteLock(ToolchainSettings settings)
    {
        var dotnetVersion = Shell.Run(settings.DotnetPath, new[] { "--version" });
        var pkg = Path.Combine(settings.CompilerCloneDir, "CSharp.lua", "CSharp.lua.csproj");
        var lockData = new
        {
            dotnet = new { path = settings.DotnetPath, version = dotnetVersion.StdOut.Trim() },
            cscharplua = new
            {
                repository = settings.CompilerRepo,
                commit = settings.CompilerCommit,
                launcher = settings.CompilerDll,
                patch = File.Exists(settings.CompilerPatch) ? Path.GetFileName(settings.CompilerPatch) : null,
            },
            lua = new
            {
                interpreter = Shell.Lua(settings),
                compiler = Shell.Luac(settings),
            },
            factorio = new { version = settings.ApiVersion, apiVersion = settings.ApiSchemaVersion },
            generatedAtUtc = DateTime.UtcNow.ToString("o"),
            note = "Re-generated by `factorio-mod bootstrap`; the inputs above are what the last build used.",
        };
        Directory.CreateDirectory(settings.BuildDirectory);
        File.WriteAllText(settings.LockFile, JsonSerializer.Serialize(lockData, new JsonSerializerOptions { WriteIndented = true }) + "\n");
        _ = pkg;
    }
}
