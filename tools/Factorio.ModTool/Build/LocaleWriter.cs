using System.Text;

namespace Factorio.ModTool.Build;

/// <summary>
/// Merges generated <c>section/key -&gt; text</c> entries into an existing Factorio <c>locale.cfg</c>,
/// preserving hand-authored comments, sections, and unrelated keys without duplicating entries.
/// </summary>
public static class LocaleWriter
{
    public static string Merge(string existing, IReadOnlyDictionary<string, string> entries)
    {
        if (entries.Count == 0) return existing;

        var bySection = new Dictionary<string, Dictionary<string, string>>(StringComparer.Ordinal);
        foreach (var (fullKey, value) in entries)
        {
            var slash = fullKey.IndexOf('/');
            if (slash <= 0 || slash == fullKey.Length - 1) continue;
            var section = fullKey[..slash];
            var key = fullKey[(slash + 1)..];
            if (!bySection.TryGetValue(section, out var map))
            {
                map = new Dictionary<string, string>(StringComparer.Ordinal);
                bySection[section] = map;
            }
            map[key] = value;
        }

        var written = new HashSet<string>(StringComparer.Ordinal);
        var output = new List<string>();
        var currentSection = "";

        void FlushRemainingInCurrentSection()
        {
            if (currentSection.Length == 0 || !bySection.TryGetValue(currentSection, out var sectionEntries))
                return;

            foreach (var (key, value) in sectionEntries.OrderBy(p => p.Key, StringComparer.Ordinal))
            {
                if (written.Add($"{currentSection}/{key}"))
                    output.Add($"{key}={value}");
            }
        }

        var rawLines = string.IsNullOrEmpty(existing)
            ? Array.Empty<string>()
            : existing.Replace("\r\n", "\n").Split('\n');

        foreach (var line in rawLines)
        {
            var trimmed = line.Trim();
            if (trimmed.StartsWith('[') && trimmed.EndsWith(']'))
            {
                FlushRemainingInCurrentSection();
                currentSection = trimmed[1..^1].Trim();
                output.Add(line);
                continue;
            }

            if (currentSection.Length > 0 && !trimmed.StartsWith(';') && !trimmed.StartsWith('#'))
            {
                var eq = line.IndexOf('=');
                if (eq > 0)
                {
                    var key = line[..eq].Trim();
                    if (bySection.TryGetValue(currentSection, out var sectionEntries) &&
                        sectionEntries.TryGetValue(key, out var replacement))
                    {
                        written.Add($"{currentSection}/{key}");
                        output.Add($"{key}={replacement}");
                        continue;
                    }
                }
            }

            output.Add(line);
        }

        FlushRemainingInCurrentSection();

        foreach (var (section, sectionEntries) in bySection.OrderBy(p => p.Key, StringComparer.Ordinal))
        {
            var remaining = sectionEntries
                .Where(p => !written.Contains($"{section}/{p.Key}"))
                .OrderBy(p => p.Key, StringComparer.Ordinal)
                .ToList();
            if (remaining.Count == 0) continue;

            if (output.Count > 0 && output[^1].Trim().Length > 0)
                output.Add("");

            output.Add($"[{section}]");
            foreach (var (key, value) in remaining)
                output.Add($"{key}={value}");
        }

        var sb = new StringBuilder();
        for (var i = 0; i < output.Count; i++)
        {
            if (i == output.Count - 1 && output[i].Length == 0) break;
            sb.AppendLine(output[i]);
        }

        return sb.ToString();
    }
}
