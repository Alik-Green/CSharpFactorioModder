namespace Factorio.ModTool.Api;

/// <summary>
/// Decides what a Factorio class is called in C#.
///
/// The game's API names every object it hands to Lua <c>LuaSomething</c>, because from Lua's point
/// of view they are views of engine objects. That prefix carries no information in C#, where every
/// value already has a type, so it is dropped: <c>LuaPlayer</c> becomes <c>Player</c> and
/// <c>game.get_player(1)</c> returns a <c>Player</c>. The native name is still written into the
/// emitted documentation and into every <c>@CSharpLua</c> attribute, so the Lua side is unchanged.
///
/// A name is only shortened when the result is unambiguous: if two classes would end up with the
/// same C# name, both keep their native name.
/// </summary>
public sealed class TypeRenamer
{
    private readonly Dictionary<string, string> _map;

    private TypeRenamer(Dictionary<string, string> map) => _map = map;

    /// <summary>A renamer that changes nothing, used for stages that declare no classes.</summary>
    public static TypeRenamer Empty { get; } = new(new Dictionary<string, string>(StringComparer.Ordinal));

    /// <summary>Names that must not be produced by dropping a prefix.</summary>
    private static readonly HashSet<string> Reserved = new(StringComparer.Ordinal)
    {
        // C# fundamentals, and the compat types in Factorio.Bindings/Compat.cs
        "Object", "String", "Array", "Table", "CustomTable", "Function", "Event", "Type", "Value", "Action", "Task",
    };

    /// <summary>
    /// Names where the mechanical rule is not the name a reader wants. <c>LuaGameScript</c> is the
    /// <c>game</c> global and <c>LuaBootstrap</c> is the <c>script</c> global, so they keep the
    /// names people already use for them.
    /// </summary>
    private static readonly Dictionary<string, string> Preferred = new(StringComparer.Ordinal)
    {
        ["LuaGameScript"] = "Game",
        ["LuaBootstrap"] = "Script",
        ["LuaObject"] = "LuaObject",
    };

    /// <summary>Builds the map for one API document.</summary>
    /// <param name="apiNames">Every class name declared by the API.</param>
    /// <param name="reservedNames">
    /// Names already taken by something else the generator emits into the same namespace, such as
    /// the table classes (<c>Tile</c>, <c>LogisticSection</c>): a rename must not collide with them.
    /// </param>
    /// <returns>A renamer for those names.</returns>
    public static TypeRenamer Build(IEnumerable<string> apiNames, IEnumerable<string>? reservedNames = null)
    {
        var names = apiNames.Distinct(StringComparer.Ordinal).ToList();
        var taken = new HashSet<string>(names, StringComparer.Ordinal);
        if (reservedNames != null) taken.UnionWith(reservedNames);
        var proposed = new Dictionary<string, string>(StringComparer.Ordinal);

        foreach (var name in names)
        {
            if (Preferred.TryGetValue(name, out var preferred))
            {
                proposed[name] = preferred;
                continue;
            }

            if (!name.StartsWith("Lua", StringComparison.Ordinal) || name.Length <= 3)
            {
                proposed[name] = name;
                continue;
            }

            var stripped = name.Substring(3);
            proposed[name] = Reserved.Contains(stripped) || taken.Contains(stripped) ? name : stripped;
        }

        // Two classes may not share a C# name: fall back to the native name for every member of a
        // colliding group, so the result stays predictable.
        var collisions = proposed
            .GroupBy(pair => pair.Value, StringComparer.Ordinal)
            .Where(group => group.Count() > 1)
            .SelectMany(group => group.Select(pair => pair.Key))
            .ToHashSet(StringComparer.Ordinal);

        foreach (var name in collisions) proposed[name] = name;

        // A rename must not land on the name of a class that keeps its own.
        foreach (var name in names)
        {
            if (proposed[name] == name) continue;
            if (names.Contains(proposed[name]))
                proposed[name] = name;
        }

        return new TypeRenamer(proposed);
    }

    /// <summary>The C# name for a Factorio class.</summary>
    /// <param name="apiName">The name used by the API, e.g. <c>LuaEntity</c>.</param>
    /// <returns>The name to emit, e.g. <c>Entity</c>.</returns>
    public string Rename(string apiName) => _map.TryGetValue(apiName, out var renamed) ? renamed : apiName;

    /// <summary>Every renamed class, for documentation and diagnostics.</summary>
    public IReadOnlyDictionary<string, string> Renames =>
        _map.Where(pair => pair.Key != pair.Value)
            .ToDictionary(pair => pair.Key, pair => pair.Value, StringComparer.Ordinal);
}
