using FluentValidation;
using InvoicesNet7CQRS.Domain.Commands.CustomerCommands;

namespace InvoicesNet7CQRS.Domain.Validations
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(command => command._user)
            .NotNull().WithMessage("User cannot be null.");

            RuleFor(command => command._user.Id)
                .GreaterThan(0).WithMessage("Invalid user ID.");

            RuleFor(command => command._user.FirstName)
                .NotEmpty().WithMessage("First name is required.");

            RuleFor(command => command._user.LastName)
                .NotEmpty().WithMessage("Last name is required.");

            RuleFor(command => command._user.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(command => command._user.Username)
                .NotEmpty().WithMessage("Username is required.");

            RuleFor(command => command._user.Pass)
                .NotEmpty().WithMessage("Password is required.");
        }
    }
}
