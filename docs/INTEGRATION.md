# Running the mod inside Factorio

## Current status: NOT RUN

There is no Factorio binary in this environment, so no in-game claim in this repository is
verified. Every other gate passes (`make test`), but those gates prove the *Lua* is well formed and
behaves as intended against a mock — not that the game accepts it.

Reported, not hidden:

```
$ factorio-mod build mods/HelloFactorio mods/GasExtractor
build HelloFactorio 0.2.0
  factorio   2.0 (api 2.0.72)
  compiling 755 C# files
  bundled 1 modules, dropped 754 namespace-only modules
  data stage: 1 prototype(s) checked against prototype-api 2.0.72
  mod        build/mods/HelloFactorio_0.2.0
  zip        build/mods/HelloFactorio_0.2.0.zip
  checks     Lua 5.2 syntax ok, 1 prototype(s) validated, 0 problem(s)
build GasExtractor 0.4.0
  assets     graphics/entity/gas-extractor: up to date
  analyzers  0 finding(s)
  compiling 758 C# files
  bundled 4 modules, dropped 754 namespace-only modules
  data stage: 6 prototype(s) checked against prototype-api 2.0.72
  mod        build/mods/GasExtractor_0.4.0
  zip        build/mods/GasExtractor_0.4.0.zip
  checks     Lua 5.2 syntax ok, 6 prototype(s) validated, 0 problem(s)

$ lua5.2 tests/lua/run_mod.lua build/mods/HelloFactorio_0.2.0/control.lua
runtime check PASS (7 game.print calls, 1 log calls)
$ lua5.2 tests/lua/gas_extractor_test.lua build/mods/GasExtractor_0.4.0/control.lua
gas extractor check PASS (13 log lines, 2 commands registered)
# Factorio itself: not available here.
```

## Reproducing it

1. Install Factorio **2.0.72** or **2.1.22** (the Linux tarball is enough; `bin/x64/factorio` is
   the headless binary). The shipped mods target 2.0, so either install loads them.
2. Build the mod, straight into the game's mod directory:

   ```bash
   make tool bootstrap api generate
   dotnet tools/Factorio.ModTool/bin/Release/net8.0/factorio-mod.dll \
       build mods/HelloFactorio mods/GasExtractor --output "$HOME/.factorio/mods"
   ```

   This produces `~/.factorio/mods/HelloFactorio_0.2.0/` (and a zip next to it) — the folder name
   and archive layout Factorio expects.

3. Create a save headlessly, which runs `script.on_init`, then let it tick:

   ```bash
   FACTORIO="$HOME/factorio/bin/x64/factorio"
   "$FACTORIO" --create "$HOME/.factorio/saves/hello.zip" --mod-directory "$HOME/.factorio/mods" \
       --console-log "$PWD/build/integration.log"
   "$FACTORIO" --load "$HOME/.factorio/saves/hello.zip" --mod-directory "$HOME/.factorio/mods" \
       --until-tick 120 --console-log "$PWD/build/integration.log"
   ```

4. Assert on the log:

   ```bash
   grep -F "Hello from C#! This mod was compiled to Lua by factorio-mod." build/integration.log
   grep -F "The map has been running for " build/integration.log
   grep -F "Force 1: player" build/integration.log
   ! grep -E "Error while running|Lua error|stack traceback" build/integration.log
   ```

## What the in-game run proves that the local gates cannot

| claim | local gate | in game |
| --- | --- | --- |
| the bindings match the JSON | generator tests | – |
| the C# compiles | CSharp.lua / Roslyn | – |
| the Lua is valid | `luac5.2 -p` | Factorio's modified Lua 5.2 also accepts it |
| the Lua calls natives correctly | mock enforces dot calls and argument types | real API objects answer |
| `defines.events.on_tick` exists | mock invents the id | the game's value |
| the zip layout loads | zip structure test | the game indexes the mod |
| `script.on_init` runs | mock invokes it | the game invokes it |

On a headless `--create` there is no player, so `on_player_joined_game` will not fire; that handler
is exercised by `tests/lua/run_mod.lua` and by joining a normal game.

## What to look for in the log

```
   0.123 Script@.../control.lua:1: Hello from C#! This mod was compiled to Lua by factorio-mod.
   0.123 Script@.../control.lua:1: The map has been running for 0 ticks.
   0.123 Script@.../control.lua:1: Force 1: player
   0.123 Script@.../control.lua:1: Force 2: enemy
```

If the chunk fails to load, Factorio prints the exact Lua error and line in `control.lua`, which is
a single file by design — that is what makes this kind of failure easy to report.

## Installing in the GUI instead

Copy `build/mods/HelloFactorio_0.2.0/` into the mods folder, or import
`build/mods/HelloFactorio_0.2.0.zip` with "Install mod from file". Both are the same content:
`info.json`, `control.lua`, `data.lua`, `graphics/hello-icon.png`, `locale/en/strings.cfg`,
`thumbnail.png`.

## What to look for with the gas extractor

The mod adds a `Gas extractor` to the crafting menu (no research: the recipes are enabled from
the start so the machine is available immediately). In game:

1. place a gas extractor, connect a pipe to its north or south port, and power it;
2. open it and pick one of the three gas recipes — the machine fills the pipe with that fluid, the
   recipe list stays open while you click, and the choice survives the next per-second pass (it
   becomes that machine's recorded gas);
3. run `/gas light-oil` to switch every placed extractor at once (and set the default for new
   ones), and `/gas-status` for a per-player production total;
4. mine one, and `/gas-status` should report one fewer machine.

If a machine's recipe slot is cleared (a blueprint built without settings, or a player emptying
the slot), the per-second pass sets it back to the recorded gas — but only when no player has that
machine's window open, because writing a recipe closes it. Before that rule, the per-second pass
rewrote the machine's recipe from storage, so a recipe a player picked reverted a second later and
the recipe list closed mid-click.

Known unknowns that only the game can settle: that the assembling machine's fluid output behaves
as documented when the recipe has no item ingredients, and that `require("generated.…")` in
`data.lua` resolves against the mod root the same way the build's data-stage check assumes.
