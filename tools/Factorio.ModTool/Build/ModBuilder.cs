using System.Diagnostics;
using Factorio.ModTool.Api;
using Factorio.ModTool.Assets;
using Factorio.ModTool.Data;

namespace Factorio.ModTool.Build;

/// <summary>
/// Orchestrates the end-to-end build pipeline for a Factorio C# mod:
/// assets -&gt; bindings -&gt; analyzers -&gt; C# data stage -&gt; CSharp.lua compile -&gt; bundle -&gt;
/// package -&gt; Lua 5.2 &amp; prototype checks.
/// </summary>
public static class ModBuilder
{
    public sealed record Options(
        bool Zip = true,
        bool Regenerate = false,
        bool Validate = true,
        bool ClassicLua = false,
        bool Assets = true,
        bool Analyze = true,
        string? OutputDirectory = null);

    public sealed record BuildResult(
        bool Ok,
        string ModDirectory,
        string? ZipPath,
        IReadOnlyList<string> Modules,
        IReadOnlyList<string> DroppedModules,
        bool SyntaxChecked,
        int PrototypesChecked,
        IReadOnlyList<LuaCheck.Finding> Findings,
        IReadOnlyList<string> Warnings,
        TimeSpan Duration);

    public static BuildResult Build(
        ToolchainSettings settings,
        ModProject project,
        Options options,
        Action<string> log)
    {
        var stopwatch = Stopwatch.StartNew();

        // 1. Render 3D sprite assets when present
        if (options.Assets)
            AssetPipeline.Run(settings, project, force: false, log);

        // 2. Resolve target Factorio version and regenerate bindings if missing or requested
        var target = settings.Target(project.FactorioVersion);
        log($"factorio   {target.ModVersion} (api {target.DocsVersion})");

        var localBindingsRoot = Path.Combine(project.ProjectDirectory, "bindings");
        var useLocalBindings = Directory.Exists(localBindingsRoot);
        if (!useLocalBindings && (options.Regenerate || !Directory.Exists(Path.Combine(target.GeneratedDirectory, "Runtime"))))
        {
            log($"bindings   generating {target.DocsVersion}");
            BindingsGenerator.Generate(
                Path.Combine(target.ApiDirectory, "runtime-api.json"),
                Path.Combine(target.ApiDirectory, "prototype-api.json"),
                target.GeneratedDirectory,
                target.DocsVersion,
                msg => log("  " + msg));
        }

        // 3. Run C# analyzers before starting the compiler
        var warnings = new List<string>();
        if (options.Analyze)
        {
            var analyzerFindings = Analysis.Run(project);
            foreach (var w in analyzerFindings.Where(f => !f.IsError))
                warnings.Add($"{w.Id}: {w.Message} ({w.File}:{w.Line})");

            var errors = analyzerFindings.Where(f => f.IsError).ToList();
            if (errors.Count > 0)
            {
                var baseOut = options.OutputDirectory ?? Path.Combine(settings.BuildDirectory, "mods");
                var plannedModDir = Path.Combine(baseOut, $"{project.Name}_{project.Version}");
                var mapped = errors
                    .Select(e => new LuaCheck.Finding(e.File, e.Line, $"error {e.Id}: {e.Message}"))
                    .ToList();
                return new BuildResult(
                    Ok: false,
                    ModDirectory: plannedModDir,
                    ZipPath: null,
                    Modules: Array.Empty<string>(),
                    DroppedModules: Array.Empty<string>(),
                    SyntaxChecked: false,
                    PrototypesChecked: 0,
                    Findings: mapped,
                    Warnings: warnings,
                    Duration: stopwatch.Elapsed);
            }
        }

        // 4. Evaluate optional C# data stage (`dataEntry`)
        string? generatedDataLua = null;
        IReadOnlySet<string>? inheritedPrototypes = null;
        var generatedLocale = new Dictionary<string, string>(StringComparer.Ordinal);

        if (!string.IsNullOrWhiteSpace(project.DataEntry))
        {
            var dataResult = DataEntryGenerator.Generate(
                project.DataEntry,
                project.DataSourceFiles(),
                target.ModVersion,
                project.Name);
            generatedDataLua = dataResult.Lua;
            inheritedPrototypes = dataResult.Inherited;

            foreach (var (protoKey, locs) in dataResult.Names)
            {
                var slash = protoKey.IndexOf('/');
                if (slash <= 0) continue;
                var protoType = protoKey[..slash];
                var protoName = protoKey[(slash + 1)..];

                if (locs.TryGetValue("localised-name", out var locName))
                {
                    var sec = LocaleCheck.NameSection(protoType);
                    if (sec != null) generatedLocale[$"{sec}/{protoName}"] = locName;
                }
                if (locs.TryGetValue("localised-description", out var locDesc))
                {
                    var sec = LocaleCheck.DescriptionSection(protoType);
                    if (sec != null) generatedLocale[$"{sec}/{protoName}"] = locDesc;
                }
            }
        }

        // 5. Compile C# runtime sources + bindings with CSharp.lua
        var allSources = CollectCompilerInputs(settings, project, target, localBindingsRoot, useLocalBindings);
        log($"compiling  {allSources.Count} C# file(s)");
        var compiled = CompilerRunner.Compile(settings, project.Name, allSources, options.ClassicLua);

        // 6. Bundle into a single control.lua
        var bindingsRoot = useLocalBindings ? localBindingsRoot : settings.BindingsDirectory;
        var compatLua = Path.Combine(bindingsRoot, "FactorioCompat.lua");
        var utilsLua = Path.Combine(bindingsRoot, "FactorioUtils.lua");
        if (!File.Exists(compatLua)) compatLua = Path.Combine(settings.BindingsDirectory, "FactorioCompat.lua");
        if (!File.Exists(utilsLua)) utilsLua = Path.Combine(settings.BindingsDirectory, "FactorioUtils.lua");

        var bundled = LuaBundle.Bundle(compatLua, utilsLua, compiled.LuaFiles, project.Entry);

        // 7. Package folder & zip
        var packaged = Packager.Package(
            project,
            bundled.Lua,
            generatedDataLua,
            generatedLocale,
            settings,
            zip: options.Zip,
            outputDirectory: options.OutputDirectory);
        warnings.AddRange(packaged.Warnings);

        // 8. Validate Lua 5.2 syntax, CoreSystem calls, data-stage prototypes & locale names
        var findings = new List<LuaCheck.Finding>();
        var syntaxChecked = false;
        var prototypesChecked = 0;

        if (options.Validate)
        {
            findings.AddRange(LuaCheck.ScanSystemCalls("control.lua", bundled.Lua));

            var syntax = LuaCheck.CheckSyntax(settings, packaged.ModDirectory);
            syntaxChecked = syntax.Checked;
            findings.AddRange(syntax.Findings);

            var dataStage = DataStage.Run(settings, project, packaged.ModDirectory, target, inheritedPrototypes);
            prototypesChecked = dataStage.Shapes.Count;
            findings.AddRange(dataStage.Findings);
            warnings.AddRange(dataStage.Warnings);

            if (dataStage.Shapes.Count > 0)
                warnings.AddRange(LocaleCheck.Names(packaged.ModDirectory, dataStage.Shapes));
        }

        var ok = findings.Count == 0;
        if (!ok)
        {
            try
            {
                if (Directory.Exists(packaged.ModDirectory)) Directory.Delete(packaged.ModDirectory, recursive: true);
                if (packaged.ZipPath != null && File.Exists(packaged.ZipPath)) File.Delete(packaged.ZipPath);
            }
            catch (IOException)
            {
                // Best-effort cleanup of failed output.
            }
        }

        return new BuildResult(
            Ok: ok,
            ModDirectory: packaged.ModDirectory,
            ZipPath: packaged.ZipPath,
            Modules: bundled.Modules,
            DroppedModules: bundled.DroppedModules,
            SyntaxChecked: syntaxChecked,
            PrototypesChecked: prototypesChecked,
            Findings: findings,
            Warnings: warnings,
            Duration: stopwatch.Elapsed);
    }

    /// <summary>Returns the trailing <paramref name="lines"/> lines of <paramref name="text"/>.</summary>
    public static string Tail(string text, int lines)
    {
        var all = text.Replace("\r\n", "\n").Split('\n');
        return string.Join("\n", all.Skip(Math.Max(0, all.Length - lines)));
    }

    private static IReadOnlyList<string> CollectCompilerInputs(
        ToolchainSettings settings,
        ModProject project,
        ApiTarget target,
        string localBindingsRoot,
        bool useLocalBindings)
    {
        var inputs = new List<string>();
        var bindingsBase = useLocalBindings ? localBindingsRoot : settings.BindingsDirectory;
        var generatedBase = useLocalBindings
            ? Path.Combine(localBindingsRoot, "Generated", target.DocsVersion)
            : target.GeneratedDirectory;

        if (!string.Equals(project.Bindings, "none", StringComparison.OrdinalIgnoreCase))
        {
            var stageFolder = string.Equals(project.Bindings, "prototype", StringComparison.OrdinalIgnoreCase)
                ? "Prototypes"
                : "Runtime";
            var stageDir = Path.Combine(generatedBase, stageFolder);
            if (Directory.Exists(stageDir))
                inputs.AddRange(Directory.EnumerateFiles(stageDir, "*.cs", SearchOption.AllDirectories)
                    .OrderBy(p => p, StringComparer.Ordinal));

            var utilitiesDir = Path.Combine(bindingsBase, "Utilities");
            if (Directory.Exists(utilitiesDir))
                inputs.AddRange(Directory.EnumerateFiles(utilitiesDir, "*.cs", SearchOption.AllDirectories)
                    .OrderBy(p => p, StringComparer.Ordinal));

            var compatFile = Path.Combine(bindingsBase, "Compat.cs");
            if (File.Exists(compatFile))
                inputs.Add(compatFile);
        }

        inputs.AddRange(project.SourceFiles());
        return inputs;
    }
}
