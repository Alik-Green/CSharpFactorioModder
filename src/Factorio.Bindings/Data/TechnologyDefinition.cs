using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Factorio.Data
{
    /// <summary>
    /// A research technology in Factorio's tech tree that unlocks recipes when researched.
    /// <code>
    /// public sealed class AtmosphericCondensationTech : TechnologyDefinition
    /// {
    ///     public AtmosphericCondensationTech() : base("atmospheric-condensation")
    ///     {
    ///         LocalisedName = "Atmospheric Condensation";
    ///         Icon = Asset.Mod("graphics/technology/atmospheric-condensation.png");
    ///         Prerequisites = ["fluid-handling"];
    ///         Effects = [TechEffect.UnlockRecipe("atmospheric-condenser")];
    ///         Count = 100;
    ///         Time = 30;
    ///         SciencePacks = [SciencePack.Automation, SciencePack.Logistic];
    ///     }
    /// }
    /// </code>
    /// </summary>
    public class TechnologyDefinition : PrototypeDefinition
    {
        /// <summary>Declares a research technology with the given prototype name.</summary>
        public TechnologyDefinition(string name) : base("technology", name)
        {
        }

        /// <summary>Technology names that must be researched before this one becomes available.</summary>
        public StringList? Prerequisites
        {
            get => Get("prerequisites") as StringList;
            set => Set("prerequisites", value);
        }

        /// <summary>Effects granted when researched (e.g. <c>[TechEffect.UnlockRecipe("atmospheric-condenser")]</c>).</summary>
        public TechEffectList? Effects
        {
            get => Get("effects") as TechEffectList;
            set => Set("effects", value);
        }

        /// <summary>Number of research cycles required to complete the technology.</summary>
        public int Count
        {
            get => GetUnit("count") is int v ? v : 0;
            set => SetUnit("count", value);
        }

        /// <summary>Seconds each research cycle takes in a lab (commonly <c>15</c>, <c>30</c>, or <c>60</c>).</summary>
        public double Time
        {
            get => GetUnit("time") is double v ? v : 0;
            set => SetUnit("time", value);
        }

        /// <summary>Science packs consumed per cycle (e.g. <c>[SciencePack.Automation, SciencePack.Logistic]</c>).</summary>
        public SciencePackList? SciencePacks
        {
            get => GetUnit("ingredients") as SciencePackList;
            set => SetUnit("ingredients", value);
        }

        private object? GetUnit(string key) =>
            Get("unit") is UnitTable u ? u.Get(key) : null;

        private void SetUnit(string key, object? value)
        {
            if (Get("unit") is not UnitTable u)
            {
                u = new UnitTable();
                Set("unit", u);
            }
            u.Set(key, value);
        }

        private sealed class UnitTable : ILuaValue
        {
            private readonly List<KeyValuePair<string, object?>> _fields = new();

            public void Set(string key, object? value)
            {
                for (var i = 0; i < _fields.Count; i++)
                {
                    if (_fields[i].Key != key) continue;
                    _fields[i] = new KeyValuePair<string, object?>(key, value);
                    return;
                }
                _fields.Add(new KeyValuePair<string, object?>(key, value));
            }

            public object? Get(string key)
            {
                foreach (var f in _fields)
                    if (f.Key == key) return f.Value;
                return null;
            }

            public void Write(LuaWriter writer)
            {
                writer.BeginTable();
                foreach (var f in _fields)
                {
                    if (f.Value == null) continue;
                    writer.Key(f.Key);
                    writer.Value(f.Value);
                }
                writer.EndTable();
            }
        }
    }

    /// <summary>One technology research effect (such as unlocking a recipe).</summary>
    public sealed class TechEffect : ILuaValue
    {
        private readonly string _recipe;

        private TechEffect(string recipe) => _recipe = recipe;

        /// <summary>Unlocks <paramref name="recipe"/> when the technology completes.</summary>
        public static TechEffect UnlockRecipe(RecipeId recipe) => new(recipe.Name);

        /// <inheritdoc />
        public void Write(LuaWriter writer)
        {
            writer.BeginTable();
            writer.Key("type");
            writer.Value("unlock-recipe");
            writer.Key("recipe");
            writer.Value(_recipe);
            writer.EndTable();
        }
    }

    /// <summary>One science-pack cost per research cycle (e.g. <see cref="Automation"/>).</summary>
    public sealed class SciencePack : ILuaValue
    {
        private readonly string _name;
        private readonly int _amount;

        /// <summary>Creates a science pack ingredient.</summary>
        public SciencePack(string name, int amount = 1)
        {
            _name = name;
            _amount = amount;
        }

        /// <summary>Red Automation science pack (<c>"automation-science-pack"</c>).</summary>
        public static readonly SciencePack Automation = new("automation-science-pack", 1);

        /// <summary>Green Logistic science pack (<c>"logistic-science-pack"</c>).</summary>
        public static readonly SciencePack Logistic = new("logistic-science-pack", 1);

        /// <summary>Grey Military science pack (<c>"military-science-pack"</c>).</summary>
        public static readonly SciencePack Military = new("military-science-pack", 1);

        /// <summary>Blue Chemical science pack (<c>"chemical-science-pack"</c>).</summary>
        public static readonly SciencePack Chemical = new("chemical-science-pack", 1);

        /// <summary>Purple Production science pack (<c>"production-science-pack"</c>).</summary>
        public static readonly SciencePack Production = new("production-science-pack", 1);

        /// <summary>Yellow Utility science pack (<c>"utility-science-pack"</c>).</summary>
        public static readonly SciencePack Utility = new("utility-science-pack", 1);

        /// <summary>White Space science pack (<c>"space-science-pack"</c>).</summary>
        public static readonly SciencePack Space = new("space-science-pack", 1);

        /// <inheritdoc />
        public void Write(LuaWriter writer)
        {
            writer.BeginArray();
            writer.Value(_name);
            writer.Value(_amount);
            writer.EndArray();
        }
    }

    /// <summary>Collection of string names for <see cref="TechnologyDefinition.Prerequisites"/>.</summary>
    [CollectionBuilder(typeof(StringList), nameof(Create))]
    public sealed class StringList : ILuaValue, IEnumerable<string>
    {
        private readonly List<string> _items = new();

        /// <summary>Builds from <c>["fluid-handling", ...]</c>.</summary>
        public static StringList Create(ReadOnlySpan<string> items)
        {
            var list = new StringList();
            foreach (var item in items) list._items.Add(item);
            return list;
        }

        /// <inheritdoc />
        public void Write(LuaWriter writer)
        {
            writer.BeginArray();
            foreach (var item in _items) writer.Value(item);
            writer.EndArray();
        }

        /// <inheritdoc />
        public IEnumerator<string> GetEnumerator() => _items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    /// <summary>Collection of <see cref="TechEffect"/> entries.</summary>
    [CollectionBuilder(typeof(TechEffectList), nameof(Create))]
    public sealed class TechEffectList : ILuaValue, IEnumerable<TechEffect>
    {
        private readonly List<TechEffect> _items = new();

        /// <summary>Builds from <c>[TechEffect.UnlockRecipe(...), ...]</c>.</summary>
        public static TechEffectList Create(ReadOnlySpan<TechEffect> items)
        {
            var list = new TechEffectList();
            foreach (var item in items) list._items.Add(item);
            return list;
        }

        /// <inheritdoc />
        public void Write(LuaWriter writer)
        {
            writer.BeginArray();
            foreach (var item in _items) writer.Value(item);
            writer.EndArray();
        }

        /// <inheritdoc />
        public IEnumerator<TechEffect> GetEnumerator() => _items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    /// <summary>Collection of <see cref="SciencePack"/> entries.</summary>
    [CollectionBuilder(typeof(SciencePackList), nameof(Create))]
    public sealed class SciencePackList : ILuaValue, IEnumerable<SciencePack>
    {
        private readonly List<SciencePack> _items = new();

        /// <summary>Builds from <c>[SciencePack.Automation, SciencePack.Logistic]</c>.</summary>
        public static SciencePackList Create(ReadOnlySpan<SciencePack> items)
        {
            var list = new SciencePackList();
            foreach (var item in items) list._items.Add(item);
            return list;
        }

        /// <inheritdoc />
        public void Write(LuaWriter writer)
        {
            writer.BeginArray();
            foreach (var item in _items) writer.Value(item);
            writer.EndArray();
        }

        /// <inheritdoc />
        public IEnumerator<SciencePack> GetEnumerator() => _items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
