using Comfy.Application.Handlers.Questions.Questions;
using Comfy.Application.Handlers.Questions.Questions.Validators;
using Comfy.Application.Interfaces;
using FluentAssertions;
using Moq;

namespace Comfy.Tests.Questions.Validators;

public sealed class CreateQuestionCommandValidatorTests
{
    private readonly CreateQuestionCommand _baseCommand;
    private readonly Mock<IApplicationDbContext> _contextMock;
    public CreateQuestionCommandValidatorTests()
    {
        _baseCommand = new CreateQuestionCommand
        {
            ProductId = 47,
            Text = "text",
            UserId = Guid.NewGuid()
        };
        _contextMock = new Mock<IApplicationDbContext>();
    }

    [Theory]
    [InlineData("FC960927-17F0-41DA-998B-709DC33EEADD")]
    public async Task Handle_Should_ReturnTrue_WhenUserIdIsGuid(Guid userId)
    {
        // Arrange
        var validator = new CreateQuestionCommandValidator(_contextMock.Object);
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
        var validator = new CreateQuestionCommandValidator(_contextMock.Object);
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
        var validator = new CreateQuestionCommandValidator(_contextMock.Object);
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
        var validator = new CreateQuestionCommandValidator(_contextMock.Object);
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
        var validator = new CreateQuestionCommandValidator(_contextMock.Object);
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
        var validator = new CreateQuestionCommandValidator(_contextMock.Object);
        var command = _baseCommand with { Text = text };

        // Act
        var validationResult = await validator.ValidateAsync(command);

        // Assert
        validationResult.IsValid.Should().Be(false);
    }
}