-- FactorioUtils.lua -- the Lua half of the `Factorio.Util` bindings.
--
-- This library is bundled into every mod's control.lua, before the mod's own code, as
-- `local util = (function () ... end)()` plus one local alias per function. It exists because
-- some operations cannot be expressed as a C# template: iterating an arbitrary Lua table,
-- creating a table on demand, or checking whether an object that may be nil is still valid.
--
-- Everything here is plain Lua 5.2 with no dependencies, no globals and no `require`, so it also
-- runs unchanged inside Factorio's sandbox. tests/lua/util_test.lua exercises it directly.
--
-- Contract: the C# side (src/Factorio.Bindings/Utilities/*.cs) is a typed façade over these
-- functions. If you add a function here, add the matching template there and a case to the tests.

local util = {}

-- `x ~= nil and x.valid`, in one place, so no call site has to repeat (and possibly mistype) the
-- short-circuit. Returns false for nil and for a stale reference to a removed object.
---@param object any value that may be a LuaObject or nil
---@return boolean
function util.valid(object)
  if object == nil then
    return false
  end
  return object.valid ~= false
end

-- `object ~= nil and object.valid and object.name == expected`, the three-part check that almost
-- every entity handler starts with.
---@param object any value that may be a LuaObject or nil
---@param expected string
---@return boolean
function util.is(object, expected)
  if object == nil or object.valid == false then
    return false
  end
  return object.name == expected
end

-- Returns `root[key]`, creating an empty table if it is missing. `root` is normally `storage`,
-- which starts out as an empty table and persists with the save.
--
-- Anything that is not a table is replaced: a section that holds `false` or a number is a bug (or
-- an older save format), and returning it would only move the failure to some caller that indexes
-- the result.
---@param root table
---@param key string
---@return table
function util.ensure_table(root, key)
  local value = root[key]
  if type(value) ~= "table" then
    value = {}
    root[key] = value
  end
  return value
end

-- `pairs`, as a function: `util.each(t, function(k, v) ... end)`.
---@param t table
---@param fn function(key, value)
function util.each(t, fn)
  for key, value in pairs(t) do
    fn(key, value)
  end
end

-- `ipairs`, as a function; stops at the first nil as Factorio's own array iteration does.
---@param t table
---@param fn function(value)
function util.each_array(t, fn)
  for _, value in ipairs(t) do
    fn(value)
  end
end

-- Sorted iteration, for deterministic output (log lines, GUI lists) when the table's order is
-- otherwise engine-defined. Keys must be all numbers or all strings.
---@param t table
---@param fn function(key, value)
function util.each_sorted(t, fn)
  local keys = {}
  for key in pairs(t) do
    keys[#keys + 1] = key
  end
  table.sort(keys)
  for _, key in ipairs(keys) do
    fn(key, t[key])
  end
end

-- Number of entries that `pairs` would visit; Factorio's own table_size does the same thing but
-- this keeps the library usable outside the game (tests, tooling).
---@param t table
---@return number
function util.count(t)
  local n = 0
  for _ in pairs(t) do
    n = n + 1
  end
  return n
end

-- Copies a table shallowly. Handy for snapshotting a prototype or a fluidbox reading before
-- handing it to the game.
---@param t table
---@return table
function util.copy(t)
  local out = {}
  for key, value in pairs(t) do
    out[key] = value
  end
  return out
end

-- Joins values with a separator, calling `tostring`-style formatting on each, so a mod can print
-- a list without pulling in a string library.
---@param list table
---@param separator string
---@return string
function util.join(list, separator)
  local parts = {}
  for i = 1, #list do
    parts[i] = tostring(list[i])
  end
  return table.concat(parts, separator)
end

-- ---------------------------------------------------------------- object access
-- The C# side calls these instead of spelling out `target[key]` in a template. Keeping the
-- indexing here means the C# wrapper is a plain function call with string arguments: no Lua
-- syntax, no quotes and no table literals ever appear inside a `@CSharpLua.Template`, which is
-- where the compiler's lexer used to trip over them.

-- `target[key]`. Nil-tolerant on purpose: a registry can hold a reference to an object that has
-- since been removed, and asking it for a field should answer nil rather than raise.
---@param target table|nil
---@param key string
function util.get(target, key)
  if target == nil then return nil end
  return target[key]
end

-- `target[key] = value`
---@param target table
---@param key string
---@param value any
function util.set(target, key, value)
  target[key] = value
end

-- `target[key] = nil`. Nil-tolerant like `util.get`: clearing a field of something that is already
-- gone is a no-op, not a crash.
---@param target table|nil
---@param key string
function util.unset(target, key)
  if target == nil then return end
  target[key] = nil
end

-- `target[key](...)`: Factorio's API uses dot calls, so the receiver is passed explicitly by the
-- caller when the function needs it.
---@param target table
---@param key string
function util.call(target, key, ...)
  return target[key](...)
end

-- Builds a small table from up to three key/value pairs, skipping absent ones. Used by the
-- generated constructors so a table literal never has to be written inside a template.
---@param k1 string|nil
---@param v1 any
function util.table(k1, v1, k2, v2, k3, v3)
  local t = {}
  if k1 ~= nil then t[k1] = v1 end
  if k2 ~= nil then t[k2] = v2 end
  if k3 ~= nil then t[k3] = v3 end
  return t
end

-- ---------------------------------------------------------------- storage slots
-- A storage section addressed by name: `storage[key]`. The C# side wraps these in StorageSlot<T>.

---@param root table
---@param section string
---@return any
function util.slot_get(root, section, key)
  local t = root[section]
  if t == nil then return nil end
  return t[key]
end

---@param root table
---@param section string
function util.slot_set(root, section, key, value)
  util.ensure_table(root, section)[key] = value
end

---@param root table
---@param section string
function util.slot_remove(root, section, key)
  local t = root[section]
  if t ~= nil then t[key] = nil end
end

---@param root table
---@param section string
---@return boolean
function util.slot_has(root, section, key)
  local t = root[section]
  if t == nil then return false end
  return t[key] ~= nil
end

---@param root table
---@param section string
---@return number
function util.slot_count(root, section)
  local t = root[section]
  if t == nil then return 0 end
  return util.count(t)
end

---@param root table
---@param section string
function util.slot_clear(root, section)
  root[section] = nil
end

-- ----------------------------------------------------------- entity registries
-- A registry is one saved table of entities keyed by unit number (`storage[section][unit] =
-- entity`) plus any number of typed value tables that belong to it (`storage["<section>/<name>"]`).
-- The C# side wraps this in EntityRegistry / EntityValue<T>; see Utilities/EntityRegistry.cs.
--
-- Value tables are discovered from the mod's own code: `registry_value` is called once per value
-- when the module is loaded, and records the name in this in-memory list. Keeping the list out of
-- storage matters: module initialisers run while the save is loading, where writing to `storage`
-- is not allowed. The list is rebuilt on every load from the same declarations, so nothing is
-- lost by not saving it.

local registry_fields = {}

-- Returns the section a value table lives in (`"<section>/<name>"`) and remembers that the
-- registry owns it, so removing a machine also removes its values.
---@param section string registry section name
---@param name string value name, unique within the registry
---@return string
function util.registry_value(section, name)
  local names = registry_fields[section]
  if names == nil then
    names = {}
    registry_fields[section] = names
  end
  local known = false
  for i = 1, #names do
    if names[i] == name then
      known = true
      break
    end
  end
  if not known then
    names[#names + 1] = name
  end
  return section .. "/" .. name
end

-- Drops one key from every value table the registry owns.
---@param root table
---@param section string
---@param key string
local function registry_forget(root, section, key)
  local names = registry_fields[section]
  if names == nil then
    return
  end
  for i = 1, #names do
    local values = root[section .. "/" .. names[i]]
    if values ~= nil then
      values[key] = nil
    end
  end
end

-- The storage key for an entity: its unit number as a string, or nil for entities that have none
-- (which cannot be stored, because they have no stable identity).
---@param entity any
---@return string|nil
local function entity_key(entity)
  if entity == nil or entity.unit_number == nil then
    return nil
  end
  return tostring(entity.unit_number)
end

---@param root table
---@param section string
---@param entity any
function util.registry_add(root, section, entity)
  local key = entity_key(entity)
  if key == nil then
    return
  end
  util.ensure_table(root, section)[key] = entity
end

---@param root table
---@param section string
---@param entity any
function util.registry_remove(root, section, entity)
  local key = entity_key(entity)
  if key == nil then
    return
  end
  local entities = root[section]
  if entities ~= nil then
    entities[key] = nil
  end
  registry_forget(root, section, key)
end

---@param root table
---@param section string
---@param entity any
---@return boolean
function util.registry_has(root, section, entity)
  local key = entity_key(entity)
  if key == nil then
    return false
  end
  local entities = root[section]
  if entities == nil then
    return false
  end
  return util.valid(entities[key])
end

-- Number of machines that still exist. Entries whose entity was removed without an event are not
-- counted; `registry_each` is what drops them.
---@param root table
---@param section string
---@return number
function util.registry_count(root, section)
  local entities = root[section]
  if entities == nil then
    return 0
  end
  local count = 0
  for _, entity in pairs(entities) do
    if util.valid(entity) then
      count = count + 1
    end
  end
  return count
end

-- Visits every machine that still exists, then drops the entries (and values) of the ones that do
-- not. Removing entries while a `pairs` loop is running is undefined, so the dead keys are
-- collected first. Requested by a player or a script rather than by the game, which is why it is
-- safe to write to storage here.
---@param root table
---@param section string
---@param fn function(entity)
function util.registry_each(root, section, fn)
  local entities = util.ensure_table(root, section)
  local dead = nil
  for key, entity in pairs(entities) do
    if util.valid(entity) then
      fn(entity)
    else
      if dead == nil then
        dead = {}
      end
      dead[#dead + 1] = key
    end
  end
  if dead ~= nil then
    for i = 1, #dead do
      entities[dead[i]] = nil
      registry_forget(root, section, dead[i])
    end
  end
end

-- Forgets every machine and every value the registry owns.
---@param root table
---@param section string
function util.registry_clear(root, section)
  local entities = root[section]
  if entities ~= nil then
    for key in pairs(entities) do
      registry_forget(root, section, key)
    end
  end
  local names = registry_fields[section]
  if names ~= nil then
    for i = 1, #names do
      root[section .. "/" .. names[i]] = nil
    end
  end
  root[section] = nil
end

-- One typed value per machine, stored in the section named by `field`.
---@param root table
---@param field string
---@param entity any
---@return any
function util.value_get(root, field, entity)
  local key = entity_key(entity)
  if key == nil then
    return nil
  end
  local values = root[field]
  if values == nil then
    return nil
  end
  return values[key]
end

---@param root table
---@param field string
---@param entity any
---@param value any
function util.value_set(root, field, entity, value)
  local key = entity_key(entity)
  if key == nil then
    return
  end
  util.ensure_table(root, field)[key] = value
end

---@param root table
---@param field string
---@param entity any
---@return boolean
function util.value_has(root, field, entity)
  local key = entity_key(entity)
  if key == nil then
    return false
  end
  local values = root[field]
  if values == nil then
    return false
  end
  return values[key] ~= nil
end

---@param root table
---@param field string
---@param entity any
function util.value_remove(root, field, entity)
  local key = entity_key(entity)
  if key == nil then
    return
  end
  local values = root[field]
  if values ~= nil then
    values[key] = nil
  end
end

-- ------------------------------------------------------------- single settings
-- One value in one storage key: `storage[key]`. The C# side wraps this in Setting<T>.

---@param root table
---@param key string
---@return any
function util.setting_get(root, key)
  return root[key]
end

---@param root table
---@param key string
---@param value any
function util.setting_set(root, key, value)
  root[key] = value
end

---@param root table
---@param key string
---@return boolean
function util.setting_has(root, key)
  return root[key] ~= nil
end

---@param root table
---@param key string
function util.setting_clear(root, key)
  root[key] = nil
end

-- ---------------------------------------------------------------- mod lifecycle
-- The handlers a mod always needs, with the event bookkeeping done once here instead of at every
-- call site: build and removal across every way an entity can appear or disappear.

-- Factorio keeps ONE handler per event per script: a second `script.on_event(same_event, ...)`
-- replaces the first. A mod that subscribes twice - one handler for the first tier of a machine and
-- another for the second, say - would silently lose the first subscription, with no error anywhere.
-- So every subscription is collected here and dispatched from a single registration per event.
local event_handlers = {}

-- Subscribes only if this Factorio version defines the event, so the same compiled mod runs on a
-- version that lacks (or has renamed) one of the less common build events.
---@param event number|nil
---@param handler function
local function subscribe(event, handler)
  if event == nil then return end

  local handlers = event_handlers[event]
  if handlers == nil then
    handlers = {}
    event_handlers[event] = handlers
    script.on_event(event, function (event_data)
      local count = #handlers
      for index = 1, count do
        handlers[index](event_data)
      end
    end)
  end

  handlers[#handlers + 1] = handler
end

-- The same applies to `script.on_init` and `script.on_nth_tick`: one registration, many handlers.
local init_handlers = {}

---@param fn function()
function util.on_init(fn)
  if #init_handlers == 0 then
    script.on_init(function ()
      local count = #init_handlers
      for index = 1, count do
        init_handlers[index]()
      end
    end)
  end
  init_handlers[#init_handlers + 1] = fn
end

local tick_handlers = {}

---@param fn function(event)
function util.every_second(fn)
  if #tick_handlers == 0 then
    script.on_nth_tick(60, function (event)
      local count = #tick_handlers
      for index = 1, count do
        tick_handlers[index](event)
      end
    end)
  end
  tick_handlers[#tick_handlers + 1] = fn
end

-- Every way an entity can be built. `fn` receives the entity and the player who built it, which is
-- nil for construction robots and for script-raised builds.
---@param prototype string|nil only this prototype, or nil for every build
---@param fn function(entity, player)
function util.on_built(prototype, fn)
  local handler = function (event)
    -- `script_raised_*` events carry whatever table the raising script passed, so the entity is
    -- not guaranteed to exist any more. A dead reference answers `valid == false`, not an error.
    local entity = event.entity
    if not util.valid(entity) then
      return
    end
    if prototype ~= nil and entity.name ~= prototype then
      return
    end
    local player = nil
    local index = event.player_index
    if index ~= nil then
      player = game.get_player(index)
    end
    fn(entity, player)
  end
  subscribe(defines.events.on_built_entity, handler)
  subscribe(defines.events.on_robot_built_entity, handler)
  subscribe(defines.events.script_raised_built, handler)
  subscribe(defines.events.on_space_platform_built_entity, handler)
end

-- Every way an entity can go away: mined by hand, mined by robots, destroyed, killed, or removed
-- by a script.
---@param prototype string|nil only this prototype, or nil for every removal
---@param fn function(entity)
function util.on_removed(prototype, fn)
  local handler = function (event)
    -- Same as `on_built`: a script can raise this event with an entity it has already destroyed, in
    -- which case there is nothing left to look up. The registry drops the entry on its next pass.
    local entity = event.entity
    if not util.valid(entity) then
      return
    end
    if prototype ~= nil and entity.name ~= prototype then
      return
    end
    fn(entity)
  end
  subscribe(defines.events.on_player_mined_entity, handler)
  subscribe(defines.events.on_robot_mined_entity, handler)
  subscribe(defines.events.on_space_platform_mined_entity, handler)
  subscribe(defines.events.on_entity_died, handler)
  subscribe(defines.events.script_raised_destroy, handler)
end

-- ---------------------------------------------------------------- console commands

-- `commands.add_command(name, {"help-key"}, handler)`, with the help text as a localised string
-- so the locale file decides the wording.
---@param name string
---@param help_key string
---@param handler function(event)
function util.command(name, help_key, handler)
  commands.add_command(name, { help_key }, handler)
end

---@param event table command event data
---@return table|nil player who ran the command, nil for the console
function util.command_player(event)
  local index = event.player_index
  if index == nil then
    return nil
  end
  return game.get_player(index)
end

-- The typed argument, trimmed; nil when the player typed nothing.
---@param event table command event data
---@return string|nil
function util.command_parameter(event)
  local text = event.parameter
  if text == nil then
    return nil
  end
  text = string.match(text, "^%s*(.-)%s*$")
  if text == "" then
    return nil
  end
  return text
end

-- Answers whoever ran the command: the player, or the console when there is none.
---@param event table command event data
---@param message any string or localised string
function util.command_reply(event, message)
  local player = util.command_player(event)
  if player ~= nil then
    player.print(message)
  else
    game.print(message)
  end
end

-- ------------------------------------------------------------ crafting machines
-- Reading and setting the recipe is two API calls with an awkward shape (a method returning two
-- values from a getter), so it is wrapped once here.

---@param entity table|nil
---@return string|nil name of the recipe the machine is set to
function util.machine_recipe(entity)
  if entity == nil then
    return nil
  end
  local recipe = entity.get_recipe()
  if recipe == nil then
    return nil
  end
  return recipe.name
end

---@param entity table
---@param recipe string|nil
function util.machine_set_recipe(entity, recipe)
  entity.set_recipe(recipe)
end

-- Whether any player currently has this machine's window open, including the recipe-selection
-- panel. Setting a recipe while that panel is open closes it, so a mod that repairs recipes has to
-- ask before it writes.
---@param entity table|nil
---@return boolean
function util.machine_is_open(entity)
  if entity == nil or entity.unit_number == nil then
    return false
  end
  local unit = entity.unit_number
  for _, player in ipairs(game.connected_players) do
    local opened = player.opened
    if opened ~= nil and player.opened_gui_type == defines.gui_type.entity and opened.unit_number == unit then
      return true
    end
  end
  return false
end

-- ------------------------------------------------------------------ data stage
-- Prototype authoring in C# (Factorio.Data) compiles to calls in this section. They only run in
-- the data stage, where `data` and `defines` exist; control.lua bundles the same library but never
-- calls these.

-- Renders a number the way Factorio wants it in a prototype string ("250", "1.25").
---@param value number
---@return string
function util.number(value)
  if value ~= value or value == math.huge or value == -math.huge then
    return "0"
  end
  if value == math.floor(value) and math.abs(value) < 1e15 then
    return string.format("%d", value)
  end
  return string.format("%.6g", value)
end

-- A power or energy string from a number and a unit: util.energy(250, "kW") -> "250kW".
---@param value number
---@param unit string
---@return string
function util.energy(value, unit)
  return util.number(value) .. unit
end

-- A path inside this mod. Factorio resolves a path without a `__mod__/` prefix against the mod that
-- declares the prototype, which is what a mod's own files want.
---@param path string
---@return string
function util.asset(path)
  return path
end

-- A path inside the base game. Anything already carrying a `__mod__/` prefix is left alone, so a
-- hand-written prefix is not doubled.
---@param path string
---@return string
function util.asset_base(path)
  if string.sub(path, 1, 2) == "__" then
    return path
  end
  return "__base__/" .. path
end

-- Starts a prototype table: `{ type = "item", name = "demo-pellet" }`.
---@param root table
---@param prototype_type string
---@param name string
function util.proto_new(root, prototype_type, name)
  root.type = prototype_type
  root.name = name
end

-- Sets a prototype field. A nil value is not written: it is *recorded*, so a prototype that
-- inherits from a vanilla one can say "not this field" (C# `NextUpgrade = null`).
---@param root table
---@param key string
---@param value any
function util.proto_set(root, key, value)
  if value == nil then
    local cleared = root.__cleared
    if cleared == nil then
      cleared = {}
      root.__cleared = cleared
    end
    cleared[#cleared + 1] = key
    return
  end
  root[key] = value
end

-- Sets a field of a nested table, creating the nested table on first use: minable.result.
---@param root table
---@param parent string
---@param key string
---@param value any
function util.proto_set_path(root, parent, key, value)
  local nested = root[parent]
  if type(nested) ~= "table" then
    nested = {}
    root[parent] = nested
  end
  util.proto_set(nested, key, value)
end

-- Records that this prototype takes unspecified fields from a vanilla prototype at load time.
-- `base` is a `{ type = ..., name = ... }` table (Factorio.Data.VanillaPrototype).
---@param root table
---@param base table
function util.proto_inherit(root, base)
  root.__inherit = { type = base.type, name = base.name }
end

-- Appends to a native list, for `Values.Add`.
---@param list table
---@param value any
---@return table
function util.list_add(list, value)
  list[#list + 1] = value
  return list
end

-- A bounding box in Factorio's short form: `{ { left, top }, { right, bottom } }`.
---@param left number
---@param top number
---@param right number
---@param bottom number
---@return table
function util.bounds(left, top, right, bottom)
  return { { left, top }, { right, bottom } }
end

-- A native list from up to five values. Used wherever C# takes a `Values`.
---@param a any
---@param b any
---@param c any
---@param d any
---@param e any
---@return table
function util.list(a, b, c, d, e)
  local out = {}
  if a ~= nil then out[#out + 1] = a end
  if b ~= nil then out[#out + 1] = b end
  if c ~= nil then out[#out + 1] = c end
  if d ~= nil then out[#out + 1] = d end
  if e ~= nil then out[#out + 1] = e end
  return out
end

-- A pipe connection: `{ direction = defines.direction.north, position = { x = 0, y = -1 } }`.
---@param direction string one of north, south, east, west
---@param x number
---@param y number
---@return table
function util.port(direction, x, y)
  return {
    direction = defines.direction[direction],
    position = { x = x, y = y },
  }
end

-- A fluid box. `ports` is a Values of util.port results.
---@param production_type string "input", "output" or "input-output"
---@param filter string|nil fluid that may enter this box
---@param volume number
---@param ports table|nil
---@return table
function util.fluid_box(production_type, filter, volume, ports)
  local box = { production_type = production_type, volume = volume }
  if filter ~= nil then box.filter = filter end
  if ports ~= nil then box.pipe_connections = ports end
  return box
end

-- One ingredient or product: `{ type = "item", name = "iron-plate", amount = 2 }`.
---@param kind string "item" or "fluid"
---@param name string
---@param amount number
---@return table
function util.ingredient(kind, name, amount)
  return { type = kind, name = name, amount = amount }
end

-- Adds a definition to the builder. The copy is what makes the result plain data: a definition is
-- a C# object with a metatable, a prototype is a table the game can read.
---@param builder table
---@param prototype table
function util.builder_add(builder, prototype)
  local prototypes = builder.prototypes
  prototypes[#prototypes + 1] = util.copy(prototype)
  builder.count = #prototypes
end

-- Hands everything the builder collected to the game.
---@param builder table
function util.builder_flush(builder)
  data.extend(data, builder.prototypes)
end

return util
