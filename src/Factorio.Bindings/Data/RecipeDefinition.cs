using System;
using System.Collections.Generic;
using System.Linq;

namespace Factorio.Data
{
    /// <summary>
    /// A recipe: what a machine turns into what.
    /// <para>
    /// The recipe's categories decide which machines can run it, and a machine's
    /// <see cref="AssemblingMachineDefinition.CraftingCategories"/> decides which recipes it accepts;
    /// if the two do not meet, the recipe can never be crafted. The build checks that every category
    /// a recipe names exists - either in the base game or as a
    /// <see cref="RecipeCategoryDefinition"/> of the same mod.
    /// </para>
    /// <code>
    /// public sealed class PolymerRecipe : RecipeDefinition
    /// {
    ///     public PolymerRecipe() : base("demo-polymer-pellet")
    ///     {
    ///         Categories = ["chemistry"];
    ///         Enabled = true;
    ///         EnergyRequired = 2;
    ///         Ingredients = [Ingredient.Item("iron-plate", 2), Ingredient.Fluid("demo-coolant", 25)];
    ///         Results = [Product.Item("demo-polymer-pellet", 1)];
    ///     }
    /// }
    /// </code>
    /// </summary>
    public class RecipeDefinition : PrototypeDefinition
    {
        /// <summary>Declares a recipe with the given prototype name.</summary>
        /// <param name="name">Recipe prototype name; usually the name of what it makes.</param>
        public RecipeDefinition(string name) : base("recipe", name)
        {
        }

        /// <summary>
        /// The categories this recipe belongs to, e.g. <c>["chemistry"]</c>. One is the usual case.
        /// <para>
        /// Written as <c>category</c> plus <c>additional_categories</c> for Factorio 2.0 and as
        /// <c>categories</c> for 2.1, because that is the spelling each version reads - a mod that
        /// declared 2.0 but wrote <c>categories</c> would load with a recipe no machine can make.
        /// </para>
        /// </summary>
        public Values? Categories
        {
            get => Get("categories") as Values;
            set => Set("categories", value);
        }

        /// <summary>Whether the recipe is available from the start, without research.</summary>
        public bool Enabled
        {
            get => Flag("enabled");
            set => Set("enabled", value);
        }

        /// <summary>Seconds one craft takes in a machine of crafting speed 1.</summary>
        public double EnergyRequired
        {
            get => Number("energy_required");
            set => Set("energy_required", value);
        }

        /// <summary>What one craft consumes: <c>[Ingredient.Item("iron-plate", 2)]</c>.</summary>
        public Values? Ingredients
        {
            get => Get("ingredients") as Values;
            set => Set("ingredients", value);
        }

        /// <summary>What one craft produces: <c>[Product.Item("demo-pellet", 1)]</c>.</summary>
        public Values? Results
        {
            get => Get("results") as Values;
            set => Set("results", value);
        }

        /// <summary>How many seconds of research unlock the recipe; 0 makes it a starting recipe.</summary>
        public double ResearchTime
        {
            get => Number("research_time");
            set => Set("research_time", value);
        }

        /// <summary>The product shown as the recipe's icon and name in the crafting menu.</summary>
        public string? MainProduct
        {
            get => Get("main_product") as string;
            set => Set("main_product", value);
        }

        /// <summary>Whether productivity modules affect this recipe.</summary>
        public bool AllowProductivity
        {
            get => Flag("allow_productivity");
            set => Set("allow_productivity", value);
        }

        /// <summary>Writes the recipe, spelling the category list the way the target version reads it.</summary>
        /// <param name="writer">The writer, which knows the target version.</param>
        protected override void WriteField(LuaWriter writer, string name, object? value)
        {
            if (name != "categories")
            {
                base.WriteField(writer, name, value);
                return;
            }

            var categories = (value as Values)?.ToList() ?? new List<object?>();
            if (writer.ApiVersion != "2.0")
            {
                writer.Key("categories");
                writer.Value(value);
                return;
            }

            // 2.0: one `category`, plus `additional_categories` for the rest.
            writer.Key("category");
            writer.Value(categories.Count > 0 ? categories[0] : null);
            if (categories.Count > 1)
            {
                var rest = new Values();
                foreach (var category in categories.Skip(1)) rest.Add(category);
                writer.Key("additional_categories");
                writer.Value(rest);
            }
        }
    }

    /// <summary>
    /// A recipe category: a set of recipes a kind of machine can run.
    /// <para>
    /// The base game's categories (<c>crafting</c>, <c>chemistry</c>, <c>smelting</c>, ...) exist
    /// already and need no declaration. A mod that invents one - say <c>gas-extraction</c> - has to
    /// declare it: a recipe whose category no prototype defines stops the game with
    /// "recipe-category with name ... does not exist".
    /// </para>
    /// </summary>
    public class RecipeCategoryDefinition : PrototypeDefinition
    {
        /// <summary>Declares a recipe category with the given name.</summary>
        /// <param name="name">Category name, as recipes and machines spell it.</param>
        public RecipeCategoryDefinition(string name) : base("recipe-category", name)
        {
        }
    }
}
