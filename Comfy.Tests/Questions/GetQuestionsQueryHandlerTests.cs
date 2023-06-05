using AutoMapper;
using Comfy.Application.Handlers.Questions.Questions;
using Comfy.Application.Interfaces;
using Comfy.Domain.Models;
using Comfy.Tests.Mock;
using FluentAssertions;
using MockQueryable.Moq;
using Moq;

namespace Comfy.Tests.Questions;

public sealed class GetQuestionsQueryHandlerTests
{
    private readonly Mock<IApplicationDbContext> _contextMock;
    private readonly IMapper _mapper;

    public GetQuestionsQueryHandlerTests()
    {
        _contextMock = new Mock<IApplicationDbContext>();
        _mapper = MapperMock.GetMapper();
    }

    [Fact]
    public async Task Handle_Should_HaveCountAsInList_WhenEveryQuestionIsActive()
    {
        // Arrange
        var productId = 1;
        var questions = new List<Question>
        {
            new() { Id = 1, ProductId = productId, IsActive = true },
            new() { Id = 2, ProductId = productId, IsActive = true }
        };
        var questionsMock = questions.AsQueryable().BuildMockDbSet();
        _contextMock.Setup(x => x.Questions).Returns(questionsMock.Object);

        var query = new GetQuestionsQuery(productId, null, null);
        var handler = new GetQuestionsQueryHandler(_contextMock.Object, _mapper);

        // Act
        var result = await handler.Handle(query, default);

        // Assert
        result.Questions.Should().HaveCount(questions.Count);
    }

    [Fact]
    public async Task Handle_Should_NotHaveCountAsInList_WhenEveryQuestionIsActive()
    {
        // Arrange
        var productId = 1;
        var questions = new List<Question>
        {
            new() { Id = 1, ProductId = productId, IsActive = false },
            new() { Id = 2, ProductId = productId, IsActive = false }
        };
        var questionsMock = questions.AsQueryable().BuildMockDbSet();
        _contextMock.Setup(x => x.Questions).Returns(questionsMock.Object);

        var query = new GetQuestionsQuery(productId, null, null);
        var handler = new GetQuestionsQueryHandler(_contextMock.Object, _mapper);

        // Act
        var result = await handler.Handle(query, default);

        // Assert
        result.Questions.Should().NotHaveCount(questions.Count);
    }
}