using InvoicesNet7CQRS.Data.Entities;
using InvoicesNet7CQRS.Domain.Responses;

namespace InvoicesNet7CQRS.Domain.Commands.UserCommands
{
    public class GetUserByUsernameQuery : BaseCommand<GenericResponse<IEnumerable<User>>>
    {
        public string? UserName { get; set; }

        public GetUserByUsernameQuery(string username)
        {
            UserName = username;
        }
    }
}
