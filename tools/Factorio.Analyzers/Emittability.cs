using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Factorio.Analyzers;

/// <summary>
/// Answers whether CSharp.lua will even look at a piece of C#.
///
/// A member marked <c>/// @CSharpLua.Template</c>, <c>@CSharpLua.Get</c> or <c>@CSharpLua.Set</c>
/// is replaced wholesale by the template; its C# body is never compiled, so the rules must not read
/// it. The bindings rely on that: every templated method's body is
/// <c>=&gt; throw new NotImplementedException();</c>, which is exactly the shape the language rules
/// would otherwise reject a hundred times over.
///
/// A type marked <c>/// @CSharpLua.Ignore</c> is not emitted at all, so nothing inside it matters
/// either. This is how the generated bindings declare every Factorio class.
/// </summary>
internal static class Emittability
{
    /// <summary>Markers that replace or remove the C# that follows them.</summary>
    private static readonly string[] BodyReplacing =
    {
        "@CSharpLua.Ignore",
        "@CSharpLua.Template",
        "@CSharpLua.Get",
        "@CSharpLua.Set",
        "@CSharpLua.Operator",
    };

    /// <summary>True when the node lives inside an <c>@CSharpLua.Ignore</c> type.</summary>
    /// <param name="node">Any node.</param>
    /// <returns>True when the compiler skips the whole declaration.</returns>
    public static bool IsIgnored(SyntaxNode node) => Annotated(node, "@CSharpLua.Ignore");

    /// <summary>True when the node lives in a body that the compiler replaces or skips.</summary>
    /// <param name="node">Any node.</param>
    /// <returns>True when analysing this node would be analysing dead C#.</returns>
    public static bool IsNotEmitted(SyntaxNode node) => Annotated(node, BodyReplacing);

    /// <summary>True when this declaration itself carries one of the given doc-comment markers.</summary>
    /// <param name="node">A declaration.</param>
    /// <param name="markers">Marker texts to look for, e.g. <c>@CSharpLua.Template</c>.</param>
    /// <returns>True when any marker appears in its leading doc comment.</returns>
    public static bool HasOwnMarker(SyntaxNode node, params string[] markers) =>
        node is MemberDeclarationSyntax && Mentions(node, markers);

    /// <summary>Walks up to the nearest declaration and any declaration above it.</summary>
    private static bool Annotated(SyntaxNode node, params string[] markers)
    {
        for (var current = node; current != null; current = current.Parent)
        {
            if (current is not MemberDeclarationSyntax) continue;
            if (Mentions(current, markers)) return true;
        }

        return false;
    }

    /// <summary>Scans a declaration's leading doc comment for the marker texts.</summary>
    private static bool Mentions(SyntaxNode declaration, params string[] markers)
    {
        var trivia = declaration.GetLeadingTrivia();
        if (trivia.Count == 0) return false;

        var text = trivia.ToFullString();
        foreach (var marker in markers)
            if (text.IndexOf(marker, StringComparison.Ordinal) >= 0)
                return true;

        return false;
    }
}
