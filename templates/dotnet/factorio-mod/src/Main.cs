using System;
using Factorio;
using Factorio.Runtime;

namespace FactorioMod
{
    /// <summary>
    /// Entry point of ModDisplayTitle. Everything here is ordinary C#: properties become native Lua
    /// fields, method calls become dot calls, and <c>Globals.Script.OnInit</c> registers a lifecycle
    /// hook. `factorio-mod build` turns it into a single control.lua.
    /// </summary>
    public static class Main
    {
        /// <summary>Registers the mod's handlers. This is the method named by `entry` in mod.json.</summary>
        public static void Init()
        {
            Globals.Script.OnInit(OnInit);
        }

        /// <summary>Runs once, when the save is created or the mod is added to an existing save.</summary>
        private static void OnInit()
        {
            Globals.Game.Print("ModDisplayTitle loaded. Edit src/Main.cs, then build.");
        }
    }
}
