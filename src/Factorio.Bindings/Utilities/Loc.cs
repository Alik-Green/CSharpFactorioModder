using System;
using Factorio.Runtime;

namespace Factorio
{
    /// <summary>
    /// Builds the "localised string" tables the Factorio API accepts everywhere a user-visible text
    /// is expected: <c>game.print</c>, GUI captions, tooltips, entity names.
    /// <para>
    /// A localised string is <c>{ "key" }</c> or <c>{ "key", arg1, arg2 }</c>, where the arguments
    /// are substituted for <c>__1__</c>, <c>__2__</c> in the locale entry. Using one instead of a
    /// hard-coded English sentence is what lets a mod be translated by its players.
    /// </para>
    /// </summary>
    /// @CSharpLua.Ignore
    public static class Loc
    {
        /// <summary>A localised string with no arguments: <c>{ "key" }</c>.</summary>
        /// <param name="key">Locale key, e.g. <c>item-name.iron-plate</c>.</param>
        /// <returns>The localised string table.</returns>
        /// @CSharpLua.Template = "{ {0} }"
        public static object Key(object key) => throw new NotImplementedException();

        /// <summary>A localised string with one argument: <c>{ "key", value }</c>.</summary>
        /// <param name="key">Locale key containing <c>__1__</c>.</param>
        /// <param name="value">Value substituted for <c>__1__</c>; may itself be a localised string.</param>
        /// <returns>The localised string table.</returns>
        /// @CSharpLua.Template = "{ {0}, {1} }"
        public static object With(object key, object value) => throw new NotImplementedException();

        /// <summary>A localised string with two arguments: <c>{ "key", a, b }</c>.</summary>
        /// <param name="key">Locale key containing <c>__1__</c> and <c>__2__</c>.</param>
        /// <param name="a">Value substituted for <c>__1__</c>.</param>
        /// <param name="b">Value substituted for <c>__2__</c>.</param>
        /// <returns>The localised string table.</returns>
        /// @CSharpLua.Template = "{ {0}, {1}, {2} }"
        public static object With(object key, object a, object b) => throw new NotImplementedException();
    }
}
