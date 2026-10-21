-- Executes the packaged GasExtractor control.lua inside the Factorio mock and drives it through a
-- full session: build machines, let them craft, watch a player pick a recipe by hand, switch the
-- gas, mine the machines, ask for a status report.
--
--   lua5.2 tests/lua/gas_extractor_test.lua build/mods/GasExtractor_0.4.0/control.lua
--
-- The mock is not Factorio; see docs/INTEGRATION.md for what only the real game can prove.

local path = assert(arg[1], "usage: lua5.2 gas_extractor_test.lua <control.lua>")

local Mock = dofile("tests/lua/factorio_mock.lua")
local state = Mock.new()

-- ---------------------------------------------------------------- a fake machine
local function new_machine(unit, name)
  local machine = {
    object_name = "LuaEntity",
    valid = true,
    name = name or "gas-extractor",
    unit_number = unit,
    products_finished = 0,
    recipe_name = nil,
  }
  machine.get_recipe = function()
    if machine.recipe_name == nil then return nil, nil end
    return { object_name = "LuaRecipe", name = machine.recipe_name }, nil
  end
  machine.set_recipe = function(recipe)
    assert(recipe == nil or type(recipe) == "string",
           "set_recipe expects a prototype name or nil, got " .. type(recipe))
    machine.recipe_name = recipe
  end
  return machine
end

-- ------------------------------------------------------------------- load the mod
local src = assert(io.open(path, "r"), "cannot open " .. path)
local text = src:read("*a")
src:close()

local chunk = assert(load(text, "@" .. path, "t", state.env))
local ok, err = pcall(chunk)
if not ok then
  io.stderr:write("control.lua raised on load: " .. tostring(err) .. "\n")
  os.exit(1)
end

local failures = {}
local function check(cond, msg)
  if not cond then failures[#failures + 1] = msg end
end

local function last_player_print()
  for i = #state.log, 1, -1 do
    if state.log[i].kind == "player_print" then return state.log[i].text end
  end
  return nil
end

-- A storage section may legitimately be absent (cleared and never refilled), so read through this.
local function slot(name)
  return state.storage[name] or {}
end

local function nth_tick_handler()
  for _, handler in pairs(state.nth_tick) do return handler end
  return nil
end

local function has_handler(event)
  return state.handlers[state.defines.events[event]] ~= nil
end

-- --------------------------------------------------------------- registration
check(type(state.on_init) == "function", "script.on_init was not registered")

-- Build and removal are subscribed through the lifecycle helpers, so every way an entity can
-- appear or disappear must be covered - not just the two events the old code handled by hand.
for _, event in ipairs({
  "on_built_entity", "on_robot_built_entity", "script_raised_built", "on_space_platform_built_entity",
}) do
  check(has_handler(event), event .. " was not registered")
end
for _, event in ipairs({
  "on_player_mined_entity", "on_robot_mined_entity", "on_space_platform_mined_entity",
  "on_entity_died", "script_raised_destroy",
}) do
  check(has_handler(event), event .. " was not registered")
end

local per_second = nth_tick_handler()
check(per_second ~= nil, "no nth-tick handler was registered (script.on_nth_tick)")
check(state.nth_tick[60] ~= nil, "the nth-tick handler is not registered for 60 ticks")

check(type(state.commands["gas"]) == "function", "/gas was not registered (commands.add_command)")
check(type(state.commands["gas-status"]) == "function", "/gas-status was not registered")
check(state.command_help["gas"] == "{gas-extractor.command-gas-help}",
      "the /gas help text is not the localised string from the locale file: " .. tostring(state.command_help["gas"]))
check(state.command_help["gas-status"] == "{gas-extractor.command-status-help}",
      "the /gas-status help text is not the localised string")

-- -------------------------------------------------------------------- on_init
state.on_init()
check(state.storage["gas-extractor.machines"] == nil,
      "on_init wrote to storage; a registry is created on first use and needs no initialisation")

-- ------------------------------------------------------------------ building
local machine = new_machine(101)
state.fire("on_built_entity", { entity = machine, player_index = 1 })

check(machine.recipe_name == "gas-extractor-petroleum-gas",
      "a new machine should start on the default gas, recipe is " .. tostring(machine.recipe_name))
check(slot("gas-extractor.machines")["101"] == machine, "the machine was not registered by unit number")
check(slot("gas-extractor.machines/gas")["101"] == "petroleum-gas", "the machine's gas was not recorded")
check(slot("gas-extractor.machines/crafted")["101"] == 0, "the machine's production counter was not created")
check(last_player_print() == "alice: {gas-extractor.message-built}", "the builder was not told what to do")

-- A second machine, built by a construction robot: its own event, no player to notify.
local robot_machine = new_machine(102)
state.fire("on_robot_built_entity", { entity = robot_machine })
check(slot("gas-extractor.machines")["102"] == robot_machine, "a robot-built machine was not registered")
check(robot_machine.recipe_name == "gas-extractor-petroleum-gas", "a robot-built machine was not started")

-- A machine raised by a script carries whatever recipe it was given.
local raised = new_machine(103)
raised.recipe_name = "gas-extractor-heavy-oil"
state.fire("script_raised_built", { entity = raised })
check(slot("gas-extractor.machines")["103"] == raised, "a script-raised machine was not registered")
check(slot("gas-extractor.machines/gas")["103"] == "heavy-oil",
      "a machine built with a recipe was not recorded as extracting that gas")
check(raised.recipe_name == "gas-extractor-heavy-oil", "a machine's own recipe was overwritten on build")

-- Something unrelated: the lifecycle filter must ignore it before the handler runs.
state.fire("on_built_entity", { entity = new_machine(104, "iron-chest"), player_index = 1 })
check(slot("gas-extractor.machines")["104"] == nil, "an unrelated entity was registered")

-- ---------------------------------------------------------------- one second of work
machine.products_finished = 3
per_second({ tick = state.game.tick, nth_tick = 60 })
check(slot("gas-extractor.machines/crafted")["101"] == 3 * 20,
      "crafted fluid should be 3 crafts x 20, got " .. tostring(slot("gas-extractor.machines/crafted")["101"]))

-- Polling again without new crafts must not double count.
per_second({ tick = state.game.tick + 60, nth_tick = 60 })
check(slot("gas-extractor.machines/crafted")["101"] == 60, "production was counted twice")

-- ------------------------------------------------- a recipe the player picked by hand
-- Clicking a recipe in the machine window only changes the machine: no event fires. The next pass
-- must read it back and record it - the bug this test exists for is the pass overwriting it with
-- the stored gas, which also closed the window the player was clicking in.
machine.recipe_name = "gas-extractor-light-oil"
per_second({ tick = state.game.tick + 120, nth_tick = 60 })
check(machine.recipe_name == "gas-extractor-light-oil",
      "the per-second pass undid the player's recipe choice: " .. tostring(machine.recipe_name))
check(slot("gas-extractor.machines/gas")["101"] == "light-oil",
      "the player's choice was not recorded, gas is " .. tostring(slot("gas-extractor.machines/gas")["101"]))

-- And it stays put over later passes, including one that follows a finished craft.
machine.products_finished = 4
per_second({ tick = state.game.tick + 180, nth_tick = 60 })
per_second({ tick = state.game.tick + 240, nth_tick = 60 })
check(machine.recipe_name == "gas-extractor-light-oil", "the player's choice did not stick")
check(slot("gas-extractor.machines/crafted")["101"] == 80, "a craft after the switch was not counted")

-- ------------------------------------------------------------ an open machine window
-- A player cleared the recipe slot and is looking at the window: writing a recipe now would close
-- it under their cursor, so the pass has to wait.
machine.recipe_name = nil
state.open_entity(1, machine)
per_second({ tick = state.game.tick + 300, nth_tick = 60 })
check(machine.recipe_name == nil, "the pass wrote a recipe while a player had the machine open")

-- Closing the window lets the repair happen on the next pass, from the recorded gas.
state.open_entity(1, nil)
per_second({ tick = state.game.tick + 360, nth_tick = 60 })
check(machine.recipe_name == "gas-extractor-light-oil",
      "a closed machine with no recipe was not restored, recipe is " .. tostring(machine.recipe_name))

-- A recipe that is not ours (another mod adding to our crafting category) is not ours to change.
machine.recipe_name = "some-other-mod-recipe"
per_second({ tick = state.game.tick + 420, nth_tick = 60 })
check(machine.recipe_name == "some-other-mod-recipe", "a foreign recipe was overwritten")
check(slot("gas-extractor.machines/gas")["101"] == "light-oil", "a foreign recipe changed the recorded gas")
machine.recipe_name = "gas-extractor-light-oil"

-- --------------------------------------------------------------- switching gas
local before = #state.log
state.run_command("gas", 1, "heavy-oil")

check(machine.recipe_name == "gas-extractor-heavy-oil", "the machine was not switched to heavy oil")
check(robot_machine.recipe_name == "gas-extractor-heavy-oil", "the robot-built machine was not switched")
check(raised.recipe_name == "gas-extractor-heavy-oil", "the script-raised machine was not switched")
check(slot("gas-extractor.machines/gas")["101"] == "heavy-oil", "the switch was not recorded")
check(state.storage["gas-extractor.preferred-gas"] == "heavy-oil", "the choice was not remembered")

local switched = false
for i = before + 1, #state.log do
  if state.log[i].text == "alice: {gas-extractor.message-gas-set|heavy-oil|3}" then switched = true end
end
check(switched, "the player was not told that 3 machines switched: "
      .. tostring(state.log[#state.log] and state.log[#state.log].text))

-- New machines inherit the remembered choice.
local fourth = new_machine(105)
state.fire("on_built_entity", { entity = fourth, player_index = 1 })
check(fourth.recipe_name == "gas-extractor-heavy-oil", "a new machine did not inherit the chosen gas")

-- The second tier is a separate prototype with its own subscription, but the same registry: the
-- gas recipes are shared, so to the mod a fast machine is just another extractor.
local upgraded = new_machine(107, "gas-extractor-2")
state.fire("on_built_entity", { entity = upgraded, player_index = 1 })
check(slot("gas-extractor.machines")["107"] == upgraded, "a gas-extractor-2 was not registered")
check(upgraded.recipe_name == "gas-extractor-heavy-oil",
      "a gas-extractor-2 did not start on the chosen gas, recipe is " .. tostring(upgraded.recipe_name))
check(#state.log > 0 and state.log[#state.log].text == "alice: {gas-extractor.message-built}",
      "the builder of a gas-extractor-2 was not told what to do")

-- An unknown gas changes nothing and lists the options.
before = #state.log
state.run_command("gas", 1, "unobtainium")
check(machine.recipe_name == "gas-extractor-heavy-oil", "an unknown gas still changed the recipe")
local unknown, listed = false, 0
for i = before + 1, #state.log do
  local line = state.log[i].text
  if line == "alice: {gas-extractor.message-gas-unknown|unobtainium}" then unknown = true end
  if line == "alice:   petroleum-gas" or line == "alice:   light-oil" or line == "alice:   heavy-oil" then
    listed = listed + 1
  end
end
check(unknown, "an unknown gas was not reported")
check(listed == 3, "the three known gases were not listed, listed " .. listed)

-- The argument arrives trimmed, and an empty one counts as unknown.
before = #state.log
state.run_command("gas", 1, "  light-oil  ")
check(machine.recipe_name == "gas-extractor-light-oil", "a padded gas name was not accepted")
check(upgraded.recipe_name == "gas-extractor-light-oil", "the upgraded machine was not switched")

-- A console command (no player) must report instead of crashing.
before = #state.log
state.run_command("gas", nil, "heavy-oil")
local console_reported = false
for i = before + 1, #state.log do
  if state.log[i].kind == "game_print" and state.log[i].text == "{gas-extractor.message-no-player}" then
    console_reported = true
  end
end
check(console_reported, "a console command was not answered with a message")
check(machine.recipe_name == "gas-extractor-light-oil", "a console command still switched the machines")

-- ------------------------------------------------------------------- status
-- 101 crafted 80 and nothing else was polled after a craft, so the totals are known exactly.
-- Five machines are alive: 101, 102, 103, 105 and the upgraded 107.
before = #state.log
state.run_command("gas-status", 1, nil)
local status = false
for i = before + 1, #state.log do
  if state.log[i].text == "alice: {gas-extractor.message-status|5|80}" then status = true end
end
check(status, "the status report is wrong: " .. tostring(last_player_print()))

-- ---------------------------------------------------------------- mining
state.fire("on_player_mined_entity", { entity = machine, player_index = 1 })
check(slot("gas-extractor.machines")["101"] == nil, "a mined machine was not unregistered")
check(slot("gas-extractor.machines/gas")["101"] == nil, "a mined machine kept its gas entry")
check(slot("gas-extractor.machines/crafted")["101"] == nil, "a mined machine kept its production total")

-- Mined by a construction robot.
state.fire("on_robot_mined_entity", { entity = robot_machine })
check(slot("gas-extractor.machines")["102"] == nil, "a robot-mined machine was not unregistered")

-- Removed by a script.
state.fire("script_raised_destroy", { entity = raised })
check(slot("gas-extractor.machines")["103"] == nil, "a script-destroyed machine was not unregistered")

-- The upgraded tier unregisters through its own subscription.
state.fire("on_robot_mined_entity", { entity = upgraded })
check(slot("gas-extractor.machines")["107"] == nil, "a mined gas-extractor-2 was not unregistered")
check(slot("gas-extractor.machines/gas")["107"] == nil, "a mined gas-extractor-2 kept its gas entry")

-- An entity destroyed rather than mined is unregistered too.
state.fire("on_entity_died", { entity = fourth })
check(slot("gas-extractor.machines")["105"] == nil, "a destroyed machine was not unregistered")

-- ------------------------------------------------- entities that vanish silently
-- `fourth` is gone but was already unregistered above; this one disappears without an event.
local ghost = new_machine(106)
state.fire("on_built_entity", { entity = ghost, player_index = 1 })
ghost.valid = false
per_second({ tick = state.game.tick + 480, nth_tick = 60 })
check(slot("gas-extractor.machines")["106"] == nil, "an invalid machine was not forgotten")
check(slot("gas-extractor.machines/gas")["106"] == nil, "the gas of a forgotten machine was left behind")
check(slot("gas-extractor.machines/crafted")["106"] == nil, "the total of a forgotten machine was left behind")

before = #state.log
state.run_command("gas-status", 1, nil)
local none = false
for i = before + 1, #state.log do
  if state.log[i].text == "alice: {gas-extractor.message-status-none}" then none = true end
end
check(none, "an empty registry should report message-status-none, got " .. tostring(last_player_print()))

-- The registry is gone but the preferred gas is not: settings outlive machines.
check(state.storage["gas-extractor.preferred-gas"] == "light-oil", "the remembered gas was lost")

-- ------------------------------------------------------------------- result
if #failures > 0 then
  io.stderr:write(#failures .. " failure(s):\n")
  for _, failure in ipairs(failures) do io.stderr:write("  - " .. failure .. "\n") end
  os.exit(1)
end

local command_count = 0
for _ in pairs(state.commands) do command_count = command_count + 1 end
print(string.format("gas extractor check PASS (%d log lines, %d commands registered)", #state.log, command_count))
