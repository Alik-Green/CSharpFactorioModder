using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Factorio.Data
{
    /// <summary>
    /// A machine's connection to the fluid network: an internal tank holding <c>volume</c> fluid units
    /// with one or more <see cref="PipePort"/> pipe connections on its hull.
    /// </summary>
    public sealed class FluidBox : ILuaValue
    {
        private readonly string _flow;
        private readonly FluidId? _filter;
        private readonly double _volume;
        private readonly PipePortList _ports;

        private FluidBox(string flow, FluidId? filter, double volume, PipePortList ports)
        {
            _flow = flow;
            _filter = filter;
            _volume = volume;
            _ports = ports;
        }

        /// <summary>
        /// An input fluid box that receives fluid into the machine.
        /// </summary>
        /// <param name="filter">Restrict to a specific fluid (e.g. <see cref="FluidId.Water"/> or <c>FluidRef.Of("water")</c>), or <c>null</c> to accept whichever fluid the active recipe consumes.</param>
        /// <param name="volume">Internal buffer capacity in fluid units (commonly <c>100</c> or <c>1000</c>).</param>
        /// <param name="ports">Pipe ports where external pipes attach, e.g. <c>[PipePort.West(-1, 0)]</c>.</param>
        public static FluidBox Input(FluidId? filter, double volume, PipePortList? ports) =>
            new("input", filter, volume, ports ?? new PipePortList());

        /// <summary>
        /// An output fluid box that pushes produced fluid into connected pipes.
        /// </summary>
        /// <param name="filter">Restrict to a specific fluid (e.g. <see cref="FluidId.Water"/>), or <c>null</c> for recipe-driven output.</param>
        /// <param name="volume">Internal buffer capacity in fluid units (commonly <c>100</c> or <c>1000</c>).</param>
        /// <param name="ports">Pipe ports where external pipes attach, e.g. <c>[PipePort.East(1, 0)]</c>.</param>
        public static FluidBox Output(FluidId? filter, double volume, PipePortList? ports) =>
            new("output", filter, volume, ports ?? new PipePortList());

        /// <summary>
        /// A pass-through input/output fluid box that can both receive fluid and pass excess fluid along a line of machines.
        /// </summary>
        public static FluidBox InputOutput(FluidId? filter, double volume, PipePortList? ports) =>
            new("input-output", filter, volume, ports ?? new PipePortList());

        /// <summary>Writes the fluid box table.</summary>
        public void Write(LuaWriter writer)
        {
            writer.BeginTable();
            writer.Key("volume");
            writer.Value(_volume);
            writer.Key("production_type");
            writer.Value(_flow);
            if (_filter != null)
            {
                writer.Key("filter");
                writer.Value(_filter);
            }
            writer.Key("pipe_connections");
            writer.Value(_ports);
            writer.EndTable();
        }
    }

    /// <summary>
    /// Strongly-typed collection of <see cref="FluidBox"/> entries (<c>[FluidBox.Input(...), FluidBox.Output(...)]</c>).
    /// </summary>
    [CollectionBuilder(typeof(FluidBoxList), nameof(Create))]
    public sealed class FluidBoxList : ILuaValue, IEnumerable<FluidBox>
    {
        private readonly List<FluidBox> _items = new();

        /// <summary>Builds a list from a C# collection expression <c>[FluidBox.Input(...), ...]</c>.</summary>
        public static FluidBoxList Create(ReadOnlySpan<FluidBox> items)
        {
            var list = new FluidBoxList();
            foreach (var item in items) list._items.Add(item);
            return list;
        }

        /// <summary>Allows assigning a legacy <see cref="Values"/> collection seamlessly.</summary>
        public static implicit operator FluidBoxList?(Values? values)
        {
            if (values == null) return null;
            var list = new FluidBoxList();
            foreach (var item in values)
            {
                if (item is FluidBox box) list._items.Add(box);
            }
            return list;
        }

        /// <summary>Number of fluid boxes.</summary>
        public int Count => _items.Count;

        /// <inheritdoc />
        public void Write(LuaWriter writer)
        {
            writer.BeginArray();
            foreach (var item in _items) writer.Value(item);
            writer.EndArray();
        }

        /// <inheritdoc />
        public IEnumerator<FluidBox> GetEnumerator() => _items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
