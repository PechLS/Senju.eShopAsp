using MediatR;

namespace eShopAsp.Core.Interfaces.Query;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
{
    
}