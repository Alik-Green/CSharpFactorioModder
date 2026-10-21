-- Unit tests for src/Factorio.Bindings/FactorioUtils.lua, the Lua half of the utility bindings.
--
--   lua5.2 tests/lua/util_test.lua
--
-- The library is plain Lua 5.2 (no Factorio API at all), so it runs here directly - no mock.

local util = assert(dofile("src/Factorio.Bindings/FactorioUtils.lua"))

local failures = {}
local function check(cond, msg)
  if not cond then failures[#failures + 1] = msg end
end

local function equal(actual, expected, what)
  if actual ~= expected then
    failures[#failures + 1] = what .. ": expected " .. tostring(expected) .. ", got " .. tostring(actual)
  end
end

-- ------------------------------------------------------------------ util.valid
equal(util.valid(nil), false, "valid(nil)")
equal(util.valid({ valid = true }), true, "valid(valid object)")
equal(util.valid({ valid = false }), false, "valid(invalid object)")
equal(util.valid({}), true, "valid(object without a valid field)")

-- --------------------------------------------------------------------- util.is
equal(util.is(nil, "iron-chest"), false, "is(nil)")
equal(util.is({ name = "iron-chest" }, "iron-chest"), true, "is(name matches)")
equal(util.is({ name = "iron-chest" }, "steel-chest"), false, "is(name differs)")
equal(util.is({ name = "iron-chest", valid = false }, "iron-chest"), false, "is(invalid object)")

-- ----------------------------------------------------------- util.ensure_table
do
  local storage = {}
  local first = util.ensure_table(storage, "extractors")
  check(type(first) == "table", "ensure_table did not create a table")
  equal(first, storage.extractors, "ensure_table did not store the table under the key")

  local second = util.ensure_table(storage, "extractors")
  equal(first, second, "ensure_table replaced an existing table")

  -- An empty table is truthy in Lua, but a nil-valued key is not: both must be created.
  storage.empty = false
  local third = util.ensure_table(storage, "empty")
  check(type(third) == "table", "ensure_table kept a false value instead of a table")

  equal(util.ensure_table(storage, 7), storage[7], "ensure_table did not accept a numeric key")
end

-- ------------------------------------------------------------------- util.each
do
  local seen = {}
  util.each({ a = 1, b = 2 }, function(key, value) seen[key] = value end)
  equal(seen.a, 1, "each(a)")
  equal(seen.b, 2, "each(b)")

  local count = 0
  util.each({}, function() count = count + 1 end)
  equal(count, 0, "each on an empty table")
end

-- --------------------------------------------------------------- util.each_array
do
  local sum = 0
  local indexes = {}
  util.each_array({ 10, 20, 30 }, function(value) sum = sum + value end)
  equal(sum, 60, "each_array sum")

  -- ipairs stops at the first hole; the array part is all that counts.
  util.each_array({ 1, 2, nil, 4 }, function(value) indexes[#indexes + 1] = value end)
  equal(#indexes, 2, "each_array walked past a hole")

  local empty = 0
  util.each_array({}, function() empty = empty + 1 end)
  equal(empty, 0, "each_array on an empty table")
end

-- -------------------------------------------------------------- util.each_sorted
do
  -- String keys come out in ascending order regardless of hash order, which is what makes log
  -- output and test expectations reproducible.
  local order = {}
  util.each_sorted({ c = 3, a = 1, b = 2 }, function(key) order[#order + 1] = key end)
  equal(table.concat(order, ","), "a,b,c", "each_sorted with string keys")

  order = {}
  util.each_sorted({ [10] = "x", [2] = "y", [1] = "z" }, function(key) order[#order + 1] = key end)
  equal(table.concat(order, ","), "1,2,10", "each_sorted with numeric keys")

  local total = 0
  util.each_sorted({ b = 2, a = 1 }, function(_, value) total = total + value end)
  equal(total, 3, "each_sorted passes values")
end

-- ------------------------------------------------------------------ util.count
do
  equal(util.count({}), 0, "count(empty)")
  equal(util.count({ a = 1, b = 2 }), 2, "count(map)")
  equal(util.count({ 1, 2, 3 }), 3, "count(array)")
  equal(util.count({ [1] = "a", [1000] = "b" }), 2, "count(sparse) - the # operator would say 1")
end

-- ------------------------------------------------------------------- util.copy
do
  local source = { a = 1, b = 2 }
  local copy = util.copy(source)
  equal(copy.a, 1, "copy(a)")
  copy.a = 99
  equal(source.a, 1, "copy shares no entries with the source")
  check(copy ~= source, "copy returned the same table")
end

-- ------------------------------------------------------------------- util.join
do
  equal(util.join({ "a", "b", "c" }, ","), "a,b,c", "join with a separator")
  equal(util.join({}, ","), "", "join an empty list")
  equal(util.join({ 1, 2, 3 }, "-"), "1-2-3", "join converts numbers")
end

-- ------------------------------------------------------------------ util.get/set/unset
do
  local target = { name = "iron-chest" }
  equal(util.get(target, "name"), "iron-chest", "get reads a field")
  equal(util.get(target, "missing"), nil, "get on a missing field")
  equal(util.get(nil, "name"), nil, "get on nothing") -- not an error, unlike raw indexing

  util.set(target, "health", 10)
  equal(target.health, 10, "set writes a field")
  util.unset(target, "health")
  equal(target.health, nil, "unset removes a field")
  util.unset(target, "never-there") -- must not raise
  util.unset({}, "x")
end

-- ------------------------------------------------------------------------ util.call
do
  local calls = {}
  local target = {
    add = function(a, b) return a + b end,
    log = function(text) calls[#calls + 1] = text end,
  }

  equal(util.call(target, "add", 2, 3), 5, "call with two arguments")
  util.call(target, "log", "hello")
  equal(calls[1], "hello", "call forwards its argument")
  -- Calling a name that is not a function is a bug in the caller (a typo in a Lua method name),
  -- so it must raise rather than quietly return nil.
  local ok = pcall(function() util.call(target, "missing") end)
  equal(ok, false, "calling a missing method raises")
end

-- ----------------------------------------------------------------------- util.table
do
  local one = util.table("type", "item")
  equal(one.type, "item", "table with one pair")

  local two = util.table("type", "item", "name", "x")
  equal(two.name, "x", "table with two pairs")

  local skipped = util.table(nil, nil, "name", "y")
  equal(skipped.type, nil, "an absent key is not written")
  equal(skipped.name, "y", "later pairs still land")

  local three = util.table("a", 1, "b", 2, "c", 3)
  equal(three.a + three.b + three.c, 6, "table with three pairs")
end

-- ------------------------------------------------------------------ storage slots
do
  local storage = {}
  util.slot_set(storage, "machines", "1", { name = "a" })
  util.slot_set(storage, "machines", "2", { name = "b" })

  equal(type(storage.machines), "table", "slot_set created the section")
  equal(util.slot_get(storage, "machines", "1").name, "a", "slot_get")
  equal(util.slot_get(storage, "machines", "nope"), nil, "slot_get on a missing key")
  equal(util.slot_get(storage, "never-created", "1"), nil, "slot_get on a missing section")
  equal(util.slot_has(storage, "machines", "2"), true, "slot_has finds an entry")
  equal(util.slot_has(storage, "machines", "3"), false, "slot_has on a missing entry")
  equal(util.slot_count(storage, "machines"), 2, "slot_count")
  equal(util.slot_count(storage, "never-created"), 0, "slot_count on a missing section")

  util.slot_remove(storage, "machines", "1")
  equal(util.slot_get(storage, "machines", "1"), nil, "slot_remove")
  util.slot_remove(storage, "never-created", "1") -- must not raise

  util.slot_clear(storage, "machines")
  equal(storage.machines, nil, "slot_clear drops the whole section")
  util.slot_clear(storage, "never-created")
end

-- ------------------------------------------------------------------- registries
-- A registry is one section of entities keyed by unit number plus one section per typed value,
-- and it must clean up after a machine that stops existing.
do
  local storage = {}
  local field = util.registry_value("reg.machines", "gas")
  equal(field, "reg.machines/gas", "registry_value names the value section after its registry")

  local function machine(unit, valid)
    return { valid = valid ~= false, unit_number = unit, name = "gas-extractor" }
  end

  local a, b = machine(1), machine(2)
  util.registry_add(storage, "reg.machines", a)
  util.registry_add(storage, "reg.machines", b)
  equal(storage["reg.machines"]["1"], a, "registry_add keys by unit number")
  equal(util.registry_count(storage, "reg.machines"), 2, "registry_count")
  equal(util.registry_has(storage, "reg.machines", a), true, "registry_has")
  equal(util.registry_has(storage, "reg.machines", machine(9)), false, "registry_has on an unknown unit")

  util.value_set(storage, field, a, "light-oil")
  equal(util.value_get(storage, field, a), "light-oil", "value_set/value_get")
  equal(util.value_has(storage, field, b), false, "value_has before any write")
  equal(util.value_get(storage, field, b), nil, "value_get without a value")

  -- Entities without a unit number cannot be keyed and are ignored rather than crashing.
  util.registry_add(storage, "reg.machines", { valid = true, name = "character" })
  util.value_set(storage, field, { valid = true, name = "character" }, "x")
  equal(util.registry_count(storage, "reg.machines"), 2, "an entity without a unit number was added")

  -- Iteration visits the live machines and drops the dead ones, values included.
  b.valid = false
  local seen = {}
  util.registry_each(storage, "reg.machines", function(entity) seen[#seen + 1] = entity end)
  equal(#seen, 1, "registry_each visits only live machines")
  equal(storage["reg.machines"]["2"], nil, "registry_each drops a dead machine")
  equal(util.value_get(storage, field, b), nil, "registry_each drops a dead machine's values")

  -- A machine can be removed explicitly, which also drops its values.
  util.registry_remove(storage, "reg.machines", a)
  equal(util.value_get(storage, field, a), nil, "registry_remove drops the values")
  util.registry_remove(storage, "reg.machines", machine(42)) -- must not raise

  -- Clearing forgets the section and everything under it.
  util.registry_add(storage, "reg.machines", machine(3))
  util.value_set(storage, field, machine(3), "heavy-oil")
  util.registry_clear(storage, "reg.machines")
  equal(storage["reg.machines"], nil, "registry_clear drops the entity section")
  equal(storage[field], nil, "registry_clear drops the value section")
  util.registry_clear(storage, "never-created")
end

-- ---------------------------------------------------------------------- settings
do
  local storage = {}
  equal(util.setting_get(storage, "preferred"), nil, "setting_get before any write")
  equal(util.setting_has(storage, "preferred"), false, "setting_has before any write")
  util.setting_set(storage, "preferred", "light-oil")
  equal(util.setting_get(storage, "preferred"), "light-oil", "setting_set/get")
  equal(util.setting_has(storage, "preferred"), true, "setting_has after a write")
  util.setting_clear(storage, "preferred")
  equal(util.setting_has(storage, "preferred"), false, "setting_clear")
end

-- ------------------------------------------------------------------- lifecycle
-- The lifecycle helpers subscribe to every event that can produce the same situation, so mod code
-- cannot accidentally handle only half of them.
do
  local subscribed = {}
  local nth = {}
  local init = nil
  local events = { on_built_entity = 1, on_robot_built_entity = 2, script_raised_built = 3,
                   on_space_platform_built_entity = 4, on_player_mined_entity = 5,
                   on_robot_mined_entity = 6, on_space_platform_mined_entity = 7,
                   on_entity_died = 8, script_raised_destroy = 9 }

  -- The helpers resolve these three globals at call time, so a stub environment is enough.
  _G.script = {
    on_event = function(event, handler) subscribed[event] = handler end,
    on_nth_tick = function(ticks, handler) nth[ticks] = handler end,
    on_init = function(handler) init = handler end,
  }
  _G.defines = { events = events, gui_type = { entity = 1 } }
  _G.game = {
    get_player = function(index) return { index = index, prints = {} } end,
    connected_players = {},
    print = function(text) end,
  }
  _G.commands = {
    add_command = function(name, help, handler) subscribed["command:" .. name] = { help, handler } end,
  }

  util.on_init(function() end)
  equal(type(init), "function", "on_init subscribes to script.on_init")

  util.every_second(function() end)
  equal(type(nth[60]), "function", "every_second registers on_nth_tick(60)")

  local built = {}
  util.on_built("gas-extractor", function(entity, player) built[#built + 1] = { entity, player } end)
  equal(type(subscribed[1]), "function", "on_built subscribes to player builds")
  equal(type(subscribed[2]), "function", "on_built subscribes to robot builds")
  equal(type(subscribed[3]), "function", "on_built subscribes to script-raised builds")
  equal(type(subscribed[4]), "function", "on_built subscribes to space-platform builds")

  subscribed[1]({ entity = { name = "gas-extractor" }, player_index = 7 })
  equal(#built, 1, "on_built passes a matching build through")
  equal(built[1][1].name, "gas-extractor", "on_built passes the entity")
  equal(built[1][2].index, 7, "on_built resolves the building player")

  subscribed[2]({ entity = { name = "gas-extractor" } })
  equal(#built, 2, "on_built handles a robot build")
  equal(built[2][2], nil, "a robot build has no player")

  subscribed[1]({ entity = { name = "iron-chest" } })
  equal(#built, 2, "on_built filters by prototype name")

  subscribed[3]({})
  equal(#built, 2, "on_built ignores an event without an entity")
  subscribed[3]({ entity = { name = "gas-extractor", valid = false } })
  equal(#built, 2, "on_built ignores an entity that no longer exists")

  local removed = 0
  util.on_removed("gas-extractor", function(entity) removed = removed + 1 end)
  for event, _ in pairs({ [5] = 1, [6] = 1, [7] = 1, [8] = 1, [9] = 1 }) do
    subscribed[event]({ entity = { name = "gas-extractor" } })
  end
  equal(removed, 5, "on_removed handles every removal event")
  subscribed[5]({ entity = { name = "iron-chest" } })
  equal(removed, 5, "on_removed filters by prototype name")
  -- A script can raise script_raised_destroy with an entity it already removed.
  subscribed[9]({ entity = { name = "gas-extractor", valid = false } })
  equal(removed, 5, "on_removed ignores an entity that no longer exists")

  -- ------------------------------------------------------- two subscriptions, one event
  -- Factorio keeps a single handler per event: `script.on_event(same, other)` replaces the first.
  -- The helpers collect subscriptions behind one dispatcher instead, so a mod that watches two
  -- prototype names hears about both. (Watching a second machine tier is exactly that case.)
  local second = {}
  util.on_built("gas-extractor-2", function(entity) second[#second + 1] = entity.name end)

  local total = #built
  subscribed[1]({ entity = { name = "gas-extractor-2" } })
  equal(#second, 1, "a second on_built subscription receives its own prototype")
  equal(#built, total, "a machine the second subscription watches is not passed to the first")

  subscribed[1]({ entity = { name = "gas-extractor" } })
  equal(#built, total + 1, "the first on_built subscription still fires after a second was added")
  equal(#second, 1, "the second subscription did not receive the first prototype")

  local ticks = 0
  util.every_second(function() ticks = ticks + 1 end)
  nth[60]({ tick = 60 })
  equal(ticks, 1, "a second every_second handler is called too")

  local inits = 0
  util.on_init(function() inits = inits + 1 end)
  init()
  equal(inits, 1, "a second on_init handler is called too")

  -- A Factorio build that does not define one of the optional events still loads. Reload the
  -- library first: its subscriptions are per-process, and this check wants an empty slate.
  util = assert(dofile("src/Factorio.Bindings/FactorioUtils.lua"))
  _G.defines = { events = { on_built_entity = 1 }, gui_type = { entity = 1 } }
  subscribed = {}
  util.on_built(nil, function() end)
  equal(type(subscribed[1]), "function", "on_built works when only on_built_entity exists")
end

-- -------------------------------------------------------------------- commands
do
  local registered = nil
  local printed = nil
  local replied = nil
  _G.commands = {
    add_command = function(name, help, handler) registered = { name = name, help = help, handler = handler } end,
  }
  _G.game = {
    get_player = function(index) return { index = index, prints = {} } end,
    connected_players = {},
    print = function(text) printed = text end,
  }

  util.command("gas", "gas-extractor.command-gas-help", function() end)
  equal(registered.name, "gas", "command registers the name")
  equal(registered.help[1], "gas-extractor.command-gas-help", "command wraps the help key in a localised string")

  local event = { player_index = 2, parameter = "  light-oil " }
  equal(util.command_player(event).index, 2, "command_player resolves the player")
  equal(util.command_parameter(event), "light-oil", "command_parameter trims")
  equal(util.command_parameter({ parameter = "   " }), nil, "an empty argument is nil")
  equal(util.command_parameter({}), nil, "a missing argument is nil")
  equal(util.command_player({}), nil, "a console command has no player")

  _G.game.get_player = function(index) return { print = function(text) replied = text end } end
  util.command_reply(event, "hello")
  equal(replied, "hello", "command_reply answers the player")

  replied = nil
  printed = nil
  util.command_reply({}, "hello")
  equal(printed, "hello", "command_reply answers the console")
  equal(replied, nil, "the console reply does not reach a player")
end

-- ------------------------------------------------------------ crafting machines
do
  local machine = { unit_number = 5 }
  machine.get_recipe = function() return { name = "gas-extractor-light-oil" }, nil end
  machine.set_recipe = function(recipe) machine.recipe = recipe end
  equal(util.machine_recipe(machine), "gas-extractor-light-oil", "machine_recipe reads the recipe name")
  equal(util.machine_recipe({ get_recipe = function() return nil, nil end }), nil, "an empty machine has no recipe")
  equal(util.machine_recipe(nil), nil, "machine_recipe on nil")

  util.machine_set_recipe(machine, "gas-extractor-heavy-oil")
  equal(machine.recipe, "gas-extractor-heavy-oil", "machine_set_recipe")

  -- A player looking at the machine, including its recipe list.
  _G.game = {
    get_player = function(index) return nil end,
    connected_players = {
      { opened = nil, opened_gui_type = nil },
    },
    print = function() end,
  }
  equal(util.machine_is_open(machine), false, "machine_is_open with nothing open")

  _G.game.connected_players[1].opened = machine
  _G.game.connected_players[1].opened_gui_type = 1 -- defines.gui_type.entity
  equal(util.machine_is_open(machine), true, "machine_is_open finds the open machine")
  equal(util.machine_is_open({ unit_number = 6 }), false, "machine_is_open compares unit numbers")

  -- Opening something that is not an entity (an item, a custom GUI) must not be mistaken for it.
  _G.game.connected_players[1].opened = { name = "iron-plate" }
  _G.game.connected_players[1].opened_gui_type = 2 -- defines.gui_type.item
  equal(util.machine_is_open(machine), false, "machine_is_open ignores non-entity GUIs")
end

-- --------------------------------------------------------------------- result
if #failures > 0 then
  io.stderr:write(#failures .. " failure(s):\n")
  for _, failure in ipairs(failures) do io.stderr:write("  - " .. failure .. "\n") end
  os.exit(1)
end

print("util check PASS")
