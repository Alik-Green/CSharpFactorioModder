using Factorio.Data;

namespace DemoChemistry
{
    /// <summary>
    /// The coolant the machine works with. Built on the base game's water - its pipe sprites, its
    /// temperatures, its autoplace settings - and recoloured, because water already exists and a
    /// second fluid with the same role needs to look different, not behave differently.
    /// </summary>
    public sealed class DemoCoolant : FluidDefinition
    {
        /// <summary>Declares the coolant, inheriting from water.</summary>
        public DemoCoolant() : base("demo-coolant", Vanilla.FluidOf("water"))
        {
            // Its own droplet, so it is not a second water in the pipes or in the crafting menu.
            // The pipe windows still come from water, through the inheritance.
            Icon = Asset.Mod("graphics/icons/demo-coolant.png");
            IconSize = 64;

            BaseColor = new Rgb(0.08, 0.40, 0.85);
            FlowColor = new Rgb(0.15, 0.70, 1.00);

            LocalisedName = "Demo coolant";
        }
    }

    /// <summary>
    /// What the refiner makes from the coolant: the intermediate the pellet is pressed from.
    /// </summary>
    public sealed class PolymerResin : ItemDefinition
    {
        /// <summary>Declares the resin item.</summary>
        public PolymerResin() : base("demo-polymer-resin")
        {
            Icon = Asset.Mod("graphics/icons/polymer-resin.png");
            IconSize = 64;
            StackSize = 100;
            Subgroup = Subgroup.IntermediateProduct;
            Order = SortOrder.At("z", "demo-polymer-resin");

            LocalisedName = "Polymer resin";
        }
    }

    /// <summary>
    /// What the assembler makes from the resin. The item the mod exists to produce.
    /// </summary>
    public sealed class PolymerPellet : ItemDefinition
    {
        /// <summary>Declares the pellet item.</summary>
        public PolymerPellet() : base("demo-polymer-pellet")
        {
            Icon = Asset.Mod("graphics/icons/polymer-pellet.png");
            IconSize = 64;
            StackSize = 100;
            Subgroup = Subgroup.IntermediateProduct;
            Order = SortOrder.At("z", "demo-polymer-pellet");

            LocalisedName = "Polymer pellet";
        }
    }

    /// <summary>
    /// The item that places <see cref="ChemicalAssembler"/>. A placeable machine needs both: the
    /// entity on the map, and an item the player can hold.
    /// </summary>
    public sealed class ChemicalAssemblerItem : ItemDefinition
    {
        /// <summary>Declares the item and points it at the machine.</summary>
        public ChemicalAssemblerItem() : base("demo-chemical-assembler")
        {
            Icon = Asset.Mod("graphics/icons/chemical-assembler.png");
            IconSize = 64;
            StackSize = 50;
            Subgroup = Subgroup.ProductionMachine;
            Order = SortOrder.At("z", "demo-chemical-assembler");

            PlaceResult = EntityRef.Of("demo-chemical-assembler");

            LocalisedName = "Chemical assembler";
        }
    }
}
