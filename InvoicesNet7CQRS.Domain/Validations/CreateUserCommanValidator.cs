using FluentValidation;
using InvoicesNet7CQRS.Domain.Commands.CustomerCommands;

namespace InvoicesNet7CQRS.Domain.Validations
{
    public class CreateUserCommanValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommanValidator()
        {
            RuleFor(x => x.User)
                .NotEmpty()
                .NotNull().WithMessage("User cannot be null.");

            RuleFor(command => command.User.FirstName)
                .NotNull()
                .NotEmpty().WithMessage("First name is required.");

            RuleFor(command => command.User.LastName)
                .NotNull()
                .NotEmpty().WithMessage("Last name is required.");

            RuleFor(command => command.User.Email)
                .NotNull()
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(command => command.User.Username)
                .NotNull()
                .NotEmpty().WithMessage("Username is required.");

            RuleFor(command => command.User.Pass)
                .NotNull()
                .NotEmpty().WithMessage("Password is required.");
        }
    }
}
