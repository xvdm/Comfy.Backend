using Comfy.Application.Handlers.Questions.Questions;
using Comfy.Application.Handlers.Questions.Questions.Validators;
using FluentAssertions;

namespace Comfy.Tests.Questions.Validators;

public sealed class DislikeQuestionCommandValidatorTests
{
    [Theory]
    [InlineData(1)]
    [InlineData(2222)]
    public async Task Handle_Should_ReturnTrue_WhenQuestionIdIsGreaterThanZero(int questionId)
    {
        // Arrange
        var validator = new DislikeQuestionCommandValidator();
        var command = new DislikeQuestionCommand(questionId, Guid.Empty);

        // Act
        var validationResult = await validator.ValidateAsync(command);

        // Assert
        validationResult.IsValid.Should().Be(true);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-2222)]
    public async Task Handle_Should_ReturnFalse_WhenQuestionIdIsNotGreaterThanZero(int questionId)
    {
        // Arrange
        var validator = new DislikeQuestionCommandValidator();
        var command = new DislikeQuestionCommand(questionId, Guid.Empty);

        // Act
        var validationResult = await validator.ValidateAsync(command);

        // Assert
        validationResult.IsValid.Should().Be(false);
    }
}