using FluentValidation;
using InvoicesNet7CQRS.Domain.Commands.CustomerCommands;

namespace InvoicesNet7CQRS.Domain.Validations
{
    public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
    {
        public GetUserByIdQueryValidator()
        {
            RuleFor(query => query.Id)
                .GreaterThan(0).WithMessage("Invalid user ID.");
        }
    }
}
