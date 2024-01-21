namespace Example.AspNetCore.Tests;

public class SqlTests
{
    [TestCase("/api/Sql/GetUsersSql",          "SELECT* FROM [user];")]
    [TestCase("/api/Sql/GetRolesSql",          "SELECT* FROM [role];")]
    [TestCase("/api/Sql/GetProductsSql",       "SELECT* FROM [product];")]
    [TestCase("/api/Sql/GetCustomersSql",      "SELECT* FROM [customer];")]
    [TestCase("/api/Sql/GetOrdersSql",         "SELECT* FROM [order];")]
    [TestCase("/api/Sql/GetPermissionsSql",    "SELECT* FROM [permission];")]
    [TestCase("/api/Sql/GetThirdPartiesSql",   "SELECT* FROM [third_party];")]
    [TestCase("/api/Sql/GetReceiptsSql",       "SELECT* FROM [receipt];")]
    [TestCase("/api/Sql/GetReceiptDetailsSql", "SELECT* FROM [receipt_details];")]
    public async Task Get_WhenSqlCodeIsRetrieved_ShouldReturnsHttpStatusCodeOk(
        string requestUri,
        string expectedSql)
    {
        // Arrange
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
        result.Should().Be(expectedSql);
    }
}
