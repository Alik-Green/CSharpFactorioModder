namespace Factorio.ModTool.Assets;

/// <summary>
/// A small software renderer: orthographic camera, flat shading, z-buffer, 2x supersampling.
/// <para>
/// It turns a <c>.obj</c> into the PNG a Factorio sprite needs, so a mod can keep the model in the
/// repository and regenerate the sprite on every build instead of hand-drawing one. It is not a
/// replacement for Blender (no textures, no animation, no PBR); it is the default because it needs
/// nothing installed. <c>"renderer": "blender"</c> in <c>assets/render.json</c> switches to Blender
/// when a frame needs more than this.
/// </para>
/// </summary>
public static class Renderer
{
    public sealed record Options(
        int Size,
        double YawDegrees,
        double PitchDegrees,
        double Zoom,
        byte Red,
        byte Green,
        byte Blue,
        bool Supersample = true,
        bool Outline = true);

    /// <summary>Renders one view of a model into an RGBA buffer.</summary>
    /// <param name="model">The mesh to draw.</param>
    /// <param name="options">Camera and colour settings.</param>
    /// <returns>Pixels, row-major, 4 bytes per pixel.</returns>
    public static byte[] Render(ObjModel model, Options options)
    {
        var scale = options.Supersample ? 2 : 1;
        var size = options.Size * scale;

        var (min, max) = model.Bounds();
        var centreX = (min.X + max.X) / 2;
        var centreY = (min.Y + max.Y) / 2;
        var centreZ = (min.Z + max.Z) / 2;
        var span = Math.Max(max.X - min.X, Math.Max(max.Y - min.Y, max.Z - min.Z));
        if (span <= 0) span = 1;

        var yaw = options.YawDegrees * Math.PI / 180.0;
        var pitch = options.PitchDegrees * Math.PI / 180.0;
        var cosYaw = Math.Cos(yaw);
        var sinYaw = Math.Sin(yaw);
        var cosPitch = Math.Cos(pitch);
        var sinPitch = Math.Sin(pitch);

        // Model units -> pixels: fit the longest axis of the model into the frame.
        var pixelsPerUnit = size * options.Zoom / span;

        var pixels = new byte[size * size * 4];
        var depth = new double[size * size];
        Array.Fill(depth, double.MaxValue);

        // Light sits above and to the left of the camera. Visible faces point along +Y (the camera
        // looks down -Y), so the light needs a positive Y component or every face is unlit.
        var light = Normalise(-0.40, 0.72, 0.57);
        const double ambient = 0.35;

        foreach (var triangle in model.Triangles)
        {
            var a = Transform(model.Vertices[triangle.A], centreX, centreY, centreZ, cosYaw, sinYaw, cosPitch, sinPitch);
            var b = Transform(model.Vertices[triangle.B], centreX, centreY, centreZ, cosYaw, sinYaw, cosPitch, sinPitch);
            var c = Transform(model.Vertices[triangle.C], centreX, centreY, centreZ, cosYaw, sinYaw, cosPitch, sinPitch);

            var normal = Normalise(
                (b.Y - a.Y) * (c.Z - a.Z) - (b.Z - a.Z) * (c.Y - a.Y),
                (b.Z - a.Z) * (c.X - a.X) - (b.X - a.X) * (c.Z - a.Z),
                (b.X - a.X) * (c.Y - a.Y) - (b.Y - a.Y) * (c.X - a.X));

            // The camera looks along +Y, so a face is visible when its normal points at the viewer.
            if (normal.Y <= 0) continue;

            var lambert = Math.Max(0, normal.X * light.X + normal.Y * light.Y + normal.Z * light.Z);
            var shade = ambient + (1 - ambient) * lambert;
            var r = (byte)Math.Clamp(options.Red * shade, 0, 255);
            var g = (byte)Math.Clamp(options.Green * shade, 0, 255);
            var bl = (byte)Math.Clamp(options.Blue * shade, 0, 255);

            // Screen: x to the right, z upwards, y is depth.
            var ax = a.X * pixelsPerUnit + size / 2.0;
            var ay = size / 2.0 - a.Z * pixelsPerUnit;
            var bx = b.X * pixelsPerUnit + size / 2.0;
            var by = size / 2.0 - b.Z * pixelsPerUnit;
            var cx = c.X * pixelsPerUnit + size / 2.0;
            var cy = size / 2.0 - c.Z * pixelsPerUnit;

            FillTriangle(pixels, depth, size, (ax, ay, a.Y), (bx, by, b.Y), (cx, cy, c.Y), r, g, bl);
        }

        var result = scale == 1 ? pixels : Downsample(pixels, options.Size, scale);
        return options.Outline ? AddOutline(result, options.Size, options.Red, options.Green, options.Blue) : result;
    }

    private static (double X, double Y, double Z) Transform(
        (double X, double Y, double Z) v,
        double cx, double cy, double cz,
        double cosYaw, double sinYaw, double cosPitch, double sinPitch)
    {
        // Centre, yaw around Z (the model's up axis), then pitch around X (tilt the camera down).
        var x = v.X - cx;
        var y = v.Y - cy;
        var z = v.Z - cz;

        var rx = x * cosYaw - y * sinYaw;
        var ry = x * sinYaw + y * cosYaw;

        var py = ry * cosPitch - z * sinPitch;
        var pz = ry * sinPitch + z * cosPitch;

        return (rx, py, pz);
    }

    private static void FillTriangle(
        byte[] pixels, double[] depth, int size,
        (double X, double Y, double Z) a,
        (double X, double Y, double Z) b,
        (double X, double Y, double Z) c,
        byte red, byte green, byte blue)
    {
        var minX = Math.Max(0, (int)Math.Floor(Math.Min(a.X, Math.Min(b.X, c.X))));
        var maxX = Math.Min(size - 1, (int)Math.Ceiling(Math.Max(a.X, Math.Max(b.X, c.X))));
        var minY = Math.Max(0, (int)Math.Floor(Math.Min(a.Y, Math.Min(b.Y, c.Y))));
        var maxY = Math.Min(size - 1, (int)Math.Ceiling(Math.Max(a.Y, Math.Max(b.Y, c.Y))));
        if (minX > maxX || minY > maxY) return;

        var area = (b.X - a.X) * (c.Y - a.Y) - (c.X - a.X) * (b.Y - a.Y);
        if (Math.Abs(area) < 1e-9) return;
        var inverse = 1.0 / area;

        for (var y = minY; y <= maxY; y++)
        {
            var py = y + 0.5;
            for (var x = minX; x <= maxX; x++)
            {
                var px = x + 0.5;

                var w0 = ((b.X - px) * (c.Y - py) - (c.X - px) * (b.Y - py)) * inverse;
                var w1 = ((c.X - px) * (a.Y - py) - (a.X - px) * (c.Y - py)) * inverse;
                var w2 = 1 - w0 - w1;
                if (w0 < 0 || w1 < 0 || w2 < 0) continue;

                var z = w0 * a.Z + w1 * b.Z + w2 * c.Z;
                var index = y * size + x;
                if (z >= depth[index]) continue;

                depth[index] = z;
                var o = index * 4;
                pixels[o] = red;
                pixels[o + 1] = green;
                pixels[o + 2] = blue;
                pixels[o + 3] = 255;
            }
        }
    }

    /// <summary>Box filter from the supersampled buffer down to the requested size.</summary>
    private static byte[] Downsample(byte[] source, int size, int scale)
    {
        var output = new byte[size * size * 4];
        var samples = scale * scale;

        for (var y = 0; y < size; y++)
        {
            for (var x = 0; x < size; x++)
            {
                int r = 0, g = 0, b = 0, a = 0;
                for (var dy = 0; dy < scale; dy++)
                {
                    for (var dx = 0; dx < scale; dx++)
                    {
                        var o = (((y * scale + dy) * size * scale) + (x * scale + dx)) * 4;
                        r += source[o];
                        g += source[o + 1];
                        b += source[o + 2];
                        a += source[o + 3];
                    }
                }

                var target = (y * size + x) * 4;
                output[target] = (byte)(r / samples);
                output[target + 1] = (byte)(g / samples);
                output[target + 2] = (byte)(b / samples);
                output[target + 3] = (byte)(a / samples);
            }
        }

        return output;
    }

    /// <summary>
    /// Draws a one-pixel dark rim around the silhouette. Without it a flat-shaded model reads as a
    /// blob at 64px; Factorio's own sprites use the same trick.
    /// </summary>
    private static byte[] AddOutline(byte[] pixels, int size, byte red, byte green, byte blue)
    {
        var output = (byte[])pixels.Clone();
        var rimR = (byte)(red * 0.22);
        var rimG = (byte)(green * 0.22);
        var rimB = (byte)(blue * 0.22);

        for (var y = 0; y < size; y++)
        {
            for (var x = 0; x < size; x++)
            {
                var index = y * size + x;
                if (pixels[index * 4 + 3] != 0) continue;

                var touches = false;
                for (var dy = -1; dy <= 1 && !touches; dy++)
                {
                    for (var dx = -1; dx <= 1 && !touches; dx++)
                    {
                        var nx = x + dx;
                        var ny = y + dy;
                        if (nx < 0 || ny < 0 || nx >= size || ny >= size) continue;
                        touches = pixels[(ny * size + nx) * 4 + 3] > 128;
                    }
                }

                if (!touches) continue;
                var o = index * 4;
                output[o] = rimR;
                output[o + 1] = rimG;
                output[o + 2] = rimB;
                output[o + 3] = 255;
            }
        }

        return output;
    }

    private static (double X, double Y, double Z) Normalise(double x, double y, double z)
    {
        var length = Math.Sqrt(x * x + y * y + z * z);
        return length < 1e-12 ? (0, 1, 0) : (x / length, y / length, z / length);
    }
}
