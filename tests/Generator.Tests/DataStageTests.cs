using System.Text.Json;
using Factorio.ModTool.Build;
using Xunit;

namespace Factorio.Bindings.Tests;

/// <summary>
/// The checks that run over the data stage's output: the shape of nested tables, and the geometry
/// Factorio validates on load.
///
/// These are the failures the game reports only after a restart ("Failed to load mods: Error while
/// loading entity prototype ..."), so each case here is one Factorio would refuse to load. The
/// shapes are written as the harness writes them: Lua lists arrive as JSON arrays.
/// </summary>
public class DataStageTests
{
    /// <summary>
    /// The failure this class was written for: a 3x3 machine whose pipe connections sit on the 1.5
    /// tile edge, outside the 1.2 collision box the game shrinks to 1.199 before testing.
    /// </summary>
    [Fact]
    public void A_pipe_connection_outside_the_collision_box_is_reported()
    {
        var findings = Check(Machine(pipePosition: "[0, -1.5]"));

        var finding = Assert.Single(findings, f => f.Message.Contains("outside the entity bounding box"));
        Assert.Contains("fluid_boxes[1].pipe_connections[1].position", finding.Message);
        Assert.Contains("{0, -1.5}", finding.Message);
        Assert.Contains("{{-1.199, -1.199}, {1.199, 1.199}}", finding.Message);
        Assert.Contains("position must be inside of entity bounding box", finding.Message);
    }

    /// <summary>A connection one tile out from the centre is inside the box; this is the fix.</summary>
    [Fact]
    public void A_pipe_connection_inside_the_collision_box_is_accepted()
    {
        var findings = Check(Machine(pipePosition: "[0, -1]"));

        Assert.DoesNotContain(findings, f => f.Message.Contains("bounding box"));
    }

    /// <summary>
    /// Exactly on the edge is already outside, because the engine shrinks the box by 0.001 first.
    /// A checker that compared against 1.2 would pass a mod the game rejects.
    /// </summary>
    [Fact]
    public void A_pipe_connection_on_the_edge_is_outside()
    {
        var findings = Check(Machine(pipePosition: "[0, -1.2]"));

        Assert.Contains(findings, f => f.Message.Contains("outside the entity bounding box"));
    }

    /// <summary>Two connections on one position cannot both be drawn, and the game rejects them.</summary>
    [Fact]
    public void Two_connections_on_one_position_are_reported()
    {
        var findings = Check(Machine(pipePosition: "[0, -1]", secondPosition: "[0, -1]"));

        Assert.Contains(findings, f => f.Message.Contains("cannot share a position"));
    }

    /// <summary>The in/out fluid boxes use the same shape, under their own property names.</summary>
    [Fact]
    public void A_named_fluid_box_is_checked_too()
    {
        var findings = Check("""
            {
              "type": "boiler", "name": "b",
              "collision_box": [[-1.2, -1.2], [1.2, 1.2]],
              "output_fluid_box": {
                "volume": 100,
                "pipe_connections": [ { "position": [0, 1.5] } ]
              }
            }
            """);

        Assert.Contains(findings, f => f.Message.Contains("output_fluid_box.pipe_connections[1].position"));
    }

    /// <summary>A fluid box that holds nothing is a load error, not a harmless default.</summary>
    [Fact]
    public void A_fluid_box_needs_a_positive_volume()
    {
        var findings = Check(Machine(pipePosition: "[0, -1]", volume: "0"));

        Assert.Contains(findings, f => f.Message.Contains("volume is 0") && f.Message.Contains("greater than 0"));
    }

    /// <summary>
    /// The bounding box can also be written in the long form; the check has to read both, or a mod
    /// that spells it that way would slip through.
    /// </summary>
    [Fact]
    public void The_long_form_of_a_bounding_box_is_understood()
    {
        var findings = Check(Machine(pipePosition: "[0, -2]", collisionBox: """
            { "left_top": { "x": -1.2, "y": -1.2 }, "right_bottom": { "x": 1.2, "y": 1.2 } }
            """));

        Assert.Contains(findings, f => f.Message.Contains("outside the entity bounding box"));
    }

    /// <summary>An inside-out box would make every position outside; report it as its own problem.</summary>
    [Fact]
    public void An_inside_out_collision_box_is_reported()
    {
        var findings = Check(Machine(pipePosition: "[0, -1]", collisionBox: """
            [[1.2, 1.2], [-1.2, -1.2]]
            """));

        Assert.Contains(findings, f => f.Message.Contains("collision_box") && f.Message.Contains("inside out"));
    }

    /// <summary>A typo one level down used to be invisible: only the top level was checked.</summary>
    [Fact]
    public void A_misspelt_key_inside_a_fluid_box_is_reported()
    {
        var findings = Check(Machine(pipePosition: "[0, -1]", connectionKey: "flow_dir"));

        Assert.Contains(findings, f =>
            f.Message.Contains("fluid_boxes[1].pipe_connections[1].flow_dir") &&
            f.Message.Contains("is not a property here"));
    }

    /// <summary>
    /// A connection without a direction cannot be built, and the game says so; the only exception
    /// is a linked connection, which names its partner instead.
    /// </summary>
    [Fact]
    public void A_connection_without_a_direction_is_reported()
    {
        var findings = Check(Machine(pipePosition: "[0, -1]", extraConnectionKey: "\"flow_direction\": \"output\""));

        Assert.Contains(findings, f => f.Message.Contains("has no direction"));
    }

    /// <summary>A misspelt key inside a struct is reported even when the value is a list.</summary>
    [Fact]
    public void A_misspelt_key_inside_an_array_of_structs_is_reported()
    {
        var findings = Check(Machine(pipePosition: "[0, -1]", extraConnectionKey: """
            "max_underground_distnace": 10
            """));

        Assert.Contains(findings, f =>
            f.Message.Contains("max_underground_distnace") && f.Message.Contains("is not a property here"));
    }

    /// <summary>
    /// `type` tags select a branch of a union, and the selected branch's problems are the useful
    /// ones: "missing fuel_inventory_size", not "not one of the five energy sources".
    /// </summary>
    [Fact]
    public void A_tagged_union_reports_the_branch_the_type_selects()
    {
        var findings = Check("""
            { "type": "assembling-machine", "name": "m", "energy_source": { "type": "burner" } }
            """);

        Assert.Contains(findings, f => f.Message.Contains("energy_source.fuel_inventory_size"));
        Assert.DoesNotContain(findings, f => f.Message.Contains("expected (ElectricEnergySource"));
    }

    /// <summary>A key the API does not know is reported at the top level, as it always was.</summary>
    [Fact]
    public void A_misspelt_top_level_key_is_still_reported()
    {
        var findings = Check("""
            { "type": "item", "name": "thing", "stack_sizes": 20 }
            """);

        Assert.Contains(findings, f => f.Message.Contains("'stack_sizes' is not a property of ItemPrototype"));
    }

    /// <summary>
    /// Checks build a machine whose only interesting part is the pipe connection; the findings
    /// about missing required properties are expected and ignored by each assertion.
    /// </summary>
    private static string Machine(
        string pipePosition,
        string? secondPosition = null,
        string volume = "200",
        string collisionBox = "[[-1.2, -1.2], [1.2, 1.2]]",
        string connectionKey = "flow_direction",
        string extraConnectionKey = "\"direction\": 0")
    {
        var connections = secondPosition == null
            ? $$"""{ "{{connectionKey}}": "output", {{extraConnectionKey}}, "position": {{pipePosition}} }"""
            : $$"""
                { "{{connectionKey}}": "output", {{extraConnectionKey}}, "position": {{pipePosition}} },
                { "{{connectionKey}}": "output", {{extraConnectionKey}}, "position": {{secondPosition}} }
                """;

        return $$"""
            {
              "type": "assembling-machine",
              "name": "gas-extractor",
              "collision_box": {{collisionBox}},
              "fluid_boxes": [
                {
                  "production_type": "output",
                  "volume": {{volume}},
                  "pipe_connections": [ {{connections}} ]
                }
              ]
            }
            """;
    }

    // ------------------------------------------------------------ cross references

    /// <summary>
    /// The failure this round was written for: a recipe in a category no prototype declares.
    /// `gas-extraction` is not an enum the engine knows, it is a prototype, and the game stops with
    /// "recipe-category with name 'gas-extraction' does not exist".
    /// </summary>
    [Fact]
    public void An_undeclared_recipe_category_is_reported()
    {
        var findings = CheckMany(false, Machine(pipePosition: "[0, -1]"), """
            { "type": "recipe", "name": "gas-extractor-petroleum-gas", "category": "gas-extraction" }
            """);

        Assert.Contains(findings, f =>
            f.Message.Contains("'gas-extraction'") && f.Message.Contains("is not a recipe category"));
        Assert.Contains(findings, f => f.Message.Contains("recipe-category with name 'gas-extraction'"));
    }

    /// <summary>The fix: declare the category, and both the machine and the recipe may use it.</summary>
    [Fact]
    public void A_declared_recipe_category_is_accepted()
    {
        var findings = CheckMany(false,
            """{ "type": "recipe-category", "name": "gas-extraction" }""",
            Machine(pipePosition: "[0, -1]"),
            """{ "type": "recipe", "name": "r", "category": "gas-extraction" }""");

        Assert.DoesNotContain(findings, f => f.Message.Contains("is not a recipe category"));
    }

    /// <summary>Base-game categories need no declaration.</summary>
    [Fact]
    public void A_base_game_recipe_category_is_accepted()
    {
        var findings = CheckMany(false,
            """{ "type": "recipe", "name": "r", "category": "smelting" }""");

        Assert.DoesNotContain(findings, f => f.Message.Contains("is not a recipe category"));
    }

    /// <summary>
    /// 2.1 spells the same thing as a list, and the check has to read it there too.
    /// </summary>
    [Fact]
    public void The_categories_list_is_checked_as_well()
    {
        var findings = CheckMany(false,
            """{ "type": "recipe", "name": "r", "categories": ["gas-extraction"] }""");

        Assert.Contains(findings, f => f.Message.Contains("categories uses 'gas-extraction'"));
    }

    /// <summary>
    /// A mod that depends on another mod may use categories that mod declares; this check cannot
    /// see those, so it stays quiet rather than rejecting a mod that loads fine.
    /// </summary>
    [Fact]
    public void A_category_from_another_mod_is_allowed()
    {
        var findings = CheckMany(true,
            """{ "type": "recipe", "name": "r", "category": "some-other-mods-category" }""");

        Assert.DoesNotContain(findings, f => f.Message.Contains("is not a recipe category"));
    }

    /// <summary>Runs the data-stage validation over one prototype written as JSON.</summary>
    // ------------------------------------------------------------ reachability
    // The game accepts all of these silently: nothing fails to load, the content is just never
    // obtainable. A warning is the only thing that can tell a mod author their machine will not be
    // in the crafting menu.

    /// <summary>An item that places a machine, with no recipe making it, cannot be crafted.</summary>
    [Fact]
    public void A_machine_item_without_a_recipe_is_reported()
    {
        var warnings = Unreachable(
            """
            { "type": "item", "name": "gas-extractor", "stack_size": 20, "subgroup": "extraction-machine",
              "place_result": "gas-extractor" }
            """,
            """
            { "type": "assembling-machine", "name": "gas-extractor",
              "energy_usage": "150kW", "energy_source": { "type": "electric" } }
            """);

        var warning = Assert.Single(warnings);
        Assert.Contains("item 'gas-extractor'", warning);
        Assert.Contains("cannot be crafted", warning);
        Assert.Contains("never appears in the crafting menu", warning);
    }

    /// <summary>A recipe producing the item clears the warning - this is the fix.</summary>
    [Fact]
    public void A_machine_item_with_a_recipe_is_fine()
    {
        var warnings = Unreachable(
            """
            { "type": "item", "name": "gas-extractor", "stack_size": 20, "subgroup": "extraction-machine",
              "place_result": "gas-extractor" }
            """,
            """
            { "type": "recipe", "name": "gas-extractor", "enabled": true,
              "results": [ { "type": "item", "name": "gas-extractor", "amount": 1 } ] }
            """);

        Assert.Empty(warnings);
    }

    /// <summary>A plain item (no place_result) needs no recipe: it may come from anywhere.</summary>
    [Fact]
    public void An_item_that_places_nothing_is_not_reported()
    {
        Assert.Empty(Unreachable(
            """
            { "type": "item", "name": "polymer-pellet", "stack_size": 100, "subgroup": "intermediate-product" }
            """));
    }

    /// <summary>A disabled recipe nothing unlocks can never be crafted.</summary>
    [Fact]
    public void A_disabled_recipe_without_a_technology_is_reported()
    {
        var warnings = Unreachable(
            """
            { "type": "recipe", "name": "gas-extractor-2", "enabled": false,
              "results": [ { "type": "item", "name": "gas-extractor-2", "amount": 1 } ] }
            """);

        var warning = Assert.Single(warnings);
        Assert.Contains("recipe 'gas-extractor-2'", warning);
        Assert.Contains("no technology", warning);
    }

    /// <summary>A technology that unlocks it clears the warning, as does leaving it enabled.</summary>
    [Fact]
    public void A_disabled_recipe_a_technology_unlocks_is_fine()
    {
        Assert.Empty(Unreachable(
            """
            { "type": "recipe", "name": "gas-extractor-2", "enabled": false,
              "results": [ { "type": "item", "name": "gas-extractor-2", "amount": 1 } ] }
            """,
            """
            { "type": "technology", "name": "gas-extraction",
              "effects": [ { "type": "unlock-recipe", "recipe": "gas-extractor-2" } ] }
            """,
            """
            { "type": "item", "name": "gas-extractor-2", "stack_size": 20, "subgroup": "extraction-machine",
              "place_result": "gas-extractor-2" }
            """));
    }

    /// <summary>
    /// An item with no subgroup loads and is filed under the base game's "Other" tab, which is the
    /// complaint this check exists for.
    /// </summary>
    [Fact]
    public void An_item_without_a_subgroup_is_reported()
    {
        var warnings = Unreachable("""{ "type": "item", "name": "demo-pellet", "stack_size": 100 }""");

        var warning = Assert.Single(warnings);
        Assert.Contains("item 'demo-pellet'", warning);
        Assert.Contains("no subgroup", warning);
        Assert.Contains("Other", warning);
    }

    /// <summary>A subgroup silences it, and so does a subgroup this mod declares itself.</summary>
    [Fact]
    public void An_item_with_a_subgroup_or_in_a_declared_tab_is_fine()
    {
        Assert.Empty(Unreachable(
            """{ "type": "item", "name": "demo-pellet", "stack_size": 100, "subgroup": "intermediate-product" }"""));

        Assert.Empty(Unreachable(
            """{ "type": "item", "name": "demo-pellet", "stack_size": 100, "subgroup": "demo-machines" }""",
            """{ "type": "item-subgroup", "name": "demo-machines", "group": "production" }"""));
    }

    /// <summary>A subgroup name nobody declares is fatal in the game, so it is a finding.</summary>
    [Fact]
    public void An_unknown_subgroup_is_an_error()
    {
        var findings = CheckMany(false,
            """{ "type": "item", "name": "demo-pellet", "stack_size": 100, "subgroup": "demo-machines" }""");

        var finding = Assert.Single(findings, f => f.Message.Contains("subgroup"));
        Assert.Contains("item-subgroup with name 'demo-machines' does not exist", finding.Message);
    }

    /// <summary>A subgroup pointing at a group that does not exist is fatal too.</summary>
    [Fact]
    public void An_unknown_item_group_is_an_error()
    {
        var findings = CheckMany(false,
            """{ "type": "item-subgroup", "name": "demo-machines", "group": "demo-tabs" }""");

        Assert.Contains(findings, f => f.Message.Contains("group 'demo-tabs' is not an item-group"));
    }

    /// <summary>Runs the reachability pass over prototypes written as JSON.</summary>
    private static IReadOnlyList<string> Unreachable(params string[] prototypes)
    {
        var shapes = new List<DataStage.Shape>();
        foreach (var prototype in prototypes)
        {
            using var document = JsonDocument.Parse(prototype);
            var root = document.RootElement.Clone();
            shapes.Add(new DataStage.Shape(
                root.GetProperty("type").GetString()!,
                root.GetProperty("name").GetString()!,
                root.EnumerateObject().Select(p => p.Name).ToList(),
                root));
        }

        return DataStageRules.Unreachable(shapes).ToList();
    }

    private static IReadOnlyList<LuaCheck.Finding> Check(string prototype) => CheckMany(false, prototype);

    /// <summary>Runs the validation over several prototypes, as one data stage would register them.</summary>
    private static IReadOnlyList<LuaCheck.Finding> CheckMany(bool hasForeignDependencies, params string[] prototypes)
    {
        var shapes = new List<DataStage.Shape>();
        foreach (var prototype in prototypes)
        {
            using var document = JsonDocument.Parse(prototype);
            var root = document.RootElement;
            shapes.Add(new DataStage.Shape(
                root.GetProperty("type").GetString()!,
                root.GetProperty("name").GetString()!,
                root.EnumerateObject().Select(p => p.Name).ToList(),
                root.Clone()));
        }

        return DataStage.Validate(shapes, TestEnvironment.PrototypeJson, TestEnvironment.Version, hasForeignDependencies);
    }
}
