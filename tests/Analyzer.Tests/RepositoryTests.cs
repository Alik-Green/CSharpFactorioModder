using Xunit;
using System.Reflection;
using Factorio.ModTool;
using Factorio.ModTool.Build;
using Factorio.ModTool.Commands;
using Microsoft.CodeAnalysis;

namespace Factorio.Analyzers.Tests;

/// <summary>
/// The analyzers against real code: the shipped mods and the whole bindings tree must stay silent,
/// and the build must refuse a mod that trips an error rule.
/// </summary>
public class RepositoryTests
{
    /// <summary>
    /// The assembly loads the way a compiler loads it, and publishes all three rule classes.
    /// Without this, the "analyzers" would only be a library that the build tool happens to call.
    /// </summary>
    [Fact]
    public void BuiltAssemblyIsALoadableRoslynAnalyzer()
    {
        var ids = AnalyzerHarness.IdsFromBuiltAssembly();

        Assert.Equal(
            AnalyzerCatalog.Descriptors.Select(d => d.Id).OrderBy(id => id, StringComparer.Ordinal),
            ids);
    }

    /// <summary>The shipped mods are clean: they are the reference for what correct code looks like.</summary>
    [Theory]
    [InlineData("GasExtractor")]
    [InlineData("HelloFactorio")]
    public void ShippedModIsClean(string name)
    {
        var directory = Path.Combine(AnalyzerHarness.RepositoryRoot, "mods", name);
        var files = Directory.EnumerateFiles(directory, "*.cs", SearchOption.AllDirectories)
            .Where(f => !f.Split(Path.DirectorySeparatorChar).Any(p => p is "bin" or "obj"))
            .ToList();

        var findings = Analysis.Run(files);

        Assert.True(findings.Count == 0,
            $"{name} has analyzer findings:" + Environment.NewLine + Analysis.Format(findings));
    }

    /// <summary>
    /// The bindings - hand-written helpers and 1,500 generated files - are clean too. The generated
    /// files use `@CSharpLua.Ignore` on every type and templated bodies that throw, both of which
    /// must stay exempt or the rules would fire thousands of times on code nobody wrote by hand.
    /// </summary>
    [Fact]
    public void BindingsAreClean()
    {
        var directory = Path.Combine(AnalyzerHarness.RepositoryRoot, "src", "Factorio.Bindings");

        // Data/ is the build-time data-stage model. It is not a mod's runtime code: the tool
        // compiles it into `factorio-mod` and runs it during the build, and only the Lua it writes
        // ships. So the rules that keep runtime code inside what CSharp.lua and Factorio can do -
        // no arrays, no CoreSystem, no .NET - do not apply to it.
        var files = Directory.EnumerateFiles(directory, "*.cs", SearchOption.AllDirectories)
            .Where(f => !f.Contains(Path.Combine("Factorio.Bindings", "Data") + Path.DirectorySeparatorChar, StringComparison.Ordinal))
            .ToList();
        Assert.True(files.Count > 1000, $"expected the generated bindings, found {files.Count} files");

        var findings = Analysis.Run(files);

        Assert.True(findings.Count == 0,
            $"{findings.Count} finding(s):" + Environment.NewLine +
            Analysis.Format(findings.Take(20)));
    }

    /// <summary>
    /// `analyze` reports the errors and exits non-zero, so a CI job can use it without building.
    /// </summary>
    [Fact]
    public void AnalyzeCommandFailsOnABadMod()
    {
        using var fixture = new TemporaryMod("""
            namespace Bad
            {
                public static class Main
                {
                    public static void Init()
                    {
                        int count = 0;
                    }
                }
            }
            """);

        var exitCode = 0;
        var text = Capture(() => exitCode = AnalyzeCommand.Run(fixture.Settings, new[] { fixture.Directory }));

        Assert.Equal(1, exitCode);
        Assert.Contains("FX0006", text);
        Assert.Contains("error", text);
    }

    /// <summary>
    /// `build` stops before the compiler runs: the message names the rule, the file and the line,
    /// and `--no-analyze` is the documented escape hatch.
    /// </summary>
    [Fact]
    public void BuildStopsOnAnalyzerErrors()
    {
        using var fixture = new TemporaryMod("""
            namespace Bad
            {
                public static class Main
                {
                    public static void Init()
                    {
                        var values = new double[4];
                    }
                }
            }
            """);

        var exitCode = 0;
        var text = Capture(() => exitCode = BuildCommand.Run(fixture.Settings, new[] { fixture.Directory }));

        Assert.Equal(1, exitCode);
        Assert.Contains("FX0002", text);
        Assert.Contains("error", text);
        Assert.DoesNotContain("compiling", text);
    }

    /// <summary>Runs something with stdout and stderr captured into one string.</summary>
    private static string Capture(Action action)
    {
        var writer = new StringWriter();
        var originalOut = Console.Out;
        var originalError = Console.Error;
        Console.SetOut(writer);
        Console.SetError(writer);
        try
        {
            action();
        }
        finally
        {
            Console.SetOut(originalOut);
            Console.SetError(originalError);
        }

        return writer.ToString();
    }

    /// <summary>
    /// A mod directory with a manifest and one source file, plus settings that point the build's
    /// output at a scratch directory so the test cannot disturb <c>build/</c>.
    /// </summary>
    private sealed class TemporaryMod : IDisposable
    {
        public TemporaryMod(string source)
        {
            Directory = Path.Combine(Path.GetTempPath(), "factorio-analyzer-test-" + Guid.NewGuid().ToString("N"));
            System.IO.Directory.CreateDirectory(Path.Combine(Directory, "src"));
            File.WriteAllText(Path.Combine(Directory, "mod.json"), """
                {
                  "name": "AnalyzerFixture",
                  "title": "Analyzer fixture",
                  "version": "0.1.0",
                  "author": "tests",
                  "entry": "Bad.Main.Init"
                }
                """);
            File.WriteAllText(Path.Combine(Directory, "src", "Main.cs"), source);

            var located = ToolchainSettings.Locate(AnalyzerHarness.RepositoryRoot);
            Settings = new ToolchainSettings
            {
                Root = located.Root,
                DotnetPath = located.DotnetPath,
                CompilerDll = located.CompilerDll,
                CompilerProject = located.CompilerProject,
                CompilerCloneDir = located.CompilerCloneDir,
                CompilerRepo = located.CompilerRepo,
                CompilerCommit = located.CompilerCommit,
                CompilerPatch = located.CompilerPatch,
                LuaPath = located.LuaPath,
                LuaCompilerPath = located.LuaCompilerPath,
                ApiVersion = located.ApiVersion,
                ApiSchemaVersion = located.ApiSchemaVersion,
                ApiBaseUrl = located.ApiBaseUrl,
                BuildDirectoryOverride = Path.Combine(Directory, "build"),
            };
        }

        public string Directory { get; }

        public ToolchainSettings Settings { get; }

        public void Dispose()
        {
            try
            {
                if (System.IO.Directory.Exists(Directory)) System.IO.Directory.Delete(Directory, recursive: true);
            }
            catch (IOException)
            {
                // A leftover temp directory is not worth failing a test over.
            }
        }
    }
}
