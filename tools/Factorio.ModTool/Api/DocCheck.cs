using System.Text.RegularExpressions;

namespace Factorio.ModTool.Api;

/// <summary>
/// Enforces the "every public member is documented" rule on the generated C# itself, so a gap in
/// `Docs` shows up as a build failure instead of a missing comment somewhere in 1,500 files.
/// A member counts as documented when the nearest non-blank line above it is an XML doc comment.
/// </summary>
public static class DocCheck
{
    public sealed record Finding(string File, int Line, string Member);

    private static readonly Regex PublicMember = new(
        @"^\s*public\s+(?!class\b|static class\b|partial class\b|sealed class\b|enum\b|struct\b|interface\b|const\b)[^;{=]*?\b(?<name>[A-Za-z_][A-Za-z0-9_]*)\s*(?:\(|\{|=|;)",
        RegexOptions.Compiled);

    private static readonly Regex PublicType = new(
        @"^\s*public\s+(?:static\s+|partial\s+|abstract\s+|sealed\s+)*(?:class|struct|enum|interface)\s+(?<name>[A-Za-z_][A-Za-z0-9_]*)",
        RegexOptions.Compiled);

    public static IReadOnlyList<Finding> Check(IEnumerable<string> files)
    {
        var findings = new List<Finding>();
        foreach (var file in files)
        {
            var lines = File.ReadAllLines(file);
            for (int i = 0; i < lines.Length; i++)
            {
                var type = PublicType.Match(lines[i]);
                var member = type.Success ? type : PublicMember.Match(lines[i]);
                if (!member.Success) continue;
                if (lines[i].TrimStart().StartsWith("//")) continue;
                if (!HasSummaryAbove(lines, i))
                    findings.Add(new Finding(file, i + 1, member.Groups["name"].Value));
            }
        }
        return findings;
    }

    private static bool HasSummaryAbove(string[] lines, int index)
    {
        for (int j = index - 1; j >= 0; j--)
        {
            var t = lines[j].Trim();
            if (t.Length == 0) continue;
            return t.StartsWith("///", StringComparison.Ordinal);
        }
        return false;
    }
}
