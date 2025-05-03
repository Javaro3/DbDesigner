using MediatR;

namespace CQRS.CQRS.Interfaces;

public interface ICommandHandler<TRequest> 
    where TRequest : IRequest
{
    Task Handle(TRequest command);
}