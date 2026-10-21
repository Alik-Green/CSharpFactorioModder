using Factorio.Data;

namespace DemoChemistry
{
    /// <summary>
    /// Coolant in, polymer resin out: the recipe the machine runs.
    /// </summary>
    public sealed class PolymerResinRecipe : RecipeDefinition
    {
        /// <summary>Declares the recipe.</summary>
        public PolymerResinRecipe() : base("demo-polymer-resin")
        {
            Categories = [CraftingCategory.Chemistry];
            Enabled = true;
            EnergyRequired = 2;
            Order = SortOrder.At("z", "demo-polymer-resin");

            Ingredients =
            [
                Ingredient.Item("iron-plate", 2),
                Ingredient.Fluid("demo-coolant", 25),
            ];

            Results =
            [
                Product.Item("demo-polymer-resin", 2),
            ];

            LocalisedName = "Polymer resin";
        }
    }

    /// <summary>
    /// Resin into pellets, in the same machine: a second chemistry recipe shows off picking a recipe
    /// in the machine window.
    /// </summary>
    public sealed class PolymerPelletRecipe : RecipeDefinition
    {
        /// <summary>Declares the recipe.</summary>
        public PolymerPelletRecipe() : base("demo-polymer-pellet")
        {
            Categories = [CraftingCategory.Chemistry];
            Enabled = true;
            EnergyRequired = 1;
            Order = SortOrder.At("z", "demo-polymer-pellet");

            Ingredients =
            [
                Ingredient.Item("demo-polymer-resin", 2),
            ];

            Results =
            [
                Product.Item("demo-polymer-pellet", 4),
            ];

            LocalisedName = "Polymer pellet";
        }
    }

    /// <summary>
    /// The recipe that builds the machine, hand-craftable from the start.
    /// </summary>
    public sealed class ChemicalAssemblerRecipe : RecipeDefinition
    {
        /// <summary>Declares the recipe.</summary>
        public ChemicalAssemblerRecipe() : base("demo-chemical-assembler")
        {
            Categories = [CraftingCategory.Crafting];
            Enabled = true;
            EnergyRequired = 5;
            Order = SortOrder.At("z", "demo-chemical-assembler");

            Ingredients =
            [
                Ingredient.Item("iron-plate", 10),
                Ingredient.Item("electronic-circuit", 5),
                Ingredient.Item("stone-brick", 5),
            ];

            Results =
            [
                Product.Item("demo-chemical-assembler", 1),
            ];

            LocalisedName = "Chemical assembler";
        }
    }
}
