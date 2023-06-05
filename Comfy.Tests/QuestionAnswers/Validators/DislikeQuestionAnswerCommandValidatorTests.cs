using Comfy.Application.Handlers.Questions.QuestionAnswers;
using Comfy.Application.Handlers.Questions.QuestionAnswers.Validators;
using FluentAssertions;

namespace Comfy.Tests.QuestionAnswers.Validators;

public sealed class DislikeQuestionAnswerCommandValidatorTests
{
    [Theory]
    [InlineData(1)]
    [InlineData(2222)]
    public async Task Handle_Should_ReturnTrue_WhenQuestionAnswerIdIsGreaterThanZero(int questionAnswerId)
    {
        // Arrange
        var validator = new DislikeQuestionAnswerCommandValidator();
        var command = new DislikeQuestionAnswerCommand(questionAnswerId, Guid.Empty);

        // Act
        var validationResult = await validator.ValidateAsync(command);

        // Assert
        validationResult.IsValid.Should().Be(true);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-2222)]
    public async Task Handle_Should_ReturnFalse_WhenQuestionAnswerIdIsNotGreaterThanZero(int questionAnswerId)
    {
        // Arrange
        var validator = new DislikeQuestionAnswerCommandValidator();
        var command = new DislikeQuestionAnswerCommand(questionAnswerId, Guid.Empty);

        // Act
        var validationResult = await validator.ValidateAsync(command);

        // Assert
        validationResult.IsValid.Should().Be(false);
    }
}