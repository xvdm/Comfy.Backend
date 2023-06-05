using Comfy.Application.Handlers.Reviews.ReviewAnswers;
using Comfy.Application.Handlers.Reviews.ReviewAnswers.Validators;
using FluentAssertions;

namespace Comfy.Tests.ReviewAnswers.Validators;

public sealed class LikeReviewAnswerCommandValidatorTests
{
    [Theory]
    [InlineData(1)]
    [InlineData(2222)]
    public async Task Handle_Should_ReturnTrue_WhenReviewAnswerIdIsGreaterThanZero(int reviewAnswerId)
    {
        // Arrange
        var validator = new LikeReviewAnswerCommandValidator();
        var command = new LikeReviewAnswerCommand(reviewAnswerId, Guid.Empty);

        // Act
        var validationResult = await validator.ValidateAsync(command);

        // Assert
        validationResult.IsValid.Should().Be(true);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-2222)]
    public async Task Handle_Should_ReturnFalse_WhenReviewAnswerIdIsNotGreaterThanZero(int reviewAnswerId)
    {
        // Arrange
        var validator = new LikeReviewAnswerCommandValidator();
        var command = new LikeReviewAnswerCommand(reviewAnswerId, Guid.Empty);

        // Act
        var validationResult = await validator.ValidateAsync(command);

        // Assert
        validationResult.IsValid.Should().Be(false);
    }
}