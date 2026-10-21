using System.Diagnostics;

namespace Factorio.ModTool.Commands;

/// <summary>
/// `factorio-mod template`: installs the mod template into the .NET SDK's template engine.
///
/// That is what puts "Factorio mod (C#)" into Rider's **New Solution / New Project** dialog - the
/// dialog is driven by <c>dotnet new</c>, and any template the SDK knows is listed there, under
/// *Other* (or *Custom templates*). The same template then works from the command line and in Visual
/// Studio, with no plugin installed.
///
/// The template itself is <c>templates/dotnet/factorio-mod/</c> in this repository, so there is
/// nothing to package or download: the SDK installs a folder as happily as a .nupkg.
/// </summary>
public static class TemplateCommand
{
    /// <summary>The template folder, relative to the repository root.</summary>
    public const string TemplateDirectory = "templates/dotnet/factorio-mod";

    public static int Run(ToolchainSettings settings, string[] args)
    {
        var source = Path.Combine(settings.Root, TemplateDirectory);
        if (!Directory.Exists(source))
            throw new ToolException($"no template folder at {source}");

        var uninstall = args.Contains("--uninstall") || args.Contains("uninstall");
        var list = args.Contains("--list") || args.Contains("list");

        if (list) return RunDotnet(settings, "new", "list", "factorio-mod");

        if (uninstall)
        {
            Console.WriteLine($"removing {source}");
            return RunDotnet(settings, "new", "uninstall", source);
        }

        // The same command Rider's "Install Template" button runs. `--force` makes it an update
        // rather than an error when the template is already installed, which is what a rebuild after
        // editing the template needs.
        Console.WriteLine($"installing {source}");
        var exit = RunDotnet(settings, "new", "install", source, "--force");
        if (exit != 0) return exit;

        Console.WriteLine();
        Console.WriteLine("using it");
        Console.WriteLine("  Rider      File -> New Solution -> More Templates -> the template is in the list");
        Console.WriteLine("             (or 'Install Template' with this folder, if the list was already open)");
        Console.WriteLine("  command    dotnet new factorio-mod -n MyMod -o MyMod");
        Console.WriteLine("             dotnet new factorio-mod -n MyMod --title \"My Mod\" --factorio 2.1");
        Console.WriteLine();
        Console.WriteLine("then edit mod.json, write C# in src/ and data/, and press Build - the project's");
        Console.WriteLine(".csproj runs factorio-mod, which writes build/mods/MyMod_0.1.0.zip.");
        return 0;
    }

    /// <summary>Runs `dotnet` and passes its output through.</summary>
    private static int RunDotnet(ToolchainSettings settings, params string[] arguments)
    {
        var start = new ProcessStartInfo(settings.DotnetPath)
        {
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
        };
        foreach (var argument in arguments) start.ArgumentList.Add(argument);

        try
        {
            using var process = Process.Start(start)!;
            var stdout = process.StandardOutput.ReadToEnd();
            var stderr = process.StandardError.ReadToEnd();
            process.WaitForExit();

            Console.Write(stdout);
            if (!string.IsNullOrWhiteSpace(stderr)) Console.Error.Write(stderr);
            return process.ExitCode;
        }
        catch (System.ComponentModel.Win32Exception e)
        {
            throw new ToolException(
                $"cannot run '{settings.DotnetPath}': {e.Message}. The template is the .NET SDK's, so " +
                "installing it needs `dotnet` (or copy the folder into Rider by hand: New Solution -> " +
                $"More Templates -> Install Template -> {Path.Combine(settings.Root, TemplateDirectory)}).");
        }
    }
}
