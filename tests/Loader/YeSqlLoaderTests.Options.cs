namespace YeSql.Net.Tests.Loader;

public partial class YeSqlLoaderTests
{
    [Test]
    public void Exclude_WhenSqlFilesIsNull_ShouldThrowArgumentNullException()
    {
        // Arrange
        var loader = new YeSqlLoader();

        // Act
        Action action = () => loader.Exclude(null);

        // Assert
        action.Should().Throw<ArgumentNullException>();
    }

    [TestCase("")]
    [TestCase(" ")]
    [TestCase("      ")]
    [TestCase("schema.sql", null)]
    [TestCase("schema.sql", "", "  ", null)]
    public void Exclude_WhenCollectionHasNullValueOrOnlyWhitespace_ShouldThrowArgumentException(params string[] sqlFiles)
    {
        // Arrange
        var loader = new YeSqlLoader();
        var expectedMessage = string.Format(ExceptionMessages.CollectionHasNullValueOrOnlyWhitespace, nameof(sqlFiles));

        // Act
        Action action = () => loader.Exclude(sqlFiles);

        // Assert
        action.Should()
              .Throw<ArgumentException>()
              .WithMessage(expectedMessage);
    }
}
