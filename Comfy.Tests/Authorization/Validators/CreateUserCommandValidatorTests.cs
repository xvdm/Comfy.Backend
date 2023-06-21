using Comfy.Application.Handlers.Authorization;
using Comfy.Application.Handlers.Authorization.Validators;
using Comfy.Application.Interfaces;
using Comfy.Domain.Identity;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace Comfy.Tests.Authorization.Validators;

public sealed class CreateUserCommandValidatorTests
{
    private readonly CreateUserCommand _baseCommand;
    public CreateUserCommandValidatorTests()
    {
        _baseCommand = new CreateUserCommand
        {
            Email = "email@gmail.com",
            Name = "name",
            Password = "password"
        };
    }

    [Theory]
    [InlineData("na")]
    [InlineData("name")]
    [InlineData("012345678901234567890123456789")]
    public async Task Handle_Should_ReturnTrue_WhenNameLengthIsInclusiveBetweenTwoAndThirty(string name)
    {
        // Arrange
        var store = new Mock<IUserStore<User>>();
        var userManagerMock = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
        var validator = new CreateUserCommandValidator(userManagerMock.Object);
        var command = _baseCommand with { Name = name };

        // Act
        var validationResult = await validator.ValidateAsync(command);

        // Assert
        validationResult.IsValid.Should().Be(true);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("a")]
    [InlineData("012345678901234567890123456789a")]
    public async Task Handle_Should_ReturnFalse_WhenNameLengthIsNotInclusiveBetweenTwoAndThirty(string name)
    {
        // Arrange
        var store = new Mock<IUserStore<User>>();
        var userManagerMock = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
        var validator = new CreateUserCommandValidator(userManagerMock.Object);
        var command = _baseCommand with { Name = name };

        // Act
        var validationResult = await validator.ValidateAsync(command);

        // Assert
        validationResult.IsValid.Should().Be(false);
    }

    [Theory]
    [InlineData("qwerty")]
    [InlineData("012345678901234567890123456789")]
    public async Task Handle_Should_ReturnTrue_WhenPasswordLengthIsNotInclusiveBetweenSixAndThirty(string password)
    {
        // Arrange
        var store = new Mock<IUserStore<User>>();
        var userManagerMock = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
        var validator = new CreateUserCommandValidator(userManagerMock.Object);
        var command = _baseCommand with { Password = password };

        // Act
        var validationResult = await validator.ValidateAsync(command);

        // Assert
        validationResult.IsValid.Should().Be(true);
    }

    [Theory]
    [InlineData("")]
    [InlineData("qwert")]
    [InlineData(null)]
    [InlineData("012345678901234567890123456789a")]
    public async Task Handle_Should_ReturnFalse_WhenPasswordLengthIsNotInclusiveBetweenSixAndThirty(string password)
    {
        // Arrange
        var store = new Mock<IUserStore<User>>();
        var userManagerMock = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
        var validator = new CreateUserCommandValidator(userManagerMock.Object);
        var command = _baseCommand with { Password = password };

        // Act
        var validationResult = await validator.ValidateAsync(command);

        // Assert
        validationResult.IsValid.Should().Be(false);
    }

    [Theory]
    [InlineData("mail@gmail.com")]
    [InlineData("mail@i.ua")]
    public async Task Handle_Should_ReturnTrue_WhenEmailIsValidEmailAddress(string email)
    {
        // Arrange
        var store = new Mock<IUserStore<User>>();
        var userManagerMock = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
        var validator = new CreateUserCommandValidator(userManagerMock.Object);
        var command = _baseCommand with { Email = email };

        // Act
        var validationResult = await validator.ValidateAsync(command);

        // Assert
        validationResult.IsValid.Should().Be(true);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("email")]
    [InlineData("email.com")]
    public async Task Handle_Should_ReturnFalse_WhenEmailIsNotValidEmailAddress(string email)
    {
        // Arrange
        var store = new Mock<IUserStore<User>>();
        var userManagerMock = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
        var validator = new CreateUserCommandValidator(userManagerMock.Object);
        var command = _baseCommand with { Email = email };

        // Act
        var validationResult = await validator.ValidateAsync(command);

        // Assert
        validationResult.IsValid.Should().Be(false);
    }
}