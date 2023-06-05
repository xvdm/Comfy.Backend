using Comfy.Application.Handlers.Reviews.Reviews;
using Comfy.Application.Handlers.Reviews.Reviews.Validators;
using FluentAssertions;

namespace Comfy.Tests.Reviews.Validation;

public class LikeReviewCommandValidationTests
{
    [Theory]
    [InlineData(1)]
    [InlineData(2222)]
    public async Task Handle_Should_ReturnTrue(int userId)
    {
        // Arrange
        var validator = new LikeReviewCommandValidator();
        var command = new LikeReviewCommand(userId, Guid.Empty);

        // Act
        var validationResult = await validator.ValidateAsync(command);

        // Assert
        validationResult.IsValid.Should().Be(true);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-2222)]
    public async Task Handle_Should_ReturnFalse(int userId)
    {
        // Arrange
        var validator = new LikeReviewCommandValidator();
        var command = new LikeReviewCommand(userId, Guid.Empty);

        // Act
        var validationResult = await validator.ValidateAsync(command);

        // Assert
        validationResult.IsValid.Should().Be(false);
    }
}