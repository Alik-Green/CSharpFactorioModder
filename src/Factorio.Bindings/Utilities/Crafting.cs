using System;
using Factorio.Runtime;

namespace Factorio
{
    /// <summary>
    /// The recipe side of a crafting machine, as plain values.
    /// <para>
    /// The native API reads a recipe through <c>get_recipe()</c>, which returns a recipe and a
    /// quality, and writes one through <c>set_recipe()</c>. Both take a prototype or <c>nil</c>, and
    /// the recipe is the machine's whole configuration - which also makes it the thing players
    /// change in the machine window, and the reason changing it from a script closes that window.
    /// These helpers keep all of that in one place.
    /// </para>
    /// <code>
    /// var recipe = Crafting.RecipeName(machine);          // null when the slot is empty
    /// if (recipe != wanted &amp;&amp; !Crafting.IsOpen(machine)) Crafting.SetRecipe(machine, wanted);
    /// </code>
    /// </summary>
    /// @CSharpLua.Ignore
    public static class Crafting
    {
        /// <summary>
        /// The name of the recipe the machine is set to, or <c>null</c> when it has none (a fresh
        /// machine, or one whose recipe was cleared). The quality is not reported: it is the
        /// machine's, not the recipe's, and a mod that sets a recipe picks one.
        /// </summary>
        /// <param name="machine">A crafting machine, e.g. an assembling machine or furnace.</param>
        /// <returns>The recipe prototype name, or <c>null</c>.</returns>
        /// @CSharpLua.Template = "util_machine_recipe({0})"
        public static string RecipeName(Entity machine) => throw new NotImplementedException();

        /// <summary>
        /// Sets the machine's recipe, or clears it when <paramref name="recipe"/> is <c>null</c>.
        /// <para>
        /// This always writes: it resets any craft in progress and asks an open machine window to
        /// close. Call it only when the recipe actually has to change - compare with
        /// <see cref="RecipeName"/> first, and check <see cref="IsOpen"/> when a player may be
        /// looking at the machine.
        /// </para>
        /// </summary>
        /// <param name="machine">A crafting machine.</param>
        /// <param name="recipe">Recipe prototype name, or <c>null</c> to clear.</param>
        /// @CSharpLua.Template = "util_machine_set_recipe({0}, {1})"
        public static void SetRecipe(Entity machine, string recipe) => throw new NotImplementedException();

        /// <summary>How many crafts this machine has finished since it was built.</summary>
        /// <param name="machine">A crafting machine.</param>
        /// <returns>The number of completed crafts, 0 for machines that do not count them.</returns>
        /// @CSharpLua.Template = "({0}.products_finished or 0)"
        public static double ProductsFinished(Entity machine) => throw new NotImplementedException();

        /// <summary>
        /// Whether a player currently has this machine's window open, including the recipe list
        /// they get when they click the recipe slot.
        /// <para>
        /// Writing a recipe while that list is open closes it, which is exactly the moment a player
        /// is trying to choose something. Any code that repairs recipes should wait until this is
        /// <c>false</c>; a recipe the player picked needs no repair at all, it just has to be
        /// recorded.
        /// </para>
        /// </summary>
        /// <param name="machine">The machine to ask about.</param>
        /// <returns><c>true</c> when at least one connected player has it open.</returns>
        /// @CSharpLua.Template = "util_machine_is_open({0})"
        public static bool IsOpen(Entity machine) => throw new NotImplementedException();
    }
}
