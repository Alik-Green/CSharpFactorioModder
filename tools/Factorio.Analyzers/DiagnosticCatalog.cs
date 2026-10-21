using Microsoft.CodeAnalysis;

namespace Factorio.Analyzers;

/// <summary>
/// Every diagnostic this assembly can produce, in one place.
/// <para>
/// Each rule exists because the construct it rejects was observed to fail: either the pinned
/// CSharp.lua compiler rejects it, the emitted Lua needs CSharp.lua's CoreSystem runtime (which a
/// Factorio mod cannot ship), or the result would silently do something different in Factorio's
/// Lua 5.2. The rules mirror, at compile time, the checks <c>factorio-mod build</c> performs on the
/// emitted Lua — so a mistake is a red squiggle in the editor instead of a failed build.
/// </para>
/// </summary>
public static class DiagnosticCatalog
{
    private const string DocumentationUrl = "https://github.com/factorio-csharp/factorio-csharp/blob/main/docs/ANALYZERS.md";

    private static DiagnosticDescriptor Rule(
        string id,
        string title,
        string message,
        string description,
        DiagnosticSeverity severity,
        string category = "Factorio")
    {
        return new DiagnosticDescriptor(
            id,
            title,
            message,
            category,
            severity,
            isEnabledByDefault: true,
            description: description,
            helpLinkUri: DocumentationUrl + "#" + id.ToLowerInvariant());
    }

    // ------------------------------------------------------------ language rules

    /// <summary>`using static` of a Factorio namespace: crashes the pinned compiler.</summary>
    public static readonly DiagnosticDescriptor StaticUsingOfFactorio = Rule(
        "FX0001",
        "Do not use 'using static' with Factorio types",
        "'using static {0}' crashes the pinned CSharp.lua compiler; qualify the member instead (Globals.Game, not Game)",
        "A static member reached through a static import is rendered with an implicit receiver, which " +
        "triggers a NullReferenceException inside the compiler. Always write the qualifier: " +
        "Globals.Game.Print(...).",
        DiagnosticSeverity.Error,
        "CSharpLua");

    /// <summary>C# arrays: `System.Array` is CoreSystem-only.</summary>
    public static readonly DiagnosticDescriptor ArrayType = Rule(
        "FX0002",
        "C# arrays cannot be used in a mod",
        "C# arrays need CSharp.lua's CoreSystem runtime, which a Factorio mod cannot ship; use LuaArray<T>, Lists or Native.Table",
        "Arrays are compiled to System.Array.new/System.Array.call, which only exist in CoreSystem.lua. " +
        "For a native Lua array use LuaArray<T>; to build one use Native.Table or a plain Lua helper.",
        DiagnosticSeverity.Error,
        "CSharpLua");

    /// <summary>`foreach`: compiles to `System.each`, which is CoreSystem-only.</summary>
    public static readonly DiagnosticDescriptor ForEach = Rule(
        "FX0003",
        "foreach cannot be used in a mod",
        "'foreach' compiles to System.each, which a Factorio mod cannot ship; use Lists.ForEach / Lists.ForEachArray, or a numeric for loop over LuaArray",
        "CSharp.lua turns foreach into a call into CoreSystem. Iterating a Lua table is done with " +
        "Factorio.Lists, which calls a small Lua function instead; LuaArray<T> is walked with an " +
        "index-based for loop and Length().",
        DiagnosticSeverity.Error,
        "CSharpLua");

    /// <summary>Tuples need CoreSystem for the boxing and the deconstruction.</summary>
    public static readonly DiagnosticDescriptor Tuple = Rule(
        "FX0004",
        "Tuples cannot be used in a mod",
        "Tuples need CSharp.lua's CoreSystem runtime; return several values with 'out' parameters instead",
        "The generated bindings expose native multi-returns as trailing out parameters for the same " +
        "reason. ValueTuple also drags in System.ValueTuple, which the emitted-Lua check rejects.",
        DiagnosticSeverity.Error,
        "CSharpLua");

    /// <summary>An explicit cast: only numeric casts are safe.</summary>
    public static readonly DiagnosticDescriptor Cast = Rule(
        "FX0005",
        "Explicit casts need CoreSystem except for numbers",
        "Cast to '{0}' is emitted as System.cast, which a Factorio mod cannot ship; use 'as' (identity) or keep the value typed",
        "Lua tables carry no runtime type information, so a cast cannot be performed. 'as' compiles " +
        "to an identity, which is honest about the fact that nothing is checked.",
        DiagnosticSeverity.Warning,
        "CSharpLua");

    /// <summary>Integer types: the compiler emits Lua 5.3 integer operators.</summary>
    public static readonly DiagnosticDescriptor NumericType = Rule(
        "FX0006",
        "Use double, not an integer type",
        "'{0}' makes CSharp.lua emit Lua 5.3 integer operators; use double (Factorio runs Lua 5.2)",
        "With an integer type in scope the compiler can emit //, %, bit operators and integer " +
        "division, none of which behave like Lua 5.2. Every number in the bindings is double for " +
        "this reason; Numbers/Ticks provide the rounding helpers.",
        DiagnosticSeverity.Error,
        "CSharpLua");

    /// <summary>string.Format / Convert.ToString: CoreSystem-only, and banned by the Lua check.</summary>
    public static readonly DiagnosticDescriptor StringFormat = Rule(
        "FX0007",
        "String formatting needs CoreSystem",
        "'{0}' compiles to a CoreSystem call; build the string with concatenation ('a' + x + 'b') instead",
        "C# string + object works: the compatibility shim prints numbers like C# does. Format " +
        "strings, Convert.* and interpolations with a format or alignment clause do not - they " +
        "end up as System.String.Format / System.Convert, which factorio-mod build rejects.",
        DiagnosticSeverity.Error,
        "CSharpLua");

    /// <summary>Namespaces whose members need the .NET runtime.</summary>
    public static readonly DiagnosticDescriptor BannedNamespace = Rule(
        "FX0008",
        "Namespace is not available inside Factorio",
        "'{0}' is not available in a mod (no .NET runtime, no console, no file system)",
        "Factorio runs the mod in a sandbox with no io, no os, no threads and no reflection. " +
        "Anything from these namespaces would either fail to compile or fail at run time.",
        DiagnosticSeverity.Error,
        "CSharpLua");

    /// <summary>try/throw/lock/using/async/yield/delegates: all CoreSystem or unsupported.</summary>
    public static readonly DiagnosticDescriptor UnsupportedConstruct = Rule(
        "FX0009",
        "Construct is not supported by the mod runtime",
        "'{0}' is not supported: {1}",
        "Exceptions, finalisation, locks, iterators, async and user-declared delegates all need " +
        "runtime support that a Factorio mod does not have. Report the problem and return instead.",
        DiagnosticSeverity.Error,
        "CSharpLua");

    // ------------------------------------------------------------ template rules

    /// <summary>An instance member whose template uses its first parameter as the receiver.</summary>
    public static readonly DiagnosticDescriptor TemplateReceiver = Rule(
        "FX0010",
        "A template must name its receiver literally",
        "This instance member's template starts with '{0}'; the receiver must be literal text ({this})",
        "CSharp.lua substitutes a template's first argument as a value, not as a receiver, so " +
        "\"{0}[{1}] = nil\" indexes the wrong thing. Write \"{this}[{0}] = nil\" instead, or move " +
        "the operation into FactorioUtils.lua and call it as a plain function.",
        DiagnosticSeverity.Warning,
        "CSharpLua");

    /// <summary>A table literal with a quoted key inside a template.</summary>
    public static readonly DiagnosticDescriptor TemplateTableLiteral = Rule(
        "FX0011",
        "Table literals with quoted keys break template substitution",
        "This template contains a table literal with a quoted key; it duplicates the key instead of substituting, build the table in Lua instead",
        "Observed in this project: '\"entity-name\", {0}' emitted the key twice, and quoted " +
        "literals came out over-escaped. Build the table in FactorioUtils.lua (util.table) and " +
        "keep the template a plain function call.",
        DiagnosticSeverity.Warning,
        "CSharpLua");

    // ------------------------------------------------------------- Factorio rules

    /// <summary>`is`/`typeof`: Lua has no runtime type information.</summary>
    public static readonly DiagnosticDescriptor TypeTest = Rule(
        "FX0013",
        "Runtime type tests are not possible",
        "'{0}' needs runtime type information, which Lua does not have; use 'as' (which is an identity) or test a real property such as Name or Valid",
        "CSharp.lua compiles `is` to System.is, which the emitted-Lua check rejects: with no type tags " +
        "it could only answer wrongly. `x is null` is fine and compiles to `x == nil`; `x is SomeType` " +
        "is not.",
        DiagnosticSeverity.Error,
        "CSharpLua");

    /// <summary>A .NET type that only CoreSystem implements, reached through a qualifier.</summary>
    public static readonly DiagnosticDescriptor CoreSystemType = Rule(
        "FX0014",
        "This .NET type is only available in CSharp.lua's CoreSystem",
        "'{0}' is implemented by CSharp.lua's CoreSystem, which a Factorio mod cannot ship; use the Factorio API instead",
        "Console, File, Guid, StringBuilder, DateTime, Task, Regex and friends exist in CSharp.lua's " +
        "CoreSystem.Lua, a runtime library that is loaded at start-up. Without it the call is a " +
        "nil index at run time. Output goes through Globals.Game.Print, time through Globals.Game.Tick, " +
        "and there is no file system, no console and no threads.",
        DiagnosticSeverity.Error,
        "Factorio");

    /// <summary>Storage.Data used as a receiver: a static template property cannot be one.</summary>
    public static readonly DiagnosticDescriptor StorageDataReceiver = Rule(
        "FX0012",
        "Storage.Data cannot be used as a receiver",
        "'Storage.Data' used as a receiver crashes the pinned compiler; use Storage.Table(key) or Storage.Slot<T>() and call the table's methods",
        "A static property with a template is only substituted when it is a value; using it as " +
        "the object of a call or index emits an empty receiver. The typed helpers exist for this.",
        DiagnosticSeverity.Warning);
}
