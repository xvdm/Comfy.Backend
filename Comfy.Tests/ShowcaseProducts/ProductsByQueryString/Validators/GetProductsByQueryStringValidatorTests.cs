using Comfy.Application.Handlers.Products.ShowcaseProducts.ProductsByQueryString;
using Comfy.Application.Handlers.Products.ShowcaseProducts.ProductsByQueryString.Validators;
using FluentAssertions;

namespace Comfy.Tests.ShowcaseProducts.ProductsByQueryString.Validators;

public sealed class GetProductsByQueryStringValidatorTests
{
    [Theory]
    [InlineData(1)]
    [InlineData(2222)]
    public async Task Handle_Should_ReturnTrue_WhenSubcategoryIdIsGreaterThanZero(int subcategoryId)
    {
        // Arrange
        var validator = new GetProductsByQueryStringValidator();
        var query = new GetProductsByQueryString(subcategoryId, null, null, null);

        // Act
        var validationResult = await validator.ValidateAsync(query);

        // Assert
        validationResult.IsValid.Should().Be(true);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-2222)]
    public async Task Handle_Should_ReturnFalse_WhenSubcategoryIdIsNotGreaterThanZero(int subcategoryId)
    {
        // Arrange
        var validator = new GetProductsByQueryStringValidator();
        var query = new GetProductsByQueryString(subcategoryId, null, null, null);

        // Act
        var validationResult = await validator.ValidateAsync(query);

        // Assert
        validationResult.IsValid.Should().Be(false);
    }
}