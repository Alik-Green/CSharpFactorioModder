using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Factorio.Data
{
    /// <summary>
    /// Builds Factorio resource paths (<c>__source__/path</c>) for mod and vanilla game assets,
    /// and resolves mod-local PNG files at build time for automatic dimension &amp; scale calculation.
    /// </summary>
    public static class Asset
    {
        private static readonly AsyncLocal<string?> CurrentSource = new();
        private static readonly AsyncLocal<IReadOnlyList<string>?> CurrentResourceRoots = new();

        /// <summary>
        /// Current mod name supplied by the data-stage generator while evaluating <see cref="ModDefinition.Define"/>.
        /// </summary>
        public static string? Source
        {
            get => CurrentSource.Value;
            set => CurrentSource.Value = value;
        }

        /// <summary>
        /// Resource root directories (e.g. <c>&lt;mod&gt;/resources</c>) set during build so sprites
        /// and icons can inspect real PNG pixel dimensions automatically.
        /// </summary>
        public static IReadOnlyList<string>? ResourceRoots
        {
            get => CurrentResourceRoots.Value;
            set => CurrentResourceRoots.Value = value;
        }

        /// <summary>Prefixes a mod-local asset path with <c>__&lt;Source&gt;__/</c> unless already prefixed.</summary>
        public static string Mod(string path)
        {
            if (string.IsNullOrEmpty(path)) return path;
            if (HasSourcePrefix(path)) return path;

            var trimmed = path.TrimStart('/');
            return string.IsNullOrEmpty(Source) ? trimmed : $"__{Source}__/{trimmed}";
        }

        /// <summary>Prefixes a vanilla base-game asset path with <c>__base__/</c> unless already prefixed.</summary>
        public static string Base(string path)
        {
            if (string.IsNullOrEmpty(path)) return path;
            if (HasSourcePrefix(path)) return path;

            var trimmed = path.TrimStart('/');
            return $"__base__/{trimmed}";
        }

        /// <summary>
        /// Resolves a <c>__&lt;Mod&gt;__/...</c> (or relative) asset path to a real file inside
        /// <see cref="ResourceRoots"/> when present on disk.
        /// </summary>
        public static bool TryResolveModFile(string? resourcePath, out string fullPath)
        {
            fullPath = "";
            if (string.IsNullOrWhiteSpace(resourcePath) || ResourceRoots == null || ResourceRoots.Count == 0)
                return false;

            if (resourcePath.StartsWith("__base__/", StringComparison.Ordinal) ||
                resourcePath.StartsWith("__core__/", StringComparison.Ordinal))
                return false;

            var relative = StripOwnPrefix(resourcePath);
            if (relative == null) return false;

            var normalized = relative.TrimStart('/').Replace('/', Path.DirectorySeparatorChar);
            foreach (var root in ResourceRoots)
            {
                if (string.IsNullOrWhiteSpace(root) || !Directory.Exists(root)) continue;
                var candidate = Path.Combine(root, normalized);
                if (File.Exists(candidate))
                {
                    fullPath = candidate;
                    return true;
                }
            }

            return false;
        }

        /// <summary>Reads the pixel dimensions of a PNG asset on disk from its 24-byte IHDR header.</summary>
        public static bool TryReadModPngSize(string? resourcePath, out int width, out int height)
        {
            width = 0;
            height = 0;
            if (!TryResolveModFile(resourcePath, out var fullPath))
                return false;

            return TryReadPngHeader(fullPath, out width, out height);
        }

        /// <summary>Reads width and height from any valid PNG file header.</summary>
        public static bool TryReadPngHeader(string filePath, out int width, out int height)
        {
            width = 0;
            height = 0;
            try
            {
                Span<byte> header = stackalloc byte[24];
                using var stream = File.OpenRead(filePath);
                if (stream.Read(header) < 24) return false;

                // PNG signature: 89 50 4E 47 0D 0A 1A 0A, followed by IHDR length + 'IHDR' + width + height
                if (header[0] != 0x89 || header[1] != 0x50 || header[2] != 0x4E || header[3] != 0x47 ||
                    header[4] != 0x0D || header[5] != 0x0A || header[6] != 0x1A || header[7] != 0x0A)
                    return false;

                width = (header[16] << 24) | (header[17] << 16) | (header[18] << 8) | header[19];
                height = (header[20] << 24) | (header[21] << 16) | (header[22] << 8) | header[23];
                return width > 0 && height > 0;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Reads the non-transparent pixel bounding box (<c>alpha &gt; 10</c>) of a mod PNG on disk so
        /// <see cref="Sprite"/> and <see cref="GraphicsSet"/> automatically trim empty transparent borders,
        /// scale the visible artwork accurately to the tile grid, and cancel off-center margins.
        /// Falls back cleanly to the full PNG canvas dimensions when alpha chunks are absent.
        /// </summary>
        public static bool TryReadModPngBounds(string? resourcePath, out PngBounds bounds)
        {
            bounds = default;
            if (!TryResolveModFile(resourcePath, out var fullPath))
                return false;

            return TryReadPngBounds(fullPath, out bounds);
        }

        /// <summary>Reads canvas size and visible non-transparent pixel bounds from a PNG file.</summary>
        public static bool TryReadPngBounds(string filePath, out PngBounds bounds)
        {
            bounds = default;
            if (!TryReadPngHeader(filePath, out var canvasW, out var canvasH))
                return false;

            // Default fallback: entire canvas is treated as visible content.
            bounds = new PngBounds(canvasW, canvasH, 0, 0, canvasW - 1, canvasH - 1);

            if ((long)canvasW * canvasH > 67_108_864L)
                return true;

            try
            {
                using var stream = File.OpenRead(filePath);
                Span<byte> sig = stackalloc byte[8];
                if (stream.Read(sig) < 8) return true;

                int bitDepth = 0, colorType = 0, interlace = 0;
                byte[]? trns = null;
                using var idat = new MemoryStream();
                Span<byte> chunkHeader = stackalloc byte[8];

                while (stream.Read(chunkHeader) == 8)
                {
                    int len = (chunkHeader[0] << 24) | (chunkHeader[1] << 16) | (chunkHeader[2] << 8) | chunkHeader[3];
                    if (len < 0) break;

                    bool isIhdr = chunkHeader[4] == 'I' && chunkHeader[5] == 'H' && chunkHeader[6] == 'D' && chunkHeader[7] == 'R';
                    bool isTrns = chunkHeader[4] == 't' && chunkHeader[5] == 'R' && chunkHeader[6] == 'N' && chunkHeader[7] == 'S';
                    bool isIdat = chunkHeader[4] == 'I' && chunkHeader[5] == 'D' && chunkHeader[6] == 'A' && chunkHeader[7] == 'T';
                    bool isIend = chunkHeader[4] == 'I' && chunkHeader[5] == 'E' && chunkHeader[6] == 'N' && chunkHeader[7] == 'D';

                    if (isIend) break;

                    if (isIhdr && len >= 13)
                    {
                        var ihdr = new byte[len];
                        if (ReadExact(stream, ihdr, len) < len) return true;
                        bitDepth = ihdr[8];
                        colorType = ihdr[9];
                        interlace = ihdr[12];
                    }
                    else if (isTrns)
                    {
                        trns = new byte[len];
                        if (ReadExact(stream, trns, len) < len) return true;
                    }
                    else if (isIdat)
                    {
                        var buf = new byte[Math.Min(len, 65536)];
                        int rem = len;
                        while (rem > 0)
                        {
                            int n = stream.Read(buf, 0, Math.Min(rem, buf.Length));
                            if (n <= 0) return true;
                            idat.Write(buf, 0, n);
                            rem -= n;
                        }
                    }
                    else
                    {
                        stream.Seek(len, SeekOrigin.Current);
                    }

                    // Skip 4-byte CRC
                    stream.Seek(4, SeekOrigin.Current);
                }

                // Only non-interlaced PNGs with real IDAT data need scanline decoding.
                if (interlace != 0 || idat.Length == 0)
                    return true;

                // Determine bytes per pixel for 8-bit and 16-bit PNGs (covers RGBA, Gray+A, Indexed+tRNS, RGB+tRNS).
                int bpp = (colorType, bitDepth) switch
                {
                    (6, 8) => 4,
                    (6, 16) => 8,
                    (4, 8) => 2,
                    (4, 16) => 4,
                    (3, 8) => 1,
                    (2, 8) => 3,
                    (2, 16) => 6,
                    _ => 0,
                };
                if (bpp == 0) return true;

                // If RGB has no tRNS chunk, every pixel is 100% opaque -> full canvas!
                if (colorType == 2 && (trns == null || trns.Length < 6))
                    return true;

                idat.Position = 0;
                using var zlib = new ZLibStream(idat, CompressionMode.Decompress);

                int stride = canvasW * bpp;
                var prevRow = new byte[stride];
                var currRow = new byte[stride];
                var rawRow = new byte[stride + 1];

                int minX = canvasW, minY = canvasH, maxX = -1, maxY = -1;
                const int AlphaThreshold = 10;

                for (int y = 0; y < canvasH; y++)
                {
                    if (ReadExact(zlib, rawRow, stride + 1) < stride + 1)
                        return true;

                    byte filter = rawRow[0];
                    for (int i = 0; i < stride; i++)
                    {
                        byte raw = rawRow[i + 1];
                        byte left = i >= bpp ? currRow[i - bpp] : (byte)0;
                        byte up = prevRow[i];
                        byte upLeft = i >= bpp ? prevRow[i - bpp] : (byte)0;

                        currRow[i] = filter switch
                        {
                            0 => raw,
                            1 => (byte)(raw + left),
                            2 => (byte)(raw + up),
                            3 => (byte)(raw + ((left + up) >> 1)),
                            4 => (byte)(raw + PaethPredictor(left, up, upLeft)),
                            _ => raw,
                        };
                    }

                    for (int x = 0; x < canvasW; x++)
                    {
                        int off = x * bpp;
                        int alpha = colorType switch
                        {
                            6 => bitDepth == 8 ? currRow[off + 3] : currRow[off + 6],
                            4 => bitDepth == 8 ? currRow[off + 1] : currRow[off + 2],
                            3 => trns != null && currRow[off] < trns.Length ? trns[currRow[off]] : 255,
                            2 when bitDepth == 8 =>
                                (currRow[off] == trns![1] && currRow[off + 1] == trns[3] && currRow[off + 2] == trns[5]) ? 0 : 255,
                            2 =>
                                (currRow[off] == trns![0] && currRow[off + 1] == trns[1] &&
                                 currRow[off + 2] == trns[2] && currRow[off + 3] == trns[3] &&
                                 currRow[off + 4] == trns[4] && currRow[off + 5] == trns[5]) ? 0 : 255,
                            _ => 255,
                        };

                        if (alpha > AlphaThreshold)
                        {
                            if (x < minX) minX = x;
                            if (x > maxX) maxX = x;
                            if (y < minY) minY = y;
                            if (y > maxY) maxY = y;
                        }
                    }

                    (prevRow, currRow) = (currRow, prevRow);
                }

                if (maxX >= minX && maxY >= minY)
                    bounds = new PngBounds(canvasW, canvasH, minX, minY, maxX, maxY);

                return true;
            }
            catch
            {
                return true;
            }
        }

        private static int ReadExact(Stream stream, byte[] buffer, int count)
        {
            int total = 0;
            while (total < count)
            {
                int n = stream.Read(buffer, total, count - total);
                if (n <= 0) break;
                total += n;
            }
            return total;
        }

        private static int PaethPredictor(int a, int b, int c)
        {
            int p = a + b - c;
            int pa = Math.Abs(p - a);
            int pb = Math.Abs(p - b);
            int pc = Math.Abs(p - c);
            if (pa <= pb && pa <= pc) return a;
            return pb <= pc ? b : c;
        }

        private static string? StripOwnPrefix(string path)
        {
            if (!HasSourcePrefix(path)) return path;
            var secondDoubleUnderscore = path.IndexOf("__/", 2, StringComparison.Ordinal);
            if (secondDoubleUnderscore < 0) return null;

            var prefixSource = path[2..secondDoubleUnderscore];
            if (!string.IsNullOrEmpty(Source) && !string.Equals(prefixSource, Source, StringComparison.Ordinal))
                return null;

            return path[(secondDoubleUnderscore + 3)..];
        }

        private static bool HasSourcePrefix(string path)
        {
            if (!path.StartsWith("__", StringComparison.Ordinal)) return false;
            var end = path.IndexOf("__/", 2, StringComparison.Ordinal);
            return end > 2;
        }
    }

    /// <summary>
    /// A prototype icon path (e.g. <c>Asset.Mod("graphics/icons/condenser.png")</c> or
    /// <c>IconSpec.Mod("graphics/icons/condenser.png")</c>).
    /// <para>
    /// When the icon points to a PNG inside <c>resources/</c>, the build automatically inspects its
    /// actual pixel dimensions so <see cref="PrototypeDefinition.IconSize"/> never exceeds the image
    /// bounds even if <c>IconSize</c> was left unset or set to a different size.
    /// </para>
    /// </summary>
    public sealed class IconSpec : ILuaValue
    {
        private readonly ILuaValue? _sprite;

        /// <summary>Creates an icon descriptor for <paramref name="path"/>.</summary>
        /// <param name="path">Prefixed resource path, e.g. from <see cref="Asset.Mod"/> or <see cref="Asset.Base"/>.</param>
        public IconSpec(string path)
        {
            Path = path;
        }

        private IconSpec(Sprite sprite)
        {
            Path = "";
            _sprite = sprite;
        }

        /// <summary>The Factorio resource path written to <c>icon</c> (when path-based).</summary>
        public string Path { get; }

        /// <summary>An icon bundled inside this mod's <c>resources/</c> directory.</summary>
        public static IconSpec Mod(string path) => new(Asset.Mod(path));

        /// <summary>An icon from the base Factorio game (<c>__base__/...</c>).</summary>
        public static IconSpec Base(string path) => new(Asset.Base(path));

        /// <summary>Implicitly wraps a string path from <c>Asset.Mod(...)</c> or <c>Asset.Base(...)</c>.</summary>
        public static implicit operator IconSpec?(string? path) => path == null ? null : new IconSpec(path);

        /// <summary>Implicitly wraps a <see cref="Sprite"/> icon table.</summary>
        public static implicit operator IconSpec?(Sprite? sprite) => sprite == null ? null : new IconSpec(sprite);

        /// <summary>Implicitly unwraps back to the underlying resource path string.</summary>
        public static implicit operator string?(IconSpec? icon) => icon?.Path;

        /// <inheritdoc />
        public override string ToString() => Path;

        /// <inheritdoc />
        public void Write(LuaWriter writer)
        {
            if (_sprite != null) _sprite.Write(writer);
            else writer.Value(Path);
        }
    }

    /// <summary>
    /// One layer inside <see cref="PrototypeDefinition.Icons"/> — lets you create tinted or badged
    /// icons directly from vanilla Factorio icons in pure C# without external image editing!
    /// <code>
    /// Icons = [
    ///     IconLayer.Tinted(Vanilla.Icons.Machines.ChemicalPlant, Rgb.Hex("#38BDF8")),
    ///     IconLayer.Badge(Vanilla.Icons.Fluids.Water),
    /// ];
    /// </code>
    /// </summary>
    public sealed class IconLayer : ILuaValue
    {
        private readonly string _icon;
        private readonly int? _iconSize;
        private readonly Rgb? _tint;
        private readonly double? _scale;
        private readonly double _shiftX;
        private readonly double _shiftY;

        private IconLayer(string icon, int? iconSize = null, Rgb? tint = null, double? scale = null, double shiftX = 0, double shiftY = 0)
        {
            _icon = icon;
            _iconSize = iconSize;
            _tint = tint;
            _scale = scale;
            _shiftX = shiftX;
            _shiftY = shiftY;
        }

        /// <summary>Full-size base icon layer.</summary>
        public static IconLayer Of(IconSpec icon, int? iconSize = null, Rgb? tint = null, double? scale = null, double shiftX = 0, double shiftY = 0) =>
            new(icon.Path, iconSize, tint, scale, shiftX, shiftY);

        /// <summary>Tints an existing icon with <paramref name="tint"/>.</summary>
        public static IconLayer Tinted(IconSpec icon, Rgb tint, int? iconSize = null) =>
            new(icon.Path, iconSize, tint: tint);

        /// <summary>Overlays a small corner badge icon (default top-left corner at quarter scale).</summary>
        public static IconLayer Badge(IconSpec icon, double scale = 0.25, double shiftX = -8, double shiftY = -8, Rgb? tint = null) =>
            new(icon.Path, tint: tint, scale: scale, shiftX: shiftX, shiftY: shiftY);

        /// <inheritdoc />
        public void Write(LuaWriter writer)
        {
            int? resolvedSize = _iconSize;
            if (!resolvedSize.HasValue)
            {
                if (Asset.TryReadModPngSize(_icon, out var pngW, out var pngH))
                    resolvedSize = Math.Min(pngW, pngH);
                else if (_icon.StartsWith("__base__/graphics/technology/", StringComparison.Ordinal) ||
                         _icon.StartsWith("__space-age__/graphics/technology/", StringComparison.Ordinal))
                    resolvedSize = 256;
            }

            writer.BeginTable();
            writer.Key("icon");
            writer.Value(_icon);
            if (resolvedSize.HasValue)
            {
                writer.Key("icon_size");
                writer.Value(resolvedSize.Value);
            }
            if (_tint != null)
            {
                writer.Key("tint");
                writer.Value(_tint);
            }
            if (_scale.HasValue)
            {
                writer.Key("scale");
                writer.Value(_scale.Value);
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

    /// <summary>Strongly-typed collection of <see cref="IconLayer"/> entries for <see cref="PrototypeDefinition.Icons"/>.</summary>
    [CollectionBuilder(typeof(IconLayerList), nameof(Create))]
    public sealed class IconLayerList : ILuaValue, IEnumerable<IconLayer>
    {
        private readonly List<IconLayer> _items = new();

        /// <summary>Builds from <c>[IconLayer.Tinted(...), IconLayer.Badge(...)]</c>.</summary>
        public static IconLayerList Create(ReadOnlySpan<IconLayer> items)
        {
            var list = new IconLayerList();
            foreach (var item in items) list._items.Add(item);
            return list;
        }

        /// <inheritdoc />
        public void Write(LuaWriter writer)
        {
            writer.BeginArray();
            foreach (var item in _items) writer.Value(item);
            writer.EndArray();
        }

        /// <inheritdoc />
        public IEnumerator<IconLayer> GetEnumerator() => _items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    /// <summary>Canvas dimensions and non-transparent content bounding box of a PNG image.</summary>
    public readonly record struct PngBounds(
        int CanvasWidth,
        int CanvasHeight,
        int MinX,
        int MinY,
        int MaxX,
        int MaxY)
    {
        /// <summary>Width of visible non-transparent content in pixels.</summary>
        public int ContentWidth => MaxX >= MinX ? (MaxX - MinX + 1) : CanvasWidth;

        /// <summary>Height of visible non-transparent content in pixels.</summary>
        public int ContentHeight => MaxY >= MinY ? (MaxY - MinY + 1) : CanvasHeight;

        /// <summary>Horizontal pixel centre of the visible content.</summary>
        public double ContentCenterX => MaxX >= MinX ? (MinX + MaxX + 1) * 0.5 : CanvasWidth * 0.5;

        /// <summary>Vertical pixel centre of the visible content.</summary>
        public double ContentCenterY => MaxY >= MinY ? (MinY + MaxY + 1) * 0.5 : CanvasHeight * 0.5;

        /// <summary>Bottom pixel edge of the visible content.</summary>
        public double ContentBottom => MaxY >= MinY ? (MaxY + 1) : CanvasHeight;
    }
}
