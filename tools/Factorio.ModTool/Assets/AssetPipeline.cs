using System.Security.Cryptography;
using Factorio.ModTool.Build;
using System.Text;
using System.Text.Json;

namespace Factorio.ModTool.Assets;

/// <summary>
/// The resource step of a mod build: a 3D model goes in, sprite PNGs and a Lua sprite table come
/// out.
///
/// The contract is <c>assets/render.json</c>:
/// <code>
/// {
///   "sprites": [
///     {
///       "model": "gas-extractor.obj",         // relative to assets/
///       "output": "graphics/entity/gas-extractor",  // relative to resources/, no extension
///       "size": 128,                          // square sprite size in pixels
///       "views": [0, 90, 180, 270],           // yaw angles; one PNG per view
///       "pitch": 35,                          // camera elevation in degrees
///       "zoom": 0.85,                         // 1.0 fills the frame edge to edge
///       "color": "#6E7A8A",                   // flat material colour
///       "lua": "lua/generated/gas-extractor.lua",   // optional; defaults to lua/generated/&lt;name&gt;.lua
///       "renderer": "builtin"                 // or "blender"
///     }
///   ]
/// }
/// </code>
///
/// The generated Lua returns the sprite table itself (one view) or a table keyed by direction
/// (<c>north</c>, <c>east</c>, ...), so <c>data.lua</c> can `require` it and drop it straight into a
/// prototype. Results are cached in <c>assets/.render.json</c> by a hash of the inputs, so a build
/// that changes nothing re-renders nothing.
/// </summary>
public static class AssetPipeline
{
    public sealed class SpriteSpec
    {
        public string Model { get; set; } = "";
        public string Output { get; set; } = "";
        public int Size { get; set; } = 128;
        public List<double> Views { get; set; } = new() { 0 };
        public double Pitch { get; set; } = 35;
        public double Zoom { get; set; } = 0.85;
        public string Color { get; set; } = "#6E7A8A";
        public string? Lua { get; set; }
        public string Renderer { get; set; } = "builtin";
    }

    public sealed class Spec
    {
        public List<SpriteSpec> Sprites { get; set; } = new();
    }

    public sealed record Result(int Rendered, int Skipped, IReadOnlyList<string> Outputs);

    private static readonly string[] Directions =
    {
        "north", "northeast", "east", "southeast", "south", "southwest", "west", "northwest",
    };

    /// <summary>Renders every sprite described by the mod's <c>assets/render.json</c>.</summary>
    /// <param name="settings">Toolchain paths (blender is looked up here).</param>
    /// <param name="project">The mod; its <c>assets/</c> directory is read.</param>
    /// <param name="force">Re-render even when the cache says the output is current.</param>
    /// <param name="log">Progress sink.</param>
    /// <returns>Counts and the files written.</returns>
    public static Result Run(ToolchainSettings settings, ModProject project, bool force, Action<string> log)
    {
        var assetsDirectory = Path.Combine(project.ProjectDirectory, "assets");
        var specPath = Path.Combine(assetsDirectory, "render.json");
        if (!File.Exists(specPath)) return new Result(0, 0, Array.Empty<string>());

        var spec = JsonSerializer.Deserialize<Spec>(File.ReadAllText(specPath), new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            ReadCommentHandling = JsonCommentHandling.Skip,
            AllowTrailingCommas = true,
        }) ?? throw new ToolException(specPath + ": empty render spec");

        if (spec.Sprites.Count == 0) throw new ToolException(specPath + ": no sprites");

        var cachePath = Path.Combine(assetsDirectory, ".render.json");
        var cache = LoadCache(cachePath);
        var outputs = new List<string>();
        int rendered = 0, skipped = 0;

        foreach (var sprite in spec.Sprites)
        {
            if (string.IsNullOrWhiteSpace(sprite.Model)) throw new ToolException(specPath + ": a sprite has no 'model'");
            if (string.IsNullOrWhiteSpace(sprite.Output)) throw new ToolException(specPath + ": a sprite has no 'output'");

            var modelPath = Path.Combine(assetsDirectory, sprite.Model);
            var hash = Hash(modelPath, sprite);
            var targets = TargetFiles(sprite, project);
            if (!force && cache.TryGetValue(sprite.Output, out var cached) && cached == hash && targets.All(File.Exists))
            {
                skipped++;
                log($"assets     {sprite.Output}: up to date");
                outputs.AddRange(targets);
                continue;
            }

            if (sprite.Renderer == "blender") RenderWithBlender(settings, project, assetsDirectory, sprite, modelPath);
            else RenderBuiltin(assetsDirectory, sprite, modelPath, targets);

            WriteLua(project, sprite);
            cache[sprite.Output] = hash;
            rendered++;
            outputs.AddRange(targets);
            log($"assets     {sprite.Output}: {sprite.Views.Count} view(s) at {sprite.Size}px");
        }

        SaveCache(cachePath, cache);
        return new Result(rendered, skipped, outputs);
    }

    private static void RenderBuiltin(string assetsDirectory, SpriteSpec sprite, string modelPath, List<string> targets)
    {
        var model = ObjModel.Load(modelPath);
        var (r, g, b) = ParseColor(sprite.Color);

        for (var i = 0; i < sprite.Views.Count && i < targets.Count; i++)
        {
            var options = new Renderer.Options(
                sprite.Size, sprite.Views[i], sprite.Pitch, sprite.Zoom, r, g, b);
            var pixels = Renderer.Render(model, options);
            Directory.CreateDirectory(Path.GetDirectoryName(targets[i])!);
            File.WriteAllBytes(targets[i], Png.Encode(sprite.Size, sprite.Size, pixels));
        }
    }

    /// <summary>
    /// Blender path: the same contract, rendered by a real renderer. The script is written next to
    /// the build output so a failure can be reproduced by hand.
    /// </summary>
    private static void RenderWithBlender(
        ToolchainSettings settings, ModProject project, string assetsDirectory, SpriteSpec sprite, string modelPath)
    {
        var blender = Shell.Find(settings.BlenderPath) ??
                      throw new ToolException(
                          "assets/render.json asks for the Blender renderer but 'blender' is not on PATH.\n" +
                          "Install Blender, or set renderer to \"builtin\" to use the software renderer.");

        var scriptPath = Path.Combine(settings.BuildDirectory, "obj", project.Name, "blender_render.py");
        Directory.CreateDirectory(Path.GetDirectoryName(scriptPath)!);
        File.WriteAllText(scriptPath, BlenderScript);

        var outputDirectory = Path.Combine(project.ProjectDirectory, "resources", Path.GetDirectoryName(sprite.Output) ?? "");
        Directory.CreateDirectory(outputDirectory);

        var run = Shell.Run(blender, new[]
        {
            "--background", "--python", scriptPath, "--",
            "--model", modelPath,
            "--out", Path.Combine(project.ProjectDirectory, "resources", sprite.Output),
            "--size", sprite.Size.ToString(),
            "--pitch", sprite.Pitch.ToString("0.##"),
            "--zoom", sprite.Zoom.ToString("0.##"),
            "--color", sprite.Color,
            "--views", string.Join(",", sprite.Views),
        });

        if (run.ExitCode != 0)
            throw new ToolException("Blender failed:\n" + ModBuilder.Tail(run.StdOut + "\n" + run.StdErr, 20));
    }

    /// <summary>Writes the Lua module that the data stage requires for this sprite.</summary>
    private static void WriteLua(ModProject project, SpriteSpec sprite)
    {
        var luaPath = sprite.Lua;
        if (string.IsNullOrWhiteSpace(luaPath))
        {
            var name = Path.GetFileName(sprite.Output);
            luaPath = "lua/generated/" + name + ".lua";
        }

        var full = Path.Combine(project.ProjectDirectory, luaPath);
        Directory.CreateDirectory(Path.GetDirectoryName(full)!);

        var prefix = "__" + project.Name + "__/";
        var sb = new StringBuilder();
        sb.AppendLine("-- Generated by `factorio-mod assets` from assets/" + sprite.Model + ". Do not edit.");
        sb.AppendLine($"-- {sprite.Views.Count} view(s), {sprite.Size}x{sprite.Size}, renderer: {sprite.Renderer}.");
        sb.AppendLine();

        string Entry(int index) =>
            "{ filename = \"" + prefix + sprite.Output.Replace('\\', '/') + FileSuffix(sprite, index) + "\", width = " +
            sprite.Size + ", height = " + sprite.Size + " }";

        if (sprite.Views.Count == 1)
        {
            sb.AppendLine("return " + Entry(0));
        }
        else
        {
            sb.AppendLine("return {");
            for (var i = 0; i < sprite.Views.Count; i++)
                sb.AppendLine("  " + DirectionName(sprite.Views[i], i) + " = " + Entry(i) + ",");
            sb.AppendLine("}");
        }

        File.WriteAllText(full, sb.ToString(), new UTF8Encoding(false));
    }

    private static string FileSuffix(SpriteSpec sprite, int index) =>
        sprite.Views.Count == 1 ? ".png" : "-" + index + ".png";

    private static List<string> TargetFiles(SpriteSpec sprite, ModProject project) =>
        Enumerable.Range(0, sprite.Views.Count)
            .Select(i => Path.Combine(project.ProjectDirectory, "resources",
                sprite.Output.Replace('\\', '/') + FileSuffix(sprite, i)))
            .ToList();

    /// <summary>The nearest of the eight compass names for a yaw angle.</summary>
    private static string DirectionName(double degrees, int index)
    {
        var normalised = ((degrees % 360) + 360) % 360;
        var octant = (int)Math.Round(normalised / 45.0) % 8;
        var name = Directions[octant];

        // Duplicate angles would collide in the returned table; fall back to an index suffix.
        return name + (index == 0 || normalised % 360 != 0 ? "" : index.ToString());
    }

    private static (byte R, byte G, byte B) ParseColor(string color)
    {
        var text = color.TrimStart('#');
        if (text.Length != 6 || !int.TryParse(text, System.Globalization.NumberStyles.HexNumber, null, out var value))
            throw new ToolException($"'{color}' is not a #RRGGBB colour");
        return ((byte)(value >> 16), (byte)(value >> 8), (byte)value);
    }

    private static string Hash(string modelPath, SpriteSpec sprite)
    {
        var input = string.Join("|",
            File.Exists(modelPath) ? Convert.ToHexString(SHA256.HashData(File.ReadAllBytes(modelPath))) : "missing",
            sprite.Model, sprite.Size, sprite.Pitch, sprite.Zoom, sprite.Color, sprite.Renderer,
            string.Join(",", sprite.Views));
        return Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(input)))[..16].ToLowerInvariant();
    }

    private static Dictionary<string, string> LoadCache(string path)
    {
        if (!File.Exists(path)) return new Dictionary<string, string>(StringComparer.Ordinal);
        try
        {
            return JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText(path))
                   ?? new Dictionary<string, string>(StringComparer.Ordinal);
        }
        catch (JsonException)
        {
            return new Dictionary<string, string>(StringComparer.Ordinal);
        }
    }

    private static void SaveCache(string path, Dictionary<string, string> cache)
    {
        var ordered = cache.OrderBy(pair => pair.Key, StringComparer.Ordinal)
            .ToDictionary(pair => pair.Key, pair => pair.Value, StringComparer.Ordinal);
        File.WriteAllText(path, JsonSerializer.Serialize(ordered, new JsonSerializerOptions { WriteIndented = true }) + "\n");
    }

    /// <summary>
    /// The Blender side of the pipeline. Renders each requested yaw with an orthographic camera and
    /// a sun lamp, transparent film, then writes <c>&lt;out&gt;-&lt;index&gt;.png</c>.
    /// </summary>
    private const string BlenderScript = """
        # Generated by factorio-mod. Blender 3.x/4.x: blender --background --python this.py -- args
        import sys
        import math
        import bpy

        argv = sys.argv[sys.argv.index("--") + 1:] if "--" in sys.argv else []
        opts = dict(zip(argv[0::2], argv[1::2]))

        model = opts["--model"]
        out = opts["--out"]
        size = int(opts["--size"])
        pitch = math.radians(float(opts["--pitch"]))
        zoom = float(opts["--zoom"])
        color = opts["--color"].lstrip("#")
        rgb = tuple(int(color[i:i+2], 16) / 255.0 for i in (0, 2, 4))
        views = [float(v) for v in opts["--views"].split(",") if v.strip()]

        bpy.ops.wm.read_factory_settings(use_empty=True)
        bpy.ops.wm.obj_import(filepath=model)

        mesh_objects = [o for o in bpy.context.scene.objects if o.type == "MESH"]
        if not mesh_objects:
            raise SystemExit("no mesh objects in " + model)

        material = bpy.data.materials.new("sprite")
        material.use_nodes = True
        bsdf = material.node_tree.nodes["Principled BSDF"]
        bsdf.inputs["Base Color"].default_value = (*rgb, 1.0)
        bsdf.inputs["Roughness"].default_value = 0.8
        for o in mesh_objects:
            o.data.materials.clear()
            o.data.materials.append(material)

        camera_data = bpy.data.cameras.new("camera")
        camera_data.type = "ORTHO"
        camera_data.ortho_scale = max(
            max(o.dimensions.x, o.dimensions.y, o.dimensions.z) for o in mesh_objects) / zoom
        camera = bpy.data.objects.new("camera", camera_data)
        bpy.context.scene.collection.objects.link(camera)
        bpy.context.scene.camera = camera

        sun_data = bpy.data.lights.new("sun", type="SUN")
        sun_data.energy = 4.0
        sun = bpy.data.objects.new("sun", sun_data)
        bpy.context.scene.collection.objects.link(sun)
        sun.rotation_euler = (math.radians(50), math.radians(10), math.radians(30))

        scene = bpy.context.scene
        scene.render.engine = "BLENDER_EEVEE_NEXT" if hasattr(bpy.types, "RenderEngineEeveeNext") else "BLENDER_EEVEE"
        scene.render.film_transparent = True
        scene.render.resolution_x = size
        scene.render.resolution_y = size
        scene.render.image_settings.file_format = "PNG"
        scene.render.image_settings.color_mode = "RGBA"
        scene.world = bpy.data.worlds.new("world")
        scene.world.use_nodes = True
        scene.world.node_tree.nodes["Background"].inputs[0].default_value = (0.6, 0.6, 0.6, 1.0)
        scene.world.node_tree.nodes["Background"].inputs[1].default_value = 0.6

        centre = (
            sum(o.location.x for o in mesh_objects) / len(mesh_objects),
            sum(o.location.y for o in mesh_objects) / len(mesh_objects),
            sum(o.location.z for o in mesh_objects) / len(mesh_objects),
        )
        radius = camera_data.ortho_scale * 3.0

        for index, yaw_degrees in enumerate(views):
            yaw = math.radians(yaw_degrees)
            camera.location = (
                centre[0] + radius * math.sin(yaw) * math.cos(pitch),
                centre[1] - radius * math.cos(yaw) * math.cos(pitch),
                centre[2] + radius * math.sin(pitch),
            )
            camera.rotation_euler = (math.pi / 2 - pitch, 0.0, yaw)
            scene.render.filepath = out + ("-%d.png" % index)
            bpy.ops.render.render(write_still=True)
        """;
}
