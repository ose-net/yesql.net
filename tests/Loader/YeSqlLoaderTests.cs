namespace YeSql.Net.Tests.Loader;

public partial class YeSqlLoaderTests
{
    [Test]
    public void LoadFromDefaultDirectory_WhenSqlFilesExistInDefaultDirectoryCalledYesql_ShouldReturnsSqlCollection()
    {
        // Arrange
        var loader = new YeSqlLoader();
        var expectedCollection = new Dictionary<string, string>
        {
            { "GetProducts", "SELECT * FROM products;" }
        };

        // Act
        ISqlCollection sqlStatements = loader.LoadFromDefaultDirectory();

        // Assert
        sqlStatements.Should().BeEquivalentTo(expectedCollection);
    }
}
