using Comfy.Application.Handlers.Questions.Questions;
using Comfy.Application.Handlers.Questions.Questions.Validators;
using FluentAssertions;

namespace Comfy.Tests.Questions.Validation;

public class LikeQuestionCommandValidationTests
{
    [Theory]
    [InlineData(1)]
    [InlineData(2222)]
    public async Task Handle_Should_ReturnTrue(int userId)
    {
        // Arrange
        var validator = new LikeQuestionCommandValidator();
        var command = new LikeQuestionCommand(userId, Guid.Empty);

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
        var validator = new LikeQuestionCommandValidator();
        var command = new LikeQuestionCommand(userId, Guid.Empty);

        // Act
        var validationResult = await validator.ValidateAsync(command);

        // Assert
        validationResult.IsValid.Should().Be(false);
    }
}