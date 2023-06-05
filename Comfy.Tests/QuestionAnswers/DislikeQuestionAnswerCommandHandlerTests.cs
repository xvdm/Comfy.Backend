using Comfy.Application.Common.Exceptions;
using Comfy.Application.Handlers.Questions.QuestionAnswers;
using Comfy.Application.Interfaces;
using Comfy.Domain.Models;
using MockQueryable.Moq;
using Moq;

namespace Comfy.Tests.QuestionAnswers;

public sealed class DislikeQuestionAnswerCommandHandlerTests
{
    private readonly Mock<IApplicationDbContext> _contextMock;
    public DislikeQuestionAnswerCommandHandlerTests()
    {
        _contextMock = new Mock<IApplicationDbContext>();
    }

    [Fact]
    public async Task Handle_Should_ThrowNotFoundException_WhenQuestionAnswerIsNotFound()
    {
        // Arrange
        var questionAnswers = new List<QuestionAnswer>
        {
            new() { Id = 1 }
        };
        var questionAnswersMock = questionAnswers.AsQueryable().BuildMockDbSet();
        _contextMock.Setup(x => x.QuestionAnswers).Returns(questionAnswersMock.Object);

        var command = new DislikeQuestionAnswerCommand(47, Guid.Empty);
        var handler = new DislikeQuestionAnswerCommandHandler(_contextMock.Object);

        // Act
        // Assert
        await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, default));
    }

    [Fact]
    public async Task Handle_Should_DecreaseLikesNumber()
    {
        // Arrange
        var dislikes = 5;
        var questionAnswers = new List<QuestionAnswer>
        {
            new() { Id = 47, Dislikes = dislikes }
        };
        var questionAnswersMock = questionAnswers.AsQueryable().BuildMockDbSet();
        _contextMock.Setup(x => x.QuestionAnswers).Returns(questionAnswersMock.Object);

        var command = new DislikeQuestionAnswerCommand(47, Guid.Empty);
        var handler = new DislikeQuestionAnswerCommandHandler(_contextMock.Object);

        // Act
        await handler.Handle(command, default);

        // Assert
        Assert.Equal(dislikes + 1, questionAnswers.First().Dislikes);
    }
}