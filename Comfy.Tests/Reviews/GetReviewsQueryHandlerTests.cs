using AutoMapper;
using Comfy.Application.Handlers.Reviews.Reviews;
using Comfy.Application.Interfaces;
using Comfy.Domain.Entities;
using Comfy.Tests.Mock;
using FluentAssertions;
using MockQueryable.Moq;
using Moq;

namespace Comfy.Tests.Reviews;

public sealed class GetReviewsQueryHandlerTests
{
    private readonly Mock<IApplicationDbContext> _contextMock;
    private readonly IMapper _mapper;

    public GetReviewsQueryHandlerTests()
    {
        _contextMock = new Mock<IApplicationDbContext>();
        _mapper = MapperMock.GetMapper();
    }

    [Fact]
    public async Task Handle_Should_HaveCountAsInList_WhenEveryReviewIsActive()
    {
        // Arrange
        var productId = 1;
        var reviews = new List<Review>
        {
            new() { Id = 1, ProductId = productId, IsActive = true },
            new() { Id = 2, ProductId = productId, IsActive = true }
        };
        var reviewsMock = reviews.AsQueryable().BuildMockDbSet();
        _contextMock.Setup(x => x.Reviews).Returns(reviewsMock.Object);

        var query = new GetReviewsQuery(productId, null, null);
        var handler = new GetReviewsQueryHandler(_contextMock.Object, _mapper);

        // Act
        var result = await handler.Handle(query, default);

        // Assert
        result.Reviews.Should().HaveCount(reviews.Count);
    }

    [Fact]
    public async Task Handle_Should_NotHaveCountAsInList_WhenEveryReviewIsActive()
    {
        // Arrange
        var productId = 1;
        var reviews = new List<Review>
        {
            new() { Id = 1, ProductId = productId, IsActive = false },
            new() { Id = 2, ProductId = productId, IsActive = false }
        };
        var reviewsMock = reviews.AsQueryable().BuildMockDbSet();
        _contextMock.Setup(x => x.Reviews).Returns(reviewsMock.Object);

        var query = new GetReviewsQuery(productId, null, null);
        var handler = new GetReviewsQueryHandler(_contextMock.Object, _mapper);

        // Act
        var result = await handler.Handle(query, default);

        // Assert
        result.Reviews.Should().NotHaveCount(reviews.Count);
    }
}