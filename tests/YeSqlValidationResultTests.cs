namespace YeSql.Net.Tests;

public class YeSqlValidationResultTests
{
    [Test]
    public void HasError_WhenThereAreErrors_ShouldReturnsTrue()
    {
        // Arrange
        var validationResult = new YeSqlValidationResult
        {
            "Error!"
        };

        // Act
        bool actual = validationResult.HasError();

        // Assert
        actual.Should().BeTrue();
    }

    [Test]
    public void HasError_WhenThereAreNoErrors_ShouldReturnsFalse()
    {
        // Arrange
        var validationResult = new YeSqlValidationResult();

        // Act
        bool actual = validationResult.HasError();

        // Assert
        actual.Should().BeFalse();
    }

    [Test]
    public void ErrorMessages_WhenThereAreNoErrors_ShouldReturnsEmptyString()
    {
        // Arrange
        var validationResult = new YeSqlValidationResult();

        // Act
        string actual = validationResult.ErrorMessages;

        // Assert
        actual.Should().BeEmpty();
    }

    [Test]
    public void ErrorMessages_WhenThereAreErrors_ShouldReturnsSetOfErrors()
    {
        // Arrange
        var validationResult = new YeSqlValidationResult
        {
            "Error1",
            "Error2",
            "Error3"
        };
        var expectedMessages =
        """
        Error1
        Error2
        Error3
        """;

        // Act
        string actual = validationResult.ErrorMessages;

        // Assert
        actual.Should().Be(expectedMessages);
    }
}
