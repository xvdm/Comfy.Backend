using Comfy.Application.Common.Exceptions;
using Comfy.Application.Handlers.Reviews.Reviews;
using Comfy.Application.Interfaces;
using Comfy.Domain.Models;
using MockQueryable.Moq;
using Moq;

namespace Comfy.Tests.Reviews;

public sealed class CreateReviewCommandHandlerTests
{
    private readonly Mock<IApplicationDbContext> _contextMock;
    public CreateReviewCommandHandlerTests()
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

        var command = new CreateReviewCommand();
        var handler = new CreateReviewCommandHandler(_contextMock.Object);

        // Act
        // Assert
        await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(command, default));
    }
}