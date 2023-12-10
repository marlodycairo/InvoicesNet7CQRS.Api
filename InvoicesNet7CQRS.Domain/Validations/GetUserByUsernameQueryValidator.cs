using FluentValidation;
using InvoicesNet7CQRS.Domain.Commands.UserCommands;

namespace InvoicesNet7CQRS.Domain.Validations
{
    public class GetUserByUsernameQueryValidator : AbstractValidator<GetUserByUsernameQuery>
    {
        public GetUserByUsernameQueryValidator()
        {
            RuleFor(query => query.UserName)
                .NotEmpty().WithMessage("Username is required.");
        }
    }
}
