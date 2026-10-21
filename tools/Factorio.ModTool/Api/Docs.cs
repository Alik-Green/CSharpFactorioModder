using System.Text.RegularExpressions;

namespace Factorio.ModTool.Api;

/// <summary>
/// Turns the markdown used by lua-api.factorio.com into XML doc comments, and supplies a
/// generated summary when the official documentation has none.
///
/// The goal is that every public member of the generated API has a `&lt;summary&gt;`, either taken
/// from Factorio's documentation or written here from the member's shape (see
/// <see cref="Fallback(string, string)"/>).
/// </summary>
public static class Docs
{
    private const int MaxLength = 700;

    /// <summary>Factorio JSON stores markdown; XML comments need escaped plain text.</summary>
    public static string ToSummary(string? markdown, int max = MaxLength)
    {
        if (string.IsNullOrWhiteSpace(markdown)) return "";
        var s = markdown.Replace("\r", " ");
        s = s.Replace("\n", " ");
        // [text](runtime:Type) -> text
        s = Regex.Replace(s, @"\[([^\]]*)\]\([^)]*\)", "$1");
        // `code` -> code
        s = s.Replace("`", "");
        // **bold** / *italics* -> plain
        s = Regex.Replace(s, @"\*\*([^*]+)\*\*", "$1");
        s = Regex.Replace(s, @"(?<!\*)\*([^*]+)\*(?!\*)", "$1");
        // factorio doc anchors like :::info ... ::: -> dropped
        s = Regex.Replace(s, @":::+\w*", " ");
        s = Regex.Replace(s, @"\s+", " ").Trim();
        if (s.Length > max)
        {
            var cut = s.LastIndexOfAny(new[] { '.', ';', ' ' }, Math.Min(max, s.Length - 1));
            if (cut < max / 2) cut = max;
            s = s.Substring(0, cut).TrimEnd() + " ...";
        }
        return Escape(s);
    }

    public static string Escape(string text) =>
        text.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;");

    /// <summary>
    /// A summary written by the generator for members that the JSON documents with an empty
    /// description. Keeps the native name visible, because that is what a modder looks up in the
    /// Factorio API reference.
    /// </summary>
    public static string Fallback(string kind, string luaName, string? owner = null)
    {
        var where = owner == null ? "" : $" of <c>{Escape(owner)}</c>";
        return kind switch
        {
            "read" => $"Reads the native Lua attribute <c>{Escape(luaName)}</c>{where}.",
            "write" => $"Writes the native Lua attribute <c>{Escape(luaName)}</c>{where}.",
            "readwrite" => $"Gets or sets the native Lua attribute <c>{Escape(luaName)}</c>{where}.",
            "method" => $"Calls the native Lua method <c>{Escape(luaName)}</c>{where}.",
            "function" => $"Calls the native global Lua function <c>{Escape(luaName)}</c>.",
            "defines" => $"Value of the native <c>defines.{Escape(luaName)}</c> constant.",
            "type" => $"Native Factorio object type <c>{Escape(luaName)}</c>.",
            "table" => $"Lua table used by <c>{Escape(owner ?? luaName)}</c>.",
            "field" => $"The native table field <c>{Escape(luaName)}</c>{where}; Factorio's API reference does not describe it.",
            _ => $"Factorio API member <c>{Escape(luaName)}</c>.",
        };
    }

    /// <summary>Link to the official documentation for a type in a given API version.</summary>
    public static string DocLink(string version, string stage, string? type = null) =>
        type == null
            ? $"https://lua-api.factorio.com/{version}/{stage}-api.html"
            : $"https://lua-api.factorio.com/{version}/{stage}-api.html#{type}";

    /// <summary>One-line `<remarks>` for members whose binding is not exact.</summary>
    public static string Remark(string text) => Escape(text);
}
