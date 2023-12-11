using InvoicesNet7CQRS.Domain.Commands.CustomerCommands;
using InvoicesNet7CQRS.Domain.Commands.UserCommands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InvoicesNet7CQRS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult> GetUsers()
        {
            var response = await _mediator.Send(new GetAllUsersQuery());

            return response.Error is null ? Ok(response) : StatusCode(StatusCodes.Status500InternalServerError, response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetUserById(int id)
        {
            var response = await _mediator.Send(new GetUserByIdQuery(id));

            if (response is null)
            {
                return NotFound();
            }

            return response.Error is null ? Ok(response) : StatusCode(StatusCodes.Status500InternalServerError, response);
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser(CreateUserCommand user)
        {
            var response = await _mediator.Send(user);

            if (response is null)
            {
                return NotFound();
            }

            return response.Error is null ? Ok(response) : StatusCode(StatusCodes.Status500InternalServerError, response);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(UpdateUserCommand userCommand)
        {
            var response = await _mediator.Send(userCommand);

            if (response is null)
            {
                return NotFound();
            }

            return response.Error is null ? Ok(response) : StatusCode(StatusCodes.Status500InternalServerError, response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            await _mediator.Send(new DeleteUserCommand(id));

            return Ok();
        }

        [HttpGet("GetUserByUsername")]
        public async Task<ActionResult> GetUserByUsername(string username)
        {
            var response = await _mediator.Send(new GetUserByUsernameQuery(username!));

            if (response is null)
            {
                return NotFound();
            }

            return response.Error is null ? Ok(response) : StatusCode(StatusCodes.Status500InternalServerError, response);
        }
    }
}
