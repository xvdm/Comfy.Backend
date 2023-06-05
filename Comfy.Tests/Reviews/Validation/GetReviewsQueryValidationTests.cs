using Comfy.Application.Handlers.Reviews.Reviews.Validators;
using Comfy.Application.Handlers.Reviews.Reviews;
using FluentAssertions;

namespace Comfy.Tests.Reviews.Validation;

public sealed class GetReviewsQueryValidationTests
{
    [Theory]
    [InlineData(1)]
    [InlineData(2222)]
    public async Task Handle_Should_ReturnTrue(int productId)
    {
        // Arrange
        var validator = new GetReviewsQueryValidator();
        var query = new GetReviewsQuery(productId, null, null);

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
        var validator = new GetReviewsQueryValidator();
        var command = new GetReviewsQuery(productId, null, null);

        // Act
        var validationResult = await validator.ValidateAsync(command);

        // Assert
        validationResult.IsValid.Should().Be(false);
    }
}