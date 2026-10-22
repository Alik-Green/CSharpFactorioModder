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
        /// The prototype's icon: e.g. <c>Asset.Mod("graphics/icons/condenser.png")</c>,
        /// <c>IconSpec.Mod("graphics/icons/condenser.png")</c>, or <c>Asset.Base("graphics/icons/iron-plate.png")</c>.
        /// <para>
        /// When pointing to a PNG file inside <c>resources/</c>, <see cref="IconSize"/> is automatically
        /// fitted to the PNG's actual pixel dimensions even if <see cref="IconSize"/> was omitted or mismatched!
        /// </para>
        /// </summary>
        public IconSpec? Icon
        {
            get => Get("icon") as IconSpec;
            set => Set("icon", value);
        }

        /// <summary>
        /// Size of <see cref="Icon"/> in pixels. Optional when using <c>Asset.Mod("...")</c> —
        /// the build inspects the PNG automatically!
        /// </summary>
        public double IconSize
        {
            get => Number("icon_size");
            set => Set("icon_size", value);
        }

        /// <summary>
        /// Composite multi-layer icons (e.g. <c>[IconLayer.Tinted(Vanilla.Icons.Machines.ChemicalPlant, Rgb.Hex("#38BDF8")), IconLayer.Badge(Vanilla.Icons.Fluids.Water)]</c>).
        /// </summary>
        public IconLayerList? Icons
        {
            get => Get("icons") as IconLayerList;
            set => Set("icons", value);
        }

        /// <summary>
        /// The name the player sees, e.g. <c>"Atmospheric Condenser"</c>.
        /// <para>
        /// Merged automatically into <c>locale/en/locale.cfg</c> at build time.
        /// </para>
        /// </summary>
        public string? LocalisedName
        {
            get => Localise("localised-name");
            set => SetLocalised("localised-name", value);
        }

        /// <summary>
        /// One line of tooltip text shown under <see cref="LocalisedName"/>.
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
        /// Sort order inside the crafting menu or prototype list:
        /// <c>SortOrder.At("b", "atmospheric-condenser")</c> writes <c>b[atmospheric-condenser]</c>.
        /// </summary>
        public SortOrder? Order
        {
            get => Get("order") is string order ? SortOrder.Of(order) : null;
            set => Set("order", value);
        }

        /// <summary>
        /// The tab/row the prototype appears in: e.g. <c>Subgroup.ProductionMachine</c>,
        /// <c>Subgroup.ExtractionMachine</c>, or a custom <see cref="SubgroupDefinition"/>.
        /// </summary>
        public Subgroup? Subgroup
        {
            get => Get("subgroup") is string subgroup ? Subgroup.Of(subgroup) : null;
            set => Set("subgroup", value);
        }

        /// <summary>The vanilla prototype this definition takes its unset fields from, or <c>null</c>.</summary>
        public VanillaPrototype? InheritsFrom => Get("__inherit") as VanillaPrototype;

        /// <summary>Sets any prototype field by raw Lua field name.</summary>
        public void Field(string name, object? value) => Set(name, value);

        /// <summary>Copies <paramref name="donor"/>'s field into this definition, for inheritance.</summary>
        public void CopyFieldFrom(PrototypeDefinition donor, string name) => Set(name, donor.Get(name));

        /// <summary>Writes the prototype table. Automatically sizes <c>icon_size</c> when a mod PNG exists on disk.</summary>
        public virtual void Write(LuaWriter writer)
        {
            int? autoIconSize = null;
            if (Get("icon") is IconSpec iconSpec && !string.IsNullOrEmpty(iconSpec.Path))
            {
                if (Asset.TryReadModPngSize(iconSpec.Path, out var pngW, out var pngH))
                    autoIconSize = Math.Min(pngW, pngH);
                else if (iconSpec.Path.StartsWith("__base__/graphics/technology/", StringComparison.Ordinal) ||
                         iconSpec.Path.StartsWith("__space-age__/graphics/technology/", StringComparison.Ordinal))
                    autoIconSize = 256;
            }

            writer.BeginTable();
            foreach (var field in _fields)
            {
                if (field.Key == "icon_size" && autoIconSize.HasValue)
                {
                    WriteField(writer, field.Key, autoIconSize.Value);
                    autoIconSize = null;
                    continue;
                }
                WriteField(writer, field.Key, field.Value);
            }

            if (autoIconSize.HasValue)
            {
                WriteField(writer, "icon_size", autoIconSize.Value);
            }

            WriteClearedFields(writer);
            writer.EndTable();
        }

        /// <summary>Writes one field; overridden to change a spelling for a version.</summary>
        protected virtual void WriteField(LuaWriter writer, string name, object? value)
        {
            if (value == null || value is LocalisedText) return;
            writer.Key(name);
            writer.Value(value);
        }

        /// <summary>Reads a field that was assigned, if any.</summary>
        protected object? Get(string name)
        {
            foreach (var field in _fields)
                if (field.Key == name) return field.Value;
            return null;
        }

        /// <summary>Assigns a field, keeping the order fields were first written in.</summary>
        protected void Set(string name, object? value)
        {
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
        protected double Number(string name)
        {
            var value = Get(name);
            return value == null ? 0 : Convert.ToDouble(value, System.Globalization.CultureInfo.InvariantCulture);
        }

        /// <summary>Reads a boolean field, false when it was never assigned.</summary>
        protected bool Flag(string name) => Get(name) is true;

        /// <summary>The localisation entries this definition declares.</summary>
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

        private string? Localise(string kind) =>
            _fields.FirstOrDefault(f => f.Value is LocalisedText text && text.Kind == kind).Value is LocalisedText found
                ? found.Value
                : null;

        private void SetLocalised(string kind, string? value)
        {
            if (value == null) return;
            Set(kind, new LocalisedText(kind, Name, value));
        }

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
