using Xunit;
namespace Factorio.Analyzers.Tests;

/// <summary>
/// One rule: a snippet that must trigger it, and a near-identical snippet that must not.
///
/// The negative case is always the construct that looks like the rule but is fine - `(double)`
/// cast rather than `(Widget)`, `for` rather than `foreach`, a static template that starts with
/// `{0}`. A rule that cannot tell those apart is worse than no rule, because it teaches people to
/// ignore the output.
/// </summary>
/// <param name="Id">Rule id, e.g. <c>FX0006</c>.</param>
/// <param name="Name">What the case demonstrates.</param>
/// <param name="Positive">Source that must report exactly this one rule.</param>
/// <param name="Negative">Source that must report nothing.</param>
public sealed record RuleCase(string Id, string Name, string Positive, string Negative);

/// <summary>The fixture table. Every rule in the catalog has at least one case here.</summary>
public static class RuleCases
{
    public static IEnumerable<object[]> All => Cases.Select(c => new object[] { c });

    public static IReadOnlyList<RuleCase> Cases { get; } = new List<RuleCase>
    {
        new(
            "FX0001",
            "using static of a Factorio type",
            """
            using static Factorio.Globals;

            namespace Mod
            {
                public static class Main
                {
                    public static void M() { }
                }
            }
            """,
            """
            using static System.Math;

            namespace Mod
            {
                public static class Main
                {
                    public static void M() { }
                }
            }
            """),

        new(
            "FX0002",
            "C# array",
            AnalyzerHarness.InMethod("            var values = new double[4];"),
            AnalyzerHarness.InMethod("            var value = 4;")),

        new(
            "FX0003",
            "foreach",
            AnalyzerHarness.InMethod("            foreach (var item in items) { }"),
            AnalyzerHarness.InMethod("            for (double i = 0; i < 3; i = i + 1) { }")),

        new(
            "FX0004",
            "tuple",
            AnalyzerHarness.InMethod("            var pair = (1, 2);"),
            AnalyzerHarness.InMethod("            var pair = 1;")),

        new(
            "FX0005",
            "explicit cast to a non-numeric type",
            """
            namespace Mod
            {
                public static class Main
                {
                    public static void M(object value)
                    {
                        var widget = (Widget)value;
                    }
                }
            }
            """,
            """
            namespace Mod
            {
                public static class Main
                {
                    public static void M(object value)
                    {
                        var number = (double)value;
                        var text = (string)value;
                    }
                }
            }
            """),

        new(
            "FX0006",
            "integer type",
            AnalyzerHarness.InMethod("            int count = 0;"),
            AnalyzerHarness.InMethod("            double count = 0;")),

        new(
            "FX0007",
            "string.Format",
            AnalyzerHarness.InMethod("            var text = string.Format(\"{0}\", 1);"),
            AnalyzerHarness.InMethod("            var text = \"value \" + 1;")),

        new(
            "FX0008",
            "banned namespace",
            """
            using System.IO;

            namespace Mod
            {
                public static class Main
                {
                    public static void M() { }
                }
            }
            """,
            """
            using System;

            namespace Mod
            {
                public static class Main
                {
                    public static void M() { }
                }
            }
            """),

        new(
            "FX0009",
            "try/catch",
            AnalyzerHarness.InMethod("            try { } catch { }"),
            AnalyzerHarness.InMethod("            var value = 1;")),

        new(
            "FX0009",
            "throw",
            AnalyzerHarness.InMethod("            throw null;"),
            AnalyzerHarness.InMethod("            var value = 1;")),

        new(
            "FX0009",
            "lock",
            AnalyzerHarness.InMethod("            lock (gate) { }"),
            AnalyzerHarness.InMethod("            var value = 1;")),

        new(
            "FX0009",
            "using statement",
            AnalyzerHarness.InMethod("            using (var handle = Open()) { }"),
            AnalyzerHarness.InMethod("            var value = 1;")),

        new(
            "FX0009",
            "await",
            """
            namespace Mod
            {
                public static class Main
                {
                    public static async void M()
                    {
                        await Work();
                    }
                }
            }
            """,
            AnalyzerHarness.InMethod("            var value = 1;")),

        new(
            "FX0009",
            "yield",
            """
            namespace Mod
            {
                public static class Main
                {
                    public static void M()
                    {
                        yield return 1;
                    }
                }
            }
            """,
            AnalyzerHarness.InMethod("            var value = 1;")),

        new(
            "FX0009",
            "delegate declaration",
            """
            namespace Mod
            {
                public delegate void Changed(double value);
            }
            """,
            """
            namespace Mod
            {
                public static class Main
                {
                    public static void Changed(double value) { }
                }
            }
            """),

        new(
            "FX0009",
            "event",
            """
            namespace Mod
            {
                public static class Main
                {
                    public static event System.Action Changed;
                }
            }
            """,
            """
            namespace Mod
            {
                public static class Main
                {
                    public static System.Action Changed;
                }
            }
            """),

        new(
            "FX0009",
            "default(T)",
            AnalyzerHarness.InMethod("            var value = default(double);"),
            AnalyzerHarness.InMethod("            double value = 0;")),

        new(
            "FX0010",
            "instance template that starts with {0}",
            """
            namespace Mod
            {
                public sealed class Widget
                {
                    /// @CSharpLua.Template = "{0}.size"
                    public double Size(double other) => 0;
                }
            }
            """,
            """
            namespace Mod
            {
                public static class Widgets
                {
                    /// @CSharpLua.Template = "{0}.size"
                    public static double Size(double other) => 0;
                }
            }
            """),

        new(
            "FX0011",
            "template holding a quoted table key",
            """
            namespace Mod
            {
                public static class Widgets
                {
                    /// @CSharpLua.Template = "util.make({ t, {"kind"} })"
                    public static double Make(double t) => 0;
                }
            }
            """,
            """
            namespace Mod
            {
                public static class Widgets
                {
                    /// @CSharpLua.Template = "util.make({0}, {1})"
                    public static double Make(double t, double kind) => 0;
                }
            }
            """),

        new(
            "FX0012",
            "Storage.Data used as a receiver",
            AnalyzerHarness.InMethod("            Storage.Data.Get(\"key\");"),
            AnalyzerHarness.InMethod("            var table = Storage.Table(\"key\");")),

        new(
            "FX0014",
            "CoreSystem-only .NET type",
            AnalyzerHarness.InMethod("            Console.WriteLine(\"hello\");"),
            AnalyzerHarness.InMethod("            Globals.Game.Print(\"hello\");")),

        new(
            "FX0013",
            "is/typeof",
            AnalyzerHarness.InMethod("            var isText = value is string;"),
            AnalyzerHarness.InMethod("            if (value == null) { }")),
    };
}
