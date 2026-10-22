using System;
using System.Collections.Generic;
using System.IO;
using Factorio.Data;
using Factorio.ModTool.Build;
using Factorio.ModTool.Data;
using Xunit;

namespace Generator.Tests;

public class TypedDataAndAutoScaleTests
{
    private static void WriteMinimalPng(string path, int width, int height)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(path)!);
        var bytes = new byte[24];
        // PNG 8-byte signature
        bytes[0] = 0x89; bytes[1] = 0x50; bytes[2] = 0x4E; bytes[3] = 0x47;
        bytes[4] = 0x0D; bytes[5] = 0x0A; bytes[6] = 0x1A; bytes[7] = 0x0A;
        // IHDR length (13) + 'IHDR'
        bytes[11] = 13;
        bytes[12] = (byte)'I'; bytes[13] = (byte)'H'; bytes[14] = (byte)'D'; bytes[15] = (byte)'R';
        // Big-endian width & height
        bytes[16] = (byte)(width >> 24); bytes[17] = (byte)(width >> 16);
        bytes[18] = (byte)(width >> 8);  bytes[19] = (byte)width;
        bytes[20] = (byte)(height >> 24); bytes[21] = (byte)(height >> 16);
        bytes[22] = (byte)(height >> 8);  bytes[23] = (byte)height;
        File.WriteAllBytes(path, bytes);
    }

    [Fact]
    public void Sprite_Auto_and_Icon_automatically_read_PNG_dimensions_and_scale_to_machine_tiles()
    {
        var tempModDir = Path.Combine(Path.GetTempPath(), "fcm-autoscale-" + Guid.NewGuid().ToString("N"));
        try
        {
            // Exact dimensions of a non-square user PNG (146x149) + 256x256 icon
            WriteMinimalPng(Path.Combine(tempModDir, "resources", "graphics", "entity", "atmospheric-condenser.png"), 146, 149);
            WriteMinimalPng(Path.Combine(tempModDir, "resources", "graphics", "icons", "atmospheric-condenser.png"), 256, 256);

            var dataCs = Path.Combine(tempModDir, "data", "ModData.cs");
            Directory.CreateDirectory(Path.GetDirectoryName(dataCs)!);
            File.WriteAllText(dataCs,
                """
                using Factorio.Data;

                namespace GreenMod.Data;

                public sealed class ModData : ModDefinition
                {
                    public override void Define(ModBuilder mod)
                    {
                        mod.Add(new CondenserMachine());
                        mod.Add(new CondenserItem());
                        mod.Add(new CondenserTech());
                    }
                }

                public sealed class CondenserMachine : AssemblingMachineDefinition
                {
                    public CondenserMachine() : base("atmospheric-condenser")
                    {
                        LocalisedName = "Atmospheric Condenser";
                        Icon = IconSpec.Mod("graphics/icons/atmospheric-condenser.png");
                        CollisionBox = BoundingBox.Collision(2, 3);
                        SelectionBox = BoundingBox.Selection(2, 3);
                        Graphics = GraphicsSet.Auto(Asset.Mod("graphics/entity/atmospheric-condenser.png"), tiles: 3);
                        CraftingCategories = [CraftingCategory.Chemistry];
                        CraftingSpeed = 1.5;
                        FixedRecipe = "condense-water";
                        EnergyUsage = Energy.Kilowatts(180);
                        EnergySource = EnergySource.Electric(drain: Energy.Kilowatts(6), emissionsPerMinute: 1.5);
                        FluidBoxes = [FluidBox.Output(FluidId.Water, 100, [PipePort.East(1, 0)])];
                        ModuleSlots = 2;
                        AllowedEffects = EffectType.Speed | EffectType.Consumption;
                        MinableResult = "atmospheric-condenser";
                    }
                }

                public sealed class CondenserItem : ItemDefinition
                {
                    public CondenserItem() : base("atmospheric-condenser")
                    {
                        // Deliberately pass a mismatched IconSize = 64 on a 146x149 PNG: auto-fit saves it!
                        Icon = Asset.Mod("graphics/entity/atmospheric-condenser.png");
                        IconSize = 64;
                        StackSize = 10;
                        Subgroup = Subgroup.ExtractionMachine;
                        PlaceResult = "atmospheric-condenser";
                    }
                }

                public sealed class CondenserTech : TechnologyDefinition
                {
                    public CondenserTech() : base("atmospheric-condensation")
                    {
                        Icon = Asset.Mod("graphics/icons/atmospheric-condenser.png");
                        Prerequisites = ["fluid-handling"];
                        Effects = [TechEffect.UnlockRecipe("atmospheric-condenser")];
                        Count = 100;
                        Time = 30;
                        SciencePacks = [SciencePack.Automation, SciencePack.Logistic];
                    }
                }
                """);

            var result = DataEntryGenerator.Generate("GreenMod.Data.ModData", new[] { dataCs }, "2.0", "GreenMod");

            // 1. Auto-measured 146x149 sprite scaled cleanly to 3 tiles (96 px / 149 px = 0.6443)
            Assert.Contains("width = 146", result.Lua);
            Assert.Contains("height = 149", result.Lua);
            Assert.Contains("scale = 0.6443", result.Lua);

            // 2. Omitted IconSize on 256x256 icon automatically became icon_size = 256,
            //    and mismatched IconSize = 64 on 146x149 PNG automatically became icon_size = 146!
            Assert.Contains("icon_size = 256", result.Lua);
            Assert.Contains("icon_size = 146", result.Lua);

            // 3. Strongly-typed BoundingBox helpers wrote clean Factorio 2x3 boxes
            Assert.Contains("{ -0.7, -1.2 }", result.Lua);
            Assert.Contains("{ 0.7, 1.2 }", result.Lua);
            Assert.Contains("{ -1, -1.5 }", result.Lua);
            Assert.Contains("{ 1, 1.5 }", result.Lua);

            // 4. Strongly-typed FixedRecipe, ModuleSlots, AllowedEffects, EnergySource, and TechnologyDefinition
            Assert.Contains("fixed_recipe = \"condense-water\"", result.Lua);
            Assert.Contains("module_slots = 2", result.Lua);
            Assert.Contains("drain = \"6kW\"", result.Lua);
            Assert.Contains("type = \"technology\"", result.Lua);
            Assert.Contains("type = \"unlock-recipe\"", result.Lua);
        }
        finally
        {
            if (Directory.Exists(tempModDir))
                Directory.Delete(tempModDir, recursive: true);
        }
    }

    [Fact]
    public void Legacy_Sprite_Scaled_with_oversized_dimensions_automatically_clamps_to_actual_PNG_size()
    {
        var tempModDir = Path.Combine(Path.GetTempPath(), "fcm-legacyclamp-" + Guid.NewGuid().ToString("N"));
        try
        {
            WriteMinimalPng(Path.Combine(tempModDir, "resources", "graphics", "entity", "atmospheric-condenser.png"), 146, 149);

            Asset.Source = "GreenMod";
            Asset.ResourceRoots = new[] { Path.Combine(tempModDir, "resources") };
            try
            {
                var sprite = Sprite.Scaled(Asset.Mod("graphics/entity/atmospheric-condenser.png"), 192, 128, 0.5);
                var writer = new LuaWriter("2.0");
                sprite.Write(writer);
                var lua = writer.ToString();

                Assert.Contains("width = 146", lua);
                Assert.Contains("height = 149", lua);
                // Desired box was 96x64 px; fitting inside 146x149 gives min(96/146, 64/149) = 0.4295
                Assert.Contains("scale = 0.4295", lua);
            }
            finally
            {
                Asset.Source = null;
                Asset.ResourceRoots = null;
            }
        }
        finally
        {
            if (Directory.Exists(tempModDir))
                Directory.Delete(tempModDir, recursive: true);
        }
    }

    [Fact]
    public void BoundingBox_supports_corners_width_height_and_tuples()
    {
        var corners = new BoundingBox(-1, -1, 1, 1);
        Assert.Equal((-1, -1, 1, 1), (corners.Left, corners.Top, corners.Right, corners.Bottom));

        var centered = new BoundingBox(2, 3);
        Assert.Equal((-1, -1.5, 1, 1.5), (centered.Left, centered.Top, centered.Right, centered.Bottom));

        BoundingBox from4Tuple = (-1.2, -1.2, 1.2, 1.2);
        Assert.Equal(2.4, from4Tuple.Width, 3);

        BoundingBox from2Tuple = (4, 2);
        Assert.Equal((-2, -1, 2, 1), (from2Tuple.Left, from2Tuple.Top, from2Tuple.Right, from2Tuple.Bottom));

        var hexColor = Rgb.Hex("#FF8000");
        Assert.Equal(1.0, hexColor.Red, 3);
        Assert.Equal(0.502, hexColor.Green, 3);
        Assert.Equal(0.0, hexColor.Blue, 3);
    }

    [Fact]
    public void DataStageReferences_catches_entity_missing_its_own_MinableResult_item()
    {
        using var doc = System.Text.Json.JsonDocument.Parse(
            """
            {
              "type": "assembling-machine",
              "name": "atmospheric-condenser",
              "minable": { "result": "atmospheric-condenser", "mining_time": 0.5 }
            }
            """);

        var shapes = new List<DataStage.Shape>
        {
            new("assembling-machine", "atmospheric-condenser", Array.Empty<string>(), doc.RootElement.Clone()),
        };

        var findings = new List<LuaCheck.Finding>();
        DataStageReferences.Check(shapes, hasForeignDependencies: false, findings);

        var finding = Assert.Single(findings);
        Assert.Contains("minable.result names item 'atmospheric-condenser'", finding.Message);
        Assert.Contains("ItemDefinition", finding.Message);
    }
}
