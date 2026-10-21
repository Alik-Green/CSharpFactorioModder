using Xunit;
namespace Factorio.Analyzers.Tests;

/// <summary>
/// Every rule, both ways round: the construct is caught, and the construct that merely looks like
/// it is not.
/// </summary>
public class RuleTests
{
    /// <summary>The positive fixture reports exactly the expected rule, and nothing else.</summary>
    /// <param name="testCase">One row of <see cref="RuleCases"/>.</param>
    [Theory]
    [MemberData(nameof(RuleCases.All), MemberType = typeof(RuleCases))]
    public void PositiveCaseReportsExactlyThatRule(RuleCase testCase)
    {
        var ids = AnalyzerHarness.Ids(testCase.Positive);

        Assert.True(
            ids.SequenceEqual(new[] { testCase.Id }),
            $"{testCase.Name}: expected only {testCase.Id}, got [{string.Join(", ", ids)}]" + Environment.NewLine +
            string.Join(Environment.NewLine, AnalyzerHarness.Run(testCase.Positive)));
    }

    /// <summary>The negative fixture is silent.</summary>
    /// <param name="testCase">One row of <see cref="RuleCases"/>.</param>
    [Theory]
    [MemberData(nameof(RuleCases.All), MemberType = typeof(RuleCases))]
    public void NegativeCaseIsSilent(RuleCase testCase)
    {
        var findings = AnalyzerHarness.Run(testCase.Negative);

        Assert.True(
            findings.Count == 0,
            $"{testCase.Name}: expected no findings, got" + Environment.NewLine + string.Join(Environment.NewLine, findings));
    }

    /// <summary>
    /// Every rule in the catalog has at least one fixture, and every fixture names a real rule. This
    /// is what stops a rule from being added without a test, or deleted while its test stays behind.
    /// </summary>
    [Fact]
    public void EveryRuleHasAFixture()
    {
        var catalog = AnalyzerCatalog.Descriptors.Select(d => d.Id).OrderBy(id => id, StringComparer.Ordinal).ToList();
        var covered = RuleCases.Cases.Select(c => c.Id).Distinct().OrderBy(id => id, StringComparer.Ordinal).ToList();

        Assert.Equal(catalog, covered);
    }

    /// <summary>
    /// The rule ids a rule class advertises are the ones its catalog entries declare: a typo here
    /// would make Roslyn reject the diagnostic at run time, or silently drop it.
    /// </summary>
    [Fact]
    public void RuleClassesAdvertiseTheirCatalogDescriptors()
    {
        var catalog = AnalyzerCatalog.Descriptors.Select(d => d.Id).ToHashSet(StringComparer.Ordinal);
        var supported = AnalyzerCatalog.All
            .SelectMany(analyzer => analyzer.SupportedDiagnostics)
            .Select(descriptor => descriptor.Id)
            .ToList();

        Assert.All(supported, id => Assert.Contains(id, catalog));
        Assert.Equal(catalog.OrderBy(id => id, StringComparer.Ordinal),
            supported.Distinct().OrderBy(id => id, StringComparer.Ordinal));
    }

    /// <summary>Every descriptor carries a help link and a category, so an editor can explain it.</summary>
    [Fact]
    public void DescriptorsAreDocumented()
    {
        Assert.All(AnalyzerCatalog.Descriptors, descriptor =>
        {
            Assert.StartsWith("FX00", descriptor.Id);
            Assert.False(string.IsNullOrWhiteSpace(descriptor.Title.ToString()));
            Assert.False(string.IsNullOrWhiteSpace(descriptor.MessageFormat.ToString()));
            Assert.False(string.IsNullOrWhiteSpace(descriptor.Description.ToString()));
            Assert.Equal(
                "https://github.com/factorio-csharp/factorio-csharp/blob/main/docs/ANALYZERS.md#" +
                descriptor.Id.ToLowerInvariant(),
                descriptor.HelpLinkUri);
        });
    }
}
