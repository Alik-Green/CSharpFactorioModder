using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Factorio.Data
{
    /// <summary>
    /// One pipe connection on the outside of a machine: which side it leaves through and at what
    /// offset from the entity centre.
    /// <para>
    /// On a 3×3 machine centered on <c>(0, 0)</c>:
    /// north edge is <c>(0, -1)</c>, south edge is <c>(0, 1)</c>,
    /// west edge is <c>(-1, 0)</c>, east edge is <c>(1, 0)</c>.
    /// </para>
    /// </summary>
    public sealed class PipePort : ILuaValue
    {
        private readonly int _direction;
        private readonly double _x;
        private readonly double _y;

        private PipePort(int direction, double x, double y)
        {
            _direction = direction;
            _x = x;
            _y = y;
        }

        /// <summary>A pipe leaving the north side of the machine (direction = 0).</summary>
        public static PipePort North(double x = 0, double y = -1) => new(0, x, y);

        /// <summary>A pipe leaving the east side of the machine (direction = 4).</summary>
        public static PipePort East(double x = 1, double y = 0) => new(4, x, y);

        /// <summary>A pipe leaving the south side of the machine (direction = 8).</summary>
        public static PipePort South(double x = 0, double y = 1) => new(8, x, y);

        /// <summary>A pipe leaving the west side of the machine (direction = 12).</summary>
        public static PipePort West(double x = -1, double y = 0) => new(12, x, y);

        /// <summary>Writes the pipe connection table.</summary>
        public void Write(LuaWriter writer)
        {
            writer.BeginTable();
            writer.Key("direction");
            writer.Value(_direction);
            writer.Key("position");
            writer.BeginArray();
            writer.Value(_x);
            writer.Value(_y);
            writer.EndArray();
            writer.EndTable();
        }
    }

    /// <summary>
    /// Strongly-typed collection of <see cref="PipePort"/> entries (<c>[PipePort.East(1, 0)]</c>).
    /// </summary>
    [CollectionBuilder(typeof(PipePortList), nameof(Create))]
    public sealed class PipePortList : ILuaValue, IEnumerable<PipePort>
    {
        private readonly List<PipePort> _items = new();

        /// <summary>Builds a list from a C# collection expression <c>[PipePort.North(...), ...]</c>.</summary>
        public static PipePortList Create(ReadOnlySpan<PipePort> items)
        {
            var list = new PipePortList();
            foreach (var item in items) list._items.Add(item);
            return list;
        }

        /// <summary>Allows assigning a legacy <see cref="Values"/> collection seamlessly.</summary>
        public static implicit operator PipePortList?(Values? values)
        {
            if (values == null) return null;
            var list = new PipePortList();
            foreach (var item in values)
            {
                if (item is PipePort port) list._items.Add(port);
            }
            return list;
        }

        /// <summary>Number of pipe ports.</summary>
        public int Count => _items.Count;

        /// <inheritdoc />
        public void Write(LuaWriter writer)
        {
            writer.BeginArray();
            foreach (var item in _items) writer.Value(item);
            writer.EndArray();
        }

        /// <inheritdoc />
        public IEnumerator<PipePort> GetEnumerator() => _items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
