using System;

namespace Factorio.Data
{
    /// <summary>
    /// A machine's fluid connections.
    /// <para>
    /// An input box takes fluid from the pipes connected to it; an output box pushes fluid out. A
    /// machine that turns one fluid into another needs one of each; a machine that only consumes a
    /// fluid needs just the input.
    /// </para>
    /// <code>
    /// FluidBoxes =
    /// [
    ///     FluidBox.Input(filter: FluidRef.Of("demo-coolant"), volume: 100,
    ///                    ports: [PipePort.North(0, -1)]),
    /// ]
    /// </code>
    /// </summary>
    public sealed class FluidBox : ILuaValue
    {
        private readonly string _productionType;
        private readonly object? _filter;
        private readonly double _volume;
        private readonly Values? _ports;

        private FluidBox(string productionType, object? filter, double volume, Values? ports)
        {
            _productionType = productionType;
            _filter = filter;
            _volume = volume;
            _ports = ports;
        }

        /// <summary>A box the machine draws fluid from.</summary>
        /// <param name="filter">The fluid it accepts; <c>null</c> accepts any.</param>
        /// <param name="volume">Capacity in fluid units.</param>
        /// <param name="ports">Where pipes connect: <c>[PipePort.North(0, -1)]</c>.</param>
        /// <returns>The fluid box.</returns>
        public static FluidBox Input(object? filter, double volume, Values? ports) =>
            new("input", filter, volume, ports);

        /// <summary>A box the machine pushes fluid into.</summary>
        /// <param name="filter">The fluid it holds; <c>null</c> for whatever the recipe produced.</param>
        /// <param name="volume">Capacity in fluid units.</param>
        /// <param name="ports">Where pipes connect: <c>[PipePort.South(0, 1)]</c>.</param>
        /// <returns>The fluid box.</returns>
        public static FluidBox Output(object? filter, double volume, Values? ports) =>
            new("output", filter, volume, ports);

        /// <summary>A box fluid can both enter and leave, e.g. a tank's connection.</summary>
        /// <param name="filter">The fluid it holds; <c>null</c> for any.</param>
        /// <param name="volume">Capacity in fluid units.</param>
        /// <param name="ports">Where pipes connect.</param>
        /// <returns>The fluid box.</returns>
        public static FluidBox InputOutput(object? filter, double volume, Values? ports) =>
            new("input-output", filter, volume, ports);

        /// <summary>Writes the fluid box table.</summary>
        /// <param name="writer">The writer.</param>
        public void Write(LuaWriter writer)
        {
            writer.BeginTable();
            writer.Key("production_type");
            writer.Value(_productionType);
            if (_filter != null)
            {
                writer.Key("filter");
                writer.Value(_filter);
            }
            writer.Key("volume");
            writer.Value(_volume);
            if (_ports != null)
            {
                writer.Key("pipe_connections");
                writer.Value(_ports);
            }
            writer.EndTable();
        }
    }
}
