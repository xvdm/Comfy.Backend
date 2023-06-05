using Comfy.Application.Common.Exceptions;
using Comfy.Application.Handlers.Reviews.Reviews;
using Comfy.Application.Interfaces;
using Comfy.Domain.Models;
using MockQueryable.Moq;
using Moq;

namespace Comfy.Tests.Reviews;

public class DislikeReviewCommandHandlerTests
{
    private readonly Mock<IApplicationDbContext> _contextMock;
    public DislikeReviewCommandHandlerTests()
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

        var command = new DislikeReviewCommand(47, Guid.Empty);
        var handler = new DislikeReviewCommandHandler(_contextMock.Object);

        // Act
        // Assert
        await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, default));
    }

    [Fact]
    public async Task Handle_Should_IncreaseDislikesNumber()
    {
        // Arrange
        var dislikes = 5;
        var reviews = new List<Review>
        {
            new() { Id = 47, Dislikes = dislikes }
        };
        var reviewsMock = reviews.AsQueryable().BuildMockDbSet();
        _contextMock.Setup(x => x.Reviews).Returns(reviewsMock.Object);

        var command = new DislikeReviewCommand(47, Guid.Empty);
        var handler = new DislikeReviewCommandHandler(_contextMock.Object);

        // Act
        await handler.Handle(command, default);

        // Assert
        Assert.Equal(dislikes + 1, reviews.First().Dislikes);
    }
}