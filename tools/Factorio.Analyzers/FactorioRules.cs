using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Factorio.Analyzers;

/// <summary>
/// Rules about the bindings' own API, as opposed to the C# language.
///
/// These are the cases where the code compiles, the build succeeds, and the result is still wrong -
/// so they are caught here, where the message can explain what to use instead.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class FactorioRules : DiagnosticAnalyzer
{
    /// <inheritdoc/>
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } =
        ImmutableArray.Create(DiagnosticCatalog.StorageDataReceiver);

    /// <inheritdoc/>
    public override void Initialize(AnalysisContext context)
    {
        context.EnableConcurrentExecution();
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.RegisterSyntaxNodeAction(AnalyzeMemberAccess, SyntaxKind.SimpleMemberAccessExpression);
        context.RegisterSyntaxNodeAction(AnalyzeElementAccess, SyntaxKind.ElementAccessExpression);
    }

    private static void AnalyzeMemberAccess(SyntaxNodeAnalysisContext context)
    {
        if (Emittability.IsNotEmitted(context.Node)) return;
        var access = (MemberAccessExpressionSyntax)context.Node;
        if (!EndsWithStorageData(access)) return;

        // `Storage.Data.Get("x")`: the outer access uses this one as its receiver.
        var receiver = access.Parent is MemberAccessExpressionSyntax outer && outer.Expression == access;
        var invoked = access.Parent is InvocationExpressionSyntax call && call.Expression == access;
        if (!receiver && !invoked) return;

        context.ReportDiagnostic(Diagnostic.Create(DiagnosticCatalog.StorageDataReceiver, access.GetLocation()));
    }

    private static void AnalyzeElementAccess(SyntaxNodeAnalysisContext context)
    {
        if (Emittability.IsNotEmitted(context.Node)) return;
        var access = (ElementAccessExpressionSyntax)context.Node;
        if (!EndsWithStorageData(access.Expression)) return;
        context.ReportDiagnostic(Diagnostic.Create(DiagnosticCatalog.StorageDataReceiver, access.GetLocation()));
    }

    /// <summary>Matches <c>Storage.Data</c>, <c>Factorio.Storage.Data</c> and the global:: forms.</summary>
    private static bool EndsWithStorageData(ExpressionSyntax expression)
    {
        if (expression is not MemberAccessExpressionSyntax member) return false;
        if (member.Name.Identifier.Text != "Data") return false;

        var owner = member.Expression.ToString();
        return owner == "Storage" || owner == "Factorio.Storage" || owner == "global::Factorio.Storage";
    }
}
