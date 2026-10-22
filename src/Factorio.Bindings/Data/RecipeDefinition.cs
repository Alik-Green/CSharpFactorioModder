using System;
using System.Collections.Generic;
using System.Linq;

namespace Factorio.Data
{
    /// <summary>
    /// A recipe: what a machine turns into what.
    /// </summary>
    public class RecipeDefinition : PrototypeDefinition
    {
        /// <summary>Declares a recipe with the given prototype name.</summary>
        /// <param name="name">Recipe prototype name; usually the name of what it makes.</param>
        public RecipeDefinition(string name) : base("recipe", name)
        {
        }

        /// <summary>
        /// The categories this recipe belongs to, e.g. <c>[CraftingCategory.Chemistry]</c> or <c>["atmospheric"]</c>.
        /// </summary>
        public CategoryList? Categories
        {
            get => Get("categories") as CategoryList;
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

        /// <summary>What one craft consumes: <c>[Ingredient.Item("iron-plate", 2)]</c> (or <c>[]</c>).</summary>
        public RecipeValueList? Ingredients
        {
            get => Get("ingredients") as RecipeValueList;
            set => Set("ingredients", value);
        }

        /// <summary>What one craft produces: <c>[Product.Item("atmospheric-condenser", 1)]</c>.</summary>
        public RecipeValueList? Results
        {
            get => Get("results") as RecipeValueList;
            set => Set("results", value);
        }

        /// <summary>Seconds of research to unlock the recipe.</summary>
        public double ResearchTime
        {
            get => Number("research_time");
            set => Set("research_time", value);
        }

        /// <summary>The primary product name shown as the recipe's icon and name in the crafting menu.</summary>
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
        protected override void WriteField(LuaWriter writer, string name, object? value)
        {
            if (name != "categories")
            {
                base.WriteField(writer, name, value);
                return;
            }

            var categories = value switch
            {
                CategoryList typed => typed.Select(c => (object?)c.Name).ToList(),
                Values untyped => untyped.ToList(),
                _ => new List<object?>(),
            };

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
    /// A custom recipe category: e.g. <c>new RecipeCategoryDefinition("atmospheric-condensation")</c>.
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
