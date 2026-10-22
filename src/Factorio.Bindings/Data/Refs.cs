using System;

namespace Factorio.Data
{
    public sealed partial class ItemId
    {
        /// <summary>Recipe shorthand: <c>Vanilla.Items.ElectronicCircuit * 5</c> creates an item ingredient/product of count <paramref name="amount"/>.</summary>
        public static RecipeValue operator *(ItemId item, double amount) =>
            RecipeValue.Item(item.Name, amount);

        /// <summary>Recipe shorthand: <c>5 * Vanilla.Items.ElectronicCircuit</c>.</summary>
        public static RecipeValue operator *(double amount, ItemId item) =>
            RecipeValue.Item(item.Name, amount);

        /// <summary>Creates an item product with a probability between <c>0</c> and <c>1</c> (e.g. <c>0.25</c> = 25%).</summary>
        public RecipeValue Chance(double amount, double probability) =>
            RecipeValue.Item(Name, amount, probability: probability);

        /// <summary>Creates an item product giving a random count between <paramref name="min"/> and <paramref name="max"/>.</summary>
        public RecipeValue Range(double min, double max, double? probability = null) =>
            RecipeValue.ItemRange(Name, min, max, probability);
    }

    public sealed partial class FluidId
    {
        /// <summary>Allows passing <c>Vanilla.Fluids.Water</c> directly to <c>new FluidDefinition("...", Vanilla.Fluids.Water)</c>.</summary>
        public static implicit operator VanillaPrototype?(FluidId? fluid) =>
            fluid == null ? null : new VanillaPrototype("fluid", fluid.Name);

        /// <summary>Recipe shorthand: <c>Vanilla.Fluids.Water * 50</c> creates a fluid ingredient/product of <paramref name="amount"/> units.</summary>
        public static RecipeValue operator *(FluidId fluid, double amount) =>
            RecipeValue.Fluid(fluid.Name, amount);

        /// <summary>Recipe shorthand: <c>50 * Vanilla.Fluids.Water</c>.</summary>
        public static RecipeValue operator *(double amount, FluidId fluid) =>
            RecipeValue.Fluid(fluid.Name, amount);

        /// <summary>Creates a fluid product with an explicit output temperature (e.g. <c>Vanilla.Fluids.Steam.AtTemp(50, 165)</c>).</summary>
        public RecipeValue AtTemp(double amount, double temperature, double? probability = null) =>
            RecipeValue.Fluid(Name, amount, temperature: temperature, probability: probability);
    }

    /// <summary>Factory helpers for item prototype references.</summary>
    public static class ItemRef
    {
        /// <summary>References an item prototype by name.</summary>
        public static ItemId Of(string name) => new(name);
    }

    /// <summary>Factory helpers for entity prototype references.</summary>
    public static class EntityRef
    {
        /// <summary>References an entity prototype by name.</summary>
        public static EntityId Of(string name) => new(name);
    }

    /// <summary>Factory helpers for fluid prototype references.</summary>
    public static class FluidRef
    {
        /// <summary>References a fluid prototype by name.</summary>
        public static FluidId Of(string name) => new(name);
    }

    /// <summary>Factory helpers for recipe prototype references.</summary>
    public static class RecipeRef
    {
        /// <summary>References a recipe prototype by name.</summary>
        public static RecipeId Of(string name) => new(name);
    }

    /// <summary>Factory helpers for technology prototype references.</summary>
    public static class TechRef
    {
        /// <summary>References a technology prototype by name.</summary>
        public static TechnologyId Of(string name) => new(name);
    }
}
