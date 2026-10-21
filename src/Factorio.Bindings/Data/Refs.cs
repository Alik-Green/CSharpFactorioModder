using System;

namespace Factorio.Data
{
    /// <summary>
    /// A reference to another prototype, by name.
    /// <para>
    /// Factorio resolves these by name at load time, and a misspelt one stops the game with
    /// "Failed to load mods". The typed helpers below exist to say at the call site what the name
    /// is expected to be - <c>ItemRef.Of("demo-pellet")</c> - while the value handed to the
    /// prototype stays a plain string, because that is what the game wants. The build checks the
    /// reference where it can (a recipe category, for instance).
    /// </para>
    /// </summary>
    public static class ItemRef
    {
        /// <summary>References an item prototype.</summary>
        /// <param name="name">Item prototype name.</param>
        /// <returns>The name.</returns>
        public static string Of(string name) => name;
    }

    /// <summary>A reference to an entity prototype, e.g. what an item places.</summary>
    public static class EntityRef
    {
        /// <summary>References an entity prototype.</summary>
        /// <param name="name">Entity prototype name.</param>
        /// <returns>The name.</returns>
        public static string Of(string name) => name;
    }

    /// <summary>A reference to a fluid prototype, e.g. a fluid box's filter.</summary>
    public static class FluidRef
    {
        /// <summary>References a fluid prototype.</summary>
        /// <param name="name">Fluid prototype name.</param>
        /// <returns>The name.</returns>
        public static string Of(string name) => name;
    }

    /// <summary>A reference to a recipe prototype, e.g. a machine's fixed recipe.</summary>
    public static class RecipeRef
    {
        /// <summary>References a recipe prototype.</summary>
        /// <param name="name">Recipe prototype name.</param>
        /// <returns>The name.</returns>
        public static string Of(string name) => name;
    }
}
