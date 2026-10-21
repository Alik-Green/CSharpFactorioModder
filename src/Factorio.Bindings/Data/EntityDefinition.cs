using System;

namespace Factorio.Data
{
    /// <summary>
    /// An entity: something that exists on the map.
    /// <para>
    /// This is the part every machine shares - what it drops when mined, how much punishment it
    /// takes, where its collision box is. The concrete machine type adds its own fields.
    /// </para>
    /// </summary>
    public class EntityDefinition : PrototypeDefinition
    {
        private EntityFlag _flags = EntityFlag.None;

        /// <summary>Declares an entity of a given prototype type.</summary>
        /// <param name="type">Entity prototype type, e.g. <c>assembling-machine</c>.</param>
        /// <param name="name">Entity prototype name.</param>
        public EntityDefinition(string type, string name) : base(type, name)
        {
        }

        /// <summary>
        /// The item this entity drops when mined: <c>ItemRef.Of("demo-assembler")</c>.
        /// <para>
        /// Setting it also gives <c>minable</c> the mining time the game requires (0.5 s, unless
        /// <see cref="MiningTime"/> says otherwise): the game rejects a <c>minable</c> table without
        /// one, and a partial table would not be filled in by inheritance either - inheritance
        /// works one top-level field at a time.
        /// </para>
        /// </summary>
        public string? MinableResult
        {
            get => GetNested("minable", "result") as string;
            set
            {
                SetNested("minable", "result", value);
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
        /// The entity this one upgrades to. Setting it to <c>null</c> is meaningful: a machine built
        /// on a vanilla prototype would otherwise inherit the vanilla upgrade path.
        /// </summary>
        public string? NextUpgrade
        {
            get => Get("next_upgrade") as string;
            set => Set("next_upgrade", value);
        }

        /// <summary>Hit points.</summary>
        public double MaxHealth
        {
            get => Number("max_health");
            set => Set("max_health", value);
        }

        /// <summary>
        /// What can be done with the entity. A machine the player builds wants
        /// <c>EntityFlag.PlaceableNeutral | EntityFlag.PlaceablePlayer | EntityFlag.PlayerCreation</c>,
        /// which is what <see cref="AssemblingMachineDefinition"/> starts from.
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
        /// The area the entity occupies, as <c>Bounds.Of(left, top, right, bottom)</c>. Pipe
        /// connections have to lie inside it, and the build checks that they do.
        /// </summary>
        public object? CollisionBox
        {
            get => Get("collision_box");
            set => Set("collision_box", value);
        }

        /// <summary>The area the player can click, as <c>Bounds.Of(...)</c>; usually a little larger.</summary>
        public object? SelectionBox
        {
            get => Get("selection_box");
            set => Set("selection_box", value);
        }

        /// <summary>Particle and sound effects played when the entity is destroyed.</summary>
        public string? Corpse
        {
            get => Get("corpse") as string;
            set => Set("corpse", value);
        }

        /// <summary>Reads a field of a nested table, if it was assigned.</summary>
        /// <param name="parent">The nested table's field name.</param>
        /// <param name="name">Field inside the nested table.</param>
        /// <returns>The value, or null.</returns>
        protected object? GetNested(string parent, string name) =>
            Get(parent) is NestedTable nested ? nested.Get(name) : null;

        /// <summary>Sets a field of a nested table, creating that table on first use.</summary>
        /// <param name="parent">The nested table's field name.</param>
        /// <param name="name">Field inside the nested table.</param>
        /// <param name="value">The value.</param>
        protected void SetNested(string parent, string name, object? value)
        {
            if (Get(parent) is not NestedTable nested)
            {
                nested = new NestedTable();
                Set(parent, nested);
            }
            nested.Set(name, value);
        }

        /// <summary>
        /// A prototype table built field by field, for the nested tables a definition writes into
        /// (<c>minable</c>, <c>energy_source</c>).
        /// </summary>
        protected sealed class NestedTable : ILuaValue
        {
            private readonly System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<string, object?>> _fields = new();

            /// <summary>Assigns a field.</summary>
            /// <param name="name">Field name.</param>
            /// <param name="value">The value.</param>
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
            /// <param name="name">Field name.</param>
            /// <returns>The value, or null.</returns>
            public object? Get(string name)
            {
                foreach (var field in _fields)
                    if (field.Key == name) return field.Value;
                return null;
            }

            /// <summary>Writes the table.</summary>
            /// <param name="writer">The writer.</param>
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
