using Comfy.Application.Common.Exceptions;
using Comfy.Application.Handlers.Questions.Questions;
using Comfy.Application.Interfaces;
using Comfy.Domain.Entities;
using MockQueryable.Moq;
using Moq;

namespace Comfy.Tests.Questions;

public sealed class CreateQuestionCommandHandlerTests
{
    private readonly Mock<IApplicationDbContext> _contextMock;
    public CreateQuestionCommandHandlerTests()
    {
        _contextMock = new Mock<IApplicationDbContext>();
    }

    [Fact]
    public async Task Handle_Should_ThrowNotFoundException_WhenProductIsNotFound()
    {
        // Arrange
        var products = new List<Product>
        {
            new() { Id = 1 }
        };
        var productsMock = products.AsQueryable().BuildMockDbSet();
        _contextMock.Setup(x => x.Products).Returns(productsMock.Object);

        var command = new CreateQuestionCommand { ProductId = 47 };
        var handler = new CreateQuestionCommandHandler(_contextMock.Object);

        // Act
        // Assert
        await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, default));
    }
}