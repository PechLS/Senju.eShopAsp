using MediatR;

namespace eShopAsp.Core.Interfaces.Command;

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}