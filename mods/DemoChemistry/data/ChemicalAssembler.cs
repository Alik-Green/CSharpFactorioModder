using Factorio.Data;

namespace DemoChemistry
{
    /// <summary>
    /// A chemistry machine that makes its own coolant from water and turns it into polymer resin.
    /// <para>
    /// It is built on the vanilla assembling machine for its collision box, energy source and
    /// sounds, and overrides what makes it different: its own graphics, the chemistry category, the
    /// fluid boxes. Nothing here is inherited by accident - the vanilla upgrade path is cleared, so
    /// this machine is not an assembling machine 2 in disguise.
    /// </para>
    /// </summary>
    public sealed class ChemicalAssembler : AssemblingMachineDefinition
    {
        /// <summary>Declares the machine.</summary>
        public ChemicalAssembler() : base("demo-chemical-assembler", Vanilla.AssemblingMachine1)
        {
            Icon = Asset.Mod("graphics/icons/chemical-assembler.png");
            IconSize = 64;
            Subgroup = Subgroup.ProductionMachine;
            Order = SortOrder.At("z", "demo-chemical-assembler");

            // A 3x3 machine is three tiles across, and a tile is 32 pixels: the 192-pixel render is
            // drawn at half size, which makes it exactly cover the 96 pixels of the entity.
            Graphics = GraphicsSet.Of(Sprite.Scaled(Asset.Mod("graphics/entity/chemical-assembler.png"), 192, 192, 0.5));

            CraftingCategories = [CraftingCategory.Chemistry];
            CraftingSpeed = 1.25;
            EnergyUsage = Energy.Kilowatts(250);

            MinableResult = ItemRef.Of("demo-chemical-assembler");

            // The vanilla machine upgrades to assembling-machine-2; this one is not part of that
            // chain, so the inherited upgrade path is cleared. Writing null is how a definition
            // says "not this field" when it inherits.
            NextUpgrade = null;

            FluidBoxes =
            [
                // Unfiltered, so the machine can run both of its recipes: water arrives through it
                // for the coolant, coolant arrives through it for the resin. A filter here would
                // accept one fluid and leave the other recipe in the window unable to run.
                FluidBox.Input(
                    filter: null,
                    volume: 100,
                    ports: [PipePort.North(0, -1)]),

                // Filtered, because the only fluid this machine makes is the coolant.
                FluidBox.Output(
                    filter: FluidRef.Of("demo-coolant"),
                    volume: 100,
                    ports: [PipePort.South(0, 1)]),
            ];

            LocalisedName = "Chemical assembler";
            LocalisedDescription = "Runs chemistry recipes, and pipes the coolant itself.";
        }
    }
}
