using System;

namespace Factorio.Data
{
    /// <summary>
    /// A prototype name used as a typed identifier in both data-stage definitions and runtime scripts.
    /// </summary>
    /// @CSharpLua.Ignore
    public abstract partial class PrototypeName : IEquatable<PrototypeName>
    {
        /// <summary>Creates a strongly-typed prototype name.</summary>
        /// <param name="name">The exact prototype name as Factorio spells it.</param>
        protected PrototypeName(string name)
        {
            Name = name;
        }

        /// <summary>The exact prototype name as Factorio spells it.</summary>
        /// @CSharpLua.Get = "{this}"
        public string Name { get; }

        /// <inheritdoc />
        /// @CSharpLua.Template = "{this}"
        public override string ToString() => Name;

        /// <inheritdoc />
        /// @CSharpLua.Template = "({this} == {0})"
        public bool Equals(PrototypeName? other) => other is not null && Name == other.Name;

        /// <summary>Concatenates a string with a prototype name.</summary>
        /// @CSharpLua.Template = "({0} .. {1})"
        public static string operator +(string left, PrototypeName right) => left + right.Name;

        /// <summary>Concatenates a prototype name with a string.</summary>
        /// @CSharpLua.Template = "({0} .. {1})"
        public static string operator +(PrototypeName left, string right) => left.Name + right;

        /// <summary>Concatenates two prototype names.</summary>
        /// @CSharpLua.Template = "({0} .. {1})"
        public static string operator +(PrototypeName left, PrototypeName right) => left.Name + right.Name;

        /// <inheritdoc />
        public override bool Equals(object? obj) => obj is PrototypeName other && Equals(other);

        /// <inheritdoc />
        public override int GetHashCode() => Name.GetHashCode();
    }

    /// <summary>Strongly-typed reference to an item prototype name.</summary>
    /// @CSharpLua.Ignore
    public sealed partial class ItemId : PrototypeName
    {
        /// <summary>Creates an item prototype reference.</summary>
        /// @CSharpLua.Template = "{0}"
        public ItemId(string name) : base(name) { }

        /// <summary>Implicitly wraps a string prototype name.</summary>
        /// @CSharpLua.Template = "{0}"
        public static implicit operator ItemId?(string? name) => name == null ? null : new ItemId(name);

        /// <summary>Implicitly unwraps to its string prototype name.</summary>
        /// @CSharpLua.Template = "{0}"
        public static implicit operator string(ItemId id) => id.Name;

        /// <summary>References any item prototype by name.</summary>
        /// @CSharpLua.Template = "{0}"
        public static ItemId Of(string name) => new(name);
    }

    /// <summary>Strongly-typed reference to an entity prototype name.</summary>
    /// @CSharpLua.Ignore
    public sealed partial class EntityId : PrototypeName
    {
        /// <summary>Creates an entity prototype reference.</summary>
        /// @CSharpLua.Template = "{0}"
        public EntityId(string name) : base(name) { }

        /// <summary>Implicitly wraps a string prototype name.</summary>
        /// @CSharpLua.Template = "{0}"
        public static implicit operator EntityId?(string? name) => name == null ? null : new EntityId(name);

        /// <summary>Implicitly unwraps to its string prototype name.</summary>
        /// @CSharpLua.Template = "{0}"
        public static implicit operator string(EntityId id) => id.Name;

        /// <summary>References any entity prototype by name.</summary>
        /// @CSharpLua.Template = "{0}"
        public static EntityId Of(string name) => new(name);

        /// <summary>Vanilla 1×1 corpse remnants (<c>"small-remnants"</c>).</summary>
        /// @CSharpLua.Get = "\"small-remnants\""
        public static EntityId SmallRemnants { get; } = new("small-remnants");

        /// <summary>Vanilla 2×2 corpse remnants (<c>"medium-remnants"</c>).</summary>
        /// @CSharpLua.Get = "\"medium-remnants\""
        public static EntityId MediumRemnants { get; } = new("medium-remnants");

        /// <summary>Vanilla 3×3 corpse remnants (<c>"big-remnants"</c>).</summary>
        /// @CSharpLua.Get = "\"big-remnants\""
        public static EntityId BigRemnants { get; } = new("big-remnants");
    }

    /// <summary>Strongly-typed reference to a fluid prototype name.</summary>
    /// @CSharpLua.Ignore
    public sealed partial class FluidId : PrototypeName
    {
        /// <summary>Creates a fluid prototype reference.</summary>
        /// @CSharpLua.Template = "{0}"
        public FluidId(string name) : base(name) { }

        /// <summary>Implicitly wraps a string fluid name.</summary>
        /// @CSharpLua.Template = "{0}"
        public static implicit operator FluidId?(string? name) => name == null ? null : new FluidId(name);

        /// <summary>Implicitly unwraps to its string fluid name.</summary>
        /// @CSharpLua.Template = "{0}"
        public static implicit operator string(FluidId id) => id.Name;

        /// <summary>References any fluid prototype by name.</summary>
        /// @CSharpLua.Template = "{0}"
        public static FluidId Of(string name) => new(name);

        /// <summary>Base-game water (<c>"water"</c>).</summary>
        /// @CSharpLua.Get = "\"water\""
        public static FluidId Water { get; } = new("water");

        /// <summary>Base-game steam (<c>"steam"</c>).</summary>
        /// @CSharpLua.Get = "\"steam\""
        public static FluidId Steam { get; } = new("steam");

        /// <summary>Base-game crude oil (<c>"crude-oil"</c>).</summary>
        /// @CSharpLua.Get = "\"crude-oil\""
        public static FluidId CrudeOil { get; } = new("crude-oil");

        /// <summary>Base-game heavy oil (<c>"heavy-oil"</c>).</summary>
        /// @CSharpLua.Get = "\"heavy-oil\""
        public static FluidId HeavyOil { get; } = new("heavy-oil");

        /// <summary>Base-game light oil (<c>"light-oil"</c>).</summary>
        /// @CSharpLua.Get = "\"light-oil\""
        public static FluidId LightOil { get; } = new("light-oil");

        /// <summary>Base-game petroleum gas (<c>"petroleum-gas"</c>).</summary>
        /// @CSharpLua.Get = "\"petroleum-gas\""
        public static FluidId PetroleumGas { get; } = new("petroleum-gas");

        /// <summary>Base-game lubricant (<c>"lubricant"</c>).</summary>
        /// @CSharpLua.Get = "\"lubricant\""
        public static FluidId Lubricant { get; } = new("lubricant");

        /// <summary>Base-game sulfuric acid (<c>"sulfuric-acid"</c>).</summary>
        /// @CSharpLua.Get = "\"sulfuric-acid\""
        public static FluidId SulfuricAcid { get; } = new("sulfuric-acid");
    }

    /// <summary>Strongly-typed reference to a recipe prototype name.</summary>
    /// @CSharpLua.Ignore
    public sealed partial class RecipeId : PrototypeName
    {
        /// <summary>Creates a recipe prototype reference.</summary>
        /// @CSharpLua.Template = "{0}"
        public RecipeId(string name) : base(name) { }

        /// <summary>Implicitly wraps a string recipe name.</summary>
        /// @CSharpLua.Template = "{0}"
        public static implicit operator RecipeId?(string? name) => name == null ? null : new RecipeId(name);

        /// <summary>Implicitly unwraps to its string recipe name.</summary>
        /// @CSharpLua.Template = "{0}"
        public static implicit operator string(RecipeId id) => id.Name;

        /// <summary>References any recipe prototype by name.</summary>
        /// @CSharpLua.Template = "{0}"
        public static RecipeId Of(string name) => new(name);
    }

    /// <summary>
    /// Reference to an existing prototype (vanilla or modded) that can be inherited from in the data
    /// stage AND used anywhere an <see cref="EntityId"/>, <see cref="ItemId"/>, <see cref="FluidId"/>,
    /// or <see cref="string"/> name is expected.
    /// </summary>
    /// @CSharpLua.Ignore
    public sealed partial class VanillaPrototype
    {
        /// <summary>References an existing prototype by type and name.</summary>
        public VanillaPrototype(string type, string name)
        {
            Type = type;
            Name = name;
        }

        /// <summary>The Factorio prototype type, e.g. <c>"assembling-machine"</c>.</summary>
        public string Type { get; }

        /// <summary>The Factorio prototype name, e.g. <c>"chemical-plant"</c>.</summary>
        /// @CSharpLua.Get = "{this}"
        public string Name { get; }

        /// <inheritdoc />
        /// @CSharpLua.Template = "{this}"
        public override string ToString() => Name;

        /// <summary>Implicitly converts to its string prototype name.</summary>
        /// @CSharpLua.Template = "{0}"
        public static implicit operator string(VanillaPrototype prototype) => prototype.Name;

        /// <summary>Implicitly converts to an <see cref="EntityId"/>.</summary>
        /// @CSharpLua.Template = "{0}"
        public static implicit operator EntityId?(VanillaPrototype? prototype) =>
            prototype == null ? null : new EntityId(prototype.Name);

        /// <summary>Implicitly converts to an <see cref="ItemId"/> (every buildable machine shares its item name).</summary>
        /// @CSharpLua.Template = "{0}"
        public static implicit operator ItemId?(VanillaPrototype? prototype) =>
            prototype == null ? null : new ItemId(prototype.Name);

        /// <summary>Implicitly converts to a <see cref="FluidId"/>.</summary>
        /// @CSharpLua.Template = "{0}"
        public static implicit operator FluidId?(VanillaPrototype? prototype) =>
            prototype == null ? null : new FluidId(prototype.Name);

        /// <summary>Concatenates a string with a prototype reference.</summary>
        /// @CSharpLua.Template = "({0} .. {1})"
        public static string operator +(string left, VanillaPrototype right) => left + right.Name;

        /// <summary>Concatenates a prototype reference with a string.</summary>
        /// @CSharpLua.Template = "({0} .. {1})"
        public static string operator +(VanillaPrototype left, string right) => left.Name + right;

        /// <summary>Concatenates two prototype references.</summary>
        /// @CSharpLua.Template = "({0} .. {1})"
        public static string operator +(VanillaPrototype left, VanillaPrototype right) => left.Name + right.Name;
    }
}
