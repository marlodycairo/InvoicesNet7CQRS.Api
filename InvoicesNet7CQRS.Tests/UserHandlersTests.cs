using InvoicesNet7CQRS.Data.Entities;
using InvoicesNet7CQRS.Data.Interfaces;
using InvoicesNet7CQRS.Domain.Commands.CustomerCommands;
using InvoicesNet7CQRS.Domain.Commands.UserCommands;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Linq.Expressions;
using static InvoicesNet7CQRS.Services.CommandHandlers.CustomerCommandHandler.UserCommandHandler;

namespace InvoicesNet7CQRS.Tests
{
    public class UserHandlersTests
    {
        [Fact]
        public async Task CreateUserCommandHandler_ShouldCreateUser()
        {
            // Arrange
            var dbContextMock = new Mock<IDbContext>();
            var handler = new CreateUserCommandHandler(dbContextMock.Object);
            var user = new User { /* Initialize user properties */ };
            var command = new CreateUserCommand(user);

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(response.Result);
            Assert.Equal("Saved successfully!", response.Message);
            dbContextMock.Verify(db => db.Users.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Once);
            dbContextMock.Verify(db => db.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAllUsersCommandHandler_ShouldReturnAllUsers()
        {
            // Arrange
            var dbContextMock = new Mock<IDbContext>();
            var handler = new GetAllUsersCommandHander(dbContextMock.Object);
            var users = new List<User> { /* Create some user instances */ };
            dbContextMock.Setup(db => db.Users.AsNoTracking().ToListAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(users);

            // Act
            var response = await handler.Handle(new GetAllUsersQuery(), CancellationToken.None);

            // Assert
            Assert.Equal(users, response.Result);
        }

        [Fact]
        public async Task GetUserByIdCommandHandler_ShouldReturnUserById()
        {
            // Arrange
            var dbContextMock = new Mock<IDbContext>();
            var handler = new GetUserByIdCommandHandler(dbContextMock.Object);
            var userId = 1;
            var user = new User { Id = userId, /* Set other properties */ };
            dbContextMock.Setup(db => db.Users.FirstOrDefaultAsync(It.IsAny<Expression<Func<User, bool>>>(), CancellationToken.None))
                .ReturnsAsync(user);

            // Act
            var response = await handler.Handle(new GetUserByIdQuery(userId), CancellationToken.None);

            // Assert
            Assert.Equal(user, response.Result);
        }

        [Fact]
        public async Task DeleteUserCommandHandler_ShouldDeleteUser()
        {
            // Arrange
            var dbContextMock = new Mock<IDbContext>();
            var handler = new DeleteUserCommandHandler(dbContextMock.Object);
            var userId = 1;
            var user = new User { Id = userId, /* Set other properties */ };
            dbContextMock.Setup(db => db.Users.FindAsync(It.IsAny<object[]>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);

            // Act
            await handler.Handle(new DeleteUserCommand(userId), CancellationToken.None);

            // Assert
            dbContextMock.Verify(db => db.Users.Remove(It.IsAny<User>()), Times.Once);
            dbContextMock.Verify(db => db.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateUserCommandHandler_ShouldUpdateUser()
        {
            // Arrange
            var dbContextMock = new Mock<IDbContext>();
            var handler = new UpdateUserCommandHandler(dbContextMock.Object);
            var userId = 1;
            var existingUser = new User { Id = userId, /* Set other properties */ };
            var updatedUser = new User { Id = userId, /* Set other properties */ };
            dbContextMock.Setup(db => db.Users.FindAsync(It.IsAny<object[]>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingUser);

            // Act
            var response = await handler.Handle(new UpdateUserCommand(updatedUser), CancellationToken.None);

            // Assert
            Assert.Equal(updatedUser, response.Result);
            dbContextMock.Verify(db => db.Users.Update(It.IsAny<User>()), Times.Once);
            dbContextMock.Verify(db => db.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task GetUserByUsernameCommandHandler_ShouldReturnUsersByUsername()
        {
            // Arrange
            var dbContextMock = new Mock<IDbContext>();
            var handler = new GetUserByUsernameCommandHandler(dbContextMock.Object);
            var userName = "testuser";
            var users = new List<User> { /* Create some user instances with the specified username */ };
            dbContextMock.Setup(db => db.Users.AsNoTracking().Where(It.IsAny<Expression<Func<User, bool>>>()).ToListAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(users);

            // Act
            var response = await handler.Handle(new GetUserByUsernameQuery(userName), CancellationToken.None);

            // Assert
            Assert.Equal(users, response.Result!);
        }
    }
}
