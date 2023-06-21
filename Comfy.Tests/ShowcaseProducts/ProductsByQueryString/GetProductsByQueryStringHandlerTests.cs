using AutoMapper;
using Comfy.Application.Handlers.Products.ShowcaseProducts.ProductsByQueryString;
using Comfy.Application.Handlers.Products.ShowcaseProducts.ProductsByQueryString.DTO;
using Comfy.Application.Interfaces;
using Comfy.Domain.Entities;
using Comfy.Tests.Mock;
using FluentAssertions;
using MockQueryable.Moq;
using Moq;

namespace Comfy.Tests.ShowcaseProducts.ProductsByQueryString;

public sealed class GetProductsByQueryStringHandlerTests
{
    private readonly Mock<IApplicationDbContext> _contextMock;
    private readonly IMapper _mapper;

    public GetProductsByQueryStringHandlerTests()
    {
        _mapper = MapperMock.GetMapper();
        _contextMock = new Mock<IApplicationDbContext>();
    }

    [Fact]
    public async Task Handle_ShouldReturnProductsPageDTO()
    {
        // Arrange
        var categories = new List<Subcategory>
        {
            new() { Id = 1, UniqueCharacteristics = new HashSet<Characteristic>(), UniqueBrands = new HashSet<Brand>() }
        };
        var products = new List<Product>
        {
            new() { Id = 1, CategoryId = 1 }
        };
        var productsMock = products.AsQueryable().BuildMockDbSet();
        _contextMock.Setup(x => x.Products).Returns(productsMock.Object);

        var categoriesMock = categories.AsQueryable().BuildMockDbSet();
        _contextMock.Setup(x => x.Subcategories).Returns(categoriesMock.Object);

        var query = new GetProductsQuery(1, null, null, null, null, null, null);
        var handler = new GetProductsQueryHandler(_contextMock.Object, _mapper);

        // Act
        var result = await handler.Handle(query, default);

        // Assert
        result.Should().BeOfType<ProductsPageDTO>();
    }
}