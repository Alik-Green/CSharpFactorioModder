using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Factorio.Data
{
    /// <summary>
    /// One ingredient or product of a recipe: <c>{ type = "item"|"fluid", name = ..., amount = ... }</c>.
    /// </summary>
    public sealed class RecipeValue : ILuaValue
    {
        private readonly string _kind;
        private readonly string _name;
        private readonly double? _amount;
        private readonly double? _amountMin;
        private readonly double? _amountMax;
        private readonly double? _probability;
        private readonly double? _temperature;

        private RecipeValue(
            string kind,
            string name,
            double? amount,
            double? amountMin = null,
            double? amountMax = null,
            double? probability = null,
            double? temperature = null)
        {
            _kind = kind;
            _name = name;
            _amount = amount;
            _amountMin = amountMin;
            _amountMax = amountMax;
            _probability = probability;
            _temperature = temperature;
        }

        /// <summary>An item ingredient or product.</summary>
        public static RecipeValue Item(string name, double amount, double? probability = null) =>
            new("item", name, amount, probability: probability);

        /// <summary>An item product yielding a random range between <paramref name="min"/> and <paramref name="max"/>.</summary>
        public static RecipeValue ItemRange(string name, double min, double max, double? probability = null) =>
            new("item", name, amount: null, amountMin: min, amountMax: max, probability: probability);

        /// <summary>A fluid ingredient or product.</summary>
        public static RecipeValue Fluid(string name, double amount, double? temperature = null, double? probability = null) =>
            new("fluid", name, amount, probability: probability, temperature: temperature);

        /// <summary>Writes the ingredient or product table.</summary>
        public void Write(LuaWriter writer)
        {
            writer.BeginTable();
            writer.Key("type");
            writer.Value(_kind);
            writer.Key("name");
            writer.Value(_name);
            if (_amount.HasValue)
            {
                writer.Key("amount");
                writer.Value(_amount.Value);
            }
            if (_amountMin.HasValue)
            {
                writer.Key("amount_min");
                writer.Value(_amountMin.Value);
            }
            if (_amountMax.HasValue)
            {
                writer.Key("amount_max");
                writer.Value(_amountMax.Value);
            }
            if (_probability.HasValue)
            {
                writer.Key("probability");
                writer.Value(_probability.Value);
            }
            if (_temperature.HasValue)
            {
                writer.Key("temperature");
                writer.Value(_temperature.Value);
            }
            writer.EndTable();
        }
    }

    /// <summary>
    /// Strongly-typed collection of recipe ingredients or products (<c>[Ingredient.Item(...), Ingredient.Fluid(...)]</c>).
    /// </summary>
    [CollectionBuilder(typeof(RecipeValueList), nameof(Create))]
    public sealed class RecipeValueList : ILuaValue, IEnumerable<RecipeValue>
    {
        private readonly List<object?> _items = new();

        /// <summary>Builds a list from a C# collection expression <c>[...]</c>.</summary>
        public static RecipeValueList Create(ReadOnlySpan<RecipeValue> items)
        {
            var list = new RecipeValueList();
            foreach (var item in items) list._items.Add(item);
            return list;
        }

        /// <summary>Allows assigning a legacy <see cref="Values"/> collection seamlessly.</summary>
        public static implicit operator RecipeValueList?(Values? values)
        {
            if (values == null) return null;
            var list = new RecipeValueList();
            foreach (var item in values) list._items.Add(item);
            return list;
        }

        /// <summary>Builds from any sequence of <see cref="RecipeValue"/>.</summary>
        public static RecipeValueList From(IEnumerable<RecipeValue> items)
        {
            var list = new RecipeValueList();
            foreach (var item in items) list._items.Add(item);
            return list;
        }

        /// <summary>Number of entries.</summary>
        public int Count => _items.Count;

        /// <inheritdoc />
        public void Write(LuaWriter writer)
        {
            writer.BeginArray();
            foreach (var item in _items) writer.Value(item);
            writer.EndArray();
        }

        /// <inheritdoc />
        public IEnumerator<RecipeValue> GetEnumerator()
        {
            foreach (var item in _items)
            {
                if (item is RecipeValue rv) yield return rv;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    /// <summary>
    /// Strongly-typed collection of <see cref="CraftingCategory"/> entries (<c>[CraftingCategory.Chemistry, "custom"]</c>).
    /// </summary>
    [CollectionBuilder(typeof(CategoryList), nameof(Create))]
    public sealed class CategoryList : ILuaValue, IEnumerable<CraftingCategory>
    {
        private readonly List<CraftingCategory> _items = new();

        /// <summary>Builds a list from a C# collection expression <c>[CraftingCategory.Chemistry, ...]</c>.</summary>
        public static CategoryList Create(ReadOnlySpan<CraftingCategory> items)
        {
            var list = new CategoryList();
            foreach (var item in items) list._items.Add(item);
            return list;
        }

        /// <summary>Allows assigning a legacy <see cref="Values"/> collection seamlessly.</summary>
        public static implicit operator CategoryList?(Values? values)
        {
            if (values == null) return null;
            var list = new CategoryList();
            foreach (var item in values)
            {
                if (item is CraftingCategory cc) list._items.Add(cc);
                else if (item is string s) list._items.Add(CraftingCategory.Of(s));
            }
            return list;
        }

        /// <summary>Number of categories.</summary>
        public int Count => _items.Count;

        /// <inheritdoc />
        public void Write(LuaWriter writer)
        {
            writer.BeginArray();
            foreach (var item in _items) writer.Value(item.Name);
            writer.EndArray();
        }

        /// <inheritdoc />
        public IEnumerator<CraftingCategory> GetEnumerator() => _items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    /// <summary>
    /// What a recipe consumes: <c>Ingredient.Item(name, amount)</c> and <c>Ingredient.Fluid(name, amount)</c>.
    /// </summary>
    public static class Ingredient
    {
        /// <summary>An item ingredient, counted in items (e.g. <c>Ingredient.Item(Vanilla.Items.ElectronicCircuit, 5)</c>).</summary>
        public static RecipeValue Item(string name, double amount) => RecipeValue.Item(name, amount);

        /// <summary>A fluid ingredient, counted in fluid units (e.g. <c>Ingredient.Fluid(Vanilla.Fluids.Water, 50)</c>).</summary>
        public static RecipeValue Fluid(string name, double amount) => RecipeValue.Fluid(name, amount);

        /// <summary>Typed item ingredient: <c>Ingredient.Of(Vanilla.Items.ElectronicCircuit, 5)</c>.</summary>
        public static RecipeValue Of(ItemId item, double amount) => RecipeValue.Item(item.Name, amount);

        /// <summary>Typed fluid ingredient: <c>Ingredient.Of(Vanilla.Fluids.Water, 50)</c>.</summary>
        public static RecipeValue Of(FluidId fluid, double amount) => RecipeValue.Fluid(fluid.Name, amount);
    }

    /// <summary>
    /// What a recipe produces: <c>Product.Item(name, amount)</c>, <c>Product.ItemRange(...)</c>,
    /// and <c>Product.Fluid(name, amount)</c>.
    /// </summary>
    public static class Product
    {
        /// <summary>An item product.</summary>
        public static RecipeValue Item(string name, double amount) => RecipeValue.Item(name, amount);

        /// <summary>An item product with a probability between 0 and 1 (e.g. <c>0.25</c> for 25% chance).</summary>
        public static RecipeValue Item(string name, double amount, double probability) =>
            RecipeValue.Item(name, amount, probability: probability);

        /// <summary>An item product giving between <paramref name="min"/> and <paramref name="max"/> items.</summary>
        public static RecipeValue ItemRange(string name, double min, double max, double probability = 1.0) =>
            RecipeValue.ItemRange(name, min, max, probability < 1.0 ? probability : null);

        /// <summary>A fluid product, counted in fluid units.</summary>
        public static RecipeValue Fluid(string name, double amount) => RecipeValue.Fluid(name, amount);

        /// <summary>A fluid product with an explicit output temperature or probability.</summary>
        public static RecipeValue Fluid(string name, double amount, double? temperature = null, double? probability = null) =>
            RecipeValue.Fluid(name, amount, temperature: temperature, probability: probability);

        /// <summary>Typed item product: <c>Product.Of(Vanilla.Items.ElectronicCircuit, 2)</c>.</summary>
        public static RecipeValue Of(ItemId item, double amount, double? probability = null) =>
            RecipeValue.Item(item.Name, amount, probability: probability);

        /// <summary>Typed fluid product: <c>Product.Of(Vanilla.Fluids.Water, 50)</c>.</summary>
        public static RecipeValue Of(FluidId fluid, double amount, double? temperature = null, double? probability = null) =>
            RecipeValue.Fluid(fluid.Name, amount, temperature: temperature, probability: probability);
    }

    /// <summary>
    /// Strongly-typed Factorio bounding box (<c>{{ left, top }, { right, bottom }}</c>) for
    /// <see cref="EntityDefinition.CollisionBox"/> and <see cref="EntityDefinition.SelectionBox"/>.
    /// <code>
    /// // 1. Explicit corners (left, top, right, bottom):
    /// CollisionBox = new BoundingBox(-1.2, -1.2, 1.2, 1.2);
    ///
    /// // 2. Centered width x height in tiles (e.g. 2 wide by 3 tall -> -1.0, -1.5, 1.0, 1.5):
    /// SelectionBox = new BoundingBox(2, 3);
    ///
    /// // 3. Standard Factorio machine grid helpers (insets CollisionBox by 0.3 tiles for walking &amp; pipes):
    /// CollisionBox = BoundingBox.Collision(2, 3);   // (-0.7, -1.2, 0.7, 1.2)
    /// SelectionBox = BoundingBox.Selection(2, 3);   // (-1.0, -1.5, 1.0, 1.5)
    /// </code>
    /// </summary>
    public sealed class BoundingBox : ILuaValue
    {
        /// <summary>Creates a bounding box centered on <c>(0, 0)</c> with total size <paramref name="width"/> × <paramref name="height"/> tiles.</summary>
        /// <param name="width">Total horizontal span in tiles (e.g. <c>2</c> gives <c>-1 .. +1</c>).</param>
        /// <param name="height">Total vertical span in tiles (e.g. <c>3</c> gives <c>-1.5 .. +1.5</c>).</param>
        public BoundingBox(double width, double height)
            : this(-width / 2.0, -height / 2.0, width / 2.0, height / 2.0)
        {
        }

        /// <summary>Creates a bounding box from explicit corner coordinates <c>(left, top, right, bottom)</c> in tiles.</summary>
        /// <param name="left">Left edge (negative X, west of centre).</param>
        /// <param name="top">Top edge (negative Y, north of centre).</param>
        /// <param name="right">Right edge (positive X, east of centre).</param>
        /// <param name="bottom">Bottom edge (positive Y, south of centre).</param>
        public BoundingBox(double left, double top, double right, double bottom)
        {
            Left = Math.Round(left, 4);
            Top = Math.Round(top, 4);
            Right = Math.Round(right, 4);
            Bottom = Math.Round(bottom, 4);
        }

        /// <summary>Left X coordinate (west).</summary>
        public double Left { get; }

        /// <summary>Top Y coordinate (north).</summary>
        public double Top { get; }

        /// <summary>Right X coordinate (east).</summary>
        public double Right { get; }

        /// <summary>Bottom Y coordinate (south).</summary>
        public double Bottom { get; }

        /// <summary>Total width in tiles (<c>Right - Left</c>).</summary>
        public double Width => Right - Left;

        /// <summary>Total height in tiles (<c>Bottom - Top</c>).</summary>
        public double Height => Bottom - Top;

        /// <summary>Standard Factorio collision box for a 1×1 entity (<c>-0.4, -0.4, 0.4, 0.4</c>).</summary>
        public static BoundingBox Machine1x1 => new(-0.4, -0.4, 0.4, 0.4);

        /// <summary>Standard Factorio collision box for a 2×2 entity (<c>-0.9, -0.9, 0.9, 0.9</c>).</summary>
        public static BoundingBox Machine2x2 => Collision(2, 2, margin: 0.1);

        /// <summary>Standard Factorio collision box for a 3×3 machine (<c>-1.2, -1.2, 1.2, 1.2</c>).</summary>
        public static BoundingBox Machine3x3 => Collision(3, 3);

        /// <summary>Standard Factorio collision box for a 4×4 machine (<c>-1.7, -1.7, 1.7, 1.7</c>).</summary>
        public static BoundingBox Machine4x4 => Collision(4, 4);

        /// <summary>Standard Factorio collision box for a 5×5 machine (<c>-2.2, -2.2, 2.2, 2.2</c>).</summary>
        public static BoundingBox Machine5x5 => Collision(5, 5);

        /// <summary>Full-tile 1×1 selection box (<c>-0.5, -0.5, 0.5, 0.5</c>).</summary>
        public static BoundingBox Tiles1x1 => Selection(1, 1);

        /// <summary>Full-tile 2×2 selection box (<c>-1.0, -1.0, 1.0, 1.0</c>).</summary>
        public static BoundingBox Tiles2x2 => Selection(2, 2);

        /// <summary>Full-tile 3×3 selection box (<c>-1.5, -1.5, 1.5, 1.5</c>).</summary>
        public static BoundingBox Tiles3x3 => Selection(3, 3);

        /// <summary>Full-tile 4×4 selection box (<c>-2.0, -2.0, 2.0, 2.0</c>).</summary>
        public static BoundingBox Tiles4x4 => Selection(4, 4);

        /// <summary>Full-tile 5×5 selection box (<c>-2.5, -2.5, 2.5, 2.5</c>).</summary>
        public static BoundingBox Tiles5x5 => Selection(5, 5);

        /// <summary>Explicit corner coordinates <c>(left, top, right, bottom)</c>.</summary>
        public static BoundingBox Of(double left, double top, double right, double bottom) =>
            new(left, top, right, bottom);

        /// <summary>Explicit corner coordinates <c>(left, top, right, bottom)</c>.</summary>
        public static BoundingBox Corners(double left, double top, double right, double bottom) =>
            new(left, top, right, bottom);

        /// <summary>Centered box of exact <paramref name="width"/> × <paramref name="height"/> tiles.</summary>
        public static BoundingBox Size(double width, double height) =>
            new(width, height);

        /// <summary>Centered square box of <paramref name="size"/> × <paramref name="size"/> tiles.</summary>
        public static BoundingBox Square(double size) =>
            new(size, size);

        /// <summary>
        /// Standard Factorio machine collision box for a <paramref name="tilesWide"/> × <paramref name="tilesHigh"/>
        /// building, inset from the tile edges by <paramref name="margin"/> (default <c>0.3</c>, e.g. <c>3×3 -> -1.2..1.2</c>).
        /// </summary>
        public static BoundingBox Collision(double tilesWide, double tilesHigh, double margin = 0.3)
        {
            var halfW = Math.Max(0.1, (tilesWide / 2.0) - margin);
            var halfH = Math.Max(0.1, (tilesHigh / 2.0) - margin);
            return new BoundingBox(-halfW, -halfH, halfW, halfH);
        }

        /// <summary>Full-tile selection box for a <paramref name="tilesWide"/> × <paramref name="tilesHigh"/> building.</summary>
        public static BoundingBox Selection(double tilesWide, double tilesHigh) =>
            new(tilesWide, tilesHigh);

        /// <summary>Implicit conversion from a 4-tuple <c>(left, top, right, bottom)</c>.</summary>
        public static implicit operator BoundingBox((double left, double top, double right, double bottom) box) =>
            new(box.left, box.top, box.right, box.bottom);

        /// <summary>Implicit conversion from a 2-tuple <c>(width, height)</c>.</summary>
        public static implicit operator BoundingBox((double width, double height) size) =>
            new(size.width, size.height);

        /// <inheritdoc />
        public void Write(LuaWriter writer)
        {
            writer.BeginArray();
            WritePoint(writer, Left, Top);
            WritePoint(writer, Right, Bottom);
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

    /// <summary>Backwards-compatible factory alias for <see cref="BoundingBox"/>.</summary>
    public static class Bounds
    {
        /// <summary>Builds <c>{ { left, top }, { right, bottom } }</c>.</summary>
        public static BoundingBox Of(double left, double top, double right, double bottom) =>
            BoundingBox.Of(left, top, right, bottom);

        /// <summary>Centered box of <paramref name="width"/> × <paramref name="height"/> tiles.</summary>
        public static BoundingBox Size(double width, double height) =>
            BoundingBox.Size(width, height);

        /// <summary>Factorio machine collision box for <paramref name="tilesWide"/> × <paramref name="tilesHigh"/> tiles.</summary>
        public static BoundingBox Collision(double tilesWide, double tilesHigh, double margin = 0.3) =>
            BoundingBox.Collision(tilesWide, tilesHigh, margin);

        /// <summary>Full-tile selection box for <paramref name="tilesWide"/> × <paramref name="tilesHigh"/> tiles.</summary>
        public static BoundingBox Selection(double tilesWide, double tilesHigh) =>
            BoundingBox.Selection(tilesWide, tilesHigh);
    }
}
