using Factorio.Data;

namespace {{namespace}}
{
    /// <summary>
    /// The mod's prototypes, written in C#.
    /// <para>
    /// This file is a data stage: <c>factorio-mod build</c> runs it and writes the prototypes it
    /// declares to <c>data.lua</c>. It is ordinary .NET code evaluated at build time, so loops,
    /// LINQ and helper methods all work here - the restrictions that apply to <c>src/</c>, which
    /// becomes Lua, do not apply.
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
        public Token() : base("{{id}}-token")
        {
            // Reusing the base game's art means nothing has to be drawn or rendered. A mod with
            // its own graphics puts them in resources/ and uses Asset.Mod("graphics/icons/x.png").
            Icon = Asset.Base("graphics/icons/iron-plate.png");
            IconSize = 64;
            StackSize = 100;

            // The tab it appears in, and where in that tab. Without a subgroup an item is filed
            // under "Other"; the build warns about that, because the game does not.
            Subgroup = Subgroup.IntermediateProduct;
            Order = SortOrder.At("z", "{{id}}-token");

            // The name the player sees. Written into locale/en/locale.cfg by the build, so it
            // cannot drift from the prototype - and the build warns about any visible prototype
            // that has no name at all.
            LocalisedName = "{{title}} token";
        }
    }

    /// <summary>A hand-craftable recipe for <see cref="Token"/>.</summary>
    public sealed class TokenRecipe : RecipeDefinition
    {
        /// <summary>Declares the recipe.</summary>
        public TokenRecipe() : base("{{id}}-token")
        {
            Categories = [CraftingCategory.Crafting];
            Enabled = true;
            EnergyRequired = 1;
            Order = SortOrder.At("z", "{{id}}-token");

            Ingredients = [Ingredient.Item("iron-plate", 1)];
            Results = [Product.Item("{{id}}-token", 2)];

            LocalisedName = "{{title}} token";
        }
    }
}
