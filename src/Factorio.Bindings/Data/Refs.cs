using System;

namespace Factorio.Data
{
    /// <summary>Strongly-typed reference to an item prototype name (implicitly convertible with <see cref="string"/>).</summary>
    public sealed class ItemId : PrototypeName
    {
        /// <summary>Creates an item prototype reference.</summary>
        public ItemId(string name) : base(name) { }

        /// <summary>Implicitly wraps a string prototype name.</summary>
        public static implicit operator ItemId?(string? name) => name == null ? null : new ItemId(name);

        /// <summary>Implicitly unwraps to its string prototype name.</summary>
        public static implicit operator string?(ItemId? id) => id?.Name;

        /// <summary>References any item prototype by name.</summary>
        public static ItemId Of(string name) => new(name);
    }

    /// <summary>Strongly-typed reference to an entity prototype name (implicitly convertible with <see cref="string"/>).</summary>
    public sealed class EntityId : PrototypeName
    {
        /// <summary>Creates an entity prototype reference.</summary>
        public EntityId(string name) : base(name) { }

        /// <summary>Implicitly wraps a string prototype name.</summary>
        public static implicit operator EntityId?(string? name) => name == null ? null : new EntityId(name);

        /// <summary>Implicitly unwraps to its string prototype name.</summary>
        public static implicit operator string?(EntityId? id) => id?.Name;

        /// <summary>References any entity prototype by name.</summary>
        public static EntityId Of(string name) => new(name);

        /// <summary>Vanilla 1×1 corpse remnants.</summary>
        public static readonly EntityId SmallRemnants = new("small-remnants");

        /// <summary>Vanilla 2×2 corpse remnants.</summary>
        public static readonly EntityId MediumRemnants = new("medium-remnants");

        /// <summary>Vanilla 3×3 corpse remnants (used by assembling machines &amp; chemical plants).</summary>
        public static readonly EntityId BigRemnants = new("big-remnants");
    }

    /// <summary>Strongly-typed reference to a fluid prototype name, with named constants for vanilla fluids.</summary>
    public sealed class FluidId : PrototypeName
    {
        /// <summary>Creates a fluid prototype reference.</summary>
        public FluidId(string name) : base(name) { }

        /// <summary>Implicitly wraps a string fluid name.</summary>
        public static implicit operator FluidId?(string? name) => name == null ? null : new FluidId(name);

        /// <summary>Implicitly unwraps to its string fluid name.</summary>
        public static implicit operator string?(FluidId? id) => id?.Name;

        /// <summary>References any fluid prototype by name.</summary>
        public static FluidId Of(string name) => new(name);

        /// <summary>Base-game water (<c>"water"</c>).</summary>
        public static readonly FluidId Water = new("water");

        /// <summary>Base-game steam (<c>"steam"</c>).</summary>
        public static readonly FluidId Steam = new("steam");

        /// <summary>Base-game crude oil (<c>"crude-oil"</c>).</summary>
        public static readonly FluidId CrudeOil = new("crude-oil");

        /// <summary>Base-game heavy oil (<c>"heavy-oil"</c>).</summary>
        public static readonly FluidId HeavyOil = new("heavy-oil");

        /// <summary>Base-game light oil (<c>"light-oil"</c>).</summary>
        public static readonly FluidId LightOil = new("light-oil");

        /// <summary>Base-game petroleum gas (<c>"petroleum-gas"</c>).</summary>
        public static readonly FluidId PetroleumGas = new("petroleum-gas");

        /// <summary>Base-game lubricant (<c>"lubricant"</c>).</summary>
        public static readonly FluidId Lubricant = new("lubricant");

        /// <summary>Base-game sulfuric acid (<c>"sulfuric-acid"</c>).</summary>
        public static readonly FluidId SulfuricAcid = new("sulfuric-acid");
    }

    /// <summary>Strongly-typed reference to a recipe prototype name (implicitly convertible with <see cref="string"/>).</summary>
    public sealed class RecipeId : PrototypeName
    {
        /// <summary>Creates a recipe prototype reference.</summary>
        public RecipeId(string name) : base(name) { }

        /// <summary>Implicitly wraps a string recipe name.</summary>
        public static implicit operator RecipeId?(string? name) => name == null ? null : new RecipeId(name);

        /// <summary>Implicitly unwraps to its string recipe name.</summary>
        public static implicit operator string?(RecipeId? id) => id?.Name;

        /// <summary>References any recipe prototype by name.</summary>
        public static RecipeId Of(string name) => new(name);
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
}
