using System.Text;
using System.Text.Json;

namespace Factorio.ModTool.Api;

/// <summary>
/// Emits the *runtime* stage bindings: one C# file per native type, with a C#-idiomatic surface
/// (PascalCase members, properties for attributes, XML documentation on everything) while the
/// emitted Lua keeps the exact names and calling conventions of the game.
///
/// Mapping mechanism (all of it compile-time only):
///   * every Factorio class becomes a class marked `@CSharpLua.Ignore`: no Lua class, constructor
///     or metatable is emitted, the C# type is a *view* over a native Lua table owned by the game;
///   * attributes become properties with `@CSharpLua.Get` / `@CSharpLua.Set` templates, which is
///     how a PascalCase property still compiles to `obj.health`;
///   * methods become methods with `@CSharpLua.Template = "{this}.name({0})"`, required because
///     Factorio uses dot calls while CSharp.lua would emit `obj:name(...)` (implicit self);
///   * globals become static properties (`Globals.Game` -> `game`);
///   * `defines.*` values become static properties (`Defines.Events.OnTick` -> `defines.events.on_tick`);
///   * structured table parameters become classes whose constructor template builds the Lua table.
/// </summary>
public sealed class RuntimeEmitter
{
    private sealed class PlannedProperty
    {
        public string LuaName = "";
        public string CsName = "";
        public string CsType = "object";
        public bool Readable;
        public bool Writable;
        public string Doc = "";
        public List<string> Remarks = new();
    }

    private sealed class PlannedParameter
    {
        public string LuaName = "";
        public string CsName = "";
        public string CsType = "object";
        public bool Optional;
        public bool IsParams;
        public string Doc = "";
    }

    private sealed class PlannedMethod
    {
        public string LuaName = "";
        public string CsName = "";
        public List<PlannedParameter> Parameters = new();
        public List<PlannedParameter> OutParameters = new();
        public string ReturnType = "void";
        public string Doc = "";
        public string ReturnDoc = "";
        public List<string> Remarks = new();
        public int MinArity;
        public bool HasParams;
        /// <summary>True for Factorio's global functions: they have no receiver, so the template is <c>name(...)</c>.</summary>
        public bool IsGlobal;
    }

    private sealed class PlannedClass
    {
        public ClassDef Def = null!;
        public string Name = "";
        public string? Parent;
        public List<PlannedProperty> Properties = new();
        public List<PlannedMethod> Methods = new();
    }

    private const string RootObjectClass = "LuaObject";
    private const string Ignore = "/// @CSharpLua.Ignore";
    private const string RuntimeNamespace = "Factorio.Runtime";

    private readonly ApiDocument _doc;
    private readonly TypeMapper _mapper;
    private readonly string _version;
    private readonly bool _runtimeStage;

    private readonly List<PlannedClass> _classes = new();
    private readonly List<(string lua, string cs, string csType, string doc, Parameter def)> _globals = new();
    private readonly List<PlannedMethod> _globalFunctions = new();
    private readonly List<(EventDef def, string className)> _events = new();
    private readonly Dictionary<string, string> _eventHelpers = new(StringComparer.Ordinal);

    public RuntimeEmitter(ApiDocument doc, TypeMapper mapper, string version, bool runtimeStage)
    {
        _doc = doc;
        _mapper = mapper;
        _version = version;
        _runtimeStage = runtimeStage;
    }

    // ================================================================= planning

    public void Plan()
    {
        foreach (var c in _doc.Classes.OrderBy(c => c.Name, StringComparer.Ordinal))
            _classes.Add(PlanClass(c));

        if (_runtimeStage)
        {
            foreach (var g in _doc.GlobalObjects ?? new())
            {
                var mapped = _mapper.Map(TypeNode.Parse(g.Type), "global " + g.Name);
                if (mapped.Cs == "object")
                    _mapper.Skipped("global " + g.Name, "global object could not be mapped to a concrete C# type");
                _globals.Add((g.Name, Naming.Pascal(g.Name), mapped.Cs, Docs.ToSummary(g.Description), g));
            }
            foreach (var f in _doc.GlobalFunctions ?? new())
            {
                var pm = PlanMethod("Globals", f, isStatic: true);
                if (pm != null)
                {
                    pm.IsGlobal = true;
                    _globalFunctions.Add(pm);
                }
            }
            PlanEvents();
        }

        // Resolve every generated table class (nested table types are materialised while mapping).
        for (int pass = 0; pass < 16; pass++)
        {
            var before = _mapper.TableClasses.Count;
            foreach (var tc in _mapper.TableClasses.Values.ToList()) PlanTableClass(tc);
            if (_mapper.TableClasses.Count == before) break;
        }
    }

    private PlannedClass PlanClass(ClassDef c)
    {
        var pc = new PlannedClass { Def = c, Name = c.Name };
        if (c.Parent != null && _mapper.IsClass(c.Parent)) pc.Parent = c.Parent;
        else if (c.Name != RootObjectClass) pc.Parent = RootObjectClass;

        var inherited = new HashSet<string>(StringComparer.Ordinal);
        var ancestor = pc.Parent;
        var guard = 0;
        while (ancestor != null && _mapper.IsClass(ancestor) && guard++ < 64)
        {
            var ac = _mapper.GetClass(ancestor)!;
            foreach (var a in ac.Attributes) inherited.Add("a:" + a.Name);
            foreach (var m in ac.Methods) inherited.Add("m:" + m.Name);
            ancestor = ac.Parent;
        }

        var usedNames = new HashSet<string>(StringComparer.Ordinal);
        foreach (var a in c.Attributes.OrderBy(a => a.Name, StringComparer.Ordinal))
        {
            if (inherited.Contains("a:" + a.Name))
            {
                _mapper.Info($"{c.Name}::{a.Name}", "attribute already declared on a base class, not re-declared");
                continue;
            }
            var p = PlanProperty(c.Name, a, usedNames);
            if (p != null) pc.Properties.Add(p);
        }

        foreach (var m in c.Methods.OrderBy(m => m.Name, StringComparer.Ordinal))
        {
            if (inherited.Contains("m:" + m.Name))
            {
                _mapper.Info($"{c.Name}::{m.Name}", "method already declared on a base class, not re-declared");
                continue;
            }
            var pm = PlanMethod(c.Name, m, isStatic: false);
            if (pm == null) continue;
            pm.CsName = Naming.Unique(pm.CsName, usedNames);
            pc.Methods.Add(pm);
        }

        if (c.Operators is { } ops && ops.ValueKind == JsonValueKind.Object && ops.EnumerateObject().Any())
            _mapper.Info($"{c.Name}", "Lua operator metamethods are not bound; call the explicit methods instead",
                ops.ToString());

        return pc;
    }

    private PlannedProperty? PlanProperty(string className, AttributeDef a, ISet<string> usedNames)
    {
        var member = $"{className}::{a.Name}";
        MappedType? read = null, write = null;
        if (a.ReadType is { } rt && rt.ValueKind != JsonValueKind.Null)
            read = _mapper.Map(TypeNode.Parse(rt), member);
        if (a.WriteType is { } wt && wt.ValueKind != JsonValueKind.Null)
            write = _mapper.Map(TypeNode.Parse(wt), member);
        if (read == null && write == null)
        {
            _mapper.Skipped(member, "attribute has neither read_type nor write_type");
            return null;
        }

        var p = new PlannedProperty
        {
            LuaName = a.Name,
            // The collision check compares against the name the class is *emitted* with: LuaGroup
            // becomes Group, so its `group` attribute would otherwise produce `Group Group`.
            CsName = Naming.Unique(Naming.Member(Naming.Pascal(a.Name), _mapper.Renamer.Rename(className)), usedNames),
            Readable = read != null,
            Writable = write != null,
            Doc = Docs.ToSummary(a.Description),
        };

        if (read != null && write != null && read.Cs != write.Cs)
        {
            p.CsType = "object";
            var note = $"the documented read type ({read.Cs}) and write type ({write.Cs}) differ, so the value is exposed as object";
            p.Remarks.Add(note);
            _mapper.Degraded(member, note);
        }
        else
        {
            p.CsType = (read ?? write!).Cs;
        }

        if (p.Doc.Length == 0)
            p.Doc = Docs.Fallback(p.Readable && p.Writable ? "readwrite" : p.Readable ? "read" : "write",
                a.Name, className);
        if (a.Visibility is { Count: > 0 })
            p.Remarks.Add("Restricted to the game expansion(s): " + string.Join(", ", a.Visibility) + ".");
        if (a.Subclasses is { Count: > 0 })
            p.Remarks.Add("Only meaningful on these subclasses: " + string.Join(", ", a.Subclasses) + ".");
        return p;
    }

    private PlannedMethod? PlanMethod(string className, MethodDef m, bool isStatic)
    {
        var member = isStatic ? $"global::{m.Name}" : $"{className}::{m.Name}";
        var pm = new PlannedMethod
        {
            LuaName = m.Name,
            CsName = Naming.Member(Naming.Pascal(m.Name), _mapper.Renamer.Rename(className)),
            Doc = Docs.ToSummary(m.Description),
        };

        if (m.Format?.TakesTable == true)
        {
            var tableClass = new TableClass
            {
                Name = UniqueTableName(Naming.Pascal(className) + Naming.Pascal(m.Name) + "Parameters"),
                Description = m.Description,
                Parameters = m.Parameters,
                Groups = m.VariantParameterGroups ?? new(),
                Source = member,
            };
            _mapper.TableClasses[tableClass.Name] = tableClass;
            _mapper.MarkInput(tableClass.Name);
            if (m.VariantParameterGroups is { Count: > 0 })
            {
                _mapper.Info(member, "variant parameter groups merged into one optional-parameter table class",
                    string.Join("; ", m.VariantParameterGroups.Select(g => g.Name)));
                pm.Remarks.Add("The documented variant parameter groups (" +
                    string.Join(", ", m.VariantParameterGroups.Select(g => Naming.Pascal(g.Name))) +
                    ") are merged into one parameter table; only the keys that belong to the chosen variant are meaningful.");
            }
            pm.Parameters.Add(new PlannedParameter
            {
                CsName = "parameters",
                CsType = tableClass.Name,
                Optional = m.Format?.TableOptional ?? false,
                LuaName = "table",
                Doc = "The native parameter table. Build it with the generated constructor or reuse a table returned by the API.",
            });
        }
        else
        {
            foreach (var p in m.Parameters.OrderBy(p => p.Order).ThenBy(p => p.Name, StringComparer.Ordinal))
            {
                var mapped = _mapper.Map(TypeNode.Parse(p.Type), member + "::" + p.Name, input: true);
                var csType = mapped.Cs;
                if (p.Optional && mapped.IsValueType && csType != "object") csType += "?";
                pm.Parameters.Add(new PlannedParameter
                {
                    LuaName = p.Name,
                    CsName = Naming.Escape(Naming.Camel(p.Name)),
                    CsType = csType,
                    Optional = p.Optional,
                    Doc = Docs.ToSummary(p.Description, 300),
                });
            }
        }

        if (m.VariadicParameter != null)
        {
            pm.HasParams = true;
            pm.Parameters.Add(new PlannedParameter
            {
                LuaName = "...",
                CsName = "args",
                CsType = "object",
                IsParams = true,
                Doc = Docs.ToSummary(m.VariadicParameter.Description, 300),
            });
        }

        var rets = m.ReturnValues;
        if (rets.Count == 1)
        {
            pm.ReturnType = _mapper.Map(TypeNode.Parse(rets[0].Type), member + "::return").Cs;
            pm.ReturnDoc = Docs.ToSummary(rets[0].Description, 300);
        }
        else if (rets.Count > 1)
        {
            for (int i = 0; i < rets.Count; i++)
            {
                pm.OutParameters.Add(new PlannedParameter
                {
                    CsName = i == 0 ? "result" : "result" + (i + 1),
                    CsType = _mapper.Map(TypeNode.Parse(rets[i].Type), member + $"::return{i + 1}").Cs,
                    Doc = Docs.ToSummary(rets[i].Description, 300),
                });
            }
            pm.Remarks.Add("The native function returns " + rets.Count + " values; they are exposed as out parameters " +
                           "because C# tuples would require the CSharp.lua runtime support library, which Factorio mods cannot load.");
        }

        if (pm.Doc.Length == 0)
            pm.Doc = Docs.Fallback(isStatic ? "function" : "method", m.Name, isStatic ? null : className);
        if (m.Visibility is { Count: > 0 })
            pm.Remarks.Add("Restricted to the game expansion(s): " + string.Join(", ", m.Visibility) + ".");
        if (m.Subclasses is { Count: > 0 })
            pm.Remarks.Add("Only meaningful on these subclasses: " + string.Join(", ", m.Subclasses) + ".");

        // C# requires optional parameters to be trailing, and `out` parameters to come last.
        int lastRequired = -1;
        for (int i = 0; i < pm.Parameters.Count; i++)
            if (!pm.Parameters[i].Optional && !pm.Parameters[i].IsParams) lastRequired = i;

        if (pm.OutParameters.Count > 0)
        {
            foreach (var p in pm.Parameters) p.Optional = false;
            pm.MinArity = pm.Parameters.Count;
        }
        else if (pm.HasParams)
        {
            pm.MinArity = pm.Parameters.Count;
        }
        else
        {
            for (int i = 0; i <= lastRequired; i++) pm.Parameters[i].Optional = false;
            pm.MinArity = lastRequired + 1;
        }

        return pm;
    }

    private void PlanTableClass(TableClass tc)
    {
        tc.Fields.Clear();
        var ordered = new List<(Parameter def, bool fromGroup)>();
        foreach (var p in tc.Parameters.OrderBy(p => p.Order).ThenBy(p => p.Name, StringComparer.Ordinal))
            ordered.Add((p, false));
        foreach (var g in tc.Groups.OrderBy(g => g.Order))
            foreach (var p in g.Parameters.OrderBy(p => p.Order).ThenBy(p => p.Name, StringComparer.Ordinal))
                ordered.Add((p, true));

        var seen = new HashSet<string>(StringComparer.Ordinal);
        var usedNames = new HashSet<string>(StringComparer.Ordinal);
        // Required fields first: the constructor cannot have a required parameter after an optional one.
        foreach (var (p, fromGroup) in ordered.OrderBy(x => x.def.Optional || x.fromGroup).ThenBy(x => x.def.Order))
        {
            if (!seen.Add(p.Name))
            {
                _mapper.Info(tc.Source + "::" + p.Name,
                    "duplicate field from a variant parameter group, merged into one table key");
                continue;
            }
            var mapped = _mapper.Map(TypeNode.Parse(p.Type), tc.Source + "::" + p.Name, input: true);
            var csType = mapped.Cs;
            var optional = p.Optional || fromGroup;
            if (optional && mapped.IsValueType && csType != "object") csType += "?";
            tc.Fields.Add(new TableField
            {
                Name = p.Name,
                CsName = Naming.Unique(Naming.Member(Naming.Pascal(p.Name), tc.Name), usedNames),
                CsType = csType,
                Optional = optional,
                Doc = Docs.ToSummary(p.Description, 300),
            });
        }
        if (tc.Groups.Count > 0) _mapper.MarkInput(tc.Name);
    }

    private string UniqueTableName(string name)
    {
        if (!_mapper.TableClasses.ContainsKey(name)) return name;
        for (int i = 2; ; i++)
            if (!_mapper.TableClasses.ContainsKey(name + i)) return name + i;
    }

    private void PlanEvents()
    {
        if (_doc.Concepts.FirstOrDefault(c => c.Name == "EventData") is { } eventData)
            _mapper.Map(TypeNode.Parse(eventData.Type), "concept EventData", "EventData");

        var defineNames = new HashSet<string>(StringComparer.Ordinal);
        var eventsDef = _doc.Defines.FirstOrDefault(d => d.Name == "events");
        foreach (var v in eventsDef?.Values ?? new()) defineNames.Add(v.Name);

        var used = new HashSet<string>(StringComparer.Ordinal);
        foreach (var e in _doc.Events.OrderBy(x => x.Name, StringComparer.Ordinal))
        {
            var name = Naming.Pascal(e.Name) + "Event";
            while (!used.Add(name)) name += "_";
            _events.Add((e, name));
            var cls = new TableClass
            {
                Name = name,
                Description = e.Description,
                BaseClass = "EventData",
                Source = "event " + e.Name,
                EmitConstructor = false,
            };
            foreach (var d in e.Data.OrderBy(p => p.Order))
            {
                if (d.Name is "name" or "tick" or "mod_name") continue;
                cls.Parameters.Add(d);
            }
            _mapper.TableClasses[name] = cls;
            if (defineNames.Contains(e.Name)) _eventHelpers[e.Name] = name;
            else _mapper.Info("event " + e.Name, "event has no `defines.events` entry; only the event data class is generated");
        }
        if (_mapper.TableClasses.TryGetValue("EventData", out var ed)) ed.IsBase = true;
    }

    // ================================================================== emitting

    public EmitResult Emit()
    {
        var result = new EmitResult();
        result.Diagnostics.AddRange(_mapper.Diagnostics);

        foreach (var c in _classes)
            result.Files.Add(new GeneratedFile
            {
                Path = $"Runtime/Objects/{_mapper.Renamer.Rename(c.Name)}.cs",
                Content = EmitClass(c),
                Kind = "runtime",
            });

        foreach (var tc in _mapper.TableClasses.Values.OrderBy(t => t.Name, StringComparer.Ordinal))
            result.Files.Add(new GeneratedFile
            {
                Path = tc.BaseClass != null ? $"Runtime/Events/{tc.Name}.cs" : $"Runtime/Tables/{tc.Name}.cs",
                Content = EmitTableClass(tc),
                Kind = "runtime",
            });

        if (_runtimeStage)
        {
            result.Files.Add(new GeneratedFile { Path = "Runtime/Globals.cs", Content = EmitGlobals(), Kind = "runtime" });
            result.Files.Add(new GeneratedFile { Path = "Runtime/Defines.cs", Content = EmitDefines(), Kind = "runtime" });
            result.Files.Add(new GeneratedFile { Path = "Runtime/Events.cs", Content = EmitEventHelpers(), Kind = "runtime" });
        }

        return result;
    }

    private static string FileHeader(string what)
    {
        var sb = new StringBuilder();
        sb.AppendLine("// <auto-generated>");
        sb.AppendLine("//   " + what);
        sb.AppendLine("//   Generated by factorio-mod; do not edit. Run `factorio-mod generate` to refresh.");
        sb.AppendLine("// </auto-generated>");
        return sb.ToString();
    }

    private string EmitClass(PlannedClass c)
    {
        var w = new CodeWriter();
        var def = c.Def;
        w.Line("// <auto-generated>");
        w.Line($"//   Factorio {_version} runtime API: {c.Name}.");
        w.Line("//   Generated by factorio-mod; do not edit. Run `factorio-mod generate` to refresh.");
        w.Line("// </auto-generated>");
        w.Line("#nullable disable");
        w.Line("using System;");
        w.Line("using Factorio;");
        w.Line();
        w.Open($"namespace {RuntimeNamespace}");
        if (!string.IsNullOrEmpty(def.Description))
            w.Line("/// <summary>" + Docs.ToSummary(def.Description) + "</summary>");
        else
            w.Line("/// <summary>" + Docs.Fallback("type", c.Name) + "</summary>");
        w.Line("/// <remarks>");
        w.Line("/// Native Factorio object of type <c>" + c.Name + "</c>. Instances are created and owned by the");
        w.Line("/// game; a C# value of this type is a view over the native Lua table and is never boxed or");
        w.Line("/// copied. See <see href=\"" + Docs.DocLink(_version, "runtime", c.Name) + "\"/>.");
        if (def.Abstract) w.Line("/// This type is abstract in the Factorio API: the game never hands out a bare instance.");
        if (def.Visibility is { Count: > 0 })
            w.Line("/// Restricted to the game expansion(s): " + Docs.Escape(string.Join(", ", def.Visibility)) + ".");
        w.Line("/// </remarks>");
        w.Line(Ignore);
        var baseClass = c.Parent != null ? " : " + _mapper.Renamer.Rename(c.Parent) : "";
        w.Open($"public {(def.Abstract ? "abstract " : "")}partial class {_mapper.Renamer.Rename(c.Name)}{baseClass}");
        foreach (var p in c.Properties) EmitProperty(w, p, isStatic: false);
        foreach (var m in c.Methods) EmitMethod(w, m, isStatic: false);
        w.Close();
        w.Close();
        return w.ToString();
    }

    /// <summary>Value types need `= default`; reference types need `= null` in an optional parameter.</summary>
    internal static bool IsValueType(string csType) =>
        csType is "double" or "bool" or "float" or "int" or "long" or "byte" or "uint"
        || (csType.EndsWith("?", StringComparison.Ordinal) && IsValueType(csType[..^1]));

    private void EmitProperty(CodeWriter w, PlannedProperty p, bool isStatic)
    {
        if (!string.IsNullOrEmpty(p.Doc)) w.Line("/// <summary>" + p.Doc + "</summary>");
        foreach (var r in p.Remarks) w.Line("/// <remarks>" + Docs.Remark(r) + "</remarks>");
        w.Line("/// @CSharpLua.Get = " + "\"{this}" + Naming.LuaMember(p.LuaName) + "\"");
        if (p.Writable)
            w.Line("/// @CSharpLua.Set = " + "\"{this}" + Naming.LuaMember(p.LuaName) + " = {0}\"");
        var modifier = isStatic ? "static " : "";
        var accessors = p.Readable && p.Writable ? "{ get; set; }"
            : p.Readable ? "{ get; }"
            : "{ set { } }";
        w.Line($"public {modifier}{p.CsType} {p.CsName} {accessors}");
    }

    private void EmitMethod(CodeWriter w, PlannedMethod m, bool isStatic)
    {
        for (int arity = m.MinArity; arity <= m.Parameters.Count; arity++)
        {
            var ps = m.Parameters.Take(arity).ToList();
            var decls = new List<string>();
            var callArgs = new List<string>();
            for (int i = 0; i < ps.Count; i++)
            {
                var p = ps[i];
                var csType = p.CsType;
                if (p.IsParams)
                {
                    csType = "params object[]";
                    callArgs.Add("{*" + i + "}");
                }
                else
                {
                    callArgs.Add("{" + i + "}");
                }
                decls.Add($"{csType} {p.CsName}");
            }
            foreach (var o in m.OutParameters) decls.Add($"out {o.CsType} {o.CsName}");

            if (!string.IsNullOrEmpty(m.Doc)) w.Line("/// <summary>" + m.Doc + "</summary>");
            foreach (var p in ps)
                if (!string.IsNullOrEmpty(p.Doc))
                    w.Line($"/// <param name=\"{p.CsName.TrimStart('@')}\">{p.Doc}</param>");
            foreach (var o in m.OutParameters)
                w.Line($"/// <param name=\"{o.CsName}\">{(string.IsNullOrEmpty(o.Doc) ? "Native return value." : o.Doc)}</param>");
            if (m.ReturnType != "void" && !string.IsNullOrEmpty(m.ReturnDoc))
                w.Line("/// <returns>" + m.ReturnDoc + "</returns>");
            foreach (var r in m.Remarks) w.Line("/// <remarks>" + Docs.Remark(r) + "</remarks>");
            // A global function has no receiver: `log(x)`, not `obj.log(x)`.
            var target = m.IsGlobal ? m.LuaName : "{this}" + Naming.LuaMember(m.LuaName);
            w.Line("/// @CSharpLua.Template = " +
                   "\"" + target + "(" + string.Join(", ", callArgs) + ")\"");
            var modifier = isStatic ? "static " : "";
            w.Line($"public {modifier}{m.ReturnType} {m.CsName}({string.Join(", ", decls)}) {{ throw new NotImplementedException(); }}");
        }
    }

    private string EmitTableClass(TableClass tc)
    {
        var w = new CodeWriter();
        w.Line("// <auto-generated>");
        w.Line($"//   Factorio {_version} runtime API: {tc.Name} (Lua table).");
        w.Line("//   Generated by factorio-mod; do not edit. Run `factorio-mod generate` to refresh.");
        w.Line("// </auto-generated>");
        w.Line("#nullable disable");
        w.Line("using System;");
        w.Line("using Factorio;");
        w.Line();
        w.Open($"namespace {RuntimeNamespace}");
        if (!string.IsNullOrEmpty(tc.Description))
            w.Line("/// <summary>" + Docs.ToSummary(tc.Description) + "</summary>");
        else
            w.Line("/// <summary>" + Docs.Fallback("table", tc.Name, tc.Source) + "</summary>");
        w.Line("/// <remarks>");
        w.Line("/// A native Lua table used by <c>" + Docs.Escape(tc.Source) + "</c>.");
        w.Line("/// The properties read and write the native keys (snake_case in Lua, PascalCase in C#);");
        w.Line("/// the constructor builds a Lua table literal, so no C# object ever reaches the game.");
        w.Line("/// </remarks>");
        w.Line(Ignore);
        var baseClass = tc.BaseClass != null ? " : " + tc.BaseClass : "";
        w.Open($"public partial class {tc.Name}{baseClass}");

        foreach (var f in tc.Fields)
        {
            w.Line("/// <summary>" +
                   (string.IsNullOrEmpty(f.Doc)
                       ? Docs.Fallback("field", f.Name, tc.Name)
                       : f.Doc) + "</summary>");
            w.Line("/// @CSharpLua.Get = " + "\"{this}" + Naming.LuaMember(f.Name) + "\"");
            w.Line("/// @CSharpLua.Set = " + "\"{this}" + Naming.LuaMember(f.Name) + " = {0}\"");
            w.Line($"public {f.CsType} {f.CsName} {{ get; set; }}");
        }

        if (tc.IsBase)
        {
            w.Line("/// <summary>Creates an empty instance; the game fills the fields when it raises the event.</summary>");
            w.Line($"protected {tc.Name}() {{ }}");
        }

        if (tc.EmitConstructor && tc.Fields.Count > 0)
        {
            var parts = new List<string>();
            for (int i = 0; i < tc.Fields.Count; i++)
                parts.Add(Naming.LuaKey(tc.Fields[i].Name) + " = {" + i + "}");
            w.Line("/// <summary>Builds the native Lua table literal. Fields that are omitted are simply absent.</summary>");
            w.Line("/// @CSharpLua.Template = " +
                   "\"{" + string.Join(", ", parts) + "}\"");
            var args = tc.Fields
                .Select(f => $"{f.CsType} {Naming.Escape(Naming.Camel(f.Name))}" +
                             (f.Optional ? (IsValueType(f.CsType) ? " = default" : " = null") : ""))
                .ToList();
            w.Line($"public {tc.Name}({string.Join(", ", args)}) {{ }}");
        }

        w.Close();
        w.Close();
        return w.ToString();
    }

    private string EmitGlobals()
    {
        var w = new CodeWriter();
        w.Line(FileHeader($"Factorio {_version} global objects and functions.").TrimEnd());
        w.Line("#nullable disable");
        w.Line("using System;");
        w.Line();
        w.Open($"namespace {RuntimeNamespace}");
        w.Line("/// <summary>");
        w.Line("/// The native Factorio globals: <c>Globals.Game</c> is <c>game</c>, <c>Globals.Script</c> is");
        w.Line("/// <c>script</c>, and the methods are the global Lua functions.");
        w.Line("/// <para>Always qualify them (<c>Globals.Game.Print(...)</c>). Do <b>not</b> write");
        w.Line("/// <c>using static " + RuntimeNamespace + ".Globals;</c>: the pinned CSharp.lua crashes on a static");
        w.Line("/// property used as a receiver through a static import. See docs/LIMITATIONS.md.</para>");
        w.Line("/// </summary>");
        w.Line(Ignore);
        w.Open("public static class Globals");

        foreach (var g in _globals)
        {
            if (!string.IsNullOrEmpty(g.doc)) w.Line("/// <summary>" + g.doc + "</summary>");
            else w.Line("/// <summary>The global <c>" + g.lua + "</c> object.</summary>");
            w.Line("/// @CSharpLua.Get = " + "\"" + g.lua + "\"");
            w.Line($"public static {g.csType} {g.cs} {{ get; }}");
        }

        foreach (var f in _globalFunctions) EmitMethod(w, f, isStatic: true);

        w.Close();
        w.Close();
        return w.ToString();
    }

    private string EmitDefines()
    {
        var w = new CodeWriter();
        w.Line(FileHeader($"Factorio {_version} `defines` constants.").TrimEnd());
        w.Line("#nullable disable");
        w.Line("using System;");
        w.Line();
        w.Open($"namespace {RuntimeNamespace}");
        w.Line("/// <summary>");
        w.Line("/// The native <c>defines</c> table: named constants owned by the game. Every member is");
        w.Line("/// a static property whose value is read from the game, so a missing constant stays");
        w.Line("/// visible as a Lua error instead of being baked into the mod as a stale number.");
        w.Line("/// </summary>");
        w.Line(Ignore);
        w.Open("public static class Defines");
        foreach (var d in _doc.Defines.OrderBy(d => d.Name, StringComparer.Ordinal))
            EmitDefine(w, d, "defines", "Defines");
        w.Close();
        w.Close();
        return w.ToString();
    }

    private void EmitDefine(CodeWriter w, DefineDef def, string luaPath, string csPath)
    {
        var csLeaf = csPath.Split('.').Last();
        var csName = Naming.Member(Naming.Pascal(def.Name), csLeaf);
        var luaName = luaPath + Naming.LuaMember(def.Name);
        w.Line();
        if (!string.IsNullOrEmpty(def.Description))
            w.Line("/// <summary>" + Docs.ToSummary(def.Description, 300) + "</summary>");
        else
            w.Line("/// <summary>Values of the native <c>" + Docs.Escape(luaName) + "</c> table.</summary>");
        w.Line(Ignore);
        w.Open($"public static class {csName}");

        var used = new HashSet<string>(StringComparer.Ordinal);
        foreach (var v in (def.Values ?? new()).OrderBy(v => v.Name, StringComparer.Ordinal))
        {
            var memberName = Naming.Member(Naming.Pascal(v.Name), csName);
            memberName = Naming.Unique(memberName, used);
            if (!string.IsNullOrEmpty(v.Description))
                w.Line("/// <summary>" + Docs.ToSummary(v.Description, 300) + "</summary>");
            else
                w.Line("/// <summary>Value of the native <c>" + Docs.Escape(luaPath + Naming.LuaMember(def.Name) + Naming.LuaMember(v.Name)) + "</c> constant.</summary>");
            w.Line("/// @CSharpLua.Get = " + "\"" + luaName + Naming.LuaMember(v.Name) + "\"");
            w.Line($"public static double {memberName} {{ get; }}");
        }

        foreach (var s in (def.Subkeys ?? new()).OrderBy(s => s.Name, StringComparer.Ordinal))
            EmitDefine(w, s, luaName, csPath + "." + csName);
        w.Close();
    }

    private string EmitEventHelpers()
    {
        var w = new CodeWriter();
        w.Line(FileHeader($"Factorio {_version} typed event registration helpers.").TrimEnd());
        w.Line("#nullable disable");
        w.Line("using System;");
        w.Line();
        w.Open($"namespace {RuntimeNamespace}");
        w.Line("/// <summary>");
        w.Line("/// Typed shortcuts for <c>Script.OnEvent</c>: <c>Events.OnTick(e =&gt; ...)</c> expands to");
        w.Line("/// <c>script.on_event(defines.events.on_tick, handler)</c>.");
        w.Line("/// </summary>");
        w.Line(Ignore);
        w.Open("public static class Events");
        foreach (var (def, className) in _events)
        {
            if (!_eventHelpers.ContainsKey(def.Name)) continue;
            var methodName = Naming.Pascal(def.Name);
            if (!string.IsNullOrEmpty(def.Description))
                w.Line("/// <summary>" + Docs.ToSummary(def.Description) + "</summary>");
            else
                w.Line("/// <summary>Registers a handler for the native <c>" + Docs.Escape(def.Name) + "</c> event.</summary>");
            var filter = def.Filter;
            if (!string.IsNullOrEmpty(filter))
                w.Line("/// <remarks>The event supports filters; use <c>Script.OnEvent</c> directly to pass them.</remarks>");
            w.Line("/// <param name=\"handler\">Called with the event data while the mod is running.</param>");
            w.Line("/// @CSharpLua.Template = \"script.on_event(defines.events." + def.Name + ", {0})\"");
            w.Line($"public static void {methodName}(Action<{className}> handler) {{ throw new NotImplementedException(); }}");
        }
        w.Close();
        w.Close();
        return w.ToString();
    }
}
