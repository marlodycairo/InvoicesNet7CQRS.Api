using InvoicesNet7CQRS.Data.Entities;
using InvoicesNet7CQRS.Domain.Responses;

namespace InvoicesNet7CQRS.Domain.Commands.CustomerCommands
{
    public class CreateUserCommand : BaseCommand<GenericResponse<User>>
    {
        public User User { get; set; }

        public CreateUserCommand(User user)
        {
            User = user;
        }
    }
}
