using System.Diagnostics;
using System.Text.Json;
using System.Text.RegularExpressions;
using Xunit;

namespace Factorio.Bindings.TranspileTests;

public sealed record TranspileResult(int ExitCode, string StdOut, string StdErr, string OutDir, IReadOnlyList<string> Files)
{
    public string Read(string file) => File.ReadAllText(Path.Combine(OutDir, file));
    public string Everything => string.Join("\n", Files.Select(Read));
}

/// <summary>
/// Runs the pinned CSharp.lua compiler with the same arguments `factorio-mod build` uses. Every test in
/// this assembly needs the bootstrap (dotnet + built compiler), so a missing toolchain is reported
/// as a hard failure instead of a silent skip.
/// </summary>
public sealed class TranspilerFixture : IDisposable
{
    public static string Root { get; } = FindRoot();
    public string WorkDir { get; }

    private static string FindRoot()
    {
        var dir = new DirectoryInfo(AppContext.BaseDirectory);
        while (dir != null && !File.Exists(Path.Combine(dir.FullName, "config", "toolchain.json")))
            dir = dir.Parent;
        if (dir == null) throw new InvalidOperationException("repository root not found");
        return dir.FullName;
    }

    public static JsonElement Toolchain =>
        JsonDocument.Parse(File.ReadAllText(Path.Combine(Root, "config", "toolchain.json"))).RootElement;

    public static string CompilerDll
    {
        get
        {
            var c = Toolchain.GetProperty("cscharplua");
            return Path.Combine(Root, c.GetProperty("cloneDir").GetString()!, c.GetProperty("launcherDll").GetString()!);
        }
    }

    /// <summary>The API version the tests transpile against: the toolchain's default.</summary>
    public static string DefaultApiVersion => Toolchain.GetProperty("factorio").GetProperty("version").GetString()!;

    private static string GeneratedRoot => Path.Combine(Root, "src/Factorio.Bindings/Generated", DefaultApiVersion);

    /// <summary>Every generated runtime binding file (one type per file).</summary>
    public IReadOnlyList<string> Bindings => Directory
        .GetFiles(Path.Combine(GeneratedRoot, "Runtime"), "*.cs", SearchOption.AllDirectories)
        .OrderBy(f => f, StringComparer.Ordinal).ToList();

    public IReadOnlyList<string> PrototypeBindings => Directory
        .GetFiles(Path.Combine(GeneratedRoot, "Prototypes"), "*.cs", SearchOption.AllDirectories)
        .OrderBy(f => f, StringComparer.Ordinal).ToList();

    public string Compat => Path.Combine(Root, "src/Factorio.Bindings/Compat.cs");

    /// <summary>
    /// The hand-written utility bindings (Storage, Lifecycle, Commands, Crafting, the value types).
    /// Compiling them here means the templates the mods rely on are checked by the real compiler,
    /// not only by `factorio-mod build`.
    /// </summary>
    public IReadOnlyList<string> Utilities => Directory
        .GetFiles(Path.Combine(Root, "src/Factorio.Bindings/Utilities"), "*.cs")
        .OrderBy(f => f, StringComparer.Ordinal).ToList();
    public string ExampleMod => Path.Combine(Root, "mods/HelloFactorio/src/HelloFactorio.cs");

    public TranspileResult Result { get; }

    public TranspilerFixture()
    {
        WorkDir = Path.Combine(Path.GetTempPath(), "factorio-transpile-" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(WorkDir);
        Result = Run(Bindings
            .Concat(Utilities)
            .Concat(new[] { Compat, Path.Combine(Root, "tests/fixtures/Constructs.cs") }));
    }

    public TranspileResult Run(IEnumerable<string> sources)
    {
        var outDir = Path.Combine(WorkDir, "out" + Guid.NewGuid().ToString("N")[..6]);
        Directory.CreateDirectory(outDir);
        foreach (var s in sources)
            Assert.True(File.Exists(s), "missing source: " + s);

        var psi = new ProcessStartInfo("dotnet")
        {
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            WorkingDirectory = WorkDir,
        };
        psi.ArgumentList.Add(CompilerDll);
        psi.ArgumentList.Add("-s");
        psi.ArgumentList.Add(string.Join(";", sources));
        psi.ArgumentList.Add("-d");
        psi.ArgumentList.Add(outDir);

        using var p = Process.Start(psi)!;
        var stdout = p.StandardOutput.ReadToEnd();
        var stderr = p.StandardError.ReadToEnd();
        p.WaitForExit();
        var files = Directory.GetFiles(outDir, "*.lua").Select(Path.GetFileName).OrderBy(x => x).ToList();
        return new TranspileResult(p.ExitCode, stdout, stderr, outDir, files!);
    }

    public void Dispose()
    {
        try { Directory.Delete(WorkDir, true); } catch { /* best effort */ }
    }
}

[CollectionDefinition("transpiler")]
public class TranspilerCollection : ICollectionFixture<TranspilerFixture> { }

public static class LuaChecks
{
    /// <summary>Parse a file with a real Lua 5.2 compiler; this is the authoritative syntax check.</summary>
    public static (bool ok, string message) ParseLua52(string path)
    {
        foreach (var exe in new[] { "luac5.2", "luac" })
        {
            var psi = new ProcessStartInfo(exe) { RedirectStandardError = true, RedirectStandardOutput = true };
            psi.ArgumentList.Add("-p");
            psi.ArgumentList.Add(path);
            try
            {
                using var p = Process.Start(psi)!;
                var err = p.StandardError.ReadToEnd();
                p.WaitForExit();
                return (p.ExitCode == 0, err.Trim());
            }
            catch (System.ComponentModel.Win32Exception)
            {
                continue;
            }
        }
        return (false, "no Lua 5.2 compiler available");
    }

    /// <summary>
    /// Every `System.*` helper the emitted Lua calls must be implemented by
    /// src/Factorio.Bindings/Runtime/FactorioCompat.lua. CSharp.lua's CoreSystem is not available
    /// inside a Factorio mod, so anything else is a build-time failure rather than a runtime crash.
    /// </summary>
    public static readonly HashSet<string> AllowedSystemMembers = new(StringComparer.Ordinal)
    {
        "namespace", "import", "init", "get", "toString",
    };

    /// <summary>Removes Lua comments and string literals so documentation text is not scanned.</summary>
    public static string StripCommentsAndStrings(string src)
    {
        var sb = new System.Text.StringBuilder(src.Length);
        int i = 0, n = src.Length;
        while (i < n)
        {
            char c = src[i];
            if (c == '-' && i + 1 < n && src[i + 1] == '-')
            {
                int j = src.IndexOf('\n', i);
                i = j < 0 ? n : j;
            }
            else if (c == '"' || c == '\'')
            {
                char q = c;
                i++;
                while (i < n)
                {
                    if (src[i] == '\\') { i += 2; continue; }
                    if (src[i] == q) { i++; break; }
                    i++;
                }
            }
            else
            {
                sb.Append(c);
                i++;
            }
        }
        return sb.ToString();
    }

    public static IReadOnlyList<string> UnsupportedSystemCalls(string lua)
    {
        var bad = new List<string>();
        foreach (Match m in Regex.Matches(StripCommentsAndStrings(lua), @"\bSystem\.([A-Za-z_][A-Za-z0-9_]*)"))
            if (!AllowedSystemMembers.Contains(m.Groups[1].Value)) bad.Add(m.Value);
        return bad.Distinct().ToList();
    }
}
