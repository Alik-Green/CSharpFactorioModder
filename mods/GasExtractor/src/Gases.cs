namespace GasExtractor
{
    /// <summary>
    /// The gases this machine can pull out of the air, and the recipes that select them.
    /// <para>
    /// The machine's recipe <em>is</em> its setting: the player picks one in the machine window and
    /// <see cref="Extractors"/> reads it back with <see cref="Crafting.RecipeName"/>. Every recipe
    /// is named <c>gas-extractor-&lt;gas&gt;</c> and produces one fluid, so the mapping between the
    /// two is a string prefix rather than a lookup table.
    /// </para>
    /// <para>Keep in sync with <c>lua/data.lua</c>, which defines the recipes.</para>
    /// </summary>
    public static class Gases
    {
        /// <summary>Fluid name of petroleum gas.</summary>
        public const string Petroleum = "petroleum-gas";

        /// <summary>Fluid name of light oil.</summary>
        public const string LightOil = "light-oil";

        /// <summary>Fluid name of heavy oil.</summary>
        public const string HeavyOil = "heavy-oil";

        /// <summary>Every recipe is <c>gas-extractor-</c> followed by the fluid name.</summary>
        public const string RecipePrefix = "gas-extractor-";

        /// <summary>Fluid produced by one craft. Mirrors <c>FLUID_PER_CRAFT</c> in data.lua.</summary>
        public const double FluidPerCraft = 20;

        /// <summary>The gas a new extractor starts with.</summary>
        public const string Default = Petroleum;

        /// <summary>Recipe prototype name that selects <paramref name="gas"/>.</summary>
        /// <param name="gas">Fluid prototype name, e.g. <c>light-oil</c>.</param>
        /// <returns>The recipe name, e.g. <c>gas-extractor-light-oil</c>.</returns>
        public static string RecipeOf(string gas)
        {
            return RecipePrefix + gas;
        }

        /// <summary>Whether a string names a gas this mod can extract.</summary>
        /// <param name="gas">Candidate fluid name, usually typed by a player.</param>
        /// <returns><c>true</c> when the machine has a recipe for it.</returns>
        public static bool IsKnown(string gas)
        {
            return gas == Petroleum || gas == LightOil || gas == HeavyOil;
        }

        /// <summary>Fluid produced by <paramref name="recipeName"/>, or <c>null</c> for other recipes.</summary>
        /// <param name="recipeName">A recipe prototype name read from the machine.</param>
        /// <returns>The gas, or <c>null</c> when the recipe is not one of ours.</returns>
        public static string OfRecipe(string recipeName)
        {
            if (recipeName == null) return null;
            if (recipeName == RecipeOf(Petroleum)) return Petroleum;
            if (recipeName == RecipeOf(LightOil)) return LightOil;
            if (recipeName == RecipeOf(HeavyOil)) return HeavyOil;
            return null;
        }

        /// <summary>Visits every gas once, in the order they are offered to players.</summary>
        /// <param name="visit">Called with each fluid prototype name.</param>
        public static void ForEach(System.Action<string> visit)
        {
            visit(Petroleum);
            visit(LightOil);
            visit(HeavyOil);
        }
    }
}
