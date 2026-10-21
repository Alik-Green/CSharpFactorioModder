using System;

namespace Factorio.Data
{
    /// <summary>
    /// A colour, as the game stores it: <c>{ r = 0.08, g = 0.40, b = 0.85 }</c>.
    /// <para>
    /// Used by <see cref="FluidDefinition.BaseColor"/>, <see cref="FluidDefinition.FlowColor"/> and
    /// anywhere else a prototype takes a colour. Components are 0 to 1.
    /// </para>
    /// </summary>
    public sealed class Rgb : ILuaValue
    {
        /// <summary>Creates a colour.</summary>
        /// <param name="red">Red component, 0 to 1.</param>
        /// <param name="green">Green component, 0 to 1.</param>
        /// <param name="blue">Blue component, 0 to 1.</param>
        public Rgb(double red, double green, double blue)
        {
            Red = red;
            Green = green;
            Blue = blue;
        }

        /// <summary>Red component.</summary>
        public double Red { get; }

        /// <summary>Green component.</summary>
        public double Green { get; }

        /// <summary>Blue component.</summary>
        public double Blue { get; }

        /// <summary>Writes <c>{ r = ..., g = ..., b = ... }</c>.</summary>
        /// <param name="writer">The writer.</param>
        public void Write(LuaWriter writer)
        {
            writer.BeginTable();
            writer.Key("r");
            writer.Value(Red);
            writer.Key("g");
            writer.Value(Green);
            writer.Key("b");
            writer.Value(Blue);
            writer.EndTable();
        }
    }
}
