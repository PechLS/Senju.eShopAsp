using eShopAsp.Core.Interfaces.Query;
using eShopAsp.Core.Result;

namespace eShopAsp.UseCases.CatalogTypes.List;

public class ListCatalogItemsHandler : IQueryHandler<ListCatalogItemsQuery, Result<IEnumerable<CatalogTypeDTO>>>
{
    private readonly IListCatalogTypesQueryService _query;

    public ListCatalogItemsHandler(IListCatalogTypesQueryService query) => _query = query;

    public async Task<Result<IEnumerable<CatalogTypeDTO>>> Handle(ListCatalogItemsQuery request, CancellationToken cancellationToken)
    {
        var result = await _query.ListAsync();
        return Result.Success(result);
    }
}