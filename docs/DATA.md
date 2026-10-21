# The data stage in C#

A Factorio mod has two stages. The **data stage** declares prototypes - items, fluids, machines,
recipes - and runs once when the game loads mods. The **runtime stage** reacts to events and runs in
the game. `factorio-mod` has always compiled the runtime stage from C#; a data stage can now be C#
too, written as definitions instead of prototype tables.

The whole API is in `Factorio.Data` (`src/Factorio.Bindings/Data/`), and `mods/DemoChemistry` is a
complete example of it.

## A mod's prototypes

```csharp
public sealed class ExampleMod : ModDefinition
{
    public override void Define(ModBuilder mod)
    {
        mod.Add(new DemoCoolant());
        mod.Add(new PolymerPellet());
        mod.Add(new ChemicalAssemblerItem());
        mod.Add(new ChemicalAssembler());
        mod.Add(new PolymerRecipe());
        mod.Add(new ChemicalAssemblerRecipe());
    }
}
```

Each definition is a class: the prototype name goes to the base constructor, and the properties
assigned in the constructor are the prototype's fields.

```csharp
public sealed class DemoCoolant : FluidDefinition
{
    public DemoCoolant() : base("demo-coolant", Vanilla.FluidOf("water"))
    {
        Icon = Asset.Mod("graphics/icons/demo-coolant.png");
        IconSize = 64;
        BaseColor = new Rgb(0.08, 0.40, 0.85);
        FlowColor = new Rgb(0.15, 0.70, 1.00);
        LocalisedName = "Demo coolant";
    }
}

public sealed class PolymerPellet : ItemDefinition
{
    public PolymerPellet() : base("demo-polymer-pellet")
    {
        Icon = Asset.Mod("graphics/icons/polymer-pellet.png");
        IconSize = 64;
        StackSize = 100;
        Subgroup = Subgroup.IntermediateProduct;          // the tab it appears in
        Order = SortOrder.At("z", "demo-polymer-pellet"); // where in that tab
        LocalisedName = "Polymer pellet";
    }
}

public sealed class ChemicalAssembler : AssemblingMachineDefinition
{
    public ChemicalAssembler() : base("demo-chemical-assembler", Vanilla.AssemblingMachine1)
    {
        Icon = Asset.Mod("graphics/icons/chemical-assembler.png");
        IconSize = 64;
        Subgroup = Subgroup.ProductionMachine;
        Order = SortOrder.At("z", "demo-chemical-assembler");

        // A 3x3 machine is 96 px across, so a 192 px render is drawn at 0.5.
        Graphics = GraphicsSet.Of(Sprite.Scaled(Asset.Mod("graphics/entity/chemical-assembler.png"), 192, 192, 0.5));

        CraftingCategories = [CraftingCategory.Chemistry];
        CraftingSpeed = 1.25;
        EnergyUsage = Energy.Kilowatts(250);
        MinableResult = ItemRef.Of("demo-chemical-assembler");
        NextUpgrade = null;

        // Unfiltered: water comes in for the coolant recipe, coolant comes in for the resin one.
        // The output is filtered, because the only fluid the machine makes is the coolant.
        FluidBoxes =
        [
            FluidBox.Input(filter: null, volume: 100, ports: [PipePort.North(0, -1)]),
            FluidBox.Output(filter: FluidRef.Of("demo-coolant"), volume: 100, ports: [PipePort.South(0, 1)]),
        ];

        LocalisedName = "Chemical assembler";
        LocalisedDescription = "Runs chemistry recipes, and pipes the coolant itself.";
    }
}

public sealed class PolymerResinRecipe : RecipeDefinition
{
    public PolymerResinRecipe() : base("demo-polymer-resin")
    {
        Categories = [CraftingCategory.Chemistry];
        Enabled = true;
        EnergyRequired = 2;
        Order = SortOrder.At("z", "demo-polymer-resin");
        Ingredients = [Ingredient.Item("iron-plate", 2), Ingredient.Fluid("demo-coolant", 25)];
        Results = [Product.Item("demo-polymer-resin", 2)];
        LocalisedName = "Polymer resin";
    }
}
```

`mod.json` points at the entry class and at the folder holding the definitions:

```jsonc
{
  "entry": "DemoChemistry.Control.Init",          // runtime stage, compiled to control.lua
  "dataEntry": "DemoChemistry.ExampleMod",        // data stage, run at build time
  "sources": ["src"],                             // *.cs for control.lua
  "dataSources": ["data"]                         // *.cs for data.lua
}
```

A data-stage file must not sit under a runtime source directory: it is build-time .NET code, and
compiling it into `control.lua` could not work. The build rejects the overlap by name.

## How it runs

The data stage is **not** transpiled. `factorio-mod build` compiles `dataSources` with Roslyn into
an in-memory assembly, runs `Define`, and writes what it collected as `data.lua`:

```lua
local prototypes =
{
  {
    type = "fluid",
    name = "demo-coolant",
    icon = "__base__/graphics/icons/fluid/water.png",
    ...
```

So `data/` is ordinary .NET: LINQ, `List<T>`, loops, helper methods, generics, exceptions. None of
it ships - the strings, numbers and tables it computes do. The generated `data.lua` then goes
through exactly the same validation a hand-written one does (see the README's data-stage section),
against `prototype-api.json` for the target version.

Whatever produced the table, the same checks run over it - including the ones the game is silent
about: a machine item with no recipe and a disabled recipe with no technology are reported as
warnings, because both load fine and both are invisible to the player.

That split is the point: the runtime stage is limited to what CSharp.lua can lower and Factorio can
run (no arrays, no `System`, no exceptions), while the data stage is a small program that runs at
build time and only has to produce a correct table.

## What the API models

| type | what it declares |
| --- | --- |
| `ModDefinition` | the entry class: `Define(ModBuilder)` adds every definition |
| `ModBuilder` | collects definitions, in order |
| `PrototypeDefinition` | `Type`, `Name`, `Icon`, `IconSize`, `Hidden`, `LocalisedName`, `Order`, `Subgroup` |
| `FluidDefinition` | `default_temperature`, `max_temperature`, `base_color`, `flow_color`, `heat_capacity`, `fuel_value` |
| `ItemDefinition` | `stack_size`, `place_result`, `flags`, `fuel_value` |
| `EntityDefinition` | `minable` result and time, `max_health`, `flags`, `collision_box`, `selection_box`, `next_upgrade` |
| `AssemblingMachineDefinition` | `crafting_categories`, `crafting_speed`, `energy_usage`, `energy_source`, `fluid_boxes`, `graphics_set`, `Electric()`, `Burner()` |
| `RecipeDefinition` | `categories`, `enabled`, `energy_required`, `ingredients`, `results`, `main_product`, `allow_productivity` |
| `RecipeCategoryDefinition` | a category the mod invents |
| `ItemGroupDefinition` / `ItemSubgroupDefinition` | a tab in the crafting menu and a section inside it |

Values: `Asset.Base(...)`/`Asset.Mod(...)`/`Asset.Generated(...)`, `Energy.Kilowatts(...)` and
friends, `Rgb`, `Bounds.Of(...)`, `PipePort.North(...)`, `FluidBox.Input(...)`,
`Ingredient.Item/Fluid`, `Product.Item/Fluid`, `ItemRef`/`EntityRef`/`FluidRef`/`RecipeRef`,
`Sprite.File`/`Sprite.Scaled`/`Sprite.Shift` and `GraphicsSet.Of(...)`, and `Vanilla.*` for a base-game prototype to build on.
Arrays are `Values`, which is a `CollectionBuilder` - so `[CraftingCategory.Chemistry]` and
`Values.Of(...)` both work, and `Values.From(sequence)` takes anything enumerable.

### Names: enums where the game fixes the set, strings where it does not

Some of the strings a prototype takes are a closed set the game defines, and some name a prototype
that a mod may invent. The API splits them accordingly:

| what | how it is written | why |
| --- | --- | --- |
| `flags` | `EntityFlag` / `ItemFlag` - a `[Flags]` enum | the game defines the set; nothing else is legal |
| `subgroup` | `Subgroup.ProductionMachine`, `Subgroup.Of("my-tab")`, or `"my-tab"` | base-game tabs are a fixed set, a mod may declare more |
| `group` | `ItemGroup.Production`, `ItemGroup.Of(...)` or a string | same |
| `categories` / `crafting_categories` | `CraftingCategory.Chemistry`, `CraftingCategory.Of(...)` or a string | same |
| `order` | `SortOrder.At("b", "name")` or `SortOrder.Of("anything")` | the game compares strings; the content is the mod's |

Each of `Subgroup`, `ItemGroup`, `CraftingCategory` and `SortOrder` is a string with a type: it
converts implicitly, so a name the API does not list is no harder to write than one it does, and the
build checks both kinds against what actually exists. The flag enums are written as the arrays of
names the prototype API wants (`ItemFlag.NotStackable | ItemFlag.OnlyInCursor` becomes
`{ "not-stackable", "only-in-cursor" }`).

Anything the API does not model is one call away:

```csharp
Field("emissions_per_minute", new Values { ... });   // any prototype key, written as given
```

`Field` is the documented escape hatch, not a workaround: the typed properties exist so the common
case reads well and is checked, and every prototype has hundreds of keys no library will ever
enumerate one by one.

## Names, tabs and localisation

Three things decide whether a prototype is *findable* in the game, and all three fail silently:

1. **the tab**: `Subgroup`. An item with no subgroup is filed under the base game's "Other" tab. The
   build warns about it, and about a subgroup or group name that no prototype declares (that one is
   fatal in the game: `Error in assignID: item-subgroup with name '...' does not exist`).
2. **the order**: `Order`, written with `SortOrder.At("z", "name")`. Optional; without it the
   prototype sorts by name.
3. **the name**: `LocalisedName`. The game never reads a prototype's name from the data stage - it
   looks up `item-name.<name>`, `recipe-name.<name>`, `entity-name.<name>` in the mod's locale - so a
   missing entry shows `Unknown key: item-name.demo-polymer-pellet` wherever the player looks.

Rather than have the name live in one file and the prototype in another, a definition declares it:

```csharp
LocalisedName = "Polymer pellet";
LocalisedDescription = "A small pellet of polymer, ready to be pressed into something.";
```

The build collects those and merges them into the packaged `locale/en/locale.cfg`, keeping whatever
the author wrote there (other languages, or strings that are not a prototype's own name). It warns
about any visible prototype left without a name, naming the section and the key to add.

## Graphics

`Graphics` is the machine's `graphics_set`:

```csharp
Graphics = GraphicsSet.Of(Sprite.Scaled(Asset.Mod("graphics/entity/chemical-assembler.png"), 192, 192, 0.5));
```

A tile is 32 pixels, so a 3x3 machine is 96 pixels across and a 192-pixel render is drawn at
`scale = 0.5` - get that wrong and the machine overlaps its neighbours. `GraphicsSet.Of(sprite)` is
the one-picture form; for four directions, pass four sprites. A machine built on a vanilla prototype
inherits its graphics and needs none of this.

`Asset.Mod(...)` points into the mod's own `resources/`, which the build copies to the mod root, and
writes the `__<mod name>__/` prefix the game demands: `Asset.Mod("graphics/icons/x.png")` in
`DemoChemistry` becomes `__DemoChemistry__/graphics/icons/x.png`. `Asset.Base(...)` reuses the base
game's art; `Asset.Generated("name")` splices `require("generated.name")` for a sprite the asset
pipeline rendered (those tables are written with the same prefix).

### Resource paths need a source

Factorio does not resolve a bare path against the mod that declares it. Every `icon`, every sprite
`filename` and every sound has to name where the file lives, or the game stops at load with

```
Path graphics/icons/polymer-pellet.png not matching the resource path pattern: __source__/path
```

and disables the mod. That is why `Asset.Mod` prefixes instead of returning the path unchanged, and
why the build checks every resource path a prototype contains - an unprefixed one is a build error
that quotes the game's message and the path it should have:

```
data.lua:0: item demo-pellet: icon is 'graphics/icons/polymer-pellet.png', which has no source
prefix. Factorio stops with "Path graphics/icons/polymer-pellet.png not matching the resource path
pattern: __source__/path". Write Asset.Mod("graphics/icons/polymer-pellet.png") for a file in this
mod (it becomes '__DemoChemistry__/graphics/icons/polymer-pellet.png') or
Asset.Base("graphics/icons/polymer-pellet.png") for a base-game one
```

The prefix a mod uses is its `info.json` name, not its folder name: the packaging renames the folder
to `Name_version`, and `__Name__` still resolves. `mods/DemoChemistry/CREDITS.md`
records where its borrowed art came from, because most free Factorio graphics - including
[snouz's catalogue](https://github.com/snouz/factorio_free_graphics_for_modders) - are MIT or CC BY
and both require the credit to travel with the work.

## Inheritance and `null`

`new AssemblingMachineDefinition("demo-chemical-assembler", Vanilla.AssemblingMachine1)` copies
every field the definition does not set from the vanilla prototype. The copy happens in the data
stage, in the game, because that is the only place the vanilla prototype exists - the generated Lua
contains this loop:

```lua
local source = data.raw[inherit.type] and data.raw[inherit.type][inherit.name]
for key, value in pairs(source) do
  if prototype[key] == nil and not cleared[key] then prototype[key] = deepcopy(value) end
end
```

* the copy is **deep**, so editing the new machine's `graphics_set` cannot reach the base game's;
* fields assigned in C# win over inherited ones;
* a field assigned `null` is listed in `__nil` and **not** inherited - `NextUpgrade = null` is how a
  machine opts out of the vanilla upgrade path, where simply not mentioning it would inherit
  `assembling-machine-2`;
* `__inherit` and `__nil` are removed before `data:extend`, so the game never sees them;
* a prototype that inherits is exempt from the "required property is missing" check, because the
  build cannot see the game's prototypes. Its own fields are still checked.

Setting a property to `null` on a definition that does *not* inherit writes nothing: absent is the
normal case, and the game's default applies.

## Version differences

`RecipeDefinition.Categories` is written the way the target version reads it:
`category` + `additional_categories` for 2.0, and `categories` for 2.1. A 2.0 mod that wrote
`categories` would load with a recipe no machine can craft, which is a build error here instead.

## Everything has to be obtainable

A prototype that loads but cannot be obtained is invisible, and the game says nothing about it. The
build therefore checks the whole chain:

* an item with a `place_result` that no recipe produces is warned about (the machine is simply not
  in the crafting menu);
* a recipe with `enabled = false` that no technology unlocks is warned about;
* a fluid, item or recipe with no name is warned about;
* an item with no `Subgroup` is warned about.

`DemoChemistry` exercises all of it: the coolant is made from water (in a vanilla chemical plant, or
in the demo machine itself), the resin and the pellet in the demo machine, and the machine from a
hand-craftable recipe. Nothing in the chain needs a cheat or another mod.

That is also a constraint on the definitions rather than on the checks: a machine whose input fluid
box is filtered to `demo-coolant` cannot run a recipe that needs water, so the filter would have to
leave the coolant recipe permanently unusable. The demo machine's input is therefore unfiltered and
its *output* is filtered, which is where the filter does what it is for.

## Where the build's settings live

`mod.json` carries a `build` object, so the folder - not a remembered command - says how the mod is
built. The command line still wins, which is what makes a one-off `--no-zip` possible:

```jsonc
"build": {
  "output": "out",        // where the folder and zip go; relative to the mod
  "zip": true,            // false: keep the folder, skip the zip
  "analyze": true,        // false: skip the analyzers
  "assets": true,         // false: skip assets/render.json
  "validate": true,       // false: skip the Lua 5.2 and symbol checks
  "lua51": false,         // true: emit Lua 5.1 syntax
  "regenerate": false,    // true: regenerate the bindings first
  "format": "text"        // or "msbuild": findings as `file(line,col): error FACTORIO: ...`
}
```

This is what lets an IDE build a mod with a single, fixed command: the generated `.csproj` runs
`factorio-mod build <folder>` and nothing else. See [`RIDER.md`](RIDER.md).

## Deviations from the ideal, and why

The API is meant to read like the snippet everyone writes by hand. Where the language or the
prototype schema forced something, it is listed here:

1. **`Vanilla.AssemblingMachine1` is a property, not a static class.** `Vanilla` is static and its
   members are `VanillaPrototype` values; the constructor argument type is `VanillaPrototype`. This
   keeps `base("name", Vanilla.X)` and `base("name", Vanilla.Of("assembling-machine", "x"))` the
   same call.
2. **`MinableResult` implies `mining_time`.** Setting only the result writes `mining_time = 0.5`
   if `MiningTime` was not set: the game rejects a `minable` table without one, and inheritance
   copies top-level fields only, so it cannot fill in half a table. Set `MiningTime` to say
   otherwise.
3. **`EnergyUsage` is a string.** `energy_usage` is `"250kW"`, not `250`: `Energy.Kilowatts(250)`
   makes the unit explicit, and getting it wrong is a factor-of-a-thousand bug, not a typo.
4. **`Painting` an icon is a path.** `Icon` is a string or a sprite table, not an image type: a
   plain path is what the prototype takes (with `IconSize`), and `Asset.Generated(...)` splices
   `require("generated.…")` for a sprite the asset pipeline rendered.
5. **`Field(name, value)` for the long tail.** No typed property for a prototype key means
   `Field("key", value)`; the key is written as given and validated by the same prototype check.
6. **`Values` rather than `List<T>`.** Prototype arrays are `Values` (a collection-builder type), so
   collection expressions work in the constructors. `Values.From(query)` adapts any sequence.
7. **No `base` call for a plain recipe/item.** `RecipeDefinition(string name)` and
   `ItemDefinition(string name)` take the name directly; only the machine types have the second
   constructor, because only they can inherit.
