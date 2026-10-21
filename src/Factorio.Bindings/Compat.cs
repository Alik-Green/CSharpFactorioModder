// Hand-written CSharp.lua compatibility layer for the Factorio bindings.
//
// Everything here is COMPILE-TIME ONLY: the classes are marked `@CSharpLua.Ignore`, so no Lua
// class table is emitted for them, and every member carries a `@CSharpLua.Template` that expands
// to a native Lua expression at the call site. Nothing in this file allocates a wrapper object,
// a proxy, or a C# collection at runtime -- the values flowing through these types are the plain
// Lua tables Factorio itself returns.
//
// Verified against CSharp.lua commit 790d3cf (see config/toolchain.json):
//   * templates on members of an ignored *generic* class are honoured,
//   * a template may be a whole statement (`{this}[{0}] = {1}`),
//   * `#{this}` expands to a Lua length operator.
// Counter-examples that must NOT be reintroduced (they emit CoreSystem calls):
//   * `new LuaArray<T>()`      -> generic instantiation in System.import (module does not exist)
//   * `(LuaEntity)x`           -> System.cast
//   * `x as LuaEntity`         -> identity in Lua (documented, no runtime type check)
//   * auto-property templates -> ignored by the compiler, use a method instead
//   * `foreach (var x in arr)` -> System.each
using System;

namespace Factorio
{
    /// <summary>
    /// Marker base class for every native Factorio object (game, player, entity, force, ...).
    /// Compile-time only: a value of this type <em>is</em> the game's own Lua table, so no wrapper
    /// is ever allocated and none of these types appear in the emitted Lua.
    /// </summary>
    /// @CSharpLua.Ignore
    public abstract class LuaObject
    {
    }

    /// <summary>
    /// A Lua array returned by the Factorio API (1-based, plain table). Compile-time view only.
    /// </summary>
    /// @CSharpLua.Ignore
    public class LuaArray<T>
    {
        /// <summary>Array element at a 1-based index: <c>arr[i]</c>.</summary>
        /// @CSharpLua.Template = "{this}[{0}]"
        public T Get(double index) => throw new NotImplementedException();

        /// <summary>Writes an element: <c>arr[i] = value</c>.</summary>
        /// @CSharpLua.Template = "{this}[{0}] = {1}"
        public void Set(double index, T value) => throw new NotImplementedException();

        /// <summary>Number of elements: <c>#arr</c>.</summary>
        /// @CSharpLua.Template = "#{this}"
        public double Length() => throw new NotImplementedException();
    }

    /// <summary>
    /// A Lua "custom table" (dictionary with arbitrary keys, e.g. <c>game.forces</c>).
    /// Compile-time view only.
    /// </summary>
    /// @CSharpLua.Ignore
    public class LuaCustomTable<TKey, TValue>
    {
        /// <summary>Value for a key: <c>t[key]</c>.</summary>
        /// @CSharpLua.Template = "{this}[{0}]"
        public TValue Get(TKey key) => throw new NotImplementedException();

        /// <summary>Writes a key: <c>t[key] = value</c>.</summary>
        /// @CSharpLua.Template = "{this}[{0}] = {1}"
        public void Set(TKey key, TValue value) => throw new NotImplementedException();

        /// <summary>Number of entries, via Factorio's global <c>table_size</c> (correct for sparse tables).</summary>
        /// @CSharpLua.Template = "table_size({this})"
        public double Count() => throw new NotImplementedException();

        /// <summary>Removes a key: <c>t[key] = nil</c>.</summary>
        /// @CSharpLua.Template = "{this}[{0}] = nil"
        public void Remove(TKey key) => throw new NotImplementedException();
    }

    /// <summary>
    /// A plain Lua table with string keys, e.g. <c>script.active_mods</c>.
    /// Compile-time view only.
    /// </summary>
    /// @CSharpLua.Ignore
    public class LuaTable<TKey, TValue>
    {
        /// <summary>Value for a key: <c>t[key]</c>.</summary>
        /// @CSharpLua.Template = "{this}[{0}]"
        public TValue Get(TKey key) => throw new NotImplementedException();

        /// <summary>Writes a key: <c>t[key] = value</c>.</summary>
        /// @CSharpLua.Template = "{this}[{0}] = {1}"
        public void Set(TKey key, TValue value) => throw new NotImplementedException();

        /// <summary>Number of entries, via Factorio's global <c>table_size</c>.</summary>
        /// @CSharpLua.Template = "table_size({this})"
        public double Count() => throw new NotImplementedException();

        /// <summary>Removes a key: <c>t[key] = nil</c>.</summary>
        /// @CSharpLua.Template = "{this}[{0}] = nil"
        public void Remove(TKey key) => throw new NotImplementedException();
    }
}
