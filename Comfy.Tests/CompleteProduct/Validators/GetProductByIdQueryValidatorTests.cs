using Comfy.Application.Handlers.Products.CompleteProduct;
using Comfy.Application.Handlers.Products.CompleteProduct.Validators;
using FluentAssertions;

namespace Comfy.Tests.CompleteProduct.Validators;

public sealed class GetProductByIdQueryValidatorTests
{
    [Theory]
    [InlineData(1)]
    [InlineData(2222)]
    public async Task Handle_Should_ReturnTrue_WhenProductIdIsGreaterThanZero(int productId)
    {
        // Arrange
        var validator = new GetProductByIdQueryValidator();
        var query = new GetProductByIdQuery(productId);

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
        var validator = new GetProductByIdQueryValidator();
        var command = new GetProductByIdQuery(productId);

        // Act
        var validationResult = await validator.ValidateAsync(command);

        // Assert
        validationResult.IsValid.Should().Be(false);
    }
}