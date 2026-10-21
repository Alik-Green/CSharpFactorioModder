-- Data stage: the machines, their items, one recipe per gas, the machine recipes and the research
-- that unlocks the second tier.
--
-- Written in Lua on purpose. The prototype API is generated as C# (`Factorio.Prototypes`), but the
-- runtime behaviour - the part a mod actually has to get right - is C# in `src/`. The sibling
-- `mods/DemoChemistry` shows the same data stage written as C# definitions, for comparison.
--
-- The sprites are not drawn by hand: `assets/render.json` names a 3D model and
-- `factorio-mod build` renders it to PNGs plus these Lua tables before the mod is packaged.
-- `require` is available in the data stage and resolves against the packaged mod folder, where
-- `lua/` has been flattened away, so the path is `generated.<file>`.
--
-- `factorio-mod build` type-checks this file against the official prototype API: every key below
-- is a real property of the class named by `type`, and every required property is present. It also
-- warns about prototypes that load but can never be obtained - an item with no recipe, or a recipe
-- no technology unlocks.

local sprite = require("generated.gas-extractor-sprite")
local icon = require("generated.gas-extractor-icon")
local sprite2 = require("generated.gas-extractor-sprite-mk2")
local icon2 = require("generated.gas-extractor-icon-mk2")

local GASES = {
  { name = "petroleum-gas", order = "a" },
  { name = "light-oil",     order = "b" },
  { name = "heavy-oil",     order = "c" },
}

local FLUID_PER_CRAFT = 20
local SECONDS_PER_CRAFT = 2

-- A machine may not invent a crafting category: the name used by `crafting_categories` and by
-- each recipe has to be a recipe-category prototype, or the game stops with
-- "recipe-category with name ... does not exist".
local category = {
  type = "recipe-category",
  name = "gas-extraction",
}

-- Both tiers share the same fluid box and the same `fast_replaceable_group`, so an upgraded
-- machine can be dropped straight onto an old one: the game swaps the entity and keeps the recipe,
-- which for this mod *is* the gas the machine extracts.
local function fluid_boxes()
  return {
    {
      production_type = "output",
      volume = 200,
      -- A pipe connection has to sit *inside* the collision box - the game shrinks the box by
      -- 0.001 and rejects anything outside it, so a connection on the 1.5 tile edge of a 3x3
      -- machine fails to load. One tile out from the centre is inside the box and still reads as
      -- a pipe at the edge of the machine.
      pipe_connections = {
        { flow_direction = "output", direction = defines.direction.north, position = { 0, -1 } },
        { flow_direction = "output", direction = defines.direction.south, position = { 0, 1 } },
      },
    },
  }
end

local machine = {
  type = "assembling-machine",
  name = "gas-extractor",
  icon = icon.filename,
  icon_size = icon.width,
  flags = { "placeable-neutral", "placeable-player", "player-creation" },
  minable = { mining_time = 1, result = "gas-extractor" },
  max_health = 350,
  collision_box = { { -1.2, -1.2 }, { 1.2, 1.2 } },
  selection_box = { { -1.5, -1.5 }, { 1.5, 1.5 } },
  crafting_categories = { "gas-extraction" },
  crafting_speed = 1,
  energy_usage = "150kW",
  energy_source = {
    type = "electric",
    usage_priority = "secondary-input",
    emissions_per_minute = { pollution = 2 },
  },
  graphics_set = {
    animation = {
      layers = { sprite.north },
    },
  },
  fluid_boxes = fluid_boxes(),
  fast_replaceable_group = "gas-extractor",
}

local machine2 = {
  type = "assembling-machine",
  name = "gas-extractor-2",
  icon = icon2.filename,
  icon_size = icon2.width,
  flags = { "placeable-neutral", "placeable-player", "player-creation" },
  minable = { mining_time = 1, result = "gas-extractor-2" },
  max_health = 450,
  collision_box = { { -1.2, -1.2 }, { 1.2, 1.2 } },
  selection_box = { { -1.5, -1.5 }, { 1.5, 1.5 } },
  crafting_categories = { "gas-extraction" },
  crafting_speed = 2,
  energy_usage = "300kW",
  energy_source = {
    type = "electric",
    usage_priority = "secondary-input",
    emissions_per_minute = { pollution = 3 },
  },
  graphics_set = {
    animation = {
      layers = { sprite2.north },
    },
  },
  fluid_boxes = fluid_boxes(),
  fast_replaceable_group = "gas-extractor",
}

-- `subgroup` is what puts an item on a tab of the crafting menu: without one it lands in "other",
-- which is where machines go to be lost. `extraction-machine` is the base game's subgroup for
-- drills and pumps.
local item = {
  type = "item",
  name = "gas-extractor",
  icon = icon.filename,
  icon_size = icon.width,
  subgroup = "extraction-machine",
  order = "b[gas-extractor]",
  place_result = "gas-extractor",
  stack_size = 20,
}

local item2 = {
  type = "item",
  name = "gas-extractor-2",
  icon = icon2.filename,
  icon_size = icon2.width,
  subgroup = "extraction-machine",
  order = "b[gas-extractor-2]",
  place_result = "gas-extractor-2",
  stack_size = 20,
}

-- The machines themselves are hand-craftable from the start. An item with a `place_result` and no
-- recipe is the one prototype mistake the game does not report: it loads, and then the machine is
-- simply not in the crafting menu.
local machine_recipe = {
  type = "recipe",
  name = "gas-extractor",
  category = "crafting",
  energy_required = 4,
  ingredients = {
    { type = "item", name = "iron-gear-wheel", amount = 10 },
    { type = "item", name = "electronic-circuit", amount = 10 },
    { type = "item", name = "pipe", amount = 10 },
    { type = "item", name = "steel-plate", amount = 5 },
  },
  results = { { type = "item", name = "gas-extractor", amount = 1 } },
  enabled = true,
  order = "b[gas-extractor]",
}

-- The second tier is researched, so its recipe starts disabled - `enabled = false` plus the
-- technology below, or the recipe would sit in the crafting menu from the first minute.
local machine2_recipe = {
  type = "recipe",
  name = "gas-extractor-2",
  category = "crafting",
  energy_required = 8,
  ingredients = {
    { type = "item", name = "gas-extractor", amount = 1 },
    { type = "item", name = "processing-unit", amount = 5 },
    { type = "item", name = "electric-engine-unit", amount = 5 },
    { type = "item", name = "steel-plate", amount = 20 },
  },
  results = { { type = "item", name = "gas-extractor-2", amount = 1 } },
  enabled = false,
  order = "b[gas-extractor-2]",
}

-- A trigger technology needs no science packs: it completes when the player has built enough of the
-- first tier. Cheap to play with, and it shows the effect shape a normal research would use.
local technology = {
  type = "technology",
  name = "gas-extraction",
  icon = icon2.filename,
  icon_size = icon2.width,
  effects = {
    { type = "unlock-recipe", recipe = "gas-extractor-2" },
  },
  research_trigger = {
    type = "craft-item",
    item = "gas-extractor",
    count = 10,
  },
  order = "c-a",
}

local prototypes = { category, machine, machine2, item, item2, machine_recipe, machine2_recipe, technology }

-- One recipe per gas. No ingredients: the machine pulls the gas out of the air. The recipe is
-- also the machine's setting - the player picks a gas in the machine's GUI, and `Control.cs`
-- reads it back with `entity.get_recipe()`.
for _, gas in pairs(GASES) do
  prototypes[#prototypes + 1] = {
    type = "recipe",
    name = "gas-extractor-" .. gas.name,
    -- Factorio 2.0 has one category per recipe; 2.1 added `categories` (a list).
    category = "gas-extraction",
    energy_required = SECONDS_PER_CRAFT,
    ingredients = {},
    results = { { type = "fluid", name = gas.name, amount = FLUID_PER_CRAFT } },
    icon = icon.filename,
    icon_size = icon.width,
    order = "g[gas-extractor]-" .. gas.order .. "[" .. gas.name .. "]",
    enabled = true,
    hidden = false,
    hide_from_player_crafting = true,
  }
end

data:extend(prototypes)
