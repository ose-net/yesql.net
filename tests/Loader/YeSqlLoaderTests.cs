namespace YeSql.Net.Tests.Loader;

public partial class YeSqlLoaderTests
{
    [Test]
    public void Load_WhenSqlFilesExistInDefaultDirectoryCalledYesql_ShouldReturnsYeSqlCollection()
    {
        // Arrange
        var loader = new YeSqlLoader();
        var expectedCollection = new Dictionary<string, string>
        {
            { "GetProducts", "SELECT * FROM products;" }
        };

        // Act
        var sqlStatements = loader.Load();

        // Assert
        sqlStatements.Should().BeEquivalentTo(expectedCollection);
    }
}
