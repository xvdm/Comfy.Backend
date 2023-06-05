using Comfy.Application.Handlers.Reviews.Reviews;
using Comfy.Application.Handlers.Reviews.Reviews.Validators;
using FluentAssertions;

namespace Comfy.Tests.Reviews.Validators;

public sealed class LikeReviewCommandValidatorTests
{
    [Theory]
    [InlineData(1)]
    [InlineData(2222)]
    public async Task Handle_Should_ReturnTrue_WhenReviewIdIsGreaterThanZero(int reviewId)
    {
        // Arrange
        var validator = new LikeReviewCommandValidator();
        var command = new LikeReviewCommand(reviewId, Guid.Empty);

        // Act
        var validationResult = await validator.ValidateAsync(command);

        // Assert
        validationResult.IsValid.Should().Be(true);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-2222)]
    public async Task Handle_Should_ReturnFalse_WhenReviewIdIsNotGreaterThanZero(int reviewId)
    {
        // Arrange
        var validator = new LikeReviewCommandValidator();
        var command = new LikeReviewCommand(reviewId, Guid.Empty);

        // Act
        var validationResult = await validator.ValidateAsync(command);

        // Assert
        validationResult.IsValid.Should().Be(false);
    }
}