# Windows

The toolchain is a .NET 8 application, so everything runs on Windows. What does **not** work out
of the box is `make`: the `Makefile` this repository ships is GNU make syntax, and on Windows
`make` is frequently something else.

## Why `make` fails

```
C:\...>make setup
Undefined option: setup

C:\...>make
Updating TCC
 > Syncing TCC from https://github.com/vlang/tccbin
fatal: not a git repository (or any of the parent directories): .git
...
Building V...
Backend compiler error
```

That output is not this repository's makefile - it is the **V compiler's `make.bat`**, which ships
next to `v.exe` and wins the name `make` if V's folder is on `PATH`. Confirm which program you are
running:

```bat
where make
```

If it prints a path under `v\` (or `mingw32-make`, `nmake`, `choco`'s shim, ...), `make` is not
GNU make and will never read this `Makefile`. Three fixes, best first.

## 1. Skip make entirely (recommended)

`factorio-mod.cmd` in the repository root is the tool with no make in between. Double-click it or
run it from the folder that contains it:

```bat
cd C:\Users\you\Downloads\workspace-...\
factorio-mod.cmd setup
factorio-mod.cmd new AirExtractor
factorio-mod.cmd build mods\HelloFactorio
factorio-mod.cmd doctor
```

`setup` builds the tool, builds the pinned CSharp.lua compiler from the bundled `vendor/` sources,
and uses the API JSON already in `api\`. Re-running it is cheap: every step is skipped when its
output is there.

Each `make` target maps to one command:

| make | Windows |
| --- | --- |
| `make tool` | done automatically by `factorio-mod.cmd` |
| `make setup` | `factorio-mod.cmd setup` |
| `make new MOD=AirExtractor` | `factorio-mod.cmd new AirExtractor` |
| `make build` | `factorio-mod.cmd build` |
| `make build MOD=X` | `factorio-mod.cmd build mods\X` |
| `make analyze` | `factorio-mod.cmd analyze` |
| `make assets MOD=X FORCE=1` | `factorio-mod.cmd assets mods\X --force` |
| `make test` | see [Tests](#tests) |

## 2. Use GNU make

If you want the `make` targets, install GNU make and call it by a name that does not collide:

```bat
winget install --id ezwinports.make
mingw32-make setup
```

or `choco install make` and run `make` from a shell where `where make` points at it. `mingw32-make`
is the simplest: it is GNU make under a different file name, so the V `make.bat` cannot shadow it.

With GNU make, `make setup`, `make build`, `make new` and `make analyze` work as they do on Linux.
`make test` does not: the recipe names `lua5.2` and shells out to `python3`, so use the commands in
[Tests](#tests) instead.

## 3. WSL

Everything in this repository's CI runs on Linux; `wsl` with Ubuntu is the closest match. Clone or
copy the workspace into the Linux filesystem (`~/`), not `/mnt/c/`, then `make setup`.

## Requirements

| what | why | how |
| --- | --- | --- |
| .NET 8 SDK | builds the tool and runs the compiler | <https://dotnet.microsoft.com/download/dotnet/8.0> |
| Git | only for `bootstrap` when `vendor/CSharp.lua` is absent | <https://git-scm.com/download/win> |
| Lua 5.2 (`lua52.exe`, `luac52.exe`) | data-stage checks and Lua syntax checks | see below |

The workspace ships `vendor/CSharp.lua` as plain sources (no `.git`), so `bootstrap` builds it
in place and skips the commit check; Git is only needed if that folder is missing and the compiler
has to be cloned.

### Lua 5.2 on Windows

`lua5.2`/`luac5.2` are Unix names. On Windows the same binaries are usually called `lua52.exe` and
`luac52.exe`; the tool looks for those names too, so putting them on `PATH` is enough:

```bat
where lua52
lua52 -v
```

* Prebuilt binaries: the "LuaBinaries" builds from <https://luabinaries.sourceforge.net/> - take the
  **5.2.4** Windows x64 zip and copy `lua52.exe` and `luac52.exe` into a folder on `PATH`.
* Or build it: `winget install --id Lua.Lua` installs a *newer* Lua (5.4), which parses 5.2 code but
  is not the version Factorio embeds, so it is not a substitute for the syntax check.

To point the tool at a specific copy instead of changing `PATH`:

```bat
set FACTORIO_LUA=C:\tools\lua52.exe
set FACTORIO_LUAC=C:\tools\luac52.exe
factorio-mod.cmd doctor
```

`FACTORIO_LUA` / `FACTORIO_LUAC` override `config/toolchain.json`. `doctor` prints the resolved
binaries and their versions, so a wrong copy is visible before a build uses it.

Without Lua 5.2 the build still works: the CoreSystem scan and every prototype check that does not
need an interpreter still run, `data.lua` is not executed, and `doctor` says which checks are being
skipped. With Lua 5.2 missing, `make test`'s Lua steps cannot run either.

### Other environment variables

| variable | meaning |
| --- | --- |
| `FACTORIO_REPO` | where the repository is, for running the tool from another folder |
| `FACTORIO_LUA`, `FACTORIO_LUAC` | Lua interpreter and compiler |
| `DOTNET` | the `dotnet` executable, if it is not the one on `PATH` |
| `FACTORIO_MOD_NAME`, `FACTORIO_MOD_DIR`, `FACTORIO_MOD_AUTHOR` | defaults for `new` |

## Tests

`make test` runs three xunit projects and then three Lua scripts. On Windows:

```bat
dotnet test tests\Generator.Tests -c Release
dotnet test tests\Transpile.Tests -c Release
dotnet test tests\Analyzer.Tests -c Release
lua52 tests\lua\run_mod.lua build\mods\HelloFactorio_0.2.0\control.lua
lua52 tests\lua\gas_extractor_test.lua build\mods\GasExtractor_0.4.0\control.lua
lua52 tests\lua\util_test.lua
```

The first three need the compiler built (`factorio-mod.cmd setup`) and take a few minutes; the Lua
scripts need `lua52.exe` on `PATH` and the two mods built first.

## Building for the game

Point the build straight at Factorio's mod folder - no copying afterwards:

```bat
factorio-mod.cmd build mods\GasExtractor --output "%APPDATA%\Factorio\mods"
```

The output is `<output>\GasExtractor_0.4.0\` and `GasExtractor_0.4.0.zip`; the zip is what the mod
portal wants, the folder is what the game loads while you are iterating.
