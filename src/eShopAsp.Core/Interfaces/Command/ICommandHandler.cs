using MediatR;

namespace eShopAsp.Core.Interfaces.Command;

public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse> 
    where TCommand : ICommand<TResponse>
{
    
}