using System;

namespace Factorio.Data
{
    /// <summary>
    /// Where a pipe connects to a fluid box, as an offset from the entity's centre.
    /// <para>
    /// The offsets are in tiles and must lie on the entity's edge or inside its collision box:
    /// Factorio refuses to load a mod whose pipe connection is outside the entity. A 2.4x2.4
    /// machine, for example, takes <c>PipePort.North(0, -1)</c> and <c>PipePort.South(0, 1)</c>.
    /// </para>
    /// </summary>
    public sealed class PipePort : ILuaValue
    {
        private readonly string _direction;
        private readonly double _x;
        private readonly double _y;

        private PipePort(string direction, double x, double y)
        {
            _direction = direction;
            _x = x;
            _y = y;
        }

        /// <summary>A connection on the north edge.</summary>
        /// <param name="x">Horizontal offset; 0 is the centre.</param>
        /// <param name="y">Vertical offset; negative is north.</param>
        /// <returns>The pipe connection.</returns>
        public static PipePort North(double x, double y) => new("north", x, y);

        /// <summary>A connection on the south edge.</summary>
        /// <param name="x">Horizontal offset; 0 is the centre.</param>
        /// <param name="y">Vertical offset; positive is south.</param>
        /// <returns>The pipe connection.</returns>
        public static PipePort South(double x, double y) => new("south", x, y);

        /// <summary>A connection on the west edge.</summary>
        /// <param name="x">Horizontal offset; negative is west.</param>
        /// <param name="y">Vertical offset; 0 is the centre.</param>
        /// <returns>The pipe connection.</returns>
        public static PipePort West(double x, double y) => new("west", x, y);

        /// <summary>A connection on the east edge.</summary>
        /// <param name="x">Horizontal offset; positive is east.</param>
        /// <param name="y">Vertical offset; 0 is the centre.</param>
        /// <returns>The pipe connection.</returns>
        public static PipePort East(double x, double y) => new("east", x, y);

        /// <summary>Writes <c>{ direction = defines.direction.north, position = { x, y } }</c>.</summary>
        /// <param name="writer">The writer.</param>
        public void Write(LuaWriter writer)
        {
            writer.BeginTable();
            writer.Key("direction");
            // `defines` exists in the data stage, so the game's own constant is used rather than a
            // number copied out of the API documents.
            writer.Raw("defines.direction." + _direction);
            writer.Key("position");
            writer.BeginTable();
            writer.Key("x");
            writer.Value(_x);
            writer.Key("y");
            writer.Value(_y);
            writer.EndTable();
            writer.EndTable();
        }
    }
}
