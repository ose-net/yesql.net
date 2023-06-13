namespace YeSql.Net.Tests.Parser;

public class YeSqlParserTests
{
    [Test]
    public void Parse_WhenDataSourceIsNull_ShouldThrowArgumentNullException()
    {
        // Arrange
        var parser = new YeSqlParser();

        // Act
        Action action =  () => parser.Parse(null, out _);

        // Assert
        action.Should().Throw<ArgumentNullException>();
    }

    [TestCase("")]
    [TestCase("  ")]
    public void Parse_WhenDataSourceIsEmptyOrWithWhitespace_ShouldReturnsEmptyCollection(string source)
    {
        // Arrange
        var parser = new YeSqlParser();
        var expectedMessage = ExceptionMessages.DataSourceIsEmptyOrWhitespace;

        // Act
        var sqlStatements = parser.Parse(source, out var validationResult);

        // Asserts
        sqlStatements.Should().BeEmpty();
        validationResult.ErrorMessages.Should().Contain(expectedMessage);
    }

    [TestCaseSource(typeof(TagIsEmptyOrWhitespaceTestCases))]
    public void Parse_WhenTagIsEmptyOrConsistsOnlyOfWhitespaces_ShouldGenerateAnError(
        string source,
        List<string> expectedErrors)
    {
        // Arrange
        var parser = new YeSqlParser();
        var expectedCollection = new Dictionary<string, string>
        {
            { 
                "GetProducts",
                """
                SELECT
                name,
                price
                FROM products;
                """
            }
        };

        // Act
        var sqlStatements = parser.Parse(source, out var validationResult);
        var errors = validationResult.ToList();

        // Asserts
        sqlStatements.Should().BeEquivalentTo(expectedCollection);
        errors.Should().BeEquivalentTo(expectedErrors);
    }

    [TestCaseSource(typeof(TagNameIsDuplicatedTestCases))]
    public void Parse_WhenTagNameIsDuplicated_ShouldGenerateAnError(
        string source,
        List<string> expectedErrors)
    {
        // Arrange
        var parser = new YeSqlParser();
        var expectedCollection = new Dictionary<string, string>
        {
            { 
                "GetUsers",
                """
                SELECT id, name FROM users;
                SELECT email, name FROM users;
                SELECT email FROM users;
                """
            },
            {
                "GetProducts",
                """
                SELECT
                name,
                price
                FROM products;
                """
            }
        };

        // Act
        var sqlStatements = parser.Parse(source, out var validationResult);
        var errors = validationResult.ToList();

        // Asserts
        sqlStatements.Should().BeEquivalentTo(expectedCollection);
        errors.Should().BeEquivalentTo(expectedErrors);
    }

    [TestCaseSource(typeof(LineIsNotAssociatedWithAnyTagTestCases))]
    public void Parse_WhenLineIsNotAssociatedWithAnyTag_ShouldGenerateAnError(
        string source,
        List<string> expectedErrors)
    {
        // Arrange
        var parser = new YeSqlParser();
        var expectedCollection = new Dictionary<string, string>
        {
            {
                "GetProducts",
                """
                SELECT
                name,
                price
                FROM products;
                """
            }
        };

        // Act
        var sqlStatements = parser.Parse(source, out var validationResult);
        var errors = validationResult.ToList();

        // Asserts
        sqlStatements.Should().BeEquivalentTo(expectedCollection);
        errors.Should().BeEquivalentTo(expectedErrors);
    }

    [TestCaseSource(typeof(LineIsCommentWithoutTagTestCases))]
    public void Parse_WhenLineIsCommentWithoutTag_ShouldIgnoreComment(
        string source,
        Dictionary<string, string> expectedCollection)
    {
        // Arrange
        var parser = new YeSqlParser();

        // Act
        var sqlStatements = parser.Parse(source, out _);

        // Assert
        sqlStatements.Should().BeEquivalentTo(expectedCollection);
    }

    [TestCaseSource(typeof(TagHasNoSqlStatementTestCases))]
    public void Parse_WhenTagDoesNotHaveAnySqlStatement_ShouldReturnsTagWithEmptyString(string source)
    {
        // Arrange
        var parser = new YeSqlParser();
        var expectedCollection = new Dictionary<string, string>
        {
            { "GetProducts", "SELECT id, name, price FROM products;" },
            { "GetUsers", string.Empty },
            { "GetRoles", string.Empty }
        };

        // Act
        var sqlStatements = parser.Parse(source, out _);

        // Assert
        sqlStatements.Should().BeEquivalentTo(expectedCollection);
    }

    [TestCaseSource(typeof(LineIsCommentWithTagTestCases))]
    public void Parse_WhenLineIsCommentWithTag_ShouldExtractSqlStatements(string source)
    {
        // Arrange
        var parser = new YeSqlParser();
        var expectedCollection = new Dictionary<string, string>
        {
            {
               "GetProducts",
                """
                SELECT
                id,
                name,
                price
                FROM products;
                """
               },
               {
               "GetUsers",
                """
                SELECT
                id,
                name,
                email
                FROM users;
                """
               },
               {
               "GetRoles",
                """
                SELECT
                id,
                name
                FROM roles;
                """
            }
        };

        // Act
        var sqlStatements = parser.Parse(source, out _);

        // Assert
        sqlStatements.Should().BeEquivalentTo(expectedCollection);
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
            $"Parsing error (line 1, col 1): error: {string.Format(ExceptionMessages.LineIsNotAssociatedWithAnyTag, "    SELECT * FROM users;")}",
            $"Parsing error (line 8, col 13): error: {ExceptionMessages.TagIsEmptyOrWhitespace}",
            $"Parsing error (line 9, col 1): error: {string.Format(ExceptionMessages.LineIsNotAssociatedWithAnyTag, "    SELECT name FROM roles;")}",
            $"Parsing error (line 11, col 13): error: {string.Format(ExceptionMessages.DuplicateTagName, "GetUsers")}",
            $"Parsing error (line 14, col 13): error: {ExceptionMessages.TagIsEmptyOrWhitespace}",
            $"Parsing error (line 15, col 1): error: {string.Format(ExceptionMessages.LineIsNotAssociatedWithAnyTag, "    SELECT * FROM roles;")}",
        };

        // Act
        parser.Parse(source, out var validationResult);
        var errors = validationResult.ToList();

        // Asserts
        validationResult.HasError().Should().BeTrue();
        errors.Should().BeEquivalentTo(expectedErrors);
    }

    [TestCase]
    public void ParseAndThrow_WhenErrorsAreFound_ShouldThrowYeSqlParserException()
    {
        // Arrange
        var parser = new YeSqlParser();
        var source =
        """
            -- name: GetUsers
            SELECT * FROM users;

            -- name:
            SELECT name FROM roles;

            -- name:  GetUsers    
            SELECT name FROM users;

            -- name:          
            SELECT * FROM roles;
        """;

        // Act
        Action action = () => parser.ParseAndThrow(source);

        // Assert
        action.Should().Throw<YeSqlParserException>();
    }

    [TestCase]
    public void ParseAndThrow_WhenNoErrorsAreFound_ShouldNotThrowAnException()
    {
        // Arrange
        var parser = new YeSqlParser();
        var source =
        """
            -- name: GetUsers
            SELECT * FROM users;
        """;

        // Act
        Action action = () => parser.ParseAndThrow(source);

        // Assert
        action.Should().NotThrow<YeSqlParserException>();
    }
}
