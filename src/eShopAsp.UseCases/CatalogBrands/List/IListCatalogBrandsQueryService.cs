namespace eShopAsp.UseCases.CatalogBrands.List;

public interface IListCatalogBrandsQueryService
{
    Task<IEnumerable<CatalogBrandDTO>> ListAsync();
}