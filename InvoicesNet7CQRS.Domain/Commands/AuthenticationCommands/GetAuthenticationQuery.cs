using InvoicesNet7CQRS.Data.Entities;
using InvoicesNet7CQRS.Domain.Responses;

namespace InvoicesNet7CQRS.Domain.Commands.AuthenticationCommands
{
    public class GetAuthenticationQuery : BaseCommand<GenericResponse<User>>
    {
        public string Username { get; set; }
        public string Pass { get; set; }

        public GetAuthenticationQuery(string username, string pass)
        {
            Username = username;
            Pass = pass;
        }
    }
}
