using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.Json;

namespace Factorio.ModTool;

/// <summary>
/// One Factorio version the toolchain can build for: the API documents in <c>api/&lt;docsVersion&gt;</c>
/// and the bindings generated from them in <c>src/Factorio.Bindings/Generated/&lt;docsVersion&gt;</c>.
/// </summary>
/// <param name="ModVersion">What a mod writes in <c>mod.json</c>: <c>"2.0"</c> or <c>"2.1"</c>.</param>
/// <param name="DocsVersion">The exact API documentation version, e.g. <c>"2.0.72"</c>.</param>
/// <param name="SchemaVersion">The documents' <c>api_version</c>, which the generator pins.</param>
/// <param name="ApiDirectory">Where the two JSON documents live.</param>
/// <param name="GeneratedDirectory">Where the bindings generated from them live.</param>
public sealed record ApiTarget(
    string ModVersion,
    string DocsVersion,
    int SchemaVersion,
    string ApiDirectory,
    string GeneratedDirectory);

/// <summary>Everything the tool needs to find and run: the repository, dotnet, CSharp.lua, Lua.</summary>
public sealed class ToolchainSettings
{
    public string Root { get; init; } = "";
    public string DotnetPath { get; init; } = "dotnet";
    public string CompilerDll { get; init; } = "";
    public string CompilerProject { get; init; } = "";
    public string CompilerCloneDir { get; init; } = "";
    public string CompilerRepo { get; init; } = "";
    public string CompilerCommit { get; init; } = "";
    public string CompilerPatch { get; init; } = "";
    public string LuaPath { get; init; } = "lua5.2";
    /// <summary>Blender executable used by the asset pipeline when a sprite asks for it.</summary>
    public string BlenderPath { get; init; } = "blender";
    public string LuaCompilerPath { get; init; } = "luac5.2";
    /// <summary>The default API documentation version, i.e. the one a mod gets when it names none.</summary>
    public string ApiVersion { get; init; } = "2.0.72";
    public int ApiSchemaVersion { get; init; } = 6;
    public string ApiBaseUrl { get; init; } = "https://lua-api.factorio.com/{version}/";

    /// <summary>Every Factorio version that can be built for: <c>"2.0"</c> to <c>"2.0.72"</c>.</summary>
    public IReadOnlyDictionary<string, string> ModVersions { get; init; } =
        new Dictionary<string, string>(StringComparer.Ordinal) { ["2.0"] = "2.0.72" };

    /// <summary>What `new` writes into `mod.json`, and the fallback for a manifest that says nothing.</summary>
    public string DefaultModVersion { get; init; } = "2.0";

    public string ApiDirectory => Path.Combine(Root, "api", ApiVersion);
    public string BindingsDirectory => Path.Combine(Root, "src", "Factorio.Bindings");

    /// <summary>Where the bindings for the default version live: <c>Generated/&lt;docsVersion&gt;</c>.</summary>
    public string GeneratedDirectory => Path.Combine(BindingsDirectory, "Generated", ApiVersion);

    /// <summary>All supported Factorio versions, oldest first.</summary>
    /// <returns>The targets, sorted by documentation version.</returns>
    public IReadOnlyList<ApiTarget> Targets() =>
        ModVersions.OrderBy(pair => pair.Value, StringComparer.Ordinal)
            .Select(pair => new ApiTarget(
                pair.Key,
                pair.Value,
                ApiSchemaVersion,
                Path.Combine(Root, "api", pair.Value),
                Path.Combine(BindingsDirectory, "Generated", pair.Value)))
            .ToList();

    /// <summary>
    /// The API documents and bindings for one Factorio version. Accepts what a mod writes
    /// (<c>"2.0"</c>) and the exact documentation version (<c>"2.0.72"</c>).
    /// </summary>
    /// <param name="modVersion">Value of `factorioVersion` in mod.json.</param>
    /// <returns>The matching target.</returns>
    public ApiTarget Target(string? modVersion)
    {
        var wanted = string.IsNullOrWhiteSpace(modVersion) ? DefaultModVersion : modVersion.Trim();
        var targets = Targets();

        var exact = targets.FirstOrDefault(t =>
            string.Equals(t.ModVersion, wanted, StringComparison.OrdinalIgnoreCase) ||
            string.Equals(t.DocsVersion, wanted, StringComparison.OrdinalIgnoreCase));
        if (exact != null) return exact;

        throw new ToolException(
            $"Factorio {wanted} is not supported by this checkout: it can build for " +
            string.Join(", ", targets.Select(t => $"{t.ModVersion} ({t.DocsVersion})")) +
            ". Add it to `factorio.factorioVersions` in config/toolchain.json and run " +
            "`factorio-mod setup --all`.");
    }

    /// <summary>The default target, for commands that were not given a version.</summary>
    /// <returns>The target whose docs version is <see cref="ApiVersion"/>.</returns>
    public ApiTarget DefaultTarget() => Target(DefaultModVersion);
    public string ModsDirectory => Path.Combine(Root, "mods");
    /// <summary>Overrides the build directory; used by tests. <c>null</c> means <c>&lt;root&gt;/build</c>.</summary>
    public string? BuildDirectoryOverride { get; init; }

    /// <summary>
    /// Where build output goes. <c>&lt;root&gt;/build</c> in a checkout - which is what makes a build
    /// reproducible from the repository - and a per-user directory when the tool is *installed*,
    /// because Program Files is not writable by the user running it.
    /// </summary>
    public string BuildDirectory => BuildDirectoryOverride ?? DefaultBuildDirectory();

    /// <summary><c>&lt;root&gt;/build</c> if that can be written to, otherwise the user's cache directory.</summary>
    private string DefaultBuildDirectory()
    {
        var rootBuild = Path.Combine(Root, "build");
        if (IsWritablePath(rootBuild)) return rootBuild;

        var user = Path.Combine(
            Environment.GetFolderPath(OperatingSystem.IsWindows()
                ? Environment.SpecialFolder.LocalApplicationData
                : Environment.SpecialFolder.UserProfile),
            OperatingSystem.IsWindows() ? "factorio-mod" : ".cache/factorio-mod",
            "build");

        return user;
    }

    /// <summary>Whether a directory exists and can be written to, or can be created.</summary>
    /// <param name="path">The directory to test.</param>
    /// <returns>True when a file can be created in it.</returns>
    private static bool IsWritablePath(string path)
    {
        try
        {
            Directory.CreateDirectory(path);
            var probe = Path.Combine(path, ".write-test-" + Environment.ProcessId);
            File.WriteAllText(probe, "");
            File.Delete(probe);
            return true;
        }
        catch (Exception e) when (e is IOException or UnauthorizedAccessException)
        {
            return false;
        }
    }
    public string LockFile => Path.Combine(BuildDirectory, "toolchain.lock.json");

    /// <summary>
    /// Finds the repository, in this order: an explicit <c>--repo</c> directory, the
    /// <c>FACTORIO_REPO</c> environment variable, the working directory, then the directory the tool
    /// itself lives in. The last fallback is what lets a mod anywhere on disk be built with a tool
    /// that lives inside the checkout.
    /// </summary>
    /// <param name="startDirectory">Value of <c>--repo</c>, if given.</param>
    /// <returns>The resolved settings.</returns>
    public static ToolchainSettings Locate(string? startDirectory = null)
    {
        var environment = Environment.GetEnvironmentVariable("FACTORIO_REPO");
        var candidates = new List<string?>
        {
            startDirectory,
            string.IsNullOrWhiteSpace(environment) ? null : environment,
            Directory.GetCurrentDirectory(),
            AppContext.BaseDirectory,
        };

        foreach (var candidate in candidates)
        {
            if (candidate == null) continue;
            var dir = new DirectoryInfo(candidate);
            if (File.Exists(candidate)) dir = dir.Parent;
            while (dir != null)
            {
                var config = Path.Combine(dir.FullName, "config", "toolchain.json");
                if (File.Exists(config)) return Load(dir.FullName, config);
                dir = dir.Parent;
            }
        }

        throw new ToolException(
            "not inside the repository: no config/toolchain.json found above " +
            Directory.GetCurrentDirectory() + ", nor next to the tool. Pass --repo <dir> or set FACTORIO_REPO.");
    }

    private static ToolchainSettings Load(string root, string configPath)
    {
        using var doc = JsonDocument.Parse(File.ReadAllText(configPath));
        var c = doc.RootElement;
        var lua = c.GetProperty("lua");
        var factorio = c.GetProperty("factorio");
        var cs = c.GetProperty("cscharplua");
        var cloneDir = Path.Combine(root, cs.GetProperty("cloneDir").GetString()!);

        return new ToolchainSettings
        {
            Root = root,
            DotnetPath = ResolveDefaultDotnet(),
            CompilerRepo = cs.GetProperty("repository").GetString()!,
            CompilerCommit = cs.GetProperty("commit").GetString()!,
            CompilerCloneDir = cloneDir,
            CompilerProject = Path.Combine(cloneDir, cs.GetProperty("launcherProject").GetString()!),
            CompilerDll = Path.Combine(cloneDir, cs.GetProperty("launcherDll").GetString()!),
            CompilerPatch = Path.Combine(root, cs.GetProperty("patch").GetString()!),
            // The environment wins over the config: on Windows the binaries are usually lua52.exe
            // and luac52.exe, and the user knows where they are better than a pinned name does.
            LuaPath = Environment.GetEnvironmentVariable("FACTORIO_LUA")
                      ?? (lua.TryGetProperty("interpreter", out var i) ? i.GetString()! : "lua5.2"),
            LuaCompilerPath = Environment.GetEnvironmentVariable("FACTORIO_LUAC")
                              ?? (lua.TryGetProperty("compiler", out var comp) ? comp.GetString()! : "luac5.2"),
            ApiVersion = factorio.GetProperty("version").GetString()!,
            ApiSchemaVersion = factorio.GetProperty("apiVersion").GetInt32(),
            ApiBaseUrl = factorio.GetProperty("baseUrl").GetString()!,
            DefaultModVersion = factorio.TryGetProperty("defaultModVersion", out var defaultMod)
                ? defaultMod.GetString()!
                : "2.0",
            ModVersions = ReadVersions(factorio),
        };
    }

    /// <summary>
    /// Reads the supported-version map. A config written before versions existed only has
    /// `version`, and then that one version is the only target.
    /// </summary>
    private static IReadOnlyDictionary<string, string> ReadVersions(JsonElement factorio)
    {
        var versions = new Dictionary<string, string>(StringComparer.Ordinal);
        if (factorio.TryGetProperty("factorioVersions", out var map) && map.ValueKind == JsonValueKind.Object)
        {
            foreach (var entry in map.EnumerateObject())
                versions[entry.Name] = entry.Value.GetString()!;
        }

        if (versions.Count == 0)
            versions[factorio.GetProperty("version").GetString()!] = factorio.GetProperty("version").GetString()!;

        return versions;
    }

    private static string ResolveDefaultDotnet()
    {
        var env = Environment.GetEnvironmentVariable("DOTNET");
        if (!string.IsNullOrWhiteSpace(env)) return env;

        var exeName = OperatingSystem.IsWindows() ? "dotnet.exe" : "dotnet";
        try
        {
            var runtimeDir = RuntimeEnvironment.GetRuntimeDirectory();
            var hostMuxer = Path.GetFullPath(Path.Combine(runtimeDir, "..", "..", "..", exeName));
            if (File.Exists(hostMuxer)) return hostMuxer;
        }
        catch
        {
            // Fall through to DOTNET_ROOT / PATH.
        }

        var dotnetRoot = Environment.GetEnvironmentVariable("DOTNET_ROOT");
        if (!string.IsNullOrWhiteSpace(dotnetRoot))
        {
            var fromRoot = Path.Combine(dotnetRoot, exeName);
            if (File.Exists(fromRoot)) return fromRoot;
        }

        var userDotnet = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
            ".dotnet",
            exeName);
        if (Shell.Find("dotnet") == null && File.Exists(userDotnet))
            return userDotnet;

        return "dotnet";
    }
}

/// <summary>An expected, user-facing failure: printed without a stack trace, exit code 1.</summary>
public sealed class ToolException : Exception
{
    public ToolException(string message) : base(message) { }
}

/// <summary>Process helpers shared by every command.</summary>
public static class Shell
{
    public sealed record RunResult(int ExitCode, string StdOut, string StdErr);

    public static RunResult Run(string fileName, IEnumerable<string> args, string? workingDirectory = null)
    {
        var psi = new ProcessStartInfo(fileName)
        {
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            WorkingDirectory = workingDirectory ?? Directory.GetCurrentDirectory(),
        };
        if (!psi.Environment.ContainsKey("DOTNET_ROLL_FORWARD"))
            psi.Environment["DOTNET_ROLL_FORWARD"] = "LatestMajor";
        foreach (var a in args) psi.ArgumentList.Add(a);
        try
        {
            using var p = Process.Start(psi) ?? throw new ToolException("failed to start " + fileName);
            var stdout = p.StandardOutput.ReadToEnd();
            var stderr = p.StandardError.ReadToEnd();
            p.WaitForExit();
            return new RunResult(p.ExitCode, stdout, stderr);
        }
        catch (System.ComponentModel.Win32Exception e)
        {
            throw new ToolException($"cannot run '{fileName}': {e.Message}");
        }
    }

    /// <summary>The dotnet executable, or a helpful error if the SDK is missing from PATH.</summary>
    public static string Dotnet(ToolchainSettings settings)
    {
        if (Find(settings.DotnetPath) == null)
            throw new ToolException(
                $"'{settings.DotnetPath}' is not on PATH. Install the .NET 8 SDK, or set DOTNET=<path to dotnet>.");
        return settings.DotnetPath;
    }

    /// <summary>The pinned CSharp.lua launcher, or a helpful error if bootstrap has not run.</summary>
    public static string Compiler(ToolchainSettings settings)
    {
        if (!File.Exists(settings.CompilerDll))
            throw new ToolException(
                "CSharp.lua is not built yet. Run `factorio-mod bootstrap` (needs git and the .NET 8 SDK).");
        return settings.CompilerDll;
    }

    /// <summary>Resolves an executable on PATH, or null.</summary>
    public static string? Find(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) return null;
        if (Path.IsPathRooted(name)) return File.Exists(name) ? name : null;

        var which = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "where" : "which";
        return Run(which, new[] { name }).ExitCode == 0 ? name : null;
    }

    /// <summary>
    /// The Lua 5.2 interpreter: the configured name, then the names the same binary ships under -
    /// Windows distributions call it <c>lua52.exe</c>, and source builds install <c>lua5.2</c>.
    /// </summary>
    /// <param name="settings">Toolchain settings (<c>FACTORIO_LUA</c> overrides the configured name).</param>
    /// <returns>The executable to run, or null when Lua 5.2 is not installed.</returns>
    public static string? Lua(ToolchainSettings settings) =>
        FirstFound(settings.LuaPath, "lua5.2", "lua52", "lua-5.2", "lua524", "lua");

    /// <summary>The Lua 5.2 compiler (<c>luac -p</c> checks syntax).</summary>
    /// <param name="settings">Toolchain settings (<c>FACTORIO_LUAC</c> overrides the configured name).</param>
    /// <returns>The executable to run, or null.</returns>
    public static string? Luac(ToolchainSettings settings) =>
        FirstFound(settings.LuaCompilerPath, "luac5.2", "luac52", "luac-5.2", "luac524", "luac");

    /// <summary>The configured version of an interpreter, for `doctor`.</summary>
    /// <param name="executable">The resolved executable.</param>
    /// <returns>Its version banner, or null.</returns>
    public static string? Version(string executable)
    {
        var run = Run(executable, new[] { "-v" });
        var text = (run.StdOut + " " + run.StdErr).Trim();
        return text.Length == 0 ? null : text.Split('\n')[0].Trim();
    }

    private static string? FirstFound(string configured, params string[] alternatives)
    {
        var direct = Find(configured);
        if (direct != null) return direct;

        foreach (var name in alternatives)
        {
            var found = Find(name);
            if (found != null) return found;
        }

        return null;
    }
}
