using Comfy.Application.Handlers.Products.CompleteProduct.Validators;
using Comfy.Application.Handlers.Products.CompleteProduct;
using FluentAssertions;

namespace Comfy.Tests.CompleteProduct.Validation;

public sealed class GetProductByUrlQueryValidationTests
{
    [Theory]
    [InlineData("u")]
    [InlineData("url123")]
    public async Task Handle_Should_ReturnTrue_WhenUrlIsNotEmpty(string url)
    {
        // Arrange
        var validator = new GetProductByUrlQueryValidator();
        var query = new GetProductByUrlQuery(url);

        // Act
        var validationResult = await validator.ValidateAsync(query);

        // Assert
        validationResult.IsValid.Should().Be(true);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async Task Handle_Should_ReturnFalse_WhenUrlIsEmpty(string url)
    {
        // Arrange
        var validator = new GetProductByUrlQueryValidator();
        var command = new GetProductByUrlQuery(url);

        // Act
        var validationResult = await validator.ValidateAsync(command);

        // Assert
        validationResult.IsValid.Should().Be(false);
    }
}