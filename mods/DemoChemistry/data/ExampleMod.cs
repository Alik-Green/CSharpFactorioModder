using Factorio.Data;

namespace DemoChemistry
{
    /// <summary>
    /// The mod's prototypes, in C#.
    /// <para>
    /// This file is evaluated by <c>factorio-mod build</c>, not by the game: each definition below
    /// becomes a prototype table in the generated <c>data.lua</c>. It is ordinary .NET code, so
    /// loops, LINQ, helper methods and interfaces all work here - the restrictions that apply to
    /// <c>src/</c> (which becomes Lua) do not apply to this file.
    /// </para>
    /// </summary>
    public sealed class ExampleMod : ModDefinition
    {
        /// <summary>Adds every prototype of this mod, in the order they should appear in data.lua.</summary>
        /// <param name="mod">The builder collecting the definitions.</param>
        public override void Define(ModBuilder mod)
        {
            // The fluid first: everything else refers to it, and its own recipe makes it reachable
            // without cheating.
            mod.Add(new DemoCoolant());
            mod.Add(new CoolantRecipe());

            mod.Add(new PolymerResin());
            mod.Add(new PolymerPellet());
            mod.Add(new ChemicalAssemblerItem());
            mod.Add(new ChemicalAssembler());

            mod.Add(new PolymerResinRecipe());
            mod.Add(new PolymerPelletRecipe());
            mod.Add(new ChemicalAssemblerRecipe());
        }
    }
}
