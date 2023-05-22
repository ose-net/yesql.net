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

    [TestCaseSource(typeof(LineIsCommentWithoutTagTestCases))]
    public void Parse_WhenLineIsCommentWithoutTag_ShouldIgnoreComment(
        string source,
        Dictionary<string, string> expectedSqlStatements)
    {
        // Arrange
        var parser = new YeSqlParser();

        // Act
        var actual = parser.Parse(source, out _).ToDictionary();

        // Assert
        actual.Should().Equal(expectedSqlStatements);
    }

    [TestCaseSource(typeof(TagHasNoSqlStatementTestCases))]
    public void Parse_WhenTagDoesNotHaveAnySqlStatement_ShouldReturnsTagWithEmptyString(string source)
    {
        // Arrange
        var parser = new YeSqlParser();
        var expectedSqlStatements = new Dictionary<string, string>
        {
            { "GetProducts", $"SELECT id, name, price FROM products;{NewLine}" },
            { "GetUsers", string.Empty },
            { "GetRoles", string.Empty }
        };

        // Act
        var actual = parser.Parse(source, out _).ToDictionary();

        // Assert
        actual.Should().Equal(expectedSqlStatements);
    }

    [TestCaseSource(typeof(LineIsCommentWithTagTestCases))]
    public void Parse_WhenLineIsCommentWithTag_ShouldExtractSqlStatements(string source)
    {
        // Arrange
        var parser = new YeSqlParser();
        var expectedSqlStatements = new Dictionary<string, string>
        {
            {
               "GetProducts",
               $"""
                SELECT
                id,
                name,
                price
                FROM products;{NewLine}
                """
               },
               {
               "GetUsers",
               $"""
                SELECT
                id,
                name,
                email
                FROM users;{NewLine}
                """
               },
               {
               "GetRoles",
               $"""
                SELECT
                id,
                name
                FROM roles;{NewLine}
                """
            }
        };

        // Act
        var actual = parser.Parse(source, out _).ToDictionary();

        // Assert
        actual.Should().Equal(expectedSqlStatements);
    }

    [TestCase]
    public void Parse_WhenAnErrorIsFound_ShouldStoreErrorMessageInCollection()
    {
        // Arrange
        var parser = new YeSqlParser();
        var source =
        """
            SELECT * FROM users;
            -- name: GetProducts
            SELECT id, name, price FROM products;

            -- name: GetUsers
            SELECT id, name, email FROM users;

            -- name:
            SELECT name FROM roles;

            -- name:  GetUsers    
            SELECT name FROM users;

            -- name:          
            SELECT * FROM roles;
        """;
        var expectedErrors = new List<string>()
        {
            $"Parsing error (line 1, col 1): error: {string.Format(ExceptionMessages.LineIsNotAssociatedWithAnyTagMessage, "    SELECT * FROM users;")}",
            $"Parsing error (line 8, col 13): error: {ExceptionMessages.TagIsEmptyOrWhitespaceMessage}",
            $"Parsing error (line 9, col 1): error: {string.Format(ExceptionMessages.LineIsNotAssociatedWithAnyTagMessage, "    SELECT name FROM roles;")}",
            $"Parsing error (line 11, col 13): error: {string.Format(ExceptionMessages.DuplicateTagNameMessage, "GetUsers")}",
            $"Parsing error (line 14, col 13): error: {ExceptionMessages.TagIsEmptyOrWhitespaceMessage}",
            $"Parsing error (line 15, col 1): error: {string.Format(ExceptionMessages.LineIsNotAssociatedWithAnyTagMessage, "    SELECT * FROM roles;")}",
        };

        // Act
        parser.Parse(source, out var validationResult);
        var errors = validationResult.ToList();

        // Asserts
        validationResult.HasError().Should().BeTrue();
        errors.Should().BeEquivalentTo(expectedErrors);
    }
}
