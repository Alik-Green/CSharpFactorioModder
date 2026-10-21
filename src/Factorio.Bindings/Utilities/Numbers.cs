using System;
using Factorio.Runtime;

namespace Factorio
{
    /// <summary>
    /// Conversions between ticks and the units people think in. Factorio runs at
    /// <see cref="PerSecond"/> ticks per second, so a value typed as "2 seconds" must be converted
    /// before it reaches <c>schedule</c>, <c>on_nth_tick</c> or a timer.
    /// </summary>
    /// @CSharpLua.Ignore
    public static class Ticks
    {
        /// <summary>Ticks in one second (60). Multiply, do not assume, when the game changes it.</summary>
        public const double PerSecond = 60;

        /// <summary>Ticks in one minute (3,600).</summary>
        public const double PerMinute = 3600;

        /// <summary>Turns a duration in seconds into a whole number of ticks.</summary>
        /// <param name="seconds">Duration in seconds; fractions are allowed (0.5 = 30 ticks).</param>
        /// <returns>The duration rounded down to a tick boundary.</returns>
        /// @CSharpLua.Template = "math.floor({0} * 60)"
        public static double FromSeconds(double seconds) => throw new NotImplementedException();

        /// <summary>Turns a duration in minutes into a whole number of ticks.</summary>
        /// <param name="minutes">Duration in minutes; fractions are allowed.</param>
        /// <returns>The duration rounded down to a tick boundary.</returns>
        /// @CSharpLua.Template = "math.floor({0} * 3600)"
        public static double FromMinutes(double minutes) => throw new NotImplementedException();

        /// <summary>Turns a tick count back into seconds, for display.</summary>
        /// <param name="ticks">A number of ticks.</param>
        /// <returns>The equivalent duration in seconds.</returns>
        /// @CSharpLua.Template = "({0} / 60)"
        public static double ToSeconds(double ticks) => throw new NotImplementedException();

        /// <summary>
        /// The tick count as whole seconds, rounded down: what a player expects when a mod prints
        /// "running for N seconds".
        /// </summary>
        /// <param name="ticks">A number of ticks.</param>
        /// <returns>Whole seconds elapsed.</returns>
        /// @CSharpLua.Template = "math.floor({0} / 60)"
        public static double ToWholeSeconds(double ticks) => throw new NotImplementedException();
    }

    /// <summary>
    /// Small numeric helpers that do not need a wrapper type. They exist so mod code reads like
    /// C# and still compiles to the obvious Lua expression.
    /// </summary>
    /// @CSharpLua.Ignore
    public static class Numbers
    {
        /// <summary>Larger of two values.</summary>
        /// <param name="a">First value.</param>
        /// <param name="b">Second value.</param>
        /// <returns>The larger value.</returns>
        /// @CSharpLua.Template = "math.max({0}, {1})"
        public static double Max(double a, double b) => throw new NotImplementedException();

        /// <summary>
        /// <paramref name="value"/>, or 0 when it is nil. Reading a number that a save from an
        /// older mod version may not have written yet would otherwise turn into an arithmetic
        /// error on nil.
        /// </summary>
        /// <param name="value">A number, or nil.</param>
        /// <returns>The number, or 0.</returns>
        /// @CSharpLua.Template = "({0} or 0)"
        public static double OrZero(double value) => throw new NotImplementedException();

        /// <summary>
        /// <paramref name="value"/> as a number, or 0 when it is nil or not a number. Use this for
        /// values read through <see cref="Native"/> or from a save written by an older version.
        /// </summary>
        /// <param name="value">A number, or nil.</param>
        /// <returns>The number, or 0.</returns>
        /// @CSharpLua.Template = "(tonumber({0}) or 0)"
        public static double ToNumberOrZero(object value) => throw new NotImplementedException();

        /// <summary>Smaller of two values.</summary>
        /// <param name="a">First value.</param>
        /// <param name="b">Second value.</param>
        /// <returns>The smaller value.</returns>
        /// @CSharpLua.Template = "math.min({0}, {1})"
        public static double Min(double a, double b) => throw new NotImplementedException();

        /// <summary>Rounds down to the nearest whole number.</summary>
        /// <param name="value">Value to round.</param>
        /// <returns>The largest whole number not greater than <paramref name="value"/>.</returns>
        /// @CSharpLua.Template = "math.floor({0})"
        public static double Floor(double value) => throw new NotImplementedException();

        /// <summary>Rounds up to the nearest whole number.</summary>
        /// <param name="value">Value to round.</param>
        /// <returns>The smallest whole number not less than <paramref name="value"/>.</returns>
        /// @CSharpLua.Template = "math.ceil({0})"
        public static double Ceiling(double value) => throw new NotImplementedException();

        /// <summary>
        /// Restricts a value to an inclusive range. Useful for clamping a player-supplied amount
        /// before it reaches the game API, which rejects out-of-range values with a script error.
        /// </summary>
        /// <param name="value">Value to clamp.</param>
        /// <param name="low">Lower bound, inclusive.</param>
        /// <param name="high">Upper bound, inclusive.</param>
        /// <returns><paramref name="value"/>, or the bound it fell outside of.</returns>
        /// @CSharpLua.Template = "math.max({1}, math.min({2}, {0}))"
        public static double Clamp(double value, double low, double high) => throw new NotImplementedException();

        /// <summary>Rest of a division that is always non-negative, unlike Lua's plain <c>%</c> for negative inputs.</summary>
        /// <param name="value">Dividend.</param>
        /// <param name="modulus">Divisor; must be positive.</param>
        /// <returns>A value in <c>[0, modulus)</c>.</returns>
        /// @CSharpLua.Template = "(({0} % {1}) + {1}) % {1}"
        public static double PositiveModulo(double value, double modulus) => throw new NotImplementedException();
    }
}
