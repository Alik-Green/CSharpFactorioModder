using System;
using Factorio.Runtime;

namespace Factorio
{
    /// <summary>
    /// Checks for native objects that may be gone. Every <c>LuaObject</c> the game hands out can
    /// become invalid between two events (the entity was mined, the player left), and touching an
    /// invalid object raises a script error that disables the mod. These helpers make the check
    /// one call instead of a nil-test plus a validity test that is easy to forget.
    /// </summary>
    /// @CSharpLua.Ignore
    public static class Entities
    {
        /// <summary>
        /// True when the object exists and is still valid: <c>obj ~= nil and obj.valid</c>.
        /// </summary>
        /// <param name="entity">Any native object, possibly null.</param>
        /// <returns>False for null and for stale references.</returns>
        /// @CSharpLua.Template = "util_valid({0})"
        public static bool IsValid(object entity) => throw new NotImplementedException();

        /// <summary>
        /// True when the object exists, is valid and is of the given prototype name. This is the
        /// check an <c>on_built_entity</c> handler needs before doing anything with the entity.
        /// </summary>
        /// <param name="entity">Any native object, possibly null.</param>
        /// <param name="name">Prototype name to compare against, e.g. <c>air-extractor</c>.</param>
        /// <returns>False for null, stale objects and other prototypes.</returns>
        /// @CSharpLua.Template = "util_is({0}, {1})"
        public static bool Is(object entity, string name) => throw new NotImplementedException();

        /// <summary>
        /// Converts a value stored in <c>storage</c> back into a typed entity reference. The value
        /// is returned unchanged; the C# compiler only gains the type. Use <see cref="IsValid"/>
        /// before touching it, because a save can outlive the entity it refers to.
        /// </summary>
        /// <param name="value">A value read from storage.</param>
        /// <returns>The same value typed as <see cref="Entity"/>.</returns>
        /// @CSharpLua.Template = "{0}"
        public static Entity AsEntity(object value) => throw new NotImplementedException();

        /// <summary>
        /// The unit number of an entity, or 0 when it has none. Unit numbers are the stable key for
        /// storing per-entity data: unlike an index they are never reused within a save.
        /// </summary>
        /// <param name="entity">The entity to identify.</param>
        /// <returns>The unit number, or 0 for entities without one.</returns>
        /// @CSharpLua.Template = "({0} ~= nil and {0}.unit_number or 0)"
        public static double UnitNumber(object entity) => throw new NotImplementedException();
    }
}
