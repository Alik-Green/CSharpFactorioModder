using System.Text.Json;

namespace Factorio.ModTool.Api;

/// <summary>Normalised representation of the polymorphic "Type" object of the Factorio JSON format.</summary>
public sealed class TypeNode
{
    public string Kind = "";              // simple | type | union | array | dictionary | LuaCustomTable | table |
                                          // tuple | function | literal | LuaLazyLoadedValue | LuaStruct | unknown
    public string? Name;                  // simple name, or the wrapped name of a "type" node
    public string? Description;
    public List<TypeNode>? Options;       // union
    public TypeNode? Value;               // array / type / LuaLazyLoadedValue
    public TypeNode? Key;                 // dictionary / LuaCustomTable
    public List<Parameter>? Parameters;   // table / function(serialised as strings, handled below)
    public List<string>? FunctionParameters;
    public List<ParameterGroup>? Groups;  // table
    public List<TypeNode>? Values;        // tuple
    public string? LiteralKind;           // literal
    public object? LiteralValue;
    public List<AttributeDef>? Attributes; // LuaStruct
    public bool FullFormat;

    public static TypeNode Parse(JsonElement e)
    {
        if (e.ValueKind == JsonValueKind.String)
        {
            var s = e.GetString()!;
            // builtin aliases are concepts in api_version >= 5 but may appear directly
            return new TypeNode { Kind = "simple", Name = s };
        }
        if (e.ValueKind != JsonValueKind.Object)
            return new TypeNode { Kind = "unknown", Description = e.ToString() };

        var kind = e.TryGetProperty("complex_type", out var ct) ? ct.GetString()! : "";
        var n = new TypeNode { Kind = kind };
        if (e.TryGetProperty("description", out var d)) n.Description = d.GetString();
        if (e.TryGetProperty("full_format", out var ff)) n.FullFormat = ff.GetBoolean();
        switch (kind)
        {
            case "type":
            {
                var v = e.GetProperty("value");
                if (v.ValueKind == JsonValueKind.String)
                {
                    n.Name = v.GetString();
                    return n;
                }
                // api_version 6 allows a full type object inside a "type" node (e.g. concept ForceSet)
                var inner = Parse(v);
                inner.Description ??= n.Description;
                return inner;
            }
            case "union":
                n.Options = e.GetProperty("options").EnumerateArray().Select(Parse).ToList();
                return n;
            case "array":
                n.Value = Parse(e.GetProperty("value"));
                return n;
            case "dictionary":
            case "LuaCustomTable":
                n.Key = Parse(e.GetProperty("key"));
                n.Value = Parse(e.GetProperty("value"));
                return n;
            case "LuaLazyLoadedValue":
                n.Value = Parse(e.GetProperty("value"));
                return n;
            case "table":
            {
                n.Parameters = ParseParameters(e);
                if (e.TryGetProperty("variant_parameter_groups", out var g))
                    n.Groups = JsonSerializer.Deserialize<List<ParameterGroup>>(g.GetRawText())!;
                return n;
            }
            case "tuple":
                n.Values = e.GetProperty("values").EnumerateArray().Select(Parse).ToList();
                return n;
            case "function":
            {
                n.FunctionParameters = new();
                if (e.TryGetProperty("parameters", out var p))
                    foreach (var item in p.EnumerateArray())
                        n.FunctionParameters.Add(item.ValueKind == JsonValueKind.String ? item.GetString()! : item.GetRawText());
                return n;
            }
            case "literal":
            {
                var v = e.GetProperty("value");
                n.LiteralKind = v.ValueKind switch
                {
                    JsonValueKind.String => "string",
                    JsonValueKind.Number => "number",
                    JsonValueKind.True or JsonValueKind.False => "boolean",
                    _ => "unknown",
                };
                n.LiteralValue = n.LiteralKind switch
                {
                    "string" => v.GetString(),
                    "number" => v.GetDouble(),
                    "boolean" => v.GetBoolean(),
                    _ => null,
                };
                return n;
            }
            case "LuaStruct":
            {
                n.Attributes = JsonSerializer.Deserialize<List<AttributeDef>>(e.GetProperty("attributes").GetRawText())!;
                return n;
            }
            default:
                n.Description = e.GetRawText();
                return n;
        }
    }

    public static List<Parameter> ParseParameters(JsonElement e)
    {
        if (!e.TryGetProperty("parameters", out var p)) return new();
        return JsonSerializer.Deserialize<List<Parameter>>(p.GetRawText())!;
    }

    public TypeNode? Unwrap() => Kind == "type" ? new TypeNode { Kind = "simple", Name = Name } : this;

    public bool IsNil => Kind == "simple" && Name == "nil";

    public override string ToString() => Kind switch
    {
        "simple" => Name!,
        "type" => Name!,
        "union" => "(" + string.Join("|", Options!.Select(o => o.ToString())) + ")",
        "array" => "array[" + Value + "]",
        "dictionary" => "dict[" + Key + "->" + Value + "]",
        "LuaCustomTable" => "customtable[" + Key + "->" + Value + "]",
        "table" => "table{" + string.Join(",", (Parameters ?? new()).Select(x => x.Name)) + "}",
        "tuple" => "[" + string.Join(",", Values!.Select(v => v.ToString())) + "]",
        "function" => "function(" + string.Join(",", FunctionParameters ?? new()) + ")",
        "literal" => "literal(" + LiteralKind + ":" + LiteralValue + ")",
        "LuaLazyLoadedValue" => "lazy[" + Value + "]",
        "LuaStruct" => "struct{" + string.Join(",", (Attributes ?? new()).Select(a => a.Name)) + "}",
        _ => Kind,
    };
}

/// <summary>Result of mapping a TypeNode to a C# type.</summary>
public sealed class MappedType
{
    public string Cs { get; init; } = "object";
    public bool IsValueType { get; init; }
    /// <summary>True when the Lua value may legitimately be nil in this position.</summary>
    public bool AllowsNil { get; init; }
    /// <summary>Set when the mapping is lossy (union collapsed, tuple form dropped, ...).</summary>
    public string? Note { get; init; }
    public override string ToString() => Cs;
}

public sealed class Diagnostic
{
    public string Stage { get; set; } = "runtime";   // runtime | prototype
    public string Severity { get; init; } = "info";  // info | degraded | skipped | error
    public string ApiMember { get; init; } = "";     // e.g. LuaEntity::health
    public string Message { get; init; } = "";
    public string Detail { get; init; } = "";
}
