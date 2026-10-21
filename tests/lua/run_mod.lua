-- Executes a packaged mod's control.lua inside the Factorio mock and asserts its behaviour.
--
--   lua5.2 tests/lua/run_mod.lua build/mods/HelloFactorio_0.2.0/control.lua
--
-- This is not Factorio: it proves that the Lua emitted by `factorio-mod build` parses, that it
-- registers the handlers the C# asked for, and that the calls it makes are the dot-call forms the
-- real API uses (`game.get_player(1)`, not `game:get_player(1)`). The authoritative check remains
-- running the mod in the game; see docs/INTEGRATION.md.

local path = assert(arg[1], "usage: lua5.2 run_mod.lua <control.lua>")

local Mock = dofile("tests/lua/factorio_mock.lua")
local state = Mock.new()

local src = assert(io.open(path, "r"), "cannot open " .. path)
local text = src:read("*a")
src:close()

-- Factorio executes control.lua with its API as the environment; Lua 5.2's load() takes `env`.
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

-- ---------------------------------------------------------------- registration
check(type(state.on_init) == "function", "script.on_init was not registered (Globals.Script.OnInit)")
check(state.handlers[state.defines.events.on_tick] ~= nil,
      "on_tick handler was not registered (Events.OnTick)")
check(state.handlers[state.defines.events.on_player_joined_game] ~= nil,
      "on_player_joined_game handler was not registered (Events.OnPlayerJoinedGame)")

-- ------------------------------------------------------------------- on_init
if type(state.on_init) == "function" then
  local okInit, errInit = pcall(state.on_init)
  check(okInit, "on_init raised: " .. tostring(errInit))

  local prints = state.texts("game_print")
  check(prints[1] == "Hello from C#! This mod was compiled to Lua by factorio-mod.",
        "first game.print was: " .. tostring(prints[1]))
  check(prints[2] == "The map has been running for 1234 ticks.",
        "second game.print was: " .. tostring(prints[2]))
  check(prints[3] == "Player alice is at 12.5, -3.25",
        "position print was: " .. tostring(prints[3]))
  check(prints[4] == "Force 1: player" and prints[5] == "Force 2: enemy",
        "force iteration printed: " .. tostring(prints[4]) .. " | " .. tostring(prints[5]))

  -- Writing a native property must hit the table, not a local copy.
  check(state.game.speed == 1, "game.speed was not set (got " .. tostring(state.game.speed) .. ")")
end

-- ------------------------------------------------------------ event handlers
local okJoin, errJoin = pcall(state.fire, "on_player_joined_game", { player_index = 2 })
check(okJoin, "on_player_joined_game handler raised: " .. tostring(errJoin))
local welcome = state.texts("game_print")
check(welcome[6] == "Welcome, bob!", "welcome print was: " .. tostring(welcome[6]))
check(welcome[7] == "You are player index 2.", "index print was: " .. tostring(welcome[7]))

state.game.tick = 1
local okTick, errTick = pcall(state.fire, "on_tick", {})
check(okTick, "on_tick handler raised: " .. tostring(errTick))
local logs = state.texts("log")
check(logs[1] == "factorio_mock ticked once",
      "log() call was: " .. tostring(logs[1]))

-- ------------------------------------------------------------------- verdict
if #failures > 0 then
  io.stderr:write("runtime check FAILED (" .. #failures .. " problem(s)):\n")
  for _, f in ipairs(failures) do io.stderr:write("  - " .. f .. "\n") end
  os.exit(1)
end

print(string.format("runtime check PASS (%d game.print calls, %d log calls)",
                    #state.texts("game_print"), #state.texts("log")))
