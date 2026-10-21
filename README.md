# C# modding for Factorio 2.0 and 2.1

Write Factorio mods in C#. This repository generates a complete, documented C# view of the
official Factorio API from `lua-api.factorio.com` and compiles mod sources to the single
`control.lua` a Factorio mod is allowed to ship — using the real, pinned
[CSharp.lua](https://github.com/yanghuan/CSharp.lua) compiler, then packaging the result
(with its images, locale files and data-stage Lua) as a loadable mod zip.

```csharp
public static class Extractors
{
    private static readonly EntityRegistry Machines = Storage.Registry("machines");
    private static readonly EntityValue<string> Gas = Machines.Value<string>("gas");

    public static void Init()
    {
        Lifecycle.OnBuilt("gas-extractor", (machine, builder) => Machines.Add(machine));
        Lifecycle.OnRemoved("gas-extractor", machine => Machines.Remove(machine));
        Lifecycle.EverySecond(() => Machines.ForEach(machine => Gas.Set(machine, Pick(machine))));

        Commands.Add("gas", "gas-extractor.command-gas-help", command =>
        {
            if (command.Player == null) { command.Reply(Loc.Key("gas-extractor.message-no-player")); return; }
            command.Reply(Loc.With("gas-extractor.message-gas-set", command.Parameter));
        });
    }
}
```

Prototypes are C# too. A definition declares its prototype the way the rest of the API is written -
typed names for the closed sets, a `LocalisedName` for the string the player actually sees, and a
`build` that checks the result against the game's own API before writing the zip:

```csharp
public sealed class PolymerPellet : ItemDefinition
{
    public PolymerPellet() : base("demo-polymer-pellet")
    {
        Icon = Asset.Mod("graphics/icons/polymer-pellet.png");
        IconSize = 64;
        StackSize = 100;
        Subgroup = Subgroup.IntermediateProduct;   // not the "Other" tab
        Order = SortOrder.At("z", "demo-polymer-pellet");
        LocalisedName = "Polymer pellet";          // not "Unknown key: item-name.demo-polymer-pellet"
    }
}
```

Everything on the C# side is idiomatic C#: `PascalCase` properties and methods, `camelCase`
parameters, `Lifecycle.OnBuilt/OnRemoved` instead of half a dozen build and removal event ids,
`out` parameters instead of tuples, `null` checks that compile to `== nil`. Native types lose the
`Lua` prefix — `game.get_player` returns a `Player`, `player.character` an `Entity` — while the
emitted Lua keeps the names the game expects. The Lua it produces is ordinary, hand-written
looking mod code — `game.get_player(1)`, never `game:get_player(1)`.

The utilities in `src/Factorio.Bindings/Utilities/` are what make a mod read like a small C#
program instead of a script: an `EntityRegistry` that holds a save's machines by unit number and
drops the ones that stop existing, `EntityValue<T>`/`Setting<T>` handles for the data recorded
against them, `Lifecycle` for every way an entity can be built or removed, `Commands` for commands
that already know who typed them, and `Crafting` for the awkward `get_recipe()`/`set_recipe()`
pair. All of them compile to plain Lua calls in `FactorioUtils.lua`; none allocates a wrapper.

## Install it

If you only want to write a mod, do not clone this repository: download a release and let the
installer put the toolchain on the machine.

| Platform | Download | Then |
| --- | --- | --- |
| Windows | `FactorioCSharpModder-<version>-win64.exe` | run it, next, next, finish |
| Linux x64 | `FactorioCSharpModder-<version>-linux-x64.tar.gz` | `tar -xzf ...` then `./install.sh` |
| macOS (Apple Silicon) | `FactorioCSharpModder-<version>-osx-arm64.tar.gz` | the same |

The installer copies the tool and its data to a folder of your choosing, offers to put
`factorio-mod` on `PATH`, and **asks** whether to add the "Factorio mod (C#)" project template to
`dotnet new` - which is what puts it in Rider's **New Solution** dialog. Either answer can be
changed later (`factorio-mod template`, `factorio-mod template --uninstall`).

```
factorio-mod doctor          # toolchain, API documents, compiler, examples
factorio-mod new MyMod       # a mod project, template and all
```

A mod project builds with its own Build button (Ctrl+F9 in Rider) or `factorio-mod build`; the
result is `build/mods/<Name>_<version>.zip`, which goes straight into `mods/` in your Factorio
folder. See [`docs/RIDER.md`](docs/RIDER.md) for the IDE side.

Maintainers: [`docs/RELEASING.md`](docs/RELEASING.md) is the release recipe, and
`.github/workflows/release.yml` runs it on a tag.

## Factorio versions

The toolchain builds for **Factorio 2.0** and **2.1** from the same sources, one API document set
each:

| mod.json says | API documents | bindings | loads in |
| --- | --- | --- | --- |
| `"factorioVersion": "2.0"` (default) | `api/2.0.72` | `src/Factorio.Bindings/Generated/2.0.72` | 2.0 and newer |
| `"factorioVersion": "2.1"` | `api/2.1.22` | `src/Factorio.Bindings/Generated/2.1.22` | 2.1 only |

A game accepts a mod that declares an older version, so `2.0` is the default: the same zip loads in
2.0 and in 2.1. Declaring `2.1` opts into the members added in 2.1 - and then the mod will not load
in 2.0, which is the point of declaring it. Bindings are generated per version, so a 2.0 mod cannot
call a 2.1-only member by accident, and the data-stage check validates prototypes against the same
version's schema (2.0 recipes take `category`; 2.1 added `categories`).

Both trees ship with the repository, so a 2.0 or a 2.1 mod builds out of the box. `factorio-mod
doctor` lists every version with its documents and bindings; `factorio-mod setup --all` (or
`make setup ALL=1`) re-fetches and regenerates all of them after an API change.

## Quick start

The `make` targets are defined by the **`Makefile` in the repository root**, so `make` has to be
run from that directory (the one containing `Makefile`, `config/` and `tools/`). `make` does not search
upward for a makefile, so from any other directory it reports

```
make: *** No rule to make target 'tool'.  Stop.
```

`cd` to the repository root first:

```bash
cd /path/to/factorio-csharp      # the folder with Makefile in it
make setup                       # compiler + API JSON + bindings, in one step
make build                       # compile mods/* -> build/mods/<Name>_<version>{,.zip}
make new MOD=AirExtractor        # scaffold a new mod and build it once
make template                    # install the mod template into `dotnet new` (Rider's New Solution list)
make plugin                      # build the Rider/IntelliJ plugin (first run downloads a platform)
make test                        # generator, transpiler, analyzer tests + Lua mock run
```

`make` is only a shortcut for the tool, which does everything on its own and does not care which
directory it is run from:

```
factorio-mod setup [--offline] [--all]      compiler, API JSON and bindings in one go
factorio-mod new [NAME] [--dir DIR]         scaffold a new mod (and build it once)
factorio-mod bootstrap [--force]            clone, patch and build the pinned CSharp.lua
factorio-mod fetch-api [--factorio 2.0|2.1] [--all] [--offline]
                                            download the API JSON + write api/api.lock.json
factorio-mod generate [--factorio 2.0|2.1] [--all] [--out DIR]
                                            generate the C# bindings for one or all versions
factorio-mod build [project...] [options]   render assets, compile a mod, package it, check it
factorio-mod assets [project...] [--force]  render models to sprites + sprite tables only
factorio-mod analyze [project...|DIR]       run the C# analyzers without building anything
factorio-mod doctor                         print the resolved toolchain and its status
factorio-mod template [--uninstall|--list]  install the `dotnet new` template (Rider's New Solution)
factorio-mod dist [--output DIR] [--version V] [--runtime RID]
                                            assemble the tree an installer ships
```

Run it as `dotnet tools/Factorio.ModTool/bin/Release/net8.0/factorio-mod.dll <command>` from the
repository root. It finds the repository itself — from `--repo DIR`, then `$FACTORIO_REPO`, then the
working directory, then the tool's own folder — so `factorio-mod build ~/mods/Whatever` works from
anywhere once the tool is built.

**Windows**: `make` there is often not GNU make - if running it prints `Updating TCC` / `Building V`
or `No rule to make target`, it is another program that happens to be called `make` (V's
`make.bat`, for instance). Use `factorio-mod.cmd` in the repository root instead, which is the same
tool with no make in between:

```bat
factorio-mod.cmd setup
factorio-mod.cmd new AirExtractor
factorio-mod.cmd build mods\HelloFactorio
```

See [`docs/WINDOWS.md`](docs/WINDOWS.md) for the requirements (Lua 5.2 is called `lua52.exe` there),
the environment variables, and how to get GNU make if you want the targets.

## Starting a new mod

```bash
factorio-mod new AirExtractor
```

writes `./AirExtractor/` from `templates/mod/` (mod manifest, a documented `Main.cs`,
`locale.cfg`, `changelog.txt`, a generated `thumbnail.png`, and an `AirExtractor.csproj` so an IDE
can build it), then builds it once, so the first thing you get is
`build/mods/AirExtractor_0.1.0.zip` that loads in the game.

| argument | environment variable | meaning |
| --- | --- | --- |
| `NAME` | `FACTORIO_MOD_NAME`, `MOD_NAME`, `MOD` | mod name: letters, digits, `_` and `-` |
| `--dir DIR` (also `--dir=DIR`, `-d`) | `FACTORIO_MOD_DIR` | where to create it; `new C:\mods\Mod` also works, and the last path segment becomes the name |
| `--author NAME` | `FACTORIO_MOD_AUTHOR`, `GIT_AUTHOR_NAME`, `$USER` | author in `mod.json` |
| `--title T` (also `-t`) | — | title shown in Factorio's mod list (default: the name, spaced out) |
| `--version V` (also `-v`) | — | starting version (default `0.1.0`) |
| `--bindings` | — | copy the generated bindings into the mod (self-contained) |
| `--no-build` | — | write the files, do not build |
| `--force` | — | write into a directory that already has files |

A path can be given instead of a name: `factorio-mod new C:\mods\AirExtractor` creates the mod in
that folder and derives the name (`AirExtractor`), the namespace and the entry point from the last
segment.

The C# namespace and namespaces inside the template are derived from the name
(`gas-extractor` → namespace `GasExtractor`, entry `GasExtractor.Main.Init`). The API itself is
**not** regenerated per mod: it lives in this repository
(`src/Factorio.Bindings/Generated/<version>`, 1,466 files for 2.0.72, 11 MB) and every mod builds against it, so the bindings are not copied into each of your mods.
`--bindings` exists for the case where a mod has to build without this repository next to it.

## What `build` does

`factorio-mod build mods/HelloFactorio` is the whole pipeline:

1. renders every model named by `assets/render.json` into sprite PNGs and a Lua sprite table
   (`--no-assets` skips this; `factorio-mod assets` does it alone);
2. resolves the mod's `factorioVersion` and picks that version's bindings; regenerates them if
   they are missing (`--regenerate` always does);
3. runs the C# analyzers over the mod's own sources and stops on anything that cannot work
   (`--no-analyze` skips this; see [`docs/ANALYZERS.md`](docs/ANALYZERS.md));
4. passes the mod sources **plus** the generated bindings to CSharp.lua;
5. bundles the compiler's modules into one `control.lua` with a 150-line compatibility shim
   (the stock `CoreSystem.lua` is Lua 5.3-only and far too large for a mod);
6. drops the binding modules — every generated type is `@CSharpLua.Ignore`, so they contain
   nothing but namespace declarations — and re-declares just those namespaces;
7. copies `resources/**` to the same relative paths, `lua/**` to the mod root, and explicit
   `files` (thumbnail, changelog) to the root;
8. writes `info.json` from the mod's `mod.json`;
9. verifies that every `graphics/...`-style path mentioned in Lua actually exists, and that the
   result parses as **Lua 5.2** and calls nothing from CoreSystem;
10. zips it to `build/mods/<Name>_<version>.zip` with the documented top-level
   `<Name>_<version>/` folder (`--no-zip` skips this, `--output DIR` writes the folder and the zip
   somewhere else, e.g. straight into Factorio's `mods` directory).

Steps 1-9 read their settings from the mod's `"build"` object first and the command line second, so
a build started by an IDE has nothing to pass but the folder.

```
build HelloFactorio 0.2.0
  factorio   2.0 (api 2.0.72)
  compiling 749 C# files
  bundled 1 modules, dropped 748 namespace-only modules
  data stage: 1 prototype(s) checked against prototype-api 2.0.72
  mod        build/mods/HelloFactorio_0.2.0
  zip        build/mods/HelloFactorio_0.2.0.zip
  checks     Lua 5.2 syntax ok, 1 prototype(s) validated, 0 problem(s)
  done       11.0s
```

## Building it from an IDE

`factorio-mod new` writes a `<Name>.csproj` next to `mod.json`. It compiles nothing: its `Build`
target runs `factorio-mod build`, so **Rider's or Visual Studio's own Build button produces the Lua
mod and its zip** — no run configuration, no external tool.

```text
Ctrl+F9 →  factorio-mod build  →  build/mods/<Name>_<version>.zip
```

Everything that used to be a flag is written in the mod, next to the mod:

```jsonc
"build": {
  "output": "out",        // where the folder and zip go
  "zip": true,
  "analyze": true,
  "assets": true,
  "validate": true,
  "lua51": false,
  "regenerate": false,
  "format": "msbuild"     // findings that an IDE's build log can link
}
```

A flag on the command line still overrides the file. Outside this repository, point the project at
it once with `FACTORIO_MOD_REPO`, or pass `-p:FactorioRepo=<path>`.

**A template, if you would rather start from one.** `factorio-mod template` installs
`templates/dotnet/factorio-mod/` into the .NET template engine, which is what Rider's New Solution
dialog reads, so **Factorio mod (C#)** appears in it — and `dotnet new factorio-mod -n MyMod` works
on the command line and in Visual Studio, with no plugin:

```bash
factorio-mod template
dotnet new factorio-mod -n AirFilter --title "Air Filter" --factorio 2.1
```

There is also a Rider/IntelliJ plugin: `plugin/FactorioCSharpMods-1.0.0.zip` adds
*New Factorio Mod…* (File → New and Tools) and *Build Factorio Mod* and shows the checker's findings
in a console where they are clickable. Install it from disk in Settings → Plugins. Both routes are
described in [`docs/RIDER.md`](docs/RIDER.md).

## A mod project

```
mods/DemoChemistry/
  mod.json                  project description (not part of the shipped mod)
  src/*.cs                  C# sources - any *.cs below `sources`, compiled to control.lua
  data/*.cs                 data-stage sources - any *.cs below `dataSources`, run at build time
  assets/gas-extractor.obj  3D model
  assets/render.json        what to render from it (views, size, colour)
  resources/                packaged verbatim: resources/graphics/x.png -> graphics/x.png
  lua/data.lua              static Lua copied to the mod root (data stage, migrations, ...)
  lua/generated/*.lua       sprite tables written by the asset pipeline (packaged as generated/)
  thumbnail.png             listed in `files`, copied to the mod root
```

Three example mods ship with the repository:

| mod | what it shows |
| --- | --- |
| `mods/HelloFactorio` | the minimum: events, globals, native objects, one sprite |
| `mods/GasExtractor` | two machine tiers and their recipes written in `lua/data.lua`, a trigger technology, one 3D model rendered to four sprite sets, a typed registry, recipe handling that survives players picking recipes, a per-second pass, console commands, localised strings |
| `mods/DemoChemistry` | the data stage in C#: a fluid built on a vanilla one, two items, a machine with its own rendered graphics built on a vanilla prototype, the item that places it and four recipes - a complete, craftable chain with borrowed (and credited) art - see [`docs/DATA.md`](docs/DATA.md) |

The gas extractor's setting *is* its recipe, and the game fires no event when a player clicks a
different one in the machine window. The mod therefore treats the machine's own recipe as the
truth: whatever it is set to is recorded as that machine's gas. It writes a recipe only when the
slot is empty, and never while a player has the machine open - changing the recipe under an open
window closes it, which is what made the recipe list disappear mid-click. `Crafting.IsOpen` is
that check, and `Extractors.Sync` is the four-line rule that uses it.

## Assets: model → sprite

`factorio-mod build` renders models before it compiles anything, so a sprite is never out of date
with its model:

```jsonc
// mods/GasExtractor/assets/render.json
{
  "sprites": [
    {
      "model": "gas-extractor.obj",        // relative to assets/
      "output": "graphics/entity/gas-extractor",  // relative to resources/
      "size": 128,
      "views": [0, 90, 180, 270],          // one PNG per view; 1 view returns a single sprite
      "pitch": 45, "zoom": 0.74,
      "color": "#6E7A8A",
      "lua": "lua/generated/gas-extractor-sprite.lua",
      "renderer": "builtin"                // or "blender"
    }
  ]
}
```

The generated Lua is a plain sprite table, so `data.lua` stays readable:

```lua
local sprite = require("generated.gas-extractor-sprite")
...
graphics_set = { animation = { layers = { sprite.north } } }
```

* **`builtin`** is a small software renderer in the tool (orthographic, flat shading, supersampled,
  one-pixel outline). It needs nothing installed and is what the shipped model uses.
* **`blender`** shells out to `blender --background --python ...` with the same contract, for models
  that need real materials or more than a silhouette. It is skipped with a clear error if Blender is
  not on `PATH`.
* Results are hashed into `assets/.render.json`, so a build with an unchanged model re-renders
  nothing.

```jsonc
{
  "name": "HelloFactorio",             // Factorio-legal mod name (validated)
  "title": "Hello Factorio (C#)",
  "version": "0.2.0",
  "author": "factorio-csharp",
  "description": "...",
  "factorioVersion": "2.0",            // -> info.json factorio_version; "2.1" opts into 2.1
  "dependencies": ["base"],
  "entry": "HelloFactorio.Main.Init",  // the C# method called once the mod loads
  "dataEntry": "Demo.ExampleMod",      // the ModDefinition run at build time, or omit for lua/data.lua
  "bindings": "runtime",               // runtime | prototype | none
  "sources": ["src"],                  // *.cs compiled to control.lua
  "dataSources": ["data"],             // *.cs run at build time to produce data.lua
  "resources": ["resources"],
  "lua": ["lua"],
  "files": ["thumbnail.png"]
}
```

## Data-stage validation

Prototypes come from one of two places: `lua/data.lua` written by hand (`GasExtractor`), or C#
definitions under `data/` that the build runs and writes out as `data.lua` (`DemoChemistry`, see
[`docs/DATA.md`](docs/DATA.md)). Both are then checked the same way.

`factorio-mod build` runs the packaged `data.lua` under Lua 5.2 with a stub `data` table and a
`defines` table built from the real API document (`defines.direction.north` is `0`, so a misspelt
`defines` member is an error rather than a silent stub). Every prototype the script hands to
`data:extend` is then checked against `api/<version>/prototype-api.json`:

* the type exists, every key is a property of it (inherited included), every required property is
  there — and the same again inside nested tables, so a typo in
  `fluid_boxes[1].pipe_connections[2].flow_direction` is reported with its path;
* the geometry Factorio validates when it loads the entity: a pipe connection must sit inside
  `collision_box`, which the engine shrinks by 0.001 first; two connections of one fluid box cannot
  share a position; every connection needs a direction unless it is a linked one; a fluid box
  volume must be greater than zero; a box cannot be inside out;
* the names a prototype looks up in `data.raw`: a recipe `category`/`categories` and a machine's
  `crafting_categories` must be a `recipe-category` this mod declares or one the base game ships,
  otherwise the game stops with `Error in assignID: recipe-category with name … does not exist`;
* **reachability**, which the game does not check at all: an item that places a machine but that no
  recipe produces, and a recipe that is disabled with no technology unlocking it. Both load fine and
  both are invisible, which is why a machine can be missing from the crafting menu with nothing in
  the log. These are warnings, not errors — another mod may add the recipe:

  ```
  warning  item 'gas-extractor' places 'gas-extractor' but no recipe in this mod produces it,
           so it cannot be crafted and never appears in the crafting menu
  ```

That last group is the difference between a build error and this, after a restart:

```
Failed to load mods: Error while loading entity prototype "gas-extractor" (assembling-machine):
PipeConnectionDefinition: position must be inside of entity bounding box.
position={0.000, -1.500}, leftTop={-1.199, -1.199}, rightBottom={1.199, 1.199}
```

and the build error it now produces instead:

```
data.lua:0: assembling-machine gas-extractor: fluid_boxes[1].pipe_connections[1].position is
{0, -1.5}, outside the entity bounding box {{-1.199, -1.199}, {1.199, 1.199}}; a pipe connection
must sit inside collision_box, or Factorio refuses to load the entity
("position must be inside of entity bounding box")
```

## Generated API

`make generate` writes **1,466 files** for Factorio 2.0.72 (**1,569** for 2.1.22) from the real
documents:

| output | contents |
| --- | --- |
| `Runtime/Objects/<Name>.cs` | one file per native object type (148 in 2.0, 157 in 2.1; `LuaPlayer` → `Player`) |
| `Runtime/Tables/<Name>.cs` | one file per Lua table shape (589 in 2.0, 666 in 2.1) |
| `Runtime/Events.cs`, `Runtime/Events/*.cs` | typed `script.on_event` helpers and their event data |
| `Runtime/Globals.cs` | `Globals.Game`, `Globals.Script`, `Globals.Log`, … |
| `Runtime/Defines.cs` | `Defines.Events.OnTick` → `defines.events.on_tick` |
| `Prototypes/**` | the data stage: `Globals.Data.Raw`, one class per prototype |
| `api-report.{json,md}` | every decision the generator made, per member |
| `renames.json` | API name → C# name for every class the generator renamed |

Every public member has a `<summary>`: Factorio's own text where the JSON provides one,
otherwise a written fallback that names the native member. The generator **fails the build**
if any public member would ship undocumented (`Api/DocCheck.cs`), which is why the docs cannot
rot silently.

See [`docs/DATA.md`](docs/DATA.md) for the data stage in C#,
[`docs/ARCHITECTURE.md`](docs/ARCHITECTURE.md) for the layout,
[`docs/COMPATIBILITY.md`](docs/COMPATIBILITY.md) for the full C# ↔ Lua mapping rules and
[`docs/ANALYZERS.md`](docs/ANALYZERS.md) for the fourteen compile-time rules that reject C# the
compiler or the runtime cannot lower.

## Verification

| check | how | result |
| --- | --- | --- |
| bindings generated from the official JSON | `api/2.0.72/*.json` and `api/2.1.22/*.json`, sha256-pinned in `api/api.lock.json` | 2.0.72: 148 classes, 963 methods, 2,300 attributes, 1,352 define leaves, 219 events; 2.1.22: 157 / 1,044 / 2,773 / 1,414 / 229 |
| mod C# is legal | 14 Roslyn analyzers (FX0001-FX0014) run before the compiler; the shipped mods and all 3,043 bindings files are clean | 0 findings |
| emitted C# compiles | CSharp.lua type-checks it with Roslyn on every build | 0 errors, 0 skipped |
| emitted Lua is real Lua | `luac5.2 -p` on every packaged file | passes |
| emitted Lua has no CoreSystem dependency | banned-symbol scan at build time | passes |
| emitted Lua behaves | `tests/lua/run_mod.lua` and `tests/lua/gas_extractor_test.lua` run the packaged `control.lua` against a Factorio mock and assert exact output | passes (both mods) |
| the util library behaves | `tests/lua/util_test.lua` runs `FactorioUtils.lua` directly under Lua 5.2 | passes |
| data-stage prototypes match the API | every `data:extend` entry checked against the mod's version of `prototype-api.json` key by key, nested tables included, plus the geometry and reachability the engine either validates or silently ignores | 21 prototypes across three mods, 0 problems |
| a data stage in C# behaves | `DataEntryTests`/`DataNamesTests` run the definitions through the tool; `tests/lua/data_inherit_test.lua` runs the generated `data.lua` against a stub `data.raw` and asserts the inherited copy, the cleared field and the removal of the build's markers | passes |
| a prototype's graphics can be found in the game | every `icon`/`filename`/`sound` checked for the `__source__/` prefix Factorio requires, and every `__<this mod>__/` path checked against the packaged files (`Build/DataStageResources.cs`) | passes (both mods) |
| the crafting-menu tab and the display name are right | every `subgroup`/`group` checked against the base game's 11 groups and 113 subgroups or the mod's own, items with no subgroup warned about, every visible prototype required to have a locale entry (`Build/DataStageReferences.cs`, `Build/LocaleCheck.cs`) | passes (both mods, 0 warnings) |
| data-stage geometry is loadable | pipe connections tested against `collision_box` shrunk by 0.001, duplicate connections, fluid-box volumes, box orientation (`Build/DataStageRules.cs`) | passes (both mods) |
| rendered sprites are real images | PNG written by the tool, decoded by the tests | passes |
| the IDE plugin loads | JetBrains `intellij-plugin-verifier check-plugin` against IC-242.24807.4 and IC-262.10968.63 | Compatible (1.0.0) |
| a mod builds from an IDE's Build button | every mod carries a `.csproj` whose Build target runs `factorio-mod build`; `ProjectSettingsTests` checks the generated project and mod.json's `build` object | passes |
| the example mod's art is licensed | `mods/DemoChemistry/CREDITS.md`, shipped in the zip; every file names its author and licence | MIT / CC BY |
| golden output is stable | `tests/golden/HelloFactorio.lua` | passes |
| runs inside Factorio | **not run** — no game binary in this environment | see [`docs/INTEGRATION.md`](docs/INTEGRATION.md) |

## Layout

```
factorio-mod.cmd          Windows launcher: builds the tool if needed, then runs it (no make)
config/toolchain.json     pinned dotnet, CSharp.lua commit/patch, Factorio version
templates/mod/            what `factorio-mod new` copies: manifest, Main.cs, locale, changelog
api/2.0.72/*.json         official API documents, one folder per supported version
api/2.1.22/*.json
api/api.lock.json         sha256 of every document, per version
src/Factorio.Bindings/    Compat.cs (hand-written compile-time types) + FactorioCompat.lua (shim)
src/Factorio.Bindings/Generated/<version>/   bindings + api-report, one tree per version
tools/Factorio.ModTool/   the tool: Api/ (schema, mapping, emitters), Build/ (compile, bundle, package),
                          Assets/ (OBJ reader, renderer, PNG writer)
src/Factorio.Bindings/Utilities/  EntityRegistry, EntityValue<T>, Setting<T>, Lifecycle, Commands, Crafting,
                          StorageSlot<T>, Native, Entities, Lists, Numbers, Ticks, Loc
src/Factorio.Bindings/Data/       the data-stage API: ModDefinition, *Definition, Ambient values, LuaWriter
mods/HelloFactorio/       minimal example mod
mods/GasExtractor/        the bigger example: machine, model, assets, registry, commands
mods/DemoChemistry/       data stage in C#: fluid, items, machine, recipes, credits
tests/                    generator tests, transpiler tests, Lua mock, golden output
rider-plugin/             the Rider/IntelliJ plugin (Java, no Gradle)
installer/                Windows (Inno Setup) and POSIX installers for the release assets
.github/workflows/        the release workflow: tag -> installer + archives -> GitHub release
templates/dotnet/         the `dotnet new` template: what Rider's New Solution dialog lists
plugin/                   the built plugin zip, ready to install
docs/                     architecture, compatibility, limitations, integration
```

## Honest limitations

* No Factorio binary here, so `control.lua` has never been executed by the game itself; the Lua
  mock and `luac5.2` are the strongest checks available in this environment.
* The data-stage API covers the prototypes a mod commonly adds (fluids, items, assembling machines,
  recipes, categories); every other prototype type is reachable through `Field("key", value)` rather
  than a typed class. See [`docs/DATA.md`](docs/DATA.md#deviations-from-the-ideal-and-why).
* The data-stage check sees only what goes through `data:extend`, and it skips entities that
  declare no `collision_box`, so it cannot catch every geometry problem the engine would.
* The builtin renderer shades flat and writes no textures; the Blender path is wired but untested
  here (no Blender in this environment).
* `using static Factorio.Runtime.Globals;` crashes the pinned compiler; qualify the globals.

Details in [`docs/LIMITATIONS.md`](docs/LIMITATIONS.md).
