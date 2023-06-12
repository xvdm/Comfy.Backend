using AutoMapper;
using Comfy.Application.Handlers.Banners;
using Comfy.Application.Interfaces;
using Comfy.Domain.Entities;
using Comfy.Tests.Mock;
using FluentAssertions;
using MockQueryable.Moq;
using Moq;

namespace Comfy.Tests.Banners;

public sealed class GetBannersQueryHandlerTests
{
    private readonly Mock<IApplicationDbContext> _contextMock;
    private readonly IMapper _mapper;

    public GetBannersQueryHandlerTests()
    {
        _contextMock = new Mock<IApplicationDbContext>();
        _mapper = MapperMock.GetMapper();
    }

    [Fact]
    public async Task Handle_Should_ReturnBannersDTO()
    {
        // Arrange
        var banners = new List<Banner>
        {
            new() { Id = 1, ImageUrl = "imageUrl-1", Name = "name-1", PageUrl = "pageUrl-1" },
            new() { Id = 2, ImageUrl = "imageUrl-2", Name = "name-2", PageUrl = "pageUrl-2" },
            new() { Id = 3, ImageUrl = "imageUrl-3", Name = "name-3", PageUrl = "pageUrl-3" }
        };
        var mock = banners.AsQueryable().BuildMockDbSet();
        _contextMock.Setup(x => x.Banners).Returns(mock.Object);

        var query = new GetBannersQuery();
        var handler = new GetBannersQueryHandler(_contextMock.Object, _mapper);

        // Act
        var result = await handler.Handle(query, default);

        // Assert
        result.Should().HaveCount(banners.Count);
    }
}
