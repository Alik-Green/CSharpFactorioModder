using System;
namespace Factorio
{
    /// <summary>
    /// One named section of <see cref="Storage"/>, typed by the values it holds.
    /// <para>
    /// A slot is the section's name and nothing else: in Lua it <em>is</em> the string, so a field
    /// of this type costs no table and no metatable. That makes it usable as a
    /// <c>static readonly</c> field:
    /// </para>
    /// <code>
    /// private static readonly StorageSlot&lt;Entity&gt; Machines = Storage.Slot&lt;Entity&gt;("machines");
    ///
    /// Machines.Set(unit, entity);
    /// if (Machines.Has(unit)) { ... }
    /// </code>
    /// </summary>
    /// <typeparam name="T">What the section holds: entities, strings or numbers.</typeparam>
    /// @CSharpLua.Ignore
    public sealed class StorageSlot<T>
    {
        /// <summary>The section name, i.e. the key inside <c>storage</c>.</summary>
        /// @CSharpLua.Get = "{this}"
        public string Key => throw new NotImplementedException();

        /// <summary>The section as a table, created on first use.</summary>
        /// @CSharpLua.Get = "util_ensure_table(storage, {this})"
        public LuaTable<string, T> Table => throw new NotImplementedException();

        /// <summary>Reads one entry, or <c>default</c> when the slot has no such key.</summary>
        /// <param name="key">Entry key, normally an entity's unit number as a string.</param>
        /// <returns>The value, or <c>null</c>/<c>0</c> when absent.</returns>
        /// @CSharpLua.Template = "util_slot_get(storage, {this}, {0})"
        public T Get(string key) => throw new NotImplementedException();

        /// <summary>Writes one entry, creating the section if this is its first value.</summary>
        /// <param name="key">Entry key.</param>
        /// <param name="value">Value to store.</param>
        /// @CSharpLua.Template = "util_slot_set(storage, {this}, {0}, {1})"
        public void Set(string key, T value) => throw new NotImplementedException();

        /// <summary>Removes one entry, leaving the section in place.</summary>
        /// <param name="key">Entry key.</param>
        /// @CSharpLua.Template = "util_slot_remove(storage, {this}, {0})"
        public void Remove(string key) => throw new NotImplementedException();

        /// <summary>Whether the slot has an entry for this key.</summary>
        /// <param name="key">Entry key.</param>
        /// <returns><c>true</c> when a value is stored.</returns>
        /// @CSharpLua.Template = "util_slot_has(storage, {this}, {0})"
        public bool Has(string key) => throw new NotImplementedException();

        /// <summary>How many entries the section holds (correct for sparse keys).</summary>
        /// <returns>The number of entries, or 0 when the section does not exist yet.</returns>
        /// @CSharpLua.Template = "util_slot_count(storage, {this})"
        public double Count() => throw new NotImplementedException();

        /// <summary>Drops the whole section, freeing its memory in the save file.</summary>
        /// @CSharpLua.Template = "util_slot_clear(storage, {this})"
        public void Clear() => throw new NotImplementedException();
    }
}
