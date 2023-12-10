using MediatR;

namespace InvoicesNet7CQRS.Domain.Commands.CustomerCommands
{
    public class DeleteUserCommand : IRequest
    {
        public int Id { get; set; }

        public DeleteUserCommand(int id)
        {
            Id = id;
        }
    }
}
