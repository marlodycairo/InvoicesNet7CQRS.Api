using InvoicesNet7CQRS.Data.Entities;
using InvoicesNet7CQRS.Domain.Responses;

namespace InvoicesNet7CQRS.Domain.Commands.CustomerCommands
{
    public class UpdateUserCommand : BaseCommand<GenericResponse<User>>
    {
        public readonly User _user;

        public UpdateUserCommand(User user)
        {
            _user = user;
        }
    }
}
