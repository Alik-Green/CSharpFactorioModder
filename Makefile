# One tool, a handful of targets. Everything goes through tools/Factorio.ModTool (the
# `factorio-mod` executable); these rules only exist so `make` users do not have to remember the
# dotnet paths.

DOTNET ?= dotnet
TOOL   := $(DOTNET) tools/Factorio.ModTool/bin/Release/net8.0/factorio-mod.dll
BUILD  := $(DOTNET) build tools/Factorio.ModTool -c Release -v quiet --nologo
TAG   ?= 0.0.0-dev
RID   ?= linux-x64
VERSION = $$(python3 -c "import json,sys;print(json.load(open(sys.argv[1]))['version'])" $(1))

.PHONY: all tool setup new bootstrap api generate build assets analyze test util integration clean dist release

all: tool bootstrap api generate test

tool:
	$(BUILD)

# The one-time setup in a single step: pinned compiler, API JSON, generated bindings.
# `make setup ALL=1` does every supported Factorio version (2.0 and 2.1); `make setup FACTORIO=2.1`
# does one. Without either, the default version - what `new` scaffolds - is enough.
setup: tool
	$(TOOL) setup $(if $(ALL),--all,) $(if $(FACTORIO),--factorio $(FACTORIO),)

# Scaffold a new mod and build it once: `make new MOD=AirExtractor`, or `make new MOD=x DIR=~/mods/x`.
# The name can also come from FACTORIO_MOD_NAME and the folder from FACTORIO_MOD_DIR.
new: tool
	$(TOOL) new $(MOD) $(if $(DIR),--dir $(DIR),) $(if $(AUTHOR),--author $(AUTHOR),)

bootstrap: tool
	$(TOOL) bootstrap

# Install the mod template into `dotnet new`, which is what Rider's New Solution dialog reads.
template: tool
	$(TOOL) template

# Build the Rider/IntelliJ plugin from rider-plugin/ into build/plugin/, and a copy in plugin/.
# The first run downloads a JDK and the IntelliJ Platform it compiles against (~700 MB, cached).
plugin: tool
	$(TOOL) plugin

# `make api FACTORIO=2.1` fetches one version's documents; `make api ALL=1` fetches every version.
api: tool
	$(TOOL) fetch-api $(if $(ALL),--all,) $(if $(FACTORIO),--factorio $(FACTORIO),)

generate: tool
	$(TOOL) generate $(if $(ALL),--all,) $(if $(FACTORIO),--factorio $(FACTORIO),)

build: tool
	$(TOOL) build

# Check every mod's C# against the analyzers without building anything: `make analyze`, or
# `make analyze MOD=GasExtractor`. Exits non-zero on an error-severity rule.
analyze: tool
	$(TOOL) analyze $(if $(MOD),mods/$(MOD),)

# Render models to sprites without compiling: `make assets`, or `make assets MOD=GasExtractor`.
assets: tool
	$(TOOL) assets $(if $(MOD),mods/$(MOD),) $(if $(FORCE),--force,)

# 1. tool tests (renamer, data stage, renderer)   2. transpiler tests (the emitted Lua)
# 3. analyzer tests (every rule, both ways round, plus the shipped mods and bindings)
# 4. build the mods                               5. run their control.lua against the Lua mock
# 6. the generated data.lua of the C#-data mod     7. unit-test the Lua utility library
test: tool
	$(DOTNET) test tests/Generator.Tests -c Release -v quiet --nologo
	$(DOTNET) test tests/Transpile.Tests -c Release -v quiet --nologo
	$(DOTNET) test tests/Analyzer.Tests -c Release -v quiet --nologo
	$(TOOL) build mods/HelloFactorio mods/GasExtractor mods/DemoChemistry
	lua5.2 tests/lua/run_mod.lua build/mods/HelloFactorio_$(call VERSION,mods/HelloFactorio/mod.json)/control.lua
	lua5.2 tests/lua/gas_extractor_test.lua build/mods/GasExtractor_$(call VERSION,mods/GasExtractor/mod.json)/control.lua
	lua5.2 tests/lua/demo_chemistry_test.lua build/mods/DemoChemistry_$(call VERSION,mods/DemoChemistry/mod.json)/control.lua
	lua5.2 tests/lua/data_inherit_test.lua build/mods/DemoChemistry_$(call VERSION,mods/DemoChemistry/mod.json)/data.lua
	lua5.2 tests/lua/util_test.lua

integration: tool
	@echo "Needs a real Factorio install; see docs/INTEGRATION.md."
	@echo "  $(TOOL) build mods/GasExtractor --output ~/.factorio/mods"

# Assemble the folder an installer ships: `make dist TAG=1.2.3 RID=win-x64`.
dist: tool
	$(TOOL) dist --output dist --version $(TAG) --runtime $(RID)

# Package that folder for Linux/macOS: `make release TAG=1.2.3 RID=linux-x64` (Windows: installer\build.cmd).
release: tool
	installer/package.sh $(TAG) $(RID)

clean:
	rm -rf build
