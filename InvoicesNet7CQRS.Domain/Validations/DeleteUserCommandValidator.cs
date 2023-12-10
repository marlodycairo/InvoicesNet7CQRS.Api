using FluentValidation;
using InvoicesNet7CQRS.Domain.Commands.CustomerCommands;

namespace InvoicesNet7CQRS.Domain.Validations
{
    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(command => command.Id)
                .GreaterThan(0).WithMessage("Invalid user ID.");
        }
    }
}
