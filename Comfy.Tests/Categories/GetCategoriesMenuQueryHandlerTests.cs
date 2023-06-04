using AutoMapper;
using Comfy.Application.Handlers.Categories;
using Comfy.Application.Interfaces;
using Comfy.Domain.Models;
using Comfy.Tests.Mock;
using FluentAssertions;
using MockQueryable.Moq;
using Moq;

namespace Comfy.Tests.Categories;

public sealed class GetCategoriesMenuQueryHandlerTests
{
    private readonly Mock<IApplicationDbContext> _contextMock;
    private readonly IMapper _mapper;
    public GetCategoriesMenuQueryHandlerTests()
    {
        _contextMock = new Mock<IApplicationDbContext>();
        _mapper = MapperMock.GetMapper();
    }

    [Fact]
    public async Task Handle_Should_ReturnCategoriesMenuDTO()
    {
        // Arrange
        var subcategoryFilters = new List<SubcategoryFilter>
        {
            new() { Id = 1, Name = "Filer-1", FilterQuery = "Query-1", SubcategoryId = 1 },
            new() { Id = 2, Name = "Filer-2", FilterQuery = "Query-2", SubcategoryId = 1 },
            new() { Id = 3, Name = "Filer-3", FilterQuery = "Query-3", SubcategoryId = 1 },
            new() { Id = 4, Name = "Filer-4", FilterQuery = "Query-4", SubcategoryId = 1 }
        };
        var subcategories = new List<Subcategory>
        {
            new () { Id = 1, Name = "Sub-1", MainCategoryId = 1, Filters = subcategoryFilters },
            new () { Id = 2, Name = "Sub-2", MainCategoryId = 1 },
            new () { Id = 3, Name = "Sub-3", MainCategoryId = 1 }
        };
        var mainCategories = new List<MainCategory>
        {
            new() { Id = 1, Name = "Main-1", Categories = subcategories },
            new() { Id = 2, Name = "Main-2" }
        };
        var mainCategoriesMock = mainCategories.AsQueryable().BuildMockDbSet();
        _contextMock.Setup(x => x.MainCategories).Returns(mainCategoriesMock.Object);
        
        var subcategoriesMock = subcategories.AsQueryable().BuildMockDbSet();
        _contextMock.Setup(x => x.Subcategories).Returns(subcategoriesMock.Object);

        var query = new GetCategoriesMenuQuery();
        var handler = new GetCategoriesMenuQueryHandler(_contextMock.Object, _mapper);

        // Act
        var result = await handler.Handle(query, default);

        // Assert
        var main = result.MainCategories.ToList();
        var sub = main.FirstOrDefault()?.Categories.ToList();
        var filters = sub?.FirstOrDefault()?.Filters;
        main.Should().HaveCount(mainCategories.Count);
        sub.Should().HaveCount(subcategories.Count);
        filters.Should().HaveCount(subcategoryFilters.Count);
    }
}