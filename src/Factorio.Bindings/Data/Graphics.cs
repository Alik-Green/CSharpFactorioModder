using System;
using System.Linq;

namespace Factorio.Data
{
    /// <summary>
    /// Controls how <see cref="Sprite.Auto(string, double, double, double, SpriteAlign)"/> and
    /// <see cref="GraphicsSet.Auto(string, double, double, double, SpriteAlign)"/> scale and align
    /// a PNG image inside an entity's grid hitbox.
    /// </summary>
    public enum SpriteAlign
    {
        /// <summary>
        /// <b>Default (Factorio 45° building standard):</b> Automatically trims transparent borders in
        /// the PNG, centers visible pixels horizontally on the grid, scales width to match the tile
        /// footprint cleanly, and anchors tall buildings' bases flush with the bottom of the tile box
        /// so roofs/chimneys extend upward (<c>-Y</c>) naturally just like vanilla Factorio machines!
        /// </summary>
        Footprint = 0,

        /// <summary>
        /// Trims transparent borders in the PNG, fits the entire visible sprite inside the
        /// <c>tilesWide × tilesHigh</c> box, and centers both X and Y on <c>(0, 0)</c>.
        /// </summary>
        Center = 1,

        /// <summary>
        /// Trims transparent borders in the PNG, fits inside <c>tilesWide × tilesHigh</c>, centers X on
        /// <c>0</c>, and places the visible bottom edge flush on the bottom of the selection box.
        /// </summary>
        Bottom = 2,

        /// <summary>
        /// Uses the raw full PNG canvas dimensions with zero alpha-channel border trimming (for sprites
        /// that already baked exact Factorio canvas offsets).
        /// </summary>
        Canvas = 3,
    }

    /// <summary>
    /// One picture: the <c>filename</c>, its pixel size, scale, and world/grid alignment offset.
    /// <para>
    /// In Factorio, <b>1 tile = 32 pixels</b>, <c>+X</c> moves East (right), <c>-Y</c> moves North (up),
    /// and <c>+Y</c> moves South (down):
    /// </para>
    /// <code>
    /// // Auto-trims transparent PNG margins, fits your grid footprint, &amp; allows fine nudging:
    /// Graphics = GraphicsSet.Auto(Asset.Mod("graphics/entity/machine.png"), tiles: 3)
    ///     .Shift(0, -0.25)       // nudge in tiles (or use .ShiftPixels(0, -8)!)
    ///     .ScaleBy(1.1);         // optional artistic scale tweak while keeping alignment!
    /// </code>
    /// </summary>
    public sealed class Sprite : ILuaValue
    {
        /// <summary>Pixels per world tile in Factorio's standard coordinate space.</summary>
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
        private readonly double _scaleMultiplier;
        private readonly SpriteAlign _align;

        /// <summary>
        /// Creates a sprite that automatically reads the PNG's visible pixel bounds from <c>resources/</c>,
        /// scales it cleanly to a <paramref name="tiles"/> × <paramref name="tiles"/> grid footprint, and
        /// centers/aligns it inside the entity hitbox.
        /// </summary>
        public Sprite(
            string filename,
            double tiles = 3.0,
            double shiftX = 0,
            double shiftY = 0,
            SpriteAlign align = SpriteAlign.Footprint)
            : this(filename, 0, 0, 1, shiftX, shiftY, autoMeasure: true, tiles, tiles, 1.0, align)
        {
        }

        /// <summary>
        /// Creates a sprite that automatically reads the PNG's visible pixel bounds from <c>resources/</c>,
        /// scales it cleanly to a <paramref name="tilesWide"/> × <paramref name="tilesHigh"/> footprint, and
        /// aligns it inside the entity hitbox.
        /// </summary>
        public Sprite(
            string filename,
            double tilesWide,
            double tilesHigh,
            double shiftX = 0,
            double shiftY = 0,
            SpriteAlign align = SpriteAlign.Footprint)
            : this(filename, 0, 0, 1, shiftX, shiftY, autoMeasure: true, tilesWide, tilesHigh, 1.0, align)
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
            double targetTilesH = 0,
            double scaleMultiplier = 1.0,
            SpriteAlign align = SpriteAlign.Footprint)
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
            _scaleMultiplier = scaleMultiplier;
            _align = align;
        }

        /// <summary>
        /// Automatically inspects non-transparent pixels in <paramref name="filename"/>, scales it to fit a
        /// square <paramref name="tiles"/> × <paramref name="tiles"/> footprint, and aligns it cleanly on the grid.
        /// </summary>
        public static Sprite Auto(
            string filename,
            double tiles = 3.0,
            double shiftX = 0,
            double shiftY = 0,
            SpriteAlign align = SpriteAlign.Footprint) =>
            new(filename, tiles, shiftX, shiftY, align);

        /// <summary>
        /// Automatically inspects non-transparent pixels in <paramref name="filename"/>, scales it to fit a
        /// rectangular <paramref name="tilesWide"/> × <paramref name="tilesHigh"/> footprint, and aligns it on the grid.
        /// </summary>
        public static Sprite Auto(
            string filename,
            double tilesWide,
            double tilesHigh,
            double shiftX = 0,
            double shiftY = 0,
            SpriteAlign align = SpriteAlign.Footprint) =>
            new(filename, tilesWide, tilesHigh, shiftX, shiftY, align);

        /// <summary>A picture drawn at its natural pixel size (<c>scale = 1</c>).</summary>
        public static Sprite File(string filename, double width, double height) =>
            new(filename, width, height, 1, 0, 0);

        /// <summary>A picture drawn with an explicit pixel box and <paramref name="scale"/>.</summary>
        public static Sprite Scaled(string filename, double width, double height, double scale) =>
            new(filename, width, height, scale, 0, 0);

        /// <summary>
        /// Offsets this sprite in <b>tiles</b> relative to its aligned grid position:
        /// <c>+x</c> = East (right), <c>-x</c> = West (left), <c>-y</c> = North (up), <c>+y</c> = South (down).
        /// </summary>
        public Sprite Shift(double x, double y) =>
            new(_filename, _width, _height, _scale, _shiftX + x, _shiftY + y, _autoMeasure, _targetTilesW, _targetTilesH, _scaleMultiplier, _align);

        /// <summary>
        /// Offsets this sprite in <b>pixels</b> (<c>32 pixels = 1 tile</c>, identical to Factorio's <c>util.by_pixel(x, y)</c>):
        /// <c>+pixelsX</c> = right, <c>-pixelsY</c> = up, <c>+pixelsY</c> = down.
        /// </summary>
        public Sprite ShiftPixels(double pixelsX, double pixelsY) =>
            Shift(pixelsX / PixelsPerTile, pixelsY / PixelsPerTile);

        /// <summary>
        /// Multiplies the sprite's rendered size by <paramref name="multiplier"/> (e.g. <c>1.15</c> = 15% larger,
        /// <c>0.9</c> = 10% smaller) while keeping grid centering/footprint alignment intact!
        /// </summary>
        public Sprite ScaleBy(double multiplier) =>
            new(_filename, _width, _height, _scale, _shiftX, _shiftY, _autoMeasure, _targetTilesW, _targetTilesH, _scaleMultiplier * multiplier, _align);

        /// <summary>Chooses how transparent borders and vertical anchoring are handled (<see cref="SpriteAlign"/>).</summary>
        public Sprite Align(SpriteAlign align) =>
            new(_filename, _width, _height, _scale, _shiftX, _shiftY, _autoMeasure, _targetTilesW, _targetTilesH, _scaleMultiplier, align);

        /// <summary>Writes the sprite table, resolving automatic PNG content bounds, scale, and grid alignment.</summary>
        public void Write(LuaWriter writer)
        {
            var width = _width;
            var height = _height;
            var scale = _scale * _scaleMultiplier;
            var totalShiftX = _shiftX;
            var totalShiftY = _shiftY;

            if (_autoMeasure)
            {
                if (!Asset.TryReadModPngBounds(_filename, out var bounds))
                {
                    throw new InvalidOperationException(
                        $"Sprite.Auto could not find or read PNG file for '{_filename}'. " +
                        "Place the PNG inside your mod's resources/ directory (e.g. resources/graphics/...), " +
                        "or pass explicit dimensions with Sprite.Scaled(path, width, height, scale).");
                }

                width = bounds.CanvasWidth;
                height = bounds.CanvasHeight;
                var desiredPxW = Math.Max(1.0, _targetTilesW * PixelsPerTile);
                var desiredPxH = Math.Max(1.0, _targetTilesH * PixelsPerTile);

                if (_align == SpriteAlign.Canvas)
                {
                    scale = Math.Round(Math.Min(desiredPxW / bounds.CanvasWidth, desiredPxH / bounds.CanvasHeight) * _scaleMultiplier, 4);
                }
                else
                {
                    var contentW = Math.Max(1.0, bounds.ContentWidth);
                    var contentH = Math.Max(1.0, bounds.ContentHeight);

                    double rawScale = _align switch
                    {
                        // Footprint: if the building is taller than the footprint aspect ratio (standard
                        // Factorio 45° building with roof/tower/chimney), match width cleanly to tilesWide
                        // so the base fills its horizontal grid tiles instead of shrinking!
                        SpriteAlign.Footprint when (contentH / contentW) > (desiredPxH / desiredPxW) =>
                            desiredPxW / contentW,
                        _ => Math.Min(desiredPxW / contentW, desiredPxH / contentH),
                    };

                    scale = Math.Round(rawScale * _scaleMultiplier, 4);

                    // Horizontal offset that places visible content center exactly at X = 0:
                    var autoShiftX = -((bounds.ContentCenterX - bounds.CanvasWidth * 0.5) * scale) / PixelsPerTile;

                    // Vertical offset:
                    // - Bottom: always anchor bottom of visible art to +tilesHigh / 2
                    // - Footprint: if art height exceeds desiredPxH (tall building), anchor base flush to +tilesHigh / 2;
                    //              otherwise center visible art on Y = 0
                    // - Center: always center visible art on Y = 0
                    bool anchorBottom = _align == SpriteAlign.Bottom ||
                        (_align == SpriteAlign.Footprint && contentH * rawScale > desiredPxH + 0.5);

                    var autoShiftY = anchorBottom
                        ? (_targetTilesH * 0.5) - ((bounds.ContentBottom - bounds.CanvasHeight * 0.5) * scale) / PixelsPerTile
                        : -((bounds.ContentCenterY - bounds.CanvasHeight * 0.5) * scale) / PixelsPerTile;

                    totalShiftX = Math.Round(autoShiftX + _shiftX, 4);
                    totalShiftY = Math.Round(autoShiftY + _shiftY, 4);
                }
            }
            else if (Asset.TryReadModPngSize(_filename, out var actualW, out var actualH) &&
                     (width > actualW || height > actualH))
            {
                var desiredPxW = width * scale;
                var desiredPxH = height * scale;
                width = actualW;
                height = actualH;
                scale = Math.Round(Math.Min(desiredPxW / actualW, desiredPxH / actualH), 4);
            }
            else
            {
                scale = Math.Round(scale, 4);
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
            if (Math.Abs(totalShiftX) > 1e-6 || Math.Abs(totalShiftY) > 1e-6)
            {
                writer.Key("shift");
                writer.BeginArray();
                writer.Value(Math.Round(totalShiftX, 4));
                writer.Value(Math.Round(totalShiftY, 4));
                writer.EndArray();
            }
            writer.EndTable();
        }
    }

    /// <summary>
    /// A crafting machine's <c>graphics_set</c>: what the machine looks like on the map.
    /// <code>
    /// // Automatic transparent-margin trimming, grid scaling, &amp; footprint alignment:
    /// Graphics = GraphicsSet.Auto(Asset.Mod("graphics/entity/atmospheric-condenser.png"), tiles: 3);
    ///
    /// // Plus effortless manual fine-tuning in tiles OR pixels:
    /// Graphics = GraphicsSet.Auto(Asset.Mod("graphics/entity/condenser.png"), tilesWide: 2, tilesHigh: 3)
    ///     .Shift(0, -0.25)       // negative Y = up (North), positive X = right (East), in tiles
    ///     .ScaleBy(1.1);         // optional scale multiplier
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
        /// Automatically inspects visible PNG bounds inside <c>resources/</c>, scales cleanly to a
        /// <paramref name="tiles"/> × <paramref name="tiles"/> machine footprint, and aligns it on the grid.
        /// </summary>
        public static GraphicsSet Auto(
            string filename,
            double tiles = 3.0,
            double shiftX = 0,
            double shiftY = 0,
            SpriteAlign align = SpriteAlign.Footprint) =>
            new(Sprite.Auto(filename, tiles, shiftX, shiftY, align));

        /// <summary>
        /// Automatically inspects visible PNG bounds inside <c>resources/</c>, scales cleanly to a
        /// <paramref name="tilesWide"/> × <paramref name="tilesHigh"/> footprint, and aligns it on the grid.
        /// </summary>
        public static GraphicsSet Auto(
            string filename,
            double tilesWide,
            double tilesHigh,
            double shiftX = 0,
            double shiftY = 0,
            SpriteAlign align = SpriteAlign.Footprint) =>
            new(Sprite.Auto(filename, tilesWide, tilesHigh, shiftX, shiftY, align));

        /// <summary>A machine drawn from one picture in all directions.</summary>
        public static GraphicsSet Of(Sprite sprite) => new(sprite);

        /// <summary>A machine drawn from one PNG file, automatically measured and aligned to <paramref name="tiles"/> × <paramref name="tiles"/> tiles.</summary>
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

        /// <summary>
        /// Offsets every layer in this <see cref="GraphicsSet"/> in <b>tiles</b>:
        /// <c>+x</c> = East (right), <c>-x</c> = West (left), <c>-y</c> = North (up), <c>+y</c> = South (down).
        /// </summary>
        public GraphicsSet Shift(double x, double y) =>
            new(_layers.Select(l => l.Shift(x, y)).ToArray());

        /// <summary>
        /// Offsets every layer in this <see cref="GraphicsSet"/> in <b>pixels</b> (<c>32 pixels = 1 tile</c>,
        /// identical to Factorio's <c>util.by_pixel(x, y)</c>).
        /// </summary>
        public GraphicsSet ShiftPixels(double pixelsX, double pixelsY) =>
            new(_layers.Select(l => l.ShiftPixels(pixelsX, pixelsY)).ToArray());

        /// <summary>
        /// Multiplies the scale of every layer by <paramref name="multiplier"/> (e.g. <c>1.15</c> = 15% bigger)
        /// while keeping grid centering and footprint alignment intact!
        /// </summary>
        public GraphicsSet ScaleBy(double multiplier) =>
            new(_layers.Select(l => l.ScaleBy(multiplier)).ToArray());

        /// <summary>Sets the <see cref="SpriteAlign"/> mode for every layer in this <see cref="GraphicsSet"/>.</summary>
        public GraphicsSet Align(SpriteAlign align) =>
            new(_layers.Select(l => l.Align(align)).ToArray());

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
