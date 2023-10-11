using MediatR;

namespace eShopAsp.Core.Interfaces.Query;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
    
}