using eShopAsp.Core.Interfaces.Query;
using eShopAsp.Core.Result;

namespace eShopAsp.UseCases.CatalogBrands.List;

public class ListCatalogBrandsHandler : IQueryHandler<ListCatalogBrandsQuery, Result<IEnumerable<CatalogBrandDTO>>>
{
    private readonly IListCatalogBrandsQueryService _query;
    public ListCatalogBrandsHandler(IListCatalogBrandsQueryService query) => _query = query;
    
    public async Task<Result<IEnumerable<CatalogBrandDTO>>> Handle(ListCatalogBrandsQuery request, CancellationToken cancellationToken)
    {
        var result = await _query.ListAsync();
        return Result.Success(result);
    }
}