using eShopAsp.Core.Interfaces.Query;
using eShopAsp.Core.Result;

namespace eShopAsp.UseCases.CatalogItems.List;

public class ListCatalogItemsHandler : IQueryHandler<ListCatalogItemsQuery, Result<IEnumerable<CatalogItemDTO>>>
{
    public Task<Result<IEnumerable<CatalogItemDTO>>> Handle(ListCatalogItemsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}