using System.IO.Compression;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Factorio.ModTool.Build;

/// <summary>
/// Assembles a Factorio-loadable <c>&lt;Name&gt;_&lt;version&gt;/</c> directory and optional
/// <c>&lt;Name&gt;_&lt;version&gt;.zip</c>, merges generated locale entries, and checks packaged Lua
/// string literals for broken or unprefixed asset paths.
/// </summary>
public static class Packager
{
    public sealed record Result(
        string ModDirectory,
        string? ZipPath,
        IReadOnlyList<string> Warnings);

    private static readonly Regex QuotedResource = new(
        @"['""](?<path>(?:__[^'""/]+__/)?[A-Za-z0-9_./-]+\.(?:png|jpg|ogg|wav))['""]",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    /// <summary>Writes the packaged mod directory (and zip when <paramref name="zip"/> is true).</summary>
    public static Result Package(
        ModProject project,
        string controlLua,
        string? generatedDataLua,
        IReadOnlyDictionary<string, string> generatedLocale,
        ToolchainSettings settings,
        bool zip = true,
        string? outputDirectory = null)
    {
        var baseOut = outputDirectory ?? Path.Combine(settings.BuildDirectory, "mods");
        Directory.CreateDirectory(baseOut);

        var folderName = $"{project.Name}_{project.Version}";
        var modDirectory = Path.Combine(baseOut, folderName);
        if (Directory.Exists(modDirectory)) Directory.Delete(modDirectory, recursive: true);
        Directory.CreateDirectory(modDirectory);

        var utf8NoBom = new UTF8Encoding(false);

        // 1. info.json
        var info = new
        {
            name = project.Name,
            version = project.Version,
            title = project.Title,
            author = project.Author,
            description = project.Description,
            factorio_version = project.FactorioVersion,
            dependencies = project.Dependencies,
        };
        File.WriteAllText(
            Path.Combine(modDirectory, "info.json"),
            JsonSerializer.Serialize(info, new JsonSerializerOptions { WriteIndented = true }) + "\n",
            utf8NoBom);

        // 2. control.lua
        File.WriteAllText(Path.Combine(modDirectory, "control.lua"), controlLua, utf8NoBom);

        // 3. resources/** -> mod root
        foreach (var resourceDir in project.Resources)
        {
            var sourceDir = Path.Combine(project.ProjectDirectory, resourceDir);
            CopyTree(sourceDir, modDirectory);
        }

        // 4. static & generated lua/** -> mod root
        foreach (var luaDir in project.Lua)
        {
            var sourceDir = Path.Combine(project.ProjectDirectory, luaDir);
            CopyTree(sourceDir, modDirectory);
        }

        // Also include lua/generated if rendered sprites wrote tables there even when "lua" was omitted.
        var generatedSpritesDir = Path.Combine(project.ProjectDirectory, "lua", "generated");
        if (Directory.Exists(generatedSpritesDir))
            CopyTree(generatedSpritesDir, Path.Combine(modDirectory, "generated"));

        // 5. C# generated data.lua
        if (generatedDataLua != null)
            File.WriteAllText(Path.Combine(modDirectory, "data.lua"), generatedDataLua, utf8NoBom);

        // 6. Merge C# LocalisedName / LocalisedDescription into locale/en/locale.cfg
        if (generatedLocale.Count > 0)
        {
            var localePath = Path.Combine(modDirectory, "locale", "en", "locale.cfg");
            Directory.CreateDirectory(Path.GetDirectoryName(localePath)!);
            var existing = File.Exists(localePath) ? File.ReadAllText(localePath) : "";
            File.WriteAllText(localePath, LocaleWriter.Merge(existing, generatedLocale), utf8NoBom);
        }

        // 7. Root files (thumbnail.png, changelog.txt, CREDITS.md, ...)
        foreach (var relativeFile in project.Files)
        {
            var sourceFile = Path.Combine(project.ProjectDirectory, relativeFile);
            if (!File.Exists(sourceFile)) continue;
            var destination = Path.Combine(modDirectory, Path.GetFileName(relativeFile));
            File.Copy(sourceFile, destination, overwrite: true);
        }

        // 8. Scan packaged Lua files for unprefixed or missing __<Mod>__/ resource paths
        var warnings = ScanLuaResources(project.Name, modDirectory);

        // 9. Optional zip with top-level <Name>_<version>/ entries
        string? zipPath = null;
        if (zip)
        {
            zipPath = Path.Combine(baseOut, folderName + ".zip");
            if (File.Exists(zipPath)) File.Delete(zipPath);

            using var stream = File.Create(zipPath);
            using var archive = new ZipArchive(stream, ZipArchiveMode.Create);
            foreach (var file in Directory.EnumerateFiles(modDirectory, "*", SearchOption.AllDirectories)
                         .OrderBy(p => p, StringComparer.Ordinal))
            {
                var rel = Path.GetRelativePath(modDirectory, file).Replace('\\', '/');
                archive.CreateEntryFromFile(file, $"{folderName}/{rel}", CompressionLevel.Optimal);
            }
        }

        return new Result(modDirectory, zipPath, warnings);
    }

    private static IReadOnlyList<string> ScanLuaResources(string modName, string modDirectory)
    {
        var ownPrefix = $"__{modName}__/";
        var warnings = new List<string>();

        foreach (var luaFile in Directory.EnumerateFiles(modDirectory, "*.lua", SearchOption.AllDirectories)
                     .OrderBy(p => p, StringComparer.Ordinal))
        {
            var relLua = Path.GetRelativePath(modDirectory, luaFile).Replace('\\', '/');
            var text = File.ReadAllText(luaFile);

            foreach (Match match in QuotedResource.Matches(text))
            {
                var raw = match.Groups["path"].Value;
                if (!raw.StartsWith("__", StringComparison.Ordinal))
                {
                    warnings.Add($"{relLua}: '{raw}' has no source prefix (expected '__{modName}__/{raw}' or '__base__/{raw}')");
                    continue;
                }

                if (!raw.StartsWith(ownPrefix, StringComparison.Ordinal))
                    continue;

                var subPath = raw[ownPrefix.Length..].Replace('/', Path.DirectorySeparatorChar);
                if (!File.Exists(Path.Combine(modDirectory, subPath)))
                    warnings.Add($"{relLua}: packaged file missing for '{raw}'");
            }
        }

        return warnings;
    }

    private static void CopyTree(string sourceDir, string destinationDir)
    {
        if (!Directory.Exists(sourceDir)) return;

        foreach (var file in Directory.EnumerateFiles(sourceDir, "*", SearchOption.AllDirectories))
        {
            var rel = Path.GetRelativePath(sourceDir, file);
            var target = Path.Combine(destinationDir, rel);
            Directory.CreateDirectory(Path.GetDirectoryName(target)!);
            File.Copy(file, target, overwrite: true);
        }
    }
}
