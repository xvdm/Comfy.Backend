using AutoMapper;
using Comfy.Application.Handlers.Products.DTO;
using Comfy.Application.Handlers.Products.ShowcaseProducts.ProductsByIds;
using Comfy.Application.Interfaces;
using Comfy.Domain.Entities;
using Comfy.Tests.Mock;
using FluentAssertions;
using MockQueryable.Moq;
using Moq;

namespace Comfy.Tests.ShowcaseProducts.ProductsByIds;

public sealed class GetProductsByIdsQueryHandlerTests
{
    private readonly Mock<IApplicationDbContext> _contextMock;
    private readonly IMapper _mapper;

    public GetProductsByIdsQueryHandlerTests()
    {
        _mapper = MapperMock.GetMapper();
        _contextMock = new Mock<IApplicationDbContext>();
    }

    [Fact]
    public async Task Handle_ShouldReturnShowcaseProductDTO()
    {
        // Arrange
        var products = new List<Product>
        {
            new() { Id = 1 }
        };
        var productsMock = products.AsQueryable().BuildMockDbSet();
        _contextMock.Setup(x => x.Products).Returns(productsMock.Object);
        var query = new GetProductsByIdsQuery(new[] { 1 });
        var handler = new GetProductsByIdsQueryHandler(_contextMock.Object, _mapper);

        // Act
        var result = await handler.Handle(query, default);

        // Assert
        result.ToList().Should().BeOfType<List<ShowcaseProductDTO>>();
    }
}