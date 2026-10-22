using System;

namespace Factorio.Data
{
    /// <summary>
    /// One picture: the <c>filename</c>, its pixel size, and how big it is drawn in-game.
    /// <para>
    /// In Factorio, <b>1 tile = 32 pixels</b>:
    /// </para>
    /// <list type="bullet">
    /// <item><description>Use <see cref="Auto(string, double)"/> or <see cref="Auto(string, double, double)"/> to read the PNG's real dimensions automatically and scale it to fit a machine's tile footprint (e.g. <c>Sprite.Auto(Asset.Mod("graphics/entity/machine.png"), tiles: 3)</c>).</description></item>
    /// <item><description>Even if you use <see cref="Scaled"/> or <see cref="File"/>, if the PNG inside <c>resources/</c> is smaller than the crop rectangle you passed, the build automatically fits to the PNG's actual pixel dimensions so Factorio never fails with <em>sprite rectangle is outside the actual sprite size</em>.</description></item>
    /// </list>
    /// </summary>
    public sealed class Sprite : ILuaValue
    {
        /// <summary>Pixels per world tile in Factorio's standard resolution coordinate space.</summary>
        public const double PixelsPerTile = 32.0;

        private readonly string _filename;
        private readonly double _width;
        private readonly double _height;
        private readonly double _scale;
        private readonly double _shiftX;
        private readonly double _shiftY;
        private readonly bool _autoMeasure;
        private readonly double _targetTilesW;
        private readonly double _targetTilesH;

        /// <summary>
        /// Creates a sprite that automatically reads the PNG's pixel dimensions from <c>resources/</c>
        /// and scales it uniformly to fit a <paramref name="tiles"/> × <paramref name="tiles"/> footprint.
        /// </summary>
        /// <param name="filename">Path to the PNG, e.g. <c>Asset.Mod("graphics/entity/machine.png")</c>.</param>
        /// <param name="tiles">Square entity size in tiles (default <c>3</c> for a 3×3 machine).</param>
        public Sprite(string filename, double tiles = 3.0)
            : this(filename, 0, 0, 1, 0, 0, autoMeasure: true, tiles, tiles)
        {
        }

        /// <summary>
        /// Creates a sprite that automatically reads the PNG's pixel dimensions from <c>resources/</c>
        /// and scales it uniformly to fit <paramref name="tilesWide"/> × <paramref name="tilesHigh"/> tiles.
        /// </summary>
        /// <param name="filename">Path to the PNG, e.g. <c>Asset.Mod("graphics/entity/machine.png")</c>.</param>
        /// <param name="tilesWide">Target width in tiles.</param>
        /// <param name="tilesHigh">Target height in tiles.</param>
        public Sprite(string filename, double tilesWide, double tilesHigh)
            : this(filename, 0, 0, 1, 0, 0, autoMeasure: true, tilesWide, tilesHigh)
        {
        }

        private Sprite(
            string filename,
            double width,
            double height,
            double scale,
            double shiftX,
            double shiftY,
            bool autoMeasure = false,
            double targetTilesW = 0,
            double targetTilesH = 0)
        {
            _filename = filename;
            _width = width;
            _height = height;
            _scale = scale;
            _shiftX = shiftX;
            _shiftY = shiftY;
            _autoMeasure = autoMeasure;
            _targetTilesW = targetTilesW;
            _targetTilesH = targetTilesH;
        }

        /// <summary>
        /// Reads the PNG's real pixel dimensions from <c>resources/</c> at build time and scales it
        /// automatically to fit a square <paramref name="tiles"/> × <paramref name="tiles"/> entity
        /// (preserving aspect ratio).
        /// </summary>
        /// <param name="filename">Path to the PNG, e.g. <c>Asset.Mod("graphics/entity/condenser.png")</c>.</param>
        /// <param name="tiles">Entity width/height in tiles (e.g. <c>3</c> for a 3×3 machine).</param>
        public static Sprite Auto(string filename, double tiles = 3.0) =>
            new(filename, tiles, tiles);

        /// <summary>
        /// Reads the PNG's real pixel dimensions from <c>resources/</c> at build time and scales it
        /// automatically to fit a <paramref name="tilesWide"/> × <paramref name="tilesHigh"/> entity
        /// (preserving aspect ratio).
        /// </summary>
        /// <param name="filename">Path to the PNG, e.g. <c>Asset.Mod("graphics/entity/condenser.png")</c>.</param>
        /// <param name="tilesWide">Target width in tiles (e.g. <c>2</c>).</param>
        /// <param name="tilesHigh">Target height in tiles (e.g. <c>3</c>).</param>
        public static Sprite Auto(string filename, double tilesWide, double tilesHigh) =>
            new(filename, tilesWide, tilesHigh);

        /// <summary>A picture drawn at its natural pixel size (<c>scale = 1</c>).</summary>
        /// <param name="filename">Path to the PNG, e.g. <c>Asset.Mod("graphics/entity/pump.png")</c>.</param>
        /// <param name="width">Width in pixels.</param>
        /// <param name="height">Height in pixels.</param>
        public static Sprite File(string filename, double width, double height) =>
            new(filename, width, height, 1, 0, 0);

        /// <summary>
        /// A picture drawn with an explicit pixel box and <paramref name="scale"/>. If the actual PNG on
        /// disk is smaller than <paramref name="width"/> × <paramref name="height"/>, the build automatically
        /// clamps to the PNG's true size and adjusts <c>scale</c> so the rendered size stays the same.
        /// </summary>
        /// <param name="filename">Path to the PNG.</param>
        /// <param name="width">Width in pixels.</param>
        /// <param name="height">Height in pixels.</param>
        /// <param name="scale">Scale to draw at; 0.5 draws at half size.</param>
        public static Sprite Scaled(string filename, double width, double height, double scale) =>
            new(filename, width, height, scale, 0, 0);

        /// <summary>The same sprite, moved by a number of tiles relative to entity centre.</summary>
        /// <param name="x">Horizontal shift in tiles; positive is east.</param>
        /// <param name="y">Vertical shift in tiles; positive is south.</param>
        public Sprite Shift(double x, double y) =>
            new(_filename, _width, _height, _scale, x, y, _autoMeasure, _targetTilesW, _targetTilesH);

        /// <summary>Writes the sprite table, resolving automatic PNG measurements when available.</summary>
        public void Write(LuaWriter writer)
        {
            var width = _width;
            var height = _height;
            var scale = _scale;

            if (_autoMeasure)
            {
                if (!Asset.TryReadModPngSize(_filename, out var actualW, out var actualH))
                {
                    throw new InvalidOperationException(
                        $"Sprite.Auto could not find or read PNG file for '{_filename}'. " +
                        "Place the PNG inside your mod's resources/ directory (e.g. resources/graphics/...), " +
                        "or pass explicit dimensions with Sprite.Scaled(path, width, height, scale).");
                }

                width = actualW;
                height = actualH;
                var desiredPxW = Math.Max(1.0, _targetTilesW * PixelsPerTile);
                var desiredPxH = Math.Max(1.0, _targetTilesH * PixelsPerTile);
                scale = Math.Round(Math.Min(desiredPxW / actualW, desiredPxH / actualH), 4);
            }
            else if (Asset.TryReadModPngSize(_filename, out var actualW, out var actualH) &&
                     (width > actualW || height > actualH))
            {
                // The user passed larger dimensions than the actual PNG file on disk (which always
                // crashes Factorio with "The given sprite rectangle ... is outside the actual sprite size").
                // Preserve their requested on-screen size (width * scale × height * scale) automatically!
                var desiredPxW = width * scale;
                var desiredPxH = height * scale;
                width = actualW;
                height = actualH;
                scale = Math.Round(Math.Min(desiredPxW / actualW, desiredPxH / actualH), 4);
            }

            writer.BeginTable();
            writer.Key("filename");
            writer.Value(_filename);
            writer.Key("width");
            writer.Value(width);
            writer.Key("height");
            writer.Value(height);
            if (scale != 1)
            {
                writer.Key("scale");
                writer.Value(scale);
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
    /// A crafting machine's <c>graphics_set</c>: what the machine looks like on the map.
    /// <code>
    /// // Automatic PNG measurement &amp; scaling to a 3x3 machine:
    /// Graphics = GraphicsSet.Auto(Asset.Mod("graphics/entity/atmospheric-condenser.png"), tiles: 3);
    ///
    /// // Or rectangular 2x3 machine:
    /// Graphics = GraphicsSet.Auto(Asset.Mod("graphics/entity/condenser.png"), tilesWide: 2, tilesHigh: 3);
    /// </code>
    /// </summary>
    public sealed class GraphicsSet : ILuaValue
    {
        private readonly Sprite[] _layers;

        /// <summary>Creates a <see cref="GraphicsSet"/> from a single sprite.</summary>
        public GraphicsSet(Sprite sprite)
        {
            _layers = new[] { sprite };
        }

        private GraphicsSet(Sprite[] layers)
        {
            _layers = layers;
        }

        /// <summary>Allows assigning a <see cref="Sprite"/> directly to <c>Graphics = Sprite.Auto(...)</c>.</summary>
        public static implicit operator GraphicsSet?(Sprite? sprite) =>
            sprite == null ? null : new GraphicsSet(sprite);

        /// <summary>
        /// Automatically measures the PNG inside <c>resources/</c> and scales it uniformly to fit a
        /// <paramref name="tiles"/> × <paramref name="tiles"/> machine footprint.
        /// </summary>
        public static GraphicsSet Auto(string filename, double tiles = 3.0) =>
            new(Sprite.Auto(filename, tiles));

        /// <summary>
        /// Automatically measures the PNG inside <c>resources/</c> and scales it uniformly to fit a
        /// <paramref name="tilesWide"/> × <paramref name="tilesHigh"/> machine footprint.
        /// </summary>
        public static GraphicsSet Auto(string filename, double tilesWide, double tilesHigh) =>
            new(Sprite.Auto(filename, tilesWide, tilesHigh));

        /// <summary>A machine drawn from one picture in all directions.</summary>
        public static GraphicsSet Of(Sprite sprite) => new(sprite);

        /// <summary>
        /// A machine drawn from one PNG file, automatically measured and scaled to <paramref name="tiles"/> × <paramref name="tiles"/> tiles.
        /// </summary>
        public static GraphicsSet Of(string filename, double tiles = 3.0) =>
            Auto(filename, tiles);

        /// <summary>A machine drawn from one file with explicit pixel size and scale.</summary>
        public static GraphicsSet Of(string filename, double width, double height, double scale) =>
            new(Sprite.Scaled(filename, width, height, scale));

        /// <summary>A machine drawn from four directional layers (north, east, south, west) or stacked layers.</summary>
        public static GraphicsSet Of(Sprite north, Sprite east, Sprite south, Sprite west) =>
            new(new[] { north, east, south, west });

        /// <summary>A machine drawn from multiple stacked sprite layers (e.g. base sprite + shadow).</summary>
        public static GraphicsSet Layers(params Sprite[] layers) => new(layers);

        /// <inheritdoc />
        public void Write(LuaWriter writer)
        {
            writer.BeginTable();
            writer.Key("animation");
            writer.BeginTable();
            writer.Key("layers");
            writer.BeginArray();
            foreach (var sprite in _layers) writer.Value(sprite);
            writer.EndArray();
            writer.EndTable();
            writer.EndTable();
        }
    }
}
