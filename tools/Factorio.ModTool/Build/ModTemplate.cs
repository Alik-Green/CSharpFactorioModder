using System.Text;
using System.Text.RegularExpressions;

namespace Factorio.ModTool.Build;

/// <summary>
/// Instantiates <c>templates/mod/</c> into a buildable Factorio C# mod directory and draws a
/// default <c>thumbnail.png</c>.
/// </summary>
public static class ModTemplate
{
    private static readonly Regex LegalName = new(
        @"^[A-Za-z][A-Za-z0-9_-]{0,99}$",
        RegexOptions.Compiled);

    /// <summary>Returns null when <paramref name="name"/> is a Factorio-legal mod identifier, or why not.</summary>
    public static string? ValidateName(string? name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return "name must not be empty";
        if (!char.IsLetter(name[0]) || name[0] > 127)
            return "name must start with an ASCII letter (A-Z or a-z)";
        if (!LegalName.IsMatch(name))
            return "only ASCII letters, digits, '-' and '_' are allowed";
        return null;
    }

    /// <summary>Derives a PascalCase C# namespace identifier from a Factorio mod name.</summary>
    public static string NamespaceFor(string? name)
    {
        if (string.IsNullOrEmpty(name)) return "Mod";
        var sb = new StringBuilder(name.Length);
        var upperNext = true;
        foreach (var ch in name)
        {
            if (ch is '-' or '_')
            {
                upperNext = true;
                continue;
            }

            if (char.IsLetterOrDigit(ch))
            {
                sb.Append(upperNext ? char.ToUpperInvariant(ch) : ch);
                upperNext = false;
            }
        }

        return sb.Length == 0 ? "Mod" : sb.ToString();
    }

    /// <summary>Derives a readable display title (<c>gas-extractor</c> -&gt; <c>Gas Extractor</c>).</summary>
    public static string TitleFor(string name)
    {
        var words = new List<string>();
        var current = new StringBuilder();

        for (var i = 0; i < name.Length; i++)
        {
            var ch = name[i];
            if (ch is '-' or '_')
            {
                if (current.Length > 0) { words.Add(current.ToString()); current.Clear(); }
                continue;
            }

            if (i > 0 && char.IsUpper(ch) && current.Length > 0 && !char.IsUpper(current[^1]))
            {
                words.Add(current.ToString());
                current.Clear();
            }

            current.Append(ch);
        }

        if (current.Length > 0) words.Add(current.ToString());

        return string.Join(" ", words.Select(w => char.ToUpperInvariant(w[0]) + w[1..]));
    }

    /// <summary>Scaffolds a mod directory from <c>templates/mod/</c> and returns relative paths written.</summary>
    public static IReadOnlyList<string> Create(
        string repoRoot,
        string directory,
        string name,
        string author,
        bool overwrite = false,
        string factorioVersion = "2.0",
        string? title = null,
        string version = "0.1.0")
    {
        var problem = ValidateName(name);
        if (problem != null) throw new ToolException($"'{name}' is not a valid mod name: {problem}");

        var fullTarget = Path.GetFullPath(directory);
        if (Directory.Exists(fullTarget) && Directory.EnumerateFileSystemEntries(fullTarget).Any() && !overwrite)
            throw new ToolException($"destination directory '{fullTarget}' is not empty (pass --force to overwrite)");

        var templateRoot = Path.Combine(repoRoot, "templates", "mod");
        if (!Directory.Exists(templateRoot))
            throw new ToolException($"template directory not found: {templateRoot}");

        var ns = NamespaceFor(name);
        var displayTitle = string.IsNullOrWhiteSpace(title) ? TitleFor(name) : title.Trim();
        var tokens = new Dictionary<string, string>(StringComparer.Ordinal)
        {
            ["{{name}}"] = name,
            ["{{id}}"] = name.ToLowerInvariant().Replace('_', '-'),
            ["{{namespace}}"] = ns,
            ["{{title}}"] = displayTitle,
            ["{{author}}"] = author,
            ["{{version}}"] = version,
            ["{{factorioVersion}}"] = factorioVersion,
            ["{{entry}}"] = $"{ns}.Main.Init",
            ["{{date}}"] = DateTime.UtcNow.ToString("yyyy-MM-dd"),
        };

        Directory.CreateDirectory(fullTarget);
        var utf8NoBom = new UTF8Encoding(false);
        var written = new List<string>();

        foreach (var templateFile in Directory.EnumerateFiles(templateRoot, "*", SearchOption.AllDirectories)
                     .OrderBy(p => p, StringComparer.Ordinal))
        {
            var relPath = ApplyTokens(Path.GetRelativePath(templateRoot, templateFile), tokens);
            var content = ApplyTokens(File.ReadAllText(templateFile), tokens);
            var outPath = Path.Combine(fullTarget, relPath);
            Directory.CreateDirectory(Path.GetDirectoryName(outPath)!);
            File.WriteAllText(outPath, content, utf8NoBom);
            written.Add(relPath.Replace('\\', '/'));
        }

        var thumbnailPath = Path.Combine(fullTarget, "thumbnail.png");
        File.WriteAllBytes(thumbnailPath, Thumbnail.Render(displayTitle));
        written.Add("thumbnail.png");

        return written;
    }

    private static string ApplyTokens(string text, IReadOnlyDictionary<string, string> tokens)
    {
        var result = text;
        foreach (var (token, value) in tokens)
            result = result.Replace(token, value, StringComparison.Ordinal);

        var unreplaced = result.IndexOf("{{", StringComparison.Ordinal);
        if (unreplaced >= 0)
            throw new ToolException($"unreplaced placeholder in template near '{result[unreplaced..Math.Min(result.Length, unreplaced + 24)]}'");

        return result;
    }
}
