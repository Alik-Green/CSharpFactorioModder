using Factorio.Data;

namespace DemoChemistry
{
    /// <summary>
    /// How the coolant is made: water through a vanilla chemical plant, so the demo can be played
    /// without cheating one in.
    /// <para>
    /// The recipe is in the base game's <c>chemistry</c> category, which means every vanilla
    /// chemical plant can run it - the demo machine is not needed to produce its own input, only to
    /// use it.
    /// </para>
    /// </summary>
    public sealed class CoolantRecipe : RecipeDefinition
    {
        /// <summary>Declares the recipe.</summary>
        public CoolantRecipe() : base("demo-coolant")
        {
            Categories = [CraftingCategory.Chemistry];
            Enabled = true;
            EnergyRequired = 2;
            Order = SortOrder.At("z", "demo-coolant");

            Ingredients =
            [
                Ingredient.Fluid("water", 50),
                Ingredient.Item("iron-plate", 1),
            ];

            Results =
            [
                Product.Fluid("demo-coolant", 50),
            ];

            LocalisedName = "Demo coolant";
        }
    }
}
