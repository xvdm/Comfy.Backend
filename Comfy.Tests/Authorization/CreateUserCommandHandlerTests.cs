using Comfy.Application.Common.Exceptions;
using Comfy.Application.Handlers.Authorization;
using Comfy.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace Comfy.Tests.Authorization;

public sealed class CreateUserCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldThrowUserWithGivenEmailAlreadyExistsException_WhenUserWithGivenEmailAlreadyExists()
    {
        // Arrange
        var user = new User
        {
            Email = "email@gmail.com"
        };
        var store = new Mock<IUserStore<User>>();
        var userManagerMock = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
        userManagerMock.Setup(um => um.FindByEmailAsync(user.Email)).ReturnsAsync(user);

        var command = new CreateUserCommand
        {
            Email = "email@gmail.com"
        };
        var handler = new CreateUserCommandHandler(userManagerMock.Object);

        // Act
        // Assert
        await Assert.ThrowsAsync<UserWithGivenEmailAlreadyExistsException>(async () => await handler.Handle(command, default));
    }

    [Fact]
    public async Task Handle_ShouldReturnGuid()
    {
        // Arrange
        var user = new User
        {
            Email = "email@gmail.com",
            UserName = "username"
        };
        var store = new Mock<IUserStore<User>>();
        var userManagerMock = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
        userManagerMock.Setup(um => um.FindByNameAsync(user.Name)).ReturnsAsync(user);
        userManagerMock.Setup(um => um.CreateAsync(It.IsAny<User>(), "password")).ReturnsAsync(IdentityResult.Success);


        var command = new CreateUserCommand
        {
            Email = "newEmail@gmail.com",
            Password = "password"
        };
        var handler = new CreateUserCommandHandler(userManagerMock.Object);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        Assert.True(Guid.TryParse(result.ToString(), out _));
    }
}