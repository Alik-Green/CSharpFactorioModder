namespace Factorio.Data
{
    /// <summary>When a Factorio mod setting takes effect in the Settings menu.</summary>
    public enum SettingStage
    {
        /// <summary><c>"startup"</c> — loaded before the data stage; requires restarting Factorio to change.</summary>
        Startup,

        /// <summary><c>"runtime-global"</c> — shared across the map; changeable live by admins during a game.</summary>
        RuntimeGlobal,

        /// <summary><c>"runtime-per-user"</c> — personal per-player preference changeable live at any time.</summary>
        RuntimePerUser,
    }

    /// <summary>
    /// Base class for strongly-typed Factorio mod settings emitted cleanly to <c>settings.lua</c>
    /// with automatic <c>[mod-setting-name]</c> and <c>[mod-setting-description]</c> locale generation!
    /// </summary>
    public abstract class SettingDefinition : PrototypeDefinition
    {
        /// <summary>Creates a setting of the specified prototype type, name, and stage.</summary>
        protected SettingDefinition(string type, string name, SettingStage stage) : base(type, name)
        {
            Set("setting_type", stage switch
            {
                SettingStage.Startup => "startup",
                SettingStage.RuntimeGlobal => "runtime-global",
                _ => "runtime-per-user",
            });
        }
    }

    /// <summary>A checkbox setting (<c>type = "bool-setting"</c>) in Factorio's Mod Settings menu.</summary>
    public sealed class BoolSettingDefinition : SettingDefinition
    {
        /// <summary>Declares a boolean mod setting.</summary>
        public BoolSettingDefinition(string name, bool defaultValue, SettingStage stage = SettingStage.Startup)
            : base("bool-setting", name, stage)
        {
            Set("default_value", defaultValue);
        }
    }

    /// <summary>An integer setting (<c>type = "int-setting"</c>) in Factorio's Mod Settings menu.</summary>
    public sealed class IntSettingDefinition : SettingDefinition
    {
        /// <summary>Declares an integer mod setting with optional <paramref name="min"/> and <paramref name="max"/> bounds.</summary>
        public IntSettingDefinition(
            string name,
            long defaultValue,
            long? min = null,
            long? max = null,
            SettingStage stage = SettingStage.Startup)
            : base("int-setting", name, stage)
        {
            Set("default_value", defaultValue);
            if (min.HasValue) Set("minimum_value", min.Value);
            if (max.HasValue) Set("maximum_value", max.Value);
        }
    }

    /// <summary>A floating-point number setting (<c>type = "double-setting"</c>) in Factorio's Mod Settings menu.</summary>
    public sealed class DoubleSettingDefinition : SettingDefinition
    {
        /// <summary>Declares a double mod setting with optional <paramref name="min"/> and <paramref name="max"/> bounds.</summary>
        public DoubleSettingDefinition(
            string name,
            double defaultValue,
            double? min = null,
            double? max = null,
            SettingStage stage = SettingStage.Startup)
            : base("double-setting", name, stage)
        {
            Set("default_value", defaultValue);
            if (min.HasValue) Set("minimum_value", min.Value);
            if (max.HasValue) Set("maximum_value", max.Value);
        }
    }

    /// <summary>A text or dropdown choice setting (<c>type = "string-setting"</c>) in Factorio's Mod Settings menu.</summary>
    public sealed class StringSettingDefinition : SettingDefinition
    {
        /// <summary>Declares a string or dropdown mod setting.</summary>
        public StringSettingDefinition(
            string name,
            string defaultValue,
            StringList? allowedValues = null,
            bool allowBlank = false,
            SettingStage stage = SettingStage.Startup)
            : base("string-setting", name, stage)
        {
            Set("default_value", defaultValue);
            if (allowBlank) Set("allow_blank", true);
            if (allowedValues != null) Set("allowed_values", allowedValues);
        }
    }
}
