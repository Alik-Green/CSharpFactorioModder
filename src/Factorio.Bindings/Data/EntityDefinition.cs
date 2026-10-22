using System;

namespace Factorio.Data
{
    /// <summary>
    /// An entity: something that exists on the map.
    /// <para>
    /// This is the part every machine shares - what it drops when mined, how much damage it takes,
    /// and its <see cref="CollisionBox"/> / <see cref="SelectionBox"/>.
    /// </para>
    /// </summary>
    public class EntityDefinition : PrototypeDefinition
    {
        private EntityFlag _flags = EntityFlag.None;

        private Func<EntityDefinition, PrototypeDefinition[]>? _companionFactory;

        /// <summary>Declares an entity of a given prototype type.</summary>
        /// <param name="type">Entity prototype type, e.g. <c>assembling-machine</c>.</param>
        /// <param name="name">Entity prototype name.</param>
        public EntityDefinition(string type, string name) : base(type, name)
        {
        }

        /// <summary>
        /// Sets both <see cref="CollisionBox"/> and <see cref="SelectionBox"/> cleanly for a square
        /// <paramref name="tiles"/> × <paramref name="tiles"/> entity (e.g. <c>SetSize(3)</c> for a 3×3 machine).
        /// </summary>
        public void SetSize(double tiles) => SetSize(tiles, tiles);

        /// <summary>
        /// Sets both <see cref="CollisionBox"/> and <see cref="SelectionBox"/> cleanly for a rectangular
        /// <paramref name="tilesWide"/> × <paramref name="tilesHigh"/> entity (e.g. <c>SetSize(2, 3)</c>).
        /// </summary>
        public void SetSize(double tilesWide, double tilesHigh)
        {
            CollisionBox = BoundingBox.Collision(tilesWide, tilesHigh);
            SelectionBox = BoundingBox.Selection(tilesWide, tilesHigh);
        }

        /// <summary>
        /// Marks this entity minable AND tells <see cref="ModBuilder.Add(PrototypeDefinition)"/> to
        /// automatically generate its matching <see cref="ItemDefinition"/> (<c>PlaceResult = Name</c>,
        /// inheriting icon, localised name/description, subgroup, and order) WITHOUT generating a recipe.
        /// Use <see cref="Craftable"/> when you also want the crafting recipe generated automatically.
        /// </summary>
        public void Placeable(double stackSize = 50, Subgroup? subgroup = null)
        {
            if (MinableResult == null)
                MinableResult = Name;

            _companionFactory = self =>
            {
                var item = new ItemDefinition(self.Name)
                {
                    PlaceResult = self.Name,
                    StackSize = stackSize,
                };
                if (self.Icon != null) item.Icon = self.Icon;
                if (self.Icons != null) item.Icons = self.Icons;
                if (self.IconSize > 0) item.IconSize = self.IconSize;
                if (subgroup != null) item.Subgroup = subgroup;
                if (self.Order != null) item.Order = self.Order;
                if (self.LocalisedName != null) item.LocalisedName = self.LocalisedName;
                if (self.LocalisedDescription != null) item.LocalisedDescription = self.LocalisedDescription;
                return new PrototypeDefinition[] { item };
            };
        }

        /// <summary>
        /// All-in-one QoL helper: marks this machine minable AND tells <see cref="ModBuilder.Add(PrototypeDefinition)"/>
        /// to automatically generate its matching <see cref="ItemDefinition"/> (inheriting icon,
        /// localised name/description, subgroup, order, and <c>PlaceResult = Name</c>) plus its
        /// <see cref="RecipeDefinition"/>!
        /// <code>
        /// Craftable(
        ///     ingredients: [Vanilla.Items.IronPlate * 10, Vanilla.Items.ElectronicCircuit * 5],
        ///     stackSize: 10,
        ///     subgroup: Subgroup.ExtractionMachine,
        ///     energyRequired: 2.0,
        ///     enabled: false);
        /// </code>
        /// </summary>
        public void Craftable(
            RecipeValueList ingredients,
            double stackSize = 50,
            Subgroup? subgroup = null,
            double energyRequired = 0.5,
            bool enabled = true,
            CategoryList? categories = null,
            double resultCount = 1)
        {
            if (MinableResult == null)
                MinableResult = Name;

            _companionFactory = self =>
            {
                var item = new ItemDefinition(self.Name)
                {
                    PlaceResult = self.Name,
                    StackSize = stackSize,
                };
                if (self.Icon != null) item.Icon = self.Icon;
                if (self.Icons != null) item.Icons = self.Icons;
                if (self.IconSize > 0) item.IconSize = self.IconSize;
                if (subgroup != null) item.Subgroup = subgroup;
                if (self.Order != null) item.Order = self.Order;
                if (self.LocalisedName != null) item.LocalisedName = self.LocalisedName;
                if (self.LocalisedDescription != null) item.LocalisedDescription = self.LocalisedDescription;

                var recipe = new RecipeDefinition(self.Name)
                {
                    Enabled = enabled,
                    EnergyRequired = energyRequired,
                    Ingredients = ingredients,
                    Results = [Product.Item(self.Name, resultCount)],
                };
                if (categories != null) recipe.Categories = categories;
                if (self.LocalisedName != null) recipe.LocalisedName = self.LocalisedName;
                if (self.LocalisedDescription != null) recipe.LocalisedDescription = self.LocalisedDescription;

                return new PrototypeDefinition[] { item, recipe };
            };
        }

        internal PrototypeDefinition[] CreateCompanionDefinitions() =>
            _companionFactory == null ? Array.Empty<PrototypeDefinition>() : _companionFactory(this);

        /// <summary>
        /// The item this entity drops when mined: e.g. <c>ItemRef.Of("atmospheric-condenser")</c> or
        /// <c>"atmospheric-condenser"</c>. Automatically sets <see cref="MiningTime"/> to <c>0.5</c> if unset.
        /// </summary>
        public ItemId? MinableResult
        {
            get => GetNested("minable", "result") as string;
            set
            {
                SetNested("minable", "result", value?.Name);
                if (value != null && GetNested("minable", "mining_time") == null)
                    SetNested("minable", "mining_time", 0.5);
            }
        }

        /// <summary>How long mining takes, in seconds.</summary>
        public double MiningTime
        {
            get => GetNested("minable", "mining_time") is { } value
                ? Convert.ToDouble(value, System.Globalization.CultureInfo.InvariantCulture)
                : 0;
            set => SetNested("minable", "mining_time", value);
        }

        /// <summary>
        /// The entity this one upgrades to. Setting it to <c>null</c> clears an inherited vanilla upgrade path.
        /// </summary>
        public EntityId? NextUpgrade
        {
            get => Get("next_upgrade") as string;
            set => Set("next_upgrade", value?.Name);
        }

        /// <summary>Hit points.</summary>
        public double MaxHealth
        {
            get => Number("max_health");
            set => Set("max_health", value);
        }

        /// <summary>
        /// Entity flags (e.g. <c>EntityFlag.PlaceableNeutral | EntityFlag.PlaceablePlayer | EntityFlag.PlayerCreation</c>).
        /// </summary>
        public EntityFlag Flags
        {
            get => _flags;
            set
            {
                _flags = value;
                Set("flags", FlagList.Of(value));
            }
        }

        /// <summary>
        /// Solid collision area relative to the entity's centre (in tiles):
        /// <list type="bullet">
        /// <item><description><c>new BoundingBox(-1.2, -1.2, 1.2, 1.2)</c> — explicit <c>(left, top, right, bottom)</c></description></item>
        /// <item><description><c>new BoundingBox(2, 3)</c> — centered <c>width × height</c> in tiles</description></item>
        /// <item><description><c>BoundingBox.Collision(2, 3)</c> or <c>BoundingBox.Machine3x3</c> — standard Factorio machine inset</description></item>
        /// </list>
        /// </summary>
        public BoundingBox? CollisionBox
        {
            get => Get("collision_box") as BoundingBox;
            set => Set("collision_box", value);
        }

        /// <summary>
        /// Yellow selection rectangle shown when hovering the entity (in tiles):
        /// e.g. <c>BoundingBox.Selection(3, 3)</c>, <c>BoundingBox.Tiles3x3</c>, <c>new BoundingBox(2, 3)</c>,
        /// or <c>new BoundingBox(-1.5, -1.5, 1.5, 1.5)</c>.
        /// </summary>
        public BoundingBox? SelectionBox
        {
            get => Get("selection_box") as BoundingBox;
            set => Set("selection_box", value);
        }

        /// <summary>Extra vertical drawing headroom above the entity (in tiles) for tall sprites.</summary>
        public double DrawingBoxVerticalExtension
        {
            get => Number("drawing_box_vertical_extension");
            set => Set("drawing_box_vertical_extension", value);
        }

        /// <summary>
        /// Remnant entity spawned when destroyed (e.g. <see cref="EntityId.BigRemnants"/>,
        /// <see cref="EntityId.MediumRemnants"/>, or <c>EntityRef.Of("big-remnants")</c>).
        /// </summary>
        public EntityId? Corpse
        {
            get => Get("corpse") as string;
            set => Set("corpse", value?.Name);
        }

        /// <summary>Group name allowing machines of the same family to fast-replace one another.</summary>
        public string? FastReplaceableGroup
        {
            get => Get("fast_replaceable_group") as string;
            set => Set("fast_replaceable_group", value);
        }

        /// <summary>Custom colour drawn on the Factorio minimap/map view.</summary>
        public Rgb? MapColor
        {
            get => Get("map_color") as Rgb;
            set => Set("map_color", value);
        }

        /// <summary>Custom friendly-force colour drawn on the Factorio minimap/map view.</summary>
        public Rgb? FriendlyMapColor
        {
            get => Get("friendly_map_color") as Rgb;
            set => Set("friendly_map_color", value);
        }

        /// <summary>Reads a field of a nested table, if it was assigned.</summary>
        protected object? GetNested(string parent, string name) =>
            Get(parent) is NestedTable nested ? nested.Get(name) : null;

        /// <summary>Sets a field of a nested table, creating that table on first use.</summary>
        protected void SetNested(string parent, string name, object? value)
        {
            if (Get(parent) is not NestedTable nested)
            {
                nested = new NestedTable();
                Set(parent, nested);
            }
            nested.Set(name, value);
        }

        /// <summary>A prototype table built field by field (<c>minable</c>, etc.).</summary>
        protected sealed class NestedTable : ILuaValue
        {
            private readonly System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<string, object?>> _fields = new();

            /// <summary>Assigns a field.</summary>
            public void Set(string name, object? value)
            {
                for (var i = 0; i < _fields.Count; i++)
                {
                    if (_fields[i].Key != name) continue;
                    _fields[i] = new System.Collections.Generic.KeyValuePair<string, object?>(name, value);
                    return;
                }
                _fields.Add(new System.Collections.Generic.KeyValuePair<string, object?>(name, value));
            }

            /// <summary>Reads a field that was assigned.</summary>
            public object? Get(string name)
            {
                foreach (var field in _fields)
                    if (field.Key == name) return field.Value;
                return null;
            }

            /// <summary>Writes the table.</summary>
            public void Write(LuaWriter writer)
            {
                writer.BeginTable();
                foreach (var field in _fields)
                {
                    if (field.Value == null) continue;
                    writer.Key(field.Key);
                    writer.Value(field.Value);
                }
                writer.EndTable();
            }
        }
    }
}
