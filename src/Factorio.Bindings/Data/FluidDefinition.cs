using System;

namespace Factorio.Data
{
    /// <summary>
    /// A fluid: what pipes carry, what a fluid box holds and what a fluid recipe consumes or makes.
    /// <para>
    /// Factorio requires three things of every fluid - a default temperature, a colour for the pipe
    /// and a colour for the fluid moving through it - so they are the first properties to set:
    /// </para>
    /// <code>
    /// public sealed class DemoCoolant : FluidDefinition
    /// {
    ///     public DemoCoolant() : base("demo-coolant")
    ///     {
    ///         Icon = Asset.Base("graphics/icons/fluid/water.png");
    ///         IconSize = 64;
    ///         DefaultTemperature = 25;
    ///         MaxTemperature = 100;
    ///         BaseColor = new Rgb(0.08, 0.40, 0.85);
    ///         FlowColor = new Rgb(0.15, 0.70, 1.00);
    ///     }
    /// }
    /// </code>
    /// </summary>
    public class FluidDefinition : PrototypeDefinition
    {
        /// <summary>Declares a fluid with the given prototype name.</summary>
        /// <param name="name">Fluid prototype name.</param>
        public FluidDefinition(string name) : base("fluid", name)
        {
        }

        /// <summary>
        /// Declares a fluid that starts from an existing one: every field this definition does not
        /// set is copied from <paramref name="inheritFrom"/>, which is how a variant of water or
        /// crude oil gets the base game's pipe sprites and temperatures for free.
        /// </summary>
        /// <param name="name">Fluid prototype name.</param>
        /// <param name="inheritFrom">The prototype to build on, e.g. <see cref="Vanilla.FluidOf"/>.</param>
        public FluidDefinition(string name, VanillaPrototype inheritFrom) : base("fluid", name)
        {
            Field("__inherit", inheritFrom);
        }

        /// <summary>The temperature of the fluid as pipes carry it. Required by the game.</summary>
        public double DefaultTemperature
        {
            get => Number("default_temperature");
            set => Set("default_temperature", value);
        }

        /// <summary>The highest temperature the fluid reaches; 0 means "the same as the default".</summary>
        public double MaxTemperature
        {
            get => Number("max_temperature");
            set => Set("max_temperature", value);
        }

        /// <summary>The colour of the fluid in the pipe's windows. Required by the game.</summary>
        public object? BaseColor
        {
            get => Get("base_color");
            set => Set("base_color", value);
        }

        /// <summary>The colour of the fluid as it flows. Required by the game.</summary>
        public object? FlowColor
        {
            get => Get("flow_color");
            set => Set("flow_color", value);
        }

        /// <summary>Heat capacity per unit, for a fluid that carries heat.</summary>
        public double HeatCapacity
        {
            get => Number("heat_capacity");
            set => Set("heat_capacity", value);
        }

        /// <summary>Energy per unit when burned as fuel; 0 for a fluid that is not fuel.</summary>
        public string? FuelValue
        {
            get => Get("fuel_value") as string;
            set => Set("fuel_value", value);
        }
    }
}
