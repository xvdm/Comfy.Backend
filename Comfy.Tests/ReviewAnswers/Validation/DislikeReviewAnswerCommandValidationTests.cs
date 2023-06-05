using Comfy.Application.Handlers.Reviews.ReviewAnswers;
using Comfy.Application.Handlers.Reviews.ReviewAnswers.Validators;
using FluentAssertions;

namespace Comfy.Tests.ReviewAnswers.Validation;

public sealed class DislikeReviewAnswerCommandValidationTests
{
    [Theory]
    [InlineData(1)]
    [InlineData(2222)]
    public async Task Handle_Should_ReturnTrue(int userId)
    {
        // Arrange
        var validator = new DislikeReviewAnswerCommandValidator();
        var command = new DislikeReviewAnswerCommand(userId, Guid.Empty);

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
        var validator = new DislikeReviewAnswerCommandValidator();
        var command = new DislikeReviewAnswerCommand(userId, Guid.Empty);

        // Act
        var validationResult = await validator.ValidateAsync(command);

        // Assert
        validationResult.IsValid.Should().Be(false);
    }
}