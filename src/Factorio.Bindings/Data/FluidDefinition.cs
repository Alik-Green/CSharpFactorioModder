using System;

namespace Factorio.Data
{
    /// <summary>
    /// A fluid: what pipes carry, what a fluid box holds and what a fluid recipe consumes or makes.
    /// </summary>
    public class FluidDefinition : PrototypeDefinition
    {
        /// <summary>Declares a fluid with the given prototype name.</summary>
        /// <param name="name">Fluid prototype name.</param>
        public FluidDefinition(string name) : base("fluid", name)
        {
        }

        /// <summary>
        /// Declares a fluid that starts from an existing vanilla fluid (e.g. <see cref="Vanilla.FluidOf"/>).
        /// </summary>
        /// <param name="name">Fluid prototype name.</param>
        /// <param name="inheritFrom">The prototype to build on.</param>
        public FluidDefinition(string name, VanillaPrototype inheritFrom) : base("fluid", name)
        {
            Field("__inherit", inheritFrom);
        }

        /// <summary>The temperature of the fluid as pipes carry it (°C). Required by the game.</summary>
        public double DefaultTemperature
        {
            get => Number("default_temperature");
            set => Set("default_temperature", value);
        }

        /// <summary>The highest temperature the fluid reaches (°C).</summary>
        public double MaxTemperature
        {
            get => Number("max_temperature");
            set => Set("max_temperature", value);
        }

        /// <summary>
        /// Primary colour of the fluid in pipes: e.g. <c>new Rgb(0.08, 0.40, 0.85)</c>,
        /// <c>Rgb.Bytes(20, 102, 217)</c>, or <c>Rgb.Hex("#1466D9")</c>.
        /// </summary>
        public Rgb? BaseColor
        {
            get => Get("base_color") as Rgb;
            set => Set("base_color", value);
        }

        /// <summary>
        /// Highlight colour of the fluid flowing in pipe windows: e.g. <c>new Rgb(0.15, 0.70, 1.00)</c>
        /// or <c>Rgb.Hex("#26B3FF")</c>.
        /// </summary>
        public Rgb? FlowColor
        {
            get => Get("flow_color") as Rgb;
            set => Set("flow_color", value);
        }

        /// <summary>Heat capacity per unit.</summary>
        public double HeatCapacity
        {
            get => Number("heat_capacity");
            set => Set("heat_capacity", value);
        }

        /// <summary>Energy per unit when burned as fuel (e.g. <c>Energy.Megajoules(2)</c>).</summary>
        public EnergyValue? FuelValue
        {
            get => Get("fuel_value") as string;
            set => Set("fuel_value", value?.Formatted);
        }

        /// <summary>Temperature above which the fluid renders as gas vapour.</summary>
        public double GasTemperature
        {
            get => Number("gas_temperature");
            set => Set("gas_temperature", value);
        }
    }
}
