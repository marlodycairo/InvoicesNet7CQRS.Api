using InvoicesNet7CQRS.Api.Models;
using InvoicesNet7CQRS.Domain.Commands.AuthenticationCommands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InvoicesNet7CQRS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthenticationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> GetToken([FromQuery]Credential credential)
        {
            if (credential == null)
            {
                return BadRequest();
            }

            var response = await _mediator.Send(new GetAuthenticationQuery(credential.Username!, credential.Pass!));

            return Ok(response);
        }
    }
}
