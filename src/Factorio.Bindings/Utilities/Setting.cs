using System;
using Factorio.Runtime;

namespace Factorio
{
    /// <summary>
    /// One typed value in one storage key: <c>storage[key]</c>.
    /// <para>
    /// The registry and its <see cref="EntityValue{T}"/> handle take care of "one value per
    /// machine"; a setting is the same idea for the whole save - the gas a mod switches to, the
    /// last player who used a command, a version stamp. It is a name, so it lives in a
    /// <c>static readonly</c> field and costs no table.
    /// </para>
    /// <code>
    /// private static readonly Setting&lt;string&gt; PreferredGas = Storage.Setting&lt;string&gt;("gas-extractor.preferred-gas");
    ///
    /// PreferredGas.Set("light-oil");
    /// var gas = PreferredGas.Get();     // null until something is stored
    /// </code>
    /// </summary>
    /// <typeparam name="T">What the setting holds.</typeparam>
    /// @CSharpLua.Ignore
    public sealed class Setting<T>
    {
        /// <summary>The key inside <c>storage</c>.</summary>
        /// @CSharpLua.Get = "{this}"
        public string Key => throw new NotImplementedException();

        /// <summary>
        /// The stored value, or <c>null</c> (0 for a number) when nothing has been stored yet.
        /// A new save therefore needs no initialisation pass: the first read is the default.
        /// </summary>
        /// <returns>The value, or the type's default.</returns>
        /// @CSharpLua.Template = "util_setting_get(storage, {this})"
        public T Get() => throw new NotImplementedException();

        /// <summary>Stores a value, replacing any previous one.</summary>
        /// <param name="value">The value to store.</param>
        /// @CSharpLua.Template = "util_setting_set(storage, {this}, {0})"
        public void Set(T value) => throw new NotImplementedException();

        /// <summary>Whether a value has ever been stored.</summary>
        /// <returns><c>true</c> when the key exists.</returns>
        /// @CSharpLua.Template = "util_setting_has(storage, {this})"
        public bool Has() => throw new NotImplementedException();

        /// <summary>Forgets the value; the next <see cref="Get"/> answers the default again.</summary>
        /// @CSharpLua.Template = "util_setting_clear(storage, {this})"
        public void Clear() => throw new NotImplementedException();
    }
}
