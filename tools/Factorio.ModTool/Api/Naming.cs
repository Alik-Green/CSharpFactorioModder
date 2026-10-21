namespace Factorio.ModTool.Api;

/// <summary>
/// Converts the snake_case / kebab-case names used by the Factorio API into C# identifiers.
///
/// The generated C# API is meant to read like a normal .NET library, so every public member is
/// PascalCase and every parameter is camelCase. The Lua name is never lost: the emitters put it in
/// the `@CSharpLua.Get`/`@CSharpLua.Set`/`@CSharpLua.Template` comment that drives the code
/// generation, so the emitted Lua still uses exactly the name the game expects.
/// </summary>
public static class Naming
{
    private static readonly HashSet<string> CSharpKeywords = new(StringComparer.Ordinal)
    {
        "abstract","as","base","bool","break","byte","case","catch","char","checked","class","const","continue",
        "decimal","default","delegate","do","double","else","enum","event","explicit","extern","false","finally",
        "fixed","float","for","foreach","goto","if","implicit","in","int","interface","internal","is","lock","long",
        "namespace","new","null","object","operator","out","override","params","private","protected","public",
        "readonly","ref","return","sbyte","sealed","short","sizeof","stackalloc","static","string","struct","switch",
        "this","throw","true","try","typeof","uint","ulong","unchecked","unsafe","ushort","using","virtual","void",
        "volatile","while",
        // contextual keywords that are safe to escape anyway
        "record","init","required","scoped","file",
    };

    private static readonly HashSet<string> LuaKeywords = new(StringComparer.Ordinal)
    {
        "and","break","do","else","elseif","end","false","for","function","goto","if","in","local","nil","not",
        "or","repeat","return","then","true","until","while",
    };

    public static bool IsLuaKeyword(string name) => LuaKeywords.Contains(name);

    public static bool IsCSharpKeyword(string name) => CSharpKeywords.Contains(name);

    public static bool IsLuaIdentifier(string name)
    {
        if (string.IsNullOrEmpty(name) || char.IsDigit(name[0])) return false;
        foreach (var c in name)
            if (!char.IsLetterOrDigit(c) && c != '_') return false;
        return true;
    }

    /// <summary>Splits on '_', '-' and '.' and upper-cases the first letter of every part.</summary>
    public static string Pascal(string name)
    {
        if (string.IsNullOrEmpty(name)) return "_";
        var sb = new System.Text.StringBuilder(name.Length);
        bool upper = true;
        foreach (var c in name)
        {
            if (c is '_' or '-' or '.' or ' ')
            {
                upper = true;
                continue;
            }
            if (!char.IsLetterOrDigit(c))
            {
                upper = true;
                continue;
            }
            sb.Append(upper ? char.ToUpperInvariant(c) : c);
            upper = false;
        }
        if (sb.Length == 0) return "_";
        if (char.IsDigit(sb[0])) sb.Insert(0, '_');
        return sb.ToString();
    }

    public static string Camel(string name)
    {
        var pascal = Pascal(name);
        if (pascal.Length == 0) return pascal;
        if (pascal.Length > 1 && char.IsUpper(pascal[1])) return pascal; // keep acronyms: ID -> ID
        return char.ToLowerInvariant(pascal[0]) + pascal.Substring(1);
    }

    /// <summary>Escapes a C# keyword with '@'.</summary>
    public static string Escape(string identifier) =>
        CSharpKeywords.Contains(identifier) ? "@" + identifier : identifier;

    /// <summary>
    /// A member identifier. C# forbids a member with the same name as its enclosing type, so a
    /// trailing underscore is appended in that case (e.g. `Defines.Prototypes.Shortcut.Shortcut`).
    /// The Lua name is unaffected because every member carries an explicit template.
    /// </summary>
    /// <summary>
    /// A member may not be called exactly like the type that contains it (C# error CS0542), so a
    /// clash gets a readable suffix instead of an underscore: `defines.prototypes.achievement.achievement`
    /// becomes `Achievement.Value`, not `Achievement_`.
    /// </summary>
    public static string Member(string pascalName, string enclosingType) =>
        pascalName == enclosingType ? pascalName + "Value" : pascalName;

    /// <summary>A property name that must stay unique inside one type.</summary>
    public static string Unique(string candidate, ISet<string> used)
    {
        if (used.Add(candidate)) return candidate;
        for (int i = 2; ; i++)
        {
            var next = candidate + i;
            if (used.Add(next)) return next;
        }
    }

    /// <summary>Lua member access for a name: `obj.name` when it is a plain identifier, else `obj["a-b"]`.</summary>
    public static string LuaMember(string name) =>
        IsLuaIdentifier(name) && !IsLuaKeyword(name) ? "." + name : "[\"" + name + "\"]";

    /// <summary>Lua table key for a name: `name = v` when it is a plain identifier, else `["a-b"] = v`.</summary>
    public static string LuaKey(string name) =>
        IsLuaIdentifier(name) && !IsLuaKeyword(name) ? name : "[\"" + name + "\"]";
}
