using AutoMapper;
using Comfy.Application.Handlers.Products.HomepageShowcase;
using Comfy.Application.Handlers.Products.HomepageShowcase.DTO;
using Comfy.Application.Interfaces;
using Comfy.Domain.Models;
using Comfy.Tests.Mock;
using FluentAssertions;
using MockQueryable.Moq;
using Moq;

namespace Comfy.Tests.HomepageShowcase;

public sealed class GetShowcaseGroupsQueryHandlerTests
{
    private readonly Mock<IApplicationDbContext> _contextMock;
    private readonly IMapper _mapper;

    public GetShowcaseGroupsQueryHandlerTests()
    {
        _mapper = MapperMock.GetMapper();
        _contextMock = new Mock<IApplicationDbContext>();
    }

    [Fact]
    public async Task Handle_ShouldReturnProductDTO()
    {
        // Arrange
        var showcaseGroup = new List<ShowcaseGroup>
        {
            new() { Id = 1 }
        };
        var showcaseGroupMock = showcaseGroup.AsQueryable().BuildMockDbSet();
        _contextMock.Setup(x => x.ShowcaseGroups).Returns(showcaseGroupMock.Object);
        var query = new GetShowcaseGroupsQuery();
        var handler = new GetShowcaseGroupsQueryHandler(_contextMock.Object, _mapper);

        // Act
        var result = await handler.Handle(query, default);

        // Assert
        result.ToList().Should().BeOfType<List<ShowcaseGroupDTO>>();
    }
}