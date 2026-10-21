using System;
using Factorio;
using Factorio.Runtime;

namespace GasExtractor
{
    /// <summary>
    /// The gas extractors in the save, and the work they need once a second.
    /// <para>
    /// A machine's setting <em>is</em> its recipe: the player picks one in the machine window, or
    /// <c>/gas</c> sets every machine at once. So the mod does not keep a recipe of its own - it
    /// keeps the gas each machine was last known to extract, and treats the machine's own recipe as
    /// the truth whenever the two disagree. That single rule is what makes a recipe picked by hand
    /// stick: the next pass records the player's choice instead of overwriting it.
    /// </para>
    /// <para>
    /// Machines are held in an <see cref="EntityRegistry"/>, which stores them by unit number and
    /// drops the ones that were mined or destroyed as it walks them, along with their gas and their
    /// production counters.
    /// </para>
    /// </summary>
    public static class Extractors
    {
        /// <summary>Prototype name of the first tier machine; must match <c>lua/data.lua</c>.</summary>
        public const string MachineName = "gas-extractor";

        /// <summary>
        /// Prototype name of the upgraded machine. Both tiers are registered in the same registry:
        /// they share the gas recipes, so nothing else about them differs to the mod.
        /// </summary>
        public const string UpgradedMachineName = "gas-extractor-2";

        private static readonly EntityRegistry Machines = Storage.Registry("gas-extractor.machines");
        private static readonly EntityValue<string> Gas = Machines.Value<string>("gas");
        private static readonly EntityValue<double> Crafted = Machines.Value<double>("crafted");
        private static readonly EntityValue<double> LastCraft = Machines.Value<double>("last-craft");
        private static readonly Setting<string> PreferredGas = Storage.Setting<string>("gas-extractor.preferred-gas");

        /// <summary>
        /// Registers a newly built machine and tells the builder what it does.
        /// <para>
        /// A machine that arrives with a recipe - a blueprint, a copy-paste tool, a script - keeps
        /// that recipe and is recorded as extracting that gas. One that arrives empty starts on the
        /// gas the player picked for the save, or petroleum gas on a fresh save.
        /// </para>
        /// </summary>
        /// <param name="machine">The machine that was built.</param>
        /// <param name="builder">The player who built it; <c>null</c> for robots and scripts.</param>
        public static void Register(Entity machine, Player builder)
        {
            var gas = Gases.OfRecipe(Crafting.RecipeName(machine));
            if (gas == null) gas = Preferred();

            Machines.Add(machine);
            Gas.Set(machine, gas);
            Crafted.Set(machine, 0);
            LastCraft.Set(machine, Crafting.ProductsFinished(machine));

            Select(machine, gas);

            if (builder != null) builder.Print(Loc.Key(Keys.Built));
        }

        /// <summary>Forgets a machine that was mined, destroyed or removed by a script.</summary>
        /// <param name="machine">The machine that went away; may already be invalid.</param>
        public static void Forget(Entity machine)
        {
            Machines.Remove(machine);
        }

        /// <summary>
        /// Switches every registered machine to <paramref name="gas"/> and remembers the choice for
        /// machines built later.
        /// </summary>
        /// <param name="gas">A known fluid prototype name, e.g. <c>light-oil</c>.</param>
        /// <returns>How many machines were still alive and were switched.</returns>
        public static double Retarget(string gas)
        {
            PreferredGas.Set(gas);

            var switched = 0.0;
            Machines.ForEach(machine =>
            {
                Gas.Set(machine, gas);
                Select(machine, gas);
                switched = switched + 1;
            });

            return switched;
        }

        /// <summary>
        /// The once-a-second pass: adds up what each machine finished since the last pass, then
        /// makes sure it is still set to the gas it is supposed to extract.
        /// </summary>
        public static void Supervise()
        {
            Machines.ForEach(machine =>
            {
                CountCrafts(machine);
                Sync(machine);
            });
        }

        /// <summary>Sends a player a summary of the extractors in the save.</summary>
        /// <param name="player">The player who asked.</param>
        public static void Report(Player player)
        {
            var running = Machines.Count();
            var produced = 0.0;
            Machines.ForEach(machine =>
            {
                produced = produced + Numbers.OrZero(Crafted.Get(machine));
            });

            if (running == 0) player.Print(Loc.Key(Keys.StatusNone));
            else player.Print(Loc.With(Keys.Status, running, Numbers.Floor(produced)));
        }

        /// <summary>
        /// Adds the crafts finished since the last pass to a machine's total. <c>products_finished</c>
        /// counts for the lifetime of the entity, so the difference between two polls is exactly
        /// what the machine made in between - however many recipes it ran.
        /// </summary>
        /// <param name="machine">A registered machine.</param>
        private static void CountCrafts(Entity machine)
        {
            var done = Crafting.ProductsFinished(machine);
            var previous = Numbers.OrZero(LastCraft.Get(machine));
            if (done <= previous) return;

            Crafted.Set(machine, Numbers.OrZero(Crafted.Get(machine)) + (done - previous) * Gases.FluidPerCraft);
            LastCraft.Set(machine, done);
        }

        /// <summary>
        /// Brings the stored gas and the machine's real recipe back in line.
        /// <para>
        /// In this order, and every step matters:
        /// </para>
        /// <list type="number">
        /// <item>A machine running one of our recipes is correct by definition, whoever set it -
        /// so the choice is recorded, not undone. This is what makes a recipe the player clicked
        /// stick, and why the recipe list they are clicking in is not closed under their cursor.</item>
        /// <item>A foreign recipe (another mod's, in our crafting category) is left alone: it is
        /// not ours to overwrite.</item>
        /// <item>An open machine window means the player is choosing right now; writing a recipe
        /// would close it, so wait for the next pass.</item>
        /// <item>Only an empty recipe slot - a blueprint pasted without one, or a slot the player
        /// emptied - is repaired, from the gas recorded for the machine.</item>
        /// </list>
        /// </summary>
        /// <param name="machine">A registered machine.</param>
        private static void Sync(Entity machine)
        {
            var live = Crafting.RecipeName(machine);

            var chosen = Gases.OfRecipe(live);
            if (chosen != null)
            {
                Gas.Set(machine, chosen);
                return;
            }

            if (live != null) return;
            if (Crafting.IsOpen(machine)) return;

            Select(machine, StoredGas(machine));
        }

        /// <summary>Points a machine at the recipe for a gas, unless it is already set to it.</summary>
        /// <param name="machine">A registered machine.</param>
        /// <param name="gas">The gas it should extract.</param>
        private static void Select(Entity machine, string gas)
        {
            var recipe = Gases.RecipeOf(gas);
            if (Crafting.RecipeName(machine) == recipe) return;
            Crafting.SetRecipe(machine, recipe);
        }

        /// <summary>The gas to start a machine on: the player's choice, or petroleum gas.</summary>
        /// <returns>A known fluid prototype name; never <c>null</c>.</returns>
        private static string Preferred()
        {
            var gas = PreferredGas.Get();
            if (gas == null) return Gases.Default;
            return gas;
        }

        /// <summary>
        /// The gas recorded for a machine, falling back to the default when a save predates the
        /// registry (or the machine was added by something that did not go through
        /// <see cref="Register"/>).
        /// </summary>
        /// <param name="machine">A registered machine.</param>
        /// <returns>A known fluid prototype name; never <c>null</c>.</returns>
        private static string StoredGas(Entity machine)
        {
            var gas = Gas.Get(machine);
            if (gas == null) return Gases.Default;
            return gas;
        }
    }
}
