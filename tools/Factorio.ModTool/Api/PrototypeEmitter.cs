using System.Text;

namespace Factorio.ModTool.Api;

/// <summary>
/// Emits the *prototype* (data stage) bindings: `data`, one C# class per prototype type, and the
/// reusable `types` shapes they refer to.
///
/// This stage is deliberately separate from the runtime bindings: it runs in `data.lua`, has no
/// `game`/`script`, and its root object is the `data` table. Prototypes are plain Lua tables a mod
/// writes, so the generated classes are typed views over those tables.
/// </summary>
public sealed class PrototypeEmitter
{
    private const string PrototypeNamespace = "Factorio.Prototypes";

    private readonly ApiDocument _doc;
    private readonly string _version;
    private readonly TypeMapper _mapper;

    public PrototypeEmitter(ApiDocument doc, string version)
    {
        _doc = doc;
        _version = version;
        _mapper = new TypeMapper(doc);
    }

    public EmitResult Emit()
    {
        var result = new EmitResult();

        // The prototype stage documents reusable shapes as `types`; register them as concepts so
        // that prototype properties resolve (Sprite, Sound, Energy, ...).
        var typesByName = (_doc.Types ?? new()).ToDictionary(t => t.Name, StringComparer.Ordinal);
        foreach (var t in typesByName.Values)
            _mapper.AddConcept(t.Name, PrototypeTypes.ToTypeNode(t));

        var prototypes = _doc.Prototypes?.OrderBy(p => p.Name, StringComparer.Ordinal).ToList() ?? new();

        // 1. map every property first (this materialises nested table classes), then fill their fields
        var mapped = new Dictionary<string, List<(PrototypeProperty prop, string csType)>>(StringComparer.Ordinal);
        foreach (var proto in prototypes)
        {
            var list = new List<(PrototypeProperty, string)>();
            foreach (var prop in proto.Properties.OrderBy(p => p.Name, StringComparer.Ordinal))
            {
                var t = _mapper.Map(PrototypeTypes.ToTypeNode(prop.Type, proto.Name, typesByName),
                    proto.Name + "::" + prop.Name);
                list.Add((prop, t.Cs));
            }
            mapped[proto.Name] = list;
        }
        for (int pass = 0; pass < 8; pass++)
        {
            var before = _mapper.TableClasses.Count;
            foreach (var tc in _mapper.TableClasses.Values.ToList()) FillTableClass(tc);
            if (_mapper.TableClasses.Count == before) break;
        }

        // 2. the data table and its globals
        result.Files.Add(new GeneratedFile { Path = "Prototypes/Globals.cs", Content = EmitGlobals(), Kind = "prototype" });

        // 3. one file per reusable type shape
        foreach (var tc in _mapper.TableClasses.Values.OrderBy(t => t.Name, StringComparer.Ordinal))
            result.Files.Add(new GeneratedFile { Path = $"Prototypes/Types/{tc.Name}.cs", Content = EmitTableClass(tc), Kind = "prototype" });

        // 4. one file per prototype
        foreach (var proto in prototypes)
            result.Files.Add(new GeneratedFile
            {
                Path = $"Prototypes/{proto.Name}.cs",
                Content = EmitPrototype(proto, prototypes, mapped[proto.Name]),
                Kind = "prototype",
            });

        foreach (var d in _mapper.Diagnostics) result.Diagnostics.Add(d);
        result.Diagnostics.Add(new Diagnostic
        {
            Stage = "prototype",
            Severity = "info",
            ApiMember = "data-stage",
            Message = "prototype tables are emitted as typed views; building a prototype table from C# is not generated",
            Detail = "Use the raw data table or a hand written helper (docs/LIMITATIONS.md)",
        });
        return result;
    }

    private void FillTableClass(TableClass tc)
    {
        tc.Fields.Clear();
        var seen = new HashSet<string>(StringComparer.Ordinal);
        var used = new HashSet<string>(StringComparer.Ordinal);
        foreach (var p in tc.Parameters.OrderBy(p => p.Order).ThenBy(p => p.Name, StringComparer.Ordinal))
        {
            if (!seen.Add(p.Name)) continue;
            var t = _mapper.Map(TypeNode.Parse(p.Type), tc.Source + "::" + p.Name, input: true);
            tc.Fields.Add(new TableField
            {
                Name = p.Name,
                CsName = Naming.Unique(Naming.Member(Naming.Pascal(p.Name), tc.Name), used),
                CsType = t.Cs,
                Optional = p.Optional,
                Doc = Docs.ToSummary(p.Description, 300),
            });
        }
    }

    private string EmitGlobals()
    {
        var w = new CodeWriter();
        w.Line(Header("global objects of the data stage").TrimEnd());
        w.Line("#nullable disable");
        w.Line("using System;");
        w.Line("using Factorio;");
        w.Line();
        w.Open($"namespace {PrototypeNamespace}");
        w.Line("/// <summary>");
        w.Line("/// The data stage globals. Unlike the runtime stage there is no <c>game</c> or <c>script</c>");
        w.Line("/// here: prototype tables are built in <c>data.lua</c> before the game starts.");
        w.Line("/// </summary>");
        w.Line("/// @CSharpLua.Ignore");
        w.Open("public static class Globals");
        w.Line("/// <summary>The native <c>data</c> table that holds every prototype.</summary>");
        w.Line("/// @CSharpLua.Get = \"data\"");
        w.Line("public static Data Data { get; }");
        w.Line();
        w.Line("/// <summary>Writes a line to the Factorio log file (the data stage equivalent of <c>log()</c>).</summary>");
        w.Line("/// <param name=\"message\">Value to log; tables are serialised by the game.</param>");
        w.Line("/// @CSharpLua.Template = \"log({0})\"");
        w.Line("public static void Log(object message) { throw new NotImplementedException(); }");
        w.Close();
        w.Line();
        w.Line("/// <summary>");
        w.Line("/// The <c>data</c> table: <c>Data.Raw[type][name]</c> holds every prototype of the game.");
        w.Line("/// </summary>");
        w.Line("/// @CSharpLua.Ignore");
        w.Open("public partial class Data");
        w.Line("/// <summary>All prototypes, indexed first by prototype type (\"item\", \"recipe\", ...) and then by name.</summary>");
        w.Line("/// @CSharpLua.Get = \"{this}.raw\"");
        w.Line("/// @CSharpLua.Set = \"{this}.raw = {0}\"");
        w.Line("public LuaCustomTable<string, LuaCustomTable<string, object>> Raw { get; set; }");
        w.Close();
        w.Close();
        return w.ToString();
    }

    private string EmitTableClass(TableClass tc)
    {
        var w = new CodeWriter();
        w.Line(Header($"prototype type {tc.Name}").TrimEnd());
        w.Line("#nullable disable");
        w.Line("using System;");
        w.Line("using Factorio;");
        w.Line();
        w.Open($"namespace {PrototypeNamespace}");
        if (!string.IsNullOrEmpty(tc.Description))
            w.Line("/// <summary>" + Docs.ToSummary(tc.Description) + "</summary>");
        else
            w.Line("/// <summary>Shape used by <c>" + Docs.Escape(tc.Source) + "</c> in the data stage.</summary>");
        w.Line("/// @CSharpLua.Ignore");
        w.Open($"public partial class {tc.Name}");
        foreach (var f in tc.Fields)
        {
            if (!string.IsNullOrEmpty(f.Doc)) w.Line("/// <summary>" + f.Doc + "</summary>");
            w.Line("/// @CSharpLua.Get = \"{this}" + Naming.LuaMember(f.Name) + "\"");
            w.Line("/// @CSharpLua.Set = \"{this}" + Naming.LuaMember(f.Name) + " = {0}\"");
            w.Line($"public {f.CsType} {f.CsName} {{ get; set; }}");
        }
        w.Close();
        w.Close();
        return w.ToString();
    }

    private string EmitPrototype(PrototypeDef proto, List<PrototypeDef> all, List<(PrototypeProperty prop, string csType)> properties)
    {
        var w = new CodeWriter();
        w.Line(Header($"prototype {proto.Name}").TrimEnd());
        w.Line("#nullable disable");
        w.Line("using System;");
        w.Line("using Factorio;");
        w.Line();
        w.Open($"namespace {PrototypeNamespace}");
        if (!string.IsNullOrEmpty(proto.Description))
            w.Line("/// <summary>" + Docs.ToSummary(proto.Description) + "</summary>");
        else
            w.Line("/// <summary>Prototype <c>" + Docs.Escape(proto.TypeName) + "</c> in the data stage.</summary>");
        w.Line("/// <remarks>");
        w.Line("/// Prototype type name: <c>" + Docs.Escape(proto.TypeName) + "</c>. Prototypes are plain Lua tables;");
        w.Line("/// a C# value of this type is a view over such a table, not an object of its own.");
        w.Line("/// See <see href=\"" + Docs.DocLink(_version, "prototype", proto.Name) + "\"/>.");
        w.Line("/// </remarks>");
        w.Line("/// @CSharpLua.Ignore");
        var hasParent = proto.Parent != null && all.Any(p => p.Name == proto.Parent);
        var baseClass = hasParent ? " : " + proto.Parent : "";
        w.Open($"public {(proto.Abstract ? "abstract " : "")}partial class {proto.Name}{baseClass}");

        var typeDeclaredInBase = hasParent && InheritsMember(all, proto.Name, "type");
        var used = new HashSet<string>(StringComparer.Ordinal);
        if (!typeDeclaredInBase)
        {
            used.Add("Type");
            w.Line("/// <summary>The prototype type name, e.g. <c>\"item\"</c>.</summary>");
            w.Line("/// @CSharpLua.Get = \"{this}.type\"");
            w.Line("/// @CSharpLua.Set = \"{this}.type = {0}\"");
            w.Line("public string Type { get; set; }");
        }

        foreach (var (prop, csType) in properties)
        {
            var csName = Naming.Unique(Naming.Member(Naming.Pascal(prop.Name), proto.Name), used);
            if (!string.IsNullOrEmpty(prop.Description))
                w.Line("/// <summary>" + Docs.ToSummary(prop.Description, 300) + "</summary>");
            else
                w.Line("/// <summary>Value of the native <c>" + Docs.Escape(prop.Name) + "</c> key of this prototype.</summary>");
            w.Line("/// @CSharpLua.Get = \"{this}" + Naming.LuaMember(prop.Name) + "\"");
            w.Line("/// @CSharpLua.Set = \"{this}" + Naming.LuaMember(prop.Name) + " = {0}\"");
            w.Line($"public {csType} {csName} {{ get; set; }}");
        }

        w.Close();
        w.Close();
        return w.ToString();
    }

    private static bool InheritsMember(List<PrototypeDef> prototypes, string name, string member)
    {
        var byName = prototypes.ToDictionary(p => p.Name, StringComparer.Ordinal);
        var parent = byName.TryGetValue(name, out var self) ? self.Parent : null;
        var guard = 0;
        while (parent != null && byName.TryGetValue(parent, out var p) && guard++ < 64)
        {
            if (p.Properties.Any(x => x.Name == member) || p.Name == member) return true;
            parent = p.Parent;
        }
        return false;
    }

    private static string Header(string what) =>
        "// <auto-generated>\n" +
        "//   Factorio prototype API: " + what + ".\n" +
        "//   Generated by factorio-mod; do not edit. Run `factorio-mod generate` to refresh.\n" +
        "// </auto-generated>";
}
