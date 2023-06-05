using Comfy.Application.Common.Exceptions;
using Comfy.Application.Handlers.Reviews.ReviewAnswers;
using Comfy.Application.Interfaces;
using Comfy.Domain.Models;
using MockQueryable.Moq;
using Moq;

namespace Comfy.Tests.ReviewAnswers;

public sealed class DislikeReviewAnswerCommandHandlerTests
{
    private readonly Mock<IApplicationDbContext> _contextMock;
    public DislikeReviewAnswerCommandHandlerTests()
    {
        _contextMock = new Mock<IApplicationDbContext>();
    }

    [Fact]
    public async Task Handle_Should_ThrowNotFoundException_WhenReviewAnswerIsNotFound()
    {
        // Arrange
        var reviewAnswers = new List<ReviewAnswer>
        {
            new() { Id = 1 }
        };
        var reviewAnswersMock = reviewAnswers.AsQueryable().BuildMockDbSet();
        _contextMock.Setup(x => x.ReviewAnswers).Returns(reviewAnswersMock.Object);

        var command = new DislikeReviewAnswerCommand(47, Guid.Empty);
        var handler = new DislikeReviewAnswerCommandHandler(_contextMock.Object);

        // Act
        // Assert
        await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, default));
    }

    [Fact]
    public async Task Handle_Should_IncreaseDislikesNumber()
    {
        // Arrange
        var dislikes = 5;
        var reviewAnswers = new List<ReviewAnswer>
        {
            new() { Id = 47, Dislikes = dislikes }
        };
        var reviewAnswersMock = reviewAnswers.AsQueryable().BuildMockDbSet();
        _contextMock.Setup(x => x.ReviewAnswers).Returns(reviewAnswersMock.Object);

        var command = new DislikeReviewAnswerCommand(47, Guid.Empty);
        var handler = new DislikeReviewAnswerCommandHandler(_contextMock.Object);

        // Act
        await handler.Handle(command, default);

        // Assert
        Assert.Equal(dislikes + 1, reviewAnswers.First().Dislikes);
    }
}