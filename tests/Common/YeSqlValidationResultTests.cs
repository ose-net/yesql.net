namespace YeSql.Net.Tests.Common;

public class YeSqlValidationResultTests
{
    [Test]
    public void HasError_WhenThereAreErrors_ShouldReturnTrue()
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
    public void HasError_WhenThereAreNoErrors_ShouldReturnFalse()
    {
        // Arrange
        var validationResult = new YeSqlValidationResult();

        // Act
        bool actual = validationResult.HasError();

        // Assert
        actual.Should().BeFalse();
    }

    [Test]
    public void ErrorMessages_WhenThereAreNoErrors_ShouldReturnEmptyString()
    {
        // Arrange
        var validationResult = new YeSqlValidationResult();

        // Act
        string actual = validationResult.ErrorMessages;

        // Assert
        actual.Should().BeEmpty();
    }

    [Test]
    public void ErrorMessages_WhenThereAreErrors_ShouldReturnSetOfErrors()
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

    [Test]
    public void GetEnumerator_WhenThereAreErrors_ShouldAllowIteratingOverThem()
    {
        // Arrange
        var validationResult = new YeSqlValidationResult
        {
            "Error1",
            "Error2",
            "Error3"
        };
        var expected = new[]
        {
            "Error1",
            "Error2",
            "Error3"
        };

        // Act
        IEnumerable<string> actual = validationResult.AsEnumerable();

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }
}
