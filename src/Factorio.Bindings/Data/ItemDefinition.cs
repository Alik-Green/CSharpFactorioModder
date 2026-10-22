using System;

namespace Factorio.Data
{
    /// <summary>
    /// An item: something an inserter can carry, a chest can hold, or a machine is built from.
    /// <para>
    /// A placeable machine needs two prototypes - the entity and the item that places it - and the
    /// item points at the entity with <see cref="PlaceResult"/>.
    /// </para>
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
        /// The entity this item places when built in the world:
        /// e.g. <c>EntityRef.Of("atmospheric-condenser")</c> or <c>"atmospheric-condenser"</c>.
        /// </summary>
        public EntityId? PlaceResult
        {
            get => Get("place_result") as string;
            set => Set("place_result", value?.Name);
        }

        /// <summary>Item behaviour flags such as <c>ItemFlag.NotStackable</c>.</summary>
        public ItemFlag Flags
        {
            get => _flags;
            set
            {
                _flags = value;
                Set("flags", FlagList.Of(value));
            }
        }

        /// <summary>How much fuel energy the item is worth (e.g. <c>Energy.Megajoules(4)</c>).</summary>
        public EnergyValue? FuelValue
        {
            get => Get("fuel_value") as string;
            set => Set("fuel_value", value?.Formatted);
        }
    }
}
