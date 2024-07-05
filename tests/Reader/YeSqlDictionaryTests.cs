namespace YeSql.Net.Tests.Reader;

public class YeSqlDictionaryTests
{
    [Test]
    public void Indexer_WhenTagNameIsNull_ShouldThrowArgumentNullException()
    {
        // Arrange
        var sqlCollection = new YeSqlDictionary();
        var tagName = default(string);

        // Act
        Action action = () =>
        {
            var value = sqlCollection[tagName];
        };

        // Assert
        action.Should()
              .Throw<ArgumentNullException>()
              .WithParameterName(nameof(tagName));
    }

    [Test]
    public void Indexer_WhenTagNameIsNotFound_ShouldThrowTagNotFoundException()
    {
        // Arrange
        var sqlCollection = new YeSqlDictionary();
        var tagName = "Test";
        var expectedMessage = string.Format(ExceptionMessages.TagNotFound, tagName);

        // Act
        Action action = () =>
        {
            var value = sqlCollection[tagName];
        };

        // Assert
        action.Should()
              .Throw<TagNotFoundException>()
              .WithMessage(expectedMessage);
    }

    [Test]
    public void Indexer_WhenTagNameIsFound_ShouldReturnsSqlStatement()
    {
        // Arrange
        var sqlCollection = new YeSqlDictionary();
        var tagName = "GetUsers";
        var expectedSql = "SELECT* FROM users;";
        sqlCollection[tagName] = expectedSql;

        // Act
        string actual = sqlCollection[tagName];

        // Assert
        actual.Should().Be(expectedSql);
    }

    [Test]
    public void TryGetStatement_WhenTagNameIsNull_ShouldThrowArgumentNullException()
    {
        // Arrange
        var sqlCollection = new YeSqlDictionary();
        var tagName = default(string);

        // Act
        Action action = () =>
        {
            var value = sqlCollection.TryGetStatement(tagName, out _);
        };

        // Assert
        action.Should()
              .Throw<ArgumentNullException>()
              .WithParameterName(nameof(tagName));
    }

    [Test]
    public void TryGetStatement_WhenTagNameIsNotFound_ShouldReturnsFalse()
    {
        // Arrange
        var sqlCollection = new YeSqlDictionary();
        var tagName = "Test";

        // Act
        bool actual = sqlCollection.TryGetStatement(tagName, out _);

        // Assert
        actual.Should().BeFalse();
    }

    [Test]
    public void TryGetStatement_WhenTagNameIsFound_ShouldReturnsTrue()
    {
        // Arrange
        var sqlCollection = new YeSqlDictionary();
        var tagName = "GetUsers";
        var expectedSql = "SELECT* FROM users;";
        sqlCollection[tagName] = expectedSql;

        // Act
        bool actual = sqlCollection.TryGetStatement(tagName, out string currentSql);

        // Asserts
        actual.Should().BeTrue();
        currentSql.Should().Be(expectedSql);
    }

    [Test]
    public void ToString_WhenConvertingYeSqlDictionaryToString_ShouldReturnsString()
    {
        // Arrange
        var expected =
        """
        -- name: GetUsers
        SELECT* FROM users;
        -- name: GetProducts
        SELECT* FROM products;
        -- name: GetRoles
        SELECT* FROM roles;
        """;
        expected += Environment.NewLine;
        var sqlCollection = new YeSqlDictionary();
        sqlCollection["GetUsers"] = "SELECT* FROM users;";
        sqlCollection["GetProducts"] = "SELECT* FROM products;";
        sqlCollection["GetRoles"] = "SELECT* FROM roles;";

        // Act
        string actual = sqlCollection.ToString();

        // Assert
        actual.Should().Be(expected);
    }

    [Test]
    public void ToString_WhenThereAreNoSqlStatements_ShouldReturnsEmptyString()
    {
        // Arrange
        var sqlCollection = new YeSqlDictionary();

        // Act
        string actual = sqlCollection.ToString();

        // Assert
        actual.Should().BeEmpty();
    }

    [Test]
    public void GetEnumerator_WhenThereAreSqlStatements_ShouldAllowIteratingOverThem()
    {
        // Arrange
        var expected = new List<ModelTag>
        {
            new() { Name = "GetUsers", SqlStatement = "SELECT* FROM users;" },
            new() { Name = "GetProducts", SqlStatement = "SELECT* FROM products;" },
            new() { Name = "GetRoles", SqlStatement = "SELECT* FROM roles;" }
        };
        var sqlCollection = new YeSqlDictionary();
        sqlCollection["GetUsers"] = "SELECT* FROM users;";
        sqlCollection["GetProducts"] = "SELECT* FROM products;";
        sqlCollection["GetRoles"] = "SELECT* FROM roles;";

        // Act
        IEnumerable<ModelTag> actual = sqlCollection.AsEnumerable();

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }
}
