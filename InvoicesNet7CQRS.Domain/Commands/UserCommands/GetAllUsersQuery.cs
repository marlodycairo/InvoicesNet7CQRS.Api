using InvoicesNet7CQRS.Data.Entities;
using InvoicesNet7CQRS.Domain.Responses;
using MediatR;

namespace InvoicesNet7CQRS.Domain.Commands.CustomerCommands
{
    public class GetAllUsersQuery : BaseCommand<GenericResponse<IEnumerable<User>>>
    {
    }
}
