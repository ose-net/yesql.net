namespace YeSql.Net.Tests;

/// <summary>
/// Represents custom assertions for YeSql.Tests.
/// </summary>
/// <remarks>
/// <para>
/// It has been decided to create a custom assertion with the purpose of adding an additional code 
/// that takes care of adding a new line at the end of each SQL statement of the expected dictionary.
/// </para>
/// This is because the current parser adds a new line at the end of each processed SQL statement, therefore, 
/// to perform the comparison between the current and the expected value, both need to have the new line at the end of each SQL statement.
/// </remarks>
public class YeSqlAssertions : GenericCollectionAssertions<ISqlCollection, ModelTag, YeSqlAssertions>
{
    private readonly Dictionary<string, string> _sqlStatements;

    public YeSqlAssertions(ISqlCollection instance) : base(instance)
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
    public static YeSqlAssertions Should(this ISqlCollection instance)
        => new(instance);
}
