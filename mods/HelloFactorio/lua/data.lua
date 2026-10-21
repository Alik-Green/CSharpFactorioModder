-- Data stage. Written in Lua on purpose: the prototype stage bindings are generated but building
-- prototype tables from C# is not part of this example (see docs/LIMITATIONS.md). The sprite below
-- references a packaged resource, which `factorio-mod build` verifies exists.
data:extend({
  {
    type = "sprite",
    name = "hello-factorio-icon",
    filename = "__HelloFactorio__/graphics/hello-icon.png",
    width = 64,
    height = 64,
    priority = "extra-high",
  },
})
