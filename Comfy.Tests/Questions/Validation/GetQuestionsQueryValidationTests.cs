using Comfy.Application.Handlers.Questions.Questions;
using Comfy.Application.Handlers.Questions.Questions.Validators;
using FluentAssertions;

namespace Comfy.Tests.Questions.Validation;

public sealed class GetQuestionsQueryValidationTests
{
    [Theory]
    [InlineData(1)]
    [InlineData(2222)]
    public async Task Handle_Should_ReturnTrue(int productId)
    {
        // Arrange
        var validator = new GetQuestionsQueryValidator();
        var query = new GetQuestionsQuery(productId, null, null);

        // Act
        var validationResult = await validator.ValidateAsync(query);

        // Assert
        validationResult.IsValid.Should().Be(true);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-2222)]
    public async Task Handle_Should_ReturnFalse(int productId)
    {
        // Arrange
        var validator = new GetQuestionsQueryValidator();
        var command = new GetQuestionsQuery(productId, null, null);

        // Act
        var validationResult = await validator.ValidateAsync(command);

        // Assert
        validationResult.IsValid.Should().Be(false);
    }
}