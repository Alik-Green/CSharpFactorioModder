using System.Globalization;

namespace Factorio.ModTool.Assets;

/// <summary>
/// A Wavefront <c>.obj</c> mesh: triangles plus the material name each one uses. Only the subset a
/// hand-made or Blender-exported model needs is read: <c>v</c>, <c>f</c>, <c>usemtl</c> and
/// comments. Normals and UVs are ignored - the renderer shades flat and Factorio only needs the
/// silhouette.
/// </summary>
public sealed class ObjModel
{
    public sealed record Triangle(int A, int B, int C, string Material);

    public List<(double X, double Y, double Z)> Vertices { get; } = new();
    public List<Triangle> Triangles { get; } = new();

    /// <summary>Reads a model from disk.</summary>
    /// <param name="path">Path to the <c>.obj</c> file.</param>
    /// <returns>The parsed model.</returns>
    public static ObjModel Load(string path)
    {
        if (!File.Exists(path)) throw new ToolException("model not found: " + path);

        var model = new ObjModel();
        var material = "default";
        var lineNumber = 0;

        foreach (var raw in File.ReadLines(path))
        {
            lineNumber++;
            var line = raw.Trim();
            if (line.Length == 0 || line[0] == '#') continue;

            var parts = line.Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries);
            switch (parts[0])
            {
                case "v":
                {
                    if (parts.Length < 4) throw Bad(path, lineNumber, "a vertex needs three coordinates");
                    model.Vertices.Add((Num(parts[1]), Num(parts[2]), Num(parts[3])));
                    break;
                }
                case "usemtl":
                    material = parts.Length > 1 ? parts[1] : "default";
                    break;
                case "f":
                {
                    if (parts.Length < 4) throw Bad(path, lineNumber, "a face needs at least three vertices");
                    var indices = new List<int>();
                    for (var i = 1; i < parts.Length; i++) indices.Add(Index(parts[i], model.Vertices.Count, path, lineNumber));

                    // Fan triangulation covers quads and n-gons.
                    for (var i = 1; i + 1 < indices.Count; i++)
                        model.Triangles.Add(new Triangle(indices[0], indices[i], indices[i + 1], material));
                    break;
                }
            }
        }

        if (model.Vertices.Count == 0) throw new ToolException($"{path}: no vertices");
        if (model.Triangles.Count == 0) throw new ToolException($"{path}: no faces");
        return model;
    }

    /// <summary>The axis-aligned bounds of the mesh, used to frame the camera.</summary>
    /// <returns>Minimum and maximum corners.</returns>
    public ((double X, double Y, double Z) Min, (double X, double Y, double Z) Max) Bounds()
    {
        var min = (X: double.MaxValue, Y: double.MaxValue, Z: double.MaxValue);
        var max = (X: double.MinValue, Y: double.MinValue, Z: double.MinValue);
        foreach (var v in Vertices)
        {
            min = (Math.Min(min.X, v.X), Math.Min(min.Y, v.Y), Math.Min(min.Z, v.Z));
            max = (Math.Max(max.X, v.X), Math.Max(max.Y, v.Y), Math.Max(max.Z, v.Z));
        }
        return (min, max);
    }

    private static double Num(string text) =>
        double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out var value)
            ? value
            : throw new ToolException($"'{text}' is not a number");

    /// <summary>`f 1/2/3`, `f 1//3` and `f 1` all name the same vertex; only the first field matters.</summary>
    private static int Index(string field, int count, string path, int line)
    {
        var slash = field.IndexOf('/');
        var text = slash < 0 ? field : field.Substring(0, slash);
        if (!int.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out var index))
            throw Bad(path, line, $"'{field}' is not a vertex reference");

        // OBJ indices are 1-based and may be negative (relative to the end).
        index = index < 0 ? count + index + 1 : index;
        if (index < 1 || index > count) throw Bad(path, line, $"vertex {index} is out of range (1..{count})");
        return index - 1;
    }

    private static ToolException Bad(string path, int line, string message) =>
        new($"{path}:{line}: {message}");
}
