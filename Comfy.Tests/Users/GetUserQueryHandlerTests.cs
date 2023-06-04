using AutoMapper;
using Comfy.Application.Handlers.Users;
using Comfy.Application.Handlers.Users.DTO;
using Comfy.Domain.Identity;
using Comfy.Tests.Mock;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace Comfy.Tests.Users;

public sealed class GetUserQueryHandlerTests
{
    private readonly IMapper _mapper;

    public GetUserQueryHandlerTests()
    {
        _mapper = MapperMock.GetMapper();
    }

    [Fact]
    public async Task Handle_Should_ReturnUserDTO()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var store = new Mock<IUserStore<User>>();
        store.Setup(x => x.FindByIdAsync(userId.ToString(), default)).ReturnsAsync(new User { Id = userId });
        var userManagerMock = UserManagerMock.TestUserManager(store.Object);

        var query = new GetUserQuery(userId);
        var handler = new GetUserQueryHandler(_mapper, userManagerMock);

        // Act
        var result = await handler.Handle(query, default);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<UserDTO>();
    }

    [Fact]
    public async Task Handle_Should_ReturnNull()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var store = new Mock<IUserStore<User>>();
        store.Setup(x => x.FindByIdAsync(userId.ToString(), default)).ReturnsAsync(new User { Id = userId });
        var userManagerMock = UserManagerMock.TestUserManager(store.Object);

        var query = new GetUserQuery(Guid.NewGuid());
        var handler = new GetUserQueryHandler(_mapper, userManagerMock);

        // Act
        var result = await handler.Handle(query, default);

        // Assert
        result.Should().BeNull();
    }
}