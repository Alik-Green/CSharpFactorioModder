# Releasing

This is the recipe for publishing a GitHub release: the Windows installer, the Linux/macOS
archives, and the notes that go with them. It is written so an agent or a person can follow it
without knowing the toolchain.

Everything below runs from the repository root and is the same thing CI does; `.github/workflows/release.yml`
is the automated version of this page.

## What a release contains

| Asset | Made by | What it is |
| --- | --- | --- |
| `FactorioCSharpModder-<version>-win64.exe` | `installer/build.cmd` (Inno Setup 6) | The installer: the whole toolchain, a PATH entry, and the Rider template if the user wants it |
| `FactorioCSharpModder-<version>-linux-x64.tar.gz` | `installer/package.sh` | The same tree plus `install.sh` - unpacks, then `./install.sh` |
| `FactorioCSharpModder-<version>-osx-arm64.tar.gz` | `installer/package.sh` | The same, for Apple Silicon |

All three are built from one thing: the folder `factorio-mod dist` writes. Nothing is compiled at
release time except the tool itself - the CSharp.lua compiler is built once per release too, because
the tool cannot work without it.

## Prerequisites

* **.NET 8 SDK** (`dotnet --version` must work). The tool targets `net8.0` and a mod build needs the
  same SDK on the user's machine, so the installer checks for it.
* **git** and network access: `factorio-mod bootstrap` clones the pinned compiler.
* **Inno Setup 6** for the Windows installer: <https://jrsoftware.org/isdl.php>. Default location
  `C:\Program Files (x86)\Inno Setup 6\ISCC.exe`.
* **Python 3** if you run `make test` (the Makefile uses it to read versions). CI does not need it.

Lua 5.2 is only needed for the Lua mock tests in `make test`; the `dotnet test` projects do not
need it.

## The version number

The tag is `v<version>`, for example `v1.2.3`; everything else uses the bare number `1.2.3`.

The installer embeds the number in a Windows version resource, so it **must be numeric** - no
`-beta`, no `+build` metadata. The scripts strip a leading `v` for you.

## Build a release locally

```bash
# once: the pinned compiler (30-60 s, needs network)
dotnet build tools/Factorio.ModTool -c Release
dotnet tools/Factorio.ModTool/bin/Release/net8.0/factorio-mod.dll bootstrap

# the tool, published for one platform, in dist/
dotnet tools/Factorio.ModTool/bin/Release/net8.0/factorio-mod.dll dist \
    --output dist --version 1.2.3 --runtime win-x64
```

Check the result before packing it:

```bash
dist/bin/factorio-mod doctor                          # finds its own root, the API, the compiler
dist/bin/factorio-mod build dist/mods/HelloFactorio   # -> dist/build/mods/HelloFactorio_0.2.0.zip
```

### Windows installer

```bat
installer\build.cmd 1.2.3
```

That runs `dist --runtime win-x64` and then ISCC, and writes
`release\FactorioCSharpModder-1.2.3-win64.exe`. On Linux the `.iss` cannot be compiled - Inno Setup
is Windows-only - so the installer is built on Windows (CI uses `windows-latest`).

### Linux / macOS archives

```bash
installer/package.sh 1.2.3 linux-x64
installer/package.sh 1.2.3 osx-arm64
```

Each writes `release/FactorioCSharpModder-1.2.3-<rid>.tar.gz`. Cross-building works: the osx archive
can be produced on Linux, because the tool is published framework-dependent and only the apphost
differs per platform.

## What the installer does

Both installers are the same in substance:

1. copy the tree to a folder - `%LOCALAPPDATA%\Programs\Factorio C# Modder` by default on Windows
   (the user may choose "for all users" and end up in `C:\Program Files`, or pick any folder), and
   `~/.local/lib/factorio-mod` with a launcher in `~/.local/bin` on Linux/macOS (`/usr/local` when
   run as root);
2. put that launcher's folder on `PATH` - asked, on by default, and removed again on uninstall;
3. **ask** whether to add the "Factorio mod (C#)" project template to `dotnet new`, which is the
   list Rider's **New Solution** dialog reads. Yes runs `factorio-mod template`, which is
   `dotnet new install <install>\templates\dotnet\factorio-mod --force`.

Saying no is fine at any point: the installer prints the one command that does it later
(`factorio-mod template`), and nothing else depends on it.

Nothing is registered as a service, no registry keys beyond `PATH`, and uninstalling removes the
template and the `PATH` entry again.

For an unattended install, the question is a switch: `/TEMPLATE` installs it without asking,
`/NOTEMPLATE` never does. A plain `/SILENT` run installs the files and nothing else, because a silent
installer must not open a dialog.

## Verify a release before publishing it

On Windows:

```powershell
.\release\FactorioCSharpModder-1.2.3-win64.exe /SILENT   # or run it and click through
factorio-mod --version                          # 1.2.3, in a NEW terminal (PATH changed)
factorio-mod doctor
factorio-mod new SmokeTest                       # in an empty folder
cd SmokeTest; dotnet build                       # -> build\mods\SmokeTest_0.1.0.zip
dotnet new list factorio-mod                     # shows "Factorio mod (C#)"
```

On Linux/macOS, the same with `tar -xzf`, `./install.sh`, then `factorio-mod --version`.

Then check that Rider lists the template: **File -> New Solution -> More Templates**, look for
*Factorio mod (C#)*. If it is missing, Rider was open while the template was installed - reopen the
dialog, or `File -> New Solution -> Install Template` and point it at the install folder's
`templates/dotnet/factorio-mod`.

## Publish

The release is a normal GitHub release with the three files attached:

```bash
git tag v1.2.3
git push origin v1.2.3
```

and the workflow does the rest. Manually, with the GitHub CLI:

```bash
gh release create v1.2.3 \
    release/FactorioCSharpModder-1.2.3-win64.exe \
    release/FactorioCSharpModder-1.2.3-linux-x64.tar.gz \
    release/FactorioCSharpModder-1.2.3-osx-arm64.tar.gz \
    --title "Factorio C# Modder 1.2.3" --generate-notes
```

## CI

`.github/workflows/release.yml` builds all three assets and publishes them. Two things it needs,
and one it does not:

* nothing needs to be configured - it uses the automatic `GITHUB_TOKEN`; the workflow asks for
  `contents: write`;
* it reuses the repo's own build (`dotnet build`), so a broken build means no release;
* **it does not run the Lua tests** (no Lua 5.2 on the runners). Run `make test` locally before
  tagging if you touched code generation.

To dry-run it: **Actions -> release -> Run workflow**, with the version input. That builds and
uploads the assets to the run (no release is created) so they can be downloaded and tested.

## Troubleshooting

| Symptom | Cause and fix |
| --- | --- |
| `ISCC.exe was not found` | Inno Setup 6 is not installed, or not in `Program Files (x86)`. Install it, or run ISCC by hand with `/DAppVersion=1.2.3 /DPayloadDir=<repo>\dist`. |
| `the CSharp.lua compiler is not built` from `dist` | Run `factorio-mod bootstrap` first. CI does; a fresh clone has not. |
| `dotnet publish` fails on `-r osx-arm64` | Rare, and usually a missing NuGet cache. `dotnet nuget locals all --clear` and retry, or drop the macOS asset for that release. |
| The installer compiles but `factorio-mod` is "not recognized" | The user ticked *Add factorio-mod to PATH* off, or the terminal predates the install. New terminal, or run `%LOCALAPPDATA%\Programs\Factorio C# Modder\bin\factorio-mod.exe`. |
| The template is in the list twice | `dotnet new uninstall <path>` for the stale one; the installer uses `--force` and does not add duplicates. |
| A mod build says *Cannot find the factorio-csharp repository* | An old `<Name>.csproj` that predates the PATH fallback: the project expects a checkout. Re-scaffold with `factorio-mod new`, or add `-p:FactorioToolCommand=factorio-mod`. |
