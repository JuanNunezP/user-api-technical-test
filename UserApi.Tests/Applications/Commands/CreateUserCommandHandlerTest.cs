using Moq;
using UserApi.Application.Commands.CreateUser;
using UserApi.Application.Interfaces.Repositories;
using FluentAssertions;
using Xunit;

public class CreateUserCommandHandlerTests
{
    [Fact]
    public async Task HandleAsync_Should_Call_Repository()
    {
        // Arrange
        var repoMock = new Mock<IUserWriteRepository>();

        repoMock
            .Setup(r => r.CreateAsync(It.IsAny<CreateUserCommand>()))
            .Returns(Task.CompletedTask);

        var handler = new CreateUserCommandHandler(repoMock.Object);

        var command = new CreateUserCommand(
            "Juan",
            "123456",
            "Calle 1",
            1
        );

        // Act
        await handler.HandleAsync(command);

        // Assert
        repoMock.Verify(r => r.CreateAsync(command), Times.Once);
    }
}
