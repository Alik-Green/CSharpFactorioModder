using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Factorio.Data
{
    /// <summary>
    /// A native Lua array of prototype values.
    /// <para>
    /// Factorio's prototype tables are full of arrays - <c>ingredients</c>, <c>results</c>,
    /// <c>crafting_categories</c>, <c>pipe_connections</c> - and one is written either with a
    /// collection expression or with <see cref="Of(object)"/>:
    /// </para>
    /// <code>
    /// CraftingCategories = ["chemistry"],
    /// Ingredients        = [Ingredient.Item("iron-plate", 2), Ingredient.Fluid("demo-coolant", 25)],
    /// Results            = [Product.Item("demo-polymer-pellet", 1)],
    /// </code>
    /// <para>
    /// It is an ordinary C# collection, so LINQ, <c>foreach</c> and <c>Add</c> all work - unlike
    /// the runtime API, this code runs in the build, not inside Factorio.
    /// </para>
    /// </summary>
    [CollectionBuilder(typeof(Values), "Of")]
    public sealed class Values : ILuaValue, IEnumerable<object?>
    {
        private readonly List<object?> _items = new();

        /// <summary>Builds an array from a collection expression. Not called directly.</summary>
        /// <param name="items">The values.</param>
        /// <returns>The array.</returns>
        public static Values Of(ReadOnlySpan<object?> items)
        {
            var values = new Values();
            foreach (var item in items) values.Add(item);
            return values;
        }

        /// <summary>Builds an array from an existing sequence, e.g. a LINQ query.</summary>
        /// <param name="items">The values.</param>
        /// <returns>The array.</returns>
        public static Values From(IEnumerable<object?> items)
        {
            var values = new Values();
            foreach (var item in items) values.Add(item);
            return values;
        }

        /// <summary>Builds an array of one value.</summary>
        /// <param name="a">The value.</param>
        /// <returns>The array.</returns>
        public static Values Of(object? a) => Collect(a);

        /// <summary>Builds an array of two values.</summary>
        /// <param name="a">First value.</param>
        /// <param name="b">Second value.</param>
        /// <returns>The array.</returns>
        public static Values Of(object? a, object? b) => Collect(a, b);

        /// <summary>Builds an array of three values.</summary>
        /// <param name="a">First value.</param>
        /// <param name="b">Second value.</param>
        /// <param name="c">Third value.</param>
        /// <returns>The array.</returns>
        public static Values Of(object? a, object? b, object? c) => Collect(a, b, c);

        /// <summary>Builds an array of four values.</summary>
        /// <param name="a">First value.</param>
        /// <param name="b">Second value.</param>
        /// <param name="c">Third value.</param>
        /// <param name="d">Fourth value.</param>
        /// <returns>The array.</returns>
        public static Values Of(object? a, object? b, object? c, object? d) => Collect(a, b, c, d);

        /// <summary>Builds an array of five values.</summary>
        /// <param name="a">First value.</param>
        /// <param name="b">Second value.</param>
        /// <param name="c">Third value.</param>
        /// <param name="d">Fourth value.</param>
        /// <param name="e">Fifth value.</param>
        /// <returns>The array.</returns>
        public static Values Of(object? a, object? b, object? c, object? d, object? e) => Collect(a, b, c, d, e);

        /// <summary>Appends a value.</summary>
        /// <param name="value">The value to append; a <see cref="PrototypeName"/> becomes its string.</param>
        /// <returns>This array, so calls can be chained.</returns>
        public Values Add(object? value)
        {
            // A named value (Subgroup.Chemistry, CraftingCategory.Chemistry, SortOrder) is a string
            // with a type; the prototype table only ever wants the string.
            _items.Add(value is PrototypeName name ? name.Name : value);
            return this;
        }

        /// <summary>The number of values.</summary>
        /// <returns>The length.</returns>
        public int Count => _items.Count;

        /// <summary>Writes the array as a Lua table.</summary>
        /// <param name="writer">The writer.</param>
        public void Write(LuaWriter writer)
        {
            writer.BeginArray();
            foreach (var item in _items) writer.Value(item);
            writer.EndArray();
        }

        /// <summary>Enumerates the values.</summary>
        /// <returns>The values, in order.</returns>
        public IEnumerator<object?> GetEnumerator() => _items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private static Values Collect(params object?[] items) => From(items);
    }
}
