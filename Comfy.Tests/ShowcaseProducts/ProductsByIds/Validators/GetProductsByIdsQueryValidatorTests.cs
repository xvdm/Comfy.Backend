using Comfy.Application.Handlers.Products.ShowcaseProducts.ProductsByIds;
using Comfy.Application.Handlers.Products.ShowcaseProducts.ProductsByIds.Validators;
using FluentAssertions;

namespace Comfy.Tests.ShowcaseProducts.ProductsByIds.Validators;

public sealed class GetProductsByIdsQueryValidatorTests
{
    [Theory]
    [InlineData(new[] { 1, 2 })]
    public async Task Handle_Should_ReturnTrue_WhenIdsIsNotEmpty(int[] ids)
    {
        // Arrange
        var validator = new GetProductsByIdsQueryValidator();
        var query = new GetProductsByIdsQuery(ids);

        // Act
        var validationResult = await validator.ValidateAsync(query);

        // Assert
        validationResult.IsValid.Should().Be(true);
    }

    [Theory]
    [InlineData(null)]
    [InlineData(new int[]{})]
    public async Task Handle_Should_ReturnFalse_WhenIdsIsEmpty(int[] ids)
    {
        // Arrange
        var validator = new GetProductsByIdsQueryValidator();
        var query = new GetProductsByIdsQuery(ids);

        // Act
        var validationResult = await validator.ValidateAsync(query);

        // Assert
        validationResult.IsValid.Should().Be(false);
    }
}