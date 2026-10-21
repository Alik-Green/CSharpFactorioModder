# Limitations and unsupported constructs

This document is the honest counterpart to `docs/COMPATIBILITY.md`: what the toolchain does **not**
do, what it does approximately, and what is unverified. The machine-readable version of most of it
is generated with every run into `src/Factorio.Bindings/Generated/api-report.{json,md}`
(0 errors, 0 skipped, 689 degraded, 3,045 info for Factorio 2.0.72).

## 1. Not verified: running inside Factorio

No Factorio binary exists in this environment, so the game-side integration run was **not
executed**. Everything else below is verified only against (a) the official JSON, (b) the pinned
compiler, (c) stock Lua 5.2 and (d) a hand-written Factorio mock. See `docs/INTEGRATION.md`.

Specifically unverified in game:

* whether the emitted chunk loads under Factorio's modified Lua 5.2 (stock `luac5.2 -p` accepts it,
  but Factorio has its own restrictions),
* that every `defines.*` key used exists in the installed build,
* that the handler arity of every `script.on_event` matches the binary (the mock exercises three),
* that `storage` behaves as the game documents (it is bound now, §3, but never run in game),
* that `require` in the data stage resolves the same way under Factorio as the build's own data
  stage check assumes.

## 2. Type-system approximations (runtime stage)

All of these are reported as `degraded` in the API report.

| Construct | What is generated | Why |
|---|---|---|
| unions with incompatible kinds (`LocalisedString`, `ForceID`, `LuaEventType`, …) | `object` | C# has no union type; a wrapper would allocate and break the "the C# value *is* the Lua value" model. |
| `x as T` | the expression, unchanged | Lua tables carry no type tags; a cast cannot be checked. A wrong cast fails later at the point of use. |
| `(LuaEntity)x` | unusable | CSharp.lua would need `System.cast` (CoreSystem). |
| tuples (`{A, B}`) | `LuaArray<object>` | a bare tuple is a Lua array; element types are lost. |
| anonymous tables | `LuaTable<string, object>` | no schema in the JSON. |
| attribute with different `read_type` / `write_type` | `object` (17 cases) | one C# member cannot have two types. |
| optional value-type parameter | `double?` / `bool?` | `false`/`0` must stay distinguishable from "omitted". |
| optional parameter followed by a required one | the optional one becomes required | C# cannot express it, and reordering would corrupt the positional Lua call. |
| duplicate keys inside merged `variant_parameter_groups` | first occurrence kept | the groups *extend* one table; the key exists once. Reported (2,214 info entries). |
| `subclasses` / `visibility` restrictions | documented in `<remarks>`, not enforced | C# cannot restrict a member to a subclass without wrappers. |
| Lua operators (`__call`, `__index`, `__add`, …) | not bound | use the explicit methods. |

None of these lose access to the value: `object` *is* the native Lua value, so it can be passed
straight back into another API call.

## 3. Runtime stage caveats

* **No runtime type checking** — see the casts above.
* **Arrays are not `IEnumerable`.** No LINQ and no `foreach` over `LuaArray<T>` (that emits
  `System.each`). Use `Length()` and `Get(i)`.
* **No indexers on `LuaTable`/`LuaCustomTable`.** CSharp.lua emits `:get()`/`:set()` calls for C#
  indexers; use `Get(key)` / `Set(key, value)` / `Count()` (which maps to `table_size`).
* **No `new` for API objects.** Every API type is a view of a game-owned table; constructing one
  produces a table that is not a game object. Only generated *parameter* classes are meant to be
  constructed, and they are compile-time templates.
* **`storage` is hand-bound, not generated.** Factorio's per-save persistent table has no entry in
  the JSON documents, so `Factorio.Storage`, `EntityRegistry`, `EntityValue<T>`, `Setting<T>` and
  `StorageSlot<T>` are written by hand (and tested by `tests/lua/util_test.lua`). They cover
  sections, typed get/set/remove, membership and counting; they do not serialise anything, so a
  stored value still has to be plain data.
* **No event fires when a player changes a machine's recipe.** `player.opened` / `get_recipe()` are
  the only way to notice, so a mod that treats the recipe as a setting has to sample it (the gas
  extractor does it in its per-second pass). Writing a recipe while a player has the window open
  closes it, which is why `Crafting.IsOpen(machine)` exists: repair an empty slot, adopt a chosen
  recipe, and never write under an open window.
* **The constructs below are rejected before the compiler runs.** `factorio-mod build` runs
  fourteen Roslyn analyzers over the mod's own sources (`docs/ANALYZERS.md`): C# arrays, `foreach`,
  tuples, integer types, `string.Format`, casts, `is`/`typeof`, thrown exceptions and the rest of
  §3 fail the build with the rule id, file and line. The analysers are syntactic, so they also run
  in an editor. They are a compile-time gate, not a transformation: what they reject, the toolchain
  still cannot do.
* **`using static Factorio.Runtime.Globals;` crashes the pinned compiler** (a
  `NullReferenceException` in `LuaRenderer` when a static property is used as a receiver through a
  static import). Always write `Globals.Game`, `Globals.Script`, and so on. `factorio-mod build`
  recognises that stack trace and says so.
* **`null` as a direct argument to a call whose template inlines it** can crash the same way; store
  it in a typed local first.
* **`script.on_event` filters** are typed `LuaArray<object>` (the docs describe a union of tables),
  so filter tables are untyped.

## 4. Assets

* The **builtin renderer is a software rasteriser**: orthographic, flat-shaded, no textures, no
  UVs, no transparency sorting, no animation frames. It exists so a sprite can be regenerated from
  a model with nothing installed.
* The **Blender path is wired but untested here** (no Blender in this environment): it writes a
  script and shells out, and the script has not been executed against a real Blender build.
* Neither path imports `.blend`, `.gltf` or `.fbx` files — only Wavefront `.obj` (positions and
  triangles; normals and UVs are read past). Convert with any 3D tool first.
* Sprite output is PNG only, one file per view, square, 8-bit RGBA. Multi-layer sprites, shadows,
  `shift`, `frame_count` and animation ranges are expressed in `data.lua`, not by the renderer.

## 5. Prototype (data) stage

* Two ways to declare prototypes, both supported: a hand-written `lua/data.lua` (what `GasExtractor`
  does, checked key by key), or C# definitions under `data/` (what `DemoChemistry` does) - see
  [`DATA.md`](DATA.md).
* The C# way is **build-time code, not Lua**: `factorio-mod build` compiles `dataSources`, runs the
  mod's `ModDefinition` and writes the result as `data.lua`. The definitions therefore use the full
  .NET language, and none of that code ships.
* Generated for the *runtime* stage only: `Factorio.Prototypes.Globals.Data` → `data`, `Data.Raw`
  (typed as `LuaCustomTable<string, LuaCustomTable<string, object>>`), one C# class per prototype
  type with all documented properties, and `Log()`.
* The `Factorio.Data` API models the prototype types a mod usually adds - fluid, item, entity,
  assembling machine, recipe, recipe category, item group and item subgroup - plus the value types
  those take. Every other prototype type or property is written with `Field("key", value)`, which is
  validated by the same key-by-key check; there is no typed class per prototype type.
* The enums cover the sets the game fixes (`flags`) and the *base game's* names among the ones it
  does not (`Subgroup.ProductionMachine`). They are a spelling aid, not a whitelist: every one of
  them converts to and from a string, because a mod may declare its own tab, category or order.
  A base-game name the enum does not list yet is still written `Subgroup.Of("name")`.
* A prototype built on a vanilla one (`base(name, Vanilla.X)`) is exempt from the "required
  property is missing" check, because those fields are copied from `data.raw` in the game, where the
  build cannot see them. Its other fields are still checked, and `tests/lua/data_inherit_test.lua`
  checks the copy itself against a stub `data.raw`.
* The runtime bindings never expose `data`; the prototype bindings never expose `game`/`script`.
* The build **checks** the data stage: it runs the packaged `data.lua` with a stub `data` table and
  compares every registered prototype with `prototype-api.json` — unknown type, unknown key,
  missing required key, with a "did you mean" hint, at the top level **and** inside nested
  structures (`fluid_boxes[1].pipe_connections[2].flow_direction`).
* It also checks the geometry Factorio checks on load: a pipe connection outside the entity's
  `collision_box` (the game shrinks the box by 0.001 first, so the 1.2 edge of a 3x3 machine is
  already outside), two connections of one fluid box on the same position, a connection without a
  direction, a fluid box with a volume of zero or less, and an inside-out
  `collision_box`/`selection_box`. This is what turns
  "Failed to load mods: position must be inside of entity bounding box" into a build error.
* It warns about prototypes the game accepts but a player can never obtain: an item with a
  `place_result` that no recipe produces, and a recipe that is disabled with no technology in the
  mod unlocking it. These are warnings, because another mod or a script may provide them.
* It checks the names a prototype looks up in `data.raw`: every recipe `category`/`categories` and
  every `crafting_categories` entry has to be a `recipe-category` the mod declares, or one of the
  base game's nine. A mod that depends on another mod is exempt for names it does not declare,
  because those may come from the dependency, which is not visible here. The check is what turns
  "Error in assignID: recipe-category with name 'gas-extraction' does not exist" into a build error.
* It checks the same way for `subgroup` and `group`: the base game's 11 groups and 113 subgroups are
  listed in `Build/VanillaGroups.cs` (generated from `factorio-data`), and anything else has to be an
  `item-subgroup`/`item-group` the mod declares. Unknown names are errors; the foreign-dependency
  exemption applies as above.
* It warns about an **item with no subgroup**, which the game accepts and files under its catch-all
  "Other" tab, and about a **visible prototype with no name** in the locale the build can see
  (`locale/en/locale.cfg` plus the strings the definitions declare), which the game accepts and
  shows as `Unknown key: item-name.foo`. Both are warnings because both load; both are also the
  difference between a mod that works and a mod that looks broken.
* What the geometry check cannot see: entities that declare **no** `collision_box` (the check is
  skipped rather than guessing at the engine default), collision against *other* entities or
  tiles, and anything the data stage never registered — only tables passed to `data:extend` are
  visible, so a prototype built with `data.raw` edits or `data:extend` from another mod is not
  checked.
* Item and entity references (`minable.result`, `place_result`, recipe `ingredients`/`results`) are
  **not** checked: a name the mod does not declare may be any of the base game's thousand
  prototypes, and the tool has no list of those to compare against.
* `require` works in the data stage (Factorio provides it) and is how generated sprite tables are
  pulled in. It is rejected in `control.lua`, where the game has no module loader.
* Localisation is generated into **`en` only**. `LocalisedName`/`LocalisedDescription` become
  `item-name`, `fluid-name`, `recipe-name`, `entity-name`, … entries in the packaged
  `locale/en/locale.cfg`; other languages are whatever the author wrote by hand, and nothing checks
  that they are complete. A name is also not checked against the *game* - the build knows the keys
  it and the author declare, not the base game's locale.
* The name check reads the locale files as text: a key that is present but empty, or that has a
  `__1__`/`__2__` plural form where the game wants a single string, counts as present.
* Every resource path a prototype contains is checked for the `__source__/` prefix Factorio
  requires (an unprefixed `graphics/x.png` is a build error, `Build/DataStageResources.cs`), and the
  packaged Lua is cross-checked against the files that were packaged: a path prefixed with this
  mod's own name that no file matches is a warning. Paths under `__base__/` or another mod's name
  cannot be checked, because that mod's files are not here.
* Textures are the mod author's problem. The build copies `resources/` and checks that a path is
  prefixed and resolves, but "a sensible picture" is not something it can check -
  `mods/DemoChemistry/CREDITS.md` and the note in `docs/DATA.md` point at the free-graphics
  catalogue the example uses.

## 6. Toolchain caveats

* **CSharp.lua is pinned** to commit `790d3cf` (`config/toolchain.json`) and patched only to build
  with .NET 8 (`tools/patches/cscharplua-net8.patch`). Compiler behaviour is upstream. Upgrading the
  commit means re-running `make test`: the transpile tests pin the exact Lua semantics this project
  depends on (dot calls, flat `params`, `out` parameters, optional-parameter overloads).
* **`CoreSystem.lua` is not shipped.** `src/Factorio.Bindings/FactorioCompat.lua` implements
  `System.namespace/import/init/get/toString` and nothing else; `Build/LuaCheck.cs` fails the build
  if the emitted Lua calls anything outside that surface.
* **Single-file packaging.** All modules are concatenated into `control.lua`. The generated bindings
  emit almost no Lua (every type is ignored and its module is dropped), so the shim and the mod code
  are all that remain — currently 8.5 KB. A very large mod could eventually hit per-function local
  limits.
* **api_version 6 only.** Both supported Factorio releases publish api_version 6 documents, and
  any other is rejected with an explicit error instead of producing subtly wrong bindings.
* **Two versions, two trees.** `api/2.0.72` + `Generated/2.0.72` and `api/2.1.22` +
  `Generated/2.1.22` both ship (22 MB, 2,935 files in `src/`). A version added to
  `config/toolchain.json` is fetched and generated with `factorio-mod setup --all`; until then a mod
  declaring it fails with the exact command to run.
* **The version is chosen per mod, not per build.** A build compiles one mod for the version its
  `mod.json` declares. Cross-version outputs are never mixed; bindings from one version are never
  visible to the other.
* **Documentation gate.** Generation fails if a public member would be emitted without a
  `<summary>`; the fallback text is generated from the member's shape, so the docs are never empty,
  but a fallback is less useful than Factorio's own text.

## 7. Test coverage gaps

* Generator tests cover globals, inheritance, properties, methods, optional/variadic parameters,
  multi-returns, unions, structured tables, events, defines, naming, documentation completeness, the
  report, determinism, the prototype stage and `api_version` rejection.
* Transpile tests assert on emitted Lua for every construct the bindings use, plus a golden file for
  the example mod. They do not fuzz the whole API (all 963 (2.0) / 1,044 (2.1) methods share the
  same handful of templates).
* The Lua tests run the packaged `control.lua` against a **mock**, not the game. Unmodelled API calls
  fail loudly rather than pass silently, but the mock is not Factorio.
* The data-stage rules are covered by `tests/Generator.Tests/DataStageTests.cs`: the exact pipe
  connection failure, its fix, the 0.001 edge, duplicate positions, a zero-volume fluid box, both
  spellings of a bounding box, an inside-out box, and misspelt keys at the top level and nested.
* `tests/Analyzer.Tests` covers every rule twice (a positive and a negative fixture per rule),
  asserts that no rule is added without a fixture, and that the shipped mods and all 3,043 files
  of `src/Factorio.Bindings` (both API versions) are clean.
* `tests/lua/util_test.lua` covers the util library directly (it has no Factorio dependency).
  `tests/lua/gas_extractor_test.lua` drives the bigger mod through build → craft → switch gas →
  mine → status → stale registry.
* The asset pipeline is tested at the unit level (PNG round-trip, OBJ triangulation, a rendered
  cube being opaque in the middle and transparent in the corners) but the *visual* quality of a
  sprite is only checked by eye.
* No performance, save/load (`on_load`) or multiplayer behaviour is tested.
