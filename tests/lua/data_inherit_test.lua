-- The generated data.lua: a definition built on a vanilla prototype copies the fields it did not
-- set, drops the ones it cleared, and leaves no trace of the build's own markers.
--
--   lua5.2 tests/lua/data_inherit_test.lua build/mods/DemoChemistry_0.1.0/data.lua
--
-- The real data stage runs inside Factorio, where data.raw holds the base game. This stub is what
-- that looks like for the two prototypes DemoChemistry builds on.

local path = assert(arg[1], "usage: lua5.2 tests/lua/data_inherit_test.lua <data.lua>")

local extended = {}
local raw = {
  ["assembling-machine"] = {
    ["assembling-machine-1"] = {
      type = "assembling-machine",
      name = "assembling-machine-1",
      collision_box = { { -1.2, -1.2 }, { 1.2, 1.2 } },
      max_health = 300,
      next_upgrade = "assembling-machine-2",
      energy_source = { type = "electric", usage_priority = "secondary-input" },
      graphics_set = { animation = { layers = { { filename = "machine.png" } } } },
      crafting_categories = { "crafting", "basic-crafting" },
    },
  },
  fluid = {
    water = {
      type = "fluid",
      name = "water",
      icon = "__base__/graphics/icons/fluid/water.png",
      default_temperature = 15,
      max_temperature = 100,
      base_color = { r = 0, g = 0.34, b = 0.6 },
      flow_color = { r = 0.7, g = 0.7, b = 0.7 },
      pressure_to_speed_ratio = 0.4,
    },
  },
}

data = {
  raw = raw,
  extend = function(self, prototypes)
    for _, prototype in pairs(prototypes) do
      self.raw[prototype.type] = self.raw[prototype.type] or {}
      self.raw[prototype.type][prototype.name] = prototype
    end
    for _, prototype in pairs(prototypes) do extended[#extended + 1] = prototype end
  end,
}

defines = { direction = { north = 0, east = 4, south = 8, west = 12 } }

local chunk = assert(loadfile(path))
chunk()

assert(#extended == 9, "expected 9 prototypes, got " .. tostring(#extended))

local machine = raw["assembling-machine"]["demo-chemical-assembler"]
assert(machine, "the machine was not registered")

-- Fields the definition set are kept.
assert(machine.crafting_speed == 1.25, "crafting_speed")
assert(machine.energy_usage == "250kW", "energy_usage")
assert(machine.icon == "__DemoChemistry__/graphics/icons/chemical-assembler.png", "icon")
assert(#machine.crafting_categories == 1 and machine.crafting_categories[1] == "chemistry", "crafting_categories")
assert(machine.subgroup == "production-machine", "the machine has no subgroup")

-- The machine draws its own render, scaled to the three tiles it occupies: 192 px at 0.5 is 96 px,
-- which is three tiles of 32.
assert(machine.graphics_set.animation.layers[1].filename == "__DemoChemistry__/graphics/entity/chemical-assembler.png",
  "the machine kept the vanilla graphics instead of its own")
assert(machine.graphics_set.animation.layers[1].scale == 0.5, "the machine sprite is not scaled to 3 tiles")

-- Fields it did not set come from assembling-machine-1.
assert(machine.energy_source.type == "electric", "energy_source was not inherited")
assert(machine.max_health == 300, "max_health was not inherited")
assert(machine.collision_box[2][1] == 1.2, "collision_box was not inherited")

-- ... and they are copies: changing one must not reach the vanilla prototype.
machine.energy_source.usage_priority = "changed"
assert(raw["assembling-machine"]["assembling-machine-1"].energy_source.usage_priority == "secondary-input",
  "an inherited table was shared, not copied")

-- NextUpgrade = null means "not this field", so the vanilla upgrade path is gone.
assert(machine.next_upgrade == nil, "next_upgrade should have been cleared")

-- The build's markers never reach the game.
assert(machine.__inherit == nil, "__inherit leaked into the prototype")
assert(machine.__nil == nil, "__nil leaked into the prototype")

-- A second inherited prototype: the coolant is a recoloured water, so temperatures and pipe
-- behaviour come from the base game and only the colours and the icon are its own.
local fluid = raw["fluid"]["demo-coolant"]
assert(fluid.base_color.b == 0.85, "base_color")
assert(fluid.flow_color.b == 1, "flow_color")
assert(fluid.default_temperature == 15, "the coolant did not inherit water's temperature")
assert(fluid.pressure_to_speed_ratio == 0.4, "the coolant did not inherit water's pipe behaviour")
assert(fluid.icon == "__DemoChemistry__/graphics/icons/demo-coolant.png", "the coolant did not keep its own icon")

-- An item is filed under a tab: without one it lands in "other", which is what the build warns
-- about.
local pellet = raw["item"]["demo-polymer-pellet"]
assert(pellet.subgroup == "intermediate-product", "the pellet has no subgroup")
assert(pellet.icon == "__DemoChemistry__/graphics/icons/polymer-pellet.png", "the pellet icon is not the mod's own")

-- Every recipe that makes something is reachable, and every machine can run the recipes it offers.
-- The coolant is made from water (in a vanilla chemical plant, or in this machine); the resin and the
-- pellet are made in this machine; the machine itself is hand-crafted.
assert(raw["recipe"]["demo-coolant"].category == "chemistry", "the coolant recipe is not in chemistry")
assert(raw["recipe"]["demo-polymer-resin"].category == "chemistry", "the resin recipe is not in chemistry")
assert(raw["item"]["demo-chemical-assembler"].place_result == "demo-chemical-assembler",
  "the machine item does not place the machine")
assert(raw["recipe"]["demo-chemical-assembler"].enabled, "the machine cannot be built, and so is unreachable")

-- The input box takes any fluid, which is what lets the machine run both of its recipes: water in
-- for the coolant, coolant in for the resin. The output is filtered to the one fluid it makes.
assert(machine.fluid_boxes[1].filter == nil, "a filtered input would make one of the machine's recipes unusable")
assert(machine.fluid_boxes[2].filter == "demo-coolant", "the output box is not filtered to the coolant")

-- Every path in the file names its source. A bare "graphics/x.png" is the error the game reports as
-- "not matching the resource path pattern: __source__/path", and it stops the mod from loading.
for line in io.lines(path) do
  for quoted in line:gmatch('"([^"]*%.png)"') do
    assert(quoted:sub(1, 2) == "__",
      "the path '" .. quoted .. "' has no __source__/ prefix, which the game refuses")
  end
end

print("data inherit: PASS")
