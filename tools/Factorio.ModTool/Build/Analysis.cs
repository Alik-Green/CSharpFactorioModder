using Factorio.Analyzers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Factorio.ModTool.Build;

/// <summary>
/// In-process Roslyn host for <see cref="AnalyzerCatalog.All"/>.
/// Runs the syntactic C#-to-Lua rules over a mod's files before the compiler starts.
/// </summary>
public static class Analysis
{
    /// <summary>One diagnostic reported by the Factorio C# analyzers.</summary>
    public sealed record Finding(
        string Id,
        string File,
        int Line,
        int Column,
        string Message,
        bool IsError)
    {
        public override string ToString() =>
            $"{File}:{Line}: {(IsError ? "error" : "warning")} {Id}: {Message}";
    }

    /// <summary>Formats findings one per line for human output and test assertions.</summary>
    public static string Format(IEnumerable<Finding> findings) =>
        string.Join(Environment.NewLine, findings.Select(f => f.ToString()));

    /// <summary>Runs every rule over a mod's runtime C# files.</summary>
    public static IReadOnlyList<Finding> Run(ModProject project) => Run(project.SourceFiles());

    /// <summary>Runs every rule over a set of C# file paths.</summary>
    public static IReadOnlyList<Finding> Run(IEnumerable<string> files) =>
        RunSources(files.Select(path => (path, File.ReadAllText(path))));

    /// <summary>Runs every rule over named C# source texts without needing external references.</summary>
    public static IReadOnlyList<Finding> RunSources(IEnumerable<(string Path, string Text)> sources)
    {
        var parseOptions = CSharpParseOptions.Default.WithLanguageVersion(LanguageVersion.Latest);
        var trees = sources
            .Select(s => CSharpSyntaxTree.ParseText(s.Text, parseOptions, s.Path))
            .ToList();

        if (trees.Count == 0) return Array.Empty<Finding>();

        var compilation = CSharpCompilation.Create(
            "FactorioModAnalysis",
            trees,
            Array.Empty<MetadataReference>(),
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        var diagnostics = compilation
            .WithAnalyzers(AnalyzerCatalog.All)
            .GetAnalyzerDiagnosticsAsync()
            .GetAwaiter()
            .GetResult();

        return diagnostics
            .Where(d => d.Severity is DiagnosticSeverity.Error or DiagnosticSeverity.Warning)
            .Select(ToFinding)
            .OrderBy(f => f.File, StringComparer.Ordinal)
            .ThenBy(f => f.Line)
            .ThenBy(f => f.Column)
            .ThenBy(f => f.Id, StringComparer.Ordinal)
            .ToList();
    }

    private static Finding ToFinding(Diagnostic diagnostic)
    {
        var span = diagnostic.Location.GetLineSpan();
        return new Finding(
            diagnostic.Id,
            span.Path ?? "",
            span.StartLinePosition.Line + 1,
            span.StartLinePosition.Character + 1,
            diagnostic.GetMessage(),
            diagnostic.Severity == DiagnosticSeverity.Error);
    }
}
