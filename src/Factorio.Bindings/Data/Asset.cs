using System;
using System.Collections.Generic;
using System.IO;

namespace Factorio.Data
{
    /// <summary>
    /// Builds Factorio resource paths (<c>__source__/path</c>) for mod and vanilla game assets,
    /// and resolves mod-local PNG files at build time for automatic dimension &amp; scale calculation.
    /// </summary>
    public static class Asset
    {
        /// <summary>
        /// Current mod name supplied by the data-stage generator while evaluating <see cref="ModDefinition.Define"/>.
        /// </summary>
        public static string? Source { get; set; }

        /// <summary>
        /// Resource root directories (e.g. <c>&lt;mod&gt;/resources</c>) set during build so sprites
        /// and icons can inspect real PNG pixel dimensions automatically.
        /// </summary>
        public static IReadOnlyList<string>? ResourceRoots { get; set; }

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
}
