using AutoMapper;
using Comfy.Application.Handlers.Products.CompleteProduct;
using Comfy.Application.Handlers.Products.CompleteProduct.DTO;
using Comfy.Application.Interfaces;
using Comfy.Domain.Entities;
using Comfy.Tests.Mock;
using FluentAssertions;
using MockQueryable.Moq;
using Moq;

namespace Comfy.Tests.CompleteProduct;

public sealed class GetProductByIdQueryHandlerTests
{
    private readonly Mock<IApplicationDbContext> _contextMock;
    private readonly IMapper _mapper;

    public GetProductByIdQueryHandlerTests()
    {
        _mapper = MapperMock.GetMapper();
        _contextMock = new Mock<IApplicationDbContext>();
    }

    [Fact]
    public async Task Handle_ShouldReturnProductDTO()
    {
        // Arrange
        var productId = 1;
        var products = new List<Product>
        {
            new() { Id = productId }
        };
        var productsMock = products.AsQueryable().BuildMockDbSet();
        _contextMock.Setup(x => x.Products).Returns(productsMock.Object);
        var query = new GetProductByIdQuery(productId);
        var handler = new GetProductByIdQueryHandler(_contextMock.Object, _mapper);

        // Act
        var result = await handler.Handle(query, default);

        // Assert
        result.Should().BeOfType<ProductDTO>();
    }
}