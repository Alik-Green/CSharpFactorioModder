local System = System
local FactorioRuntime
System.import(function (out)
  FactorioRuntime = Factorio.Runtime
end)
System.namespace("HelloFactorio", function (namespace)
  -- <summary>
  -- The example mod. Everything below is ordinary C#: properties map to native Lua fields,
  -- method calls map to dot calls on the native objects, and <c>Events</c> registers handlers
  -- by event name. `factorio-mod build` turns this into a single `control.lua`.
  -- </summary>
  namespace.class("Main", function (namespace)
    local Init, OnInit, OnTick, OnPlayerJoinedGame
    -- <summary>Registers the handlers. This is the function named by `entry` in mod.json.</summary>
    Init = function ()
      -- `script.on_init` is a lifecycle hook, not an event id, so it hangs off Script.
      script.on_init(OnInit)
      script.on_event(defines.events.on_tick, OnTick)
      script.on_event(defines.events.on_player_joined_game, OnPlayerJoinedGame)
    end
    -- <summary>Runs once, when the save is created or the mod is added to an existing save.</summary>
    OnInit = function ()
      game.print("Hello from C#! This mod was compiled to Lua by factorio-mod.")

      -- Reads `game.tick`, a plain native number.
      game.print("The map has been running for " .. game.tick .. " ticks.")

      local player = game.get_player(1)
      if player == nil or not player.valid then
        return
      end

      -- Reads `player.character`, then `character.position` and its x/y fields.
      local character = player.character
      if character ~= nil then
        local pos = character.position
        game.print("Player " .. player.name .. " is at " .. pos.x .. ", " .. pos.y)
      end

      -- Writing a native property: emits `game.speed = 1`.
      game.speed = 1

      -- Iterating a native array: length + indexed access (no foreach - CSharp.lua would
      -- need its CoreSystem runtime for that, and a Factorio mod cannot ship one).
      local forces = game.forces
      do
        local i = 1
        while i <= table_size(forces) do
          local force = forces[i]
          game.print("Force " .. i .. ": " .. force.name)
          i = i + 1
        end
      end
    end
    -- <summary>Runs 60 times a second; kept trivial so the UPS cost is nil.</summary>
    OnTick = function (e)
      -- `script.mod_name` - a native attribute of the `script` object (`Globals.Script`).
      if game.tick == 1 then
        log(script.mod_name .. " ticked once")
      end
    end
    -- <summary>Demonstrates a typed event argument.</summary>
    OnPlayerJoinedGame = function (e)
      local player = game.get_player(e.player_index)
      if player == nil then
        return
      end
      game.print("Welcome, " .. player.name .. "!")
      game.print("You are player index " .. player.index .. ".")
    end
    return {
      Init = Init
    }
  end)
end)
