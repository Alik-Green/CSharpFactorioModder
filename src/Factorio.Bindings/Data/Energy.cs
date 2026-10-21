using System;

namespace Factorio.Data
{
    /// <summary>
    /// Power and energy values, as the strings Factorio parses.
    /// <para>
    /// A prototype field such as <c>energy_usage</c> or <c>fuel_value</c> is a string with a unit
    /// (<c>"250kW"</c>, <c>"5MJ"</c>), not a number, so the conversion is explicit here rather than
    /// implicit - a number with the wrong unit is a mod that silently draws a thousand times too
    /// much power.
    /// </para>
    /// </summary>
    public static class Energy
    {
        /// <summary>Watts, for a very small consumer or producer.</summary>
        /// <param name="value">The number of watts.</param>
        /// <returns>E.g. <c>"50W"</c>.</returns>
        public static string Watts(double value) => Format(value, "W");

        /// <summary>Kilowatts, the usual unit for a machine's consumption.</summary>
        /// <param name="value">The number of kilowatts.</param>
        /// <returns>E.g. <c>"250kW"</c>.</returns>
        public static string Kilowatts(double value) => Format(value, "kW");

        /// <summary>Megawatts, for large machines.</summary>
        /// <param name="value">The number of megawatts.</param>
        /// <returns>E.g. <c>"2MW"</c>.</returns>
        public static string Megawatts(double value) => Format(value, "MW");

        /// <summary>Joules per tick, which is what a recipe's <c>energy_required</c> is not - use seconds there.</summary>
        /// <param name="value">The number of joules.</param>
        /// <returns>E.g. <c>"50J"</c>.</returns>
        public static string Joules(double value) => Format(value, "J");

        /// <summary>Kilojoules, for a fuel value.</summary>
        /// <param name="value">The number of kilojoules.</param>
        /// <returns>E.g. <c>"4kJ"</c>.</returns>
        public static string Kilojoules(double value) => Format(value, "kJ");

        /// <summary>Megajoules, for a fuel value.</summary>
        /// <param name="value">The number of megajoules.</param>
        /// <returns>E.g. <c>"5MJ"</c>.</returns>
        public static string Megajoules(double value) => Format(value, "MJ");

        private static string Format(double value, string unit) =>
            value.ToString("0.###", System.Globalization.CultureInfo.InvariantCulture) + unit;
    }
}
