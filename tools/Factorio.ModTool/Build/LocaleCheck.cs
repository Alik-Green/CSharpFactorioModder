using System.Text.Json;

namespace Factorio.ModTool.Build;

/// <summary>
/// Checks that visible prototypes declare a display name in <c>locale/en/*.cfg</c>, preventing
/// <c>Unknown key: item-name.&lt;name&gt;</c> strings in game.
/// </summary>
public static class LocaleCheck
{
    private static readonly HashSet<string> ItemPrototypeTypes = new(StringComparer.Ordinal)
    {
        "item", "tool", "ammo", "capsule", "armor", "gun", "module", "rail-planner",
        "repair-tool", "item-with-entity-data", "item-with-label", "item-with-inventory",
        "selection-tool", "spidertron-remote",
    };

    private static readonly HashSet<string> EntityPrototypeTypes = new(StringComparer.Ordinal)
    {
        "assembling-machine", "furnace", "mining-drill", "boiler", "generator", "solar-panel",
        "accumulator", "reactor", "heat-pipe", "inserter", "container", "logistic-container",
        "storage-tank", "pump", "pipe", "pipe-to-ground", "transport-belt", "underground-belt",
        "splitter", "loader", "loader-1x1", "lab", "radar", "beacon", "roboport", "electric-pole",
        "wall", "gate", "ammo-turret", "electric-turret", "fluid-turret", "artillery-turret",
        "car", "spider-vehicle", "locomotive", "cargo-wagon", "fluid-wagon", "rocket-silo",
    };

    /// <summary>Maps a Factorio prototype <c>type</c> to its <c>*-name</c> locale section, or null if unnamed.</summary>
    public static string? NameSection(string prototypeType)
    {
        if (ItemPrototypeTypes.Contains(prototypeType)) return "item-name";
        if (EntityPrototypeTypes.Contains(prototypeType)) return "entity-name";
        return prototypeType switch
        {
            "fluid" => "fluid-name",
            "recipe" => "recipe-name",
            "technology" => "technology-name",
            "item-group" => "item-group-name",
            "equipment-grid" => "equipment-name",
            _ => null,
        };
    }

    /// <summary>Maps a Factorio prototype <c>type</c> to its <c>*-description</c> locale section, or null.</summary>
    public static string? DescriptionSection(string prototypeType)
    {
        var nameSection = NameSection(prototypeType);
        return nameSection?.Replace("-name", "-description", StringComparison.Ordinal);
    }

    /// <summary>Returns warnings for every non-hidden prototype missing its English locale entry.</summary>
    public static IReadOnlyList<string> Names(
        string modDirectory,
        IEnumerable<DataStage.Shape> prototypes,
        Func<string, string?>? sectionForType = null)
    {
        sectionForType ??= NameSection;
        var entries = ReadEnglishLocale(modDirectory);
        var warnings = new List<string>();

        foreach (var proto in prototypes)
        {
            if (proto.Element is { ValueKind: JsonValueKind.Object } el &&
                el.TryGetProperty("hidden", out var hiddenEl) &&
                hiddenEl.ValueKind == JsonValueKind.True)
            {
                continue;
            }

            var section = sectionForType(proto.Type);
            if (section == null) continue;
            if (entries.Contains($"{section}/{proto.Name}")) continue;

            warnings.Add(
                $"{proto.Type} '{proto.Name}' has no entry under [{section}] in locale/en/locale.cfg " +
                $"(Factorio shows \"Unknown key: {section}.{proto.Name}\")");
        }

        return warnings;
    }

    private static HashSet<string> ReadEnglishLocale(string modDirectory)
    {
        var found = new HashSet<string>(StringComparer.Ordinal);
        var enDir = Path.Combine(modDirectory, "locale", "en");
        if (!Directory.Exists(enDir)) return found;

        foreach (var cfg in Directory.EnumerateFiles(enDir, "*.cfg", SearchOption.TopDirectoryOnly))
        {
            var currentSection = "";
            foreach (var rawLine in File.ReadAllLines(cfg))
            {
                var trimmed = rawLine.Trim();
                if (trimmed.Length == 0 || trimmed.StartsWith(';') || trimmed.StartsWith('#'))
                    continue;

                if (trimmed.StartsWith('[') && trimmed.EndsWith(']'))
                {
                    currentSection = trimmed[1..^1].Trim();
                    continue;
                }

                var eq = rawLine.IndexOf('=');
                if (eq <= 0 || currentSection.Length == 0) continue;
                var key = rawLine[..eq].Trim();
                found.Add($"{currentSection}/{key}");
            }
        }

        return found;
    }
}
