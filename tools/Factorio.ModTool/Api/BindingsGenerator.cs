using System.Text.Json;

namespace Factorio.ModTool.Api;

/// <summary>
/// Reads the official Factorio API JSON and writes the C# bindings. This is the only place that
/// knows about the JSON schema; both the `generate` command and `build` go through it.
/// </summary>
public sealed class BindingsGenerator
{
    public const int SupportedApiVersion = 6;

    public sealed record Stats(
        int Classes, int Methods, int Attributes, int Globals, int Defines, int Events, int TableClasses,
        int Files, int Info, int Degraded, int Skipped, int Errors);

    public sealed record Result(int ExitCode, Stats Stats, IReadOnlyList<Diagnostic> Diagnostics, IReadOnlyList<string> WrittenFiles);

    public static ApiDocument Load(string path)
    {
        if (!File.Exists(path))
            throw new ToolException($"API document not found: {path} (run `factorio-mod fetch-api`?)");
        var doc = JsonSerializer.Deserialize<ApiDocument>(File.ReadAllText(path), new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = false,
            AllowTrailingCommas = true,
        }) ?? throw new ToolException("failed to parse " + path);
        if (doc.Application != "factorio")
            throw new ToolException($"{path}: unexpected application '{doc.Application}'");
        if (doc.ApiVersion != SupportedApiVersion)
            throw new ToolException(
                $"{path}: unsupported api_version {doc.ApiVersion}; this generator implements api_version " +
                $"{SupportedApiVersion} (Factorio 2.0.7 - 2.1.x). See docs/LIMITATIONS.md.");
        return doc;
    }

    /// <summary>Generates both stages from the documents in <paramref name="apiDirectory"/>.</summary>
    public static Result Generate(
        string apiDirectory,
        string outputDirectory,
        string version,
        int schemaVersion)
    {
        _ = schemaVersion; // validated against SupportedApiVersion when each document is loaded
        var runtimePath = Path.Combine(apiDirectory, "runtime-api.json");
        var prototypePath = Path.Combine(apiDirectory, "prototype-api.json");
        return Generate(runtimePath, File.Exists(prototypePath) ? prototypePath : null, outputDirectory, version);
    }

    public static Result Generate(
        string runtimePath,
        string? prototypePath,
        string outputDirectory,
        string version,
        Action<string>? log = null)
    {
        var runtime = Load(runtimePath);
        var mapper = new TypeMapper(runtime);
        var emitter = new RuntimeEmitter(runtime, mapper, version, runtimeStage: true);
        emitter.Plan();
        var runtimeResult = emitter.Emit();

        var diagnostics = new List<Diagnostic>(runtimeResult.Diagnostics);
        var files = new List<GeneratedFile>(runtimeResult.Files);

        if (prototypePath != null)
        {
            var prototype = Load(prototypePath);
            var prototypeEmitter = new PrototypeEmitter(prototype, version);
            var prototypeResult = prototypeEmitter.Emit();
            diagnostics.AddRange(prototypeResult.Diagnostics);
            files.AddRange(prototypeResult.Files);
        }

        // deterministic, collision free paths (case-insensitive file systems included)
        var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (var f in files)
        {
            if (!seen.Add(f.Path))
                throw new ToolException("two generated types map to the same file: " + f.Path);
        }

        if (Directory.Exists(outputDirectory)) Directory.Delete(outputDirectory, true);
        var written = new List<string>();
        foreach (var f in files.OrderBy(x => x.Path, StringComparer.Ordinal))
        {
            var path = Path.Combine(outputDirectory, f.Path.Replace('/', Path.DirectorySeparatorChar));
            Directory.CreateDirectory(Path.GetDirectoryName(path)!);
            File.WriteAllText(path, f.Content, new System.Text.UTF8Encoding(false));
            written.Add(f.Path);
        }

        var undocumented = DocCheck.Check(written.Select(p => Path.Combine(outputDirectory, p.Replace('/', Path.DirectorySeparatorChar))));
        if (undocumented.Count > 0)
        {
            foreach (var f in undocumented.Take(20))
                diagnostics.Add(new Diagnostic
                {
                    Stage = "runtime",
                    Severity = "error",
                    ApiMember = f.Member,
                    Message = $"public member has no documentation ({Path.GetFileName(f.File)}:{f.Line})",
                });
            throw new ToolException(
                $"{undocumented.Count} public member(s) have no XML documentation; the first is " +
                $"`{undocumented[0].Member}` in {undocumented[0].File}:{undocumented[0].Line}. " +
                "Every member must get a summary: add the text to Api/Docs.cs.");
        }

        var stats = new Stats(
            Classes: runtime.Classes.Count,
            Methods: runtime.Classes.Sum(c => c.Methods.Count) + (runtime.GlobalFunctions?.Count ?? 0),
            Attributes: runtime.Classes.Sum(c => c.Attributes.Count),
            Globals: runtime.GlobalObjects?.Count ?? 0,
            Defines: CountDefines(runtime.Defines),
            Events: runtime.Events.Count,
            TableClasses: mapper.TableClasses.Count,
            Files: written.Count,
            Info: diagnostics.Count(d => d.Severity == "info"),
            Degraded: diagnostics.Count(d => d.Severity == "degraded"),
            Skipped: diagnostics.Count(d => d.Severity == "skipped"),
            Errors: diagnostics.Count(d => d.Severity == "error"));

        WriteReports(outputDirectory, version, runtime, diagnostics, stats, mapper.Renamer.Renames);
        log?.Invoke($"generated {stats.Files} files: {stats.Classes} classes, {stats.Methods} methods, " +
                    $"{stats.Attributes} attributes, {stats.Globals} globals, {stats.Defines} defines, " +
                    $"{stats.Events} events, {stats.TableClasses} table classes");
        log?.Invoke($"diagnostics: {diagnostics.Count} ({stats.Skipped + stats.Errors} skipped/error, " +
                    $"{stats.Degraded} degraded) -> {Path.Combine(outputDirectory, "api-report.md")}");

        return new Result(stats.Errors > 0 ? 1 : stats.Skipped > 0 ? 2 : 0, stats, diagnostics, written);
    }

    private static int CountDefines(List<DefineDef> defs) =>
        defs.Sum(d => (d.Values?.Count ?? 0) + CountDefines(d.Subkeys ?? new()));

    private static void WriteReports(
        string outDir, string version, ApiDocument doc, List<Diagnostic> diags, Stats stats,
        IReadOnlyDictionary<string, string> renames)
    {
        // The C# name for every class that the generator renamed (LuaPlayer -> Player). Written
        // next to the sources so tooling and tests can map an API name to the emitted one.
        File.WriteAllText(Path.Combine(outDir, "renames.json"),
            JsonSerializer.Serialize(renames, new JsonSerializerOptions { WriteIndented = true }) + "\n");

        var json = new
        {
            factorioVersion = version,
            apiVersion = doc.ApiVersion,
            stage = doc.Stage,
            generated = new
            {
                classes = stats.Classes,
                methodsAndFunctions = stats.Methods,
                attributes = stats.Attributes,
                globals = stats.Globals,
                defines = stats.Defines,
                events = stats.Events,
                tableClasses = stats.TableClasses,
                files = stats.Files,
            },
            diagnostics = diags.Select(d => new
            {
                stage = d.Stage,
                severity = d.Severity,
                member = d.ApiMember,
                message = d.Message,
                detail = d.Detail,
            }),
        };
        File.WriteAllText(Path.Combine(outDir, "api-report.json"),
            JsonSerializer.Serialize(json, new JsonSerializerOptions { WriteIndented = true }) + "\n");

        var sb = new System.Text.StringBuilder();
        sb.AppendLine($"# Factorio {version} API binding report");
        sb.AppendLine();
        sb.AppendLine($"* api_version: {doc.ApiVersion} (stage `{doc.Stage}`)");
        sb.AppendLine($"* classes: {stats.Classes}, methods+global functions: {stats.Methods}, attributes: {stats.Attributes}, " +
                      $"globals: {stats.Globals}, define leaves: {stats.Defines}, events: {stats.Events}, " +
                      $"generated table classes: {stats.TableClasses}, files: {stats.Files}");
        sb.AppendLine();
        foreach (var group in new[] { "error", "skipped", "degraded", "info" })
        {
            var items = diags.Where(d => d.Severity == group).ToList();
            sb.AppendLine($"## {group} ({items.Count})");
            sb.AppendLine();
            if (items.Count == 0)
            {
                sb.AppendLine("_none_");
                sb.AppendLine();
                continue;
            }
            foreach (var d in items)
                sb.AppendLine($"* [{d.Stage}] `{d.ApiMember}` - {d.Message}" +
                              (string.IsNullOrEmpty(d.Detail) ? "" :
                                  $"  \n  _{d.Detail.Replace("\n", " ").Replace("\r", "")}_"));
            sb.AppendLine();
        }
        File.WriteAllText(Path.Combine(outDir, "api-report.md"), sb.ToString());
    }
}
