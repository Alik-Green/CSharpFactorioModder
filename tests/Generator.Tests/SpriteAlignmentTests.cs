using System;
using System.Buffers.Binary;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Factorio.Data;
using Factorio.ModTool;
using Factorio.ModTool.Build;
using Xunit;

namespace Factorio.ModTool.Tests;

public sealed class SpriteAlignmentTests
{
    private static readonly ToolchainSettings Settings = ToolchainSettings.Locate(FindRoot());

    private static string FindRoot()
    {
        var dir = new DirectoryInfo(AppContext.BaseDirectory);
        while (dir != null && !File.Exists(Path.Combine(dir.FullName, "config", "toolchain.json")))
            dir = dir.Parent;
        return dir?.FullName ?? throw new InvalidOperationException("Repository root not found.");
    }

    /// <summary>
    /// Writes a spec-compliant compressed RGBA PNG image whose opaque rectangle sits at
    /// [rectX .. rectX + rectW - 1] × [rectY .. rectY + rectH - 1] inside a transparent canvas.
    /// </summary>
    private static void WriteRgbaPng(
        string path,
        int canvasW,
        int canvasH,
        int rectX,
        int rectY,
        int rectW,
        int rectH)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(path)!);
        using var outStream = File.Create(path);

        // PNG signature
        outStream.Write(new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A });

        // IHDR: 8-bit RGBA (bitDepth=8, colorType=6, compression=0, filter=0, interlace=0)
        var ihdr = new byte[13];
        BinaryPrimitives.WriteInt32BigEndian(ihdr.AsSpan(0, 4), canvasW);
        BinaryPrimitives.WriteInt32BigEndian(ihdr.AsSpan(4, 4), canvasH);
        ihdr[8] = 8;
        ihdr[9] = 6;
        WritePngChunk(outStream, "IHDR", ihdr);

        // Build raw RGBA scanlines (filter=0 per row) with transparent padding and opaque interior box
        using var rawScanlines = new MemoryStream();
        var row = new byte[1 + canvasW * 4];
        for (int y = 0; y < canvasH; y++)
        {
            Array.Clear(row);
            row[0] = 0; // Filter type 0 (None)
            bool inY = y >= rectY && y < rectY + rectH;
            if (inY)
            {
                for (int x = rectX; x < rectX + rectW && x < canvasW; x++)
                {
                    int off = 1 + x * 4;
                    row[off + 0] = 60;
                    row[off + 1] = 160;
                    row[off + 2] = 240;
                    row[off + 3] = 255; // Opaque
                }
            }
            rawScanlines.Write(row, 0, row.Length);
        }

        using var compressed = new MemoryStream();
        using (var zlib = new ZLibStream(compressed, CompressionLevel.Fastest, leaveOpen: true))
        {
            rawScanlines.Position = 0;
            rawScanlines.CopyTo(zlib);
        }
        WritePngChunk(outStream, "IDAT", compressed.ToArray());
        WritePngChunk(outStream, "IEND", Array.Empty<byte>());
    }

    private static void WritePngChunk(Stream output, string typeAscii, byte[] data)
    {
        Span<byte> lenBytes = stackalloc byte[4];
        BinaryPrimitives.WriteInt32BigEndian(lenBytes, data.Length);
        output.Write(lenBytes);

        var typeBytes = System.Text.Encoding.ASCII.GetBytes(typeAscii);
        output.Write(typeBytes);
        output.Write(data);

        // CRC-32 over type + data
        uint crc = 0xFFFF_FFFFu;
        foreach (byte b in typeBytes) crc = UpdateCrc(crc, b);
        foreach (byte b in data) crc = UpdateCrc(crc, b);
        crc ^= 0xFFFF_FFFFu;

        Span<byte> crcBytes = stackalloc byte[4];
        BinaryPrimitives.WriteUInt32BigEndian(crcBytes, crc);
        output.Write(crcBytes);
    }

    private static uint UpdateCrc(uint crc, byte b)
    {
        crc ^= b;
        for (int k = 0; k < 8; k++)
            crc = (crc & 1) != 0 ? (crc >> 1) ^ 0xEDB88320u : (crc >> 1);
        return crc;
    }

    [Fact]
    public void PngBounds_TallIsometricAndOffCenterSprites_AutoTrimAlignAndSupportManualShiftAndScaleBy()
    {
        var tempRoot = Path.Combine(Path.GetTempPath(), "SpriteAlign_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempRoot);
        try
        {
            var pngPath = Path.Combine(tempRoot, "resources", "graphics", "entity", "condenser.png");

            // 512x512 canvas with a 256x384 tall isometric building placed off-center at x=[140..395], y=[60..443]:
            // - Canvas center is (256, 256)
            // - Visible art center X is 268 (+12 px right of center!)
            // - Visible art bottom Y is 444 (68 px above bottom edge!)
            WriteRgbaPng(pngPath, canvasW: 512, canvasH: 512, rectX: 140, rectY: 60, rectW: 256, rectH: 384);

            Assert.True(Asset.TryReadPngBounds(pngPath, out var bounds));
            Assert.Equal(512, bounds.CanvasWidth);
            Assert.Equal(512, bounds.CanvasHeight);
            Assert.Equal(256, bounds.ContentWidth);
            Assert.Equal(384, bounds.ContentHeight);
            Assert.Equal(268.0, bounds.ContentCenterX);
            Assert.Equal(444.0, bounds.ContentBottom);

            // Render a 3x3 GraphicsSet with Footprint alignment:
            // - Width scale MUST be 96 / 256 = 0.375 (fills full 3.0 tiles instead of shrinking to 0.25!)
            // - Shift X MUST be -((268 - 256) * 0.375) / 32 = -0.1406 tiles (places visible center at X = 0.000!)
            // - Shift Y MUST be 1.5 - ((444 - 256) * 0.375) / 32 = -0.7031 tiles (places base flush at +1.500 tiles!)
            var prevSource = Asset.Source;
            var prevRoots = Asset.ResourceRoots;
            Asset.Source = "TestMod";
            Asset.ResourceRoots = new[] { Path.Combine(tempRoot, "resources") };
            try
            {
                var writer = new LuaWriter("2.0");
                var gfx = GraphicsSet.Auto(Asset.Mod("graphics/entity/condenser.png"), tiles: 3);
                writer.Value(gfx);
                var lua = writer.ToString();

                Assert.Contains("width = 512", lua);
                Assert.Contains("height = 512", lua);
                Assert.Contains("scale = 0.375", lua);
                Assert.Contains("-0.1406", lua);
                Assert.Contains("-0.7031", lua);

                // Also verify manual .ShiftPixels(32, -16) adds (+1.0, -0.5) tiles cleanly on top of auto alignment!
                var shiftedWriter = new LuaWriter("2.0");
                shiftedWriter.Value(gfx.ShiftPixels(32, -16));
                var shiftedLua = shiftedWriter.ToString();
                Assert.Contains("0.8594", shiftedLua);   // -0.1406 + 1.0000 = +0.8594
                Assert.Contains("-1.2031", shiftedLua);  // -0.7031 - 0.5000 = -1.2031
            }
            finally
            {
                Asset.Source = prevSource;
                Asset.ResourceRoots = prevRoots;
            }
        }
        finally
        {
            if (Directory.Exists(tempRoot))
                Directory.Delete(tempRoot, recursive: true);
        }
    }
}
