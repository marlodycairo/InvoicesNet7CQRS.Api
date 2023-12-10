using InvoicesNet7CQRS.Api.Models;
using InvoicesNet7CQRS.Api.Utils;
using InvoicesNet7CQRS.Domain.Commands.CustomerCommands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InvoicesNet7CQRS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly JwtManage _jwtManage;

        public AuthenticationsController(IMediator mediator, JwtManage jwtManage)
        {
            _mediator = mediator;
            _jwtManage = jwtManage;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromQuery] Login login)
        {
            if (login == null)
            {
                return BadRequest();
            }

            var token = "";
            if (await IsUserValid(login))
            {
                token = await _jwtManage.GenerateJsonWebToken(login);
            }

            return Ok(new { Message = "User Authenticated", Result = token });
        }

        private async Task<bool> IsUserValid(Login login)
        {
            var users = await _mediator.Send(new GetAllUsersQuery());

            if (users.Result!.Any(x => x.Username == login.Username && x.Pass == login.Pass))
            {
                return true;
            }

            return false;
        }
    }
}
