using System.Text;

namespace Factorio.ModTool.Api;

public sealed class CodeWriter
{
    private readonly StringBuilder _sb = new();
    private int _indent;
    public string IndentText { get; set; } = "    ";

    public void Line(string s = "")
    {
        if (s.Length == 0) { _sb.Append('\n'); return; }
        for (int i = 0; i < _indent; i++) _sb.Append(IndentText);
        _sb.Append(s).Append('\n');
    }

    public void Open(string s)
    {
        Line(s);
        Line("{");
        _indent++;
    }

    public void Close(string suffix = "")
    {
        _indent--;
        Line("}" + suffix);
    }

    public IDisposable Block(string s)
    {
        Open(s);
        return new Closer(this);
    }

    private sealed class Closer : IDisposable
    {
        private readonly CodeWriter _w;
        public Closer(CodeWriter w) { _w = w; }
        public void Dispose() => _w.Close();
    }

    public override string ToString() => _sb.ToString();
}

public static class CSharpSyntax
{
    private static readonly HashSet<string> Keywords = new(StringComparer.Ordinal)
    {
        "abstract","as","base","bool","break","byte","case","catch","char","checked","class","const","continue",
        "decimal","default","delegate","do","double","else","enum","event","explicit","extern","false","finally",
        "fixed","float","for","foreach","goto","if","implicit","in","int","interface","internal","is","lock","long",
        "namespace","new","null","object","operator","out","override","params","private","protected","public",
        "readonly","ref","return","sbyte","sealed","short","sizeof","stackalloc","static","string","struct","switch",
        "this","throw","true","try","typeof","uint","ulong","unchecked","unsafe","ushort","using","virtual","void",
        "volatile","while",
    };

    private static readonly HashSet<string> LuaKeywords = new(StringComparer.Ordinal)
    {
        "and","break","do","else","elseif","end","false","for","function","goto","if","in","local","nil","not",
        "or","repeat","return","then","true","until","while",
    };

    public static bool IsLuaKeyword(string name) => LuaKeywords.Contains(name);

    /// <summary>C# identifier, escaping reserved keywords with '@'.</summary>
    public static string Id(string name)
    {
        if (Keywords.Contains(name)) return "@" + name;
        var sb = new StringBuilder();
        for (int i = 0; i < name.Length; i++)
        {
            char c = name[i];
            bool ok = char.IsLetterOrDigit(c) || c == '_';
            sb.Append(ok ? c : '_');
        }
        if (sb.Length == 0 || char.IsDigit(sb[0])) sb.Insert(0, '_');
        var s = sb.ToString();
        return Keywords.Contains(s) ? "@" + s : s;
    }

    /// <summary>
    /// C# member identifier. C# forbids a member with the same name as its enclosing type
    /// (`defines.trivial_smoke.trivial_smoke`), so a trailing underscore is appended in that case.
    /// The Lua name is unaffected because every member carries an explicit code template.
    /// </summary>
    public static string MemberId(string name, string typeName)
    {
        var id = Id(name);
        return id == typeName ? id + "_" : id;
    }

    /// <summary>A Lua table key: bare when it is a valid identifier, bracketed otherwise.</summary>
    public static string LuaKey(string name) =>
        IsLuaKeyword(name) || !IsLuaIdentifier(name) ? "[\"" + name + "\"]" : name;

    public static bool IsLuaIdentifier(string name)
    {
        if (name.Length == 0 || char.IsDigit(name[0])) return false;
        return name.All(c => char.IsLetterOrDigit(c) || c == '_');
    }

    public static string Xml(string text) =>
        text.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;");

    public static string Summary(string description, int max = 400)
    {
        var s = description.Replace("\r", " ").Replace("\n", " ").Trim();
        // strip markdown links: [text](link) -> text
        s = System.Text.RegularExpressions.Regex.Replace(s, @"\[([^\]]*)\]\([^)]*\)", "$1");
        if (s.Length > max) s = s.Substring(0, max).TrimEnd() + " ...";
        return Xml(s);
    }

    /// <summary>
    /// A code template as it appears in a `/// @CSharpLua.Template = "..."` doc comment.
    /// Doc comments are raw text: the compiler runs `Trim().Trim('"')` over the value, so quotes
    /// inside the template must NOT be C#-escaped (a backslash would end up in the Lua output).
    /// </summary>
    public static string TemplateStringLiteral(string template) => "\"" + template + "\"";
}
