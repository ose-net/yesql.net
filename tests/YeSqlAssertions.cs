namespace YeSql.Net.Tests;

/// <summary>
/// Represents custom assertions for YeSql.Tests.
/// </summary>
public class YeSqlAssertions : GenericCollectionAssertions<IYeSqlCollection, ModelTag, YeSqlAssertions>
{
    private readonly Dictionary<string, string> _sqlStatements;

    public YeSqlAssertions(IYeSqlCollection instance) : base(instance)
        => _sqlStatements = instance.ToDictionary(model => model.Name, model => model.SqlStatement);

    /// <summary>
    /// Adds new lines to the expected dictionary.
    /// </summary>
    private static void AddNewLinesToExpectation(Dictionary<string, string> expectation)
    {
        foreach (var key in expectation.Keys)
        {
            var value = expectation[key];
            if (!string.IsNullOrEmpty(value))
                expectation[key] = value + Environment.NewLine;
        }
    }

    /// <summary>
    /// Expects the current collection to contain the specified SQL statements in any order.
    /// </summary>
    // See why this attribute was used: https://fluentassertions.com/introduction#subject-identification.
    [CustomAssertion]
    public AndConstraint<YeSqlAssertions> BeEquivalentTo(
        Dictionary<string, string> expectation,
        string because = "", 
        params object[] becauseArgs)
    {
        AddNewLinesToExpectation(expectation);
        _sqlStatements.Should().BeEquivalentTo(expectation, because, becauseArgs);
        return new AndConstraint<YeSqlAssertions>(this);
    }

    protected override string Identifier => nameof(YeSqlAssertions);
}

public static class YeSqlCollectionExtensions
{
    public static YeSqlAssertions Should(this IYeSqlCollection instance)
        => new(instance);
}
