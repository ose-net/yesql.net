namespace YeSql.Net.Tests.Loader;

public partial class YeSqlLoaderTests
{
    [Test]
    public void LoadFromFiles_WhenErrorsAreFound_ShouldThrowAggregateException()
    {
        // Arrange
        var loader = new YeSqlLoader();
        var files = new[]
        {
            "errors/errors_1.sql",
            "errors/errors_2.sql",
            "file_not_found.sql",
            "file_without_extension"
        };
        var loaderErrors = new[]
        {
            string.Format(ExceptionMessages.FileNotFound, "file_not_found.sql"),
            string.Format(ExceptionMessages.FileHasNotSqlExtension, "file_without_extension")
        };
        var parserErrors = new[]
        {
            $"errors_1.sql:(line 2, col 1): error: {string.Format(ExceptionMessages.LineIsNotAssociatedWithAnyTag, "SELECT * FROM users;")}",
            $"errors_1.sql:(line 9, col 9): error: {ExceptionMessages.TagIsEmptyOrWhitespace}",
            $"errors_1.sql:(line 10, col 1): error: {string.Format(ExceptionMessages.LineIsNotAssociatedWithAnyTag, "SELECT name FROM roles;")}",
            $"errors_2.sql:(line 2, col 9): error: {string.Format(ExceptionMessages.DuplicateTagName, "GetUsers")}",
            $"errors_2.sql:(line 5, col 9): error: {ExceptionMessages.TagIsEmptyOrWhitespace}",
            $"errors_2.sql:(line 6, col 1): error: {string.Format(ExceptionMessages.LineIsNotAssociatedWithAnyTag, "SELECT * FROM roles;")}"
        };
        var expectedErrors = new[]
        {
            string.Join(Environment.NewLine, loaderErrors),
            string.Join(Environment.NewLine, parserErrors)
        };

        // Act
        Action action = () => loader.LoadFromFiles(files);

        // Assert
        action.Should()
              .Throw<AggregateException>()
              .Which
              .InnerExceptions
              .Select(innerException => innerException.Message)
              .Should()
              .BeEquivalentTo(expectedErrors);
    }

    [Test]
    public void LoadFromFiles_WhenSqlFilesIsNull_ShouldThrowArgumentNullException()
    {
        // Arrange
        var loader = new YeSqlLoader();

        // Act
        Action action = () => loader.LoadFromFiles(null);

        // Assert
        action.Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void LoadFromFiles_WhenSqlFilesExists_ShouldReturnsYeSqlCollection()
    {
        // Arrange
        var loader = new YeSqlLoader();
        var file = "./sql/users.sql";
        var expectedCollection = new Dictionary<string, string>
        {
            { "GetUsers", "SELECT* FROM [user];" }
        };

        // Act
        var sqlStatements = loader.LoadFromFiles(file);

        // Assert
        sqlStatements.Should().BeEquivalentTo(expectedCollection);
    }

    [Test]
    public void LoadFromFiles_WhenPathsAreAbsolute_ShouldBeAbleToLoadSqlFiles()
    {
        // Arrange
        var loader = new YeSqlLoader();
        var file = Path.Combine(AppContext.BaseDirectory, "sql/users.sql");
        var expectedCollection = new Dictionary<string, string>
        {
            { "GetUsers", "SELECT* FROM [user];" }
        };

        // Act
        var sqlStatements = loader.LoadFromFiles(file);

        // Assert
        sqlStatements.Should().BeEquivalentTo(expectedCollection);
    }

    [TestCase("test.txt")]
    [TestCase("test.png")]
    [TestCase("test")]
    [TestCase("test/")]
    public void LoadFromFiles_WhenFileHasNotSqlExtension_ShouldThrowAggregateException(string file)
    {
        // Arrange
        var loader = new YeSqlLoader();
        var expectedMessage = string.Format(ExceptionMessages.FileHasNotSqlExtension, file);

        // Act
        Action action = () => loader.LoadFromFiles(file);

        // Assert
        action.Should()
              .Throw<AggregateException>()
              .WithInnerException<YeSqlLoaderException>()
              .WithMessage(expectedMessage);
    }

    [Test]
    public void LoadFromFiles_WhenSqlFileNotExists_ShouldThrowAggregateException()
    {
        // Arrange
        var loader = new YeSqlLoader();
        var file = "file_not_found.sql";
        var expectedMessage = string.Format(ExceptionMessages.FileNotFound, file);

        // Act
        Action action = () => loader.LoadFromFiles(file);

        // Assert
        action.Should()
              .Throw<AggregateException>()
              .WithInnerException<YeSqlLoaderException>()
              .WithMessage(expectedMessage);
    }

    [TestCase("")]
    [TestCase(" ")]
    [TestCase("        ")]
    [TestCase("data.sql", null)]
    [TestCase("data.sql", "test.sql", "  ", "", null, "hola.sql")]
    public void LoadFromFiles_WhenCollectionHasNullValueOrOnlyWhitespace_ShouldThrowArgumentException(params string[] sqlFiles)
    {
        // Arrange
        var loader = new YeSqlLoader();
        var expectedMessage = string.Format(ExceptionMessages.CollectionHasNullValueOrOnlyWhitespace, nameof(sqlFiles));

        // Act
        Action action = () => loader.LoadFromFiles(sqlFiles);

        // Assert
        action.Should()
              .Throw<ArgumentException>()
              .WithMessage(expectedMessage);
    }

    [Test]
    public void LoadFromFiles_WhenParamsListIsZero_ShouldNotThrowException()
    {
        // Arrange
        var loader = new YeSqlLoader();

        // Act
        var sqlStatements = loader.LoadFromFiles();

        // Assert
        sqlStatements.Should().BeEmpty();
    }
}
