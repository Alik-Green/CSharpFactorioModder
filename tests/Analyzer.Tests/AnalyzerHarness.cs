using Xunit;
using System.Reflection;
using System.Runtime.Versioning;
using Factorio.ModTool.Build;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Factorio.Analyzers.Tests;

/// <summary>
/// Runs the analyzers over source text. The rules are syntactic, so no references are needed and a
/// snippet deliberately does not have to compile - which is the point: it must be rejected for what
/// it means, not for what the C# compiler thinks of it.
/// </summary>
public static class AnalyzerHarness
{
    /// <summary>Runs every analyzer over one snippet, named <c>Test.cs</c>.</summary>
    /// <param name="source">C# source text.</param>
    /// <returns>Findings, in line order.</returns>
    public static IReadOnlyList<Analysis.Finding> Run(string source) =>
        Analysis.RunSources(new[] { ("Test.cs", source) });

    /// <summary>The distinct rule ids a snippet triggers.</summary>
    /// <param name="source">C# source text.</param>
    /// <returns>Ids, without duplicates, in order of first appearance.</returns>
    public static IReadOnlyList<string> Ids(string source) =>
        Run(source).Select(f => f.Id).Distinct().ToList();

    /// <summary>Wraps a statement in a class and a method, so a snippet can be one line.</summary>
    /// <param name="body">Statements to place inside <c>static void M()</c>.</param>
    /// <returns>A complete source file.</returns>
    public static string InMethod(string body) => $@"
namespace Mod
{{
    public static class Main
    {{
        public static void M()
        {{
{body}
        }}
    }}
}}";

    /// <summary>Path of the built analyzer assembly.</summary>
    public static string AnalyzerAssemblyPath => Path.Combine(
        RepositoryRoot,
        "tools",
        "Factorio.Analyzers",
        "bin",
        "Release",
        "netstandard2.0",
        "Factorio.Analyzers.dll");

    /// <summary>
    /// Loads the built assembly and instantiates every analyzer in it, the way a compiler host or
    /// an editor does: by reflection over the DLL, not through the project reference the tool uses.
    /// </summary>
    /// <returns>The analyzers, which must all declare support for C#.</returns>
    public static IReadOnlyList<DiagnosticAnalyzer> AnalyzersFromBuiltAssembly()
    {
        Assert.True(File.Exists(AnalyzerAssemblyPath), $"analyzer assembly not built: {AnalyzerAssemblyPath}");

        var assembly = Assembly.LoadFrom(AnalyzerAssemblyPath);
        return assembly.GetTypes()
            .Where(type => !type.IsAbstract && typeof(DiagnosticAnalyzer).IsAssignableFrom(type))
            .Select(type => (DiagnosticAnalyzer)Activator.CreateInstance(type)!)
            .ToList();
    }

    /// <summary>Rule ids the built analyzer assembly publishes for C#.</summary>
    /// <returns>Ids, sorted.</returns>
    public static IReadOnlyList<string> IdsFromBuiltAssembly() =>
        AnalyzersFromBuiltAssembly()
            .SelectMany(analyzer => analyzer.SupportedDiagnostics)
            .Select(descriptor => descriptor.Id)
            .OrderBy(id => id, StringComparer.Ordinal)
            .ToList();

    /// <summary>The repository root, found by walking up to <c>config/toolchain.json</c>.</summary>
    public static string RepositoryRoot { get; } = FindRepositoryRoot();

    private static string FindRepositoryRoot()
    {
        var directory = new DirectoryInfo(AppContext.BaseDirectory);
        while (directory != null && !File.Exists(Path.Combine(directory.FullName, "config", "toolchain.json")))
            directory = directory.Parent;
        if (directory == null) throw new InvalidOperationException("repository root not found");
        return directory.FullName;
    }

}
