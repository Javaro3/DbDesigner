using CQRS.CQRS.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CQRS.CQRS.Queries;

public abstract class QueryHandler<TRequest, TResult> : IQueryHandler<TRequest, TResult>
    where TRequest : IRequest<TResult>
{
    protected readonly DbContext Context;

    protected QueryHandler(DbContext context)
    {
        Context = context;
    }

    public abstract Task<TResult> Handle(TRequest query);
}