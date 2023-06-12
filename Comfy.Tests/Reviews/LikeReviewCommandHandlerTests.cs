using Comfy.Application.Common.Exceptions;
using Comfy.Application.Handlers.Reviews.Reviews;
using Comfy.Application.Interfaces;
using Comfy.Domain.Entities;
using MockQueryable.Moq;
using Moq;

namespace Comfy.Tests.Reviews;

public sealed class LikeReviewCommandHandlerTests
{
    private readonly Mock<IApplicationDbContext> _contextMock;
    public LikeReviewCommandHandlerTests()
    {
        _contextMock = new Mock<IApplicationDbContext>();
    }

    [Fact]
    public async Task Handle_Should_ThrowNotFoundException_WhenReviewIsNotFound()
    {
        // Arrange
        var reviews = new List<Review>
        {
            new() { Id = 1 }
        };
        var reviewsMock = reviews.AsQueryable().BuildMockDbSet();
        _contextMock.Setup(x => x.Reviews).Returns(reviewsMock.Object);

        var command = new LikeReviewCommand(47, Guid.Empty);
        var handler = new LikeReviewCommandHandler(_contextMock.Object);

        // Act
        // Assert
        await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, default));
    }

    [Fact]
    public async Task Handle_Should_IncreaseLikesNumber()
    {
        // Arrange
        var likes = 5;
        var reviews = new List<Review>
        {
            new() { Id = 47, Likes = likes }
        };
        var reviewsMock = reviews.AsQueryable().BuildMockDbSet();
        _contextMock.Setup(x => x.Reviews).Returns(reviewsMock.Object);

        var command = new LikeReviewCommand(47, Guid.Empty);
        var handler = new LikeReviewCommandHandler(_contextMock.Object);

        // Act
        await handler.Handle(command, default);

        // Assert
        Assert.Equal(likes + 1, reviews.First().Likes);
    }
}