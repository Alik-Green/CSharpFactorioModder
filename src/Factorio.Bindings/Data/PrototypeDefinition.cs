using System;
using System.Collections.Generic;
using System.Linq;

namespace Factorio.Data
{
    /// <summary>
    /// What every prototype definition has: a type, a name, and the fields shared by most prototype
    /// types.
    /// <para>
    /// A definition is built by assigning properties; whatever is assigned ends up in the prototype
    /// table, and whatever is left alone does not. That is deliberate - the game's own default, or
    /// the vanilla prototype's value when a definition inherits from one, only applies to fields the
    /// mod does not mention.
    /// </para>
    /// </summary>
    public abstract class PrototypeDefinition : ILuaValue
    {
        private readonly List<KeyValuePair<string, object?>> _fields = new();

        /// <summary>Starts a definition of a given prototype type.</summary>
        /// <param name="type">Prototype type as Factorio names it, e.g. <c>item</c> or <c>recipe</c>.</param>
        /// <param name="name">Prototype name, unique across all mods.</param>
        protected PrototypeDefinition(string type, string name)
        {
            Set("type", type);
            Set("name", name);
        }

        /// <summary>The prototype type, e.g. <c>assembling-machine</c>.</summary>
        public string Type => (string)Get("type")!;

        /// <summary>The prototype name, as Factorio and every reference to it use it.</summary>
        public string Name => (string)Get("name")!;

        /// <summary>
        /// The prototype's icon: <c>Asset.Base("graphics/icons/iron-plate.png")</c>, or a sprite
        /// table for an icon a build step rendered.
        /// </summary>
        public object? Icon
        {
            get => Get("icon");
            set => Set("icon", value);
        }

        /// <summary>Size of <see cref="Icon"/> in pixels. Factorio wants this with a plain path icon.</summary>
        public double IconSize
        {
            get => Number("icon_size");
            set => Set("icon_size", value);
        }

        /// <summary>
        /// The name the player sees, e.g. <c>"Chemical assembler"</c>.
        /// <para>
        /// A definition writes a localisation entry into the generated <c>data.lua</c>, merged into
        /// <c>locale/en/locale.cfg</c> at build time, so the name cannot drift from the prototype it
        /// belongs to and cannot be forgotten: the build warns about any visible prototype that has
        /// no name at all. Languages other than English go in the mod's own locale files, which are
        /// merged rather than replaced.
        /// </para>
        /// </summary>
        public string? LocalisedName
        {
            get => Localise("localised-name");
            set => SetLocalised("localised-name", value);
        }

        /// <summary>
        /// One line of tooltip text, from the same localisation entry as
        /// <see cref="LocalisedName"/>.
        /// </summary>
        public string? LocalisedDescription
        {
            get => Localise("localised-description");
            set => SetLocalised("localised-description", value);
        }

        /// <summary>Hides the prototype from the player's lists; recipes can still use it.</summary>

        public bool Hidden
        {
            get => Flag("hidden");
            set => Set("hidden", value);
        }

        /// <summary>
        /// Sort order inside the crafting menu or the prototype list:
        /// <c>SortOrder.At("b", "demo-chemical-assembler")</c> writes <c>b[demo-chemical-assembler]</c>.
        /// <para>
        /// Leave it unset and the prototype sorts by name, which is usually fine - but a machine
        /// dropped into a tab full of vanilla machines reads better with a letter that puts it where
        /// the player expects it.
        /// </para>
        /// </summary>
        public SortOrder? Order
        {
            get => Get("order") is string order ? SortOrder.Of(order) : null;
            set => Set("order", value);
        }

        /// <summary>
        /// The tab the prototype appears in: <c>Subgroup.ProductionMachine</c>, or a name of a tab
        /// this mod declares. An item with no subgroup lands in the game's "Other" tab.
        /// </summary>
        public Subgroup? Subgroup
        {
            get => Get("subgroup") is string subgroup ? Subgroup.Of(subgroup) : null;
            set => Set("subgroup", value);
        }

        /// <summary>
        /// The vanilla prototype this definition takes its unset fields from, or <c>null</c>.
        /// <para>
        /// Read by the toolchain: those values are copied when the data stage runs, so this is a
        /// relationship between prototypes rather than a field of this one.
        /// </para>
        /// </summary>
        public VanillaPrototype? InheritsFrom => Get("__inherit") as VanillaPrototype;

        /// <summary>
        /// Sets any prototype field by name, including ones this API does not model yet.
        /// <para>
        /// The escape hatch for a property that has no typed wrapper: the key is written as given,
        /// so <c>Field("emissions_per_minute", ...)</c> is the same as writing that field in
        /// <c>data.lua</c> by hand.
        /// </para>
        /// </summary>
        /// <param name="name">Lua field name.</param>
        /// <param name="value">The value; <c>null</c> means "not this field" for an inherited prototype.</param>
        public void Field(string name, object? value) => Set(name, value);

        /// <summary>
        /// Copies <paramref name="donor"/>'s field into this definition, for inheritance.
        /// </summary>
        /// <param name="donor">A definition whose field should be reused.</param>
        /// <param name="name">Field name.</param>
        public void CopyFieldFrom(PrototypeDefinition donor, string name) => Set(name, donor.Get(name));

        /// <summary>Writes the prototype table. Called by the toolchain, not by a mod.</summary>
        /// <param name="writer">The writer collecting the Lua.</param>
        public virtual void Write(LuaWriter writer)
        {
            writer.BeginTable();
            foreach (var field in _fields)
                WriteField(writer, field.Key, field.Value);
            WriteClearedFields(writer);
            writer.EndTable();
        }

        /// <summary>Writes one field; overridden to change a spelling for a version.</summary>
        /// <param name="writer">The writer.</param>
        /// <param name="name">Field name as C# declared it.</param>
        /// <param name="value">The value; null for "cleared".</param>
        protected virtual void WriteField(LuaWriter writer, string name, object? value)
        {
            if (value == null || value is LocalisedText) return;
            writer.Key(name);
            writer.Value(value);
        }

        /// <summary>Reads a field that was assigned, if any.</summary>
        /// <param name="name">Field name.</param>
        /// <returns>The value, or null.</returns>
        protected object? Get(string name)
        {
            foreach (var field in _fields)
                if (field.Key == name) return field.Value;
            return null;
        }

        /// <summary>Assigns a field, keeping the order fields were first written in.</summary>
        /// <param name="name">Field name.</param>
        /// <param name="value">The value, or null for "cleared".</param>
        protected void Set(string name, object? value)
        {
            // A named value is a string with a type; the prototype table only wants the string.
            if (value is PrototypeName prototypeName) value = prototypeName.Name;

            for (var i = 0; i < _fields.Count; i++)
            {
                if (_fields[i].Key != name) continue;
                _fields[i] = new KeyValuePair<string, object?>(name, value);
                return;
            }
            _fields.Add(new KeyValuePair<string, object?>(name, value));
        }

        /// <summary>Reads a numeric field, 0 when it was never assigned.</summary>
        /// <param name="name">Field name.</param>
        /// <returns>The number.</returns>
        protected double Number(string name)
        {
            var value = Get(name);
            return value == null ? 0 : Convert.ToDouble(value, System.Globalization.CultureInfo.InvariantCulture);
        }

        /// <summary>Reads a boolean field, false when it was never assigned.</summary>
        /// <param name="name">Field name.</param>
        /// <returns>The flag.</returns>
        protected bool Flag(string name) => Get(name) is true;

        /// <summary>
        /// The localisation entries this definition declares, keyed by the
        /// <c>kind/name</c> the build writes them under. Read by the toolchain.
        /// </summary>
        public IReadOnlyDictionary<string, string> Localisations
        {
            get
            {
                var entries = new Dictionary<string, string>(StringComparer.Ordinal);
                foreach (var field in _fields)
                {
                    if (field.Value is LocalisedText text)
                        entries[text.Kind] = text.Value;
                }
                return entries;
            }
        }

        /// <summary>Reads a declared localisation entry.</summary>
        /// <param name="kind">The entry kind, <c>localised-name</c> or <c>localised-description</c>.</param>
        /// <returns>The text, or null.</returns>
        private string? Localise(string kind) =>
            _fields.FirstOrDefault(f => f.Value is LocalisedText text && text.Kind == kind).Value is LocalisedText found
                ? found.Value
                : null;

        /// <summary>Declares a localisation entry.</summary>
        /// <param name="kind">The entry kind, <c>localised-name</c> or <c>localised-description</c>.</param>
        /// <param name="value">The English text.</param>
        private void SetLocalised(string kind, string? value)
        {
            if (value == null) return;
            Set(kind, new LocalisedText(kind, Name, value));
        }

        /// <summary>
        /// English text a definition declares about itself rather than a prototype field: it is
        /// written to the packaged <c>locale.cfg</c>, not into the prototype table, because the game
        /// reads a prototype's name from localisation, never from the data stage.
        /// </summary>
        private sealed class LocalisedText
        {
            public LocalisedText(string kind, string key, string value)
            {
                Kind = kind;
                Key = key;
                Value = value;
            }

            public string Kind { get; }

            public string Key { get; }

            public string Value { get; }
        }

        /// <summary>
        /// Records the fields that were explicitly set to <c>null</c>. A prototype that inherits
        /// from a vanilla one copies every field it did not mention, so "not this field" has to be
        /// written down somewhere: it goes into <c>__nil</c>, which the data stage removes before
        /// the prototype reaches the game.
        /// </summary>
        /// <param name="writer">The writer.</param>
        private void WriteClearedFields(LuaWriter writer)
        {
            var cleared = _fields.Where(f => f.Value == null).Select(f => f.Key).ToList();
            if (cleared.Count == 0) return;

            writer.Key("__nil");
            writer.BeginArray();
            foreach (var name in cleared) writer.Value(name);
            writer.EndArray();
        }
    }
}
