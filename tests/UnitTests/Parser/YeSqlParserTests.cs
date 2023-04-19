namespace UnitTests.Parser;

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
}
