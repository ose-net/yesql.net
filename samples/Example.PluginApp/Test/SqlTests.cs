namespace PluginApp.Host.Tests;

public class SqlTests
{
    [TestCase("/api/EmployeeSql",     "SELECT * FROM [employee];")]
    [TestCase("/api/UserSql",         "SELECT * FROM [user];")]
    [TestCase("/api/Sql/GetOrderSql", "SELECT * FROM [order];")]
    [TestCase("/api/Hello",           "Hello World!")]
    public async Task Get_WhenSqlCodeIsRetrieved_ShouldReturnsHttpStatusCodeOk(
        string requestUri,
        string expectedResult)
    {
        // Arrange
        Environment.SetEnvironmentVariable("PLUGINS", "");
        using var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();

        // Act
        var httpResponse = await client.GetAsync(requestUri);
        var result = (await httpResponse
            .Content
            .ReadAsStringAsync())
            .TrimEnd(Environment.NewLine.ToCharArray());

        // Asserts
        httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        result.Should().Be(expectedResult);
    }

    [Test]
    public async Task Get_WhenNoPluginIsLoaded_ShouldNotThrowException()
    {
        // Arrange
        Environment.SetEnvironmentVariable("PLUGINS", "  ");
        using var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();
        var requestUri = "/api/Hello";

        // Act
        var httpResponse = await client.GetAsync(requestUri);

        // Assert
        httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
