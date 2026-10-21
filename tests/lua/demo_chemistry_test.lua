-- DemoChemistry: the mod whose prototypes are C# definitions and whose runtime stage is two lines.
-- Proves the generated control.lua loads in the mock and that the data.lua beside it was produced
-- (tests/lua/data_inherit_test.lua checks its contents).
--
--   lua5.2 tests/lua/demo_chemistry_test.lua build/mods/DemoChemistry_0.1.0/control.lua

local path = assert(arg[1], "usage: lua5.2 demo_chemistry_test.lua <control.lua>")

local Mock = dofile("tests/lua/factorio_mock.lua")
local state = Mock.new()

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
local function check(condition, message)
  if not condition then failures[#failures + 1] = message end
end

check(type(state.on_init) == "function", "Lifecycle.OnInit was not registered")
if type(state.on_init) == "function" then
  local okInit, errInit = pcall(state.on_init)
  check(okInit, "on_init raised: " .. tostring(errInit))
  local logs = state.texts("log")
  if #logs == 0 then
    -- Some mocks record log() on state.logs instead; accept either.
    logs = state.logs or {}
  end
  -- The message is "<mod name> ready", and the mock's script.mod_name is "factorio_mock".
  local expected = tostring(state.env.script.mod_name) .. " ready"
  local found = false
  for _, line in pairs(logs) do
    if tostring(line) == expected then found = true end
  end
  check(found, "on_init did not log '" .. expected .. "'")
end

if #failures > 0 then
  io.stderr:write("demo chemistry check FAILED (" .. #failures .. " problem(s)):\n")
  for _, failure in ipairs(failures) do io.stderr:write("  - " .. failure .. "\n") end
  os.exit(1)
end

print("demo chemistry check PASS")
