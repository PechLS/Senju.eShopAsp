namespace eShopAsp.UseCases.CatalogTypes.List;

public interface IListCatalogTypesQueryService
{
    Task<IEnumerable<CatalogTypeDTO>> ListAsync();
}