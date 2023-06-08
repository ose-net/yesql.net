namespace YeSql.Net.Tests.Loader;

public class YeSqlLoaderTests
{
    [Test]
    public void LoadFromFiles_WhenSqlFilesIsNull_ShouldReturnsThrowArgumentNullException()
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
        // Arranges
        var loader = new YeSqlLoader();
        var path = CreateSqlFile();

        // Acts
        var collection = loader.LoadFromFiles(path);
        File.Delete(path);

        // Asserts
        collection.Should().NotBeNull();
    }

    [Test]
    public void LoadFromFiles_WhenFileNotHaveExtension_ShouldReturnsThrowAggregateException()
    {
        // Arranges
        var loader = new YeSqlLoader();
        string path = CreateFile();

        // Act
        Action action = () => loader.LoadFromFiles(path);

        // Asserts
        action.Should().Throw<YeSqlLoaderException>()
                       .WithMessage(string.Format(ExceptionMessages.FileHasNotSqlExtension, path));
        action.Should().Throw<AggregateException>();
    }

    [Test]
    public void LoadFromFiles_WhenSqlFileNotExists_ShouldReturnsThrowAggregateException()
    {
        // Arranges
        var loader = new YeSqlLoader();
        string path = $"{Directory.GetCurrentDirectory()}/test.sql";

        // Act
        Action action = () => loader.LoadFromFiles(path);

        // Asserts
        action.Should().Throw<YeSqlLoaderException>()
                       .WithMessage(string.Format(ExceptionMessages.FileNotFound, path));
        action.Should().Throw<AggregateException>();
    }


    [Test]
    public void LoadFromDirectories_WhenDirectoriesPathIsNull_ShouldReturnsThrowArgumentNullException()
    {
        // Arrange
        var loader = new YeSqlLoader();

        // Act
        Action action = () => loader.LoadFromDirectories(null);

        // Assert
        action.Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void LoadFromDirectories_WhenNotExistsSqlFilesInDirectory_ShouldReturnsThrowAggregateException()
    {
        // Arrange
        var loader = new YeSqlLoader();
        string path = Directory.GetCurrentDirectory();

        // Act
        Action action = () => loader.LoadFromDirectories(path);

        // Assert
        action.Should().Throw<YeSqlLoaderException>()
                       .WithMessage(string.Format(ExceptionMessages.NoneFileFoundInSpecifiedDirectory, path));
        action.Should().Throw<AggregateException>();
    }

    [Test]
    public void LoadFromDirectories_WhenSqlFilesExistsInDirectory_ShouldReturnsYeSqlCollection()
    {
        // Arranges
        var loader = new YeSqlLoader();
        string pathDirectory = Directory.GetCurrentDirectory();
        string pathFile = CreateSqlFile();

        // Acts
        var collection = loader.LoadFromDirectories(pathDirectory);
        File.Delete(pathFile);

        // Asserts
        collection.Should().NotBeNull();
    }

    private static string CreateSqlFile()
    {
        string path = $"{Directory.GetCurrentDirectory()}/test.sql";
        using var fileStream = new FileStream(path, FileMode.Create);
        string content = """
                         -- name: GetUsers
                         -- Gets user records.
                         SELECT* FROM [user];
                         """;
        byte[] bytes = Encoding.UTF8.GetBytes(content);
        fileStream.Write(bytes, 0, bytes.Length);
        fileStream.Close();

        return path;
    }

    private static string CreateFile()
    {
        string path = $"{Directory.GetCurrentDirectory()}/test.txt";
        using var fileStream = new FileStream(path, FileMode.Create);
        string content = """
                         -- name: GetUsers
                         -- Gets user records.
                         SELECT* FROM [user];
                         """;
        byte[] bytes = Encoding.UTF8.GetBytes(content);
        fileStream.Write(bytes, 0, bytes.Length);
        fileStream.Close();

        return path;
    }
}
