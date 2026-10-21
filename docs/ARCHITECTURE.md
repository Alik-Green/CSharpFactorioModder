# Architecture

```
            api/2.0.72/*.json                official documents, one folder per version,
            api/2.1.22/*.json                sha256-pinned in api/api.lock.json
                     │
                     ▼
  tools/Factorio.ModTool/Api
      Schema.cs            JSON model (classes, attributes, methods, events, defines)
      TypeMapper.cs        JSON type -> C# type, and the Lua expression a value becomes
      RuntimeEmitter.cs    runtime stage: objects, tables, events, globals, defines
      PrototypeEmitter.cs  data stage: data.raw, one class per prototype
      Docs.cs              markdown -> XML docs, plus written fallbacks
      Naming.cs            PascalCase / camelCase / keyword escaping / Lua key syntax
      TypeRenamer.cs       LuaPlayer -> Player, LuaGameScript -> Game, collision-safe
      DocCheck.cs          fails generation if a public member has no <summary>
      BindingsGenerator.cs orchestration + api-report.{json,md} + renames.json
                     │
                     ▼  1,466 generated .cs files per version
  src/Factorio.Bindings/Generated/<version>/**   one tree per API version (both checked in)
                     │
                     ▼  + src/Factorio.Bindings/Compat.cs        hand-written compile-time types
                        + src/Factorio.Bindings/Utilities/*.cs  EntityRegistry, Lifecycle, Commands, Crafting, ...
                        + src/Factorio.Bindings/FactorioUtils.lua  their Lua half (bundled)
                     │
                     ▼  tools/Factorio.Analyzers  Roslyn diagnostics FX0001-FX0014 (docs/ANALYZERS.md)
                        LanguageRules / TemplateRules / FactorioRules / Emittability
  CSharp.lua 790d3cf (pinned, patched, built from vendor/)   type-checks with Roslyn
                     │
                     ▼  one .lua module per C# type
  Assets/AssetPipeline.cs  assets/render.json -> PNGs + generated sprite tables
      ObjModel.cs / Renderer.cs / Png.cs   the builtin software renderer
                     │
                     ▼
  Data/DataEntryGenerator.cs   the mod's data/ sources -> Roslyn compile -> run ModDefinition
      + src/Factorio.Bindings/Data/**        -> data.lua (prototype tables)
                     │
                     ▼
  Build/LuaBundle.cs   shim + util library + `do ... end` blocks + entry point -> one control.lua
                     │
                     ▼
  Build/Packager.cs    + resources/, lua/, files/, info.json, resource-path check
                     │
                     ▼
  Build/LuaCheck.cs    luac5.2 -p + CoreSystem symbol scan
  Build/DataStage.cs   runs the packaged data.lua, dumps every prototype as JSON
  Build/PrototypeSchema.cs  prototype-api.json indexed: types, parents, properties, aliases
  Build/DataStageRules.cs   nested structure checks + the geometry the engine validates
  Build/DataStageReferences.cs  names a prototype looks up: categories, groups, subgroups
  Build/VanillaGroups.cs     the base game's 11 groups and 113 subgroups
  Build/LocaleCheck.cs       every visible prototype has a name in the locale
  Build/LocaleWriter.cs      declared names -> locale/en/locale.cfg, merged with the author's
  Build/DataStageResources.cs  every resource path names its source (__base__/, __<mod>__/)
                     │
                     ▼
  build/mods/<Name>_<version>/  and  <Name>_<version>.zip
```

## Why one file per type

The earlier design emitted a single 22,000-line `RuntimeApi.g.cs`. Per-type files give

* readable diffs when the API version changes (only the changed types move),
* file names that match the C# type (`Entity.cs`; `renames.json` maps it back to `LuaEntity`),
* room for per-file headers that state the source URL and the degradation notes,
* a natural unit for `DocCheck` and for the tests (`Generated.Type("LuaEntity")`).

Files are written in sorted order and the emitters are deterministic, so regenerating on the
same API JSON produces byte-identical output (verified by a test).

## Two ways to write a data stage

A mod that declares prototypes in C# (`"dataEntry"` in `mod.json`) has its `dataSources` compiled by
`Data/DataEntryGenerator.cs` with Roslyn, run in the tool, and written out as `data.lua`; a mod that
ships `lua/data.lua` keeps it. Both end up in the same place and go through the same check, so the
difference is only how the table was authored.

The C# data stage is deliberately **not** transpiled by CSharp.lua. It is a build-time program: it
may use arrays, LINQ, `System.Collections.Generic` and exceptions - everything the runtime stage
cannot - because it produces Lua strings and ships none of itself. The `Factorio.Data` types
therefore live in an ordinary .NET library that the tool references, and the analyzers skip that
folder (`RepositoryTests.BindingsAreClean`).

## Why the bindings are compile-time only

Factorio's API is a set of Lua tables. A wrapper object per player/entity would allocate,
and the identity of the native object would be lost. Instead:

* every generated type is marked `/// @CSharpLua.Ignore` — CSharp.lua emits **no Lua** for it;
* every member carries a `@CSharpLua.Get` / `@CSharpLua.Set` / `@CSharpLua.Template`
  attribute that expands to a native access **at the call site**;
* the C# `struct`/`class` distinction therefore has no runtime meaning: `Player` *is*
  the Lua table Factorio handed out.

Consequence: a property read compiles to `player.character`, not `player.get_character()`
and not a function call at all. That is checked by `tests/Transpile.Tests`.

Because no Lua class table is emitted for the 1,500+ generated types, the bundle step can drop
their modules (all of them are "namespace-only") and re-declare the two namespaces the emitted
`System.import` callbacks resolve. The shipped `control.lua` is ~200 lines of mod code on top of
the 150-line shim.

## The compatibility shim

`src/Factorio.Bindings/FactorioCompat.lua` implements only what emitted code uses:

| helper | purpose |
| --- | --- |
| `System.namespace` / `namespace.class` | lazy type registry |
| `System.import` / `System.init` | deferred initialisation, exactly like CoreSystem |
| `System.get` | entry-point lookup by dotted name |
| `System.toString` | C# `+` on non-strings (`"a" + x` → `"a" .. System.toString(x)`) |
| `System.as` | C# `as` — identity, because Lua has no type tags |
| `class.static` | runs the class's static initialiser once, on first use (static `readonly` fields) |

Everything else that CoreSystem provides — casts, generics, arrays, LINQ, tuples, enums,
`System.each` — is *forbidden*: `Build/LuaCheck.cs` scans the emitted Lua and fails the build if
one appears. That turns a class of "works on my machine, crashes in game" bugs into build errors.

## The util library

`src/Factorio.Bindings/FactorioUtils.lua` is the hand-written Lua half of `Factorio.Storage`,
`Factorio.Lists`, `Factorio.Native`, `Factorio.Lifecycle`, `Factorio.Commands`,
`Factorio.Crafting` and friends. It is bundled *inside* the same local `util` table as the shim, so
it costs no globals, and it is unit-tested directly by `tests/lua/util_test.lua`.

The aliases the emitted code calls (`util_registry_add`, `util_on_built`, …) are generated from the
file itself by `Build/LuaBundle.cs` rather than listed by hand, so a helper added to the library is
reachable from a template the moment it is written.

One rule of Factorio's API shapes the event helpers: `script.on_event`, `script.on_nth_tick` and
`script.on_init` each keep **one** handler per script, so a second subscription replaces the first
without a word. `util.on_built`/`on_removed`/`every_second`/`on_init` therefore collect their
handlers and register a single dispatcher per event, which is what lets a mod watch two machine
prototypes (`gas-extractor` and `gas-extractor-2`) and hear about both. `tests/lua/util_test.lua`
covers it.

Two rules keep it honest:

1. **the C# side is a façade, never a re-implementation** — every member is a one-line
   `@CSharpLua.Template` over a `util.*` function;
2. **anything that would need Lua syntax in a template is written here instead** — that is why
   `util.get/set/table` exist. CSharp.lua's template parser mis-handles table literals, quoted
   strings and first-argument receivers (see `docs/COMPATIBILITY.md`), and every one of those bugs
   was found by writing a real mod against the API.

## Running without make

`Makefile` is a convenience layer: every target is one line that runs
`tools/Factorio.ModTool/bin/Release/net8.0/factorio-mod.dll`. `factorio-mod.cmd` does the same on
Windows, including the `dotnet build` that `make tool` performs, so nothing in the pipeline depends
on make, `bash` or a particular shell. `docs/WINDOWS.md` is the Windows version of the quick start.

## Templates and `new`

`factorio-mod new` copies `templates/mod/` - four text files plus a generated thumbnail - and
substitutes `{{name}}`, `{{namespace}}`, `{{title}}`, `{{author}}`, `{{entry}}` and `{{date}}`.
`Build/ModTemplate.cs` fails the scaffolding (and the tests) on a placeholder it does not know, so a
typo in the template cannot reach a user. `Build/Thumbnail.cs` draws the initial with the same PNG
writer the sprite renderer uses, because a mod without `thumbnail.png` shows as a grey box.

The API is deliberately **not** copied per mod: it is 11 MB and 1,466 generated files for 2.0.72,
identical for every mod on that Factorio version. `--bindings` copies it for the case where the mod has to
build without this repository; `Build/ModBuilder.cs` then prefers `<mod>/bindings` over the
repository's copy and excludes it from the mod's own sources, so nothing is compiled twice.

## The IDE

Three things connect this pipeline to an editor, none of them clever:

* **`<Name>.csproj` in every mod** - an SDK project with no compile items whose `Build` target is
  `AfterTargets="Build"` and runs `factorio-mod build <folder>`. That is the whole IDE integration
  for building: Rider's Build button is MSBuild, so the mod is built by whatever IDE the folder is
  opened in, and by `dotnet build`, with no plugin installed.
* **`"build": { ... }` in `mod.json`** - every option that used to be a flag, so the fixed command
  above is enough. `Build/ModProject.cs` reads it; `Commands/BuildCommand.cs` merges it with the
  command line (line wins) and resolves a relative `output` against the mod, not the shell.
* **`rider-plugin/`** - a Java plugin (no Gradle: `Commands/PluginCommand.cs` runs `javac` against
  the platform jars and zips the result) adding *New Factorio Mod…*, *Build Factorio Mod* and a
  settings page. It shells out to the same two commands, so the plugin cannot produce a mod that the
  command line would not.

## Analyzers

`tools/Factorio.Analyzers` is a `netstandard2.0` Roslyn analyzer assembly - the same kind an editor
loads - with fourteen rules in three classes: `LanguageRules` (constructs the pinned compiler or the
runtime cannot lower), `TemplateRules` (shapes of `@CSharpLua.Template` that silently misbehave) and
`FactorioRules` (the bindings' own API used so it cannot work). `Factorio.ModTool/Build/Analysis.cs`
is the host: it parses the mod's files itself, so the rules run before the compiler and on code that
does not compile yet. `Emittability.cs` keeps them quiet about templated bodies and `@CSharpLua.Ignore`
types, which is what lets the 1,500 generated binding files pass with zero findings.

See `docs/ANALYZERS.md` for every rule and `tests/Analyzer.Tests` for the fixtures.

## Assets

`Assets/` is a deliberately small pipeline rather than an integration:

* `ObjModel` reads positions and triangles (`.obj` only);
* `Renderer` draws them with an orthographic camera, flat shading, 2x supersampling and a
  one-pixel outline;
* `Png` writes 8-bit RGBA without an image library, so the tool stays a single executable;
* `AssetPipeline` reads `assets/render.json`, hashes the inputs into `assets/.render.json`, and
  writes both the PNGs and a Lua sprite table that the data stage requires.

`"renderer": "blender"` swaps the middle step for `blender --background --python <generated>`,
using the same contract, so moving to a real renderer is a one-line change per sprite and not a
pipeline rewrite.

## Build order and failure modes

| stage | on failure |
| --- | --- |
| bindings missing | regenerated automatically before compiling |
| mod C# uses a construct the analyzers reject | error-severity finding with rule id, file and line; the compiler is never started (`--no-analyze` overrides) |
| C# does not compile | CSharp.lua's Roslyn errors, first 25 printed, full log in `build/logs/` |
| the C# data stage does not compile | Roslyn errors with file and line, prefixed by "the data-stage sources do not compile" |
| the data stage defines nothing | the build names the entry and says no prototypes were added |
| Lua is not 5.2 | `luac5.2 -p` diagnostic, build fails, nothing is packaged |
| Lua calls CoreSystem | `file:line: System.each is not provided by the compat shim` |
| a sprite cannot be rendered | build fails, naming the model and the sprite (`--no-assets` skips rendering) |
| a referenced sprite is missing | warning naming the Lua file and path |
| `data.lua` raises, or a prototype key is wrong | `data.lua: <message>`, or `recipe x: 'category' is not a property of RecipePrototype (did you mean 'categories'?)` |
| a public member would be undocumented | generation fails, naming the member and file |

Nothing is written to `build/mods` unless the whole pipeline succeeded, so a broken build never
leaves a stale installable mod behind.
