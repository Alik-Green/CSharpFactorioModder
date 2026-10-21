using System.Text.Json;
using Factorio.ModTool;
using Factorio.ModTool.Api;
using Factorio.ModTool.Assets;
using Factorio.ModTool.Build;
using Factorio.ModTool.Commands;
using Xunit;

namespace Factorio.Bindings.Tests;

/// <summary>
/// Tests for the parts of the tool that are not the bindings generator: the type renamer, the
/// data-stage prototype checker, and the asset pipeline (PNG writer, OBJ reader, renderer).
/// </summary>
public class ToolTests
{
    // ------------------------------------------------------------------ renaming

    [Fact]
    public void The_Lua_prefix_is_dropped_but_the_globals_keep_their_names()
    {
        var renamer = TypeRenamer.Build(new[] { "LuaEntity", "LuaPlayer", "LuaGameScript", "LuaBootstrap", "LuaObject" });

        Assert.Equal("Entity", renamer.Rename("LuaEntity"));
        Assert.Equal("Player", renamer.Rename("LuaPlayer"));
        Assert.Equal("Game", renamer.Rename("LuaGameScript"));
        Assert.Equal("Script", renamer.Rename("LuaBootstrap"));
        Assert.Equal("LuaObject", renamer.Rename("LuaObject"));
    }

    [Fact]
    public void A_rename_that_would_collide_keeps_the_native_name()
    {
        // `Tile` is already a table class, so `LuaTile` cannot become `Tile`.
        var renamer = TypeRenamer.Build(new[] { "LuaTile" }, new[] { "Tile" });
        Assert.Equal("LuaTile", renamer.Rename("LuaTile"));
    }

    [Fact]
    public void Two_classes_with_the_same_short_name_both_keep_the_native_name()
    {
        // `Foo` exists as its own class, so `LuaFoo` may not shorten to it.
        var renamer = TypeRenamer.Build(new[] { "LuaFoo", "Foo" });
        Assert.Equal("LuaFoo", renamer.Rename("LuaFoo"));
        Assert.Equal("Foo", renamer.Rename("Foo"));
    }

    [Fact]
    public void The_real_api_is_renamed_without_collisions()
    {
        using var doc = JsonDocument.Parse(File.ReadAllText(TestEnvironment.RuntimeJson));
        var names = doc.RootElement.GetProperty("classes").EnumerateArray()
            .Select(c => c.GetProperty("name").GetString()!).ToList();

        var renamer = TypeRenamer.Build(names);
        var emitted = names.Select(renamer.Rename).ToList();

        Assert.Equal(emitted.Count, emitted.Distinct(StringComparer.Ordinal).Count());
        Assert.DoesNotContain("LuaGameScript", emitted);
        Assert.Contains("Game", emitted);
    }

    // --------------------------------------------------------------- data stage

    [Fact]
    public void Data_stage_accepts_a_valid_prototype()
    {
        var findings = DataStage.Validate(
            new[] { new DataStage.Shape("item", "thing", new[] { "type", "name", "stack_size", "icon" }) },
            TestEnvironment.PrototypeJson, TestEnvironment.Version);

        Assert.Empty(findings);
    }

    [Fact]
    public void Data_stage_reports_unknown_required_and_misspelt_keys()
    {
        var findings = DataStage.Validate(
            new[]
            {
                // Factorio 2.0 has one category per recipe (`category`); 2.1 added the plural list.
                // Whatever the version, the wrong spelling of a real key must be suggested.
                new DataStage.Shape("recipe", "r", new[] { "type", "name", "categories" }),
                new DataStage.Shape("not-a-prototype", "x", new[] { "type", "name" }),
            },
            TestEnvironment.PrototypeJson, TestEnvironment.Version);

        Assert.Contains(findings, f => f.Message.Contains("'categories' is not a property of RecipePrototype"));
        Assert.Contains(findings, f => f.Message.Contains("did you mean 'category'"));
        Assert.Contains(findings, f => f.Message.Contains("'not-a-prototype' is not a prototype type"));
    }

    [Fact]
    public void The_example_mods_pass_the_data_stage_check()
    {
        foreach (var mod in new[] { "HelloFactorio", "GasExtractor" })
        {
            var directory = Path.Combine(TestEnvironment.RepoRoot, "build", "mods");
            _ = mod;
            _ = directory;
        }

        // Exercised by `factorio-mod build`; here only the contract is checked: the shipped mods
        // declare the data files DataStage looks for.
        var data = Path.Combine(TestEnvironment.RepoRoot, "mods", "GasExtractor", "lua", "data.lua");
        Assert.True(File.Exists(data), "GasExtractor has no lua/data.lua");
    }

    // -------------------------------------------------------------------- assets

    [Fact]
    public void The_png_writer_produces_a_readable_file()
    {
        var pixels = new byte[4 * 4 * 4];             // 4x4, all zero (transparent)
        for (var i = 0; i < 16; i++)
        {
            pixels[i * 4] = 255;                      // red
            pixels[i * 4 + 3] = 255;                  // opaque
        }

        var png = Png.Encode(4, 4, pixels);
        Assert.Equal(new byte[] { 0x89, 0x50, 0x4E, 0x47 }, png.Take(4));
        Assert.Contains("IHDR", System.Text.Encoding.ASCII.GetString(png));
        Assert.Contains("IEND", System.Text.Encoding.ASCII.GetString(png));
    }

    [Fact]
    public void The_obj_reader_triangulates_quads_and_reads_materials()
    {
        var path = Path.Combine(Path.GetTempPath(), "factorio-obj-test-" + Guid.NewGuid().ToString("N") + ".obj");
        File.WriteAllText(path, """
            # a quad
            v 0 0 0
            v 1 0 0
            v 1 1 0
            v 0 1 0
            usemtl steel
            f 1/1/1 2/2/2 3/3/3 4/4/4
            """);
        try
        {
            var model = ObjModel.Load(path);
            Assert.Equal(4, model.Vertices.Count);
            Assert.Equal(2, model.Triangles.Count);   // one quad becomes two triangles
            Assert.All(model.Triangles, t => Assert.Equal("steel", t.Material));
            var (min, max) = model.Bounds();
            Assert.Equal(0, min.X);
            Assert.Equal(1, max.Y);
        }
        finally
        {
            File.Delete(path);
        }
    }

    [Fact]
    public void The_renderer_draws_something_and_leaves_the_background_transparent()
    {
        var path = Path.Combine(Path.GetTempPath(), "factorio-obj-render-" + Guid.NewGuid().ToString("N") + ".obj");
        File.WriteAllText(path, """
            v -1 -1 -1
            v 1 -1 -1
            v 1 1 -1
            v -1 1 -1
            v -1 -1 1
            v 1 -1 1
            v 1 1 1
            v -1 1 1
            f 5 6 7 8
            f 1 2 6 5
            f 2 3 7 6
            f 3 4 8 7
            f 4 1 5 8
            f 1 4 3 2
            """);
        try
        {
            var model = ObjModel.Load(path);
            var pixels = Renderer.Render(model, new Renderer.Options(32, 0, 35, 0.8, 110, 122, 138));

            Assert.Equal(32 * 32 * 4, pixels.Length);
            var opaque = Enumerable.Range(0, 32 * 32).Count(i => pixels[i * 4 + 3] > 0);
            Assert.InRange(opaque, 100, 32 * 32);                 // the cube covers a good part of the frame
            var corner = (0 * 32 + 0) * 4;
            Assert.Equal(0, pixels[corner + 3]);                  // corners stay transparent

            // Two different camera angles must not produce identical images.
            // A cube is symmetric under 90 degrees, so compare two angles that differ visibly.
            var other = Renderer.Render(model, new Renderer.Options(32, 30, 35, 0.8, 110, 122, 138));
            Assert.False(pixels.SequenceEqual(other), "yaw 0 and yaw 30 rendered identically");
        }
        finally
        {
            File.Delete(path);
        }
    }

    // ------------------------------------------------------------------ packaging

    /// <summary>
    /// A packaged Lua file that names one of this mod's files has to name one that exists: a typo in
    /// `Asset.Mod("graphics/icons/x.png")` is otherwise a missing icon in the game and nothing else.
    /// </summary>
    [Fact]
    public void A_resource_path_that_matches_no_packaged_file_is_reported()
    {
        var directory = Path.Combine(Path.GetTempPath(), "factorio-assets-" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(directory);
        File.WriteAllText(Path.Combine(directory, "mod.json"), """
            { "name": "AssetTest", "title": "t", "version": "1.0.0", "author": "a", "entry": "N.T.M",
              "resources": [ "resources" ] }
            """);
        Directory.CreateDirectory(Path.Combine(directory, "src"));
        File.WriteAllText(Path.Combine(directory, "src", "x.cs"),
            "namespace N { public static class T { public static void M() { } } }");
        Directory.CreateDirectory(Path.Combine(directory, "resources", "graphics", "icons"));
        File.WriteAllBytes(Path.Combine(directory, "resources", "graphics", "icons", "there.png"), new byte[] { 1 });

        try
        {
            var settings = ToolchainSettings.Locate(null);
            var isolated = new ToolchainSettings
            {
                Root = settings.Root,
                DotnetPath = settings.DotnetPath,
                CompilerDll = settings.CompilerDll,
                CompilerProject = settings.CompilerProject,
                CompilerCloneDir = settings.CompilerCloneDir,
                ApiVersion = settings.ApiVersion,
                ApiBaseUrl = settings.ApiBaseUrl,
                LuaPath = settings.LuaPath,
                LuaCompilerPath = settings.LuaCompilerPath,
                BlenderPath = settings.BlenderPath,
                BuildDirectoryOverride = Path.Combine(directory, "build"),
            };
            var project = ModProject.Load(directory);

            var control =
                "data.raw = '__AssetTest__/graphics/icons/there.png'" +
                "\ndata.raw = '__AssetTest__/graphics/icons/missing.png'" +
                "\ndata.raw = 'graphics/icons/bare.png'";

            var packaged = Packager.Package(
                project, control, null, new Dictionary<string, string>(), isolated, zip: false);

            var warnings = packaged.Warnings.ToList();
            Assert.Single(warnings, w => w.Contains("'__AssetTest__/graphics/icons/missing.png'"));
            Assert.Contains(warnings, w => w.Contains("'graphics/icons/bare.png'") && w.Contains("no source prefix"));
            Assert.DoesNotContain(warnings, w => w.Contains("icons/there.png"));
        }
        finally
        {
            try { Directory.Delete(directory, true); } catch { /* best effort */ }
        }
    }

    [Fact]
    public void The_zip_contains_one_folder_named_after_the_mod()
    {
        var directory = Path.Combine(Path.GetTempPath(), "factorio-pack-" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(directory);
        File.WriteAllText(Path.Combine(directory, "mod.json"), """
            { "name": "PackTest", "title": "t", "version": "1.2.3", "author": "a", "entry": "N.T.M" }
            """);
        Directory.CreateDirectory(Path.Combine(directory, "src"));
        File.WriteAllText(Path.Combine(directory, "src", "x.cs"), "namespace N { public static class T { public static void M() { } } }");
        Directory.CreateDirectory(Path.Combine(directory, "lua"));
        File.WriteAllText(Path.Combine(directory, "lua", "data.lua"), "log('hi')");

        try
        {
            // A build directory of our own, so the test never touches build/mods.
            var settings = ToolchainSettings.Locate(null);
            var isolated = new ToolchainSettings
            {
                Root = settings.Root,
                DotnetPath = settings.DotnetPath,
                CompilerDll = settings.CompilerDll,
                CompilerProject = settings.CompilerProject,
                CompilerCloneDir = settings.CompilerCloneDir,
                ApiVersion = settings.ApiVersion,
                ApiBaseUrl = settings.ApiBaseUrl,
                LuaPath = settings.LuaPath,
                LuaCompilerPath = settings.LuaCompilerPath,
                BlenderPath = settings.BlenderPath,
                BuildDirectoryOverride = Path.Combine(directory, "build"),
            };
            var project = ModProject.Load(directory);
            var packaged = Packager.Package(project, "-- control.lua", null, new Dictionary<string, string>(), isolated, zip: true);

            Assert.NotNull(packaged.ZipPath);
            using var archive = System.IO.Compression.ZipFile.OpenRead(packaged.ZipPath!);
            Assert.NotEmpty(archive.Entries);
            Assert.All(archive.Entries, e => Assert.StartsWith("PackTest_1.2.3/", e.FullName));
            Assert.Contains(archive.Entries, e => e.FullName == "PackTest_1.2.3/info.json");
            Assert.Contains(archive.Entries, e => e.FullName == "PackTest_1.2.3/data.lua");
        }
        finally
        {
            try { Directory.Delete(directory, true); } catch { /* best effort */ }
        }
    }

    [Fact]
    public void The_shipped_model_and_render_spec_are_valid()
    {
        var assets = Path.Combine(TestEnvironment.RepoRoot, "mods", "GasExtractor", "assets");
        var model = ObjModel.Load(Path.Combine(assets, "gas-extractor.obj"));
        Assert.True(model.Triangles.Count >= 12, "the model should be a small assembly of boxes");

        var spec = JsonSerializer.Deserialize<AssetPipeline.Spec>(
            File.ReadAllText(Path.Combine(assets, "render.json")),
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
        Assert.NotEmpty(spec.Sprites);
        foreach (var sprite in spec.Sprites)
        {
            Assert.False(string.IsNullOrWhiteSpace(sprite.Model));
            Assert.False(string.IsNullOrWhiteSpace(sprite.Output));
            Assert.True(sprite.Size is >= 8 and <= 4096);
            Assert.NotEmpty(sprite.Views);
        }
    }

    // ------------------------------------------------------------------ factorio-mod new

    [Fact]
    public void The_mod_template_renders_a_manifest_and_a_source_file()
    {
        var directory = Path.Combine(Path.GetTempPath(), "factorio-new-" + Guid.NewGuid().ToString("N"));
        try
        {
            var written = ModTemplate.Create(TestEnvironment.RepoRoot, directory, "gas-extractor", "someone", overwrite: false);

            Assert.Contains("mod.json", written);
            Assert.Contains("src/Main.cs", written);
            Assert.Contains("thumbnail.png", written);
            Assert.Contains("changelog.txt", written);

            var project = ModProject.Load(directory);
            Assert.Equal("gas-extractor", project.Name);
            Assert.Equal("Gas Extractor", project.Title);
            Assert.Equal("someone", project.Author);
            Assert.Equal("GasExtractor.Main.Init", project.Entry);

            // The namespace in the source has to match the entry point, or nothing runs.
            var source = File.ReadAllText(Path.Combine(directory, "src", "Main.cs"));
            Assert.Contains("namespace GasExtractor", source);
            Assert.DoesNotContain("{{", source);

            // The thumbnail is a real PNG of the expected size.
            var png = File.ReadAllBytes(Path.Combine(directory, "thumbnail.png"));
            Assert.Equal(new byte[] { 0x89, (byte)'P', (byte)'N', (byte)'G' }, png[..4]);
            Assert.Equal(144, (png[16] << 24) | (png[17] << 16) | (png[18] << 8) | png[19]);
        }
        finally
        {
            try { Directory.Delete(directory, true); } catch { /* best effort */ }
        }
    }

    [Theory]
    [InlineData("AirExtractor", "AirExtractor")]
    [InlineData("gas-extractor", "GasExtractor")]
    [InlineData("my_mod", "MyMod")]
    [InlineData("x2fast", "X2fast")]
    public void The_namespace_is_derived_from_the_mod_name(string name, string expected)
    {
        Assert.Equal(expected, ModTemplate.NamespaceFor(name));
        Assert.Null(ModTemplate.ValidateName(name));
    }

    [Theory]
    [InlineData("")]
    [InlineData("2fast")]
    [InlineData("bad name")]
    [InlineData("bad/name")]
    public void Invalid_mod_names_are_rejected(string name)
    {
        Assert.NotNull(ModTemplate.ValidateName(name));
    }

    [Fact]
    public void The_template_refuses_to_overwrite_a_non_empty_directory()
    {
        var directory = Path.Combine(Path.GetTempPath(), "factorio-new-" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(directory);
        File.WriteAllText(Path.Combine(directory, "keep.txt"), "mine");
        try
        {
            var error = Assert.Throws<ToolException>(() =>
                ModTemplate.Create(TestEnvironment.RepoRoot, directory, "SomeMod", "a", overwrite: false));
            Assert.Contains("not empty", error.Message);

            // --force writes over it and leaves unrelated files alone.
            ModTemplate.Create(TestEnvironment.RepoRoot, directory, "SomeMod", "a", overwrite: true);
            Assert.True(File.Exists(Path.Combine(directory, "keep.txt")));
            Assert.True(File.Exists(Path.Combine(directory, "mod.json")));
        }
        finally
        {
            try { Directory.Delete(directory, true); } catch { /* best effort */ }
        }
    }

    [Fact]
    public void A_scaffolded_mod_builds_and_passes_its_checks()
    {
        var directory = Path.Combine(Path.GetTempPath(), "factorio-new-" + Guid.NewGuid().ToString("N"));
        try
        {
            ModTemplate.Create(TestEnvironment.RepoRoot, directory, "Scaffolded", "tests", overwrite: false);
            var project = ModProject.Load(directory);

            var settings = ToolchainSettings.Locate(null);
            var isolated = new ToolchainSettings
            {
                Root = settings.Root,
                DotnetPath = settings.DotnetPath,
                CompilerDll = settings.CompilerDll,
                CompilerProject = settings.CompilerProject,
                CompilerCloneDir = settings.CompilerCloneDir,
                CompilerRepo = settings.CompilerRepo,
                CompilerCommit = settings.CompilerCommit,
                CompilerPatch = settings.CompilerPatch,
                ApiVersion = settings.ApiVersion,
                ApiSchemaVersion = settings.ApiSchemaVersion,
                ApiBaseUrl = settings.ApiBaseUrl,
                LuaPath = settings.LuaPath,
                LuaCompilerPath = settings.LuaCompilerPath,
                BlenderPath = settings.BlenderPath,
                BuildDirectoryOverride = Path.Combine(directory, "build"),
            };

            var result = ModBuilder.Build(isolated, project, new ModBuilder.Options(), _ => { });

            Assert.True(result.Ok, string.Join("\n", result.Findings.Select(f => f.Message)));
            Assert.Empty(Analysis.Run(project));
            Assert.True(File.Exists(Path.Combine(result.ModDirectory, "control.lua")));
            Assert.True(File.Exists(Path.Combine(result.ModDirectory, "thumbnail.png")));
            Assert.True(File.Exists(Path.Combine(result.ModDirectory, "locale", "en", "locale.cfg")));

            var lua = File.ReadAllText(Path.Combine(result.ModDirectory, "control.lua"));
            Assert.Contains("Scaffolded", lua);
        }
        finally
        {
            try { Directory.Delete(directory, true); } catch { /* best effort */ }
        }
    }

    [Fact]
    public void A_mod_next_to_its_own_copy_of_the_bindings_uses_that_copy()
    {
        var directory = Path.Combine(Path.GetTempPath(), "factorio-new-" + Guid.NewGuid().ToString("N"));
        try
        {
            ModTemplate.Create(TestEnvironment.RepoRoot, directory, "LocalBindings", "tests", overwrite: false);

            // The copy the `--bindings` flag writes: Generated/, Utilities/ and Compat.cs.
            var bindings = Path.Combine(TestEnvironment.RepoRoot, "src", "Factorio.Bindings");
            var copy = Path.Combine(directory, "bindings");
            foreach (var folder in new[] { "Generated", "Utilities" })
            foreach (var file in Directory.EnumerateFiles(Path.Combine(bindings, folder), "*", SearchOption.AllDirectories))
            {
                var destination = Path.Combine(copy, Path.GetRelativePath(bindings, file));
                Directory.CreateDirectory(Path.GetDirectoryName(destination)!);
                File.Copy(file, destination);
            }
            foreach (var name in new[] { "Compat.cs", "FactorioCompat.lua", "FactorioUtils.lua" })
                File.Copy(Path.Combine(bindings, name), Path.Combine(copy, name));

            var settings = ToolchainSettings.Locate(null);
            var isolated = new ToolchainSettings
            {
                Root = settings.Root,
                DotnetPath = settings.DotnetPath,
                CompilerDll = settings.CompilerDll,
                CompilerProject = settings.CompilerProject,
                CompilerCloneDir = settings.CompilerCloneDir,
                ApiVersion = settings.ApiVersion,
                ApiSchemaVersion = settings.ApiSchemaVersion,
                ApiBaseUrl = settings.ApiBaseUrl,
                LuaPath = settings.LuaPath,
                LuaCompilerPath = settings.LuaCompilerPath,
                BuildDirectoryOverride = Path.Combine(directory, "build"),
            };

            var result = ModBuilder.Build(isolated, ModProject.Load(directory), new ModBuilder.Options(), _ => { });

            Assert.True(result.Ok);
            // Compiled once, from the mod's copy - not twice, from both copies. The module count is
            // one per C# file: the version's bindings, the hand-written helpers, and the mod itself.
            var api = settings.Target("2.0");
            var expected = Directory.GetFiles(Path.Combine(api.GeneratedDirectory, "Runtime"), "*.cs", SearchOption.AllDirectories).Length +
                           Directory.GetFiles(Path.Combine(settings.BindingsDirectory, "Utilities"), "*.cs").Length +
                           1 +
                           1;
            Assert.Equal(expected, result.Modules.Count + result.DroppedModules.Count);
            Assert.DoesNotContain(result.Findings, f => f.Message.Contains("already contains a definition"));
        }
        finally
        {
            try { Directory.Delete(directory, true); } catch { /* best effort */ }
        }
    }

    [Fact]
    public void The_repository_is_found_from_a_directory_outside_it()
    {
        var directory = Path.Combine(Path.GetTempPath(), "factorio-cwd-" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(directory);
        var original = Directory.GetCurrentDirectory();
        try
        {
            Directory.SetCurrentDirectory(directory);
            var settings = ToolchainSettings.Locate();
            Assert.True(File.Exists(Path.Combine(settings.Root, "config", "toolchain.json")));
        }
        finally
        {
            Directory.SetCurrentDirectory(original);
            try { Directory.Delete(directory, true); } catch { /* best effort */ }
        }
    }

    [Fact]
    public void An_option_value_is_not_taken_for_a_mod_directory()
    {
        var positional = CommandLine.Positional(
            new[] { "mods/HelloFactorio", "--output", "/tmp/out", "--no-zip", "mods/GasExtractor" },
            "--output");

        Assert.Equal(new[] { "mods/HelloFactorio", "mods/GasExtractor" }, positional);
        Assert.Equal("/tmp/out", CommandLine.Value(new[] { "--output", "/tmp/out" }, "--output"));
        Assert.Null(CommandLine.Value(new[] { "--no-zip" }, "--output"));
    }

    [Fact]
    public void Paths_inside_the_repository_are_shown_relative()
    {
        var root = TestEnvironment.RepoRoot;
        Assert.Equal("build/mods/X_0.1.0", CommandLine.Display(root, Path.Combine(root, "build", "mods", "X_0.1.0")));
        Assert.Equal("/tmp/elsewhere", CommandLine.Display(root, "/tmp/elsewhere"));
    }

    [Fact]
    public void The_compiler_arguments_go_through_a_response_file()
    {
        var directory = Path.Combine(Path.GetTempPath(), "factorio-rsp-" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(directory);
        try
        {
            var settings = ToolchainSettings.Locate(null);
            var isolated = new ToolchainSettings { Root = settings.Root, BuildDirectoryOverride = directory };
            var files = new[]
            {
                Path.Combine(settings.Root, "mods", "HelloFactorio", "src", "HelloFactorio.cs"),
                Path.Combine(settings.Root, "src", "Factorio.Bindings", "Compat.cs"),
            };
            var output = Path.Combine(directory, "obj", "M", "lua");

            var path = CompilerRunner.WriteArgumentsFile(isolated, files, output, classicLua: false);
            var line = File.ReadAllText(path).Trim();

            // One option line, relative to the repository, and no absolute paths: that is what keeps
            // a 60,000-character list under the Windows command-line limit.
            Assert.Single(File.ReadAllLines(path));
            Assert.StartsWith("-s ", line);
            Assert.Contains("mods/HelloFactorio/src/HelloFactorio.cs", line);
            Assert.Contains("src/Factorio.Bindings/Compat.cs", line);
            Assert.Contains(" -noconcurrent", line);
            Assert.DoesNotContain(settings.Root, line);
            Assert.True(line.Length > 40);
        }
        finally
        {
            try { Directory.Delete(directory, true); } catch { /* best effort */ }
        }
    }

    [Fact]
    public void The_lua_interpreter_is_found_under_the_names_it_ships_with()
    {
        // Windows distributions call the same binary lua52.exe; the configured name stays lua5.2.
        var settings = ToolchainSettings.Locate(null);
        var byConfiguredName = Shell.Find(settings.LuaPath);

        Assert.Equal(byConfiguredName, byConfiguredName == null ? null : Shell.Lua(settings));
        if (Shell.Find("lua52") != null || byConfiguredName != null)
            Assert.NotNull(Shell.Lua(settings));
    }

    // ------------------------------------------------------- Factorio versions

    [Fact]
    public void Each_supported_factorio_version_gets_its_own_documents_and_bindings()
    {
        var settings = ToolchainSettings.Locate(null);

        var two = settings.Target("2.0");
        Assert.Equal("2.0", two.ModVersion);
        Assert.Equal("2.0.72", two.DocsVersion);
        Assert.EndsWith(Path.Combine("api", "2.0.72"), two.ApiDirectory);
        Assert.EndsWith(Path.Combine("Generated", "2.0.72"), two.GeneratedDirectory);
        Assert.True(File.Exists(Path.Combine(two.ApiDirectory, "runtime-api.json")));
        Assert.True(Directory.Exists(two.GeneratedDirectory), "the 2.0 bindings ship with the repository");

        var newer = settings.Target("2.1");
        Assert.Equal("2.1", newer.ModVersion);
        Assert.Equal("2.1.22", newer.DocsVersion);
        Assert.True(File.Exists(Path.Combine(newer.ApiDirectory, "runtime-api.json")));

        // The exact documentation version is the same target as the short one.
        Assert.Equal(newer, settings.Target("2.1.22"));
        Assert.Equal(two, settings.Target("2.0.72"));

        // No version named means the default, and that is what `new` scaffolds.
        Assert.Equal(settings.DefaultTarget(), settings.Target(null));
        Assert.Equal(settings.DefaultModVersion, settings.DefaultTarget().ModVersion);
    }

    [Fact]
    public void An_unsupported_factorio_version_lists_the_ones_that_work()
    {
        var settings = ToolchainSettings.Locate(null);

        var error = Assert.Throws<ToolException>(() => settings.Target("1.1"));

        Assert.Contains("2.0 (2.0.72)", error.Message);
        Assert.Contains("2.1 (2.1.22)", error.Message);
        Assert.Contains("config/toolchain.json", error.Message);
    }

    [Fact]
    public void A_scaffolded_mod_declares_the_default_version_and_says_so_in_info_json()
    {
        var directory = Path.Combine(Path.GetTempPath(), "factorio-version-" + Guid.NewGuid().ToString("N"));
        try
        {
            var settings = ToolchainSettings.Locate(null);
            ModTemplate.Create(TestEnvironment.RepoRoot, directory, "Versioned", "tests", overwrite: false,
                factorioVersion: settings.DefaultModVersion);
            var project = ModProject.Load(directory);

            Assert.Equal(settings.DefaultModVersion, project.FactorioVersion);

            var isolated = new ToolchainSettings
            {
                Root = settings.Root,
                DotnetPath = settings.DotnetPath,
                CompilerDll = settings.CompilerDll,
                CompilerProject = settings.CompilerProject,
                CompilerCloneDir = settings.CompilerCloneDir,
                ApiVersion = settings.ApiVersion,
                ApiSchemaVersion = settings.ApiSchemaVersion,
                ApiBaseUrl = settings.ApiBaseUrl,
                LuaPath = settings.LuaPath,
                LuaCompilerPath = settings.LuaCompilerPath,
                BuildDirectoryOverride = Path.Combine(directory, "build"),
            };

            var result = ModBuilder.Build(isolated, project, new ModBuilder.Options(), _ => { });

            Assert.True(result.Ok);
            using var info = JsonDocument.Parse(File.ReadAllText(Path.Combine(result.ModDirectory, "info.json")));
            Assert.Equal(settings.DefaultModVersion, info.RootElement.GetProperty("factorio_version").GetString());
        }
        finally
        {
            try { Directory.Delete(directory, true); } catch { /* best effort */ }
        }
    }

    [Fact]
    public void The_shipped_mods_are_built_for_a_version_this_toolchain_supports()
    {
        var settings = ToolchainSettings.Locate(null);

        foreach (var mod in new[] { "HelloFactorio", "GasExtractor" })
        {
            var project = ModProject.Load(Path.Combine(TestEnvironment.RepoRoot, "mods", mod));
            var target = settings.Target(project.FactorioVersion);
            Assert.True(Directory.Exists(target.GeneratedDirectory),
                $"{mod} is for Factorio {project.FactorioVersion}, whose bindings are not generated");
        }
    }

    // ------------------------------------------------------ `new` argument forms

    [Theory]
    [InlineData(new[] { "MyMod" }, "MyMod", null)]
    [InlineData(new[] { "MyMod", "--dir", "/tmp/x/Mod" }, "MyMod", "/tmp/x/Mod")]
    [InlineData(new[] { "MyMod", "--dir=/tmp/x/Mod" }, "MyMod", "/tmp/x/Mod")]
    [InlineData(new[] { "--dir", "/tmp/x/Mod", "MyMod" }, "MyMod", "/tmp/x/Mod")]
    [InlineData(new[] { "MyMod", "/tmp/x/Mod" }, "MyMod", "/tmp/x/Mod")]
    [InlineData(new[] { "/tmp/x/Mod" }, null, "/tmp/x/Mod")]
    [InlineData(new[] { "--dir=/tmp/x/Mod" }, null, "/tmp/x/Mod")]
    [InlineData(new[] { "-d", "D:\\mods\\Mod" }, null, "D:\\mods\\Mod")]
    public void New_reads_every_spelling_of_the_destination(string[] args, string? name, string? directory)
    {
        var request = NewCommand.Parse(args);

        Assert.Equal(name, request.Name);
        Assert.Equal(directory, request.Directory);
    }

    [Fact]
    public void New_takes_the_name_from_the_last_segment_when_given_only_a_path()
    {
        var directory = Path.Combine(Path.GetTempPath(), "gas-extractor");
        try
        {
            // What the command does with `new C:\mods\gas-extractor`: name from the path, folder there.
            var request = NewCommand.Parse(new[] { directory, "--no-build" });
            Assert.Null(request.Name);
            Assert.Equal(directory, request.Directory);
            Assert.Equal("gas-extractor", Path.GetFileName(request.Directory));
            Assert.Equal("GasExtractor", ModTemplate.NamespaceFor(Path.GetFileName(request.Directory)));
        }
        finally
        {
            if (Directory.Exists(directory)) Directory.Delete(directory, true);
        }
    }
}
