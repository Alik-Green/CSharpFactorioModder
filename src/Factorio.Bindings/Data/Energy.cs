using System;
using System.Collections.Generic;
using System.Globalization;

namespace Factorio.Data
{
    /// <summary>
    /// A strongly-typed Factorio power or energy quantity (such as <c>"250kW"</c> or <c>"5MJ"</c>),
    /// created with <see cref="Energy.Kilowatts"/>, <see cref="Energy.Megawatts"/>,
    /// <see cref="Energy.Megajoules"/>, etc.
    /// </summary>
    public sealed class EnergyValue : ILuaValue
    {
        /// <summary>Creates an energy/power value from a formatted Factorio unit string (e.g. <c>"250kW"</c>).</summary>
        public EnergyValue(string formatted)
        {
            Formatted = formatted;
        }

        /// <summary>The underlying Factorio string representation (e.g. <c>"250kW"</c>).</summary>
        public string Formatted { get; }

        /// <summary>Watts (e.g. <c>"50W"</c>).</summary>
        public static EnergyValue Watts(double value) => Energy.Watts(value);

        /// <summary>Kilowatts — the standard unit for machine power draw (e.g. <c>"250kW"</c>).</summary>
        public static EnergyValue Kilowatts(double value) => Energy.Kilowatts(value);

        /// <summary>Megawatts — for large power consumers/generators (e.g. <c>"2MW"</c>).</summary>
        public static EnergyValue Megawatts(double value) => Energy.Megawatts(value);

        /// <summary>Gigawatts (e.g. <c>"1.2GW"</c>).</summary>
        public static EnergyValue Gigawatts(double value) => Energy.Gigawatts(value);

        /// <summary>Joules (e.g. <c>"50J"</c>).</summary>
        public static EnergyValue Joules(double value) => Energy.Joules(value);

        /// <summary>Kilojoules (e.g. <c>"4kJ"</c>).</summary>
        public static EnergyValue Kilojoules(double value) => Energy.Kilojoules(value);

        /// <summary>Megajoules — standard fuel value unit (e.g. <c>"4MJ"</c>).</summary>
        public static EnergyValue Megajoules(double value) => Energy.Megajoules(value);

        /// <summary>Gigajoules (e.g. <c>"8GJ"</c>).</summary>
        public static EnergyValue Gigajoules(double value) => Energy.Gigajoules(value);

        /// <summary>Implicitly converts to <see cref="string"/> so existing string APIs work unchanged.</summary>
        public static implicit operator string?(EnergyValue? value) => value?.Formatted;

        /// <summary>Implicitly wraps a string such as <c>"250kW"</c>.</summary>
        public static implicit operator EnergyValue?(string? value) =>
            value == null ? null : new EnergyValue(value);

        /// <inheritdoc />
        public override string ToString() => Formatted;

        /// <inheritdoc />
        public void Write(LuaWriter writer) => writer.Value(Formatted);
    }

    /// <summary>
    /// Factory methods for power (<c>W</c>, <c>kW</c>, <c>MW</c>, <c>GW</c>) and energy
    /// (<c>J</c>, <c>kJ</c>, <c>MJ</c>, <c>GJ</c>) values.
    /// </summary>
    public static class Energy
    {
        /// <summary>Watts (e.g. <c>"50W"</c>).</summary>
        public static EnergyValue Watts(double value) => new(Format(value, "W"));

        /// <summary>Kilowatts, the usual unit for a machine's power consumption (e.g. <c>"250kW"</c>).</summary>
        public static EnergyValue Kilowatts(double value) => new(Format(value, "kW"));

        /// <summary>Megawatts, for heavy machines (e.g. <c>"2MW"</c>).</summary>
        public static EnergyValue Megawatts(double value) => new(Format(value, "MW"));

        /// <summary>Gigawatts (e.g. <c>"1GW"</c>).</summary>
        public static EnergyValue Gigawatts(double value) => new(Format(value, "GW"));

        /// <summary>Joules (e.g. <c>"50J"</c>).</summary>
        public static EnergyValue Joules(double value) => new(Format(value, "J"));

        /// <summary>Kilojoules, for fuel or heat capacity (e.g. <c>"4kJ"</c>).</summary>
        public static EnergyValue Kilojoules(double value) => new(Format(value, "kJ"));

        /// <summary>Megajoules, for fuel values (e.g. <c>"5MJ"</c>).</summary>
        public static EnergyValue Megajoules(double value) => new(Format(value, "MJ"));

        /// <summary>Gigajoules, for nuclear/high-density fuel values (e.g. <c>"8GJ"</c>).</summary>
        public static EnergyValue Gigajoules(double value) => new(Format(value, "GJ"));

        private static string Format(double value, string unit) =>
            value.ToString("0.###", CultureInfo.InvariantCulture) + unit;
    }

    /// <summary>
    /// Strongly-typed Factorio <c>energy_source</c> definition for powered, burner, fluid, heat,
    /// or zero-power (<see cref="Void"/>) machines.
    /// </summary>
    public sealed class EnergySource : ILuaValue
    {
        private readonly List<KeyValuePair<string, object?>> _fields;

        private EnergySource(List<KeyValuePair<string, object?>> fields)
        {
            _fields = fields;
        }

        /// <summary>
        /// Standard electric energy source (<c>type = "electric"</c>).
        /// </summary>
        /// <param name="usagePriority">Electric network priority, usually <c>"secondary-input"</c>.</param>
        /// <param name="drain">Optional idle power drain when not crafting, e.g. <c>Energy.Kilowatts(8)</c>.</param>
        /// <param name="emissionsPerMinute">Optional pollution produced per minute while running.</param>
        public static EnergySource Electric(
            string usagePriority = "secondary-input",
            EnergyValue? drain = null,
            double emissionsPerMinute = 0)
        {
            var fields = new List<KeyValuePair<string, object?>>
            {
                new("type", "electric"),
                new("usage_priority", usagePriority),
            };
            if (drain != null) fields.Add(new("drain", drain.Formatted));
            if (emissionsPerMinute != 0) fields.Add(new("emissions_per_minute", new PollutionTable(emissionsPerMinute)));
            return new EnergySource(fields);
        }

        /// <summary>
        /// Burner energy source (<c>type = "burner"</c>) fueled by coal, wood, solid fuel, etc.
        /// </summary>
        public static EnergySource Burner(
            double effectivity = 1.0,
            int? fuelInventorySize = null,
            string fuelCategory = "chemical",
            double emissionsPerMinute = 0)
        {
            var fields = new List<KeyValuePair<string, object?>>
            {
                new("type", "burner"),
                new("effectivity", effectivity),
            };
            if (fuelInventorySize.HasValue) fields.Add(new("fuel_inventory_size", fuelInventorySize.Value));
            fields.Add(new("fuel_categories", Values.Of(fuelCategory)));
            if (emissionsPerMinute != 0) fields.Add(new("emissions_per_minute", new PollutionTable(emissionsPerMinute)));
            return new EnergySource(fields);
        }

        /// <summary>
        /// Free/passive energy source (<c>type = "void"</c>) — runs with no electricity or fuel required.
        /// </summary>
        public static EnergySource Void(double emissionsPerMinute = 0)
        {
            var fields = new List<KeyValuePair<string, object?>> { new("type", "void") };
            if (emissionsPerMinute != 0) fields.Add(new("emissions_per_minute", new PollutionTable(emissionsPerMinute)));
            return new EnergySource(fields);
        }

        /// <summary>Fluid-powered energy source (<c>type = "fluid"</c>).</summary>
        public static EnergySource Fluid(
            double effectivity = 1.0,
            bool burnsFluid = true,
            double fluidUsagePerTick = 0,
            double maximumTemperature = 0)
        {
            var fields = new List<KeyValuePair<string, object?>>
            {
                new("type", "fluid"),
                new("effectivity", effectivity),
                new("burns_fluid", burnsFluid),
            };
            if (fluidUsagePerTick > 0) fields.Add(new("fluid_usage_per_tick", fluidUsagePerTick));
            if (maximumTemperature > 0) fields.Add(new("maximum_temperature", maximumTemperature));
            return new EnergySource(fields);
        }

        /// <summary>Heat-pipe energy source (<c>type = "heat"</c>).</summary>
        public static EnergySource Heat(
            double maxTemperature = 1000,
            EnergyValue? specificHeat = null,
            EnergyValue? maxTransfer = null,
            double minWorkingTemperature = 15)
        {
            var fields = new List<KeyValuePair<string, object?>>
            {
                new("type", "heat"),
                new("max_temperature", maxTemperature),
                new("specific_heat", (specificHeat ?? Energy.Megajoules(1)).Formatted),
                new("max_transfer", (maxTransfer ?? Energy.Gigawatts(1)).Formatted),
                new("min_working_temperature", minWorkingTemperature),
            };
            return new EnergySource(fields);
        }

        /// <inheritdoc />
        public void Write(LuaWriter writer)
        {
            writer.BeginTable();
            foreach (var field in _fields)
            {
                writer.Key(field.Key);
                writer.Value(field.Value);
            }
            writer.EndTable();
        }

        private sealed class PollutionTable : ILuaValue
        {
            private readonly double _pollution;

            public PollutionTable(double pollution) => _pollution = pollution;

            public void Write(LuaWriter writer)
            {
                writer.BeginTable();
                writer.Key("pollution");
                writer.Value(_pollution);
                writer.EndTable();
            }
        }
    }
}
