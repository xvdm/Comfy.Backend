using Comfy.Application.Handlers.Questions.QuestionAnswers;
using Comfy.Application.Handlers.Questions.QuestionAnswers.Validators;
using FluentAssertions;

namespace Comfy.Tests.QuestionAnswers.Validation;

public class LikeQuestionAnswerCommandValidationTests
{
    [Theory]
    [InlineData(1)]
    [InlineData(2222)]
    public async Task Handle_Should_ReturnTrue(int userId)
    {
        // Arrange
        var validator = new LikeQuestionAnswerCommandValidator();
        var command = new LikeQuestionAnswerCommand(userId, Guid.Empty);

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
        var validator = new LikeQuestionAnswerCommandValidator();
        var command = new LikeQuestionAnswerCommand(userId, Guid.Empty);

        // Act
        var validationResult = await validator.ValidateAsync(command);

        // Assert
        validationResult.IsValid.Should().Be(false);
    }
}