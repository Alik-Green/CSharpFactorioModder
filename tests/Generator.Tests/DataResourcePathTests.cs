using System.Text.Json;
using Factorio.Data;
using Factorio.ModTool.Build;
using Factorio.ModTool.Data;
using Xunit;

namespace Factorio.Bindings.Tests;

/// <summary>
/// Resource paths. Factorio resolves an icon or a sprite filename only when it says which mod the
/// file belongs to - <c>__base__/graphics/x.png</c>, <c>__MyMod__/graphics/x.png</c> - and refuses to
/// load the mod otherwise:
///
///   Path graphics/icons/polymer-pellet.png not matching the resource path pattern: __source__/path
///
/// The failure happens in the game, after a restart, with a dialog in front of a disabled mod, so it
/// is checked at build time instead.
/// </summary>
public class DataResourcePathTests
{
    /// <summary>Asset.Mod writes the prefix the game requires, from the name the build supplies.</summary>
    [Fact]
    public void Mod_paths_are_prefixed_with_the_mod_name()
    {
        var previous = Asset.Source;
        try
        {
            Asset.Source = "DemoChemistry";
            Assert.Equal("__DemoChemistry__/graphics/icons/x.png", Asset.Mod("graphics/icons/x.png"));
            Assert.Equal("__DemoChemistry__/graphics/icons/x.png", Asset.Mod("/graphics/icons/x.png"));

            // A path that already names its source is left alone, so nothing is prefixed twice.
            Assert.Equal("__base__/graphics/icons/x.png", Asset.Mod("__base__/graphics/icons/x.png"));
        }
        finally
        {
            Asset.Source = previous;
        }
    }

    /// <summary>Without a name there is nothing to prefix with, and the build reports it separately.</summary>
    [Fact]
    public void Mod_paths_without_a_mod_name_stay_relative()
    {
        var previous = Asset.Source;
        try
        {
            Asset.Source = null;
            Assert.Equal("graphics/icons/x.png", Asset.Mod("graphics/icons/x.png"));
        }
        finally
        {
            Asset.Source = previous;
        }
    }

    /// <summary>Base-game paths get __base__, and are not touched if they already have a source.</summary>
    [Fact]
    public void Base_paths_are_prefixed_with_base()
    {
        Assert.Equal("__base__/graphics/icons/iron-plate.png", Asset.Base("graphics/icons/iron-plate.png"));
        Assert.Equal("__base__/graphics/icons/iron-plate.png", Asset.Base("__base__/graphics/icons/iron-plate.png"));
    }

    /// <summary>The data stage check rejects a bare path, with the game's own message and the fix.</summary>
    [Fact]
    public void A_bare_path_is_a_build_error()
    {
        var findings = DataStage.Validate(
            new[] { Prototype("""{ "type": "item", "name": "demo-pellet", "icon": "graphics/icons/polymer-pellet.png" }""") },
            ApiPath(),
            "2.0",
            modName: "DemoChemistry");

        var finding = Assert.Single(findings, f => f.Message.Contains("resource path pattern"));
        Assert.Contains("item demo-pellet", finding.Message);
        Assert.Contains("icon is 'graphics/icons/polymer-pellet.png'", finding.Message);
        Assert.Contains("__DemoChemistry__/graphics/icons/polymer-pellet.png", finding.Message);
        Assert.Contains("Asset.Mod(", finding.Message);
    }

    /// <summary>The check looks inside the nested sprite tables too, not only at the top-level icon.</summary>
    [Fact]
    public void A_bare_path_deep_in_a_sprite_is_a_build_error()
    {
        var findings = DataStage.Validate(
            new[]
            {
                Prototype("""
                    {
                      "type": "assembling-machine",
                      "name": "demo-machine",
                      "crafting_categories": ["chemistry"],
                      "graphics_set": { "animation": { "layers": [ { "filename": "graphics/entity/x.png" } ] } }
                    }
                    """),
            },
            ApiPath(),
            "2.0",
            modName: "DemoChemistry");

        var finding = Assert.Single(findings, f => f.Message.Contains("resource path pattern"));
        Assert.Contains("graphics_set.animation.layers[0].filename", finding.Message);
    }

    /// <summary>Prefixed paths, base-game paths and paths belonging to another mod are accepted.</summary>
    [Fact]
    public void Prefixed_paths_are_fine()
    {
        var findings = DataStage.Validate(
            new[]
            {
                Prototype("""{ "type": "item", "name": "a", "icon": "__DemoChemistry__/graphics/icons/a.png" }"""),
                Prototype("""{ "type": "item", "name": "b", "icon": "__base__/graphics/icons/b.png" }"""),
                Prototype("""{ "type": "item", "name": "c", "icon": "__other-mod__/graphics/icons/c.png" }"""),
            },
            ApiPath(),
            "2.0",
            modName: "DemoChemistry");

        Assert.DoesNotContain(findings, f => f.Message.Contains("resource path pattern"));
    }

    /// <summary>A prototype name that happens to end in a dot-something is not a path.</summary>
    [Fact]
    public void A_name_is_not_mistaken_for_a_path()
    {
        var findings = DataStage.Validate(
            new[] { Prototype("""{ "type": "item", "name": "a", "subgroup": "intermediate-product", "icon": null }""") },
            ApiPath(),
            "2.0",
            modName: "DemoChemistry");

        Assert.DoesNotContain(findings, f => f.Message.Contains("resource path pattern"));
    }

    /// <summary>
    /// End to end: a definition that uses Asset.Mod gets the prefix from the name the build passes
    /// to the generator, and the generated Lua is what the game accepts.
    /// </summary>
    [Fact]
    public void The_generator_writes_the_prefixed_path()
    {
        var directory = Path.Combine(Path.GetTempPath(), "factorio-paths-" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(Path.Combine(directory, "data"));
        var file = Path.Combine(directory, "data", "Demo.cs");
        File.WriteAllText(file, """
            using Factorio.Data;
            namespace Demo
            {
                public sealed class ExampleMod : ModDefinition
                {
                    public override void Define(ModBuilder mod) => mod.Add(new Pellet());
                }

                public sealed class Pellet : ItemDefinition
                {
                    public Pellet() : base("demo-pellet")
                    {
                        Icon = Asset.Mod("graphics/icons/polymer-pellet.png");
                        IconSize = 64;
                        StackSize = 100;
                    }
                }
            }
            """);

        try
        {
            var result = DataEntryGenerator.Generate("Demo.ExampleMod", new[] { file }, "2.0", "DemoChemistry");
            Assert.Contains("__DemoChemistry__/graphics/icons/polymer-pellet.png", result.Lua);

            // The name is put back, so the next build in this process is not prefixed by this one.
            Assert.Null(Asset.Source);
        }
        finally
        {
            try { Directory.Delete(directory, true); } catch { /* best effort */ }
        }
    }

    private static DataStage.Shape Prototype(string json)
    {
        using var document = JsonDocument.Parse(json);
        var root = document.RootElement;
        var keys = root.EnumerateObject().Select(p => p.Name).ToList();
        return new DataStage.Shape(root.GetProperty("type").GetString()!, root.GetProperty("name").GetString()!, keys, root.Clone());
    }

    private static string ApiPath() => TestEnvironment.PrototypeJson;
}
