# C# ↔ Lua mapping

Every rule below is exercised by `tests/Transpile.Tests` (which asserts on the emitted Lua) or by
`tests/Generator.Tests` (which asserts on the emitted C#). The C# constructs that do **not** map -
arrays, `foreach`, tuples, integer types, `string.Format`, casts, `is`/`typeof`, exceptions - are
rejected before compilation by the analyzers; `docs/ANALYZERS.md` is the list.

## Types

| API JSON | C# | emitted Lua |
| --- | --- | --- |
| `LuaPlayer`, `LuaEntity`, … | `Factorio.Runtime.Player`, `Entity`, … (compile-time only) | the native table, unchanged |
| `boolean` | `bool` | `true` / `false` |
| `float`, `double`, `int8`…`uint64` | `double` | a Lua number |
| `string` | `string` | a Lua string |
| `LuaArray<T>` | `LuaArray<T>` | the table itself |
| `dictionary[string, T]` | `LuaTable<string, T>` | the table itself |
| `LuaCustomTable<K, V>` | `LuaCustomTable<K, V>` | the table itself |
| anonymous / polymorphic table | `LuaTable<string, object>` or `object` | the table |
| tuple of N values | `LuaArray<object>` (reported as degraded) | the table |
| union of several kinds | `object` (reported as degraded) | whichever value is there |
| optional `T` | `T?` for value types, plain reference otherwise | the key may be absent |

Native class names lose the `Lua` prefix: `LuaPlayer` is `Player`, `LuaEntity` is `Entity`,
`LuaGameScript` is `Game` and `LuaBootstrap` is `Script` (they are the `game` and `script`
globals). A rename is skipped when it would collide with another emitted type — `LuaTile` stays
`LuaTile` because the table class `Tile` already exists — and `api/…/renames.json` records every
rename the generator made. The Lua side never changes: only the C# spelling does.

Numbers are always `double` on purpose: CSharp.lua emits Lua 5.3 integer syntax (`//`, `&`) when
it sees integer types, and Factorio runs Lua 5.2.

## Members

| Factorio | C# | Lua |
| --- | --- | --- |
| attribute `player` (read) | `Player Player { get; }` | `{this}.player` |
| attribute `speed` (read+write) | `double Speed { get; set; }` | `{this}.speed` / `{this}.speed = {0}` |
| method `get_player(1)` | `Player GetPlayer(object player)` | `{this}.get_player({0})` |
| global function `log(x)` | `Globals.Log(x)` | `log({0})` |
| global object `game` | `Globals.Game` | `game` |
| `defines.events.on_tick` | `Defines.Events.OnTick` | `defines.events.on_tick` |
| table field `position.x` | `MapPosition.X` | `{this}.x` |
| `on_event(defines.events.on_tick, h)` | `Events.OnTick(h)` | `script.on_event(defines.events.on_tick, {0})` |

Names are converted, the native name stays visible in the template and in the docs:
`character.health` is `Character.Health`, and `LuaEntity::get_inventory` is `GetInventory` on
`Entity`.

## Method shapes

Optional parameters become **overloads**, not default values, because a `= null` in the signature
would still have to be rendered into the Lua call:

```csharp
void Print(object message)                       // game.print(msg)
void Print(object message, PrintSettings printSettings)  // game.print(msg, settings)
```

* **variadic** → `params object[]` with `{*i}` expansion: `Remote.Call("mod", "fn", 1, 2)`
  → `remote.call("mod", "fn", 1, 2)` (flat, not a table).
* **multiple returns** → trailing `out` parameters: `inventory.FindEmptyStack(null, out var stack,
  out var count)` → `local stack, count = inventory.find_empty_stack(nil)`.
  Tuples would need CoreSystem (`:Deconstruct()`), `out` parameters do not.
* **takes_table** → one generated `<Class><Method>Parameters` class whose constructor is a table
  literal template, so `new LuaSurfaceCreateEntityParameters(name: "iron-chest", position: p)`
  emits `{name = "iron-chest", position = {x = 1.5, y = 2}}` — never a C# object.
* **C# keywords** are escaped with `@`: `@event`, `@interface`, `@protected`.

## Naming

* types, properties, methods, enum-ish members: `PascalCase`
* parameters and locals: `camelCase`
* a member that would collide with its enclosing type gets a `Value` suffix
  (`defines.prototypes.achievement.achievement` → `Achievement.Value`)
* duplicate names after conversion are suffixed `2`, `3`, … and reported
* Lua keys that are not identifiers are emitted as `["a-b"]`

## Documentation

Every public member has a `<summary>`:

1. Factorio's own description, with markdown flattened and escaped (`Docs.ToSummary`);
2. otherwise a written fallback naming the native member and its owner (`Docs.Fallback`), e.g.
   "Gets or sets the native Lua attribute `health` of `LuaEntity`";
3. types also get a `<remarks>` with a deep link to `lua-api.factorio.com` and a note when the
   binding is not exact (union collapsed, tuple mapped to an array, read/write types differ).

`Api/DocCheck.cs` re-reads every generated file and fails the generation run if a `public`
declaration is not preceded by a doc comment. The check is itself tested.

## Degradations

`api-report.md` lists every member whose binding is not exact. Each version has its own report
(`Generated/<version>/api-report.md`); current totals:

| version | error | skipped | degraded | info |
| --- | --- | --- | --- | --- |
| 2.0.72 | 0 | 0 | 689 | 3,045 |
| 2.1.22 | 0 | 0 | 734 | 4,434 |

A degradation is a binding that exists but is approximate. In 2.0.72 they are: 506 unions → `object`,
148 tuples → `LuaArray<object>`, 24 anonymous tables → `LuaTable<string, object>`, and 11 members
whose documented read and write types differ (the read type wins). The 2.1.22 counts differ because
2.1 added prototypes and attributes, not because the generator behaves differently.

Nothing in the "degraded" list loses access to the underlying value: `object` is the native Lua
value, so it can be passed straight back to another API call or indexed with `LuaTable` helpers
when its shape is known.

## Utilities (`Factorio.Storage`, `Factorio.Lists`, …)

Some things are not members of an API type, so the generator cannot emit them: iterating a Lua
table, creating a section of `storage`, checking that an object is still valid. Those live in
hand-written bindings under `src/Factorio.Bindings/Utilities/`, each a template over a small
function in `FactorioUtils.lua`, which is concatenated into every mod's `control.lua`.

| C# | emitted Lua |
| --- | --- |
| `Storage.Registry("k")` | `"k"` (the section name) |
| `EntityRegistry.Add/Remove/Contains/Count/ForEach/Clear` | `util_registry_add/…` |
| `EntityRegistry.Value<T>("gas")` | `util_registry_value("k", "gas")` → section `"k/gas"` |
| `EntityValue<T>.Get/Set/Has/Remove` | `util_value_get/…` |
| `Storage.Setting<T>("k")` | `"k"` (the key) |
| `Setting<T>.Get/Set/Has/Clear` | `util_setting_get/…` |
| `Storage.Table("k")` | `util_ensure_table(storage, "k")` |
| `Storage.Slot<T>("k").Set(key, v)` | `util_slot_set(storage, "k", key, v)` |
| `StorageSlot<T>.Get/Has/Remove/Count/Clear` | `util_slot_get/…` |
| `Lifecycle.OnBuilt/OnRemoved(proto, h)` | `util_on_built/on_removed` (every build and removal event) |
| `Lifecycle.OnInit/EverySecond(h)` | `util_on_init` / `script.on_nth_tick(60, …)` |
| `Commands.Add(name, key, h)` | `util_command(name, key, h)` |
| `ChatCommand.Player/Parameter/Reply` | `util_command_player/parameter/reply` |
| `Crafting.RecipeName/SetRecipe/ProductsFinished/IsOpen` | `util_machine_recipe/set_recipe`, `e.products_finished`, `util_machine_is_open` |
| `Entities.IsValid(e)` | `util_valid(e)` |
| `Entities.Is(e, "name")` | `util_is(e, "name")` |
| `Lists.ForEach(t, (k, v) => …)` | `util_each(t, function(k, v) … end)` |
| `Lists.ForEachSorted/ForEachArray/Count/Copy/Join` | `util_each_sorted/each_array/count/copy/join` |
| `Ticks.FromSeconds(2)` | `math.floor(2 * 60)` |
| `Numbers.Clamp/OrZero/PositiveModulo/…` | `math.max(…)`, `(x or 0)`, `((x % m) + m) % m` |
| `Loc.Key("a.b")` / `Loc.With("a.b", x)` | `{ "a.b" }` / `{ "a.b", x }` |
| `Native.Get(e, "unit_number")` | `util_get(e, "unit_number")` |

`StorageSlot<T>`, `EntityRegistry`, `EntityValue<T>` and `Setting<T>` are *names*, not objects:
each is emitted as its own string, so they can be `static readonly` fields and cost nothing at run
time. A registry stores entities by `unit_number` and drops the entries (and the values it owns) of
entities that stopped existing the next time it is iterated.

### Why some helpers look like wrappers

CSharp.lua's template parser is the weak point of the toolchain. Three shapes crash it or emit
wrong Lua, and both were hit while building the example mods:

* a Lua table literal inside a template (`{"entity-name.x", {0}}` duplicated the key);
* quotes inside a template (the emitted string came out over-escaped, `{\"…\"}`);
* a receiver passed as the *first* template argument (`"{0}[{1}] = nil"` indexed the wrong value).

So the rule is: **when a binding would need Lua syntax, write the Lua in `FactorioUtils.lua` and
make the C# member a plain call with string arguments.** `Native` exists for exactly that, and is
also the supported escape hatch for names only known at run time.
