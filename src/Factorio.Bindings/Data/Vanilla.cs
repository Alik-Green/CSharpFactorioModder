namespace Factorio.Data
{
    public sealed partial class VanillaPrototype : ILuaValue
    {
        /// <summary>Recipe shorthand: <c>Vanilla.Machines.Pump * 2</c> or <c>Vanilla.Water * 50</c>.</summary>
        public static RecipeValue operator *(VanillaPrototype prototype, double amount) =>
            prototype.Type == "fluid"
                ? RecipeValue.Fluid(prototype.Name, amount)
                : RecipeValue.Item(prototype.Name, amount);

        /// <summary>Recipe shorthand: <c>2 * Vanilla.Machines.Pump</c>.</summary>
        public static RecipeValue operator *(double amount, VanillaPrototype prototype) =>
            prototype * amount;

        /// <summary>Writes <c>{ type = ..., name = ... }</c>, which is what the data stage copies from.</summary>
        public void Write(LuaWriter writer)
        {
            writer.BeginTable();
            writer.Key("type");
            writer.Value(Type);
            writer.Key("name");
            writer.Value(Name);
            writer.EndTable();
        }
    }

    /// <summary>
    /// Complete, strongly-typed catalog of Factorio 2.0 prototypes &amp; constants:
    /// <list type="bullet">
    /// <item><description><see cref="Items"/> — all vanilla items, science packs, circuits, barrels, &amp; player aliases (<c>Vanilla.Items.ElectronicCircuit * 5</c>)</description></item>
    /// <item><description><see cref="Fluids"/> — all vanilla fluids (<c>Vanilla.Fluids.Water * 50</c>)</description></item>
    /// <item><description><see cref="Machines"/> — all placeable machines, furnaces, drills, tanks, belts, inserters, &amp; turrets</description></item>
    /// <item><description><see cref="Entities"/> — all ~480+ vanilla world entities, ores, &amp; remnants</description></item>
    /// <item><description><see cref="Recipes"/> — all vanilla recipes</description></item>
    /// <item><description><see cref="Technologies"/> — all vanilla research technologies for <c>Prerequisites = [...]</c></description></item>
    /// <item><description><see cref="Icons"/> — exact <c>__base__/graphics/...</c> icons for instant prototyping</description></item>
    /// <item><description><see cref="SpaceAge"/> — full Factorio: Space Age DLC catalog</description></item>
    /// </list>
    /// </summary>
    public static partial class Vanilla
    {
        /// <summary>The first tier assembling machine (<c>"assembling-machine-1"</c>).</summary>
        public static VanillaPrototype AssemblingMachine1 => Machines.AssemblingMachine1;

        /// <summary>The second tier assembling machine (<c>"assembling-machine-2"</c>).</summary>
        public static VanillaPrototype AssemblingMachine2 => Machines.AssemblingMachine2;

        /// <summary>The third tier assembling machine (<c>"assembling-machine-3"</c>).</summary>
        public static VanillaPrototype AssemblingMachine3 => Machines.AssemblingMachine3;

        /// <summary>The chemical plant (<c>"chemical-plant"</c>).</summary>
        public static VanillaPrototype ChemicalPlant => Machines.ChemicalPlant;

        /// <summary>The oil refinery (<c>"oil-refinery"</c>).</summary>
        public static VanillaPrototype OilRefinery => Machines.OilRefinery;

        /// <summary>The centrifuge (<c>"centrifuge"</c>).</summary>
        public static VanillaPrototype Centrifuge => Machines.Centrifuge;

        /// <summary>The stone furnace (<c>"stone-furnace"</c>).</summary>
        public static VanillaPrototype StoneFurnace => Machines.StoneFurnace;

        /// <summary>The steel furnace (<c>"steel-furnace"</c>).</summary>
        public static VanillaPrototype SteelFurnace => Machines.SteelFurnace;

        /// <summary>The electric furnace (<c>"electric-furnace"</c>).</summary>
        public static VanillaPrototype ElectricFurnace => Machines.ElectricFurnace;

        /// <summary>The electric mining drill (<c>"electric-mining-drill"</c>).</summary>
        public static VanillaPrototype ElectricMiningDrill => Machines.ElectricMiningDrill;

        /// <summary>The storage tank (<c>"storage-tank"</c>).</summary>
        public static VanillaPrototype StorageTank => Machines.StorageTank;

        /// <summary>The base game's water (<c>"water"</c>).</summary>
        public static VanillaPrototype Water { get; } = new("fluid", "water");

        /// <summary>The base game's crude oil (<c>"crude-oil"</c>).</summary>
        public static VanillaPrototype CrudeOil { get; } = new("fluid", "crude-oil");

        /// <summary>The base game's petroleum gas (<c>"petroleum-gas"</c>).</summary>
        public static VanillaPrototype PetroleumGas { get; } = new("fluid", "petroleum-gas");

        /// <summary>The base game's lubricant (<c>"lubricant"</c>).</summary>
        public static VanillaPrototype Lubricant { get; } = new("fluid", "lubricant");

        /// <summary>The base game's sulfuric acid (<c>"sulfuric-acid"</c>).</summary>
        public static VanillaPrototype SulfuricAcid { get; } = new("fluid", "sulfuric-acid");

        /// <summary>The base game's steam (<c>"steam"</c>).</summary>
        public static VanillaPrototype Steam { get; } = new("fluid", "steam");

        /// <summary>Any fluid by name, e.g. from another mod.</summary>
        public static VanillaPrototype FluidOf(string name) => new("fluid", name);

        /// <summary>Any other prototype, by type and name.</summary>
        public static VanillaPrototype Of(string type, string name) => new(type, name);
    }
}
