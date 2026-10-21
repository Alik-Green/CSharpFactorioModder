-- A tiny simulation of the parts of the Factorio runtime that the example mods touch.
--
-- This is NOT Factorio. It exists so that the *emitted Lua* can be executed and its behaviour
-- asserted in CI without a game binary. The in-game integration test lives in
-- scripts/06-integration-test.sh and is the authoritative check.
--
-- It deliberately enforces Factorio's calling conventions:
--   * API methods are plain functions called with dot syntax (no implicit `self`),
--   * calling a method with a stray first argument (the classic `obj:method()` mistake)
--     is detected because the mock methods validate their arguments.

local M = {}

function M.new()
  local state = {
    log = {},
    handlers = {},
    ticks = {},
    nth_tick = {},
    configuration_changed = nil,
    commands = {},
    command_help = {},
    storage = nil,
    on_init = nil,
    on_load = nil,
  }

  local function record(kind, text)
    state.log[#state.log + 1] = { kind = kind, text = text }
  end

  -- Factorio accepts a plain string or a localised string table ({ "key", "arg", ... }) anywhere a
  -- message is expected. The mock renders both so a test can assert on the key and its arguments.
  local function render(msg)
    if type(msg) == "table" then
      local parts = {}
      for i = 1, #msg do parts[i] = tostring(msg[i]) end
      return "{" .. table.concat(parts, "|") .. "}"
    end
    return msg
  end

  local function make_player(index, name)
    -- `opened` / `opened_gui_type` mirror LuaControl: the machine window (including the recipe
    -- list) a player has open, or nil. state.open_entity* below sets them.
    local player = { index = index, name = name, valid = true, object_name = "LuaPlayer",
                     opened = nil, opened_gui_type = nil }
    -- Factorio: player.print(msg) -- dot call, no self
    player.print = function(msg, color)
      assert(type(msg) == "string" or type(msg) == "table",
             "LuaPlayer.print expects a string or a localised string table, got " .. type(msg))
      assert(color == nil or type(color) == "table", "LuaPlayer.print color must be a table or nil")
      record("player_print", name .. ": " .. render(msg))
    end
    player.character = {
      object_name = "LuaEntity",
      valid = true,
      name = "character",
      health = 100.0,
      position = { x = 12.5, y = -3.25 },
    }
    return player
  end

  local players = { [1] = make_player(1, "alice"), [2] = make_player(2, "bob") }

  local forces = {}
  local force_mt = {
    __index = function(_, key)
      if key == "player" then return forces.player end
      return nil
    end,
  }
  forces.enemy = setmetatable({ name = "enemy", valid = true, object_name = "LuaForce" }, {
    __index = function(t, key)
      if key == "print" then
        return function(msg)
          assert(type(msg) == "string", "LuaForce.print expects a string")
          record("force_print", "enemy: " .. msg)
        end
      end
      return force_mt.__index(t, key)
    end,
  })

  forces.player = setmetatable({ name = "player", valid = true, object_name = "LuaForce" }, {
    __index = function(t, key)
      if key == "print" then
        return function(msg)
          assert(type(msg) == "string", "LuaForce.print expects a string")
          record("force_print", "player: " .. msg)
        end
      end
      return force_mt.__index(t, key)
    end,
  })

  -- game.forces is a LuaCustomTable: indexable by name *and* iterable by integer index, with
  -- `#`/table_size covering both. The mock keeps the name and the index views in one table.
  local force_by_index = { [1] = forces.player, [2] = forces.enemy }
  local game_forces = setmetatable({}, {
    __index = function(_, key)
      if type(key) == "number" then return force_by_index[key] end
      return forces[key]
    end,
  })
  for i = 1, 2 do game_forces[i] = force_by_index[i] end

  local game = {
    tick = 1234,
    speed = 2,
    valid = true,
    object_name = "LuaGameScript",
    forces = game_forces,
    print = function(msg, color)
      assert(type(msg) == "string" or type(msg) == "table",
             "game.print expects a string or a localised string table (dot call, no self), got " .. type(msg))
      assert(color == nil or type(color) == "table", "game.print color must be a table or nil")
      record("game_print", render(msg))
    end,
    get_player = function(index)
      assert(type(index) == "number", "get_player expects a number, got " .. type(index))
      return players[index]
    end,
    get_entity_by_index = function(index)
      assert(type(index) == "number", "get_entity_by_index expects a number")
      return nil
    end,
    connected_players = { players[1], players[2] },
  }

  local script = {}
  script.on_init = function(fn)
    assert(type(fn) == "function", "script.on_init expects a function, got " .. type(fn))
    assert(state.on_init == nil, "on_init registered twice")
    state.on_init = fn
  end
  script.on_load = function(fn) state.on_load = fn end
  script.on_configuration_changed = function(fn) state.configuration_changed = fn end
  script.on_event = function(event, handler, filters)
    assert(type(event) == "number", "script.on_event: event must be a number (got " .. type(event) .. ")")
    assert(type(handler) == "function", "script.on_event: handler must be a function (got " .. type(handler) .. ")")
    assert(filters == nil or type(filters) == "table", "script.on_event: filters must be a table or nil")
    state.handlers[event] = handler
  end
  script.on_nth_tick = function(tick, handler)
    assert(type(tick) == "number" and type(handler) == "function", "script.on_nth_tick(tick, handler)")
    state.nth_tick[tick] = handler
  end
  script.mod_name = "factorio_mock"

  -- commands.add_command(name, help, handler): the help is a localised string or nil.
  local commands = {}
  commands.add_command = function(name, help, handler)
    assert(type(name) == "string", "add_command: name must be a string")
    assert(type(help) == "string" or type(help) == "table" or help == nil,
           "add_command: help must be a localised string, a string or nil")
    assert(type(handler) == "function", "add_command: handler must be a function")
    assert(state.commands[name] == nil, "command registered twice: " .. name)
    state.commands[name] = handler
    state.command_help[name] = render(help)
  end
  commands.remove_command = function(name)
    local existed = state.commands[name] ~= nil
    state.commands[name] = nil
    return existed
  end

  -- defines.events values only need to be unique numbers for the mock.
  local event_names = {
    on_tick = 1, on_player_created = 2, on_built_entity = 3, on_console_chat = 4,
    on_player_joined_game = 5, on_player_left_game = 6, on_entity_died = 7,
    on_player_mined_entity = 10, on_robot_built_entity = 11,
    on_script_trigger_effect = 8, on_gui_click = 9,
    on_robot_mined_entity = 12, script_raised_built = 13, script_raised_destroy = 14,
    on_space_platform_built_entity = 15, on_space_platform_mined_entity = 16,
  }

  local env = {
    game = game,
    script = script,
    storage = {},
    commands = commands,
    defines = {
      events = event_names,
      direction = { north = 0, east = 4, south = 8, west = 12 },
      gui_type = { none = 0, entity = 1, item = 2, equipment = 3, custom = 4 },
    },
    log = function(msg)
      assert(type(msg) == "string", "log expects a string")
      record("log", msg)
    end,
    localised_print = function(msg) record("localised_print", msg) end,
    table_size = function(t) local n = 0 for _ in pairs(t) do n = n + 1 end return n end,
    settings = { get_player_settings = function() return {} end },
    -- intentionally no `require`, no io/os/debug: Factorio mods run in a restricted sandbox
  }
  env._G = env
  env.pairs, env.ipairs, env.type, env.string, env.table, env.math, env.error = pairs, ipairs, type, string, table, math, error
  env.assert, env.select, env.tostring, env.tonumber, env.next, env.rawget, env.rawset = assert, select, tostring, tonumber, next, rawget, rawset
  env.setmetatable, env.getmetatable, env.pcall, env.unpack = setmetatable, getmetatable, pcall, table.unpack

  state.env = env
  state.storage = env.storage
  state.commands = state.commands
  state.run_command = function(name, player_index, parameter)
    local handler = assert(state.commands[name], "unknown command " .. tostring(name))
    local data = { name = name, tick = game.tick, player_index = player_index, parameter = parameter }
    return handler(data)
  end
  state.game = game
  state.script = script
  state.players = players
  state.defines = env.defines

  -- Opens (or closes) a machine window for a player, the way clicking the machine would: the game
  -- sets `opened` to the entity and `opened_gui_type` to defines.gui_type.entity.
  function state.open_entity(player_index, entity)
    local player = assert(players[player_index], "unknown player " .. tostring(player_index))
    player.opened = entity
    player.opened_gui_type = entity == nil and nil or env.defines.gui_type.entity
  end

  function state.fire(event_name, data)
    local id = event_names[event_name]
    assert(id, "unknown event " .. tostring(event_name))
    local handler = state.handlers[id]
    assert(handler, "no handler registered for " .. event_name)
    data = data or {}
    data.name = event_name
    data.tick = game.tick
    return handler(data)
  end

  function state.texts(kind)
    local out = {}
    for _, entry in ipairs(state.log) do
      if kind == nil or entry.kind == kind then out[#out + 1] = entry.text end
    end
    return out
  end

  return state
end

return M
