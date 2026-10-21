-- FactorioCompat.lua -- minimal CSharp.lua runtime shim for Factorio's Lua 5.2.
--
-- CSharp.lua emits every source file as a module that declares its types through
-- `System.namespace` / `namespace.class` and resolves cross-file references through
-- `System.import`. The stock `CoreSystem.lua` is both far larger than needed and built for
-- Lua 5.3 (integer division, bitwise operators, `math.type`, ...), which Factorio's Lua 5.2
-- rejects. This shim implements exactly the contract the emitted code uses:
--
--   System.namespace(name, fn)          enters a namespace
--   namespace.class|struct|interface(name, fn)   registers a type factory
--   namespace.namespace(name, fn)       nested namespace
--   System.import(fn)                   deferred callback, invoked by System.init
--   System.init()                       runs imports; must be called once before entry point
--   System.get(fullName)                resolves a registered type (used by the entry point)
--   System.base(this)                   the base class of a class instance (used by `base(...)`)
--   System.toString(value)              C# string concatenation / interpolation
--   System.as(value, type)              C# `x as T` (identity: Lua tables carry no type tags)
--
-- Every type is materialised lazily on first access, exactly like CoreSystem does, so
-- declaration order between concatenated files does not matter.
--
-- `toString` is the one CoreSystem helper that ordinary mod code provokes, because `"a" + x`
-- compiles to `"a" .. System.toString(x)`. It follows C# semantics (null is empty, booleans are
-- True/False, integral doubles print without a fractional part).
--
-- `as` is the second: `value as LuaEntity` compiles to `System.as(value, Runtime.LuaEntity)`.
-- Lua values have no runtime type tags, so the check cannot be performed; the value is passed
-- through unchanged and a wrong type only fails at the point of use. `is` is NOT supported: it
-- would silently answer wrong, so the build rejects it (see Build/LuaCheck.cs).
--
-- Deliberately NOT implemented (see docs/LIMITATIONS.md): boxing/casts, generic instantiations,
-- arrays, delegates, enums, `System.throw`, `System.default`, `System.cast`, `System.Nullable`,
-- `System.ValueTuple`, LINQ, `System.each`. The bindings never need them, and `factorio-mod
-- build` fails if any of them appears in the emitted Lua (see Build/LuaCheck.cs).

-- CSharp.lua's CoreSystem keeps declared namespaces in `_G`, and the emitted import callbacks rely
-- on that (`FactorioRuntime = Factorio.Runtime`). We therefore publish each declared top-level
-- namespace into the chunk environment too. That is the only global this bundle writes.
local env = _ENV

local System = {}
local modules = {}      -- "Ns.Type" -> factory function
local cache = {}        -- "Ns.Type" -> value (false while being built)
local namespaces = {}   -- "Ns" -> namespace table
local imports = {}      -- deferred System.import callbacks
local initialized = false

local function hasChildren(prefix)
  local n = #prefix + 1
  for name in pairs(modules) do
    if name:sub(1, n) == prefix .. "." then return true end
  end
  return false
end

-- Instances of a class, mapped to the class they were built from. Weak keys: an entity or a
-- prototype table must never be kept alive (or confused with a class instance) by this table.
local classOf = setmetatable({}, { __mode = "k" })

-- Forward declaration: a class factory resolves its base class through this, which is defined
-- below and calls back into `resolve`.
local namespaceTable

-- Turns the members a CSharp.lua class factory returned into a usable class:
--   * the class table is what instances index (`class.__index = class`),
--   * `base(...)` in a constructor becomes `System.base(this)`,
--   * `static` initialisers run once, like CoreSystem does,
--   * calling the class table (`X()`) constructs an instance and runs `__ctor__`.
-- A static class is registered by the same `namespace.class` call and simply never called, so
-- nothing has to distinguish the two cases here.
local function finaliseClass(fullName, members)
  local class = members
  local base = nil
  if type(class.base) == "function" then
    -- `base = function (out) return { out.Some.Namespace.BaseClass } end`; `out` is the same root
    -- namespace table System.import callbacks receive, and indexing it materialises the type.
    local ok, bases = pcall(class.base, namespaceTable(""))
    if ok and type(bases) == "table" then base = bases[1] end
  end

  class.__name = fullName
  class.__base = base
  class.__index = class
  setmetatable(class, {
    __index = base,
    __call = function (cls, ...)
      local this = setmetatable({}, cls)
      classOf[this] = cls
      if cls.__ctor__ ~= nil then cls.__ctor__(this, ...) end
      return this
    end,
  })
  return class
end

local function resolve(name)
  local cached = cache[name]
  if cached ~= nil then return cached ~= false and cached or nil end
  local factory = modules[name]
  if factory == nil then return nil end
  cache[name] = false                    -- cycle guard: a type may not use itself at load time
  local value = factory()
  if type(value) == "table" then
    -- A class factory may carry a `static` function, which CSharp.lua uses for static field
    -- initialisers and static constructors. CoreSystem runs it once, when the class is first
    -- resolved; a class whose fields are only set there is nil without this call.
    if type(value.static) == "function" then value.static(value) end
    value = finaliseClass(name, value)
  end
  cache[name] = value
  return value
end

namespaceTable = function (prefix)
  local existing = namespaces[prefix]
  if existing ~= nil then return existing end
  local t = setmetatable({}, {
    __index = function(_, key)
      if type(key) ~= "string" then return nil end
      local full = (prefix == "") and key or (prefix .. "." .. key)
      local value = resolve(full)
      if value ~= nil then return value end
      if namespaces[full] ~= nil or hasChildren(full) then return namespaceTable(full) end
      return nil
    end,
  })
  namespaces[prefix] = t
  return t
end

local function declare(prefix, name, factory, inClass)
  local full = (prefix == "") and name or (prefix .. (inClass and "+" or ".") .. name)
  local key = full:gsub("%+", ".")
  modules[key] = factory
  cache[key] = nil
end

local function makeScope(prefix, inClass)
  local scope = { prefix = prefix, inClass = inClass }
  local methods = {}
  function methods.class(name, factory) declare(prefix, name, factory, inClass) end
  function methods.struct(name, factory) declare(prefix, name, factory, inClass) end
  function methods.interface(name, factory) declare(prefix, name, factory, inClass) end
  function methods.enum(name, factory) declare(prefix, name, factory, inClass) end
  function methods.namespace(name, fn)
    local full = (prefix == "") and name or (prefix .. "." .. name)
    namespaces[full] = namespaceTable(full)   -- advertise the namespace even if it holds no types
    fn(makeScope(full, false))
  end
  return setmetatable(methods, scope)
end

-- Declares a namespace. The table is registered eagerly because `System.import` callbacks resolve
-- namespaces (`FactorioRuntime = Factorio.Runtime`) before any type in them is used, and because
-- the generated binding modules are dropped from the bundle when they contain no types (see
-- Build/LuaBundle.cs): their namespace shells are re-emitted here so the imports still resolve.
function System.namespace(name, fn)
  namespaces[name] = namespaceTable(name)
  local top = name:match("^[^.]+")
  if top ~= nil and rawget(env, top) == nil then
    rawset(env, top, namespaceTable(top))
  end
  fn(makeScope(name, false))
end

function System.import(fn)
  imports[#imports + 1] = fn
end

-- Runs every deferred import callback, exactly like CoreSystem.System.init does. Called once,
-- from the entry point that `factorio-mod build` appends to control.lua.
function System.init()
  if initialized then return end
  initialized = true
  local root = namespaceTable("")
  for i = 1, #imports do
    imports[i](root)
  end
  imports = {}
end

-- Resolves a fully qualified type, e.g. "HelloFactorio.Main". The entry point uses this.
function System.get(name)
  local value = resolve(name)
  if value == nil then error("FactorioCompat: type '" .. tostring(name) .. "' is not registered", 2) end
  return value
end

-- C# `string + object` and interpolation. Integral doubles print like C# ints ("1", not "1.0"),
-- and null prints as the empty string, matching C#'s `null + "x" == "x"`.
function System.toString(value)
  local t = type(value)
  if value == nil then return "" end
  if t == "boolean" then return value and "True" or "False" end
  if t == "number" then
    if value == value and value % 1 == 0 and value >= -1e15 and value <= 1e15 then
      return string.format("%.0f", value)
    end
    return tostring(value)
  end
  return tostring(value)
end

-- C# `x as T`. Identity: the value keeps flowing; only the C# compiler's view changes.
function System.as(value, _type)
  return value
end

-- `System.base(this)`: the class a `base(...)` call in a constructor should delegate to. The
-- emitted call is `System.base(this).__ctor__(this, ...)`, so this answers the class table.
function System.base(this)
  local cls = classOf[this]
  if cls == nil then return nil end
  return cls.__base
end

return System
