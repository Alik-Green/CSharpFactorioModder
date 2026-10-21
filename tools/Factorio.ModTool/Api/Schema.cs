using System.Text.Json;
using System.Text.Json.Serialization;

namespace Factorio.ModTool.Api;

// DTOs mirroring https://lua-api.factorio.com/latest/auxiliary/json-docs-runtime.html (api_version 6).
// Every list in the format is sorted alphabetically by name and null members are omitted.

public sealed class ApiDocument
{
    [JsonPropertyName("application")] public string Application { get; set; } = "";
    [JsonPropertyName("application_version")] public string ApplicationVersion { get; set; } = "";
    [JsonPropertyName("api_version")] public int ApiVersion { get; set; }
    [JsonPropertyName("stage")] public string Stage { get; set; } = "";
    [JsonPropertyName("classes")] public List<ClassDef> Classes { get; set; } = new();
    [JsonPropertyName("events")] public List<EventDef> Events { get; set; } = new();
    [JsonPropertyName("concepts")] public List<ConceptDef> Concepts { get; set; } = new();
    [JsonPropertyName("defines")] public List<DefineDef> Defines { get; set; } = new();
    [JsonPropertyName("global_objects")] public List<Parameter>? GlobalObjects { get; set; }
    [JsonPropertyName("global_functions")] public List<MethodDef>? GlobalFunctions { get; set; }

    // prototype stage only
    [JsonPropertyName("prototypes")] public List<PrototypeDef>? Prototypes { get; set; }
    [JsonPropertyName("types")] public List<TypeDef>? Types { get; set; }
}

public class BasicMember
{
    [JsonPropertyName("name")] public string Name { get; set; } = "";
    [JsonPropertyName("order")] public int Order { get; set; }
    [JsonPropertyName("description")] public string Description { get; set; } = "";
    [JsonPropertyName("lists")] public List<string>? Lists { get; set; }
    [JsonPropertyName("examples")] public List<string>? Examples { get; set; }
}

public sealed class ClassDef : BasicMember
{
    [JsonPropertyName("visibility")] public List<string>? Visibility { get; set; }
    [JsonPropertyName("parent")] public string? Parent { get; set; }
    [JsonPropertyName("abstract")] public bool Abstract { get; set; }
    [JsonPropertyName("methods")] public List<MethodDef> Methods { get; set; } = new();
    [JsonPropertyName("attributes")] public List<AttributeDef> Attributes { get; set; } = new();
    [JsonPropertyName("operators")] public JsonElement? Operators { get; set; }
}

public sealed class MethodDef : BasicMember
{
    [JsonPropertyName("visibility")] public List<string>? Visibility { get; set; }
    [JsonPropertyName("raises")] public List<EventRaised>? Raises { get; set; }
    [JsonPropertyName("subclasses")] public List<string>? Subclasses { get; set; }
    [JsonPropertyName("parameters")] public List<Parameter> Parameters { get; set; } = new();
    [JsonPropertyName("variant_parameter_groups")] public List<ParameterGroup>? VariantParameterGroups { get; set; }
    [JsonPropertyName("variant_parameter_description")] public string? VariantParameterDescription { get; set; }
    [JsonPropertyName("variadic_parameter")] public VariadicParameter? VariadicParameter { get; set; }
    [JsonPropertyName("format")] public MethodFormat? Format { get; set; }
    [JsonPropertyName("return_values")] public List<Parameter> ReturnValues { get; set; } = new();
}

public sealed class MethodFormat
{
    [JsonPropertyName("takes_table")] public bool TakesTable { get; set; }
    [JsonPropertyName("table_optional")] public bool TableOptional { get; set; }
}

public sealed class VariadicParameter
{
    [JsonPropertyName("type")] public JsonElement? Type { get; set; }
    [JsonPropertyName("description")] public string? Description { get; set; }
}

public sealed class AttributeDef : BasicMember
{
    [JsonPropertyName("visibility")] public List<string>? Visibility { get; set; }
    [JsonPropertyName("raises")] public List<EventRaised>? Raises { get; set; }
    [JsonPropertyName("subclasses")] public List<string>? Subclasses { get; set; }
    [JsonPropertyName("read_type")] public JsonElement? ReadType { get; set; }
    [JsonPropertyName("write_type")] public JsonElement? WriteType { get; set; }
    [JsonPropertyName("optional")] public bool Optional { get; set; }
}

public sealed class OperatorDef
{
    [JsonPropertyName("name")] public string Name { get; set; } = "";
    [JsonPropertyName("description")] public string? Description { get; set; }
    [JsonPropertyName("parameters")] public List<Parameter>? Parameters { get; set; }
    [JsonPropertyName("return_values")] public List<Parameter>? ReturnValues { get; set; }
}

public sealed class EventDef : BasicMember
{
    [JsonPropertyName("data")] public List<Parameter> Data { get; set; } = new();
    [JsonPropertyName("filter")] public string? Filter { get; set; }
}

public sealed class EventRaised
{
    [JsonPropertyName("name")] public string Name { get; set; } = "";
    [JsonPropertyName("order")] public int Order { get; set; }
    [JsonPropertyName("description")] public string Description { get; set; } = "";
    [JsonPropertyName("timeframe")] public string? Timeframe { get; set; }
    [JsonPropertyName("optional")] public bool Optional { get; set; }
}

public sealed class ConceptDef : BasicMember
{
    [JsonPropertyName("type")] public JsonElement Type { get; set; }
}

public sealed class DefineDef : BasicMember
{
    [JsonPropertyName("values")] public List<DefineValue>? Values { get; set; }
    [JsonPropertyName("subkeys")] public List<DefineDef>? Subkeys { get; set; }
}

public sealed class DefineValue
{
    [JsonPropertyName("name")] public string Name { get; set; } = "";
    [JsonPropertyName("order")] public int Order { get; set; }
    [JsonPropertyName("description")] public string Description { get; set; } = "";
}

public sealed class Parameter
{
    [JsonPropertyName("name")] public string Name { get; set; } = "";
    [JsonPropertyName("order")] public int Order { get; set; }
    [JsonPropertyName("description")] public string Description { get; set; } = "";
    [JsonPropertyName("type")] public JsonElement Type { get; set; }
    [JsonPropertyName("optional")] public bool Optional { get; set; }
}

public sealed class ParameterGroup
{
    [JsonPropertyName("name")] public string Name { get; set; } = "";
    [JsonPropertyName("order")] public int Order { get; set; }
    [JsonPropertyName("description")] public string Description { get; set; } = "";
    [JsonPropertyName("parameters")] public List<Parameter> Parameters { get; set; } = new();
}

// ---------------------------------------------------------------- prototype stage

public sealed class PrototypeDef : BasicMember
{
    [JsonPropertyName("typename")] public string TypeName { get; set; } = "";
    [JsonPropertyName("parent")] public string? Parent { get; set; }
    [JsonPropertyName("abstract")] public bool Abstract { get; set; }
    [JsonPropertyName("properties")] public List<PrototypeProperty> Properties { get; set; } = new();
    [JsonPropertyName("variants")] public JsonElement? Variants { get; set; }
}

public sealed class TypeDef : BasicMember
{
    [JsonPropertyName("parent")] public string? Parent { get; set; }
    [JsonPropertyName("abstract")] public bool Abstract { get; set; }
    [JsonPropertyName("inline")] public bool Inline { get; set; }
    [JsonPropertyName("type")] public JsonElement Type { get; set; }
    [JsonPropertyName("properties")] public List<PrototypeProperty> Properties { get; set; } = new();
}

public sealed class PrototypeProperty
{
    [JsonPropertyName("name")] public string Name { get; set; } = "";
    [JsonPropertyName("order")] public int Order { get; set; }
    [JsonPropertyName("description")] public string Description { get; set; } = "";
    [JsonPropertyName("type")] public JsonElement Type { get; set; }
    [JsonPropertyName("optional")] public bool Optional { get; set; }
    [JsonPropertyName("alt_name")] public string? AltName { get; set; }
    [JsonPropertyName("override_release")] public string? OverrideRelease { get; set; }
}
