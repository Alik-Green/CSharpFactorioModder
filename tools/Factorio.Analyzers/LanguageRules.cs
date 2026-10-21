using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Factorio.Analyzers;

/// <summary>
/// Rejects C# that either the pinned CSharp.lua compiler cannot lower or the emitted Lua cannot
/// run inside Factorio (no CoreSystem, no .NET, Lua 5.2 semantics).
///
/// Every rule is purely syntactic, so it works on a single file without references - which is what
/// lets <c>factorio-mod build</c> check a mod that has no csproj.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class LanguageRules : DiagnosticAnalyzer
{
    /// <inheritdoc/>
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = ImmutableArray.Create(
        DiagnosticCatalog.StaticUsingOfFactorio,
        DiagnosticCatalog.ArrayType,
        DiagnosticCatalog.ForEach,
        DiagnosticCatalog.Tuple,
        DiagnosticCatalog.Cast,
        DiagnosticCatalog.NumericType,
        DiagnosticCatalog.StringFormat,
        DiagnosticCatalog.BannedNamespace,
        DiagnosticCatalog.UnsupportedConstruct,
        DiagnosticCatalog.TypeTest,
        DiagnosticCatalog.CoreSystemType);

    private static readonly HashSet<string> BannedNamespaces = new(System.StringComparer.Ordinal)
    {
        "System.IO",
        "System.Linq",
        "System.Threading",
        "System.Threading.Tasks",
        "System.Net",
        "System.Net.Http",
        "System.Collections.Generic",
        "System.Collections.Concurrent",
        "System.Text",
        "System.Text.RegularExpressions",
        "System.Reflection",
        "System.Diagnostics",
        "System.Runtime.InteropServices",
        "System.Security.Cryptography",
        "System.Globalization",
        "System.Xml",
    };

    /// <summary>Namespaces whose *members* are also worth catching when used with a qualifier.</summary>
    private static readonly HashSet<string> BannedTypeRoots = new(System.StringComparer.Ordinal)
    {
        "Console", "File", "Directory", "Path", "Environment", "Guid", "DateTime", "DateTimeOffset",
        "TimeSpan", "Random", "StringBuilder", "List", "Dictionary", "HashSet", "Queue", "Stack",
        "Task", "Thread", "Regex", "Stopwatch", "Debug", "Trace", "Marshal",
    };

    /// <summary>Integer and non-double numeric types.</summary>
    private static readonly HashSet<string> BannedNumericTypes = new(System.StringComparer.Ordinal)
    {
        "int", "uint", "long", "ulong", "short", "ushort", "byte", "sbyte", "float", "decimal", "char",
    };

    /// <summary>Casts to these are still emitted as plain Lua expressions.</summary>
    private static readonly HashSet<string> AllowedCastTargets = new(System.StringComparer.Ordinal)
    {
        "double", "float", "int", "long", "short", "byte", "sbyte", "uint", "ulong", "ushort",
        "object", "string", "bool",
    };

    private const string Unsupported = "FX0009";

    /// <inheritdoc/>
    public override void Initialize(AnalysisContext context)
    {
        context.EnableConcurrentExecution();
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze);

        context.RegisterSyntaxNodeAction(AnalyzeUsing, SyntaxKind.UsingDirective);
        context.RegisterSyntaxNodeAction(AnalyzeArrayType, SyntaxKind.ArrayType);
        context.RegisterSyntaxNodeAction(AnalyzeArrayCreation,
            SyntaxKind.ArrayCreationExpression,
            SyntaxKind.ImplicitArrayCreationExpression,
            SyntaxKind.StackAllocArrayCreationExpression);
        context.RegisterSyntaxNodeAction(AnalyzeForEach, SyntaxKind.ForEachStatement);
        context.RegisterSyntaxNodeAction(AnalyzeTuple,
            SyntaxKind.TupleExpression,
            SyntaxKind.TupleType,
            SyntaxKind.ParenthesizedVariableDesignation);
        context.RegisterSyntaxNodeAction(AnalyzeCast, SyntaxKind.CastExpression);
        context.RegisterSyntaxNodeAction(AnalyzePredefinedType, SyntaxKind.PredefinedType);
        context.RegisterSyntaxNodeAction(AnalyzeInvocation, SyntaxKind.InvocationExpression);
        context.RegisterSyntaxNodeAction(AnalyzeInterpolation, SyntaxKind.InterpolatedStringExpression);
        context.RegisterSyntaxNodeAction(AnalyzeUnsupported, SyntaxKind.DefaultExpression);
        context.RegisterSyntaxNodeAction(AnalyzeTypeTest,
            SyntaxKind.IsPatternExpression,
            SyntaxKind.IsExpression,
            SyntaxKind.TypeOfExpression);
        context.RegisterSyntaxNodeAction(AnalyzeUnsupported, SyntaxKind.ThrowStatement, SyntaxKind.ThrowExpression);
        context.RegisterSyntaxNodeAction(AnalyzeUnsupported, SyntaxKind.TryStatement);
        context.RegisterSyntaxNodeAction(AnalyzeUnsupported, SyntaxKind.LockStatement);
        context.RegisterSyntaxNodeAction(AnalyzeUnsupported, SyntaxKind.UsingStatement);
        context.RegisterSyntaxNodeAction(AnalyzeUnsupported, SyntaxKind.AwaitExpression);
        context.RegisterSyntaxNodeAction(AnalyzeUnsupported, SyntaxKind.YieldReturnStatement, SyntaxKind.YieldBreakStatement);
        context.RegisterSyntaxNodeAction(AnalyzeUnsupported, SyntaxKind.DelegateDeclaration);
        context.RegisterSyntaxNodeAction(AnalyzeUnsupported, SyntaxKind.EventDeclaration, SyntaxKind.EventFieldDeclaration);
        context.RegisterSyntaxNodeAction(AnalyzeAsyncMethod, SyntaxKind.MethodDeclaration, SyntaxKind.LocalFunctionStatement);
        context.RegisterSyntaxNodeAction(AnalyzeCoreSystemType,
            SyntaxKind.SimpleMemberAccessExpression,
            SyntaxKind.ObjectCreationExpression);
    }

    // -------------------------------------------------------------- using static

    private static void AnalyzeUsing(SyntaxNodeAnalysisContext context)
    {
        if (Emittability.IsNotEmitted(context.Node)) return;
        var directive = (UsingDirectiveSyntax)context.Node;
        var name = directive.Name?.ToString() ?? "";
        if (name.Length == 0) return;

        if (directive.StaticKeyword.IsKind(SyntaxKind.StaticKeyword))
        {
            // Only Factorio's own types are affected: the crash comes from the @CSharpLua attributes
            // on their members, not from `using static System.Math`.
            var isFactorioType = name == "Globals" || name.EndsWith(".Globals", System.StringComparison.Ordinal) ||
                                 name.StartsWith("Factorio.", System.StringComparison.Ordinal) ||
                                 name == "Factorio" ||
                                 name.Contains(".Factorio.");
            if (isFactorioType)
                context.ReportDiagnostic(Diagnostic.Create(
                    DiagnosticCatalog.StaticUsingOfFactorio, directive.GetLocation(), name));
            return;
        }

        if (IsBannedNamespace(name))
            context.ReportDiagnostic(Diagnostic.Create(
                DiagnosticCatalog.BannedNamespace, directive.GetLocation(), name));
    }

    private static bool IsBannedNamespace(string name)
    {
        foreach (var banned in BannedNamespaces)
            if (name == banned || name.StartsWith(banned + ".", System.StringComparison.Ordinal))
                return true;
        return false;
    }

    // ---------------------------------------------------------------------- arrays

    private static void AnalyzeArrayType(SyntaxNodeAnalysisContext context)
    {
        if (Emittability.IsNotEmitted(context.Node)) return;
        var node = (ArrayTypeSyntax)context.Node;

        // `new string[2]` reports the creation; without this the type inside it would report again.
        if (node.Parent is ArrayCreationExpressionSyntax or ImplicitArrayCreationExpressionSyntax or
            StackAllocArrayCreationExpressionSyntax)
            return;

        context.ReportDiagnostic(Diagnostic.Create(DiagnosticCatalog.ArrayType, node.GetLocation()));
    }

    private static void AnalyzeArrayCreation(SyntaxNodeAnalysisContext context)
    {
        if (Emittability.IsNotEmitted(context.Node)) return;
        context.ReportDiagnostic(Diagnostic.Create(DiagnosticCatalog.ArrayType, context.Node.GetLocation()));
    }

    // --------------------------------------------------------------------- foreach

    private static void AnalyzeForEach(SyntaxNodeAnalysisContext context)
    {
        if (Emittability.IsNotEmitted(context.Node)) return;
        context.ReportDiagnostic(Diagnostic.Create(DiagnosticCatalog.ForEach, context.Node.GetLocation()));
    }

    // ---------------------------------------------------------------------- tuples

    private static void AnalyzeTuple(SyntaxNodeAnalysisContext context)
    {
        if (Emittability.IsNotEmitted(context.Node)) return;
        context.ReportDiagnostic(Diagnostic.Create(DiagnosticCatalog.Tuple, context.Node.GetLocation()));
    }

    // ----------------------------------------------------------------------- casts

    private static void AnalyzeCast(SyntaxNodeAnalysisContext context)
    {
        if (Emittability.IsNotEmitted(context.Node)) return;
        var cast = (CastExpressionSyntax)context.Node;
        var target = cast.Type.ToString();
        if (AllowedCastTargets.Contains(target)) return;

        context.ReportDiagnostic(Diagnostic.Create(
            DiagnosticCatalog.Cast, cast.Type.GetLocation(), target));
    }

    // -------------------------------------------------------------- numeric types

    private static void AnalyzePredefinedType(SyntaxNodeAnalysisContext context)
    {
        if (Emittability.IsNotEmitted(context.Node)) return;
        var type = (PredefinedTypeSyntax)context.Node;
        var text = type.Keyword.Text;
        if (!BannedNumericTypes.Contains(text)) return;

        // Every use of the keyword is flagged, including `(int)` inside a cast: FX0005 allows the
        // cast, but the type itself still cannot appear in Lua 5.2. `double` and `bool` fall through.
        context.ReportDiagnostic(Diagnostic.Create(DiagnosticCatalog.NumericType, type.GetLocation(), text));
    }

    // ------------------------------------------------------- CoreSystem-only types

    /// <summary>
    /// Console.WriteLine, Guid.NewGuid(), new StringBuilder(), DateTime.Now and the like. They are
    /// real APIs in CSharp.lua's CoreSystem, so they compile - and then index nil in a mod.
    /// </summary>
    private static void AnalyzeCoreSystemType(SyntaxNodeAnalysisContext context)
    {
        if (Emittability.IsNotEmitted(context.Node)) return;

        var name = context.Node switch
        {
            MemberAccessExpressionSyntax member => LastIdentifier(member.Expression),
            ObjectCreationExpressionSyntax creation => LastIdentifier(creation.Type),
            _ => null,
        };
        if (name == null || !BannedTypeRoots.Contains(name)) return;

        // A mod is free to define its own Console; only an undeclared name can be the .NET type.
        if (IsDeclaredInFile(context.Node, name)) return;

        context.ReportDiagnostic(Diagnostic.Create(DiagnosticCatalog.CoreSystemType, context.Node.GetLocation(), name));
    }

    /// <summary>
    /// The type name at the end of a qualified name: <c>Console</c> for both <c>Console</c> and
    /// <c>System.Console</c>. Null when the expression is not a name (a call, an index, a literal).
    /// </summary>
    private static string? LastIdentifier(SyntaxNode? node) => node switch
    {
        IdentifierNameSyntax identifier => identifier.Identifier.Text,
        QualifiedNameSyntax qualified => LastIdentifier(qualified.Right),
        MemberAccessExpressionSyntax member => LastIdentifier(member.Name),
        AliasQualifiedNameSyntax alias => alias.Name.Identifier.Text,
        _ => null,
    };

    /// <summary>Whether this syntax tree declares something called <paramref name="name"/>.</summary>
    private static bool IsDeclaredInFile(SyntaxNode node, string name)
    {
        foreach (var candidate in node.SyntaxTree.GetRoot().DescendantNodes())
        {
            var declared = candidate switch
            {
                BaseTypeDeclarationSyntax type => type.Identifier.Text,
                DelegateDeclarationSyntax function => function.Identifier.Text,
                MethodDeclarationSyntax method => method.Identifier.Text,
                PropertyDeclarationSyntax property => property.Identifier.Text,
                VariableDeclaratorSyntax variable => variable.Identifier.Text,
                ParameterSyntax parameter => parameter.Identifier.Text,
                _ => null,
            };

            if (declared == name) return true;
        }

        return false;
    }

    // ------------------------------------------------------------------ formatting

    private static void AnalyzeInvocation(SyntaxNodeAnalysisContext context)
    {
        if (Emittability.IsNotEmitted(context.Node)) return;
        var invocation = (InvocationExpressionSyntax)context.Node;
        if (invocation.Expression is not MemberAccessExpressionSyntax member) return;

        var receiver = member.Expression.ToString();
        var name = member.Name.Identifier.Text;

        var isStringFormat = name == "Format" &&
                             (receiver == "string" || receiver == "String" ||
                              receiver == "System.String" || receiver == "global::System.String");
        if (isStringFormat)
        {
            context.ReportDiagnostic(Diagnostic.Create(DiagnosticCatalog.StringFormat, member.GetLocation(), "string.Format"));
            return;
        }

        if (receiver == "Convert" || receiver == "System.Convert" || receiver == "global::System.Convert")
            context.ReportDiagnostic(Diagnostic.Create(DiagnosticCatalog.StringFormat, member.GetLocation(), receiver + "." + name));
    }

    private static void AnalyzeInterpolation(SyntaxNodeAnalysisContext context)
    {
        if (Emittability.IsNotEmitted(context.Node)) return;
        var interpolation = (InterpolatedStringExpressionSyntax)context.Node;
        foreach (var part in interpolation.Contents)
        {
            if (part is not InterpolationSyntax hole) continue;

            // `$"{x:F2}"` and `$"{x,5}"` are string.Format calls in disguise.
            if (hole.FormatClause == null && hole.AlignmentClause == null) continue;
            context.ReportDiagnostic(Diagnostic.Create(
                DiagnosticCatalog.StringFormat, hole.GetLocation(), hole.ToString()));
        }
    }

    // ------------------------------------------------------- unsupported constructs

    /// <summary>`x is null` is fine (`== nil`); `x is SomeType`, `is T t` and `typeof(T)` are not.</summary>
    private static void AnalyzeTypeTest(SyntaxNodeAnalysisContext context)
    {
        if (Emittability.IsNotEmitted(context.Node)) return;
        switch (context.Node)
        {
            case TypeOfExpressionSyntax typeOf:
                Report(typeOf, "typeof(" + typeOf.Type + ")");
                break;
            case IsPatternExpressionSyntax isPattern when IsTypePattern(isPattern.Pattern):
                Report(isPattern, Shorten(isPattern.ToString()));
                break;
            case BinaryExpressionSyntax binary when binary.IsKind(SyntaxKind.IsExpression):
                Report(binary, Shorten(binary.ToString()));
                break;
        }

        void Report(SyntaxNode node, string text) =>
            context.ReportDiagnostic(Diagnostic.Create(DiagnosticCatalog.TypeTest, node.GetLocation(), text));
    }

    private static bool IsTypePattern(PatternSyntax pattern) => pattern switch
    {
        TypePatternSyntax => true,
        DeclarationPatternSyntax => true,
        RecursivePatternSyntax recursive => recursive.Type != null,
        BinaryPatternSyntax binary => IsTypePattern(binary.Left) || IsTypePattern(binary.Right),
        ParenthesizedPatternSyntax parenthesized => IsTypePattern(parenthesized.Pattern),
        _ => false,
    };

    private static string Shorten(string text) => text.Length <= 48 ? text : text.Substring(0, 45) + "...";

    private static void AnalyzeUnsupported(SyntaxNodeAnalysisContext context)
    {
        if (Emittability.IsNotEmitted(context.Node)) return;
        var (construct, why) = context.Node.Kind() switch
        {
            SyntaxKind.ThrowStatement or SyntaxKind.ThrowExpression =>
                ("throw", "exceptions are compiled to System.throw, which a mod cannot ship"),
            SyntaxKind.TryStatement =>
                ("try/catch/finally", "there is no exception runtime to catch with"),
            SyntaxKind.LockStatement =>
                ("lock", "a mod runs on a single engine thread; there are no locks to take"),
            SyntaxKind.UsingStatement =>
                ("using statement", "there is no IDisposable support; Lua has no finalisers here"),
            SyntaxKind.AwaitExpression =>
                ("await", "there is no task scheduler inside Factorio"),
            SyntaxKind.YieldReturnStatement or SyntaxKind.YieldBreakStatement =>
                ("yield", "iterators compile to a state machine backed by CoreSystem"),
            SyntaxKind.DelegateDeclaration =>
                ("delegate declaration", "user-declared delegates need runtime type information; use Action<T>/Func<T> parameters"),
            SyntaxKind.EventDeclaration or SyntaxKind.EventFieldDeclaration =>
                ("event", "events need delegate support; keep the handlers in a field and call them directly"),
            SyntaxKind.DefaultExpression =>
                ("default(T)", "it compiles to System.default; write null, 0 or the value you mean"),
            _ => ("", ""),
        };

        if (construct.Length == 0) return;
        context.ReportDiagnostic(Diagnostic.Create(
            DiagnosticCatalog.UnsupportedConstruct, context.Node.GetLocation(), construct, why));
    }

    private static void AnalyzeAsyncMethod(SyntaxNodeAnalysisContext context)
    {
        if (Emittability.IsNotEmitted(context.Node)) return;
        var modifiers = context.Node switch
        {
            MethodDeclarationSyntax method => method.Modifiers,
            LocalFunctionStatementSyntax local => local.Modifiers,
            _ => default,
        };

        foreach (var modifier in modifiers)
        {
            if (!modifier.IsKind(SyntaxKind.AsyncKeyword)) continue;
            context.ReportDiagnostic(Diagnostic.Create(
                DiagnosticCatalog.UnsupportedConstruct, modifier.GetLocation(),
                "async", "there is no task scheduler inside Factorio"));
        }
    }
}
