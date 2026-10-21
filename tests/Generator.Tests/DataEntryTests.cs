using Factorio.ModTool;
using Factorio.ModTool.Build;
using Factorio.ModTool.Data;
using Xunit;

namespace Factorio.Bindings.Tests;

/// <summary>
/// The data stage written in C#: the tool compiles the definitions, runs them and writes the Lua
/// that ships. These tests drive <see cref="DataEntryGenerator"/> directly, so they are fast and do
/// not depend on CSharp.lua or Lua being installed.
/// </summary>
public class DataEntryTests : IDisposable
{
    private readonly string _directory = Path.Combine(Path.GetTempPath(), "factorio-data-" + Guid.NewGuid().ToString("N"));

    public DataEntryTests() => Directory.CreateDirectory(_directory);

    public void Dispose()
    {
        try { Directory.Delete(_directory, true); } catch { /* best effort */ }
    }

    /// <summary>Writes a data-stage source file and returns its path.</summary>
    private string Source(string name, string text)
    {
        var path = Path.Combine(_directory, name);
        Directory.CreateDirectory(Path.GetDirectoryName(path)!);
        File.WriteAllText(path, text);
        return path;
    }

    private static readonly string Demo = """
        using Factorio.Data;

        namespace Demo
        {
            public sealed class ExampleMod : ModDefinition
            {
                public override void Define(ModBuilder mod)
                {
                    mod.Add(new Coolant());
                    mod.Add(new Pellet());
                    mod.Add(new Machine());
                    mod.Add(new MachineItem());
                    mod.Add(new PolymerRecipe());
                }
            }

            public sealed class Coolant : FluidDefinition
            {
                public Coolant() : base("demo-coolant")
                {
                    Icon = Asset.Base("graphics/icons/fluid/water.png");
                    IconSize = 64;
                    DefaultTemperature = 25;
                    MaxTemperature = 100;
                    BaseColor = new Rgb(0.08, 0.40, 0.85);
                    FlowColor = new Rgb(0.15, 0.70, 1.00);
                }
            }

            public sealed class Pellet : ItemDefinition
            {
                public Pellet() : base("demo-polymer-pellet")
                {
                    Icon = Asset.Base("graphics/icons/iron-gear-wheel.png");
                    IconSize = 64;
                    StackSize = 100;
                    Subgroup = Subgroup.IntermediateProduct;
                    Order = SortOrder.At("z", "demo-polymer-pellet");
                }
            }

            public sealed class MachineItem : ItemDefinition
            {
                public MachineItem() : base("demo-chemical-assembler")
                {
                    StackSize = 50;
                    Subgroup = Subgroup.ProductionMachine;
                    PlaceResult = EntityRef.Of("demo-chemical-assembler");
                }
            }

            public sealed class Machine : AssemblingMachineDefinition
            {
                public Machine() : base("demo-chemical-assembler", Vanilla.AssemblingMachine1)
                {
                    CraftingCategories = [CraftingCategory.Chemistry];
                    CraftingSpeed = 1.25;
                    EnergyUsage = Energy.Kilowatts(250);
                    MinableResult = ItemRef.Of("demo-chemical-assembler");
                    NextUpgrade = null;
                    FluidBoxes =
                    [
                        FluidBox.Input(filter: FluidRef.Of("demo-coolant"), volume: 100,
                                       ports: [PipePort.North(0, -1)])
                    ];
                }
            }

            public sealed class PolymerRecipe : RecipeDefinition
            {
                public PolymerRecipe() : base("demo-polymer-pellet")
                {
                    Categories = [CraftingCategory.Chemistry];
                    Enabled = true;
                    EnergyRequired = 2;
                    Ingredients = [Ingredient.Item("iron-plate", 2), Ingredient.Fluid("demo-coolant", 25)];
                    Results = [Product.Item("demo-polymer-pellet", 1)];
                }
            }
        }
        """;

    [Fact]
    public void Definitions_are_written_as_a_prototype_table()
    {
        var result = DataEntryGenerator.Generate("Demo.ExampleMod", new[] { Source("data/Demo.cs", Demo) }, "2.0");

        Assert.Equal(5, result.Prototypes.Count);
        var lua = result.Lua;

        Assert.Contains("local prototypes =", lua);
        Assert.Contains("data:extend(prototypes)", lua);
        Assert.Contains("type = \"fluid\"", lua);
        Assert.Contains("name = \"demo-coolant\"", lua);
        Assert.Contains("default_temperature = 25", lua);
        Assert.Contains("icon = \"__base__/graphics/icons/fluid/water.png\"", lua);
        Assert.Contains("stack_size = 100", lua);
        Assert.Contains("place_result = \"demo-chemical-assembler\"", lua);
        Assert.Contains("crafting_categories = {", lua);
        Assert.Contains("crafting_speed = 1.25", lua);
        Assert.Contains("energy_usage = \"250kW\"", lua);
        Assert.Contains("direction = defines.direction.north", lua);
        Assert.Contains("filter = \"demo-coolant\"", lua);
        Assert.Contains("amount = 25", lua);
    }

    [Fact]
    public void Recipe_categories_are_spelled_the_way_the_target_version_reads_them()
    {
        var sources = new[] { Source("data/Demo.cs", Demo) };

        // 2.0: one category, plus additional_categories for the rest.
        var v20 = DataEntryGenerator.Generate("Demo.ExampleMod", sources, "2.0").Lua;
        Assert.Contains("category = \"chemistry\"", v20);
        Assert.DoesNotContain("\n    categories =", v20);

        // 2.1: a list.
        var v21 = DataEntryGenerator.Generate("Demo.ExampleMod", sources, "2.1").Lua;
        Assert.Contains("categories = {", v21);
        Assert.DoesNotContain("\n    category = ", v21);
    }

    [Fact]
    public void Inheriting_a_vanilla_prototype_is_recorded_and_reported()
    {
        var result = DataEntryGenerator.Generate("Demo.ExampleMod", new[] { Source("data/Demo.cs", Demo) }, "2.0");

        Assert.Contains("__inherit = {", result.Lua);
        Assert.Contains("name = \"assembling-machine-1\"", result.Lua);
        Assert.Contains("-- Prototypes built on a vanilla one", result.Lua);
        Assert.Contains("data.raw[inherit.type]", result.Lua);

        // The build needs to know, so the data-stage check does not call the inherited required
        // fields missing.
        Assert.Contains("assembling-machine/demo-chemical-assembler", result.Inherited);
    }

    [Fact]
    public void A_field_cleared_with_null_is_not_inherited()
    {
        var lua = DataEntryGenerator.Generate("Demo.ExampleMod", new[] { Source("data/Demo.cs", Demo) }, "2.0").Lua;
        Assert.Contains("__nil = {", lua);
        Assert.Contains("\"next_upgrade\"", lua);
    }

    [Fact]
    public void A_partial_minable_table_gets_the_mining_time_the_game_requires()
    {
        var lua = DataEntryGenerator.Generate("Demo.ExampleMod", new[] { Source("data/Demo.cs", Demo) }, "2.0").Lua;
        var minable = lua[lua.IndexOf("minable = {", StringComparison.Ordinal)..];
        minable = minable[..minable.IndexOf('}')];
        Assert.Contains("result = \"demo-chemical-assembler\"", minable);
        Assert.Contains("mining_time = 0.5", minable);
    }

    [Fact]
    public void Data_sources_may_use_the_whole_language()
    {
        // The data stage runs in the build, not in Factorio, so LINQ, loops and helpers are fine.
        var source = Source("data/Linq.cs", """
            using System.Linq;
            using Factorio.Data;

            namespace Demo
            {
                public sealed class ExampleMod : ModDefinition
                {
                    public override void Define(ModBuilder mod)
                    {
                        mod.Add(new Recipe());
                    }
                }

                public sealed class Recipe : RecipeDefinition
                {
                    public Recipe() : base("demo-linq")
                    {
                        Categories = ["crafting"];
                        Enabled = true;
                        EnergyRequired = 1;

                        var names = new[] { "iron-plate", "copper-plate", "steel-plate" };
                        Ingredients = Values.From(names.Select(n => (object?)Ingredient.Item(n, 1)));
                        Results = [Product.Item("demo-linq", names.Length)];
                    }
                }
            }
            """);

        var lua = DataEntryGenerator.Generate("Demo.ExampleMod", new[] { source }, "2.0").Lua;
        Assert.Contains("name = \"iron-plate\"", lua);
        Assert.Contains("name = \"copper-plate\"", lua);
        Assert.Contains("name = \"steel-plate\"", lua);
        Assert.Contains("amount = 3", lua);
    }

    [Fact]
    public void A_missing_entry_point_is_reported_with_the_classes_that_do_exist()
    {
        var error = Assert.Throws<ToolException>(() =>
            DataEntryGenerator.Generate("Demo.Typo", new[] { Source("data/Demo.cs", Demo) }, "2.0"));
        Assert.Contains("Demo.Typo", error.Message);
        Assert.Contains("Demo.ExampleMod", error.Message);
    }

    [Fact]
    public void A_definition_that_throws_is_reported()
    {
        var source = Source("data/Bad.cs", """
            using System;
            using Factorio.Data;

            namespace Demo
            {
                public sealed class ExampleMod : ModDefinition
                {
                    public override void Define(ModBuilder mod) => throw new InvalidOperationException("boom");
                }
            }
            """);

        var error = Assert.Throws<ToolException>(() => DataEntryGenerator.Generate("Demo.ExampleMod", new[] { source }, "2.0"));
        Assert.Contains("boom", error.Message);
    }

    [Fact]
    public void A_compile_error_names_the_file()
    {
        var source = Source("data/Broken.cs", """
            using Factorio.Data;
            namespace Demo
            {
                public sealed class ExampleMod : ModDefinition
                {
                    public override void Define(ModBuilder mod) { notCsharp(); }
                }
            }
            """);

        var error = Assert.Throws<ToolException>(() => DataEntryGenerator.Generate("Demo.ExampleMod", new[] { source }, "2.0"));
        Assert.Contains("Broken.cs", error.Message);
    }

    [Fact]
    public void An_empty_definition_list_is_an_error()
    {
        var source = Source("data/Empty.cs", """
            using Factorio.Data;
            namespace Demo
            {
                public sealed class ExampleMod : ModDefinition
                {
                    public override void Define(ModBuilder mod) { }
                }
            }
            """);

        var error = Assert.Throws<ToolException>(() => DataEntryGenerator.Generate("Demo.ExampleMod", new[] { source }, "2.0"));
        Assert.Contains("no prototypes", error.Message);
    }

    [Fact]
    public void A_data_source_inside_a_runtime_source_directory_is_rejected()
    {
        Directory.CreateDirectory(Path.Combine(_directory, "src"));
        File.WriteAllText(Path.Combine(_directory, "src", "Mixed.cs"), "class Mixed { }");
        File.WriteAllText(Path.Combine(_directory, "mod.json"), """
            {
              "name": "Mixed",
              "version": "0.1.0",
              "entry": "Mixed.Main.Init",
              "dataEntry": "Mixed.ExampleMod",
              "sources": ["src"],
              "dataSources": ["src"]
            }
            """);

        var error = Assert.Throws<ToolException>(() => ModProject.Load(_directory));
        Assert.Contains("data-stage source", error.Message);
    }
}
