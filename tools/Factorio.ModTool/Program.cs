using Factorio.Analyzers;
using Factorio.ModTool.Commands;

namespace Factorio.ModTool;

/// <summary>
/// `factorio-mod` — the whole workflow in one executable:
/// bootstrap the pinned compiler, fetch the official API, generate the C# bindings, build a mod
/// into a Factorio-loadable folder and zip, and check the Lua that comes out.
/// </summary>
public static class Program
{
    private const string Usage = """
        factorio-mod — C# modding toolchain for Factorio

        usage:
          factorio-mod <command> [options]

        commands:
          setup [--offline] [--all]  one-time setup: compiler, API JSON, bindings
          new [NAME] [--dir DIR]     scaffold a new mod (and build it once)
          bootstrap [--force]        clone, patch and build the pinned CSharp.lua compiler
          fetch-api [--factorio V] [--all] [--offline]
                                     download the official API JSON and record its sha256
          generate [--factorio V] [--all] [--out DIR]
                                     generate the C# bindings from the downloaded JSON
          build [MOD_DIR...] [opts]  render assets, compile a mod to Lua, package it, check it
          assets [MOD_DIR...] [--force]  render models to sprites without building
          analyze [MOD_DIR...]       run the Factorio analyzers over the mod sources
          dist [--output DIR] [--version V] [--runtime RID] [--plugin DIR] [--no-publish]
                                     assemble the installable tree an installer ships
          template [--uninstall] [--list]
                                     install the mod template into `dotnet new`, so Rider's New
                                     Solution dialog lists "Factorio mod (C#)"
          plugin [--jdk DIR] [--idea V] [--offline]
                                     build the Rider/IntelliJ plugin from rider-plugin/ into a zip
          doctor                     print the resolved toolchain and its status

        common options:
          --repo DIR                 repository root (default: search upward for config/toolchain.json)

        version options:
          --factorio V               Factorio version: 2.0 (default) or 2.1, or an exact 2.0.72
          --all                      every version listed in config/toolchain.json

        new options:
          --dir DIR                  where to create the mod (default: ./NAME, or $FACTORIO_MOD_DIR)
          --author NAME              author for mod.json (default: $FACTORIO_MOD_AUTHOR, $USER)
          --bindings                 copy the generated bindings into the mod (self-contained)
          --no-build                 write the files but do not run the first build
          --force                    write into a directory that already has files

        build options (each overrides the same setting in mod.json's "build" object):
          --zip / --no-zip           write <Name>_<version>.zip (default: yes)
          --regenerate               regenerate the bindings before compiling
          --validate / --no-validate run the Lua 5.2 and CoreSystem checks (default: yes)
          --lua51                    target Lua 5.1 syntax (Factorio uses 5.2; rarely needed)
          --assets / --no-assets     render assets/render.json before building (default: yes)
          --analyze / --no-analyze   run the C# analyzers (default: yes)
          --output DIR               write the mod folder and zip to DIR, not build/mods
          --format text|msbuild      how findings are printed; msbuild is what an IDE parses

        mod.json "build" object:
          { "output": "out", "zip": false, "analyze": true, "assets": true,
            "validate": true, "lua51": false, "regenerate": false, "format": "msbuild" }

        plugin options:
          --jdk DIR                  JDK 17+ to compile the plugin with (default: JAVA_HOME, PATH, cache)
          --idea VERSION|DIR|ZIP     IntelliJ Platform to compile against (default: 2024.2.5)
          --offline                  never download: use what is already cached
          --output DIR               where the plugin zip goes (default: build/plugin)
        """;

    public static int Main(string[] args)
    {
        try
        {
            if (args.Length == 0 || args[0] is "-h" or "--help" or "help")
            {
                Console.WriteLine(Usage);
                return args.Length == 0 ? 1 : 0;
            }

            if (args[0] is "--version" or "-V" or "version")
            {
                Console.WriteLine("factorio-mod " + Version());
                return 0;
            }

            var command = args[0];
            var rest = args.Skip(1).ToArray();
            var repoArg = IndexOfValue(rest, "--repo");
            var settings = ToolchainSettings.Locate(repoArg);
            if (repoArg != null) rest = RemovePair(rest, "--repo");

            return command switch
            {
                "setup" => SetupCommand.Run(settings, rest),
                "new" or "init" or "create" => NewCommand.Run(settings, rest),
                "bootstrap" => BootstrapCommand.Run(settings, rest),
                "fetch-api" => FetchApiCommand.Run(settings, rest),
                "generate" => GenerateCommand.Run(settings, rest),
                "build" => BuildCommand.Run(settings, rest),
                "assets" => AssetsCommand.Run(settings, rest),
                "analyze" => AnalyzeCommand.Run(settings, rest),
                "dist" => DistCommand.Run(settings, rest),
                "template" => TemplateCommand.Run(settings, rest),
                "plugin" => PluginCommand.Run(settings, rest),
                "doctor" => Doctor(settings),
                _ => Unknown(command),
            };
        }
        catch (ToolException e)
        {
            Console.Error.WriteLine("error: " + e.Message);
            return 1;
        }
    }

    private static readonly string[] Commands =
    {
        "setup", "new", "bootstrap", "fetch-api", "generate", "build", "assets", "analyze", "doctor",
    };

    private static int Unknown(string command)
    {
        Console.Error.WriteLine($"unknown command '{command}'");
        if (command is "tool" or "make")
            Console.Error.WriteLine(
                "  ('tool' is a make target, not a command: `make tool` from the repository root builds this" +
                " executable. The one-time setup is `factorio-mod setup`.)");
        else
        {
            var suggestion = Nearest(command);
            if (suggestion != null) Console.Error.WriteLine($"  did you mean '{suggestion}'?");
        }

        Console.WriteLine(Usage);
        return 1;
    }

    /// <summary>The closest command name, or null when nothing is close.</summary>
    private static string? Nearest(string command)
    {
        string? best = null;
        var bestDistance = int.MaxValue;
        foreach (var candidate in Commands)
        {
            var distance = Distance(command, candidate);
            if (distance >= bestDistance) continue;
            best = candidate;
            bestDistance = distance;
        }

        return bestDistance <= 3 ? best : null;
    }

    /// <summary>Levenshtein distance, for "did you mean".</summary>
    private static int Distance(string left, string right)
    {
        var previous = new int[right.Length + 1];
        var current = new int[right.Length + 1];
        for (var j = 0; j <= right.Length; j++) previous[j] = j;

        for (var i = 1; i <= left.Length; i++)
        {
            current[0] = i;
            for (var j = 1; j <= right.Length; j++)
            {
                var cost = left[i - 1] == right[j - 1] ? 0 : 1;
                current[j] = Math.Min(Math.Min(current[j - 1] + 1, previous[j] + 1), previous[j - 1] + cost);
            }

            (previous, current) = (current, previous);
        }

        return previous[right.Length];
    }

    /// <summary>The tool's own version, stamped at publish time from the release tag.</summary>
    /// <returns>e.g. <c>1.2.3</c>.</returns>
    private static string Version()
    {
        var assembly = typeof(Program).Assembly;
        var informational = assembly
            .GetCustomAttributes(typeof(System.Reflection.AssemblyInformationalVersionAttribute), false)
            .OfType<System.Reflection.AssemblyInformationalVersionAttribute>()
            .FirstOrDefault()?.InformationalVersion;

        return (informational ?? assembly.GetName().Version?.ToString() ?? "0.0.0").Split('+')[0];
    }

    private static string? IndexOfValue(string[] args, string name)
    {
        for (var i = 0; i < args.Length - 1; i++)
            if (args[i] == name) return args[i + 1];
        return null;
    }

    private static string[] RemovePair(string[] args, string name)
    {
        var list = args.ToList();
        var i = list.IndexOf(name);
        if (i >= 0)
        {
            list.RemoveAt(i);
            if (i < list.Count) list.RemoveAt(i);
        }
        return list.ToArray();
    }

    /// <summary>`factorio-mod doctor`: what would the next build actually use?</summary>
    private static int Doctor(ToolchainSettings settings)
    {
        Console.WriteLine("repository   " + settings.Root);
        Console.WriteLine("dotnet       " + (Shell.Find(settings.DotnetPath) ?? "MISSING") + " (" + settings.DotnetPath + ")");
        Console.WriteLine("CSharp.lua   " + (File.Exists(settings.CompilerDll) ? settings.CompilerDll : "MISSING - run `factorio-mod bootstrap`"));
        Console.WriteLine("api          default " + settings.ApiVersion + " (api_version " + settings.ApiSchemaVersion + ")");
        foreach (var target in settings.Targets())
        {
            var documents = new[] { "runtime-api.json", "prototype-api.json" }
                .All(file => File.Exists(Path.Combine(target.ApiDirectory, file)));
            var bindings = Directory.Exists(Path.Combine(target.GeneratedDirectory, "Runtime"));
            var state = (documents ? "documents" : "MISSING documents - run `factorio-mod fetch-api --factorio " + target.ModVersion + "`") +
                        ", " +
                        (bindings ? "bindings" : "MISSING bindings - run `factorio-mod generate --factorio " + target.ModVersion + "`");
            Console.WriteLine($"             Factorio {target.ModVersion,-4} {target.DocsVersion,-8} {state}");
        }
        var lua = Shell.Lua(settings);
        var luac = Shell.Luac(settings);
        var luaVersion = lua == null ? null : Shell.Version(lua);
        Console.WriteLine("lua          " + (lua == null
            ? "MISSING (data-stage checks will be skipped; set FACTORIO_LUA)"
            : lua + (luaVersion == null ? "" : "  (" + luaVersion + ")")));
        Console.WriteLine("luac         " + (luac == null
            ? "MISSING (syntax checks will be skipped; set FACTORIO_LUAC)"
            : luac + "  (" + Shell.Version(luac) + ")"));
        Console.WriteLine("analyzers    " + AnalyzerCatalog.Descriptors.Length + " rule(s) (" +
                          AnalyzerCatalog.Descriptors.First().Id + "-" + AnalyzerCatalog.Descriptors.Last().Id + ")");
        var mods = Directory.Exists(settings.ModsDirectory)
            ? Directory.EnumerateDirectories(settings.ModsDirectory).Where(d => File.Exists(Path.Combine(d, "mod.json")))
                .Select(Path.GetFileName).ToArray()
            : Array.Empty<string>();
        Console.WriteLine("mods         " + (mods.Length == 0 ? "none" : string.Join(", ", mods)));
        return 0;
    }
}
