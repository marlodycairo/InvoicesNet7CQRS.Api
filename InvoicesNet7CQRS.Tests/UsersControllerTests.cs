using InvoicesNet7CQRS.Api.Controllers;
using InvoicesNet7CQRS.Data.Entities;
using InvoicesNet7CQRS.Domain.Commands.CustomerCommands;
using InvoicesNet7CQRS.Domain.Commands.UserCommands;
using InvoicesNet7CQRS.Domain.Responses;
using InvoicesNet7CQRS.Tests.Context;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace InvoicesNet7CQRS.Tests
{
    public class UsersControllerTests
    {
        [Fact]
        public async Task GetUsers_ShouldReturnOkResult()
        {
            var dbContext = new TestDbContext();

            var mediatorMock = new Mock<IMediator>();

            mediatorMock.Setup(mediator => mediator.Send(It.IsAny<GetAllUsersQuery>(), CancellationToken.None))
                .ReturnsAsync(new GenericResponse<IEnumerable<User>> { Result = new List<User>() });

            var controller = new UsersController(mediatorMock.Object);

            var result = await controller.GetUsers();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetUserById_WithValidId_ShouldReturnOkResult()
        {
            var dbContext = new TestDbContext();

            var mediatorMock = new Mock<IMediator>();

            mediatorMock.Setup(mediator => mediator.Send(It.IsAny<GetUserByIdQuery>(), CancellationToken.None))
                .ReturnsAsync(new GenericResponse<User> { Result = new User() });

            var controller = new UsersController(mediatorMock.Object);

            var result = await controller.GetUserById(1);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task CreateUser_ShouldReturnOkResult()
        {
            var dbContext = new TestDbContext();

            var mediatorMock = new Mock<IMediator>();

            mediatorMock.Setup(mediator => mediator.Send(It.IsAny<CreateUserCommand>(), CancellationToken.None))
                .ReturnsAsync(new GenericResponse<User> { Result = new User() });

            var controller = new UsersController(mediatorMock.Object);

            var result = await controller.CreateUser(new CreateUserCommand(new User()));

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UpdateUser_ShouldReturnOkResult()
        {
            var dbContext = new TestDbContext();

            var mediatorMock = new Mock<IMediator>();

            mediatorMock.Setup(mediator => mediator.Send(It.IsAny<UpdateUserCommand>(), CancellationToken.None))
                .ReturnsAsync(new GenericResponse<User> { Result = new User() });

            var controller = new UsersController(mediatorMock.Object);

            var result = await controller.UpdateUser(new UpdateUserCommand(new User()));

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteUser_ShouldReturnOkResult()
        {
            var dbContext = new TestDbContext();

            var mediatorMock = new Mock<IMediator>();

            mediatorMock.Setup(mediator => mediator.Send(It.IsAny<DeleteUserCommand>(), CancellationToken.None))
                .Returns(Task.CompletedTask);

            var controller = new UsersController(mediatorMock.Object);

            var result = await controller.DeleteUser(1);

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task GetUserByUsername_WithValidId_ShouldReturnOkResult()
        {
            var dbContext = new TestDbContext();

            var mediatorMock = new Mock<IMediator>();

            mediatorMock.Setup(mediator => mediator.Send(It.IsAny<GetUserByUsernameQuery>(), CancellationToken.None))
                .ReturnsAsync(new GenericResponse<IEnumerable<User>> { Result = new List<User>() });

            var controller = new UsersController(mediatorMock.Object);

            var result = await controller.GetUserByUsername("laura");

            Assert.IsType<OkObjectResult>(result);
        }
    }
}