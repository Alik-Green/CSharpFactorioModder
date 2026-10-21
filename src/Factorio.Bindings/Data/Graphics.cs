using System;

namespace Factorio.Data
{
    /// <summary>
    /// One picture: the <c>filename</c>, its pixel size, and how big it is drawn.
    /// <para>
    /// Every graphic in a prototype - a machine's animation, an icon under a slot - is some form of
    /// this table. <see cref="Scale"/> is what makes a large render fit a machine: a 192-pixel image
    /// of a 3x3 machine with <c>scale = 0.5</c> covers three tiles and no more.
    /// </para>
    /// </summary>
    public sealed class Sprite : ILuaValue
    {
        private readonly string _filename;
        private readonly double _width;
        private readonly double _height;
        private readonly double _scale;
        private readonly double _shiftX;
        private readonly double _shiftY;

        private Sprite(string filename, double width, double height, double scale, double shiftX, double shiftY)
        {
            _filename = filename;
            _width = width;
            _height = height;
            _scale = scale;
            _shiftX = shiftX;
            _shiftY = shiftY;
        }

        /// <summary>A picture drawn at its natural size.</summary>
        /// <param name="filename">Path to the PNG, e.g. <c>Asset.Mod("graphics/entity/pump.png")</c>.</param>
        /// <param name="width">Width in pixels.</param>
        /// <param name="height">Height in pixels.</param>
        /// <returns>The sprite.</returns>
        public static Sprite File(string filename, double width, double height) =>
            new(filename, width, height, 1, 0, 0);

        /// <summary>
        /// A picture drawn smaller or larger than its pixels. A 3x3 entity is three tiles wide, and a
        /// tile is 32 pixels, so its sprite wants to be 96 pixels across: a 192-pixel render takes
        /// <paramref name="scale"/> 0.5.
        /// </summary>
        /// <param name="filename">Path to the PNG.</param>
        /// <param name="width">Width in pixels.</param>
        /// <param name="height">Height in pixels.</param>
        /// <param name="scale">Scale to draw at; 0.5 draws at half size.</param>
        /// <returns>The sprite.</returns>
        public static Sprite Scaled(string filename, double width, double height, double scale) =>
            new(filename, width, height, scale, 0, 0);

        /// <summary>The same sprite, moved by a number of tiles.</summary>
        /// <param name="x">Horizontal shift in tiles; positive is east.</param>
        /// <param name="y">Vertical shift in tiles; positive is south.</param>
        /// <returns>A shifted copy.</returns>
        public Sprite Shift(double x, double y) => new(_filename, _width, _height, _scale, x, y);

        /// <summary>Writes the sprite table.</summary>
        /// <param name="writer">The writer.</param>
        public void Write(LuaWriter writer)
        {
            writer.BeginTable();
            writer.Key("filename");
            writer.Value(_filename);
            writer.Key("width");
            writer.Value(_width);
            writer.Key("height");
            writer.Value(_height);
            if (_scale != 1)
            {
                writer.Key("scale");
                writer.Value(_scale);
            }
            if (_shiftX != 0 || _shiftY != 0)
            {
                writer.Key("shift");
                writer.BeginArray();
                writer.Value(_shiftX);
                writer.Value(_shiftY);
                writer.EndArray();
            }
            writer.EndTable();
        }
    }

    /// <summary>
    /// A machine's <c>graphics_set</c>: what it looks like while it stands there.
    /// <para>
    /// A crafting machine with one picture needs no more than <see cref="Of"/>. A mod that has the
    /// full four-direction sheet can pass the same sprite and let the game rotate it, or write the
    /// table by hand with <see cref="AssemblingMachineDefinition.Field"/>.
    /// </para>
    /// </summary>
    public static class GraphicsSet
    {
        /// <summary>
        /// A machine drawn from one picture, used in every direction. The picture is centred on the
        /// entity, so a sprite of the entity's own size sits exactly on it.
        /// </summary>
        /// <param name="sprite">The picture.</param>
        /// <returns>The <c>graphics_set</c> table.</returns>
        public static ILuaValue Of(Sprite sprite) => new SinglePicture(sprite);

        /// <summary>
        /// A machine drawn from one file, written with its size and scale:
        /// <c>GraphicsSet.Of(Asset.Mod("graphics/entity/demo.png"), 192, 192, 0.5)</c> - three tiles
        /// across, which is what a 3x3 machine is.
        /// </summary>
        /// <param name="filename">Path to the PNG.</param>
        /// <param name="width">Width in pixels.</param>
        /// <param name="height">Height in pixels.</param>
        /// <param name="scale">Scale to draw at; 0.5 for a 192-pixel render on a 3-tile entity.</param>
        /// <returns>The <c>graphics_set</c> table.</returns>
        public static ILuaValue Of(string filename, double width, double height, double scale) =>
            new SinglePicture(Sprite.Scaled(filename, width, height, scale));

        /// <summary>
        /// A machine drawn from several pictures, one per direction, in the order Factorio uses:
        /// north, east, south, west.
        /// </summary>
        /// <param name="north">The north-facing picture.</param>
        /// <param name="east">The east-facing picture.</param>
        /// <param name="south">The south-facing picture.</param>
        /// <param name="west">The west-facing picture.</param>
        /// <returns>The <c>graphics_set</c> table.</returns>
        public static ILuaValue Of(Sprite north, Sprite east, Sprite south, Sprite west) =>
            new FourPictures(north, east, south, west);

        /// <summary>A graphics set built from a single animation.</summary>
        private sealed class SinglePicture : ILuaValue
        {
            private readonly Sprite _sprite;

            public SinglePicture(Sprite sprite) => _sprite = sprite;

            public void Write(LuaWriter writer)
            {
                writer.BeginTable();
                writer.Key("animation");
                Layers(writer, _sprite);
                writer.EndTable();
            }
        }

        /// <summary>A graphics set built from one animation per direction.</summary>
        private sealed class FourPictures : ILuaValue
        {
            private readonly Sprite[] _sprites;

            public FourPictures(params Sprite[] sprites) => _sprites = sprites;

            public void Write(LuaWriter writer)
            {
                writer.BeginTable();
                writer.Key("animation");
                writer.BeginTable();
                writer.Key("layers");
                writer.BeginArray();
                foreach (var sprite in _sprites) writer.Value(sprite);
                writer.EndArray();
                writer.EndTable();
                writer.EndTable();
            }
        }

        /// <summary>Writes <c>{ layers = { sprite } }</c>.</summary>
        private static void Layers(LuaWriter writer, Sprite sprite)
        {
            writer.BeginTable();
            writer.Key("layers");
            writer.BeginArray();
            writer.Value(sprite);
            writer.EndArray();
            writer.EndTable();
        }
    }
}
