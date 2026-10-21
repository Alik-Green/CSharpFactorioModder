using System;
using Factorio.Runtime;

namespace Factorio
{
    /// <summary>
    /// The mod's persistent data, i.e. the native <c>storage</c> table.
    /// <para>
    /// <c>storage</c> is saved with the game and shared by every event handler, which makes it the
    /// place for anything a mod must remember between ticks or between save/load cycles. It starts
    /// out empty. Values must be plain data (numbers, strings, booleans, tables): functions,
    /// userdata and references to local variables cannot be serialised and will make the save fail.
    /// </para>
    /// <para>
    /// Reading <c>storage</c> in <c>on_load</c> is allowed; writing to it there is not.
    /// </para>
    /// </summary>
    /// @CSharpLua.Ignore
    public static class Storage
    {
        /// <summary>The whole persistent table: <c>storage</c>.</summary>
        /// @CSharpLua.Template = "storage"
        public static LuaTable<string, object> Data { get; }

        /// <summary>
        /// Returns <c>storage[key]</c>, creating an empty table the first time so callers never
        /// have to nil-check. A value that is not a table is replaced, so a corrupted or
        /// old-format section cannot turn into an indexing error later. Use numbers as keys for a
        /// list, strings for a named section.
        /// </summary>
        /// <param name="key">Name of the section inside <c>storage</c>.</param>
        /// <returns>The section table; never nil.</returns>
        /// @CSharpLua.Template = "util_ensure_table(storage, {0})"
        public static LuaTable<string, object> Table(string key) => throw new NotImplementedException();


        /// <summary>
        /// <c>storage[key]</c> holding entities, created on first use. Entity references are
        /// saved by the game; they turn invalid (<see cref="Entities.IsValid"/>) when the entity
        /// is removed, so a registry needs no separate bookkeeping.
        /// </summary>
        /// <param name="key">Name of the section inside <c>storage</c>.</param>
        /// <returns>The section table, typed for entities; never nil.</returns>
        /// @CSharpLua.Template = "util_ensure_table(storage, {0})"
        public static LuaTable<string, Entity> Entities(string key) => throw new NotImplementedException();

        /// <summary><c>storage[key]</c> holding strings, created on first use.</summary>
        /// <param name="key">Name of the section inside <c>storage</c>.</param>
        /// <returns>The section table, typed for strings; never nil.</returns>
        /// @CSharpLua.Template = "util_ensure_table(storage, {0})"
        public static LuaTable<string, string> Strings(string key) => throw new NotImplementedException();

        /// <summary>
        /// <c>storage[key]</c> holding numbers, created on first use. Read a possibly-missing
        /// value through <see cref="Numbers.OrZero"/> before doing arithmetic on it.
        /// </summary>
        /// <param name="key">Name of the section inside <c>storage</c>.</param>
        /// <returns>The section table, typed for numbers; never nil.</returns>
        /// @CSharpLua.Template = "util_ensure_table(storage, {0})"
        public static LuaTable<string, double> Numbers(string key) => throw new NotImplementedException();

        /// <summary>
        /// A saved collection of entities with typed per-entity values, addressed by unit number.
        /// <para>
        /// This is the shape most runtime state takes - "these machines exist, and each one has a
        /// setting and a counter" - and unlike <see cref="Entities"/> or <see cref="Slot{T}"/> the
        /// registry owns the values too, so a machine that goes away takes its data with it.
        /// </para>
        /// </summary>
        /// <param name="key">Name of the section inside <c>storage</c>.</param>
        /// <returns>A registry addressing that section; never <c>null</c>.</returns>
        /// @CSharpLua.Template = "{0}"
        public static EntityRegistry Registry(string key) => throw new NotImplementedException();

        /// <summary>
        /// One typed value in a key of its own, for settings that belong to the save rather than to
        /// a single entity (a preferred gas, a version stamp, the last player to run a command).
        /// </summary>
        /// <typeparam name="T">What the setting holds.</typeparam>
        /// <param name="key">Name of the key inside <c>storage</c>.</param>
        /// <returns>A handle addressing that key; never <c>null</c>.</returns>
        /// @CSharpLua.Template = "{0}"
        public static Setting<T> Setting<T>(string key) => throw new NotImplementedException();

        /// <summary>
        /// A typed handle on one section of <c>storage</c>. Prefer this to the methods above when
        /// the same section is used from more than one place: the slot is a name, so it can live in
        /// a <c>static readonly</c> field.
        /// </summary>
        /// <typeparam name="T">What the section holds.</typeparam>
        /// <param name="key">Name of the section inside <c>storage</c>.</param>
        /// <returns>A slot addressing that section.</returns>
        /// @CSharpLua.Template = "{0}"
        public static StorageSlot<T> Slot<T>(string key) => throw new NotImplementedException();

        /// <summary>Removes a section from <c>storage</c>, freeing the save file's memory for it.</summary>
        /// <param name="key">Name of the section to drop.</param>
        /// @CSharpLua.Template = "storage[{0}] = nil"
        public static void Remove(string key) => throw new NotImplementedException();
    }
}
