using eShopAsp.UseCases.CatalogBrands;

namespace eShopAsp.PublicApi.CatalogBrandEndpoints.List;

public class ListCatalogBrandsResponse : BaseResponse
{
    public ListCatalogBrandsResponse(Guid correlationId) : base(correlationId){}
    public ListCatalogBrandsResponse(){}
    public List<CatalogBrandDTO> CatalogBrands { get; set; } = new();
}