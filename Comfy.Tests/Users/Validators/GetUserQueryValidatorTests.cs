using Comfy.Application.Handlers.Users;
using Comfy.Application.Handlers.Users.Validators;
using FluentAssertions;

namespace Comfy.Tests.Users.Validators;

public sealed class GetUserQueryValidatorTests
{
    [Fact]
    public async Task Handle_Should_ReturnTrue_WhenUserIdIsGuid()
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
    public async Task Handle_Should_ReturnFalse_WhenUserIdIsGuidEmpty()
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