using Comfy.Application.Handlers.Reviews.Reviews;
using Comfy.Application.Handlers.Reviews.Reviews.Validators;
using FluentAssertions;

namespace Comfy.Tests.Reviews.Validators;

public sealed class GetReviewsQueryValidatorTests
{
    [Theory]
    [InlineData(1)]
    [InlineData(2222)]
    public async Task Handle_Should_ReturnTrue_WhenProductIdIsGreaterThanZero(int productId)
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
    public async Task Handle_Should_ReturnFalse_WhenProductIdIsNotGreaterThanZero(int productId)
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