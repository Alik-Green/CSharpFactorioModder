using System;
using Factorio.Runtime;

namespace Factorio
{
    /// <summary>
    /// One typed value per machine, owned by an <see cref="EntityRegistry"/>.
    /// <para>
    /// Created by <see cref="EntityRegistry.Value{T}"/> and kept in a <c>static readonly</c> field;
    /// it is a name, like the registry itself, so reading and writing costs one table lookup. Values
    /// are keyed by the machine's unit number, which means a handle stays correct across save/load
    /// and never has to be rebuilt.
    /// </para>
    /// <code>
    /// private static readonly EntityValue&lt;double&gt; Crafted = Machines.Value&lt;double&gt;("crafted");
    ///
    /// Crafted.Set(machine, Crafted.Get(machine) + Gases.FluidPerCraft);
    /// </code>
    /// </summary>
    /// <typeparam name="T">What is recorded per machine.</typeparam>
    /// @CSharpLua.Ignore
    public sealed class EntityValue<T>
    {
        /// <summary>The storage section this value lives in: <c>&lt;registry&gt;/&lt;name&gt;</c>.</summary>
        /// @CSharpLua.Get = "{this}"
        public string Key => throw new NotImplementedException();

        /// <summary>
        /// The value recorded for a machine.
        /// <para>
        /// A machine with no value yet answers <c>null</c> (and <c>0</c> for a number). That is the
        /// same answer a handle gives for a machine that was never in the registry, so a lookup is
        /// always safe - but it does mean the result has to be treated as possibly absent, e.g.
        /// through <see cref="Numbers.OrZero"/> before arithmetic.
        /// </para>
        /// </summary>
        /// <param name="entity">The machine to read.</param>
        /// <returns>The recorded value, or the type's default.</returns>
        /// @CSharpLua.Template = "util_value_get(storage, {this}, {0})"
        public T Get(Entity entity) => throw new NotImplementedException();

        /// <summary>Records a value for a machine, creating the section on first write.</summary>
        /// <param name="entity">The machine to write.</param>
        /// <param name="value">The value to record.</param>
        /// @CSharpLua.Template = "util_value_set(storage, {this}, {0}, {1})"
        public void Set(Entity entity, T value) => throw new NotImplementedException();

        /// <summary>Whether a value has been recorded for this machine.</summary>
        /// <param name="entity">The machine to look for.</param>
        /// <returns><c>true</c> when a value is stored.</returns>
        /// @CSharpLua.Template = "util_value_has(storage, {this}, {0})"
        public bool Has(Entity entity) => throw new NotImplementedException();

        /// <summary>
        /// Removes the value for one machine, leaving the rest alone. Removing a machine from the
        /// registry already does this; the method is for values that expire while the machine stays.
        /// </summary>
        /// <param name="entity">The machine whose value to drop.</param>
        /// @CSharpLua.Template = "util_value_remove(storage, {this}, {0})"
        public void Remove(Entity entity) => throw new NotImplementedException();
    }
}
