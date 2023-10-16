using eShopAsp.UseCases.CatalogBrands;
using eShopAsp.UseCases.CatalogBrands.List;
using Microsoft.EntityFrameworkCore;

namespace eShopAsp.Infrastructure.Data.Queries;

public class ListCatalogBrandsQueryService : IListCatalogBrandsQueryService
{
    private readonly CatalogContext _db;

    public ListCatalogBrandsQueryService(CatalogContext db) => _db = db;

    public async Task<IEnumerable<CatalogBrandDTO>> ListAsync()
    {
        var result = await _db.CatalogBrands.FromSqlRaw("SELECT Id, Brand FROM CATALOGBRANDS")
            .Select(c => new CatalogBrandDTO(c.Id, c.Brand))
            .ToListAsync();
        return result;
    }
}