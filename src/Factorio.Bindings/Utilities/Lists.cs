using System;
using Factorio.Runtime;

namespace Factorio
{
    /// <summary>
    /// Iteration and size helpers for the plain Lua tables behind <see cref="LuaTable{TKey,TValue}"/>,
    /// <see cref="LuaCustomTable{TKey,TValue}"/> and <see cref="LuaArray{T}"/>.
    /// <para>
    /// C# <c>foreach</c> cannot be used here: the compiler would need its CoreSystem runtime, which
    /// a Factorio mod cannot ship. These helpers pass a C# lambda to a small Lua function instead,
    /// which keeps the loop in Lua where <c>pairs</c> exists.
    /// </para>
    /// </summary>
    /// @CSharpLua.Ignore
    public static class Lists
    {
        /// <summary>
        /// Visits every entry of a table in engine order: <c>for k, v in pairs(t)</c>.
        /// Do not add or remove entries while iterating; collect the changes and apply them after.
        /// </summary>
        /// <param name="table">The table, array or custom table to walk.</param>
        /// <param name="visit">Called as <c>(key, value)</c> for every entry.</param>
        /// @CSharpLua.Template = "util_each({0}, {1})"
        public static void ForEach(object table, Action<object, object> visit) => throw new NotImplementedException();

        /// <summary>
        /// Typed form of <see cref="ForEach(object, Action{object, object})"/> for the tables
        /// produced by <see cref="Storage.Entities"/>, <see cref="Storage.Strings"/> and
        /// <see cref="Storage.Numbers"/>, so the lambda sees a typed value instead of <c>object</c>.
        /// </summary>
        /// <typeparam name="TValue">The value type the table holds.</typeparam>
        /// <param name="table">The table to walk.</param>
        /// <param name="visit">Called as <c>(key, value)</c> for every entry.</param>
        /// @CSharpLua.Template = "util_each({0}, {1})"
        public static void ForEach<TValue>(LuaTable<string, TValue> table, Action<string, TValue> visit) => throw new NotImplementedException();

        /// <summary>
        /// Visits every entry in sorted key order, for output that must be reproducible (log lines,
        /// test expectations). Keys must be all strings or all numbers.
        /// </summary>
        /// <param name="table">The table to walk.</param>
        /// <param name="visit">Called as <c>(key, value)</c> in ascending key order.</param>
        /// @CSharpLua.Template = "util_each_sorted({0}, {1})"
        public static void ForEachSorted(object table, Action<object, object> visit) => throw new NotImplementedException();

        /// <summary>
        /// Visits the values of a 1-based array, stopping at the first missing index:
        /// <c>for _, v in ipairs(t)</c>.
        /// </summary>
        /// <param name="array">A <see cref="LuaArray{T}"/> or any sequence-like table.</param>
        /// <param name="visit">Called as <c>(value)</c> for every element.</param>
        /// @CSharpLua.Template = "util_each_array({0}, {1})"
        public static void ForEachArray(object array, Action<object> visit) => throw new NotImplementedException();

        /// <summary>
        /// Number of entries a <c>pairs</c> loop would visit. Correct for sparse tables, where the
        /// <c>#</c> operator is undefined.
        /// </summary>
        /// <param name="table">The table to count.</param>
        /// <returns>The number of key/value pairs.</returns>
        /// @CSharpLua.Template = "util_count({0})"
        public static double Count(object table) => throw new NotImplementedException();

        /// <summary>
        /// A shallow copy of a table. Use it to snapshot values the game may mutate, such as a
        /// fluidbox reading, before storing or comparing them.
        /// </summary>
        /// <param name="table">The table to copy.</param>
        /// <returns>A new table with the same entries.</returns>
        /// @CSharpLua.Template = "util_copy({0})"
        public static LuaTable<string, object> Copy(object table) => throw new NotImplementedException();

        /// <summary>
        /// Joins the values of an array into one string. Numbers are formatted the way Lua's
        /// <c>tostring</c> does, so <c>1.0</c> becomes "1.0" - use <c>Numbers</c> to round first if
        /// the output is meant for players.
        /// </summary>
        /// <param name="array">The values to join.</param>
        /// <param name="separator">Text placed between the values.</param>
        /// <returns>The joined string.</returns>
        /// @CSharpLua.Template = "util_join({0}, {1})"
        public static string Join(object array, string separator) => throw new NotImplementedException();
    }
}
