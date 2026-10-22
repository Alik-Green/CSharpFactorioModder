using System.Text;
using System.Text.RegularExpressions;

namespace Factorio.ModTool.Build;

/// <summary>
/// Post-compile verification of packaged Lua files: Lua 5.2 bytecode parser check (<c>luac5.2 -p</c>)
/// and scan for unshimmed <c>System.*</c> CoreSystem runtime calls.
/// </summary>
public static class LuaCheck
{
    /// <summary>One build error attributed to a generated or authored file and line.</summary>
    public sealed record Finding(string File, int Line, string Message);

    private static readonly HashSet<string> AllowedSystemMembers = new(StringComparer.Ordinal)
    {
        "namespace", "import", "init", "get", "toString", "as", "base",
    };

    private static readonly Regex SystemCall = new(
        @"\bSystem\.([A-Za-z_][A-Za-z0-9_]*)",
        RegexOptions.Compiled);

    private static readonly Regex LuacLine = new(
        @":(\d+):\s*(.+)$",
        RegexOptions.Compiled);

    /// <summary>Scans a Lua source string for references to unshimmed <c>System.*</c> members.</summary>
    public static IReadOnlyList<Finding> ScanSystemCalls(string file, string lua)
    {
        var stripped = StripCommentsAndStringsPreservingLines(lua.Replace("\r\n", "\n"));
        var lines = stripped.Split('\n');
        var findings = new List<Finding>();
        var seen = new HashSet<string>(StringComparer.Ordinal);

        for (var i = 0; i < lines.Length; i++)
        {
            foreach (Match match in SystemCall.Matches(lines[i]))
            {
                var member = match.Groups[1].Value;
                if (AllowedSystemMembers.Contains(member)) continue;
                if (!seen.Add($"{i + 1}:{member}")) continue;

                findings.Add(new Finding(
                    file,
                    i + 1,
                    $"System.{member} is not provided by the compat shim"));
            }
        }

        return findings;
    }

    /// <summary>Checks Lua 5.2 syntax of every <c>*.lua</c> file in <paramref name="modDirectory"/>.</summary>
    public static (bool Checked, IReadOnlyList<Finding> Findings) CheckSyntax(
        ToolchainSettings settings,
        string modDirectory)
    {
        var luac = Shell.Luac(settings);
        if (luac == null) return (false, Array.Empty<Finding>());

        var findings = new List<Finding>();
        foreach (var luaFile in Directory.EnumerateFiles(modDirectory, "*.lua", SearchOption.AllDirectories)
                     .OrderBy(p => p, StringComparer.Ordinal))
        {
            var run = Shell.Run(luac, new[] { "-p", luaFile });
            if (run.ExitCode == 0) continue;

            var err = (run.StdErr + "\n" + run.StdOut).Trim();
            var match = LuacLine.Match(err);
            var line = match.Success && int.TryParse(match.Groups[1].Value, out var n) ? n : 0;
            var message = match.Success ? match.Groups[2].Value.Trim() : err;
            findings.Add(new Finding(Path.GetRelativePath(modDirectory, luaFile), line, message));
        }

        return (true, findings);
    }

    private static string StripCommentsAndStringsPreservingLines(string source)
    {
        var sb = new StringBuilder(source.Length);
        var i = 0;
        var n = source.Length;
        while (i < n)
        {
            var c = source[i];
            if (c == '-' && i + 1 < n && source[i + 1] == '-')
            {
                while (i < n && source[i] != '\n') i++;
            }
            else if (c is '"' or '\'')
            {
                var quote = c;
                sb.Append(' ');
                i++;
                while (i < n)
                {
                    if (source[i] == '\\' && i + 1 < n)
                    {
                        if (source[i + 1] == '\n') sb.Append('\n');
                        i += 2;
                        continue;
                    }
                    if (source[i] == '\n') sb.Append('\n');
                    if (source[i] == quote) { i++; break; }
                    i++;
                }
            }
            else
            {
                sb.Append(c);
                i++;
            }
        }

        return sb.ToString();
    }
}
