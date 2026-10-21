using System;

namespace Factorio.Data
{
    /// <summary>
    /// A prototype that already exists - one of the base game's, or another mod's.
    /// <para>
    /// A definition can be built on top of one instead of starting from nothing:
    /// <c>new AssemblingMachineDefinition("demo-assembler", Vanilla.AssemblingMachine1)</c> keeps
    /// working. Every field the definition does not set is copied from the base prototype when the
    /// data stage runs, so graphics, sounds and the energy source come along unchanged.
    /// </para>
    /// </summary>
    public sealed class VanillaPrototype : ILuaValue
    {
        /// <summary>References an existing prototype.</summary>
        /// <param name="type">Prototype type, e.g. <c>assembling-machine</c>.</param>
        /// <param name="name">Prototype name, e.g. <c>assembling-machine-1</c>.</param>
        public VanillaPrototype(string type, string name)
        {
            Type = type;
            Name = name;
        }

        /// <summary>The prototype type.</summary>
        public string Type { get; }

        /// <summary>The prototype name.</summary>
        public string Name { get; }

        /// <summary>Writes <c>{ type = ..., name = ... }</c>, which is what the data stage copies from.</summary>
        /// <param name="writer">The writer.</param>
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
    /// The base game's prototypes that a mod commonly builds on.
    /// <para>
    /// These are names, not data: the fields are copied from the game's own prototype when the data
    /// stage runs, which is why the build cannot check them - it does not ship a copy of the game's
    /// prototypes.
    /// </para>
    /// </summary>
    public static class Vanilla
    {
        /// <summary>The first tier assembling machine.</summary>
        public static VanillaPrototype AssemblingMachine1 { get; } = new("assembling-machine", "assembling-machine-1");

        /// <summary>The second tier assembling machine.</summary>
        public static VanillaPrototype AssemblingMachine2 { get; } = new("assembling-machine", "assembling-machine-2");

        /// <summary>The third tier assembling machine.</summary>
        public static VanillaPrototype AssemblingMachine3 { get; } = new("assembling-machine", "assembling-machine-3");

        /// <summary>The chemical plant.</summary>
        public static VanillaPrototype ChemicalPlant { get; } = new("assembling-machine", "chemical-plant");

        /// <summary>The oil refinery.</summary>
        public static VanillaPrototype OilRefinery { get; } = new("assembling-machine", "oil-refinery");

        /// <summary>The stone furnace.</summary>
        public static VanillaPrototype StoneFurnace { get; } = new("furnace", "stone-furnace");

        /// <summary>The steel furnace.</summary>
        public static VanillaPrototype SteelFurnace { get; } = new("furnace", "steel-furnace");

        /// <summary>The electric mining drill.</summary>
        public static VanillaPrototype ElectricMiningDrill { get; } = new("mining-drill", "electric-mining-drill");

        /// <summary>The base game's water.</summary>
        public static VanillaPrototype Water { get; } = new("fluid", "water");

        /// <summary>The base game's crude oil.</summary>
        public static VanillaPrototype CrudeOil { get; } = new("fluid", "crude-oil");

        /// <summary>The base game's petroleum gas.</summary>
        public static VanillaPrototype PetroleumGas { get; } = new("fluid", "petroleum-gas");

        /// <summary>The base game's lubricant.</summary>
        public static VanillaPrototype Lubricant { get; } = new("fluid", "lubricant");

        /// <summary>The base game's sulfuric acid.</summary>
        public static VanillaPrototype SulfuricAcid { get; } = new("fluid", "sulfuric-acid");

        /// <summary>The base game's steam.</summary>
        public static VanillaPrototype Steam { get; } = new("fluid", "steam");

        /// <summary>Any fluid by name, for the one this API does not list.</summary>
        /// <param name="name">Fluid prototype name.</param>
        /// <returns>The reference.</returns>
        public static VanillaPrototype FluidOf(string name) => new("fluid", name);

        /// <summary>The storage tank.</summary>
        public static VanillaPrototype StorageTank { get; } = new("storage-tank", "storage-tank");

        /// <summary>Any other prototype, by type and name.</summary>
        /// <param name="type">Prototype type, e.g. <c>assembling-machine</c>.</param>
        /// <param name="name">Prototype name.</param>
        /// <returns>The reference.</returns>
        public static VanillaPrototype Of(string type, string name) => new(type, name);
    }
}
