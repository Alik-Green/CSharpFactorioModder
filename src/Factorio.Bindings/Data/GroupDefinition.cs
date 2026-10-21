using System;

namespace Factorio.Data
{
    /// <summary>
    /// A tab in the crafting menu's left-hand column: <c>item-group</c>.
    /// <para>
    /// A mod rarely needs one - the base game's eleven cover most things - but a total conversion
    /// does, and an item-subgroup needs a group to belong to.
    /// </para>
    /// </summary>
    public class ItemGroupDefinition : PrototypeDefinition
    {
        /// <summary>Declares a crafting-menu tab.</summary>
        /// <param name="name">Group name, e.g. <c>production</c>.</param>
        public ItemGroupDefinition(string name) : base("item-group", name)
        {
        }
    }

    /// <summary>
    /// A section inside a crafting-menu tab: <c>item-subgroup</c>.
    /// <para>
    /// This is what an item's <see cref="PrototypeDefinition.Subgroup"/> points at. Declaring one is
    /// how a mod gets its own section - anything else sends the item to the base game's sections, or
    /// to "Other" when no subgroup is set at all.
    /// </para>
    /// <code>
    /// public sealed class DemoMachines : ItemSubgroupDefinition
    /// {
    ///     public DemoMachines() : base("demo-machines")
    ///     {
    ///         Group = ItemGroup.Production;
    ///         Order = SortOrder.Of("z");
    ///     }
    /// }
    /// </code>
    /// </summary>
    public class ItemSubgroupDefinition : PrototypeDefinition
    {
        /// <summary>Declares a crafting-menu section.</summary>
        /// <param name="name">Subgroup name, as an item's <c>subgroup</c> spells it.</param>
        public ItemSubgroupDefinition(string name) : base("item-subgroup", name)
        {
        }

        /// <summary>
        /// The tab this section belongs to, e.g. <c>ItemGroup.Production</c>. Required by the game:
        /// a subgroup that names a group which does not exist stops the load.
        /// </summary>
        public ItemGroup? Group
        {
            get => Get("group") is string group ? ItemGroup.Of(group) : null;
            set => Set("group", value);
        }
    }
}
