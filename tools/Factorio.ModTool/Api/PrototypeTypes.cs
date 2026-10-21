using System.Text.Json;

namespace Factorio.ModTool.Api;

/// <summary>
/// Helpers for the prototype-stage JSON dialect, where structures are declared as
/// `{"complex_type":"struct"}` with the actual properties on the enclosing TypeDef.
/// </summary>
public static class PrototypeTypes
{
    public static TypeNode ToTypeNode(TypeDef def) => ToTypeNode(def.Name, def.Type, def.Properties);

    /// <summary>
    /// The type of a structure whose properties may come from more than one declaration, as when
    /// a struct type inherits its parent's properties (<c>CraftingMachineGraphicsSet</c> and its
    /// <c>WorkingVisualisations</c>).
    /// </summary>
    /// <param name="name">The type's own name, used for self references.</param>
    /// <param name="type">The declared shape, usually <c>{"complex_type":"struct"}</c>.</param>
    /// <param name="properties">The properties, inherited ones included.</param>
    /// <returns>The type node.</returns>
    public static TypeNode ToTypeNode(string name, JsonElement type, IReadOnlyList<PrototypeProperty> properties)
    {
        var table = TableNode(name, properties);
        var shape = TypeNode.Parse(type);
        return shape.Kind switch
        {
            "struct" => table,
            "union" => ReplaceStruct(shape, name, table),
            _ => shape,
        };
    }

    public static TypeNode ToTypeNode(JsonElement type, string ownerName, Dictionary<string, TypeDef> types) =>
        ReplaceStruct(TypeNode.Parse(type), ownerName, null);

    private static TypeNode TableNode(string name, IReadOnlyList<PrototypeProperty> properties)
    {
        var json = JsonSerializer.Serialize(new
        {
            complex_type = "table",
            parameters = properties.Select(p => new
            {
                name = p.Name,
                order = p.Order,
                description = p.Description,
                type = p.Type,
                optional = p.Optional,
            }),
        });
        using var doc = JsonDocument.Parse(json);
        return TypeNode.Parse(doc.RootElement.Clone());
    }

    /// <summary>Replaces inline "struct" nodes with either the given table shape or a self reference.</summary>
    public static TypeNode ReplaceStruct(TypeNode node, string ownerName, TypeNode? ownerTable)
    {
        switch (node.Kind)
        {
            case "struct":
                return ownerTable ?? new TypeNode { Kind = "simple", Name = ownerName };
            case "union":
                node.Options = node.Options?.Select(o => ReplaceStruct(o, ownerName, ownerTable)).ToList();
                return node;
            case "array":
                node.Value = ReplaceStruct(node.Value!, ownerName, ownerTable);
                return node;
            case "dictionary":
            case "LuaCustomTable":
                node.Key = ReplaceStruct(node.Key!, ownerName, ownerTable);
                node.Value = ReplaceStruct(node.Value!, ownerName, ownerTable);
                return node;
            case "LuaLazyLoadedValue":
                node.Value = ReplaceStruct(node.Value!, ownerName, ownerTable);
                return node;
            case "tuple":
                node.Values = node.Values?.Select(v => ReplaceStruct(v, ownerName, ownerTable)).ToList();
                return node;
            case "table":
                node.Parameters = node.Parameters?.Select(p =>
                {
                    p.Type = Reparse(ReplaceStruct(TypeNode.Parse(p.Type), ownerName, ownerTable));
                    return p;
                }).ToList();
                if (node.Groups != null)
                    foreach (var g in node.Groups)
                        foreach (var p in g.Parameters)
                            p.Type = Reparse(ReplaceStruct(TypeNode.Parse(p.Type), ownerName, ownerTable));
                return node;
            default:
                return node;
        }
    }

    private static JsonElement Reparse(TypeNode n) => JsonSerializer.SerializeToElement(ToJson(n));

    private static object ToJson(TypeNode n) => n.Kind switch
    {
        "simple" => n.Name!,
        "union" => new { complex_type = "union", options = (n.Options ?? new()).Select(ToJson).ToArray() },
        "array" => new { complex_type = "array", value = ToJson(n.Value!) },
        "dictionary" => new { complex_type = "dictionary", key = ToJson(n.Key!), value = ToJson(n.Value!) },
        "LuaCustomTable" => new { complex_type = "LuaCustomTable", key = ToJson(n.Key!), value = ToJson(n.Value!) },
        "LuaLazyLoadedValue" => new { complex_type = "LuaLazyLoadedValue", value = ToJson(n.Value!) },
        "tuple" => new { complex_type = "tuple", values = (n.Values ?? new()).Select(ToJson).ToArray() },
        "table" => new
        {
            complex_type = "table",
            parameters = (n.Parameters ?? new()).Select(p => new
            {
                name = p.Name, order = p.Order, description = p.Description,
                type = JsonSerializer.Deserialize<object>(p.Type.GetRawText()), optional = p.Optional,
            }).ToArray(),
        },
        "literal" => new { complex_type = "literal", value = n.LiteralValue },
        "function" => new { complex_type = "function", parameters = (n.FunctionParameters ?? new()).ToArray() },
        _ => (object)new { complex_type = n.Kind },
    };
}
