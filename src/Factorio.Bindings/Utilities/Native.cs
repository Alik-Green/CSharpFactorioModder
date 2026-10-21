using System;
using Factorio.Runtime;

namespace Factorio
{
    /// <summary>
    /// Escape hatch to the native Lua object behind a C# value: read a field, write a field, or
    /// call a method by its Lua name.
    /// <para>
    /// The generated bindings cover the whole API, so this is rarely needed. It is here for the
    /// cases where a name is only known at run time, and as the fallback the generator itself uses
    /// when a member cannot be expressed as a plain property: the Lua stays in
    /// <c>FactorioUtils.lua</c> and the C# side is an ordinary function call with string
    /// arguments, instead of a template that contains Lua syntax.
    /// </para>
    /// <para>
    /// Keys are the names the game uses, not the C# names: <c>Native.Get(entity, "unit_number")</c>
    /// is <c>entity.unit_number</c>.
    /// </para>
    /// </summary>
    /// @CSharpLua.Ignore
    public static class Native
    {
        /// <summary>Reads <c>target[key]</c>.</summary>
        /// <param name="target">The native table or object; may be <c>null</c>.</param>
        /// <returns>The value, or <c>null</c> when the field (or the target) is absent.</returns>
        /// @CSharpLua.Template = "util_get({0}, {1})"
        public static object Get(object target, string key) => throw new NotImplementedException();

        /// <summary>Writes <c>target[key] = value</c>.</summary>
        /// <param name="target">The native table or object.</param>
        /// <param name="key">The Lua field name.</param>
        /// <param name="value">The new value; must not be the literal <c>null</c>.</param>
        /// @CSharpLua.Template = "util_set({0}, {1}, {2})"
        public static void Set(object target, string key, object value) => throw new NotImplementedException();

        /// <summary>Removes a field: <c>target[key] = nil</c>. A no-op when <paramref name="target"/> is <c>null</c>.</summary>
        /// <param name="target">The native table or object; may be <c>null</c>.</param>
        /// <param name="key">The Lua field name.</param>
        /// @CSharpLua.Template = "util_unset({0}, {1})"
        public static void Remove(object target, string key) => throw new NotImplementedException();

        /// <summary>Calls <c>target[key]()</c>.</summary>
        /// <param name="target">The native table or object.</param>
        /// <param name="method">The Lua method name.</param>
        /// <returns>Whatever the method returns.</returns>
        /// @CSharpLua.Template = "util_call({0}, {1})"
        public static object Call(object target, string method) => throw new NotImplementedException();

        /// <summary>Calls <c>target[key](a)</c>.</summary>
        /// <param name="target">The native table or object.</param>
        /// <param name="method">The Lua method name.</param>
        /// <param name="a">First argument.</param>
        /// <returns>Whatever the method returns.</returns>
        /// @CSharpLua.Template = "util_call({0}, {1}, {2})"
        public static object Call(object target, string method, object a) => throw new NotImplementedException();

        /// <summary>Calls <c>target[key](a, b)</c>.</summary>
        /// <param name="target">The native table or object.</param>
        /// <param name="method">The Lua method name.</param>
        /// <param name="a">First argument.</param>
        /// <param name="b">Second argument.</param>
        /// <returns>Whatever the method returns.</returns>
        /// @CSharpLua.Template = "util_call({0}, {1}, {2}, {3})"
        public static object Call(object target, string method, object a, object b) => throw new NotImplementedException();

        /// <summary>Builds a one-entry native table: <c>{ [key] = value }</c>.</summary>
        /// <param name="key">First key.</param>
        /// <param name="value">First value.</param>
        /// <returns>A new Lua table.</returns>
        /// @CSharpLua.Template = "util_table({0}, {1})"
        public static LuaTable<string, object> Table(string key, object value) => throw new NotImplementedException();

        /// <summary>Builds a two-entry native table, the shape most API parameters use.</summary>
        /// <param name="key1">First key.</param>
        /// <param name="value1">First value.</param>
        /// <param name="key2">Second key.</param>
        /// <param name="value2">Second value.</param>
        /// <returns>A new Lua table.</returns>
        /// @CSharpLua.Template = "util_table({0}, {1}, {2}, {3})"
        public static LuaTable<string, object> Table(string key1, object value1, string key2, object value2) =>
            throw new NotImplementedException();

        /// <summary>Builds a three-entry native table.</summary>
        /// <param name="key1">First key.</param>
        /// <param name="value1">First value.</param>
        /// <param name="key2">Second key.</param>
        /// <param name="value2">Second value.</param>
        /// <param name="key3">Third key.</param>
        /// <param name="value3">Third value.</param>
        /// <returns>A new Lua table.</returns>
        /// @CSharpLua.Template = "util_table({0}, {1}, {2}, {3}, {4}, {5})"
        public static LuaTable<string, object> Table(
            string key1, object value1, string key2, object value2, string key3, object value3) =>
            throw new NotImplementedException();
    }
}
