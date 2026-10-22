using Factorio.Data;

namespace FactorioMod
{
    /// <summary>
    /// The mod's prototypes, written in C#.
    /// <para>
    /// This file is a data stage: <c>factorio-mod build</c> runs it and writes the prototypes it
    /// declares to <c>data.lua</c>. It is ordinary .NET code evaluated at build time, so loops, LINQ
    /// and helper methods all work here - the restrictions that apply to <c>src/</c>, which becomes
    /// Lua, do not apply.
    /// </para>
    /// <para>
    /// Delete <c>data/</c> and the "dataEntry"/"dataSources" keys of mod.json to write
    /// <c>lua/data.lua</c> by hand instead.
    /// </para>
    /// </summary>
    public sealed class ExampleMod : ModDefinition
    {
        /// <summary>Adds every prototype of this mod, in the order they should appear in data.lua.</summary>
        /// <param name="mod">The builder collecting the definitions.</param>
        public override void Define(ModBuilder mod)
        {
            mod.Add(new Token());
            mod.Add(new TokenRecipe());
        }
    }

    /// <summary>An item the mod adds.</summary>
    public sealed class Token : ItemDefinition
    {
        /// <summary>Declares the item.</summary>
        public Token() : base("FactorioMod-token")
        {
            // Every base-game item, fluid, machine, entity, recipe, tech, and icon is typed under
            // Vanilla.* — or drop your own PNG of ANY resolution in resources/ and use Asset.Mod(...)!
            Icon = Vanilla.Icons.Items.IronPlate;
            IconSize = 64;
            StackSize = 100;

            // The tab it appears in, and where in that tab. Without a subgroup an item is filed
            // under "Other"; the build warns about that, because the game does not.
            Subgroup = Subgroup.IntermediateProduct;
            Order = SortOrder.At("z", "FactorioMod-token");

            // The name the player sees. Written into locale/en/locale.cfg by the build, so it cannot
            // drift from the prototype.
            LocalisedName = "ModDisplayTitle token";
        }
    }

    /// <summary>A hand-craftable recipe for <see cref="Token"/>.</summary>
    public sealed class TokenRecipe : RecipeDefinition
    {
        /// <summary>Declares the recipe.</summary>
        public TokenRecipe() : base("FactorioMod-token")
        {
            Categories = [CraftingCategory.Crafting];
            Enabled = true;
            EnergyRequired = 1;
            Order = SortOrder.At("z", "FactorioMod-token");

            // Tip: `Ingredient.Item(Vanilla.Items.IronPlate, 1)` AND `Vanilla.Items.IronPlate * 1`
            // both work across all ~270+ vanilla items, fluids, and machines!
            Ingredients = [Ingredient.Item(Vanilla.Items.IronPlate, 1)];
            Results = [Product.Item("FactorioMod-token", 2)];

            LocalisedName = "ModDisplayTitle token";
        }
    }
}
