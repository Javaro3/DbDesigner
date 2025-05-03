using CQRS.CQRS.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CQRS.CQRS.Commands;

public abstract class CommandHandler<TRequest> : ICommandHandler<TRequest>
    where TRequest : IRequest
{
    protected readonly DbContext Context;

    protected CommandHandler(DbContext context)
    {
        Context = context;
    }

    public abstract Task Handle(TRequest command);
}