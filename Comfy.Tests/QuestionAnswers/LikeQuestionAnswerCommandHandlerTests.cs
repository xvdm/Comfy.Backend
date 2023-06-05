using Comfy.Application.Common.Exceptions;
using Comfy.Application.Handlers.Questions.QuestionAnswers;
using Comfy.Application.Interfaces;
using Comfy.Domain.Models;
using MockQueryable.Moq;
using Moq;

namespace Comfy.Tests.QuestionAnswers;

public sealed class LikeQuestionAnswerCommandHandlerTests
{
    private readonly Mock<IApplicationDbContext> _contextMock;
    public LikeQuestionAnswerCommandHandlerTests()
    {
        _contextMock = new Mock<IApplicationDbContext>();
    }

    [Fact]
    public async Task Handle_Should_ThrowNotFoundException_WhenReviewIsNotFound()
    {
        // Arrange
        var questionAnswers = new List<QuestionAnswer>
        {
            new() { Id = 1 }
        };
        var questionAnswersMock = questionAnswers.AsQueryable().BuildMockDbSet();
        _contextMock.Setup(x => x.QuestionAnswers).Returns(questionAnswersMock.Object);

        var command = new LikeQuestionAnswerCommand(47, Guid.Empty);
        var handler = new LikeQuestionAnswerCommandHandler(_contextMock.Object);

        // Act
        // Assert
        await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, default));
    }

    [Fact]
    public async Task Handle_Should_IncreaseLikesNumber()
    {
        // Arrange
        var likes = 5;
        var questionAnswers = new List<QuestionAnswer>
        {
            new() { Id = 47, Likes = likes }
        };
        var questionAnswersMock = questionAnswers.AsQueryable().BuildMockDbSet();
        _contextMock.Setup(x => x.QuestionAnswers).Returns(questionAnswersMock.Object);

        var command = new LikeQuestionAnswerCommand(47, Guid.Empty);
        var handler = new LikeQuestionAnswerCommandHandler(_contextMock.Object);

        // Act
        await handler.Handle(command, default);

        // Assert
        Assert.Equal(likes + 1, questionAnswers.First().Likes);
    }
}