# Analyzers

C# is not the language Factorio runs, and the compiler in `vendor/CSharp.lua` is not a general one.
Some C# compiles perfectly and then produces Lua that either crashes the compiler, needs a runtime
library a mod cannot ship, or quietly does something different inside Factorio's Lua 5.2.

These fourteen rules catch that before the compiler runs. They live in
`tools/Factorio.Analyzers` as an ordinary Roslyn analyzer assembly, so they work in three places:

* inside `factorio-mod build`, which stops on error-severity findings;
* in `factorio-mod analyze`, which reports them without building anything;
* in an editor, as red and yellow squiggles, when the DLL is added to a project.

```bash
make analyze                      # every mod under mods/
make analyze MOD=GasExtractor     # one mod
factorio-mod analyze src/Factorio.Bindings   # any directory of C#
```

```
src/Extractors.cs:42: FX0006: 'int' makes CSharp.lua emit Lua 5.3 integer operators; use double (Factorio runs Lua 5.2)
```

## Severity

**Error** means the construct cannot work in a Factorio mod: the build stops. **Warning** means it
works in most cases but has a known failure mode, and the build prints it and carries on.

`factorio-mod build --no-analyze` skips the step. There is no good reason to use it on a mod you
intend to ship; it exists for bisecting a failure.

## Editor setup

The analyzer targets `netstandard2.0` and depends only on `Microsoft.CodeAnalysis`, which is what
editors load. To get the squiggles in your own mod project, add the built assembly:

```xml
<ItemGroup>
  <Analyzer Include="$(RepoRoot)/tools/Factorio.Analyzers/bin/Release/netstandard2.0/Factorio.Analyzers.dll" />
</ItemGroup>
```

Message text and the help link on each diagnostic point back at the section below.

---

<a id="fx0001"></a>
## FX0001: no `using static` of a Factorio type (error)

```csharp
using static Factorio.Globals;   // crashes the pinned compiler
```

A static member reached through a static import is rendered with an implicit receiver, which throws
a `NullReferenceException` inside CSharp.lua itself. Qualify it:

```csharp
Globals.Game.Print("hello");
```

<a id="fx0002"></a>
## FX0002: no C# arrays (error)

```csharp
var values = new double[4];       // needs CSharp.lua's CoreSystem at run time
```

CSharp.lua implements arrays on top of a runtime library (`System.Array`, `System.each`, …). A mod
ships no runtime, so an array is a call into nothing. Use `Lists` for a growable sequence, `Native`
for a table you index by hand, or a fixed `LuaArray<T>` when the bindings offer one.

## <a id="fx0003"></a>FX0003: no `foreach` (error)

```csharp
foreach (var item in items) { }   // compiles to System.each, which a mod cannot ship
```

A `foreach` over a `LuaArray`/`LuaTable` becomes a call to `System.each`. Iterate explicitly:

```csharp
Lists.ForEachArray(items, item => Game.Print(item));
for (double i = 1; i <= Lists.Count(list); i = i + 1) { ... }
```

## <a id="fx0004"></a>FX0004: no tuples (error)

```csharp
var (min, max) = Range();         // ValueTuple is a CoreSystem type
```

Tuples, tuple types and deconstruction all need `System.ValueTuple` and its helpers. Return several
values with `out` parameters, or return a small class whose fields the bindings map to a Lua table.

## <a id="fx0005"></a>FX0005: explicit casts to non-numeric types (warning)

```csharp
var widget = (Widget)value;       // emits System.cast
```

CSharp.lua renders a cast between reference types as a `System.cast` call that performs a runtime
type check - which Lua cannot do. Casts to numeric types, `string`, `bool` and `object` are plain
expressions and are allowed. If you need "treat this as that", the value already is that thing at
run time: use `as` (an identity in the emitted Lua) and test the field you actually care about.

## <a id="fx0006"></a>FX0006: no integer types (error)

```csharp
int count = 0;                    // emits // and % operators, which Lua 5.2 does not have
```

Casts to `int` are reported here too: FX0005 lets the cast through (it is a plain Lua
expression), but the keyword itself is still banned.

Lua 5.2 has one number type: a double. CSharp.lua honours C# integer semantics for `int`, `long`,
`short`, `byte`, `uint`, `char` and friends by emitting floor-division and modulo helpers that call
into CoreSystem. Two visible symptoms: `1 / 2` becomes `0`, and the emitted Lua fails the syntax
check. Use `double` everywhere, and `Numbers.Floor`/`Numbers.Round` where you need an integer value.

`float` and `decimal` are rejected for the same reason: they are not `double`, and their operators
come from the runtime library.

## <a id="fx0007"></a>FX0007: no `string.Format`, `Convert.*` or formatted interpolation (error)

```csharp
var text = string.Format("{0} of {1}", count, total);
var same = $"{count,5}";
```

`string.Format` and `Convert.ToXxx` are CoreSystem methods. Format bits inside an interpolated string
(`{x,5}`, `{x:N2}`) lower to `string.Format` too. Plain interpolation with no format specifier is
fine - it becomes `..` concatenation - and so does `"" + value`.

```csharp
var text = count + " of " + total;
```

## <a id="fx0008"></a>FX0008: banned namespaces (error)

`System.IO`, `System.Linq`, `System.Threading`, `System.Collections.Generic`, `System.Text`,
`System.Reflection`, `System.Diagnostics`, `System.Security.Cryptography`, `System.Globalization`,
`System.Xml` and their sub-namespaces.

None of them exist in a mod: there is no file system, no console, no threads, no XML parser and no
LINQ. `using` one of them does not fail until the first call, which is why the rule fires on the
directive. `System` itself is fine and is where `double`, `string` and `Math` live.

## <a id="fx0009"></a>FX0009: no throw, try, lock, using, async, yield, delegate, event or default(T) (error)

Every one of these needs machinery the mod cannot ship:

| Construct | What it needs at run time | Use instead |
| --- | --- | --- |
| `throw`, `try`/`catch`/`finally` | `System.throw` and an exception runtime - there is no `pcall` behind it | validate and return early; log with `Globals.Game.Print` |
| `lock` | CoreSystem's monitor helpers | there is one thread; nothing needs locking |
| `using` statement | `System.usingX`, `IDisposable` | call the cleanup method yourself |
| `await` | a state machine plus the task scheduler | event handlers are already synchronous |
| `yield return` | a state machine plus the iterator runtime | fill a `Lists` list and return it |
| `delegate` type | `System.Delegate` and its multicast helpers | a list of handlers called in a loop |
| `event` | the same delegate helpers | an `Action` field called from one place |
| `default(T)` | `System.default` | `null`, `0`, or the value you mean |

`try`/`catch` is the one people miss most: `pcall` exists in Lua, but C# `try` does not compile to it.
The build's emitted-Lua check reports the same constructs, so this rule mostly moves the error to the
line you wrote.

## <a id="fx0010"></a>FX0010: an instance template must not start with `{0}` (warning)

```csharp
/// @CSharpLua.Template = "{0}.size"     // inside an instance method: wrong receiver
```

A template substitutes its arguments as values, not as receivers: `{0}` is the *first parameter*.
An instance member's receiver is `{this}`. Starting with `{0}` means the call hangs off the first
argument, which is the shape that produced `nil` receivers while the bindings were being written.

```csharp
/// @CSharpLua.Template = "{this}.size"
```

Static members are exempt: their first parameter really is the subject.

## <a id="fx0011"></a>FX0011: templates must not contain a table literal with a quoted key (warning)

```csharp
/// @CSharpLua.Template = "util.make({ t, {"kind"} })"
```

The template parser substitutes `{n}` placeholders and treats every other brace as Lua table syntax,
but it also strips quotes when it reads the attribute. A quoted key inside a table literal comes out
duplicated or over-escaped - observed as `kind = "kind"` - and the argument is dropped. Build the
table in `FactorioUtils.lua` (`util.table`) and keep the template a plain call.

## <a id="fx0012"></a>FX0012: `Storage.Data` cannot be a receiver (warning)

```csharp
Storage.Data.Get("key");          // emits an empty receiver
```

`Storage.Data` is a static property whose template is a bare substitution; the compiler only expands
it when it stands alone as a value. As the object of a call or an index it becomes an empty receiver.
Use the typed API, which exists for exactly this:

```csharp
var machines = Storage.Table<Entity>("machines");
var count = Storage.Slot<double>("count");
count.Set("total", 1);
```

<a id="fx0014"></a>
## FX0014: no CoreSystem-only .NET types (error)

```csharp
Console.WriteLine("hello");       // compiles against CoreSystem.Lua, which a mod cannot ship
var text = new StringBuilder();
```

CSharp.lua ships a Lua re-implementation of part of the BCL in `CoreSystem.Lua`: `Console`, `File`,
`Directory`, `Path`, `Guid`, `StringBuilder`, `DateTime`, `Task`, `Thread`, `Regex`, `Random`,
`Stopwatch`, `Environment` and friends. The compiler is happy to emit calls into it, and the mod
then indexes a table that was never loaded. The rule fires on an undeclared name, so a mod that
defines its own `Console` class is left alone.

Factorio has its own answer for most of these: output is `Globals.Game.Print`, time is
`Globals.Game.Tick` and `game.tick_paused`, randomness is `math.random`, and there is no file
system, no console and no threads.

## <a id="fx0013"></a>FX0013: no `is` or `typeof` (error)

```csharp
if (value is string) { }          // emits System.is
```

Lua has no runtime type information, so CSharp.lua lowers `is` and `typeof` to CoreSystem calls that
could only ever answer wrongly. Two exceptions are fine:

* `x is null` - compiles to `x == nil`, no runtime involved;
* `as` - an identity in the emitted Lua, for when the value already is that type.

Test a real property instead: `entity.Valid`, `entity.Name`, the presence of a field.

---

## How a rule is written

One `DiagnosticDescriptor` per rule in `DiagnosticCatalog.cs`, one analyzer class per family:

| File | Rules | Covers |
| --- | --- | --- |
| `LanguageRules.cs` | FX0001-FX0009, FX0013, FX0014 | C# constructs the compiler or the runtime cannot lower |
| `TemplateRules.cs` | FX0010-FX0011 | `@CSharpLua.Template` shapes that silently misbehave |
| `FactorioRules.cs` | FX0012 | the bindings' own API used in a way that cannot work |

The rules are syntactic: they read the syntax tree, not symbols, so they run on a mod that does not
compile yet - which is when you want them. `Emittability.cs` keeps them quiet about code the compiler
never sees: the body of a `@CSharpLua.Template` member (usually `=> throw new NotImplementedException();`)
and everything inside an `@CSharpLua.Ignore` type, which is how the 1,500 generated binding files are
declared.

Adding a rule means: a descriptor, an analyzer, a row in `AnalyzerReleases.Unshipped.md`, a positive
and a negative case in `tests/Analyzer.Tests/RuleCases.cs` (a test fails if a rule has no fixture),
and a section here with an `<a id="fx000n"></a>` anchor.

## Tests

`tests/Analyzer.Tests` runs every rule both ways round - the construct is caught, and the construct
that merely looks like it is not - and asserts that:

* every descriptor in the catalog has a fixture, and every fixture names a real rule;
* the built assembly loads by reflection and publishes exactly the catalog's ids;
* `mods/GasExtractor`, `mods/HelloFactorio` and the whole of `src/Factorio.Bindings`
  (3,043 hand-written and generated files: both API versions) produce **no** findings;
* `analyze` exits non-zero on a bad mod, and `build` stops before the compiler with the rule id,
  file and line in the message.
