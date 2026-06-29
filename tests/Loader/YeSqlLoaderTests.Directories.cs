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
        ISqlCollection sqlStatements = loader.LoadFromDirectories(directory);

        // Assert
        sqlStatements.Should().BeEmpty();
    }

    [Test]
    public void LoadFromDirectories_WhenSqlFilesExistsInDirectory_ShouldReturnSqlCollection()
    {
        // Arrange
        var loader = new YeSqlLoader();
        var directory = "./sql";
        var expectedCollection = new Dictionary<string, string>
        {
            { "GetUsers", "SELECT* FROM [user];" }
        };

        // Act
        ISqlCollection sqlStatements = loader.LoadFromDirectories(directory);

        // Assert
        sqlStatements.Should().BeEquivalentTo(expectedCollection);
    }

    [Test]
    public void LoadFromDirectories_WhenPathsAreAbsolute_ShouldBeAbleToLoadSqlFiles()
    {
        // Arrange
        var loader = new YeSqlLoader();
        var absolutePath = Path.Combine(PathResolver.BaseDirectory, "sql");
        var expectedCollection = new Dictionary<string, string>
        {
            { "GetUsers", "SELECT* FROM [user];" }
        };

        // Act
        ISqlCollection sqlStatements = loader.LoadFromDirectories(absolutePath);

        // Assert
        sqlStatements.Should().BeEquivalentTo(expectedCollection);
    }

    [TestCase("../../../Loader/Resources/sql")]
    [TestCase("../../../Loader/Resources/sql/")]
    public void LoadFromDirectories_WhenPathsAreRelative_ShouldBeAbleToLoadSqlFiles(string relativePath)
    {
        // Arrange
        var loader = new YeSqlLoader();
        var expectedCollection = new Dictionary<string, string>
        {
            { "GetUsers", "SELECT* FROM [user];" }
        };

        // Act
        ISqlCollection sqlStatements = loader.LoadFromDirectories(relativePath);

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
    public void LoadFromDirectories_WhenParamsListIsZero_ShouldNotThrowException()
    {
        // Arrange
        var loader = new YeSqlLoader();

        // Act
        ISqlCollection sqlStatements = loader.LoadFromDirectories();

        // Assert
        sqlStatements.Should().BeEmpty();
    }

    [Test]
    public void LoadFromDirectories_WhenExcludedPathIsProvided_ShouldNotLoadStatements()
    {
        // Arrange
        var directory = "./exclude";
        var loader = new YeSqlLoader()
            .Exclude("exclude/players.sql");

        // Act
        ISqlCollection sqlStatements = loader.LoadFromDirectories(directory);

        // Assert
        sqlStatements.TryGetStatement("GetPlayers", out _)
                     .Should()
                     .BeFalse();
    }

    [Test]
    public void LoadFromDirectories_WhenNoFilesAreExcluded_ShouldLoadStatements()
    {
        // Arrange
        var loader = new YeSqlLoader();
        var directory = "./exclude";
        var expectedCollection = new Dictionary<string, string>
        {
            { "GetPlayers", "SELECT * FROM players;" },
            { "CreatePlayer", "INSERT INTO players (name) VALUES ('Admin_Player');" }
        };

        // Act
        ISqlCollection sqlStatements = loader.LoadFromDirectories(directory);

        // Assert
        sqlStatements.Should().BeEquivalentTo(expectedCollection);
    }

    [Test]
    public void LoadFromDirectories_WhenSqlFileIsExcluded_ShouldNotLoadStatements()
    {
        // Arrange
        var directory = "./exclude";
        var loader = new YeSqlLoader()
            .Exclude("players.sql");

        // Act
        ISqlCollection sqlStatements = loader.LoadFromDirectories(directory);

        // Assert
        sqlStatements.TryGetStatement("GetPlayers", out _)
                     .Should()
                     .BeFalse();
    }

    [Test]
    public void LoadFromDirectories_WhenExcludedSqlFileDoesNotExist_ShouldNotThrowException()
    {
        // Arrange
        var directory = "./exclude";
        var loader = new YeSqlLoader()
            .Exclude("file_not_exists.sql");

        // Act
        ISqlCollection sqlStatements = loader.LoadFromDirectories(directory);

        // Assert
        sqlStatements.Should().NotBeEmpty();
    }

    [Test]
    public void LoadFromDirectories_WhenAllSqlFilesAreExcluded_ShouldReturnEmptyCollection()
    {
        // Arrange
        var directory = "./exclude";
        var loader = new YeSqlLoader()
            .Exclude("players.sql", "seed_data.sql");

        // Act
        ISqlCollection sqlStatements = loader.LoadFromDirectories(directory);

        // Assert
        sqlStatements.Should().BeEmpty();
    }
}
