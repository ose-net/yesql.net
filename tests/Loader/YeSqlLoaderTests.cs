using System.Text;

namespace YeSql.Net.Tests.Loader;

public class YeSqlLoaderTests
{
    [Test]
    public void Loader_WhenSqlFilesIsNull_ShouldReturnsThrowArgumentNullException()
    {
        // Arrange
        var loader = new YeSqlLoader();

        // Act
        Action action = () => loader.LoadFromFiles(null);

        // Assert
        action.Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void Loader_WhenSqlFilesExists_ShouldReturnsYeSqlCollection()
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
    public void Loader_WhenDirectoriesPathIsNull_ShouldReturnsThrowArgumentNullException()
    {
        // Arrange
        var loader = new YeSqlLoader();

        // Act
        Action action = () => loader.LoadFromDirectories(null);

        // Assert
        action.Should().Throw<ArgumentNullException>();
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
}
