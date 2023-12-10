using MediatR;

namespace InvoicesNet7CQRS.Domain.Commands
{
    public abstract class BaseCommand<TResult> : IRequest<TResult>
    {
    }
}
