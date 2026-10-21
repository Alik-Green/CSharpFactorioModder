using System;
using System.Linq;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Factorio.Analyzers;

/// <summary>
/// The analyzer set as one value, so host code and tests run exactly the same rules.
/// </summary>
public static class AnalyzerCatalog
{
    /// <summary>Every analyzer in this assembly.</summary>
    public static ImmutableArray<DiagnosticAnalyzer> All { get; } = ImmutableArray.Create<DiagnosticAnalyzer>(
        new LanguageRules(),
        new TemplateRules(),
        new FactorioRules());

    /// <summary>All rules, for documentation and for a host that wants to list them.</summary>
    public static ImmutableArray<DiagnosticDescriptor> Descriptors { get; } = All
        .SelectMany(analyzer => analyzer.SupportedDiagnostics)
        .OrderBy(descriptor => descriptor.Id, System.StringComparer.Ordinal)
        .ToImmutableArray();
}
