using System.Collections.Concurrent;
using System.Text.Json;
using Factorio.ModTool.Api;

namespace Factorio.ModTool.Build;

/// <summary>
/// Indexed view of <c>prototype-api.json</c> for fast compile-time prototype validation.
/// Cached per file path so running dozens of tests pays deserialisation once.
/// </summary>
public sealed class PrototypeSchema
{
    public sealed record ResolvedStruct(
        string Name,
        IReadOnlyDictionary<string, PrototypeProperty> Properties,
        IReadOnlySet<string> AltNames,
        string? TypeTagLiteral);

    private static readonly ConcurrentDictionary<string, PrototypeSchema> Cache = new(StringComparer.Ordinal);

    private readonly Dictionary<string, PrototypeDef> _prototypesByName = new(StringComparer.Ordinal);
    private readonly Dictionary<string, PrototypeDef> _prototypesByTypeName = new(StringComparer.Ordinal);
    private readonly Dictionary<string, TypeDef> _typesByName = new(StringComparer.Ordinal);
    private readonly Dictionary<string, IReadOnlyDictionary<string, PrototypeProperty>> _protoPropsCache = new(StringComparer.Ordinal);
    private readonly Dictionary<string, ResolvedStruct> _typeStructCache = new(StringComparer.Ordinal);

    private PrototypeSchema(ApiDocument doc)
    {
        if (doc.Prototypes != null)
        {
            foreach (var proto in doc.Prototypes)
            {
                _prototypesByName[proto.Name] = proto;
                if (!string.IsNullOrEmpty(proto.TypeName))
                    _prototypesByTypeName[proto.TypeName] = proto;
            }
        }

        if (doc.Types != null)
        {
            foreach (var type in doc.Types)
                _typesByName[type.Name] = type;
        }
    }

    /// <summary>Loads (or returns cached) schema from a <c>prototype-api.json</c> path.</summary>
    public static PrototypeSchema Load(string prototypeJsonPath) =>
        Cache.GetOrAdd(Path.GetFullPath(prototypeJsonPath), path =>
        {
            var doc = JsonSerializer.Deserialize<ApiDocument>(File.ReadAllText(path))
                      ?? throw new ToolException($"empty prototype schema: {path}");
            return new PrototypeSchema(doc);
        });

    /// <summary>Finds a prototype definition by its Lua <c>type = "..."</c> string.</summary>
    public PrototypeDef? FindPrototype(string typeName) =>
        _prototypesByTypeName.GetValueOrDefault(typeName);

    /// <summary>Finds a named auxiliary <see cref="TypeDef"/>.</summary>
    public TypeDef? FindType(string name) =>
        _typesByName.GetValueOrDefault(name);

    /// <summary>All own + inherited properties for a <see cref="PrototypeDef"/>.</summary>
    public IReadOnlyDictionary<string, PrototypeProperty> PropertiesOf(PrototypeDef proto)
    {
        if (_protoPropsCache.TryGetValue(proto.Name, out var cached)) return cached;

        var chain = new List<PrototypeDef>();
        for (PrototypeDef? cur = proto; cur != null; cur = cur.Parent != null ? _prototypesByName.GetValueOrDefault(cur.Parent) : null)
            chain.Add(cur);

        // Walk base -> derived so child overrides parent when a property is refined.
        chain.Reverse();
        var map = new Dictionary<string, PrototypeProperty>(StringComparer.Ordinal);
        foreach (var item in chain)
        foreach (var prop in item.Properties)
            map[prop.Name] = prop;

        _protoPropsCache[proto.Name] = map;
        return map;
    }

    /// <summary>All own + inherited properties of a named <see cref="TypeDef"/> treated as a struct.</summary>
    public ResolvedStruct StructOf(TypeDef typeDef)
    {
        if (_typeStructCache.TryGetValue(typeDef.Name, out var cached)) return cached;

        var chain = new List<TypeDef>();
        for (TypeDef? cur = typeDef; cur != null; cur = cur.Parent != null ? _typesByName.GetValueOrDefault(cur.Parent) : null)
            chain.Add(cur);

        chain.Reverse();
        var props = new Dictionary<string, PrototypeProperty>(StringComparer.Ordinal);
        var alts = new HashSet<string>(StringComparer.Ordinal);
        foreach (var item in chain)
        foreach (var prop in item.Properties)
        {
            props[prop.Name] = prop;
            if (!string.IsNullOrEmpty(prop.AltName)) alts.Add(prop.AltName);
        }

        string? tagLiteral = null;
        if (props.TryGetValue("type", out var typeProp) &&
            typeProp.Type.ValueKind == JsonValueKind.Object &&
            typeProp.Type.TryGetProperty("complex_type", out var ct) &&
            ct.GetString() == "literal" &&
            typeProp.Type.TryGetProperty("value", out var litVal) &&
            litVal.ValueKind == JsonValueKind.String)
        {
            tagLiteral = litVal.GetString();
        }

        var resolved = new ResolvedStruct(typeDef.Name, props, alts, tagLiteral);
        _typeStructCache[typeDef.Name] = resolved;
        return resolved;
    }

    /// <summary>Suggests the closest known property name within Levenshtein distance 3.</summary>
    public static string? NearestProperty(string unknown, IEnumerable<string> candidates)
    {
        string? best = null;
        var bestDistance = int.MaxValue;
        foreach (var candidate in candidates)
        {
            var dist = Distance(unknown, candidate);
            if (dist < bestDistance)
            {
                bestDistance = dist;
                best = candidate;
            }
        }

        return bestDistance <= 3 ? best : null;
    }

    private static int Distance(string left, string right)
    {
        var prev = new int[right.Length + 1];
        var curr = new int[right.Length + 1];
        for (var j = 0; j <= right.Length; j++) prev[j] = j;

        for (var i = 1; i <= left.Length; i++)
        {
            curr[0] = i;
            for (var j = 1; j <= right.Length; j++)
            {
                var cost = left[i - 1] == right[j - 1] ? 0 : 1;
                curr[j] = Math.Min(Math.Min(curr[j - 1] + 1, prev[j] + 1), prev[j - 1] + cost);
            }
            (prev, curr) = (curr, prev);
        }

        return prev[right.Length];
    }
}
