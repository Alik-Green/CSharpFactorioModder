using System.Diagnostics;
using System.IO.Compression;
using System.Runtime.InteropServices;

namespace Factorio.ModTool.Commands;

/// <summary>
/// `factorio-mod dist`: assembles the tree an installer ships - the tool, everything it needs to
/// build a mod without a checkout, and the templates.
///
/// The repository is a good place to develop the toolchain and a bad place for the people using it:
/// 60 MB of bindings, a vendored compiler that has to be built, and a `git` dependency should not be
/// the first thing someone sees. This command produces the directory an installer copies:
///
/// <code>
/// &lt;out&gt;/
///   bin/factorio-mod[.exe]        the tool, published for one platform
///   bin/factorio-mod.cmd          so `factorio-mod` works from cmd.exe once bin is on PATH
///   config/toolchain.json         what ToolchainSettings looks for to find its root
///   api/&lt;version&gt;/               the official API documents
///   src/Factorio.Bindings/        the generated bindings for them
///   vendor/CSharp.lua/            the pinned compiler, already built
///   templates/                    mod scaffold + the `dotnet new` template
///   tools/patches/                the compiler patch, so `bootstrap` still works
///   mods/                         the example mods, as something to build on day one
///   plugin/                       the Rider plugin, when --plugin is given
///   VERSION
/// </code>
///
/// It is the same layout the repository has, deliberately: the tool finds its root by walking up to
/// `config/toolchain.json`, so an installed tree and a checkout are the same thing to it.
/// </summary>
public static class DistCommand
{
    public static int Run(ToolchainSettings settings, string[] args)
    {
        var version = Value(args, "--version") ?? ShellVersion(settings);
        var runtime = Value(args, "--runtime") ?? DefaultRuntime();
        var output = Path.GetFullPath(Value(args, "--output") ?? Path.Combine(settings.BuildDirectory, "dist"));
        var plugin = Value(args, "--plugin");
        var publish = !args.Contains("--no-publish");

        Console.WriteLine($"dist       {version} ({runtime})");
        Console.WriteLine($"output     {output}");

        if (Directory.Exists(output))
        {
            // Only what this command writes, so a stale build/obj from a previous run cannot leak in.
            foreach (var name in new[] { "bin", "api", "config", "src", "vendor", "templates", "tools", "mods", "docs", "plugin" })
            {
                var path = Path.Combine(output, name);
                if (Directory.Exists(path)) Directory.Delete(path, true);
            }
        }

        Directory.CreateDirectory(output);

        CopyDirectory(Path.Combine(settings.Root, "config"), Path.Combine(output, "config"));
        CopyDirectory(Path.Combine(settings.Root, "api"), Path.Combine(output, "api"), skip: "temp");
        CopyDirectory(Path.Combine(settings.Root, "templates"), Path.Combine(output, "templates"));
        CopyDirectory(Path.Combine(settings.Root, "tools", "patches"), Path.Combine(output, "tools", "patches"));
        CopyDirectory(Path.Combine(settings.Root, "docs"), Path.Combine(output, "docs"));
        WriteReadme(settings, output, version);
        // No root wrapper: the repository one builds the tool from source, which is wrong in a dist.
        // bin/ has the launchers, and the installer puts bin/ on PATH.
        var staleLauncher = Path.Combine(output, "factorio-mod.cmd");
        if (File.Exists(staleLauncher)) File.Delete(staleLauncher);

        // The bindings: Compat.cs, Utilities and Generated/<version>. Never bin/obj.
        var bindings = Path.Combine(settings.Root, "src", "Factorio.Bindings");
        foreach (var file in Directory.EnumerateFiles(bindings, "*", SearchOption.AllDirectories))
        {
            var relative = Path.GetRelativePath(bindings, file);
            if (relative.Split(Path.DirectorySeparatorChar).Any(part => part is "bin" or "obj")) continue;
            CopyFile(file, Path.Combine(output, "src", "Factorio.Bindings", relative));
        }

        CopyDirectory(Path.Combine(settings.Root, "mods"), Path.Combine(output, "mods"),
            skip: "bin", alsoSkip: "obj");

        CopyCompiler(settings, output);

        if (plugin != null)
        {
            var target = Path.Combine(output, "plugin");
            Directory.CreateDirectory(target);
            foreach (var zip in Directory.EnumerateFiles(plugin, "*.zip")) CopyFile(zip, Path.Combine(target, Path.GetFileName(zip)));
        }

        if (publish) Publish(settings, output, runtime, version);
        WriteLauncher(output, runtime);
        File.WriteAllText(Path.Combine(output, "VERSION"), version + "\n");

        Report(settings, output, version, runtime);
        return 0;
    }

    /// <summary>
    /// Copies the built CSharp.lua compiler. The dist is the one place the compiler must be *built*,
    /// because a released tool cannot ask the user to build it: `factorio-mod bootstrap` is for
    /// checkouts.
    /// </summary>
    private static void CopyCompiler(ToolchainSettings settings, string output)
    {
        var launcher = Path.GetDirectoryName(settings.CompilerDll)!;
        if (!File.Exists(settings.CompilerDll))
            throw new ToolException(
                "the CSharp.lua compiler is not built: the dist would be unusable. Run " +
                "`factorio-mod bootstrap` first (the release workflow does).");

        var relative = Path.GetRelativePath(settings.Root, launcher);
        CopyDirectory(launcher, Path.Combine(output, relative), skip: "obj");
        Console.WriteLine($"compiler   {relative}");
    }

    /// <summary>
    /// Writes the README of an installed copy: a page about *this folder*, then the repository README
    /// (which is the reference for the mod format and the commands) below it. A release reader must
    /// not be told to run `make`.
    /// </summary>
    private static void WriteReadme(ToolchainSettings settings, string output, string version)
    {
        var launcher = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "factorio-mod.exe" : "factorio-mod";
        var header = $"""
            # Factorio C# Modder {version}

            This folder is an installation. The tool is `bin/{launcher}`, and the installer put `bin`
            on PATH, so the command is:

            ```
            factorio-mod doctor        # what the tool found on this machine
            factorio-mod new MyMod     # a new mod project
            factorio-mod build MyMod   # -> build/mods/MyMod_0.1.0.zip
            factorio-mod --help
            ```

            Everything under `templates/`, `api/`, `src/Factorio.Bindings/`, `vendor/` and `tools/` is
            data the tool reads - it is not a project to build. `mods/` has the example mods, and
            `docs/` has the Rider and toolchain notes.

            The rest of this file is the repository README, which describes the mod format, the C#
            dialect and every command. Where it says `make`, use `factorio-mod` instead.

            ---

            """;

        var readme = Path.Combine(settings.Root, "README.md");
        var text = header + (File.Exists(readme) ? File.ReadAllText(readme) : string.Empty);
        File.WriteAllText(Path.Combine(output, "README.md"), text);
    }

    /// <summary>Publishes the tool for one platform into <c>&lt;out&gt;/bin</c>.</summary>
    private static void Publish(ToolchainSettings settings, string output, string runtime, string version)
    {
        var project = Path.Combine(settings.Root, "tools", "Factorio.ModTool", "Factorio.ModTool.csproj");
        var bin = Path.Combine(output, "bin");

        var start = new ProcessStartInfo(settings.DotnetPath);
        foreach (var argument in new[]
                 {
                     "publish", project,
                     "-c", "Release",
                     "-r", runtime,
                     // Framework-dependent: a mod cannot be built without the .NET SDK anyway, and the
                     // installer checks for it. This keeps the download to a few MB instead of ~70.
                     "--self-contained", "false",
                     "-p:Version=" + version,
                     "-p:DebugType=none",
                     "-o", bin,
                 })
            start.ArgumentList.Add(argument);

        start.RedirectStandardOutput = true;
        start.RedirectStandardError = true;
        start.UseShellExecute = false;

        Console.WriteLine($"publish    Factorio.ModTool for {runtime}");
        using var process = Process.Start(start) ?? throw new ToolException("cannot run " + settings.DotnetPath);
        var stdout = process.StandardOutput.ReadToEnd();
        var stderr = process.StandardError.ReadToEnd();
        process.WaitForExit();

        if (process.ExitCode != 0)
            throw new ToolException("dotnet publish failed:\n" + Tail(stdout + "\n" + stderr, 25));

        // Publish output includes symbol caches the tool does not read.
        foreach (var stale in Directory.EnumerateFiles(bin, "*.pdb")) File.Delete(stale);
        Console.WriteLine($"bin        {new DirectoryInfo(bin).EnumerateFiles().Count()} file(s)");
    }

    /// <summary>
    /// Writes the launcher scripts. On Windows the apphost is enough once <c>bin</c> is on PATH, but a
    /// .cmd is what people type, and it also makes the tool easy to call from MSBuild and from the
    /// IDE plugin without knowing the exe's name.
    /// </summary>
    private static void WriteLauncher(string output, string runtime)
    {
        var bin = Path.Combine(output, "bin");
        Directory.CreateDirectory(bin);

        var windows = runtime.StartsWith("win", StringComparison.OrdinalIgnoreCase);
        var stream = new StreamWriter(Path.Combine(bin, "factorio-mod.cmd"));
        stream.NewLine = "\r\n";
        stream.WriteLine("@echo off");
        stream.WriteLine("rem The installed factorio-mod. `factorio-mod <command>` once this folder is on PATH.");
        stream.WriteLine("setlocal");
        stream.WriteLine($"\"%~dp0{(windows ? "factorio-mod.exe" : "factorio-mod")}\" %*");
        stream.WriteLine("exit /b %errorlevel%");
        stream.Dispose();

        if (!windows)
        {
            var sh = Path.Combine(bin, "factorio-mod");
            if (File.Exists(sh)) File.SetUnixFileMode(sh, UnixFileMode.UserRead | UnixFileMode.UserWrite | UnixFileMode.UserExecute |
                                                              UnixFileMode.GroupRead | UnixFileMode.GroupExecute |
                                                              UnixFileMode.OtherRead | UnixFileMode.OtherExecute);
            var wrapper = Path.Combine(bin, "factorio-mod.sh");
            File.WriteAllText(wrapper, "#!/bin/sh\n# The installed factorio-mod.\nexec \"$(dirname \"$0\")/factorio-mod\" \"$@\"\n");
            File.SetUnixFileMode(wrapper, UnixFileMode.UserRead | UnixFileMode.UserWrite | UnixFileMode.UserExecute);
        }
    }

    /// <summary>Prints what was written, and how to check it.</summary>
    private static void Report(ToolchainSettings settings, string output, string version, string runtime)
    {
        var size = new DirectoryInfo(output).EnumerateFiles("*", SearchOption.AllDirectories).Sum(f => f.Length);
        Console.WriteLine($"dist       {output} ({size / (1024 * 1024)} MB)");
        Console.WriteLine($"run        \"{Path.Combine(output, "bin", runtime.StartsWith("win") ? "factorio-mod.exe" : "factorio-mod")}\" doctor");
        Console.WriteLine("next       the installer packages this folder; see installer/ and docs/RELEASING.md");
    }

    /// <summary>The version, from the assembly, when `--version` was not given.</summary>
    private static string ShellVersion(ToolchainSettings settings)
    {
        var file = Path.Combine(settings.Root, "VERSION");
        if (File.Exists(file)) return File.ReadAllText(file).Trim();

        var assembly = typeof(DistCommand).Assembly;
        var informational = assembly.GetCustomAttributes(typeof(System.Reflection.AssemblyInformationalVersionAttribute), false)
            .OfType<System.Reflection.AssemblyInformationalVersionAttribute>()
            .FirstOrDefault()?.InformationalVersion;

        return (informational ?? assembly.GetName().Version?.ToString() ?? "0.0.0").Split('+')[0];
    }

    /// <summary>win-x64 on Windows, linux-x64 on Linux, osx-arm64/osx-x64 on macOS.</summary>
    private static string DefaultRuntime()
    {
        if (OperatingSystem.IsWindows()) return "win-x64";
        if (OperatingSystem.IsMacOS())
            return RuntimeInformation.OSArchitecture == Architecture.Arm64 ? "osx-arm64" : "osx-x64";
        return RuntimeInformation.OSArchitecture == Architecture.Arm64 ? "linux-arm64" : "linux-x64";
    }

    /// <summary>Copies a directory, skipping any path segment named in <paramref name="skip"/>.</summary>
    private static void CopyDirectory(string source, string destination, string? skip = null, string? alsoSkip = null)
    {
        if (!Directory.Exists(source)) return;

        foreach (var file in Directory.EnumerateFiles(source, "*", SearchOption.AllDirectories))
        {
            var relative = Path.GetRelativePath(source, file);
            var parts = relative.Split(Path.DirectorySeparatorChar);
            if (skip != null && parts.Contains(skip)) continue;
            if (alsoSkip != null && parts.Contains(alsoSkip)) continue;
            if (file.EndsWith(".pdb", StringComparison.OrdinalIgnoreCase) && source.Contains("vendor")) continue;
            CopyFile(file, Path.Combine(destination, relative));
        }
    }

    /// <summary>Copies one file, creating its directory.</summary>
    private static void CopyFile(string source, string destination)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(destination)!);
        File.Copy(source, destination, true);
    }

    private static string Tail(string text, int lines)
    {
        var all = text.Replace("\r\n", "\n").Split('\n');
        return string.Join("\n", all.Skip(Math.Max(0, all.Length - lines)));
    }

    private static string? Value(string[] args, string name) => CommandLine.Value(args, name);
}
