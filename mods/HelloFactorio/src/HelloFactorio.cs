using System;
using Factorio;
using Factorio.Runtime;

namespace HelloFactorio
{
    /// <summary>
    /// The example mod. Everything below is ordinary C#: properties map to native Lua fields,
    /// method calls map to dot calls on the native objects, and <c>Events</c> registers handlers
    /// by event name. `factorio-mod build` turns this into a single `control.lua`.
    /// </summary>
    public static class Main
    {
        /// <summary>Registers the handlers. This is the function named by `entry` in mod.json.</summary>
        public static void Init()
        {
            // `script.on_init` is a lifecycle hook, not an event id, so it hangs off Script.
            Globals.Script.OnInit(OnInit);
            Events.OnTick(OnTick);
            Events.OnPlayerJoinedGame(OnPlayerJoinedGame);
        }

        /// <summary>Runs once, when the save is created or the mod is added to an existing save.</summary>
        private static void OnInit()
        {
            Globals.Game.Print("Hello from C#! This mod was compiled to Lua by factorio-mod.");

            // Reads `game.tick`, a plain native number.
            Globals.Game.Print("The map has been running for " + Globals.Game.Tick + " ticks.");

            var player = Globals.Game.GetPlayer(1);
            if (player == null || !player.Valid) return;

            // Reads `player.character`, then `character.position` and its x/y fields.
            var character = player.Character;
            if (character != null)
            {
                var pos = character.Position;
                Globals.Game.Print("Player " + player.Name + " is at " + pos.X + ", " + pos.Y);
            }

            // Writing a native property: emits `game.speed = 1`.
            Globals.Game.Speed = 1;

            // Iterating a native array: length + indexed access (no foreach - CSharp.lua would
            // need its CoreSystem runtime for that, and a Factorio mod cannot ship one).
            var forces = Globals.Game.Forces;
            for (double i = 1; i <= forces.Count(); i++)
            {
                var force = forces.Get(i);
                Globals.Game.Print("Force " + i + ": " + force.Name);
            }
        }

        /// <summary>Runs 60 times a second; kept trivial so the UPS cost is nil.</summary>
        private static void OnTick(OnTickEvent e)
        {
            // `script.mod_name` - a native attribute of the `script` object (`Globals.Script`).
            if (Globals.Game.Tick == 1)
                Globals.Log(Globals.Script.ModName + " ticked once");
        }

        /// <summary>Demonstrates a typed event argument.</summary>
        private static void OnPlayerJoinedGame(OnPlayerJoinedGameEvent e)
        {
            var player = Globals.Game.GetPlayer(e.PlayerIndex);
            if (player == null) return;
            Globals.Game.Print("Welcome, " + player.Name + "!");
            Globals.Game.Print("You are player index " + player.Index + ".");
        }
    }
}
