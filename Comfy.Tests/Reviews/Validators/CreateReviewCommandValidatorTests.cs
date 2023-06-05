using Comfy.Application.Handlers.Reviews.Reviews;
using Comfy.Application.Handlers.Reviews.Reviews.Validators;
using FluentAssertions;

namespace Comfy.Tests.Reviews.Validators;

public sealed class CreateReviewCommandValidatorTests
{
    private readonly CreateReviewCommand _baseCommand;
    public CreateReviewCommandValidatorTests()
    {
        _baseCommand = new CreateReviewCommand
        {
            Advantages = "advantages",
            Disadvantages = "disadvantages",
            ProductId = 47,
            ProductRating = 4,
            Text = "text",
            UserId = Guid.NewGuid()
        };
    }

    [Theory]
    [InlineData("FC960927-17F0-41DA-998B-709DC33EEADD")]
    public async Task Handle_Should_ReturnTrue_WhenUserIdIsGuid(Guid userId)
    {
        // Arrange
        var validator = new CreateReviewCommandValidator();
        var command = _baseCommand with { UserId = userId };

        // Act
        var validationResult = await validator.ValidateAsync(command);

        // Assert
        validationResult.IsValid.Should().Be(true);
    }

    [Theory]
    [InlineData("00000000-0000-0000-0000-000000000000")]
    public async Task Handle_Should_ReturnFalse_WhenUserIdIsGuidEmpty(Guid userId)
    {
        // Arrange
        var validator = new CreateReviewCommandValidator();
        var command = _baseCommand with { UserId = userId };

        // Act
        var validationResult = await validator.ValidateAsync(command);

        // Assert
        validationResult.IsValid.Should().Be(false);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2222)]
    public async Task Handle_Should_ReturnTrue_WhenProductIdIsGreaterThanZero(int productId)
    {
        // Arrange
        var validator = new CreateReviewCommandValidator();
        var command = _baseCommand with { ProductId = productId };

        // Act
        var validationResult = await validator.ValidateAsync(command);

        // Assert
        validationResult.IsValid.Should().Be(true);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-2222)]
    public async Task Handle_Should_ReturnFalse_WhenProductIdIsLessThanZero(int productId)
    {
        // Arrange
        var validator = new CreateReviewCommandValidator();
        var command = _baseCommand with { ProductId = productId };

        // Act
        var validationResult = await validator.ValidateAsync(command);

        // Assert
        validationResult.IsValid.Should().Be(false);
    }

    [Theory]
    [InlineData("t")]
    [InlineData("Text")]
    public async Task Handle_Should_ReturnTrue_WhenTextIsNotEmpty(string text)
    {
        // Arrange
        var validator = new CreateReviewCommandValidator();
        var command = _baseCommand with { Text = text };

        // Act
        var validationResult = await validator.ValidateAsync(command);

        // Assert
        validationResult.IsValid.Should().Be(true);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async Task Handle_Should_ReturnFalse_WhenTextIsEmpty(string text)
    {
        // Arrange
        var validator = new CreateReviewCommandValidator();
        var command = _baseCommand with { Text = text };

        // Act
        var validationResult = await validator.ValidateAsync(command);

        // Assert
        validationResult.IsValid.Should().Be(false);
    }

    [Theory]
    [InlineData("a")]
    [InlineData("Advantages")]
    public async Task Handle_Should_ReturnTrue_WhenAdvantagesIsNotEmpty(string advantages)
    {
        // Arrange
        var validator = new CreateReviewCommandValidator();
        var command = _baseCommand with { Advantages = advantages };

        // Act
        var validationResult = await validator.ValidateAsync(command);

        // Assert
        validationResult.IsValid.Should().Be(true);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async Task Handle_Should_ReturnFalse_WhenAdvantagesIsEmpty(string advantages)
    {
        // Arrange
        var validator = new CreateReviewCommandValidator();
        var command = _baseCommand with { Advantages = advantages };

        // Act
        var validationResult = await validator.ValidateAsync(command);

        // Assert
        validationResult.IsValid.Should().Be(false);
    }

    [Theory]
    [InlineData("d")]
    [InlineData("Disadvantages")]
    public async Task Handle_Should_ReturnTrue_WhenDisadvantagesIsNotEmpty(string disadvantages)
    {
        // Arrange
        var validator = new CreateReviewCommandValidator();
        var command = _baseCommand with { Disadvantages = disadvantages };

        // Act
        var validationResult = await validator.ValidateAsync(command);

        // Assert
        validationResult.IsValid.Should().Be(true);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async Task Handle_Should_ReturnFalse_WhenDisadvantagesIsEmpty(string disadvantages)
    {
        // Arrange
        var validator = new CreateReviewCommandValidator();
        var command = _baseCommand with { Disadvantages = disadvantages };

        // Act
        var validationResult = await validator.ValidateAsync(command);

        // Assert
        validationResult.IsValid.Should().Be(false);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public async Task Handle_Should_ReturnTrue_WhenProductRatingIsInclusiveBetweenOneAndFive(double productRating)
    {
        // Arrange
        var validator = new CreateReviewCommandValidator();
        var command = _baseCommand with { ProductRating = productRating };

        // Act
        var validationResult = await validator.ValidateAsync(command);

        // Assert
        validationResult.IsValid.Should().Be(true);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(0.9)]
    [InlineData(5.1)]
    [InlineData(6)]
    public async Task Handle_Should_ReturnFalse_WhenProductRatingIsNotInclusiveBetweenOneAndFive(double productRating)
    {
        // Arrange
        var validator = new CreateReviewCommandValidator();
        var command = _baseCommand with { ProductRating = productRating };

        // Act
        var validationResult = await validator.ValidateAsync(command);

        // Assert
        validationResult.IsValid.Should().Be(false);
    }
}