using System;

namespace Factorio.Data
{
    /// <summary>
    /// An item: something an inserter can carry, a chest can hold, or a machine is built from.
    /// <para>
    /// A placeable machine needs two prototypes - the entity and the item that places it - and the
    /// item points at the entity with <see cref="PlaceResult"/>.
    /// </para>
    /// <code>
    /// public sealed class ChemicalAssemblerItem : ItemDefinition
    /// {
    ///     public ChemicalAssemblerItem() : base("demo-chemical-assembler")
    ///     {
    ///         Icon = Asset.Base("graphics/icons/assembling-machine-1.png");
    ///         IconSize = 64;
    ///         StackSize = 50;
    ///         PlaceResult = EntityRef.Of("demo-chemical-assembler");
    ///     }
    /// }
    /// </code>
    /// </summary>
    public class ItemDefinition : PrototypeDefinition
    {
        private ItemFlag _flags = ItemFlag.None;

        /// <summary>Declares an item with the given prototype name.</summary>
        /// <param name="name">Item prototype name.</param>
        public ItemDefinition(string name) : base("item", name)
        {
        }

        /// <summary>How many fit in one inventory slot. Required by the game.</summary>
        public double StackSize
        {
            get => Number("stack_size");
            set => Set("stack_size", value);
        }

        /// <summary>
        /// The entity this item places, for a machine: <c>EntityRef.Of("demo-assembler")</c>. Leave
        /// it unset for a plain item such as a plate or a pellet.
        /// </summary>
        public string? PlaceResult
        {
            get => Get("place_result") as string;
            set => Set("place_result", value);
        }

        /// <summary>
        /// The item flags: special behaviour such as <c>ItemFlag.NotStackable</c>. Most items need
        /// none, which is also why an unset <c>flags</c> writes nothing.
        /// </summary>
        public ItemFlag Flags
        {
            get => _flags;
            set
            {
                _flags = value;
                Set("flags", FlagList.Of(value));
            }
        }

        /// <summary>How much fuel the item is worth; unset for a non-fuel item.</summary>
        public string? FuelValue
        {
            get => Get("fuel_value") as string;
            set => Set("fuel_value", value);
        }
    }
}
