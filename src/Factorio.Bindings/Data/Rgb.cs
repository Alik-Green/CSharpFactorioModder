using System;
using System.Globalization;

namespace Factorio.Data
{
    /// <summary>
    /// A strongly-typed Factorio colour (<c>{ r = ..., g = ..., b = ..., a = ... }</c>) for fluids,
    /// tints, and entity map markers.
    /// <code>
    /// BaseColor = new Rgb(0.08, 0.40, 0.85);   // normalized 0..1 floats
    /// FlowColor = Rgb.Bytes(38, 179, 255);     // 8-bit 0..255 integers
    /// MapColor  = Rgb.Hex("#26B3FF");          // HTML hex colour string
    /// </code>
    /// </summary>
    public sealed class Rgb : ILuaValue
    {
        /// <summary>Creates an RGB(A) colour from normalized <c>0..1</c> channel values.</summary>
        public Rgb(double red, double green, double blue, double alpha = 1.0)
        {
            Red = red;
            Green = green;
            Blue = blue;
            Alpha = alpha;
        }

        /// <summary>Red channel (<c>0..1</c>).</summary>
        public double Red { get; }

        /// <summary>Green channel (<c>0..1</c>).</summary>
        public double Green { get; }

        /// <summary>Blue channel (<c>0..1</c>).</summary>
        public double Blue { get; }

        /// <summary>Alpha channel (<c>0..1</c>).</summary>
        public double Alpha { get; }

        /// <summary>Creates a colour from <c>0..255</c> byte components.</summary>
        public static Rgb Bytes(int red, int green, int blue, int alpha = 255) =>
            new(
                Math.Round(red / 255.0, 4),
                Math.Round(green / 255.0, 4),
                Math.Round(blue / 255.0, 4),
                Math.Round(alpha / 255.0, 4));

        /// <summary>Creates a colour from a 6-digit (<c>#RRGGBB</c>) or 8-digit (<c>#RRGGBBAA</c>) hex string.</summary>
        public static Rgb Hex(string hex)
        {
            var clean = hex.Trim().TrimStart('#');
            if (clean.Length is not (6 or 8))
                throw new ArgumentException($"Expected #RRGGBB or #RRGGBBAA hex colour, got '{hex}'.", nameof(hex));

            var r = int.Parse(clean[..2], NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            var g = int.Parse(clean[2..4], NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            var b = int.Parse(clean[4..6], NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            var a = clean.Length == 8
                ? int.Parse(clean[6..8], NumberStyles.HexNumber, CultureInfo.InvariantCulture)
                : 255;
            return Bytes(r, g, b, a);
        }

        /// <summary>Implicit conversion from a hex string such as <c>"#1466D9"</c>.</summary>
        public static implicit operator Rgb?(string? hex) => hex == null ? null : Hex(hex);

        /// <summary>Implicit conversion from an <c>(r, g, b)</c> tuple.</summary>
        public static implicit operator Rgb((double r, double g, double b) c) => new(c.r, c.g, c.b);

        /// <summary>Writes <c>{ r = ..., g = ..., b = ... }</c> (plus <c>a</c> when semi-transparent).</summary>
        public void Write(LuaWriter writer)
        {
            writer.BeginTable();
            writer.Key("r");
            writer.Value(Red);
            writer.Key("g");
            writer.Value(Green);
            writer.Key("b");
            writer.Value(Blue);
            if (Alpha < 1.0)
            {
                writer.Key("a");
                writer.Value(Alpha);
            }
            writer.EndTable();
        }
    }
}
