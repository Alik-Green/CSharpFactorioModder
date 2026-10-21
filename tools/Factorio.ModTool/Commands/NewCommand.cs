using System.Security.Cryptography;
using Factorio.ModTool.Build;

namespace Factorio.ModTool.Commands;

/// <summary>
/// `factorio-mod new [name] [--dir DIR] [--author NAME] [--no-build]`: scaffold a mod that builds,
/// and build it once so the first thing the user sees is a packaged `.zip`.
///
/// The name can also come from <c>FACTORIO_MOD_NAME</c> and the destination from
/// <c>FACTORIO_MOD_DIR</c>, so a shell can do the common case with no arguments at all - the mod is
/// created in the working directory.
///
/// `--bindings` copies the generated bindings into the mod as well. That is only for a mod that is
/// meant to be handed to someone without this repository; otherwise the build reads them from the
/// repository, which is one less copy to keep in sync.
/// </summary>
public static class NewCommand
{
    public static int Run(ToolchainSettings settings, string[] args)
    {
        var request = Parse(args);

        // MOD and MOD_NAME are the names people already use on the make command line.
        var name = request.Name
                   ?? FirstSet("FACTORIO_MOD_NAME", "MOD_NAME", "MOD");

        var destination = request.Directory ?? FirstSet("FACTORIO_MOD_DIR");

        // `new C:\mods\AirExtractor` or `new --dir C:\mods\AirExtractor`: the last path segment
        // names the mod, which is the only thing that can be meant when no name was given.
        if (string.IsNullOrWhiteSpace(name) && destination != null)
            name = Path.GetFileName(Path.TrimEndingDirectorySeparator(destination));

        if (string.IsNullOrWhiteSpace(name))
            throw new ToolException(
                "no mod name: `factorio-mod new <name>`, `factorio-mod new <path>`, or set " +
                "FACTORIO_MOD_NAME (MOD_NAME and MOD also work).");

        name = name.Trim();
        var problem = ModTemplate.ValidateName(name);
        if (problem != null)
            throw new ToolException($"'{name}' is not a valid mod name: {problem}");

        var author = request.Author
                     ?? FirstSet("FACTORIO_MOD_AUTHOR", "GIT_AUTHOR_NAME", "USER", "USERNAME")
                     ?? "unknown";

        var directory = Path.GetFullPath(destination ?? Path.Combine(Directory.GetCurrentDirectory(), name));

        // The Factorio version the mod is for: what its mod.json declares, which bindings its build
        // compiles against, and the version a self-contained copy of the bindings is taken from.
        var factorio = string.IsNullOrWhiteSpace(request.FactorioVersion)
            ? settings.DefaultModVersion
            : request.FactorioVersion!.Trim();
        if (!System.Text.RegularExpressions.Regex.IsMatch(factorio, @"^\d+\.\d+$"))
            throw new ToolException($"'{factorio}' is not a Factorio version: write it as 2.0 or 2.1");

        var title = string.IsNullOrWhiteSpace(request.Title) ? null : request.Title!.Trim();
        var version = string.IsNullOrWhiteSpace(request.Version) ? "0.1.0" : request.Version!.Trim();
        if (!System.Text.RegularExpressions.Regex.IsMatch(version, @"^\d+\.\d+(\.\d+)?$"))
            throw new ToolException($"'{version}' is not a version: write it as 0.1.0");

        Console.WriteLine($"new {name}");
        var written = ModTemplate.Create(
            settings.Root, directory, name, author, request.Force, factorio, title, version);
        Console.WriteLine($"  folder     {directory}");
        foreach (var group in written.GroupBy(Describe))
            Console.WriteLine($"  {group.Key,-14}{string.Join(", ", group.Select(Path.GetFileName))}");

        if (request.CopyBindings)
        {
            var copied = CopyBindings(settings, directory, factorio);
            Console.WriteLine($"  bindings   {copied} file(s) copied into {BindingsFolder}/ (the build prefers them)");
        }
        else
        {
            Console.WriteLine($"  bindings   read from {settings.BindingsDirectory}");
        }

        Console.WriteLine($"  factorio   {factorio} (loads in {factorio} and newer);");
        Console.WriteLine("             the bindings are generated once per version, in the repository");

        if (request.NoBuild) return 0;

        // Build it once: a scaffold that does not build is a trap, and this is the shortest possible
        // path from `new` to a zip you can drop into the game.
        var project = ModProject.Load(directory);
        Console.WriteLine($"  building  {name}");
        var result = ModBuilder.Build(settings, project, new ModBuilder.Options(), line => Console.WriteLine("    " + line));
        Console.WriteLine($"  mod        {result.ModDirectory}");
        if (result.ZipPath != null) Console.WriteLine($"  zip        {result.ZipPath}");
        Console.WriteLine(result.Ok ? "  done" : "  checks reported problems (see above)");
        return result.Ok ? 0 : 1;
    }

    /// <summary>What `new` was asked for, after reading every accepted spelling.</summary>
    /// <param name="Name">The mod name, if one was given.</param>
    /// <param name="Directory">The destination, if one was given.</param>
    /// <param name="Author">The author, if one was given.</param>
    /// <param name="CopyBindings">Whether `--bindings` was passed.</param>
    /// <param name="NoBuild">Whether `--no-build` was passed.</param>
    /// <param name="Force">Whether `--force` was passed.</param>
    /// <param name="Title">The title shown in the mod list, or null for one made from the name.</param>
    /// <param name="Version">The starting version, or null for the toolchain's default.</param>
    public sealed record Request(
        string? Name,
        string? Directory,
        string? Author,
        bool CopyBindings,
        bool NoBuild,
        bool Force,
        string? Title = null,
        string? Version = null,
        string? FactorioVersion = null);

    /// <summary>
    /// Reads the arguments. `--dir PATH`, `--dir=PATH` and `-d PATH` are all accepted (cmd and
    /// PowerShell users write the `=` form), and a bare argument that contains a path separator is
    /// taken as the destination rather than as the name - `new MyMod C:\mods\MyMod`.
    /// </summary>
    /// <param name="args">The command's arguments, without the command name.</param>
    /// <returns>What was asked for.</returns>
    public static Request Parse(string[] args)
    {
        string? name = null, directory = null, author = null, title = null, version = null, factorio = null;
        var copyBindings = false;
        var noBuild = false;
        var force = false;

        for (var i = 0; i < args.Length; i++)
        {
            var arg = args[i];
            var (option, inlineValue) = SplitOption(arg);

            switch (option)
            {
                case "--dir":
                case "-d":
                case "--output":
                case "--out":
                    directory = inlineValue ?? (i + 1 < args.Length ? args[++i] : null);
                    break;
                case "--author":
                case "-a":
                    author = inlineValue ?? (i + 1 < args.Length ? args[++i] : null);
                    break;
                case "--name":
                case "-n":
                    name = inlineValue ?? (i + 1 < args.Length ? args[++i] : null);
                    break;
                case "--title":
                case "-t":
                    title = inlineValue ?? (i + 1 < args.Length ? args[++i] : null);
                    break;
                case "--version":
                case "-v":
                    version = inlineValue ?? (i + 1 < args.Length ? args[++i] : null);
                    break;
                case "--factorio":
                case "--factorio-version":
                    factorio = inlineValue ?? (i + 1 < args.Length ? args[++i] : null);
                    break;
                case "--bindings":
                    copyBindings = true;
                    break;
                case "--no-build":
                    noBuild = true;
                    break;
                case "--force":
                    force = true;
                    break;
                default:
                    if (arg.StartsWith("-", StringComparison.Ordinal)) break; // unknown switch: ignore
                    if (LooksLikePath(arg)) directory ??= arg;
                    else name ??= arg;
                    break;
            }
        }

        return new Request(name, directory, author, copyBindings, noBuild, force, title, version, factorio);
    }

    /// <summary>`--dir=x` becomes (`--dir`, `x`); anything else keeps its value null.</summary>
    private static (string Option, string? Value) SplitOption(string arg)
    {
        if (!arg.StartsWith("--", StringComparison.Ordinal) && !arg.StartsWith("-", StringComparison.Ordinal))
            return (arg, null);

        var equals = arg.IndexOf('=');
        return equals > 0 ? (arg[..equals], arg[(equals + 1)..]) : (arg, null);
    }

    /// <summary>Whether an argument is a filesystem path rather than a plain mod name.</summary>
    private static bool LooksLikePath(string arg) =>
        arg.Contains('/') || arg.Contains('\\') || arg.StartsWith(".", StringComparison.Ordinal) || Path.IsPathRooted(arg);

    /// <summary>`src/` for `src/Main.cs`, `.` for a file in the mod root.</summary>
    private static string Describe(string relative)
    {
        var directory = Path.GetDirectoryName(relative)?.Replace('\\', '/');
        return string.IsNullOrEmpty(directory) ? "." : directory + "/";
    }

    /// <summary>The first environment variable that is set and non-empty.</summary>
    private static string? FirstSet(params string[] names)
    {
        foreach (var name in names)
        {
            var value = Environment.GetEnvironmentVariable(name);
            if (!string.IsNullOrWhiteSpace(value)) return value;
        }

        return null;
    }

    private static string? Value(string[] args, string name) => CommandLine.Value(args, name);

    /// <summary>Folder inside a self-contained mod that holds the copied bindings.</summary>
    public const string BindingsFolder = "bindings";

    /// <summary>
    /// Copies `src/Factorio.Bindings` (Compat.cs, Utilities and the generated bindings for one
    /// Factorio version) into the mod, skipping anything identical. Returns how many files were copied.
    /// </summary>
    /// <param name="settings">Toolchain.</param>
    /// <param name="directory">The mod being created.</param>
    /// <param name="modVersion">Which Factorio version's bindings to copy.</param>
    /// <returns>The number of files copied.</returns>
    public static int CopyBindings(ToolchainSettings settings, string directory, string modVersion)
    {
        var target = Path.Combine(directory, BindingsFolder);
        var bindings = settings.Target(modVersion);
        var copied = 0;

        foreach (var source in Directory.EnumerateFiles(settings.BindingsDirectory, "*", SearchOption.AllDirectories))
        {
            if (source.Contains($"{Path.DirectorySeparatorChar}obj{Path.DirectorySeparatorChar}") ||
                source.Contains($"{Path.DirectorySeparatorChar}bin{Path.DirectorySeparatorChar}"))
                continue;

            // Only the bindings for this version: the others would be a second copy of the API.
            if (source.Contains($"{Path.DirectorySeparatorChar}Generated{Path.DirectorySeparatorChar}") &&
                !source.StartsWith(bindings.GeneratedDirectory, StringComparison.Ordinal))
                continue;

            var relative = Path.GetRelativePath(settings.BindingsDirectory, source);
            var destination = Path.Combine(target, relative);
            Directory.CreateDirectory(Path.GetDirectoryName(destination)!);

            if (File.Exists(destination) && SameContents(source, destination)) continue;
            File.Copy(source, destination, overwrite: true);
            copied++;
        }

        return copied;
    }

    private static bool SameContents(string left, string right)
    {
        var a = new FileInfo(left);
        var b = new FileInfo(right);
        if (a.Length != b.Length) return false;

        using var leftStream = File.OpenRead(left);
        using var rightStream = File.OpenRead(right);
        var leftHash = SHA256.HashData(leftStream);
        var rightHash = SHA256.HashData(rightStream);
        return leftHash.AsSpan().SequenceEqual(rightHash);
    }

}
