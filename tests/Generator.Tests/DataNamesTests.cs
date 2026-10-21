using System.Text.Json;
using Factorio.Data;
using Factorio.ModTool.Build;
using Factorio.ModTool.Data;
using Xunit;

namespace Factorio.Bindings.Tests;

/// <summary>
/// The names a prototype is filed under and called by: the tab it appears in, and the localisation
/// entry behind its display name. Both are silently wrong in the game - an item in the "Other" tab
/// loads, and so does one called "Unknown key: item-name.foo".
/// </summary>
public class DataNamesTests
{
    /// <summary>
    /// The names the C# API produces are plain strings in the Lua - the prototype API has no idea
    /// what a <c>Subgroup</c> is - and typed where a mod writes them.
    /// </summary>
    [Fact]
    public void Typed_names_are_written_as_plain_strings()
    {
        var lua = Generate("""
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
                        StackSize = 100;
                        Subgroup = Subgroup.IntermediateProduct;
                        Order = SortOrder.At("z", "demo-pellet");
                    }
                }
            }
            """, out _);

        Assert.Contains("subgroup = \"intermediate-product\"", lua);
        Assert.Contains("order = \"z[demo-pellet]\"", lua);
        Assert.DoesNotContain("Subgroup =", lua);
    }

    /// <summary>A name this mod invents is a string, and reads like the base game's ones.</summary>
    [Fact]
    public void A_custom_name_can_be_written_as_a_string_or_a_named_value()
    {
        var lua = Generate("""
            using Factorio.Data;
            namespace Demo
            {
                public sealed class ExampleMod : ModDefinition
                {
                    public override void Define(ModBuilder mod)
                    {
                        mod.Add(new Tab());
                        mod.Add(new Pellet());
                    }
                }

                public sealed class Tab : ItemSubgroupDefinition
                {
                    public Tab() : base("demo-machines") { Group = ItemGroup.Production; Order = "z"; }
                }

                public sealed class Pellet : ItemDefinition
                {
                    public Pellet() : base("demo-pellet") { StackSize = 100; Subgroup = "demo-machines"; }
                }
            }
            """, out _);

        Assert.Contains("name = \"demo-machines\"", lua);
        Assert.Contains("group = \"production\"", lua);
        Assert.Contains("subgroup = \"demo-machines\"", lua);
    }

    /// <summary>Flags are an enum in C# and a list of strings in the prototype.</summary>
    [Fact]
    public void Flags_are_written_as_the_names_the_game_uses()
    {
        var lua = Generate("""
            using Factorio.Data;
            namespace Demo
            {
                public sealed class ExampleMod : ModDefinition
                {
                    public override void Define(ModBuilder mod) => mod.Add(new Chest());
                }

                public sealed class Chest : ItemDefinition
                {
                    public Chest() : base("demo-chest")
                    {
                        StackSize = 1;
                        Flags = ItemFlag.NotStackable | ItemFlag.OnlyInCursor;
                    }
                }
            }
            """, out _);

        Assert.Contains("\"not-stackable\"", lua);
        Assert.Contains("\"only-in-cursor\"", lua);
        Assert.DoesNotContain("NotStackable", lua);
    }

    /// <summary>Entity flags spell the same way, including the digit in "16-way".</summary>
    [Fact]
    public void Entity_flags_spell_the_way_the_api_does()
    {
        var values = FlagList.Of(EntityFlag.PlaceableNeutral | EntityFlag.PlayerCreation | EntityFlag.BuildingDirection16Way);
        var names = values.Select(v => (string)v!).ToList();

        Assert.Equal(new[] { "placeable-neutral", "player-creation", "building-direction-16-way" }, names);
    }

    /// <summary>
    /// A name declared by a definition never reaches data.lua - the game reads names from
    /// localisation, so a `localised_name` key would be rejected as an unknown property - but it is
    /// reported to the build, which writes it into the packaged locale.
    /// </summary>
    [Fact]
    public void A_declared_name_is_reported_for_the_locale_not_written_to_the_prototype()
    {
        var lua = Generate("""
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
                        StackSize = 100;
                        LocalisedName = "Demo pellet";
                        LocalisedDescription = "A pellet.";
                    }
                }
            }
            """, out var result);

        Assert.DoesNotContain("localised_name", lua);
        Assert.DoesNotContain("localised_description", lua);

        var names = result.Names["item/demo-pellet"];
        Assert.Equal("Demo pellet", names["localised-name"]);
        Assert.Equal("A pellet.", names["localised-description"]);
    }

    /// <summary>
    /// The build's own locale warning: every visible prototype needs a name, or the game shows
    /// "Unknown key: item-name.x" where its name should be.
    /// </summary>
    [Fact]
    public void A_prototype_without_a_locale_entry_is_reported()
    {
        using var mod = new Scratch();
        var modDirectory = Path.Combine(mod.Directory, "packaged");
        Directory.CreateDirectory(Path.Combine(modDirectory, "locale", "en"));
        File.WriteAllText(Path.Combine(modDirectory, "locale", "en", "locale.cfg"), """
            [item-name]
            demo-pellet=Demo pellet
            """);

        var warnings = LocaleCheck.Names(
            modDirectory,
            new[] { Shape("item", "demo-pellet"), Shape("item", "demo-fork") },
            type => type == "item" ? "item-name" : null);

        var warning = Assert.Single(warnings);
        Assert.Contains("item 'demo-fork'", warning);
        Assert.Contains("Unknown key: item-name.demo-fork", warning);
        Assert.Contains("[item-name]", warning);
    }

    /// <summary>A hidden prototype is never shown, so it needs no name.</summary>
    [Fact]
    public void A_hidden_prototype_needs_no_name()
    {
        using var mod = new Scratch();
        var modDirectory = Path.Combine(mod.Directory, "packaged");
        Directory.CreateDirectory(modDirectory);

        using var document = JsonDocument.Parse("""{ "type": "recipe", "name": "hidden-recipe", "hidden": true }""");
        var hidden = new DataStage.Shape("recipe", "hidden-recipe", new[] { "type", "name", "hidden" }, document.RootElement.Clone());

        Assert.Empty(LocaleCheck.Names(modDirectory, new[] { hidden }, _ => "recipe-name"));
    }

    /// <summary>The generated locale keeps what the author wrote and adds what it was given.</summary>
    [Fact]
    public void The_locale_writer_merges_instead_of_replacing()
    {
        var merged = LocaleWriter.Merge("""
            [item-name]
            hand-written=A hand-written name
            """, new Dictionary<string, string>
            {
                ["item-name/demo-pellet"] = "Demo pellet",
                ["recipe-name/demo-pellet"] = "Demo pellet",
            });

        Assert.Contains("hand-written=A hand-written name", merged);
        Assert.Contains("demo-pellet=Demo pellet", merged);
        Assert.Contains("[recipe-name]", merged);
    }

    /// <summary>The generated locale replaces an entry the author had, rather than duplicating it.</summary>
    [Fact]
    public void The_locale_writer_updates_an_entry_the_author_wrote()
    {
        var merged = LocaleWriter.Merge("""
            [item-name]
            demo-pellet=Old name
            """, new Dictionary<string, string> { ["item-name/demo-pellet"] = "Demo pellet" });

        Assert.Contains("demo-pellet=Demo pellet", merged);
        Assert.DoesNotContain("Old name", merged);
        Assert.Equal(1, merged.Split("demo-pellet=").Length - 1);
    }

    /// <summary>Every base-game subgroup and group the named members cover really exists.</summary>
    [Fact]
    public void The_named_tabs_match_the_base_game()
    {
        var subgroupNames = typeof(Factorio.Data.Subgroup)
            .GetFields()
            .Where(f => f.IsStatic)
            .Select(f => ((Factorio.Data.Subgroup)f.GetValue(null)!).Name)
            .ToList();

        foreach (var name in subgroupNames)
            Assert.True(VanillaGroups.Subgroups.ContainsKey(name), $"'{name}' is not a base-game item-subgroup");

        var groupNames = typeof(Factorio.Data.ItemGroup)
            .GetFields()
            .Where(f => f.IsStatic)
            .Select(f => ((Factorio.Data.ItemGroup)f.GetValue(null)!).Name)
            .ToList();

        foreach (var name in groupNames)
            Assert.True(VanillaGroups.Groups.Contains(name), $"'{name}' is not a base-game item-group");
    }

    /// <summary>A temporary directory, deleted when the test ends.</summary>
    private sealed class Scratch : IDisposable
    {
        public Scratch()
        {
            Directory = Path.Combine(Path.GetTempPath(), "factorio-names-" + Guid.NewGuid().ToString("N"));
            System.IO.Directory.CreateDirectory(Directory);
        }

        public string Directory { get; }

        public void Dispose()
        {
            try { System.IO.Directory.Delete(Directory, true); } catch { /* best effort */ }
        }
    }

    private static DataStage.Shape Shape(string type, string name) =>
        new(type, name, new[] { "type", "name" });

    /// <summary>Compiles and runs a data stage, returning its Lua.</summary>
    private static string Generate(string source, out DataEntryGenerator.Result result)
    {
        var directory = Path.Combine(Path.GetTempPath(), "factorio-names-" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(Path.Combine(directory, "data"));
        var file = Path.Combine(directory, "data", "Demo.cs");
        File.WriteAllText(file, source);
        try
        {
            result = DataEntryGenerator.Generate("Demo.ExampleMod", new[] { file }, "2.0");
            return result.Lua;
        }
        finally
        {
            try { Directory.Delete(directory, true); } catch { /* best effort */ }
        }
    }
}
