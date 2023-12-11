using InvoicesNet7CQRS.Data.Context;
using InvoicesNet7CQRS.Data.Entities;
using InvoicesNet7CQRS.Data.Interfaces;
using InvoicesNet7CQRS.Domain.Commands.CustomerCommands;
using InvoicesNet7CQRS.Services.CommandHandlers.CustomerCommandHandler;
using InvoicesNet7CQRS.Tests.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;
using static InvoicesNet7CQRS.Services.CommandHandlers.CustomerCommandHandler.UserCommandHandler;

namespace InvoicesNet7CQRS.Tests
{
    public class UserHandlersTests
    {
        [Fact]
        public async Task CreateUserCommandHandler_ShouldCreateUser()
        {
            var dbContextMock = new Mock<ITestDbContext>();
            var userDbSetMock = new Mock<DbSet<User>>();
            dbContextMock.Setup(db => db.Users).Returns(userDbSetMock.Object);

            var userCommandHandler = new UserCommandHandler.CreateUserCommandHandler(dbContextMock.Object);

            var user = new User
            {
                Id = 3,
                FirstName = "Alice",
                LastName = "Wonderland",
                Email = "alicew@live.com",
                Username = "alice",
                Pass = "1234"
            };

            var createUserCommand = new CreateUserCommand(user);

            var response = await userCommandHandler.Handle(createUserCommand, CancellationToken.None);

            Assert.NotNull(response);
            Assert.Equal(user, response.Result);
            Assert.Equal("Saved successfully!", response.Message);
            Assert.Null(response.Error);

            userDbSetMock.Verify(dbSet => dbSet.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Once);
            dbContextMock.Verify(db => db.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAllUsersCommandHandler_ShouldNotReturnAllUsers()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDatabaseTest")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                var handler = new GetAllUsersCommandHander(context);
                var query = new GetAllUsersQuery();

                var result = await handler.Handle(query, CancellationToken.None);

                Assert.Empty(result.Result!);
            }
        }

        [Fact]
        public async Task GetUserById_ShouldNotReturnUser()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDatabaseTest")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                var handler = new GetUserByIdCommandHandler(context);
                var userId = 2;
                var command = new GetUserByIdQuery(userId);

                var result = await handler.Handle(command, CancellationToken.None);

                Assert.Null(result.Result);
            }
        }

        [Fact]
        public async Task DeleteUserCommandHandler_ShouldDeleteUser()
        {
            var dbContextMock = new Mock<IDbContext>();
            var handler = new DeleteUserCommandHandler(dbContextMock.Object);
            var userId = 1;
            var user = new User { Id = userId };
            dbContextMock.Setup(db => db.Users.FindAsync(It.IsAny<object[]>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);

            await handler.Handle(new DeleteUserCommand(userId), CancellationToken.None);

            dbContextMock.Verify(db => db.Users.Remove(It.IsAny<User>()), Times.Once);
            dbContextMock.Verify(db => db.SaveAsync(), Times.Once);
        }
    }
}
