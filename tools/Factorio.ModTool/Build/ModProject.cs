using System.Text.Json;
using System.Text.Json.Serialization;

namespace Factorio.ModTool.Build;

/// <summary>
/// A mod folder read from its <c>mod.json</c> manifest: its identity, entry points, input directories
/// and per-project build settings.
/// </summary>
public sealed class ModProject
{
    /// <summary>Per-project defaults for <c>factorio-mod build</c>, read from <c>"build"</c> in <c>mod.json</c>.</summary>
    public sealed class BuildSettings
    {
        [JsonPropertyName("output")] public string? Output { get; init; }
        [JsonPropertyName("zip")] public bool? Zip { get; init; }
        [JsonPropertyName("analyze")] public bool? Analyze { get; init; }
        [JsonPropertyName("assets")] public bool? Assets { get; init; }
        [JsonPropertyName("validate")] public bool? Validate { get; init; }
        [JsonPropertyName("lua51")] public bool? Lua51 { get; init; }
        [JsonPropertyName("regenerate")] public bool? Regenerate { get; init; }
        [JsonPropertyName("format")] public string? Format { get; init; }
    }

    private sealed class ManifestDto
    {
        [JsonPropertyName("name")] public string? Name { get; set; }
        [JsonPropertyName("title")] public string? Title { get; set; }
        [JsonPropertyName("version")] public string? Version { get; set; }
        [JsonPropertyName("author")] public string? Author { get; set; }
        [JsonPropertyName("description")] public string? Description { get; set; }
        [JsonPropertyName("factorioVersion")] public string? FactorioVersion { get; set; }
        [JsonPropertyName("dependencies")] public List<string>? Dependencies { get; set; }
        [JsonPropertyName("entry")] public string? Entry { get; set; }
        [JsonPropertyName("dataEntry")] public string? DataEntry { get; set; }
        [JsonPropertyName("bindings")] public string? Bindings { get; set; }
        [JsonPropertyName("sources")] public List<string>? Sources { get; set; }
        [JsonPropertyName("dataSources")] public List<string>? DataSources { get; set; }
        [JsonPropertyName("resources")] public List<string>? Resources { get; set; }
        [JsonPropertyName("lua")] public List<string>? Lua { get; set; }
        [JsonPropertyName("files")] public List<string>? Files { get; set; }
        [JsonPropertyName("build")] public BuildSettings? Build { get; set; }
    }

    public string ProjectDirectory { get; init; } = "";
    public string Name { get; init; } = "";
    public string Title { get; init; } = "";
    public string Version { get; init; } = "0.1.0";
    public string Author { get; init; } = "unknown";
    public string Description { get; init; } = "";
    public string FactorioVersion { get; init; } = "2.0";
    public IReadOnlyList<string> Dependencies { get; init; } = new[] { "base" };
    public string Entry { get; init; } = "";
    public string? DataEntry { get; init; }
    public string Bindings { get; init; } = "runtime";
    public IReadOnlyList<string> Sources { get; init; } = new[] { "src" };
    public IReadOnlyList<string> DataSources { get; init; } = Array.Empty<string>();
    public IReadOnlyList<string> Resources { get; init; } = Array.Empty<string>();
    public IReadOnlyList<string> Lua { get; init; } = Array.Empty<string>();
    public IReadOnlyList<string> Files { get; init; } = Array.Empty<string>();
    public BuildSettings Build { get; init; } = new();

    /// <summary>Loads and validates the <c>mod.json</c> manifest in <paramref name="directory"/>.</summary>
    public static ModProject Load(string directory)
    {
        var fullDir = Path.GetFullPath(directory);
        var manifestPath = Path.Combine(fullDir, "mod.json");
        if (!File.Exists(manifestPath))
            throw new ToolException($"no mod.json found in {fullDir}");

        ManifestDto dto;
        try
        {
            dto = JsonSerializer.Deserialize<ManifestDto>(File.ReadAllText(manifestPath), new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                ReadCommentHandling = JsonCommentHandling.Skip,
                AllowTrailingCommas = true,
            }) ?? throw new ToolException($"{manifestPath}: empty manifest");
        }
        catch (JsonException e)
        {
            throw new ToolException($"{manifestPath}: invalid JSON: {e.Message}");
        }

        if (string.IsNullOrWhiteSpace(dto.Name))
            throw new ToolException($"{manifestPath}: missing 'name'");

        var nameProblem = ModTemplate.ValidateName(dto.Name);
        if (nameProblem != null)
            throw new ToolException($"{manifestPath}: invalid mod name '{dto.Name}': {nameProblem}");

        if (string.IsNullOrWhiteSpace(dto.Version))
            throw new ToolException($"{manifestPath}: missing 'version'");

        if (string.IsNullOrWhiteSpace(dto.Entry))
            throw new ToolException($"{manifestPath}: missing 'entry'");

        var build = dto.Build ?? new BuildSettings();
        if (build.Format != null && build.Format is not ("text" or "msbuild"))
            throw new ToolException($"{manifestPath}: unknown build.format '{build.Format}' (expected 'text' or 'msbuild')");

        var sources = dto.Sources is { Count: > 0 } ? dto.Sources : new List<string> { "src" };
        var dataSources = dto.DataSources ?? (dto.DataEntry != null ? new List<string> { "data" } : new List<string>());

        // Data-stage sources execute in .NET at build time and cannot share a folder with runtime sources.
        foreach (var runtimeRel in sources)
        {
            var runtimeFull = NormalizeTrailing(Path.GetFullPath(Path.Combine(fullDir, runtimeRel)));
            foreach (var dataRel in dataSources)
            {
                var dataFull = NormalizeTrailing(Path.GetFullPath(Path.Combine(fullDir, dataRel)));
                if (dataFull.StartsWith(runtimeFull, StringComparison.OrdinalIgnoreCase) ||
                    runtimeFull.StartsWith(dataFull, StringComparison.OrdinalIgnoreCase))
                {
                    throw new ToolException(
                        $"{manifestPath}: data-stage source directory '{dataRel}' overlaps runtime source directory '{runtimeRel}'");
                }
            }
        }

        return new ModProject
        {
            ProjectDirectory = fullDir,
            Name = dto.Name.Trim(),
            Title = string.IsNullOrWhiteSpace(dto.Title) ? ModTemplate.TitleFor(dto.Name.Trim()) : dto.Title.Trim(),
            Version = dto.Version.Trim(),
            Author = string.IsNullOrWhiteSpace(dto.Author) ? "unknown" : dto.Author.Trim(),
            Description = dto.Description ?? "",
            FactorioVersion = string.IsNullOrWhiteSpace(dto.FactorioVersion) ? "2.0" : dto.FactorioVersion.Trim(),
            Dependencies = dto.Dependencies is { Count: > 0 } ? dto.Dependencies : new[] { "base" },
            Entry = dto.Entry.Trim(),
            DataEntry = string.IsNullOrWhiteSpace(dto.DataEntry) ? null : dto.DataEntry.Trim(),
            Bindings = string.IsNullOrWhiteSpace(dto.Bindings) ? "runtime" : dto.Bindings.Trim(),
            Sources = sources,
            DataSources = dataSources,
            Resources = dto.Resources ?? (IReadOnlyList<string>)Array.Empty<string>(),
            Lua = dto.Lua ?? (IReadOnlyList<string>)Array.Empty<string>(),
            Files = dto.Files ?? (IReadOnlyList<string>)Array.Empty<string>(),
            Build = build,
        };
    }

    /// <summary>All runtime C# source files belonging to this mod, in deterministic order.</summary>
    public IReadOnlyList<string> SourceFiles() => CollectCsFiles(Sources, skipBindingsCopy: true);

    /// <summary>All build-time data-stage C# source files belonging to this mod, in deterministic order.</summary>
    public IReadOnlyList<string> DataSourceFiles() => CollectCsFiles(DataSources, skipBindingsCopy: false);

    private IReadOnlyList<string> CollectCsFiles(IReadOnlyList<string> folders, bool skipBindingsCopy)
    {
        var bindingsRoot = NormalizeTrailing(Path.Combine(ProjectDirectory, "bindings"));
        var files = new List<string>();
        foreach (var relative in folders)
        {
            var folder = Path.Combine(ProjectDirectory, relative);
            if (!Directory.Exists(folder)) continue;

            foreach (var file in Directory.EnumerateFiles(folder, "*.cs", SearchOption.AllDirectories))
            {
                var full = Path.GetFullPath(file);
                if (skipBindingsCopy && full.StartsWith(bindingsRoot, StringComparison.OrdinalIgnoreCase))
                    continue;
                if (IsBuildArtifact(full)) continue;
                files.Add(full);
            }
        }

        return files.Distinct(StringComparer.Ordinal).OrderBy(f => f, StringComparer.Ordinal).ToList();
    }

    private static bool IsBuildArtifact(string fullPath)
    {
        var sep = Path.DirectorySeparatorChar;
        return fullPath.Contains($"{sep}bin{sep}", StringComparison.OrdinalIgnoreCase) ||
               fullPath.Contains($"{sep}obj{sep}", StringComparison.OrdinalIgnoreCase);
    }

    private static string NormalizeTrailing(string path) =>
        Path.TrimEndingDirectorySeparator(path) + Path.DirectorySeparatorChar;
}
