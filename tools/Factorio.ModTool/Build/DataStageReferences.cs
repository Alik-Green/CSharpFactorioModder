using System.Text.Json;

namespace Factorio.ModTool.Build;

/// <summary>
/// Cross-prototype ID resolution checks (<c>recipe-category</c>, <c>item-group</c>, <c>item-subgroup</c>).
/// Catches <c>Error in assignID: ... with name '...' does not exist</c> before launching Factorio.
/// </summary>
public static class DataStageReferences
{
    private static readonly HashSet<string> ItemPrototypeTypes = new(StringComparer.Ordinal)
    {
        "item", "item-with-entity-data", "tool", "ammo", "capsule", "gun", "armor", "module", "rail-planner",
    };

    public static void Check(
        IReadOnlyList<DataStage.Shape> shapes,
        bool hasForeignDependencies,
        List<LuaCheck.Finding> findings)
    {
        if (hasForeignDependencies) return;

        var recipeCategories = new HashSet<string>(VanillaGroups.RecipeCategories, StringComparer.Ordinal);
        var groups = new HashSet<string>(VanillaGroups.Groups, StringComparer.Ordinal);
        var subgroups = new HashSet<string>(VanillaGroups.Subgroups.Keys, StringComparer.Ordinal);
        var modItems = new HashSet<string>(StringComparer.Ordinal);

        foreach (var shape in shapes)
        {
            if (ItemPrototypeTypes.Contains(shape.Type))
                modItems.Add(shape.Name);

            switch (shape.Type)
            {
                case "recipe-category":
                    recipeCategories.Add(shape.Name);
                    break;
                case "item-group":
                    groups.Add(shape.Name);
                    break;
                case "item-subgroup":
                    subgroups.Add(shape.Name);
                    break;
            }
        }

        foreach (var shape in shapes)
        {
            if (shape.Element is not { ValueKind: JsonValueKind.Object } root) continue;

            // 1. Recipe category references on recipes and machines
            if (shape.Type == "recipe")
            {
                if (root.TryGetProperty("category", out var catEl) && catEl.ValueKind == JsonValueKind.String)
                {
                    var cat = catEl.GetString()!;
                    if (!recipeCategories.Contains(cat))
                    {
                        findings.Add(new LuaCheck.Finding(
                            "data.lua",
                            0,
                            $"recipe {shape.Name}: category '{cat}' is not a recipe category " +
                            $"(Factorio stops with \"Error in assignID: recipe-category with name '{cat}' does not exist\")"));
                    }
                }

                CheckCategoryArray(shape, root, "categories", recipeCategories, findings);
                CheckCategoryArray(shape, root, "additional_categories", recipeCategories, findings);
            }

            CheckCategoryArray(shape, root, "crafting_categories", recipeCategories, findings);

            // 2. Item-group reference on item-subgroup
            if (shape.Type == "item-subgroup" &&
                root.TryGetProperty("group", out var groupEl) &&
                groupEl.ValueKind == JsonValueKind.String)
            {
                var grp = groupEl.GetString()!;
                if (!groups.Contains(grp))
                {
                    findings.Add(new LuaCheck.Finding(
                        "data.lua",
                        0,
                        $"item-subgroup {shape.Name}: group '{grp}' is not an item-group " +
                        $"(Factorio stops with \"Error in assignID: item-group with name '{grp}' does not exist\")"));
                }
            }

            // 3. Item-subgroup reference on any prototype
            if (root.TryGetProperty("subgroup", out var subEl) && subEl.ValueKind == JsonValueKind.String)
            {
                var sub = subEl.GetString()!;
                if (!subgroups.Contains(sub))
                {
                    findings.Add(new LuaCheck.Finding(
                        "data.lua",
                        0,
                        $"{shape.Type} {shape.Name}: subgroup '{sub}' is not an item-subgroup " +
                        $"(Factorio stops with \"Error in assignID: item-subgroup with name '{sub}' does not exist\")"));
                }
            }

            // 4. Custom entity setting minable.result = "<own-name>" without adding its ItemDefinition
            if (root.TryGetProperty("minable", out var minableEl) &&
                minableEl.ValueKind == JsonValueKind.Object &&
                minableEl.TryGetProperty("result", out var minableResEl) &&
                minableResEl.ValueKind == JsonValueKind.String)
            {
                var itemName = minableResEl.GetString()!;
                if (string.Equals(itemName, shape.Name, StringComparison.Ordinal) &&
                    !modItems.Contains(itemName))
                {
                    findings.Add(new LuaCheck.Finding(
                        "data.lua",
                        0,
                        $"{shape.Type} {shape.Name}: minable.result names item '{itemName}', which is not defined " +
                        $"(Factorio stops with \"Error in assignID: item with name '{itemName}' does not exist\"; " +
                        $"define an ItemDefinition with PlaceResult = \"{shape.Name}\" and register it via mod.Add(...))"));
                }
            }
        }
    }

    private static void CheckCategoryArray(
        DataStage.Shape shape,
        JsonElement root,
        string propertyName,
        HashSet<string> validCategories,
        List<LuaCheck.Finding> findings)
    {
        if (!root.TryGetProperty(propertyName, out var arr) || arr.ValueKind != JsonValueKind.Array)
            return;

        foreach (var item in arr.EnumerateArray())
        {
            if (item.ValueKind != JsonValueKind.String) continue;
            var cat = item.GetString()!;
            if (validCategories.Contains(cat)) continue;

            findings.Add(new LuaCheck.Finding(
                "data.lua",
                0,
                $"{shape.Type} {shape.Name}: {propertyName} uses '{cat}', which is not a recipe category " +
                $"(Factorio stops with \"Error in assignID: recipe-category with name '{cat}' does not exist\")"));
        }
    }
}
