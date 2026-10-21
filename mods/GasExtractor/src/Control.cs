using Factorio;
using Factorio.Runtime;

namespace GasExtractor
{
    /// <summary>
    /// Wires the mod together: the lifecycle subscriptions and the commands.
    /// <para>
    /// This file is the whole interface between Factorio and the mod. Everything it needs beyond a
    /// plain event is in <see cref="Lifecycle"/> and <see cref="Commands"/>: one build handler for
    /// every way a machine can be built, one removal handler for every way it can disappear, and a
    /// command object that already knows who is typing and how to answer them.
    /// </para>
    /// </summary>
    public static class Control
    {
        /// <summary>
        /// Registers handlers and commands. This is the function named by <c>entry</c> in
        /// <c>mod.json</c>; it runs when the mod's chunk is loaded, before any event fires.
        /// </summary>
        public static void Init()
        {
            Lifecycle.OnInit(() => OnInit());

            // Every build path and every removal path, filtered to one prototype. A registry fed
            // by only half of them is how mods end up with entries for machines that are gone.
            Lifecycle.OnBuilt(Extractors.MachineName, (machine, builder) => Extractors.Register(machine, builder));
            Lifecycle.OnBuilt(Extractors.UpgradedMachineName, (machine, builder) => Extractors.Register(machine, builder));
            Lifecycle.OnRemoved(Extractors.MachineName, machine => Extractors.Forget(machine));
            Lifecycle.OnRemoved(Extractors.UpgradedMachineName, machine => Extractors.Forget(machine));

            // Once a second is plenty: the machine crafts on the game's own schedule, this only
            // samples the counters and repairs what needs repairing.
            Lifecycle.EverySecond(() => Extractors.Supervise());

            Commands.Add("gas", Keys.CommandGasHelp, command => OnGas(command));
            Commands.Add("gas-status", Keys.CommandStatusHelp, command => OnStatus(command));
        }

        /// <summary>Runs once, when the save is created or the mod is added to an existing save.</summary>
        private static void OnInit()
        {
            Globals.Log(Globals.Script.ModName + " ready");
        }

        /// <summary>
        /// <c>/gas &lt;name&gt;</c>: switches every extractor and remembers the choice for new ones.
        /// </summary>
        /// <param name="command">Who ran the command and what they typed.</param>
        private static void OnGas(ChatCommand command)
        {
            // Switching every machine changes the save, so it needs a player to be accountable for
            // it; a console command only gets told why it did nothing.
            if (command.Player == null)
            {
                command.Reply(Loc.Key(Keys.NoPlayer));
                return;
            }

            var gas = command.Parameter;
            if (!Gases.IsKnown(gas))
            {
                var shown = gas;
                if (shown == null) shown = "";

                command.Reply(Loc.With(Keys.GasUnknown, shown));
                Gases.ForEach(name => command.Reply("  " + name));
                return;
            }

            command.Reply(Loc.With(Keys.GasSet, gas, Extractors.Retarget(gas)));
        }

        /// <summary><c>/gas-status</c>: how many extractors are running and what they produced.</summary>
        /// <param name="command">Who ran the command.</param>
        private static void OnStatus(ChatCommand command)
        {
            if (command.Player == null)
            {
                command.Reply(Loc.Key(Keys.NoPlayer));
                return;
            }

            Extractors.Report(command.Player);
        }
    }
}
