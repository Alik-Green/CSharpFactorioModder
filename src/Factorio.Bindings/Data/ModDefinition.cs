using System;

namespace Factorio.Data
{
    /// <summary>
    /// A mod's prototypes, written in C#.
    /// <para>
    /// A data-stage mod declares one class deriving from <see cref="ModDefinition"/> and adds its
    /// definitions to the builder it is given. The toolchain runs that method at build time and
    /// writes the resulting prototype tables to <c>data.lua</c>, so the game loads ordinary data -
    /// no C# and no runtime library in the data stage at all.
    /// </para>
    /// <code>
    /// public sealed class ExampleMod : ModDefinition
    /// {
    ///     public override void Define(ModBuilder mod)
    ///     {
    ///         mod.Add(new DemoCoolant());
    ///         mod.Add(new DemoPellet());
    ///         mod.Add(new DemoRecipe());
    ///     }
    /// }
    /// </code>
    /// <para>
    /// The class named by <c>dataEntry</c> in <c>mod.json</c> is the one that gets called; it must
    /// have a public parameterless constructor.
    /// </para>
    /// </summary>
    public abstract class ModDefinition
    {
        /// <summary>
        /// Adds this mod's prototypes. Runs once per build, in a data-stage environment, so
        /// <see cref="Values"/>, <see cref="Vanilla"/> and the other helpers below are available.
        /// </summary>
        /// <param name="mod">Collects the definitions, in the order they are added.</param>
        public abstract void Define(ModBuilder mod);
    }
}
