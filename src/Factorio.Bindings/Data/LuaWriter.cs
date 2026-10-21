using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Factorio.Data
{
    /// <summary>
    /// A value that knows how to write itself as a Lua expression.
    /// <para>
    /// Every prototype value in this namespace implements it, which is how the build turns a C#
    /// definition into the table Factorio reads. A mod's own class can implement it too, for a
    /// value the API does not model.
    /// </para>
    /// </summary>
    public interface ILuaValue
    {
        /// <summary>Writes this value, exactly once, to <paramref name="writer"/>.</summary>
        /// <param name="writer">The writer collecting the expression.</param>
        void Write(LuaWriter writer);
    }

    /// <summary>
    /// Writes Lua source: prototype tables, arrays, numbers and verbatim expressions.
    /// <para>
    /// The output is laid out one entry per line so a generated <c>data.lua</c> reads like a
    /// hand-written one. Strings are quoted and escaped, and numbers use the invariant culture - a
    /// mod must not emit <c>1,25</c> because the build ran on a machine with a comma decimal
    /// separator.
    /// </para>
    /// <para>
    /// Anything that is not a string, a number, a boolean, an <see cref="ILuaValue"/> or a sequence
    /// is refused: a C# object that reached a prototype table would be a definition that forgot to
    /// describe itself, and writing it as <c>nil</c> would silently drop a field.
    /// </para>
    /// </summary>
    public sealed class LuaWriter
    {
        private readonly StringBuilder _out = new();
        private readonly Stack<int> _entries = new();   // entries written per open table/array
        private int _depth;
        private bool _lineHasContent;
        private bool _needsComma;

        /// <summary>Creates a writer for one target Factorio version.</summary>
        /// <param name="apiVersion">Version the Lua is for, e.g. <c>2.0</c>; decides field spellings.</param>
        public LuaWriter(string apiVersion)
        {
            ApiVersion = apiVersion;
        }

        /// <summary>The Factorio version being written, e.g. <c>2.0</c>.</summary>
        public string ApiVersion { get; }

        /// <summary>Starts a table: <c>{</c>, with its entries on following lines.</summary>
        public void BeginTable() => Open(isArray: false);

        /// <summary>Ends a table: <c>}</c>.</summary>
        public void EndTable() => Close();

        /// <summary>Starts an array, which writes its values with no keys.</summary>
        public void BeginArray() => Open(isArray: true);

        /// <summary>Ends an array.</summary>
        public void EndArray() => Close();

        /// <summary>Writes a key, <c>name = </c>; the next value is its value.</summary>
        /// <param name="name">Lua field name.</param>
        public void Key(string name)
        {
            StartEntry();
            _out.Append(name).Append(" = ");
            _needsComma = false;        // the value completes this entry
        }

        /// <summary>Writes <paramref name="value"/>, choosing the Lua spelling for its type.</summary>
        /// <param name="value">A string, number, boolean, <see cref="ILuaValue"/> or sequence.</param>
        public void Value(object? value)
        {
            if (_entries.Count > 0 && IsArrayEntry())
            {
                StartEntry();
            }
            WriteValue(value);
        }

        /// <summary>
        /// Splices a Lua expression in verbatim, for a value only the game can evaluate -
        /// <c>defines.direction.north</c>, <c>require("generated.sprites").north</c>.
        /// </summary>
        /// <param name="lua">A Lua expression. It is copied, never parsed or checked.</param>
        public void Raw(string lua) => Value(new RawLua(lua));

        /// <summary>Writes a whole line of Lua at the current indentation, outside any table.</summary>
        /// <param name="line">The line, without its newline.</param>
        public void Line(string line)
        {
            // A line is a statement, never an entry: a comma pending from the value just written
            // belongs inside a table, and would be a syntax error here.
            _needsComma = false;
            if (_lineHasContent) NewLine();
            if (line.Length > 0)
            {
                Indent();
                _out.Append(line);
            }
            NewLine();
        }

        /// <summary>The Lua written so far.</summary>
        /// <returns>The whole expression or chunk.</returns>
        public override string ToString() => _out.ToString();

        private bool IsArrayEntry() => _arrays.Peek();

        // Which open containers are arrays, in parallel with _entries.
        private readonly Stack<bool> _arrays = new();

        private void Open(bool isArray)
        {
            _out.Append('{');
            _lineHasContent = true;
            _entries.Push(0);
            _arrays.Push(isArray);
            _depth++;
            _needsComma = false;
        }

        private void Close()
        {
            var entries = _entries.Pop();
            _arrays.Pop();
            _depth--;
            if (entries > 0) NewLine();
            Indent();
            _out.Append('}');
            _lineHasContent = true;
            _needsComma = true;
        }

        /// <summary>Begins a key or an array element: closes the previous entry if there is one.</summary>
        private void StartEntry()
        {
            if (_needsComma) _out.Append(',');
            NewLine();
            Indent();
            _needsComma = false;
            if (_entries.Count > 0) _entries.Push(_entries.Pop() + 1);
        }

        private void WriteValue(object? value)
        {
            switch (value)
            {
                case null:
                    _out.Append("nil");
                    break;
                case string text:
                    _out.Append(Quote(text));
                    break;
                case bool flag:
                    _out.Append(flag ? "true" : "false");
                    break;
                case double number:
                    _out.Append(Number(number));
                    break;
                case float number:
                    _out.Append(Number(number));
                    break;
                case int number:
                    _out.Append(number.ToString(CultureInfo.InvariantCulture));
                    break;
                case ILuaValue lua:
                    lua.Write(this);
                    break;
                case IEnumerable sequence:
                    BeginArray();
                    foreach (var item in sequence) Value(item);
                    EndArray();
                    break;
                default:
                    throw new NotSupportedException(
                        $"{value.GetType().FullName} cannot be written to a prototype: make it an ILuaValue, " +
                        "or pass a string, number, boolean or a sequence of those.");
            }
            _lineHasContent = true;
            _needsComma = true;
        }

        private void NewLine()
        {
            _out.Append('\n');
            _lineHasContent = false;
        }

        private void Indent()
        {
            _out.Append(' ', _depth * 2);
            _lineHasContent = true;
        }

        private static string Number(double value)
        {
            if (double.IsNaN(value) || double.IsInfinity(value))
                throw new NotSupportedException("a prototype cannot contain NaN or infinity");
            if (value == Math.Floor(value) && Math.Abs(value) < 1e15)
                return ((long)value).ToString(CultureInfo.InvariantCulture);
            return value.ToString("R", CultureInfo.InvariantCulture);
        }

        private static string Quote(string text)
        {
            var builder = new StringBuilder(text.Length + 2);
            builder.Append('"');
            foreach (var c in text)
            {
                switch (c)
                {
                    case '"': builder.Append("\\\""); break;
                    case '\\': builder.Append("\\\\"); break;
                    case '\n': builder.Append("\\n"); break;
                    case '\r': builder.Append("\\r"); break;
                    case '\t': builder.Append("\\t"); break;
                    default:
                        if (c < ' ') builder.Append('\\').Append(((int)c).ToString("D3", CultureInfo.InvariantCulture));
                        else builder.Append(c);
                        break;
                }
            }
            builder.Append('"');
            return builder.ToString();
        }

        /// <summary>Lua source copied verbatim, e.g. <c>defines.direction.north</c>.</summary>
        private sealed class RawLua : ILuaValue
        {
            private readonly string _lua;

            public RawLua(string lua) => _lua = lua;

            public void Write(LuaWriter writer) => writer._out.Append(_lua);
        }
    }
}
