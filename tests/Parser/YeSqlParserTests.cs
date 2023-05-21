namespace YeSql.Net.Tests.Parser;

public class YeSqlParserTests
{
    [TestCase("")]
    [TestCase("  ")]
    public void Parse_WhenDataSourceIsEmptyOrWithWhitespace_ShouldReturnsEmptyCollection(string source)
    {
        // Arrange
        var parser = new YeSqlParser();
        var expectedMessage = ExceptionMessages.DataSourceIsEmptyOrWhitespaceMessage;

        // Act
        var actual = parser.Parse(source, out var validationResult);

        // Asserts
        actual.Should().BeEmpty();
        validationResult.ErrorMessages.Should().Contain(expectedMessage);
    }

    [TestCaseSource(typeof(TagIsEmptyOrWhitespaceTestCases))]
    public void Parse_WhenTagIsEmptyOrConsistsOnlyOfWhitespaces_ShouldGenerateAnError(
        string source,
        List<string> expectedErrors)
    {
        // Arrange
        var parser = new YeSqlParser();

        // Act
        var actual = parser.Parse(source, out var validationResult);
        var errors = validationResult.ToList();

        // Asserts
        actual.Should().HaveCount(1);
        errors.Should().BeEquivalentTo(expectedErrors);
    }

    [TestCaseSource(typeof(TagNameIsDuplicatedTestCases))]
    public void Parse_WhenTagNameIsDuplicated_ShouldGenerateAnError(
        string source,
        List<string> expectedErrors)
    {
        // Arrange
        var parser = new YeSqlParser();
        var expectedSql =
       $"""
        SELECT id, name FROM users;
        SELECT email, name FROM users;
        SELECT email FROM users;{NewLine}
        """;

        // Act
        var actual = parser.Parse(source, out var validationResult);
        var errors = validationResult.ToList();

        // Asserts
        actual.Should().HaveCount(2);
        actual["GetUsers"].Should().Be(expectedSql);
        errors.Should().BeEquivalentTo(expectedErrors);
    }

    [TestCaseSource(typeof(LineIsNotAssociatedWithAnyTagTestCases))]
    public void Parse_WhenLineIsNotAssociatedWithAnyTag_ShouldGenerateAnError(
        string source,
        List<string> expectedErrors)
    {
        // Arrange
        var parser = new YeSqlParser();

        // Act
        var actual = parser.Parse(source, out var validationResult);
        var errors = validationResult.ToList();

        // Asserts
        actual.Should().HaveCount(1);
        errors.Should().BeEquivalentTo(expectedErrors);
    }
}
