using System.Text.Json;
using System.Text.RegularExpressions;

namespace Factorio.ModTool.Build;

/// <summary>
/// Ensures every prototype icon, sprite <c>filename</c>, or sound path carries Factorio's mandatory
/// <c>__source__/path</c> prefix.
/// </summary>
public static class DataStageResources
{
    private static readonly Regex PrefixedPath = new(
        @"^__[^/]+__/.+",
        RegexOptions.Compiled);

    private static readonly HashSet<string> ResourcePropertyNames = new(StringComparer.Ordinal)
    {
        "icon",
        "dark_background_icon",
        "starmap_icon",
        "filename",
    };

    public static void CheckPrototype(
        DataStage.Shape shape,
        JsonElement root,
        string? modName,
        List<LuaCheck.Finding> findings)
    {
        WalkObject(shape, root, prefix: "", modName, findings);
    }

    private static void WalkObject(
        DataStage.Shape shape,
        JsonElement obj,
        string prefix,
        string? modName,
        List<LuaCheck.Finding> findings)
    {
        foreach (var prop in obj.EnumerateObject())
        {
            var path = prefix.Length == 0 ? prop.Name : $"{prefix}.{prop.Name}";
            if (prop.Value.ValueKind == JsonValueKind.String)
            {
                if (ResourcePropertyNames.Contains(prop.Name))
                    CheckString(shape, path, prop.Value.GetString()!, modName, findings);
            }
            else if (prop.Value.ValueKind == JsonValueKind.Object)
            {
                WalkObject(shape, prop.Value, path, modName, findings);
            }
            else if (prop.Value.ValueKind == JsonValueKind.Array)
            {
                WalkArray(shape, prop.Value, path, prop.Name == "filenames", modName, findings);
            }
        }
    }

    private static void WalkArray(
        DataStage.Shape shape,
        JsonElement arr,
        string path,
        bool elementsAreFilenames,
        string? modName,
        List<LuaCheck.Finding> findings)
    {
        var index = 0;
        foreach (var element in arr.EnumerateArray())
        {
            var elemPath = $"{path}[{index}]";
            if (elementsAreFilenames && element.ValueKind == JsonValueKind.String)
                CheckString(shape, elemPath, element.GetString()!, modName, findings);
            else if (element.ValueKind == JsonValueKind.Object)
                WalkObject(shape, element, elemPath, modName, findings);
            else if (element.ValueKind == JsonValueKind.Array)
                WalkArray(shape, element, elemPath, false, modName, findings);

            index++;
        }
    }

    private static void CheckString(
        DataStage.Shape shape,
        string path,
        string value,
        string? modName,
        List<LuaCheck.Finding> findings)
    {
        if (string.IsNullOrEmpty(value) || PrefixedPath.IsMatch(value)) return;

        var example = string.IsNullOrEmpty(modName) ? $"__<mod>__/{value}" : $"__{modName}__/{value}";
        findings.Add(new LuaCheck.Finding(
            "data.lua",
            0,
            $"{shape.Type} {shape.Name}: {path} is '{value}', which has no source prefix. " +
            $"Factorio stops with \"Path {value} not matching the resource path pattern: __source__/path\". " +
            $"Write Asset.Mod(\"{value}\") for a file in this mod (it becomes '{example}') or " +
            $"Asset.Base(\"{value}\") for a base-game one"));
    }
}
