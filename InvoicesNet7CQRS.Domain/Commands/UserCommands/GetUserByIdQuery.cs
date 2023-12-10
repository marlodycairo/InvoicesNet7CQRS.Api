using InvoicesNet7CQRS.Data.Entities;
using InvoicesNet7CQRS.Domain.Responses;

namespace InvoicesNet7CQRS.Domain.Commands.CustomerCommands
{
    public class GetUserByIdQuery : BaseCommand<GenericResponse<User>>
    {
        public int Id { get; set; }

        public GetUserByIdQuery(int id)
        {
            Id = id;
        }
    }
}
