using Factorio.ModTool.Build;

namespace Factorio.ModTool.Commands;

/// <summary>
/// `factorio-mod build [mod-dir] [--no-zip] [--regenerate] [--no-validate]`: the whole pipeline for
/// one mod, or for every `mods/*/mod.json` when no directory is given.
///
/// A flag on the command line overrides the same setting in the mod's `mod.json` (`"build": { ... }`),
/// which in turn overrides the default. That is what lets an IDE build a mod with a single
/// `factorio-mod build &lt;dir&gt;`: everything else is in the file, next to the mod it describes.
/// </summary>
public static class BuildCommand
{
    /// <summary>Options that take a value, so the value is not mistaken for a mod directory.</summary>
    private static readonly string[] ValueOptions = { "--output", "--format", "--dir", "--author" };

    /// <summary>What the command line asked for, before mod.json is consulted.</summary>
    /// <param name="Zip">`--zip`/`--no-zip`, or null when neither was passed.</param>
    /// <param name="Validate">`--validate`/`--no-validate`, or null.</param>
    /// <param name="Assets">`--assets`/`--no-assets`, or null.</param>
    /// <param name="Analyze">`--analyze`/`--no-analyze`, or null.</param>
    /// <param name="Lua51">`--lua51`, or null.</param>
    /// <param name="Regenerate">`--regenerate`, or null.</param>
    /// <param name="Output">`--output DIR`, or null.</param>
    /// <param name="Format">`--format text|msbuild`, or null.</param>
    private sealed record Overrides(
        bool? Zip = null,
        bool? Validate = null,
        bool? Assets = null,
        bool? Analyze = null,
        bool? Lua51 = null,
        bool? Regenerate = null,
        string? Output = null,
        string? Format = null);

    public static int Run(ToolchainSettings settings, string[] args)
    {
        var positional = CommandLine.Positional(args, ValueOptions);
        var overrides = new Overrides(
            Zip: Flag(args, "--zip", "--no-zip"),
            Validate: Flag(args, "--validate", "--no-validate"),
            Assets: Flag(args, "--assets", "--no-assets"),
            Analyze: Flag(args, "--analyze", "--no-analyze"),
            Lua51: args.Contains("--lua51") ? true : null,
            Regenerate: args.Contains("--regenerate") ? true : null,
            Output: Value(args, "--output"),
            Format: Value(args, "--format"));

        var directories = positional.Count > 0
            ? positional
            : Directory.Exists(settings.ModsDirectory)
                ? Directory.EnumerateDirectories(settings.ModsDirectory)
                    .Where(d => File.Exists(Path.Combine(d, "mod.json")))
                    .OrderBy(d => d, StringComparer.Ordinal)
                    .ToList()
                : new List<string>();

        if (directories.Count == 0)
            throw new ToolException("no mod to build: pass a directory containing mod.json, or add one under mods/");

        var failures = 0;
        foreach (var directory in directories)
        {
            var project = ModProject.Load(directory);
            var options = OptionsFor(project, overrides);
            var msbuild = (overrides.Format ?? project.Build.Format) == "msbuild";

            Console.WriteLine($"build {project.Name} {project.Version}");

            try
            {
                var result = ModBuilder.Build(settings, project, options, line => Console.WriteLine("  " + line));
                Console.WriteLine($"  mod        {CommandLine.Display(settings.Root, result.ModDirectory)}");
                if (result.ZipPath != null)
                    Console.WriteLine($"  zip        {CommandLine.Display(settings.Root, result.ZipPath)}");
                Console.WriteLine($"  modules    {result.Modules.Count} bundled, {result.DroppedModules.Count} namespace-only");
                Console.WriteLine($"  checks     {(result.SyntaxChecked ? "Lua 5.2 syntax ok, " : "")}" +
                                  $"{result.PrototypesChecked} prototype(s) validated, {result.Findings.Count} problem(s)");

                foreach (var warning in result.Warnings)
                    Console.WriteLine(msbuild ? MsBuildLine("warning", "data.lua", 0, warning) : "  warning    " + warning);

                foreach (var finding in result.Findings)
                {
                    var line = msbuild
                        ? MsBuildLine("error", finding.File, finding.Line, finding.Message)
                        : $"  {finding.File}:{finding.Line}: {finding.Message}";
                    Console.Error.WriteLine(line);
                }

                if (!result.Ok) failures++;
                else Console.WriteLine($"  done       {result.Duration.TotalSeconds:0.0}s");
            }
            catch (ToolException e)
            {
                Console.Error.WriteLine(msbuild
                    ? $"factorio-mod : error FACTORIO0000: {project.Name}: {e.Message}"
                    : $"  FAILED     {e.Message}");
                failures++;
            }
        }

        return failures == 0 ? 0 : 1;
    }

    /// <summary>
    /// The build options for one mod: the command line where it speaks, the mod's own mod.json
    /// where it does not, the defaults otherwise.
    /// </summary>
    /// <param name="project">The mod being built.</param>
    /// <param name="overrides">What the command line asked for.</param>
    /// <returns>Options for <see cref="ModBuilder.Build"/>.</returns>
    public static ModBuilder.Options OptionsFor(ModProject project, string[] args) =>
        OptionsFor(project, new Overrides(
            Zip: Flag(args, "--zip", "--no-zip"),
            Validate: Flag(args, "--validate", "--no-validate"),
            Assets: Flag(args, "--assets", "--no-assets"),
            Analyze: Flag(args, "--analyze", "--no-analyze"),
            Lua51: args.Contains("--lua51") ? true : null,
            Regenerate: args.Contains("--regenerate") ? true : null,
            Output: Value(args, "--output"),
            Format: Value(args, "--format")));

    /// <summary>Merges one mod's settings with the command line.</summary>
    /// <param name="project">The mod being built.</param>
    /// <param name="overrides">What the command line asked for.</param>
    /// <returns>Options for <see cref="ModBuilder.Build"/>.</returns>
    private static ModBuilder.Options OptionsFor(ModProject project, Overrides overrides)
    {
        var build = project.Build;

        // A relative path in mod.json is relative to the mod, so building `mods/X` from the
        // repository root and from `mods/` produces the same output. A path on the command line is
        // relative to where the command was run, like every other path on a command line.
        var output = overrides.Output ?? build.Output;
        if (output != null && !Path.IsPathRooted(output))
        {
            var basis = overrides.Output != null ? Directory.GetCurrentDirectory() : project.ProjectDirectory;
            output = Path.GetFullPath(Path.Combine(basis, output));
        }

        return new ModBuilder.Options(
            Zip: overrides.Zip ?? build.Zip ?? true,
            Regenerate: overrides.Regenerate ?? build.Regenerate ?? false,
            Validate: overrides.Validate ?? build.Validate ?? true,
            ClassicLua: overrides.Lua51 ?? build.Lua51 ?? false,
            Assets: overrides.Assets ?? build.Assets ?? true,
            Analyze: overrides.Analyze ?? build.Analyze ?? true,
            OutputDirectory: output);
    }

    /// <summary>
    /// One line in the format MSBuild (and Rider's build window) parses:
    /// <c>file(line,column): error FACTORIO: message</c>.
    /// </summary>
    /// <param name="kind">`error` or `warning`.</param>
    /// <param name="file">The file the problem is in.</param>
    /// <param name="line">The line, or 0 for "the whole file".</param>
    /// <param name="message">What is wrong.</param>
    /// <returns>The line.</returns>
    private static string MsBuildLine(string kind, string file, int line, string message) =>
        $"{file}({Math.Max(line, 1)},1): {kind} FACTORIO: {message}";

    /// <summary>`--zip`/`--no-zip` and friends: true for the first, false for the second, null for neither.</summary>
    /// <param name="args">The command line.</param>
    /// <param name="on">The flag that turns the setting on.</param>
    /// <param name="off">The flag that turns it off.</param>
    /// <returns>The value, or null.</returns>
    private static bool? Flag(string[] args, string on, string off) =>
        args.Contains(off) ? false : args.Contains(on) ? true : null;

    private static string? Value(string[] args, string name) => CommandLine.Value(args, name);
}
