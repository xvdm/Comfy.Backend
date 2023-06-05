using AutoMapper;
using Comfy.Application.Handlers.Products.CompleteProduct;
using Comfy.Application.Handlers.Products.CompleteProduct.DTO;
using Comfy.Application.Interfaces;
using Comfy.Domain.Models;
using Comfy.Tests.Mock;
using FluentAssertions;
using MockQueryable.Moq;
using Moq;

namespace Comfy.Tests.CompleteProduct;

public sealed class GetProductByUrlQueryHandlerTests
{
    private readonly Mock<IApplicationDbContext> _contextMock;
    private readonly IMapper _mapper;

    public GetProductByUrlQueryHandlerTests()
    {
        _mapper = MapperMock.GetMapper();
        _contextMock = new Mock<IApplicationDbContext>();
    }

    [Fact]
    public async Task Handle_ShouldReturnProductDTO()
    {
        // Arrange
        var url = "url";
        var products = new List<Product>
        {
            new() { Url = url }
        };
        var productsMock = products.AsQueryable().BuildMockDbSet();
        _contextMock.Setup(x => x.Products).Returns(productsMock.Object);
        var query = new GetProductByUrlQuery(url);
        var handler = new GetProductByUrlQueryHandler(_contextMock.Object, _mapper);

        // Act
        var result = await handler.Handle(query, default);

        // Assert
        result.Should().BeOfType<ProductDTO>();
    }
}