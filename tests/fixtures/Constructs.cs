// Compile-time fixture for the transpilation tests.
//
// It is NOT a mod: it exists to make the transpiler emit every construct the bindings rely on, so
// that tests/Transpile.Tests can assert on the produced Lua without depending on the example mod
// staying feature complete.
using System;
using Factorio;
using Factorio.Runtime;

namespace Fixtures
{
    public static class Constructs
    {
        public static void Run()
        {
            // 1. globals and dot calls. NB: static properties must be *qualified* (Globals.Game):
            // `using static Globals;` crashes the pinned compiler on property templates.
            Globals.Game.Print("hello");
            Globals.Game.Print("hello", new PrintSettings());

            // 2. properties (native attributes) read and written
            var tick = Globals.Game.Tick;
            Globals.Game.Speed = 1.5;
            var speed = Globals.Game.Speed;

            // 3. optional parameters: no trailing nil in the emitted call
            var player = Globals.Game.GetPlayer(1);
            if (player != null)
            {
                player.Print("hi");
                player.Print("hi", new PrintSettings());
            }

            // 4. methods on native objects, chained
            var forces = Globals.Game.Forces;
            var force = forces.Get("player");
            force.Print("f");

            // 5. structured table parameter (a Lua table literal, not a C# object)
            var surface = Globals.Game.GetSurface(1);
            if (surface != null)
            {
                var entity = surface.CreateEntity(new LuaSurfaceCreateEntityParameters(
                    name: "iron-chest",
                    position: new MapPosition(1.5, 2)));
                if (entity != null) entity.Health = 10;
            }

            // 6. multi-return through out parameters
            if (player != null)
            {
                var inventory = player.GetInventory(Defines.Inventory.CharacterMain);
                if (inventory != null)
                {
                    inventory.FindEmptyStack(null, out var stack, out var count);
                    if (stack != null) stack.Clear();
                }
            }

            // 7. arrays
            var entities = surface.FindEntities();
            if (entities != null)
            {
                var n = entities.Length();
                for (double i = 1; i <= n; i++)
                {
                    var e = entities.Get(i);
                    if (e != null) e.Health = 5;
                }
            }

            // 8. defines
            var eventId = Defines.Events.OnTick;
            var direction = Defines.Direction.North;

            // 9. lifecycle hooks, typed events and callbacks
            Globals.Script.OnInit(() => Globals.Game.Print("init"));
            Globals.Script.OnEvent(Defines.Events.OnTick, e => { });
            Events.OnPlayerCreated(e => Globals.Game.Print("created " + e.PlayerIndex));

            // 10. variadic forwarding and a global function
            Globals.Remote.Call("other-mod", "ping", 1, 2, "three");
            Globals.Log("done");

            // 11. The hand-written utilities. Storage handles, lifecycle, commands, recipes:
            //     Each of these is a template over FactorioUtils.lua, so the emitted text is the
            //     contract the Lua tests rely on.
            var machines = Storage.Registry("fixture.machines");
            var gas = machines.Value<string>("gas");
            var crafted = machines.Value<double>("crafted");
            var preferred = Storage.Setting<string>("fixture.preferred");

            var owner = Globals.Game.GetPlayer(1);
            if (owner != null)
            {
                var unit = owner.Character;
                machines.Add(unit);
                gas.Set(unit, "light-oil");
                var read = gas.Get(unit);
                var known = gas.Has(unit);
                machines.ForEach(one => crafted.Set(one, crafted.Get(one) + 1));
                machines.Remove(unit);

                preferred.Set(read);
                if (preferred.Has()) preferred.Clear();

                var recipe = Crafting.RecipeName(unit);
                var open = Crafting.IsOpen(unit);
                var finished = Crafting.ProductsFinished(unit);
                if (recipe != null && !open && finished > 0) Crafting.SetRecipe(unit, recipe);
            }
            machines.Clear();

            Lifecycle.OnInit(() => Globals.Log("init"));
            Lifecycle.OnBuilt("gas-extractor", (machine, builder) =>
            {
                if (builder != null) builder.Print("built");
                machines.Add(machine);
            });
            Lifecycle.OnRemoved("gas-extractor", machine => machines.Remove(machine));
            Lifecycle.EverySecond(() => Globals.Log("second"));

            Commands.Add("fixture", "fixture.command-help", command =>
            {
                var player = command.Player;
                if (player == null) { command.Reply("console"); return; }
                command.Reply(command.Parameter);
            });
        }
    }
}
