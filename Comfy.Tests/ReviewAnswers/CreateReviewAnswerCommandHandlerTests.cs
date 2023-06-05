using Comfy.Application.Common.Exceptions;
using Comfy.Application.Handlers.Reviews.ReviewAnswers;
using Comfy.Application.Interfaces;
using Comfy.Domain.Models;
using MockQueryable.Moq;
using Moq;

namespace Comfy.Tests.ReviewAnswers;

public sealed class CreateReviewAnswerCommandHandlerTests
{
    private readonly Mock<IApplicationDbContext> _contextMock;
    public CreateReviewAnswerCommandHandlerTests()
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

        var command = new CreateReviewAnswerCommand { ReviewId = 47 };
        var handler = new CreateReviewAnswerCommandHandler(_contextMock.Object);

        // Act
        // Assert
        await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, default));
    }
}