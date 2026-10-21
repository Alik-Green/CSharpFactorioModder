namespace Factorio.ModTool.Api;

/// <summary>A generated C# class that models a Lua table (concept / struct / method-table).</summary>
public sealed class TableClass
{
    public string Name = "";
    public string Description = "";
    public List<Parameter> Parameters = new();
    public List<ParameterGroup> Groups = new();
    public bool IsStruct;
    public string Source = "";
    public string? BaseClass;
    /// <summary>When false the class is emitted read-only (no templated constructor).</summary>
    public bool EmitConstructor = true;
    /// <summary>True when another generated class inherits from this one (needs a protected ctor).</summary>
    public bool IsBase;
    /// <summary>True when the class appears in an input position, i.e. mod code must build the table.</summary>
    public bool UsedAsInput;
    /// <summary>Flattened fields (own parameters followed by variant-group parameters), required first.</summary>
    public List<TableField> Fields = new();
}

public sealed class TableField
{
    /// <summary>Native Lua key.</summary>
    public string Name = "";
    /// <summary>C# property name (PascalCase).</summary>
    public string CsName = "";
    public string CsType = "object";
    public bool Optional;
    public string Doc = "";
}

/// <summary>
/// Maps Factorio JSON types onto C# types. All Lua numbers map to <c>double</c> deliberately:
/// CSharp.lua emits Lua 5.3 integer division / bitwise operators for C# integral types,
/// and Factorio's Lua 5.2 runtime rejects that syntax.
/// </summary>
public sealed class TypeMapper
{
    private readonly ApiDocument _doc;
    private readonly Dictionary<string, TypeNode> _concepts = new(StringComparer.Ordinal);
    private readonly Dictionary<string, ClassDef> _classes = new(StringComparer.Ordinal);
    private readonly Dictionary<string, TypeNode> _aliasCache = new(StringComparer.Ordinal);
    private readonly HashSet<string> _resolving = new(StringComparer.Ordinal);

    public Dictionary<string, TableClass> TableClasses { get; } = new(StringComparer.Ordinal);
    public List<Diagnostic> Diagnostics { get; } = new();

    public static readonly HashSet<string> NumericBuiltins = new(StringComparer.Ordinal)
    {
        "number", "float", "double", "uint8", "uint16", "uint32", "uint64", "int8", "int16", "int32", "int64",
        "MapTick", "ItemCountType", "RealOrientation", "MapPositionType", "uint", "int"
    };

    /// <summary>C# names for the API's classes (drops the meaningless `Lua` prefix).</summary>
    public TypeRenamer Renamer { get; }

    public TypeMapper(ApiDocument doc)
    {
        _doc = doc;
        Renamer = TypeRenamer.Build(doc.Classes.Select(c => c.Name), doc.Concepts.Select(c => c.Name));
        foreach (var c in doc.Concepts) _concepts[c.Name] = TypeNode.Parse(c.Type);
        foreach (var c in doc.Classes) _classes[c.Name] = c;
    }

    public void AddConcept(string name, TypeNode type) => _concepts[name] = type;

    /// <summary>Marks a generated table class as being constructed by mod code (needs a ctor template).</summary>
    public void MarkInput(string name)
    {
        if (TableClasses.TryGetValue(name, out var tc)) tc.UsedAsInput = true;
    }

    public bool IsClass(string name) => _classes.ContainsKey(name);
    public ClassDef? GetClass(string name) => _classes.TryGetValue(name, out var c) ? c : null;

    public void Error(string member, string message, string detail = "") =>
        Diagnostics.Add(new Diagnostic { Severity = "error", ApiMember = member, Message = message, Detail = detail });

    public void Degraded(string member, string message, string detail = "") =>
        Diagnostics.Add(new Diagnostic { Severity = "degraded", ApiMember = member, Message = message, Detail = detail });

    public void Skipped(string member, string message, string detail = "") =>
        Diagnostics.Add(new Diagnostic { Severity = "skipped", ApiMember = member, Message = message, Detail = detail });

    public void Info(string member, string message, string detail = "") =>
        Diagnostics.Add(new Diagnostic { Severity = "info", ApiMember = member, Message = message, Detail = detail });

    /// <summary>Resolves concept aliases down to a non-concept TypeNode.</summary>
    public TypeNode ResolveAliases(TypeNode t, string member)
    {
        var guard = 0;
        while (t.Kind == "simple" && t.Name != null && _concepts.TryGetValue(t.Name, out var concept) && !IsClass(t.Name))
        {
            if (++guard > 32)
            {
                Error(member, "concept alias cycle", t.Name);
                return new TypeNode { Kind = "unknown" };
            }
            var inner = concept;
            if (inner.Kind == "simple" && inner.Name == t.Name) break; // builtin self reference
            t = inner;
        }
        return t;
    }

    public MappedType Map(TypeNode t, string member, string? nameHint = null, bool input = false)
    {
        switch (t.Kind)
        {
            case "simple":
            {
                var name = t.Name!;
                if (name == "nil") return new MappedType { Cs = "object", AllowsNil = true, Note = "nil" };
                if (name == "string") return new MappedType { Cs = "string" };
                if (name == "boolean") return new MappedType { Cs = "bool", IsValueType = true };
                if (NumericBuiltins.Contains(name)) return new MappedType { Cs = "double", IsValueType = true };
                if (name == "LuaObject") return new MappedType { Cs = "LuaObject" };
                if (IsClass(name)) return new MappedType { Cs = Renamer.Rename(name) };
                if (_concepts.TryGetValue(name, out var conceptType))
                {
                    if (!_resolving.Add(name))
                    {
                        Info(member, $"recursive concept '{name}' mapped to object");
                        return new MappedType { Cs = "object" };
                    }
                    try
                    {
                        return MapConcept(name, conceptType, member, input);
                    }
                    finally
                    {
                        _resolving.Remove(name);
                    }
                }
                if (name.StartsWith("defines.", StringComparison.Ordinal))
                    return new MappedType { Cs = "double", IsValueType = true, Note = "define value" };
                Error(member, $"unknown simple type '{name}'");
                return new MappedType { Cs = "object" };
            }
            case "type":
                return Map(new TypeNode { Kind = "simple", Name = t.Name }, member, nameHint, input);
            case "union":
                return MapUnion(t, member, nameHint, input);
            case "array":
                return new MappedType { Cs = "LuaArray<" + Map(t.Value!, member).Cs + ">" };
            case "dictionary":
                return new MappedType { Cs = "LuaTable<" + Map(t.Key!, member).Cs + ", " + Map(t.Value!, member).Cs + ">" };
            case "LuaCustomTable":
                return new MappedType { Cs = "LuaCustomTable<" + Map(t.Key!, member).Cs + ", " + Map(t.Value!, member).Cs + ">" };
            case "LuaLazyLoadedValue":
                return Map(t.Value!, member, nameHint, input);
            case "table":
                if (nameHint != null) return MaterialiseTable(nameHint, t, member, input);
                Degraded(member, "anonymous table type mapped to LuaTable<string, object>", t.ToString());
                return new MappedType { Cs = "LuaTable<string, object>", Note = "anonymous table" };
            case "tuple":
                Degraded(member, "tuple type has no C# representation here, mapped to LuaArray<object>", t.ToString());
                return new MappedType { Cs = "LuaArray<object>", Note = "tuple" };
            case "function":
                return MapFunction(t, member, nameHint);
            case "literal":
                return t.LiteralKind switch
                {
                    "string" => new MappedType { Cs = "string" },
                    "number" => new MappedType { Cs = "double", IsValueType = true },
                    "boolean" => new MappedType { Cs = "bool", IsValueType = true },
                    _ => new MappedType { Cs = "object" },
                };
            case "builtin":
                Info(member, "'builtin' (unspecified fundamental) type mapped to object");
                return new MappedType { Cs = "object", Note = "builtin" };
            case "LuaStruct":
                if (nameHint != null)
                {
                    var cls = new TableClass
                    {
                        Name = nameHint,
                        Description = "",
                        IsStruct = true,
                        Source = member,
                        Parameters = (t.Attributes ?? new()).Select(a => new Parameter
                        {
                            Name = a.Name,
                            Order = a.Order,
                            Description = a.Description,
                            Type = a.ReadType ?? a.WriteType ?? System.Text.Json.JsonDocument.Parse("\"object\"").RootElement,
                            Optional = a.Optional,
                        }).ToList(),
                    };
                    Register(cls, member);
                    return new MappedType { Cs = cls.Name, Note = "LuaStruct" };
                }
                Degraded(member, "anonymous LuaStruct mapped to LuaTable<string, object>");
                return new MappedType { Cs = "LuaTable<string, object>" };
            default:
                Error(member, $"unsupported complex_type '{t.Kind}'", t.Description ?? "");
                return new MappedType { Cs = "object" };
        }
    }

    private MappedType MapConcept(string name, TypeNode inner, string member, bool input)
    {
        if (inner.Kind == "simple" && inner.Name == "builtin")
            return new MappedType { Cs = "object", Note = "builtin concept " + name };
        if (inner.Kind == "simple" || inner.Kind == "type")
            return Map(inner, member, null); // alias of a builtin/other concept
        return MaterialiseConcept(name, inner, member, input);
    }

    private MappedType MapFunction(TypeNode t, string member, string? nameHint)
    {
        var ps = t.FunctionParameters ?? new();
        if (ps.Count == 0) return new MappedType { Cs = "Action" };
        var mapped = new List<string>();
        foreach (var p in ps)
        {
            if (p == "EventData")
            {
                mapped.Add("EventData");
                continue;
            }
            var node = TypeNode.Parse(System.Text.Json.JsonDocument.Parse("\"" + p.Replace("\"", "\\\"") + "\"").RootElement);
            mapped.Add(Map(node, member).Cs);
        }
        if (ps.Count > 4)
        {
            Degraded(member, "function type with more than 4 parameters mapped to Action<object>", t.ToString());
            return new MappedType { Cs = "Action<object>" };
        }
        var name = "Action<" + string.Join(", ", mapped) + ">";
        return new MappedType { Cs = name };
    }

    private MappedType MapUnion(TypeNode t, string member, string? nameHint, bool input)
    {
        var options = t.Options ?? new();
        var hasNil = options.Any(o => o.IsNil);
        var nonNil = options.Where(o => !o.IsNil).ToList();
        if (nonNil.Count == 0) return new MappedType { Cs = "object", AllowsNil = true };

        // a named union whose only structured option is a table -> materialise that table as the concept class
        if (nameHint != null && nonNil.Count(o => o.Kind == "table") == 1)
        {
            var table = nonNil.First(o => o.Kind == "table");
            if (nonNil.All(o => o.Kind == "table" || o.Kind == "tuple" || o.Kind == "literal" || o.Kind == "simple"))
            {
                var dropped = nonNil.Where(o => o.Kind != "table").ToList();
                if (dropped.Count > 0)
                    Info(member, $"concept '{nameHint}' accepts alternative forms that C# cannot express; only the table form is generated",
                        string.Join(", ", dropped.Select(d => d.ToString())));
                return MaterialiseTable(nameHint, table, member, input, t.Description);
            }
        }

        if (nameHint == null && nonNil.Count(o => o.Kind == "table") == 1 &&
            nonNil.All(o => o.Kind is "table" or "tuple" or "literal" or "simple"))
        {
            var anon = AnonName(member);
            return MaterialiseTable(anon, nonNil.First(o => o.Kind == "table"), member, input);
        }

        var mapped = nonNil.Select(o => Map(o, member)).ToList();
        var distinct = mapped.Select(m => m.Cs).Distinct(StringComparer.Ordinal).ToList();
        if (distinct.Count == 1)
            return new MappedType { Cs = distinct[0], IsValueType = mapped[0].IsValueType, AllowsNil = hasNil, Note = mapped[0].Note };

        var allScalar = nonNil.All(o => o.Kind is "simple" or "literal" or "type") &&
                        mapped.All(m => m.Cs is "string" or "double" or "bool");
        if (allScalar) Info(member, "scalar union mapped to object", t.ToString());
        else Degraded(member, "union collapsed to object", t.ToString());
        return new MappedType { Cs = "object", AllowsNil = hasNil };
    }

    private MappedType MaterialiseConcept(string name, TypeNode shape, string member, bool input)
    {
        switch (shape.Kind)
        {
            case "table":
                return MaterialiseTable(name, shape, member, input);
            case "union":
            {
                var table2 = (shape.Options ?? new()).FirstOrDefault(o => o.Kind == "table");
                if (table2 != null) return MaterialiseTable(name, table2, member, input, shape.Description);
                var opts = (shape.Options ?? new()).Where(o => !o.IsNil).Select(o => Map(o, member)).ToList();
                var distinct = opts.Select(o => o.Cs).Distinct(StringComparer.Ordinal).ToList();
                if (distinct.Count == 1)
                    return new MappedType { Cs = distinct[0], IsValueType = opts[0].IsValueType, Note = "concept alias " + name };
                Info(member, $"concept '{name}' accepts several incompatible value kinds; mapped to object", shape.ToString());
                return new MappedType { Cs = "object" };
            }
            case "builtin":
                Info(member, "'builtin' (unspecified fundamental) type mapped to object");
                return new MappedType { Cs = "object", Note = "builtin" };
            case "LuaStruct":
                return Map(shape, member, name);
            default:
                return Map(shape, member, name);
        }
    }

    private MappedType MaterialiseTable(string name, TypeNode table, string member, bool input = false, string? description = null)
    {
        var cls = new TableClass
        {
            Name = name,
            Description = string.IsNullOrEmpty(description) ? "" : description!,
            Parameters = (table.Parameters ?? new()).ToList(),
            Groups = (table.Groups ?? new()).ToList(),
            Source = member,
        };
        Register(cls, member);
        if (input && TableClasses.TryGetValue(name, out var registered)) registered.UsedAsInput = true;
        return new MappedType { Cs = name };
    }

    private void Register(TableClass cls, string member)
    {
        if (_classes.ContainsKey(cls.Name))
        {
            // a Lua class with the same name wins; this concept is only an alias
            return;
        }
        if (TableClasses.TryGetValue(cls.Name, out var existing))
        {
            // Same name from two places: keep the first shape, report the clash once.
            if (ParameterSignature(existing) != ParameterSignature(cls))
                Info(member, $"table shape for '{cls.Name}' differs from an earlier definition; first shape kept",
                    ParameterSignature(existing) + " vs " + ParameterSignature(cls));
            return;
        }
        TableClasses[cls.Name] = cls;
    }

    /// <summary>Deterministic class name for an anonymous table type, derived from its API path.</summary>
    public static string AnonName(string member)
    {
        var parts = member.Split(new[] { ':', '|', '.', '<', '>', ',', ' ', '(', ')', '[', ']', '-' }, StringSplitOptions.RemoveEmptyEntries);
        var sb = new System.Text.StringBuilder();
        foreach (var p in parts)
        {
            var t = p.Replace("global", "");
            if (t.Length == 0) continue;
            sb.Append(char.ToUpperInvariant(t[0])).Append(t.Substring(1));
        }
        return sb.Length == 0 ? "AnonymousTable" : sb.ToString();
    }

    private static string ParameterSignature(TableClass c) =>
        string.Join(",", c.Parameters.OrderBy(p => p.Order).Select(p => p.Name + ":" + p.Order));
}
