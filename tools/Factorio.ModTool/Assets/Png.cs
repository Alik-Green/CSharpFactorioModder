using System.IO.Compression;

namespace Factorio.ModTool.Assets;

/// <summary>
/// Minimal PNG writer: 8-bit RGBA, one IDAT, no interlacing. It exists so the asset pipeline can
/// produce sprite sheets without an image library, an external process or a native dependency -
/// the tool stays one self-contained executable.
/// </summary>
public static class Png
{
    private static readonly uint[] CrcTable = BuildCrcTable();

    private static uint[] BuildCrcTable()
    {
        var table = new uint[256];
        for (uint n = 0; n < 256; n++)
        {
            var c = n;
            for (var k = 0; k < 8; k++) c = (c & 1) != 0 ? 0xEDB88320u ^ (c >> 1) : c >> 1;
            table[n] = c;
        }
        return table;
    }

    private static uint Crc(byte[] data, int offset, int count)
    {
        var crc = 0xFFFFFFFFu;
        for (var i = 0; i < count; i++) crc = CrcTable[(crc ^ data[offset + i]) & 0xFF] ^ (crc >> 8);
        return crc ^ 0xFFFFFFFFu;
    }

    /// <summary>Encodes a top-down RGBA buffer (4 bytes per pixel) as a PNG file.</summary>
    /// <param name="width">Image width in pixels.</param>
    /// <param name="height">Image height in pixels.</param>
    /// <param name="rgba">Pixels, row-major, 4 bytes each.</param>
    /// <returns>The bytes of a complete PNG file.</returns>
    public static byte[] Encode(int width, int height, byte[] rgba)
    {
        if (rgba.Length != width * height * 4)
            throw new ToolException($"image buffer is {rgba.Length} bytes, expected {width * height * 4}");

        using var output = new MemoryStream();
        output.Write(new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A });

        var header = new byte[13];
        WriteBigEndian(header, 0, (uint)width);
        WriteBigEndian(header, 4, (uint)height);
        header[8] = 8;    // bit depth
        header[9] = 6;    // colour type: RGBA
        header[10] = 0;   // deflate
        header[11] = 0;   // adaptive filtering
        header[12] = 0;   // no interlace
        WriteChunk(output, "IHDR", header);

        // Every scanline is prefixed with filter type 0 (none); deflate does the rest.
        var raw = new byte[height * (width * 4 + 1)];
        for (var y = 0; y < height; y++)
        {
            raw[y * (width * 4 + 1)] = 0;
            Array.Copy(rgba, y * width * 4, raw, y * (width * 4 + 1) + 1, width * 4);
        }

        using var compressed = new MemoryStream();
        using (var zlib = new ZLibStream(compressed, CompressionLevel.Optimal, leaveOpen: true))
            zlib.Write(raw, 0, raw.Length);
        WriteChunk(output, "IDAT", compressed.ToArray());
        WriteChunk(output, "IEND", Array.Empty<byte>());
        return output.ToArray();
    }

    private static void WriteBigEndian(byte[] buffer, int offset, uint value)
    {
        buffer[offset] = (byte)(value >> 24);
        buffer[offset + 1] = (byte)(value >> 16);
        buffer[offset + 2] = (byte)(value >> 8);
        buffer[offset + 3] = (byte)value;
    }

    private static void WriteChunk(Stream output, string type, byte[] data)
    {
        var length = new byte[4];
        WriteBigEndian(length, 0, (uint)data.Length);
        output.Write(length);

        var typeBytes = System.Text.Encoding.ASCII.GetBytes(type);
        output.Write(typeBytes);
        output.Write(data);

        var crcInput = new byte[typeBytes.Length + data.Length];
        typeBytes.CopyTo(crcInput, 0);
        data.CopyTo(crcInput, typeBytes.Length);
        var crc = new byte[4];
        WriteBigEndian(crc, 0, Crc(crcInput, 0, crcInput.Length));
        output.Write(crc);
    }
}
