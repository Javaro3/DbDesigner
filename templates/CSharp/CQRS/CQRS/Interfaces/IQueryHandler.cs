using MediatR;

namespace CQRS.CQRS.Interfaces;

public interface IQueryHandler<TRequest, TResult> 
    where TRequest : IRequest<TResult>
{
    Task<TResult> Handle(TRequest query);
}