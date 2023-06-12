using Comfy.Application.Common.Exceptions;
using Comfy.Application.Handlers.Questions.Questions;
using Comfy.Application.Interfaces;
using Comfy.Domain.Entities;
using MockQueryable.Moq;
using Moq;

namespace Comfy.Tests.Questions;

public sealed class LikeQuestionCommandHandlerTests
{
    private readonly Mock<IApplicationDbContext> _contextMock;
    public LikeQuestionCommandHandlerTests()
    {
        _contextMock = new Mock<IApplicationDbContext>();
    }

    [Fact]
    public async Task Handle_Should_ThrowNotFoundException_WhenReviewIsNotFound()
    {
        // Arrange
        var questions = new List<Question>
        {
            new() { Id = 1 }
        };
        var questionsMock = questions.AsQueryable().BuildMockDbSet();
        _contextMock.Setup(x => x.Questions).Returns(questionsMock.Object);

        var command = new LikeQuestionCommand(47, Guid.Empty);
        var handler = new LikeQuestionCommandHandler(_contextMock.Object);

        // Act
        // Assert
        await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, default));
    }

    [Fact]
    public async Task Handle_Should_IncreaseLikesNumber()
    {
        // Arrange
        var likes = 5;
        var questions = new List<Question>
        {
            new() { Id = 47, Likes = likes }
        };
        var questionsMock = questions.AsQueryable().BuildMockDbSet();
        _contextMock.Setup(x => x.Questions).Returns(questionsMock.Object);

        var command = new LikeQuestionCommand(47, Guid.Empty);
        var handler = new LikeQuestionCommandHandler(_contextMock.Object);

        // Act
        await handler.Handle(command, default);

        // Assert
        Assert.Equal(likes + 1, questions.First().Likes);
    }
}