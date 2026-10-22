using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Factorio.Data;

namespace Factorio.ModTool.Data;

/// <summary>
/// Turns a mod's C# data stage into the <c>data.lua</c> Factorio loads.
/// <para>
/// A data stage written in C# (<c>"dataEntry"</c> in mod.json) is not compiled to Lua by
/// CSharp.lua: it runs <em>here</em>, at build time. The definitions are ordinary .NET objects -
/// they may use LINQ, collections, loops, helper methods and generics, none of which a runtime mod
/// can - and this class writes what they describe as a plain prototype table.
/// </para>
/// <para>
/// That split is what makes the C# data API pleasant to use: the restrictions that make a runtime
/// mod hard (no arrays, no interfaces, no exceptions) do not apply, because none of this code
/// ships. What ships is the generated Lua, which is then checked against the official prototype
/// API exactly like a hand-written <c>data.lua</c> is.
/// </para>
/// </summary>
public static class DataEntryGenerator
{
    /// <summary>The Lua written, and what it contains, for the build log.</summary>
    /// <param name="Lua">The generated <c>data.lua</c>.</param>
    /// <param name="Prototypes">The definitions, in the order the mod added them.</param>
    /// <param name="Inherited">Prototypes built on a vanilla one, as "type/name".</param>
    /// <param name="Names">
    /// The English names and descriptions the definitions declared, as "type/name" to
    /// (kind, text). Written into the packaged locale.cfg, which is why they are not in the Lua.
    /// </param>
    public sealed record Result(
        string Lua,
        IReadOnlyList<PrototypeDefinition> Prototypes,
        IReadOnlySet<string> Inherited,
        IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>> Names);

    /// <summary>
    /// Compiles <paramref name="sources"/>, runs the mod's <see cref="ModDefinition"/> and writes
    /// the prototypes as Lua.
    /// </summary>
    /// <param name="entryType">Fully qualified name of the <see cref="ModDefinition"/> subclass.</param>
    /// <param name="sources">The mod's data-stage C# files.</param>
    /// <param name="apiVersion">Target Factorio version, e.g. <c>2.0</c>; decides field spellings.</param>
    /// <param name="modName">
    /// The mod's <c>info.json</c> name. A resource path has to carry a <c>__source__/</c> prefix, and
    /// the source of the mod's own files is this name, so it becomes
    /// <see cref="Asset.Source"/> while the definitions run.
    /// </param>
    /// <returns>The generated Lua and the definitions it came from.</returns>
    /// <exception cref="ToolException">The sources do not compile, or the entry cannot be run.</exception>
    public static Result Generate(
        string entryType,
        IReadOnlyList<string> sources,
        string apiVersion,
        string? modName = null,
        IReadOnlyList<string>? resourceRoots = null)
    {
        if (sources.Count == 0)
            throw new ToolException(
                $"mod.json sets dataEntry '{entryType}' but no data-stage sources were found; " +
                "put the definitions in data/ (see \"dataSources\") or remove dataEntry");

        var assembly = Compile(sources);
        var type = assembly.GetType(entryType, throwOnError: false);
        if (type == null)
        {
            var available = assembly.GetTypes()
                .Where(t => typeof(ModDefinition).IsAssignableFrom(t) && !t.IsAbstract)
                .Select(t => t.FullName!)
                .OrderBy(n => n, StringComparer.Ordinal)
                .ToList();
            throw new ToolException(
                $"dataEntry '{entryType}' was not found in the data-stage sources" +
                (available.Count > 0 ? "; found: " + string.Join(", ", available) : ""));
        }
        if (type.IsAbstract || !typeof(ModDefinition).IsAssignableFrom(type))
            throw new ToolException($"dataEntry '{entryType}' must be a concrete class deriving from ModDefinition");
        if (type.GetConstructor(Type.EmptyTypes) == null)
            throw new ToolException($"dataEntry '{entryType}' needs a public parameterless constructor");

        var definition = (ModDefinition)Activator.CreateInstance(type)!;
        var builder = new ModBuilder();

        // Asset.Mod & Sprite.Auto / Icon auto-sizing resolve mod files using the mod's name and
        // resources/ directories throughout both Define() and Write().
        var previousSource = Asset.Source;
        var previousRoots = Asset.ResourceRoots;
        Asset.Source = modName;
        Asset.ResourceRoots = ResolveResourceRoots(sources, resourceRoots);
        IReadOnlyList<PrototypeDefinition> prototypes;
        string lua;
        try
        {
            try
            {
                definition.Define(builder);
            }
            catch (Exception e)
            {
                var inner = e is TargetInvocationException { InnerException: { } innerException } ? innerException : e;
                throw new ToolException($"dataEntry '{entryType}' failed while defining prototypes: {inner.Message}");
            }

            prototypes = builder.Prototypes;
            if (prototypes.Count == 0)
                throw new ToolException($"dataEntry '{entryType}' defined no prototypes (did Define() call mod.Add?)");

            lua = Write(prototypes, apiVersion);
        }
        finally
        {
            Asset.Source = previousSource;
            Asset.ResourceRoots = previousRoots;
        }

        // Which prototypes take their unset fields from a vanilla one. The data-stage check has to
        // know: it cannot see the game's prototypes, and would report the inherited fields as
        // missing.
        var inherited = prototypes
            .Where(p => p.InheritsFrom != null)
            .Select(p => p.Type + "/" + p.Name)
            .ToHashSet(StringComparer.Ordinal);

        var names = prototypes.ToDictionary(
            prototype => prototype.Type + "/" + prototype.Name,
            prototype => (IReadOnlyDictionary<string, string>)prototype.Localisations,
            StringComparer.Ordinal);

        return new Result(lua, prototypes, inherited, names);
    }

    private static IReadOnlyList<string> ResolveResourceRoots(
        IReadOnlyList<string> sources,
        IReadOnlyList<string>? explicitRoots)
    {
        var roots = new List<string>();
        if (explicitRoots != null)
        {
            foreach (var r in explicitRoots)
            {
                if (!string.IsNullOrWhiteSpace(r) && !roots.Contains(r, StringComparer.OrdinalIgnoreCase))
                    roots.Add(r);
            }
        }

        foreach (var source in sources)
        {
            var dir = Path.GetDirectoryName(Path.GetFullPath(source));
            for (var depth = 0; depth < 3 && dir != null; depth++)
            {
                var candidate = Path.Combine(dir, "resources");
                if (Directory.Exists(candidate) && !roots.Contains(candidate, StringComparer.OrdinalIgnoreCase))
                    roots.Add(candidate);
                dir = Directory.GetParent(dir)?.FullName;
            }
        }

        return roots;
    }

    /// <summary>Writes the collected definitions as a complete <c>data.lua</c>.</summary>
    /// <param name="prototypes">The definitions, in the order the mod added them.</param>
    /// <param name="apiVersion">Target Factorio version.</param>
    /// <returns>Lua source for the data stage.</returns>
    public static string Write(IReadOnlyList<PrototypeDefinition> prototypes, string apiVersion)
    {
        var writer = new LuaWriter(apiVersion);
        writer.Line("-- Generated from C# by factorio-mod. Do not edit: change the definitions under data/ and rebuild.");
        writer.Line("-- " + prototypes.Count + " prototype(s), in the order the mod's ModDefinition adds them.");
        writer.Line("local prototypes =");
        writer.BeginArray();
        foreach (var prototype in prototypes) writer.Value(prototype);
        writer.EndArray();
        writer.Line("");

        return writer + Inherit + "data:extend(prototypes)\n";
    }

    /// <summary>
    /// The Lua that resolves <c>__inherit</c>: a definition built on a vanilla prototype copies
    /// every field it did not set from that prototype, and drops the ones it cleared explicitly.
    /// <para>
    /// It runs in the data stage, where <c>data.raw</c> holds the base game's prototypes - so the
    /// copy cannot be done at build time, because the build does not have the game's data.
    /// </para>
    /// </summary>
    private const string Inherit = """
        -- Prototypes built on a vanilla one: fill in every field the definition left out. A field
        -- that was set to null in C# is listed in __nil and is not copied - that is how a definition
        -- says "not this one" (e.g. NextUpgrade = null).
        --
        -- The copy is deep: an inherited table (graphics_set, energy_source) belongs to the vanilla
        -- prototype and must not be shared, or editing a mod's machine would edit the base game's.
        local function deepcopy(value)
          if type(value) ~= "table" then return value end
          local copy = {}
          for key, item in pairs(value) do copy[key] = deepcopy(item) end
          return copy
        end

        for _, prototype in pairs(prototypes) do
          local cleared = {}
          if prototype.__nil ~= nil then
            for _, key in ipairs(prototype.__nil) do cleared[key] = true end
            prototype.__nil = nil
          end

          local inherit = prototype.__inherit
          if inherit ~= nil then
            prototype.__inherit = nil
            local source = data.raw[inherit.type] and data.raw[inherit.type][inherit.name]
            if source ~= nil then
              for key, value in pairs(source) do
                if prototype[key] == nil and not cleared[key] then
                  prototype[key] = deepcopy(value)
                end
              end
            end
          end
        end

        """;

    /// <summary>Compiles the data sources into an in-memory assembly.</summary>
    /// <param name="sources">The C# files.</param>
    /// <returns>The loaded assembly.</returns>
    private static Assembly Compile(IReadOnlyList<string> sources)
    {
        var options = CSharpParseOptions.Default
            .WithLanguageVersion(LanguageVersion.Latest)
            .WithDocumentationMode(DocumentationMode.None);
        var trees = sources
            .Select(file => CSharpSyntaxTree.ParseText(File.ReadAllText(file), options, file))
            .ToList();

        var compilation = CSharpCompilation.Create(
            "FactorioModData_" + Guid.NewGuid().ToString("N"),
            trees,
            References.Value,
            new CSharpCompilationOptions(
                OutputKind.DynamicallyLinkedLibrary,
                optimizationLevel: OptimizationLevel.Release,
                concurrentBuild: false,
                nullableContextOptions: NullableContextOptions.Disable));

        using var assemblyBytes = new MemoryStream();
        var emit = compilation.Emit(assemblyBytes);
        if (!emit.Success)
        {
            var errors = emit.Diagnostics
                .Where(d => d.Severity == DiagnosticSeverity.Error)
                .Select(d => d.ToString())
                .Take(12)
                .ToList();
            throw new ToolException(
                "the data-stage sources do not compile:" + Environment.NewLine + string.Join(Environment.NewLine, errors));
        }

        return Assembly.Load(assemblyBytes.ToArray());
    }

    /// <summary>
    /// The metadata references a data-stage source may use: the .NET runtime the tool is running on,
    /// plus this tool's own assembly, which is where <c>Factorio.Data</c> lives.
    /// <para>
    /// Built once and shared: there are around two hundred runtime assemblies, and creating a
    /// reference for each of them on every build of every mod is both slow and - with a test suite
    /// compiling several mods - the largest allocation in the tool.
    /// </para>
    /// </summary>
    private static readonly Lazy<IReadOnlyList<MetadataReference>> References = new(() =>
    {
        var paths = new List<string>();
        var trusted = (string?)AppContext.GetData("TRUSTED_PLATFORM_ASSEMBLIES") ?? "";
        paths.AddRange(trusted.Split(Path.PathSeparator, StringSplitOptions.RemoveEmptyEntries));

        var self = typeof(ModDefinition).Assembly.Location;
        if (!string.IsNullOrEmpty(self)) paths.Add(self);

        return paths.Select(path => (MetadataReference)MetadataReference.CreateFromFile(path)).ToList();
    });
}
