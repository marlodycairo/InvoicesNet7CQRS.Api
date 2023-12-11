using InvoicesNet7CQRS.Data.Entities;
using InvoicesNet7CQRS.Domain.Commands.CustomerCommands;
using InvoicesNet7CQRS.Domain.Commands.UserCommands;
using InvoicesNet7CQRS.Tests.Context;
using static InvoicesNet7CQRS.Services.CommandHandlers.CustomerCommandHandler.UserCommandHandler;

namespace InvoicesNet7CQRS.Tests
{
    public class UserHandlersTests
    {
        [Fact]
        public async Task CreateUserCommandHandler_ShouldCreateUserSuccessfully()
        {
            var options = TestDbContext.Create();

            using (var context = new InMemoryDbContext(options))
            {
                var handler = new CreateUserCommandHandler(context);

                var userToCreate = new User { Id = 3, FirstName = "Juan", LastName = "Arenas", Email = "jarenas@yahua.com", Username = "juan", Pass = "1234" };

                var createUserCommand = new CreateUserCommand(userToCreate);

                var response = await handler.Handle(createUserCommand, CancellationToken.None);

                Assert.NotNull(response);

                Assert.Equal(userToCreate, response.Result);

                Assert.Equal("Saved successfully!", response.Message);
            }
        }

        [Fact]
        public async Task GetAllUsersCommandHandler_ShouldReturnAllUsers()
        {
            var options = TestDbContext.Create();

            using (var context = new InMemoryDbContext(options))
            {
                var handler = new GetAllUsersCommandHander(context);

                var usersInDatabase = new List<User>
                {
                    new User { Id = 1, FirstName = "Ana", LastName = "Sanchez", Email = "asanchez@yahua.net", Username = "ana", Pass = "1234" },
                    new User { Id = 2, FirstName = "Dany", LastName = "Barrera", Email = "dbarrera@yahua.es", Username = "dany", Pass = "1234" },
                    new User { Id = 3, FirstName = "Juan", LastName = "Arenas", Email = "jarenas@yahua.com", Username = "juan", Pass = "1234" }
                };

                context.Users.AddRange(usersInDatabase);

                await context.SaveAsync();

                var response = await handler.Handle(new GetAllUsersQuery(), CancellationToken.None);

                Assert.NotNull(response);

                Assert.True(usersInDatabase.Count().Equals(response.Result!.Count()));
            }
        }

        [Fact]
        public async Task GetUserByIdCommandHandler_ShouldReturnUserById()
        {
            var options = TestDbContext.Create();

            using (var context = new InMemoryDbContext(options))
            {
                var userIdToRetrieve = 2;

                var userToRetrieve = new User { Id = userIdToRetrieve, FirstName = "Dany", LastName = "Barrera", Email = "dbarrera@yahua.es", Username = "dany", Pass = "1234" };

                context.Users.Add(userToRetrieve);

                await context.SaveAsync();

                var handler = new GetUserByIdCommandHandler(context);

                var getUserByIdQuery = new GetUserByIdQuery(userIdToRetrieve);

                var response = await handler.Handle(getUserByIdQuery, CancellationToken.None);

                Assert.NotNull(response);

                Assert.Equal(userToRetrieve, response.Result);
            }
        }

        [Fact]
        public async Task DeleteUserCommandHandler_ShouldDeleteUser()
        {
            var options = TestDbContext.Create();

            using (var context = new InMemoryDbContext(options))
            {
                var userIdToDelete = 1;

                var userToDelete = new User { Id = userIdToDelete, FirstName = "Ana", LastName = "Sanchez", Email = "asanchez@yahua.net", Username = "ana", Pass = "1234" };

                context.Users.Add(userToDelete);

                await context.SaveAsync();

                var handler = new DeleteUserCommandHandler(context);

                var deleteUserCommand = new DeleteUserCommand(userIdToDelete);

                await handler.Handle(deleteUserCommand, CancellationToken.None);

                Assert.Empty(context.Users);
            }
        }

        [Fact]
        public async Task UpdateUserCommandHandler_ShouldUpdateUser()
        {
            var options = TestDbContext.Create();

            using (var context = new InMemoryDbContext(options))
            {
                var userIdToUpdate = 3;

                var originalUser = new User { Id = userIdToUpdate, FirstName = "Juan", LastName = "Arenas", Email = "jarenas@yahua.com", Username = "juan", Pass = "1234" };

                context.Users.Add(originalUser);

                await context.SaveAsync();

                var updatedUser = new User { Id = userIdToUpdate, FirstName = "Juan Felipe", LastName = "Arenas De los Ríos", Email = "juanfe@live.com", Username = "juan", Pass = "1234" };

                var handler = new UpdateUserCommandHandler(context);

                var updateUserCommand = new UpdateUserCommand(updatedUser);

                var response = await handler.Handle(updateUserCommand, CancellationToken.None);

                Assert.NotNull(response);

                Assert.Equal(updatedUser, response.Result);

                Assert.Equal("User updated successfully.", response.Message);

                // Check if the user has been updated in the database
                var userInDatabase = await context.Users.FindAsync(userIdToUpdate);

                Assert.Equal(updatedUser, userInDatabase);
            }
        }

        [Fact]
        public async Task GetUserByUsernameCommandHandler_ShouldReturnUsersByUsername()
        {
            var options = TestDbContext.Create();

            using (var context = new InMemoryDbContext(options))
            {
                var usernameToRetrieve = "dany";

                var usersToRetrieve = new List<User>
                {
                    new User { Id = 1, FirstName = "Ana", LastName = "Sanchez", Email = "asanchez@yahua.net", Username = "ana", Pass = "1234" },
                    new User { Id = 2, FirstName = "Dany", LastName = "Barrera", Email = "dbarrera@yahua.es", Username = "dany", Pass = "1234" },
                    new User { Id = 3, FirstName = "Juan", LastName = "Arenas", Email = "jarenas@yahua.com", Username = "juan", Pass = "1234" }
                };

                context.Users.AddRange(usersToRetrieve);

                await context.SaveAsync();

                var handler = new GetUserByUsernameCommandHandler(context);

                var getUserByUsernameQuery = new GetUserByUsernameQuery(usernameToRetrieve);

                var response = await handler.Handle(getUserByUsernameQuery, CancellationToken.None);

                Assert.NotNull(response);

                Assert.Equal(usernameToRetrieve, response.Result!.FirstOrDefault()!.Username);
            }
        }
    }
}
