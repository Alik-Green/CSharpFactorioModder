using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Factorio.Data;
using Factorio.ModTool;
using Factorio.ModTool.Build;
using Factorio.ModTool.Data;
using Xunit;

namespace Factorio.ModTool.Tests;

public sealed class VanillaCatalogAndQoLTests
{
    private static readonly ToolchainSettings Settings = ToolchainSettings.Locate(FindRoot());

    private static string FindRoot()
    {
        var dir = new DirectoryInfo(AppContext.BaseDirectory);
        while (dir != null && !File.Exists(Path.Combine(dir.FullName, "config", "toolchain.json")))
            dir = dir.Parent;
        return dir?.FullName ?? throw new InvalidOperationException("Repository root not found.");
    }

    [Fact]
    public void VanillaCatalog_CoversFullFactorio2ItemsFluidsMachinesEntitiesRecipesTechnologiesAndSpaceAge()
    {
        // Classic syntax explicitly requested by user:
        var classic = Ingredient.Item(Vanilla.Items.ElectronicCircuit, 5);
        // Ultra-clean operator syntax:
        var shorthand = Vanilla.Items.ElectronicCircuit * 5;

        Assert.Equal("electronic-circuit", Vanilla.Items.ElectronicCircuit.Name);
        Assert.Equal("electronic-circuit", Vanilla.Items.GreenCircuit.Name);
        Assert.Equal("advanced-circuit", Vanilla.Items.RedCircuit.Name);
        Assert.Equal("processing-unit", Vanilla.Items.BlueCircuit.Name);
        Assert.Equal("barrel", Vanilla.Items.Barrel.Name);
        Assert.Equal("barrel", Vanilla.Items.EmptyBarrel.Name);

        Assert.Equal("water", Vanilla.Fluids.Water.Name);
        Assert.Equal("petroleum-gas", Vanilla.Fluids.PetroleumGas.Name);
        Assert.Equal("sulfuric-acid", Vanilla.Fluids.SulfuricAcid.Name);

        Assert.Equal("assembling-machine", Vanilla.Machines.ChemicalPlant.Type);
        Assert.Equal("chemical-plant", Vanilla.Machines.ChemicalPlant.Name);
        Assert.Equal("storage-tank", Vanilla.Machines.StorageTank.Type);
        Assert.Equal("electric-furnace", Vanilla.Machines.ElectricFurnace.Name);

        Assert.Equal("resource", Vanilla.Entities.IronOre.Type);
        Assert.Equal("iron-ore", Vanilla.Entities.IronOre.Name);
        Assert.Equal("corpse", Vanilla.Entities.BigRemnants.Type);

        Assert.Equal("advanced-oil-processing", Vanilla.Recipes.AdvancedOilProcessing.Name);
        Assert.Equal("fluid-handling", Vanilla.Technologies.FluidHandling.Name);

        // Space Age DLC prototypes also strongly typed:
        Assert.Equal("foundry", Vanilla.SpaceAge.Machines.Foundry.Name);
        Assert.Equal("electromagnetic-plant", Vanilla.SpaceAge.Machines.ElectromagneticPlant.Name);
        Assert.Equal("tungsten-plate", Vanilla.SpaceAge.Items.TungstenPlate.Name);
        Assert.Equal("fluoroketone-cold", Vanilla.SpaceAge.Fluids.FluoroketoneCold.Name);

        // Verify massive catalog coverage across every category:
        Assert.True(typeof(Vanilla.Items).GetProperties(BindingFlags.Public | BindingFlags.Static).Length >= 250);
        Assert.True(typeof(Vanilla.Fluids).GetProperties(BindingFlags.Public | BindingFlags.Static).Length >= 8);
        Assert.True(typeof(Vanilla.Machines).GetProperties(BindingFlags.Public | BindingFlags.Static).Length >= 75);
        Assert.True(typeof(Vanilla.Entities).GetProperties(BindingFlags.Public | BindingFlags.Static).Length >= 450);
        Assert.True(typeof(Vanilla.Recipes).GetProperties(BindingFlags.Public | BindingFlags.Static).Length >= 190);
        Assert.True(typeof(Vanilla.Technologies).GetProperties(BindingFlags.Public | BindingFlags.Static).Length >= 180);

        // Verify classic and operator shorthand emit byte-for-byte identical Lua:
        var w1 = new LuaWriter("2.0");
        w1.Value(classic);
        var w2 = new LuaWriter("2.0");
        w2.Value(shorthand);
        Assert.Equal(w1.ToString(), w2.ToString());
        Assert.Contains("\"electronic-circuit\"", w1.ToString());
    }

    [Fact]
    public void EndToEnd_CraftableCompositeIconsSettingsAndNewEntities_PassFullValidationZeroWarnings()
    {
        var tempRoot = Path.Combine(Path.GetTempPath(), "FactorioQoLTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempRoot);
        try
        {
            Directory.CreateDirectory(Path.Combine(tempRoot, "src"));
            Directory.CreateDirectory(Path.Combine(tempRoot, "data"));

            File.WriteAllText(Path.Combine(tempRoot, "mod.json"), """
                {
                  "name": "GreenMod",
                  "version": "1.0.4",
                  "title": "Green Mod QoL Suite",
                  "author": "Alik",
                  "description": "End-to-end test of Vanilla.*, Craftable(), composite icons, settings, and furnaces/tanks.",
                  "factorioVersion": "2.0",
                  "dependencies": ["base >= 2.0"],
                  "entry": "GreenMod.Mod",
                  "dataEntry": "GreenMod.Data.ModData"
                }
                """);

            File.WriteAllText(Path.Combine(tempRoot, "src", "Main.cs"), """
                using Factorio.Runtime;
                using Factorio.Data;

                namespace GreenMod;

                public static class Mod
                {
                    public static void Main()
                    {
                        Events.OnPlayerCreated(e =>
                        {
                            var player = Globals.Game.GetPlayer(e.PlayerIndex);
                            if (player != null)
                                player.Print(Vanilla.Items.ElectronicCircuit + ":" + Vanilla.Machines.ChemicalPlant);
                        });
                    }
                }
                """);

            File.WriteAllText(Path.Combine(tempRoot, "data", "ModData.cs"), """
                using Factorio.Data;

                namespace GreenMod.Data;

                public sealed class ModData : ModDefinition
                {
                    public override void Define(ModBuilder mod)
                    {
                        mod.Add(
                            new BoolSettingDefinition("greenmod-enable-boost", defaultValue: true)
                            {
                                LocalisedName = "Enable boosted output",
                                LocalisedDescription = "Doubles atmospheric water condensation rate.",
                            },
                            new DoubleSettingDefinition("greenmod-water-multiplier", defaultValue: 1.5, min: 0.1, max: 10.0)
                            {
                                LocalisedName = "Water yield multiplier",
                            },
                            new CondenseRecipe(),
                            new CondenserMachine(),
                            new HighCapTank(),
                            new ElectricSmelter(),
                            new CondenserTech());
                    }
                }

                public sealed class CondenseRecipe : RecipeDefinition
                {
                    public CondenseRecipe() : base("condense-water")
                    {
                        LocalisedName = "Condense atmospheric water";
                        Icon = Vanilla.Icons.Fluids.Water;
                        Categories = [CraftingCategory.Chemistry];
                        Enabled = false;
                        EnergyRequired = 2.0;
                        Ingredients = [
                            Ingredient.Item(Vanilla.Items.ElectronicCircuit, 1),
                            Vanilla.Items.IronPlate * 2,
                        ];
                        Results = [
                            Vanilla.Fluids.Water * 50,
                            Vanilla.Items.Stone.Chance(1, 0.25),
                        ];
                    }
                }

                public sealed class CondenserMachine : AssemblingMachineDefinition
                {
                    public CondenserMachine() : base("atmospheric-condenser", Vanilla.Machines.ChemicalPlant)
                    {
                        LocalisedName = "Atmospheric condenser";
                        Icons = [
                            IconLayer.Tinted(Vanilla.Icons.Machines.ChemicalPlant, Rgb.Hex("#38BDF8")),
                            IconLayer.Badge(Vanilla.Icons.Fluids.Water),
                        ];
                        Corpse = Vanilla.Entities.BigRemnants;
                        CraftingCategories = [CraftingCategory.Chemistry];
                        CraftingSpeed = 2.0;
                        EnergyUsage = Energy.Kilowatts(180);
                        EnergySource = EnergySource.Electric(emissionsPerMinute: 1.5);
                        FluidBoxes = [
                            FluidBox.Output(Vanilla.Fluids.Water, volume: 1000, ports: [PipePort.North(0, -1)]),
                        ];
                        Craftable(
                            ingredients: [
                                Vanilla.Items.SteelPlate * 10,
                                Ingredient.Item(Vanilla.Items.ElectronicCircuit, 5),
                                Vanilla.Machines.Pump * 1,
                            ],
                            stackSize: 10,
                            subgroup: Subgroup.ExtractionMachine,
                            energyRequired: 2.0,
                            enabled: false);
                    }
                }

                public sealed class HighCapTank : StorageTankDefinition
                {
                    public HighCapTank() : base("high-cap-tank", Vanilla.Machines.StorageTank)
                    {
                        LocalisedName = "High-capacity tank";
                        Icons = [IconLayer.Tinted(Vanilla.Icons.Machines.StorageTank, Rgb.Hex("#22C55E"))];
                        MaxHealth = 800;
                        Craftable(
                            ingredients: [Vanilla.Items.SteelPlate * 25, Vanilla.Items.Pipe * 8],
                            stackSize: 20,
                            subgroup: Subgroup.Storage);
                    }
                }

                public sealed class ElectricSmelter : FurnaceDefinition
                {
                    public ElectricSmelter() : base("rapid-smelter", Vanilla.Machines.ElectricFurnace)
                    {
                        LocalisedName = "Rapid electric smelter";
                        Icons = [IconLayer.Tinted(Vanilla.Icons.Machines.ElectricFurnace, Rgb.Hex("#F59E0B"))];
                        CraftingSpeed = 4.0;
                        EnergyUsage = Energy.Kilowatts(320);
                        Craftable(
                            ingredients: [Vanilla.Items.SteelPlate * 15, Vanilla.Items.RedCircuit * 5],
                            stackSize: 20,
                            subgroup: Subgroup.SmeltingMachine);
                    }
                }

                public sealed class CondenserTech : TechnologyDefinition
                {
                    public CondenserTech() : base("atmospheric-condensation")
                    {
                        LocalisedName = "Atmospheric condensation";
                        Icon = Vanilla.Icons.Technologies.FluidHandling;
                        Prerequisites = [Vanilla.Technologies.FluidHandling, Vanilla.Technologies.Electronics];
                        Effects = [
                            TechEffect.UnlockRecipe("atmospheric-condenser"),
                            TechEffect.UnlockRecipe("condense-water"),
                        ];
                        Count = 100;
                        Time = 30;
                        SciencePacks = [SciencePack.Automation, SciencePack.Logistic];
                    }
                }
                """);

            var project = ModProject.Load(tempRoot);
            var outDir = Path.Combine(tempRoot, "out");
            var result = Factorio.ModTool.Build.ModBuilder.Build(
                Settings,
                project,
                new Factorio.ModTool.Build.ModBuilder.Options(Zip: true, OutputDirectory: outDir),
                _ => { });

            Assert.True(
                result.Ok,
                "Build failed:\n" + string.Join("\n", result.Findings.Select(f => $"{f.File}:{f.Line} {f.Message}")));
            Assert.Empty(result.Warnings);

            // Verify generated settings.lua exists and contains both settings:
            var settingsLua = File.ReadAllText(Path.Combine(result.ModDirectory, "settings.lua"));
            Assert.Contains("\"greenmod-enable-boost\"", settingsLua);
            Assert.Contains("\"greenmod-water-multiplier\"", settingsLua);

            // Verify locale.cfg contains both [mod-setting-name] and [entity-name] / [item-name]:
            var localeCfg = File.ReadAllText(Path.Combine(result.ModDirectory, "locale", "en", "locale.cfg"));
            Assert.Contains("[mod-setting-name]", localeCfg);
            Assert.Contains("greenmod-enable-boost=Enable boosted output", localeCfg);
            Assert.Contains("atmospheric-condenser=Atmospheric condenser", localeCfg);

            // Verify control.lua inlined Vanilla.Items.ElectronicCircuit & Vanilla.Machines.ChemicalPlant as pure strings:
            var controlLua = File.ReadAllText(Path.Combine(result.ModDirectory, "control.lua"));
            Assert.Contains("\"electronic-circuit\"", controlLua);
            Assert.Contains("\"chemical-plant\"", controlLua);
        }
        finally
        {
            if (Directory.Exists(tempRoot))
                Directory.Delete(tempRoot, recursive: true);
        }
    }
}
