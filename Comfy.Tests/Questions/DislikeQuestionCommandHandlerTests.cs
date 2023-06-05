using Comfy.Application.Common.Exceptions;
using Comfy.Application.Handlers.Questions.Questions;
using Comfy.Application.Interfaces;
using Comfy.Domain.Models;
using MockQueryable.Moq;
using Moq;

namespace Comfy.Tests.Questions;

public sealed class DislikeQuestionCommandHandlerTests
{
    private readonly Mock<IApplicationDbContext> _contextMock;
    public DislikeQuestionCommandHandlerTests()
    {
        _contextMock = new Mock<IApplicationDbContext>();
    }

    [Fact]
    public async Task Handle_Should_ThrowNotFoundException_WhenQuestionIsNotFound()
    {
        // Arrange
        var questions = new List<Question>
        {
            new() { Id = 1 }
        };
        var questionsMock = questions.AsQueryable().BuildMockDbSet();
        _contextMock.Setup(x => x.Questions).Returns(questionsMock.Object);

        var command = new DislikeQuestionCommand(47, Guid.Empty);
        var handler = new DislikeQuestionCommandHandler(_contextMock.Object);

        // Act
        // Assert
        await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, default));
    }

    [Fact]
    public async Task Handle_Should_DecreaseLikesNumber()
    {
        // Arrange
        var dislikes = 5;
        var questions = new List<Question>
        {
            new() { Id = 47, Dislikes = dislikes }
        };
        var questionsMock = questions.AsQueryable().BuildMockDbSet();
        _contextMock.Setup(x => x.Questions).Returns(questionsMock.Object);

        var command = new DislikeQuestionCommand(47, Guid.Empty);
        var handler = new DislikeQuestionCommandHandler(_contextMock.Object);

        // Act
        await handler.Handle(command, default);

        // Assert
        Assert.Equal(dislikes + 1, questions.First().Dislikes);
    }
}