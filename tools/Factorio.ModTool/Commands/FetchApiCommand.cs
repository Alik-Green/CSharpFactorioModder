using System.Security.Cryptography;
using System.Text.Json;

namespace Factorio.ModTool.Commands;

/// <summary>
/// `factorio-mod fetch-api [--factorio 2.0|2.1] [--all] [--offline]`: downloads the official API
/// documents and records their SHA-256 next to them, so a change upstream is visible in review
/// instead of silently changing the generated bindings.
///
/// One directory per documentation version (`api/2.0.72`, `api/2.1.22`), because a mod declaring
/// `"factorioVersion": "2.0"` must be generated from the 2.0 documents, not from the newest ones.
/// </summary>
public static class FetchApiCommand
{
    public static int Run(ToolchainSettings settings, string[] args)
    {
        var offline = args.Contains("--offline");

        var selected = args.Contains("--all")
            ? settings.Targets()
            : new[] { SelectedTarget(settings, args) }.ToList();

        var failures = 0;
        foreach (var target in selected)
            if (Fetch(settings, target, offline) != 0)
                failures++;

        return failures == 0 ? 0 : 1;
    }

    /// <summary>The target named by `--factorio`, or the default one.</summary>
    /// <param name="settings">Toolchain.</param>
    /// <param name="args">Command arguments.</param>
    /// <returns>The target to fetch.</returns>
    public static ApiTarget SelectedTarget(ToolchainSettings settings, string[] args)
    {
        var version = CommandLine.Value(args, "--factorio");
        return version == null ? settings.DefaultTarget() : settings.Target(version);
    }

    private static int Fetch(ToolchainSettings settings, ApiTarget target, bool offline)
    {
        var baseUrl = settings.ApiBaseUrl.Replace("{version}", target.DocsVersion);
        Directory.CreateDirectory(target.ApiDirectory);

        Console.WriteLine($"fetch-api {target.DocsVersion} (Factorio {target.ModVersion})");
        var entries = new Dictionary<string, object>();

        foreach (var file in new[] { "runtime-api.json", "prototype-api.json" })
        {
            var path = Path.Combine(target.ApiDirectory, file);
            var url = baseUrl + file;

            if (offline)
            {
                if (!File.Exists(path))
                {
                    Console.Error.WriteLine($"  offline: {path} is missing");
                    return 1;
                }

                Console.WriteLine($"  {file}  (offline, existing file)");
            }
            else
            {
                Console.WriteLine($"  GET {url}");
                byte[] bytes;
                try
                {
                    using var http = new HttpClient { Timeout = TimeSpan.FromMinutes(5) };
                    bytes = http.GetByteArrayAsync(url).GetAwaiter().GetResult();
                }
                catch (Exception e) when (e is HttpRequestException or TaskCanceledException)
                {
                    Console.Error.WriteLine(
                        $"  cannot download {url}: {e.Message}\n" +
                        $"  If this machine has no internet access, copy the two JSON files into {target.ApiDirectory}" +
                        " and re-run with --offline.");
                    return 1;
                }

                File.WriteAllBytes(path, bytes);
                Console.WriteLine($"    {bytes.Length} bytes");
            }

            var hash = Convert.ToHexString(SHA256.HashData(File.ReadAllBytes(path))).ToLowerInvariant();
            using var doc = JsonDocument.Parse(File.ReadAllBytes(path));
            var root = doc.RootElement;
            var applicationVersion = root.GetProperty("application_version").GetString();
            var apiVersion = root.GetProperty("api_version").GetInt32();
            if (applicationVersion != target.DocsVersion)
            {
                Console.Error.WriteLine(
                    $"  {file} is for Factorio {applicationVersion}, expected {target.DocsVersion}");
                return 1;
            }

            if (apiVersion != target.SchemaVersion)
            {
                Console.Error.WriteLine(
                    $"  {file} uses api_version {apiVersion}, expected {target.SchemaVersion}");
                return 1;
            }

            Console.WriteLine($"    sha256 {hash}");
            entries[file] = new { applicationVersion, apiVersion, sha256 = hash, bytes = new FileInfo(path).Length };
        }

        var lockFile = Path.Combine(settings.Root, "api", "api.lock.json");
        WriteLock(settings, lockFile, target, baseUrl, entries);
        Console.WriteLine("  wrote " + lockFile);
        return 0;
    }

    /// <summary>
    /// Records the hashes of every fetched version in one file, so `api/api.lock.json` stays a
    /// single reviewable record rather than one lock file per directory.
    /// </summary>
    private static void WriteLock(
        ToolchainSettings settings,
        string lockFile,
        ApiTarget target,
        string source,
        Dictionary<string, object> entries)
    {
        var versions = new Dictionary<string, object>(StringComparer.Ordinal);

        // Keep what is already recorded for the other versions.
        if (File.Exists(lockFile))
        {
            try
            {
                using var existing = JsonDocument.Parse(File.ReadAllText(lockFile));
                if (existing.RootElement.TryGetProperty("versions", out var map) &&
                    map.ValueKind == JsonValueKind.Object)
                {
                    foreach (var entry in map.EnumerateObject())
                        if (entry.Name != target.DocsVersion)
                            versions[entry.Name] = JsonSerializer.Deserialize<object>(entry.Value.GetRawText())!;
                }
            }
            catch (JsonException)
            {
                // A corrupt lock file is replaced, not trusted.
            }
        }

        versions[target.DocsVersion] = new
        {
            factorioVersion = target.DocsVersion,
            modVersion = target.ModVersion,
            apiVersion = target.SchemaVersion,
            source,
            files = entries,
            fetchedAtUtc = DateTime.UtcNow.ToString("o"),
        };

        var ordered = versions.OrderBy(pair => pair.Key, StringComparer.Ordinal)
            .ToDictionary(pair => pair.Key, pair => pair.Value);

        var lockData = new
        {
            note = "sha256 of every API document in api/<version>/, recorded by `factorio-mod fetch-api`.",
            versions = ordered,
        };

        Directory.CreateDirectory(Path.GetDirectoryName(lockFile)!);
        File.WriteAllText(lockFile, JsonSerializer.Serialize(lockData, new JsonSerializerOptions { WriteIndented = true }) + "\n");
        _ = settings;
    }
}
