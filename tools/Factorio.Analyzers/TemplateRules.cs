using System.Collections.Immutable;
using System.Text.RegularExpressions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Factorio.Analyzers;

/// <summary>
/// Checks the <c>/// @CSharpLua.Template = "..."</c> doc-comment attributes that hand-written
/// bindings use, against the shapes that are known to break the pinned compiler.
///
/// Two failures were hit while building the shipped bindings, and both produce Lua that compiles
/// and then behaves wrongly - the worst kind, because nothing complains:
///   * a table literal with a quoted key duplicated the key instead of substituting the argument;
///   * an instance template that starts with <c>{0}</c> treated a parameter as the receiver.
///
/// Generated code is exempt: it is produced by the generator's own templates, which a test pins.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class TemplateRules : DiagnosticAnalyzer
{
    /// <inheritdoc/>
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = ImmutableArray.Create(
        DiagnosticCatalog.TemplateReceiver,
        DiagnosticCatalog.TemplateTableLiteral);

    // Mirrors CSharp.lua's own parser: the value runs to the end of the line and has its
    // surrounding quotes trimmed, so a template may legitimately contain inner quotes -
    // `util.make({ t, {"a"} })` is one line, not three tokens. An earlier version of this
    // regex stopped at the first inner quote, which made FX0011 impossible to trigger.
    private static readonly Regex Marker = new(
        @"@CSharpLua\.(?<kind>Template|Get|Set)\s*=\s*(?<value>.+?)\s*$",
        RegexOptions.Compiled | RegexOptions.Multiline);

    /// <inheritdoc/>
    public override void Initialize(AnalysisContext context)
    {
        context.EnableConcurrentExecution();
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);

        context.RegisterSyntaxNodeAction(AnalyzeMember, SyntaxKind.MethodDeclaration);
        context.RegisterSyntaxNodeAction(AnalyzeMember, SyntaxKind.PropertyDeclaration);
        context.RegisterSyntaxNodeAction(AnalyzeMember, SyntaxKind.ConstructorDeclaration);
        context.RegisterSyntaxNodeAction(AnalyzeMember, SyntaxKind.IndexerDeclaration);
    }

    private static void AnalyzeMember(SyntaxNodeAnalysisContext context)
    {
        var member = (MemberDeclarationSyntax)context.Node;
        var trivia = member.GetLeadingTrivia();
        if (!trivia.ToString().Contains("@CSharpLua.")) return;

        var isStatic = member.Modifiers.Any(SyntaxKind.StaticKeyword);

        foreach (var piece in trivia)
        {
            if (!piece.IsKind(SyntaxKind.SingleLineDocumentationCommentTrivia)) continue;

            var text = piece.ToString();
            foreach (Match match in Marker.Matches(text))
            {
                var value = match.Groups["value"].Value.Trim().Trim('"');
                var kind = match.Groups["kind"].Value;

                // A table literal with a quoted key: the compiler emits the key twice and drops
                // the argument. Build the table in FactorioUtils.lua instead.
                if (value.Contains("{\""))
                {
                    context.ReportDiagnostic(Diagnostic.Create(
                        DiagnosticCatalog.TemplateTableLiteral, piece.GetLocation()));
                }

                // An instance member whose template starts with {0} is using its first parameter
                // as the receiver; the receiver has to be literally "{this}".
                if (!isStatic && kind == "Template" && value.StartsWith("{0}", System.StringComparison.Ordinal))
                {
                    context.ReportDiagnostic(Diagnostic.Create(
                        DiagnosticCatalog.TemplateReceiver, piece.GetLocation(), "{0}"));
                }
            }
        }
    }
}
