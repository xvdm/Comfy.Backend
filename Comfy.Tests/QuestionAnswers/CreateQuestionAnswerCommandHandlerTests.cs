using Comfy.Application.Common.Exceptions;
using Comfy.Application.Handlers.Questions.QuestionAnswers;
using Comfy.Application.Interfaces;
using Comfy.Domain.Entities;
using MockQueryable.Moq;
using Moq;

namespace Comfy.Tests.QuestionAnswers;

public sealed class CreateQuestionAnswerCommandHandlerTests
{
    private readonly Mock<IApplicationDbContext> _contextMock;
    public CreateQuestionAnswerCommandHandlerTests()
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

        var command = new CreateQuestionAnswerCommand { QuestionId = 47 };
        var handler = new CreateQuestionAnswerCommandHandler(_contextMock.Object);

        // Act
        // Assert
        await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, default));
    }
}