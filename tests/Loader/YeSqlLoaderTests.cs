﻿namespace YeSql.Net.Tests.Loader;

public class YeSqlLoaderTests
{
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
        var path = CreateSqlFile();

        // Act
        var collection = loader.LoadFromFiles(path);
        File.Delete(path);

        // Assert
        collection.Should().NotBeNull();
    }

    [Test]
    public void LoadFromFiles_WhenFileNotHaveExtension_ShouldThrowAggregateException()
    {
        // Arrange
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
    public void LoadFromFiles_WhenSqlFileNotExists_ShouldThrowAggregateException()
    {
        // Arrange
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
    public void LoadFromDirectories_WhenDirectoriesPathIsNull_ShouldThrowArgumentNullException()
    {
        // Arrange
        var loader = new YeSqlLoader();

        // Act
        Action action = () => loader.LoadFromDirectories(null);

        // Assert
        action.Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void LoadFromDirectories_WhenNotExistsSqlFilesInDirectory_ShouldThrowAggregateException()
    {
        // Arrange
        var loader = new YeSqlLoader();
        string path = Directory.GetCurrentDirectory();

        // Act
        Action action = () => loader.LoadFromDirectories(path);

        // Asserts
        action.Should().Throw<YeSqlLoaderException>()
                       .WithMessage(string.Format(ExceptionMessages.NoneFileFoundInSpecifiedDirectory, path));
        action.Should().Throw<AggregateException>();
    }

    [Test]
    public void LoadFromDirectories_WhenSqlFilesExistsInDirectory_ShouldReturnsYeSqlCollection()
    {
        // Arrange
        var loader = new YeSqlLoader();
        string pathDirectory = Directory.GetCurrentDirectory();
        string pathFile = CreateSqlFile();

        // Act
        var collection = loader.LoadFromDirectories(pathDirectory);
        File.Delete(pathFile);

        // Assert
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
