# Building mods in Rider

Two things make Rider a good place to write these mods: the plugin, and the `.csproj` every mod
carries. The second one matters more - it is what makes the **Build** button build the mod.

## 1. The Build button

Every mod folder has a `<Name>.csproj`. It is a .NET project that compiles nothing: its `Build`
target runs `factorio-mod build <mod folder>`.

```text
Build → Ctrl+F9 →  factorio-mod build
                     src/*.cs   → control.lua
                     data/*.cs  → data.lua
                     resources/ → copied
                     checks     → prototypes, tabs, names, paths, geometry
                     → build/mods/<Name>_<version>.zip
```

So there is nothing to remember and nothing to configure:

| you press | what happens |
| --- | --- |
| Build (Ctrl+F9) | the mod is built and packaged |
| Rebuild | the same, from scratch |
| Build Solution | every mod in the opened folder is built |

The .NET compiler output (`bin/`, `obj/`) is noise from an empty assembly; both are in the
template's `.gitignore`.

**Which tool it runs.** Inside this repository the project finds `../../factorio-mod.cmd` (Windows)
or the Release `factorio-mod.dll` (elsewhere) by itself. A mod checked out somewhere else needs to be
told where the repository is, once, in an environment variable:

```bat
set FACTORIO_MOD_REPO=C:\src\factorio-csharp
```

or per build: `dotnet build -p:FactorioRepo=C:\src\factorio-csharp`.

**When a build fails**, the tool's findings appear in the build log:

```
data.lua(1,1): error FACTORIO: item demo-pellet: icon is 'graphics/icons/x.png', which has no source prefix ...
```

`"format": "msbuild"` in `mod.json` makes Rider and Visual Studio link those to the file and line.

## 2. How a mod is built is written in `mod.json`

Everything that used to be a flag is a property of the mod, next to the mod's sources:

```jsonc
{
  "name": "DemoChemistry",
  "build": {
    "output": "../../build/mods",  // where the folder and zip go (default: build/mods)
    "zip": true,                   // false: leave the folder, skip the zip
    "analyze": true,               // false: skip the analyzers
    "assets": true,                // false: skip assets/render.json
    "validate": true,              // false: skip the Lua 5.2 and symbol checks
    "lua51": false,                // true: emit Lua 5.1 syntax
    "regenerate": false,           // true: regenerate the bindings first
    "format": "text"               // or "msbuild", for an IDE's build log
  }
}
```

A relative `output` is relative to the **mod**, not to where the command ran, so `mods/X` builds to
the same place from anywhere. A flag on the command line still wins for a one-off:

```bat
factorio-mod build mods\DemoChemistry --no-zip
```

The `build` object is optional: with it absent, every setting is the toolchain's default.

## 3. The template (no plugin needed)

Rider's **New Solution / New Project** dialog is driven by the .NET template engine, so a
`dotnet new` template shows up there as a template you can pick:

```bat
factorio-mod.cmd template
```

(The release installer asks this question during setup - tick *Add the "Factorio mod (C#)" project
template to Rider and dotnet new* and there is nothing left to do. `factorio-mod template` is the
same command, for when the box was left unticked.)

installs `templates/dotnet/factorio-mod/` into the SDK. Then:

**File → New Solution** → *More Templates* on the left (first time only: **Install Template** →
pick `templates\dotnet\factorio-mod`) → **Factorio mod (C#)** appears in the list, with fields for
the display name, author and Factorio version.

The same template works without Rider:

```bat
dotnet new factorio-mod -n AirFilter
dotnet new factorio-mod -n AirFilter -o C:\mods\AirFilter --title "Air Filter" --factorio 2.1
```

What it writes is the project you would otherwise assemble by hand:

```
AirFilter/
  AirFilter.csproj          Build button -> factorio-mod build
  mod.json                  name, title, author, factorioVersion, entry, dataEntry, "build"
  src/Main.cs               compiled to control.lua
  data/ExampleMod.cs        run at build time, written to data.lua
  resources/locale/en/
  changelog.txt, .gitignore
```

Open the folder and press Build; the zip lands in `build/mods/AirFilter_0.1.0.zip`.

The project finds the tool by itself: the checkout above it when the mod lives inside the
repository, and `factorio-mod` from `PATH` otherwise - which is what an installed copy provides. Nothing
has to be configured, and `-p:FactorioToolCommand=...` overrides it when it has to.

`factorio-mod template --list` shows whether it is installed, `--uninstall` removes it.

## 4. The plugin

`plugin/FactorioCSharpMods-1.0.0.zip` in this repository is a ready-to-install Rider plugin.

**Install:** Settings → Plugins → ⚙ → *Install Plugin from Disk…* → pick the zip → restart.
Requires Rider 2024.2 or newer (it is built against IntelliJ Platform 2024.2 and verified against
2024.2 and 2026.2).

| where | what it does |
| --- | --- |
| File → New → **New Factorio Mod…** | the same as below; this is where people look for "new project" |
| Tools → **New Factorio Mod…** | asks for a name, folder, title, author, version and Factorio version, runs `factorio-mod new`, optionally builds it once and opens it in a new window |
| Tools → **Build Factorio Mod** | runs `factorio-mod build` for the selected mods (or the open one) in a console where `file:line:` findings are clickable |
| right-click a mod in the project view → **Build Factorio Mod** | the same, for that mod, or for several at once |
| Tools → **Factorio C# Mods Settings…** | where `factorio-mod` is, the author, where new mods go, and whether a new mod is built and opened |

The plugin finds the tool by itself when Rider has this repository — or a mod inside it — open: it
looks for `config/toolchain.json` from the project folder upwards, then for `FACTORIO_MOD_REPO` /
`FACTORIO_MOD_TOOL`, then for `factorio-mod` on `PATH`. The settings dialog shows what a path
resolves to before you build with it.

### If the plugin does not appear

The plugin is a convenience; the template above and the `.csproj` do not need it.

1. **Check it is installed and enabled**: Settings → Plugins → Installed → "Factorio C# Mods".
   A red badge there means it failed to load (usually the IDE version - it needs **Rider 2024.2 or
   newer**; the build number is in Help → About).
2. **Find the actions by name**: press **Shift twice** (Search Everywhere) and type `Factorio`. You
   should see *New Factorio Mod…*, *Build Factorio Mod* and *Factorio C# Mods Settings…*. Running
   them from there is the same as from a menu.
3. **Where they are in menus**: File → New → *New Factorio Mod…*, and Tools → *New Factorio Mod…*,
   *Build Factorio Mod*, *Factorio C# Mods Settings…*. In the New UI the Tools menu is under the
   ☰ button.
4. **Nothing at all in Search Everywhere** means it did not load: Help → Show Log in Explorer, and
   look for `com.factorio.csharp` in `idea.log`.

### Rebuilding the plugin

```bat
factorio-mod.cmd plugin
```

or `make plugin`. It compiles `rider-plugin/src/main/java` against the IntelliJ Platform, writes
`build/plugin/FactorioCSharpMods-<version>.zip`, and copies it to `plugin/`.

The first run downloads what it needs and caches it in `%LOCALAPPDATA%\factorio-mod` (Windows) or
`~/.cache/factorio-mod` (elsewhere): a JDK 17 if none is found, and the IntelliJ Platform to compile
against (~700 MB). After that a rebuild takes a few seconds. Useful options:

```bat
factorio-mod.cmd plugin --jdk "C:\Program Files\JetBrains\Rider\jbr"
factorio-mod.cmd plugin --idea 2024.2.5          :: or a version, a folder, or an IDE zip
factorio-mod.cmd plugin --offline                :: never download; fail if the cache is empty
```

There is no Gradle here on purpose: a plugin this size is `javac` plus a zip, and the build then
needs nothing but a JDK - the same JDK Rider already ships.

## 5. What the IDE gives you, and what it does not

Worth being explicit, because the C# in `src/` is not compiled by the .NET compiler:

* **Editing** is normal C#: navigation, refactoring, find usages across the mod, all of it.
* **The analyzer rules** (FX0001-FX0014) run inside `factorio-mod build`, not in the IDE, so a
  construct they reject shows up as a build error rather than a red squiggle. `factorio-mod analyze`
  runs just that step.
* **Running** the mod means launching Factorio with the packed zip; nothing here starts the game.
* **Debugging** C# is not possible: the code that runs is the generated Lua. Debug the Lua with
  Factorio's own facilities (`log()`, `/c` console commands).
