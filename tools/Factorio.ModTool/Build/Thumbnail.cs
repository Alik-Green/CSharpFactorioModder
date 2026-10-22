using Factorio.ModTool.Assets;

namespace Factorio.ModTool.Build;

/// <summary>
/// Generates a clean 144x144 RGBA <c>thumbnail.png</c> for newly scaffolded mods using <see cref="Png"/>.
/// </summary>
public static class Thumbnail
{
    public const int Size = 144;

    private static readonly Dictionary<char, string[]> Glyphs = new()
    {
        ['A'] = new[] { "01110", "10001", "10001", "11111", "10001", "10001", "10001" },
        ['B'] = new[] { "11110", "10001", "10001", "11110", "10001", "10001", "11110" },
        ['C'] = new[] { "01111", "10000", "10000", "10000", "10000", "10000", "01111" },
        ['D'] = new[] { "11110", "10001", "10001", "10001", "10001", "10001", "11110" },
        ['E'] = new[] { "11111", "10000", "10000", "11110", "10000", "10000", "11111" },
        ['F'] = new[] { "11111", "10000", "10000", "11110", "10000", "10000", "10000" },
        ['G'] = new[] { "01111", "10000", "10000", "10011", "10001", "10001", "01111" },
        ['H'] = new[] { "10001", "10001", "10001", "11111", "10001", "10001", "10001" },
        ['I'] = new[] { "11111", "00100", "00100", "00100", "00100", "00100", "11111" },
        ['J'] = new[] { "00111", "00010", "00010", "00010", "10010", "10010", "01100" },
        ['K'] = new[] { "10001", "10010", "10100", "11000", "10100", "10010", "10001" },
        ['L'] = new[] { "10000", "10000", "10000", "10000", "10000", "10000", "11111" },
        ['M'] = new[] { "10001", "11011", "10101", "10101", "10001", "10001", "10001" },
        ['N'] = new[] { "10001", "11001", "10101", "10011", "10001", "10001", "10001" },
        ['O'] = new[] { "01110", "10001", "10001", "10001", "10001", "10001", "01110" },
        ['P'] = new[] { "11110", "10001", "10001", "11110", "10000", "10000", "10000" },
        ['Q'] = new[] { "01110", "10001", "10001", "10001", "10101", "10010", "01101" },
        ['R'] = new[] { "11110", "10001", "10001", "11110", "10100", "10010", "10001" },
        ['S'] = new[] { "01111", "10000", "10000", "01110", "00001", "00001", "11110" },
        ['T'] = new[] { "11111", "00100", "00100", "00100", "00100", "00100", "00100" },
        ['U'] = new[] { "10001", "10001", "10001", "10001", "10001", "10001", "01110" },
        ['V'] = new[] { "10001", "10001", "10001", "10001", "10001", "01010", "00100" },
        ['W'] = new[] { "10001", "10001", "10001", "10101", "10101", "11011", "10001" },
        ['X'] = new[] { "10001", "10001", "01010", "00100", "01010", "10001", "10001" },
        ['Y'] = new[] { "10001", "10001", "01010", "00100", "00100", "00100", "00100" },
        ['Z'] = new[] { "11111", "00001", "00010", "00100", "01000", "10000", "11111" },
    };

    public static byte[] Render(string modName)
    {
        var pixels = new byte[Size * Size * 4];
        var initial = modName.FirstOrDefault(char.IsLetter);
        var letter = initial == default ? 'F' : char.ToUpperInvariant(initial);
        var glyph = Glyphs.GetValueOrDefault(letter, Glyphs['F']);

        for (var y = 0; y < Size; y++)
        {
            for (var x = 0; x < Size; x++)
            {
                var idx = (y * Size + x) * 4;
                var border = x < 6 || x >= Size - 6 || y < 6 || y >= Size - 6;
                pixels[idx + 0] = border ? (byte)227 : (byte)36;
                pixels[idx + 1] = border ? (byte)138 : (byte)40;
                pixels[idx + 2] = border ? (byte)42 : (byte)47;
                pixels[idx + 3] = 255;
            }
        }

        const int scale = 10;
        var startX = (Size - 5 * scale) / 2;
        var startY = (Size - 7 * scale) / 2;
        for (var row = 0; row < 7; row++)
        {
            for (var col = 0; col < 5; col++)
            {
                if (glyph[row][col] != '1') continue;
                for (var dy = 0; dy < scale; dy++)
                for (var dx = 0; dx < scale; dx++)
                {
                    var px = startX + col * scale + dx;
                    var py = startY + row * scale + dy;
                    var idx = (py * Size + px) * 4;
                    pixels[idx + 0] = 255;
                    pixels[idx + 1] = 158;
                    pixels[idx + 2] = 56;
                    pixels[idx + 3] = 255;
                }
            }
        }

        return Png.Encode(Size, Size, pixels);
    }
}
