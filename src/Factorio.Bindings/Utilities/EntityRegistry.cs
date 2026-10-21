using System;
using Factorio.Runtime;

namespace Factorio
{
    /// <summary>
    /// A saved collection of entities, together with the typed values that belong to them.
    /// <para>
    /// This is the replacement for "a table in storage keyed by unit number" that every mod writes
    /// by hand. A registry addresses its entities by <c>unit_number</c> - stable for the lifetime of
    /// a save, never reused - and keeps its per-entity values in tables it owns, so removing a
    /// machine removes everything recorded about it.
    /// </para>
    /// <para>
    /// Entities it holds become invalid when the machine is mined, destroyed or replaced; the
    /// registry notices that while it iterates and drops those entries instead of handing a dead
    /// reference to the mod. Nothing has to be saved explicitly: a registry is plain data in
    /// <c>storage</c> and travels with the save.
    /// </para>
    /// <para>
    /// A registry is a name, exactly like <see cref="StorageSlot{T}"/>, so it can live in a
    /// <c>static readonly</c> field and costs no table of its own:
    /// </para>
    /// <code>
    /// private static readonly EntityRegistry Machines = Storage.Registry("gas-extractor.machines");
    /// private static readonly EntityValue&lt;string&gt; Gas = Machines.Value&lt;string&gt;("gas");
    ///
    /// Machines.Add(machine);
    /// Gas.Set(machine, "light-oil");
    /// Machines.ForEach(m =&gt; Gas.Set(m, "light-oil"));
    /// </code>
    /// <para>
    /// Creating a registry, a value handle or a setting is just keeping a name, so the
    /// <c>static readonly</c> fields above can be initialised whenever the module loads. Every
    /// member that actually touches <c>storage</c> - <see cref="Add"/>, <see cref="Remove"/>,
    /// <see cref="Contains"/>, <see cref="Count"/>, <see cref="ForEach"/>, <see cref="Clear"/> and
    /// all of <see cref="EntityValue{T}"/> - must run after the game has started: <c>on_init</c>,
    /// an event, or a command. Reading or writing <c>storage</c> while the save is loading (a
    /// module initialiser, <c>on_load</c>) is not allowed.
    /// </para>
    /// </summary>
    /// @CSharpLua.Ignore
    public sealed class EntityRegistry
    {
        /// <summary>The section name inside <c>storage</c> that holds the entities.</summary>
        /// @CSharpLua.Get = "{this}"
        public string Key => throw new NotImplementedException();

        /// <summary>
        /// One value per machine, kept in a table of its own that belongs to this registry: when a
        /// machine is removed - explicitly or because the game says it no longer exists - its value
        /// goes with it.
        /// <para>
        /// Call this once per value, normally from a <c>static readonly</c> field initialiser, and
        /// keep the returned handle. Names are used as storage keys, so two calls with the same
        /// name address the same data (which is what reconnecting to an existing save needs).
        /// </para>
        /// </summary>
        /// <typeparam name="T">What is recorded per machine: a <c>string</c>, a number or a boolean.</typeparam>
        /// <param name="name">Name of the value, unique inside this registry, e.g. <c>gas</c>.</param>
        /// <returns>A handle addressing that value for any machine in the registry.</returns>
        /// @CSharpLua.Template = "util_registry_value({this}, {0})"
        public EntityValue<T> Value<T>(string name) => throw new NotImplementedException();

        /// <summary>
        /// Adds a machine, or refreshes its entry if it is already registered. Entities without a
        /// unit number (most entities have one; a few, such as characters, do not) are ignored.
        /// </summary>
        /// <param name="entity">The machine to add.</param>
        /// @CSharpLua.Template = "util_registry_add(storage, {this}, {0})"
        public void Add(Entity entity) => throw new NotImplementedException();

        /// <summary>
        /// Forgets a machine and every value recorded for it. Safe to call for an entity that was
        /// never registered, and for one that no longer exists: both do nothing.
        /// </summary>
        /// <param name="entity">The machine to remove; may already be invalid.</param>
        /// @CSharpLua.Template = "util_registry_remove(storage, {this}, {0})"
        public void Remove(Entity entity) => throw new NotImplementedException();

        /// <summary>Whether the registry holds this entity and it still exists.</summary>
        /// <param name="entity">The machine to look for.</param>
        /// <returns><c>true</c> when the machine is registered and valid.</returns>
        /// @CSharpLua.Template = "util_registry_has(storage, {this}, {0})"
        public bool Contains(Entity entity) => throw new NotImplementedException();

        /// <summary>
        /// How many registered machines still exist. Entries whose machine is gone are not counted
        /// but are only dropped by <see cref="ForEach"/> (or <see cref="Clear"/>).
        /// </summary>
        /// <returns>The number of live machines.</returns>
        /// @CSharpLua.Template = "util_registry_count(storage, {this})"
        public double Count() => throw new NotImplementedException();

        /// <summary>
        /// Visits every registered machine that still exists, then drops the ones that do not -
        /// including their values. This is the one loop a mod needs: the alternative, walking the
        /// table by hand, is where dead references and leaked settings come from.
        /// <para>
        /// Do not <see cref="Add"/> or <see cref="Remove"/> machines inside the callback; the loop
        /// is over the live table. Changing a machine's <em>values</em> is fine.
        /// </para>
        /// </summary>
        /// <param name="visit">Called once per live machine.</param>
        /// @CSharpLua.Template = "util_registry_each(storage, {this}, {0})"
        public void ForEach(Action<Entity> visit) => throw new NotImplementedException();

        /// <summary>
        /// Forgets every machine and every value the registry owns. The section is removed from
        /// <c>storage</c> entirely, so it costs nothing in the save afterwards.
        /// </summary>
        /// @CSharpLua.Template = "util_registry_clear(storage, {this})"
        public void Clear() => throw new NotImplementedException();
    }
}
