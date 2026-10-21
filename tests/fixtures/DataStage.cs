// Compile-time fixture for the *prototype* (data) stage.
//
// Shows that the data stage is a separate API surface: `data.raw`, no `game`, no `script`.
using System;
using Factorio;
using Factorio.Prototypes;

namespace Fixtures
{
    public static class DataStage
    {
        public static void Main()
        {
            var items = Globals.Data.Raw.Get("item");
            var existing = items.Get("iron-plate");
            if (existing != null)
            {
                Globals.Log("iron-plate exists");
            }

            var recipes = Globals.Data.Raw.Get("recipe");
            var recipe = recipes.Get("iron-gear-wheel");
            Globals.Log("recipe: " + (recipe != null));
        }
    }
}
