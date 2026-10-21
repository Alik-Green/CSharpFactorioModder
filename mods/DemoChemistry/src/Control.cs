using Factorio;
using Factorio.Runtime;

namespace DemoChemistry
{
    /// <summary>
    /// The mod's runtime half. The prototypes live in <c>data/</c> and are compiled into the data
    /// stage; this class is what runs in the game.
    /// </summary>
    public static class Control
    {
        /// <summary>Runs when the mod loads; named by <c>entry</c> in mod.json.</summary>
        public static void Init()
        {
            Lifecycle.OnInit(() => Globals.Log(Globals.Script.ModName + " ready"));
        }
    }
}
