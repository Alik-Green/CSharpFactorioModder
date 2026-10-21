using System.Text.Json;
using Factorio.ModTool;
using Factorio.ModTool.Api;

namespace Factorio.Bindings.Tests;

/// <summary>Locates the repository and exposes the official API JSON used by the tests.</summary>
public static class TestEnvironment
{
    public static string RepoRoot { get; } = FindRepoRoot();

    private static string FindRepoRoot()
    {
        var dir = new DirectoryInfo(AppContext.BaseDirectory);
        while (dir != null && !File.Exists(Path.Combine(dir.FullName, "config", "toolchain.json")))
            dir = dir.Parent;
        if (dir == null) throw new InvalidOperationException("repository root not found (config/toolchain.json)");
        return dir.FullName;
    }

    public static string Version
    {
        get
        {
            using var doc = JsonDocument.Parse(File.ReadAllText(Path.Combine(RepoRoot, "config", "toolchain.json")));
            return doc.RootElement.GetProperty("factorio").GetProperty("version").GetString()!;
        }
    }

    public static string RuntimeJson => Path.Combine(RepoRoot, "api", Version, "runtime-api.json");
    public static string PrototypeJson => Path.Combine(RepoRoot, "api", Version, "prototype-api.json");

    /// <summary>
    /// Runs the generator in-process against the real API JSON and keeps every emitted file, so a
    /// test can assert on one type's file instead of grepping one enormous string.
    /// </summary>
    public static Generated RunGenerator(string? runtimeJson = null, string? prototypeJson = null)
    {
        var outDir = Path.Combine(Path.GetTempPath(), "factorio-bindings-test-" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outDir);
        try
        {
            int code = 0;
            string stderr = "";
            try
            {
                var result = BindingsGenerator.Generate(
                    runtimeJson ?? RuntimeJson,
                    prototypeJson ?? (runtimeJson == null ? PrototypeJson : null),
                    outDir,
                    Version,
                    _ => { });
                code = result.ExitCode;
            }
            catch (ToolException e)
            {
                code = 1;
                stderr = e.Message;
            }

            var files = new Dictionary<string, string>(StringComparer.Ordinal);
            foreach (var path in Directory.EnumerateFiles(outDir, "*", SearchOption.AllDirectories))
                files[Path.GetRelativePath(outDir, path).Replace('\\', '/')] = File.ReadAllText(path);

            return new Generated(code, files, stderr);
        }
        finally
        {
            try { Directory.Delete(outDir, true); } catch { /* best effort */ }
        }
    }
}

/// <summary>The emitted files of one generator run, addressed by their path inside `Generated/`.</summary>
public sealed record Generated(int ExitCode, IReadOnlyDictionary<string, string> Files, string StandardError)
{
    private static readonly Lazy<Generated> Cached = new(() => TestEnvironment.RunGenerator());
    public static Generated Default => Cached.Value;

    public string this[string path] => Files.TryGetValue(path, out var text) ? text : "";

    public string Runtime => this["Runtime/Objects/Game.cs"];
    public string Globals => this["Runtime/Globals.cs"];
    public string Defines => this["Runtime/Defines.cs"];
    public string Events => this["Runtime/Events.cs"];
    public string Prototype => Files.Where(f => f.Key.StartsWith("Prototypes/", StringComparison.Ordinal))
        .Select(f => f.Value).Aggregate("", (a, b) => a + b);
    public string AllCode => string.Join("\n", Files.Where(f => f.Key.EndsWith(".cs", StringComparison.Ordinal))
        .OrderBy(f => f.Key, StringComparer.Ordinal).Select(f => f.Value));
    public string ReportJson => this["api-report.json"];
    public string ReportMarkdownRenames => this["renames.json"];

    /// <summary>API name -> emitted C# name (`LuaPlayer` -> `Player`), from `renames.json`.</summary>
    public IReadOnlyDictionary<string, string> Renames
    {
        get
        {
            var json = this["renames.json"];
            return json.Length == 0
                ? new Dictionary<string, string>(StringComparer.Ordinal)
                : JsonSerializer.Deserialize<Dictionary<string, string>>(json)!;
        }
    }

    /// <summary>The C# name the generator emits for an API class name.</summary>
    public string CsName(string apiName) => Renames.TryGetValue(apiName, out var cs) ? cs : apiName;

    /// <summary>The API class name behind an emitted C# name.</summary>
    public string ApiName(string csName)
    {
        foreach (var pair in Renames) if (pair.Value == csName) return pair.Key;
        return csName;
    }
    public string ReportMarkdown => this["api-report.md"];

    /// <summary>The file that declares a runtime type (`Runtime/Objects/X.cs`, `Runtime/Tables/X.cs`, ...).</summary>
    public string Type(string name)
    {
        foreach (var prefix in new[] { "Runtime/Objects/", "Runtime/Tables/", "Runtime/Events/" })
        {
            var path = prefix + name + ".cs";
            if (Files.ContainsKey(path)) return Files[path];
        }
        return "";
    }
}
