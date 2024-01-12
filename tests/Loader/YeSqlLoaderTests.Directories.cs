namespace YeSql.Net.Tests.Loader;

public partial class YeSqlLoaderTests
{
    [Test]
    public void LoadFromDirectories_WhenErrorsAreFound_ShouldThrowAggregateException()
    {
        // Arrange
        var loader = new YeSqlLoader();
        var directories = new[]
        {
            "error",
            "directory_not_found",
            "env",
        };
        var loaderErrors = new[]
        {
            string.Format(ExceptionMessages.DirectoryNotFound, "directory_not_found")
        };
        var parserErrors = new[]
        {
            $"errors.sql:(line 2, col 1): error: {string.Format(ExceptionMessages.LineIsNotAssociatedWithAnyTag, "SELECT * FROM users;")}",
            $"errors.sql:(line 9, col 9): error: {ExceptionMessages.TagIsEmptyOrWhitespace}",
            $"errors.sql:(line 10, col 1): error: {string.Format(ExceptionMessages.LineIsNotAssociatedWithAnyTag, "SELECT name FROM roles;")}",
            $"errors.sql:(line 12, col 9): error: {string.Format(ExceptionMessages.DuplicateTagName, "GetUsers")}",
            $"errors.sql:(line 15, col 9): error: {ExceptionMessages.TagIsEmptyOrWhitespace}",
            $"errors.sql:(line 16, col 1): error: {string.Format(ExceptionMessages.LineIsNotAssociatedWithAnyTag, "SELECT * FROM roles;")}"
        };
        var expectedErrors = new[]
        {
            string.Join(Environment.NewLine, loaderErrors),
            string.Join(Environment.NewLine, parserErrors)
        };

        // Act
        Action action = () => loader.LoadFromDirectories(directories);

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
    public void LoadFromDirectories_WhenDirectoriesPathIsNull_ShouldThrowArgumentNullException()
    {
        // Arrange
        var loader = new YeSqlLoader();

        // Act
        Action action = () => loader.LoadFromDirectories(null);

        // Assert
        action.Should().Throw<ArgumentNullException>();
    }

    [TestCase("")]
    [TestCase(" ")]
    [TestCase("        ")]
    [TestCase("data/", null)]
    [TestCase("sql/", "non/", "  ", "", null, "tests/")]
    public void LoadFromDirectories_WhenCollectionHasNullValueOrOnlyWhitespace_ShouldThrowArgumentException(params string[] directories)
    {
        // Arrange
        var loader = new YeSqlLoader();
        var expectedMessage = string.Format(ExceptionMessages.CollectionHasNullValueOrOnlyWhitespace, nameof(directories));

        // Act
        Action action = () => loader.LoadFromDirectories(directories);

        // Assert
        action.Should()
              .Throw<ArgumentException>()
              .WithMessage(expectedMessage);
    }

    [Test]
    public void LoadFromDirectories_WhenNotExistsSqlFilesInDirectory_ShouldNotThrowException()
    {
        // Arrange
        var loader = new YeSqlLoader();
        var directory = "env";

        // Act
        var sqlStatements = loader.LoadFromDirectories(directory);

        // Assert
        sqlStatements.Should().BeEmpty();
    }

    [Test]
    public void LoadFromDirectories_WhenSqlFilesExistsInDirectory_ShouldReturnsYeSqlCollection()
    {
        // Arrange
        var loader = new YeSqlLoader();
        var directory = "./sql";
        var expectedCollection = new Dictionary<string, string>
        {
            { "GetUsers", "SELECT* FROM [user];" }
        };

        // Act
        var sqlStatements = loader.LoadFromDirectories(directory);

        // Assert
        sqlStatements.Should().BeEquivalentTo(expectedCollection);
    }

    [Test]
    public void LoadFromDirectories_WhenPathsAreAbsolute_ShouldBeAbleToLoadSqlFiles()
    {
        // Arrange
        var loader = new YeSqlLoader();
        var directory = Path.Combine(AppContext.BaseDirectory, "sql");
        var expectedCollection = new Dictionary<string, string>
        {
            { "GetUsers", "SELECT* FROM [user];" }
        };

        // Act
        var sqlStatements = loader.LoadFromDirectories(directory);

        // Assert
        sqlStatements.Should().BeEquivalentTo(expectedCollection);
    }

    [Test]
    public void LoadFromDirectories_WhenDirectoryNotExists_ShouldThrowAggregateException()
    {
        // Arrange
        var loader = new YeSqlLoader();
        var directory = "directory_not_found";
        var expectedMessage = string.Format(ExceptionMessages.DirectoryNotFound, directory);

        // Act
        Action action = () => loader.LoadFromDirectories(directory);

        // Asserts
        action.Should()
              .Throw<AggregateException>()
              .WithInnerException<YeSqlLoaderException>()
              .WithMessage(expectedMessage);
    }

    [Test]
    public void LoadFromDirectories_WhenParamsListIsZero_ShouldThrowArgumentException()
    {
        // Arrange
        var loader = new YeSqlLoader();
        var expectedMessage = ExceptionMessages.LengthOfParamsListIsZero;

        // Act
        Action action = () => loader.LoadFromDirectories();

        // Assert
        action.Should()
              .Throw<ArgumentException>()
              .WithMessage(expectedMessage);
    }
}
