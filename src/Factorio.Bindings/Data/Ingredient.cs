using System;

namespace Factorio.Data
{
    /// <summary>
    /// One ingredient or product of a recipe: <c>{ type = "item", name = ..., amount = ... }</c>.
    /// <para>
    /// The same shape serves both sides of a recipe, which is why the two factories below produce
    /// the same type - they differ only in what they read like at the call site.
    /// </para>
    /// </summary>
    public sealed class RecipeValue : ILuaValue
    {
        private readonly string _kind;
        private readonly string _name;
        private readonly double _amount;

        private RecipeValue(string kind, string name, double amount)
        {
            _kind = kind;
            _name = name;
            _amount = amount;
        }

        /// <summary>An item ingredient.</summary>
        /// <param name="name">Item prototype name.</param>
        /// <param name="amount">How many are consumed per craft.</param>
        /// <returns>The ingredient.</returns>
        public static RecipeValue Item(string name, double amount) => new("item", name, amount);

        /// <summary>A fluid ingredient.</summary>
        /// <param name="name">Fluid prototype name.</param>
        /// <param name="amount">How many units are consumed per craft.</param>
        /// <returns>The ingredient.</returns>
        public static RecipeValue Fluid(string name, double amount) => new("fluid", name, amount);

        /// <summary>Writes the ingredient table.</summary>
        /// <param name="writer">The writer.</param>
        public void Write(LuaWriter writer)
        {
            writer.BeginTable();
            writer.Key("type");
            writer.Value(_kind);
            writer.Key("name");
            writer.Value(_name);
            writer.Key("amount");
            writer.Value(_amount);
            writer.EndTable();
        }
    }

    /// <summary>
    /// What a recipe consumes. <c>Ingredient.Item(name, amount)</c> and
    /// <c>Ingredient.Fluid(name, amount)</c>.
    /// </summary>
    public static class Ingredient
    {
        /// <summary>An item ingredient, counted in items.</summary>
        /// <param name="name">Item prototype name.</param>
        /// <param name="amount">How many are consumed per craft.</param>
        /// <returns>The ingredient.</returns>
        public static RecipeValue Item(string name, double amount) => RecipeValue.Item(name, amount);

        /// <summary>A fluid ingredient, counted in fluid units.</summary>
        /// <param name="name">Fluid prototype name.</param>
        /// <param name="amount">How many units are consumed per craft.</param>
        /// <returns>The ingredient.</returns>
        public static RecipeValue Fluid(string name, double amount) => RecipeValue.Fluid(name, amount);
    }

    /// <summary>
    /// What a recipe produces. <c>Product.Item(name, amount)</c> and
    /// <c>Product.Fluid(name, amount)</c>.
    /// </summary>
    public static class Product
    {
        /// <summary>An item product.</summary>
        /// <param name="name">Item prototype name.</param>
        /// <param name="amount">How many are produced per craft.</param>
        /// <returns>The product.</returns>
        public static RecipeValue Item(string name, double amount) => RecipeValue.Item(name, amount);

        /// <summary>A fluid product, counted in fluid units.</summary>
        /// <param name="name">Fluid prototype name.</param>
        /// <param name="amount">How many units are produced per craft.</param>
        /// <returns>The product.</returns>
        public static RecipeValue Fluid(string name, double amount) => RecipeValue.Fluid(name, amount);
    }

    /// <summary>Bounding boxes, in the short form Factorio accepts.</summary>
    public sealed class Bounds : ILuaValue
    {
        private readonly double _left;
        private readonly double _top;
        private readonly double _right;
        private readonly double _bottom;

        private Bounds(double left, double top, double right, double bottom)
        {
            _left = left;
            _top = top;
            _right = right;
            _bottom = bottom;
        }

        /// <summary>Builds <c>{ { left, top }, { right, bottom } }</c>.</summary>
        /// <param name="left">Left edge, typically negative.</param>
        /// <param name="top">Top edge, typically negative.</param>
        /// <param name="right">Right edge, typically positive.</param>
        /// <param name="bottom">Bottom edge, typically positive.</param>
        /// <returns>The bounding box.</returns>
        public static Bounds Of(double left, double top, double right, double bottom) =>
            new(left, top, right, bottom);

        /// <summary>Writes the box.</summary>
        /// <param name="writer">The writer.</param>
        public void Write(LuaWriter writer)
        {
            writer.BeginArray();
            WritePoint(writer, _left, _top);
            WritePoint(writer, _right, _bottom);
            writer.EndArray();
        }

        private static void WritePoint(LuaWriter writer, double x, double y)
        {
            writer.BeginArray();
            writer.Value(x);
            writer.Value(y);
            writer.EndArray();
        }
    }
}
