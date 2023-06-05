using Comfy.Application.Handlers.Users;
using Comfy.Application.Handlers.Users.Validators;
using FluentAssertions;

namespace Comfy.Tests.Users.Validation;

public class GetUserQueryValidationTests
{
    [Fact]
    public async Task Handle_Should_ReturnTrue()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var validator = new GetUserQueryValidator();
        var query = new GetUserQuery(userId);

        // Act
        var validationResult = await validator.ValidateAsync(query);

        // Assert
        validationResult.IsValid.Should().Be(true);
    }

    [Fact]
    public async Task Handle_Should_ReturnFalse()
    {
        // Arrange
        var userId = Guid.Empty;
        var validator = new GetUserQueryValidator();
        var query = new GetUserQuery(userId);

        // Act
        var validationResult = await validator.ValidateAsync(query);

        // Assert
        validationResult.IsValid.Should().Be(false);
    }
}