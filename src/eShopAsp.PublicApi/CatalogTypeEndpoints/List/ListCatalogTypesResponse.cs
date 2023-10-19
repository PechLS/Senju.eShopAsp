
using eShopAsp.UseCases.CatalogTypes;

namespace eShopAsp.PublicApi.CatalogTypeEndpoints.List;

public class ListCatalogTypesResponse : BaseResponse
{
    public ListCatalogTypesResponse(Guid correlationId) : base(correlationId){}
    public ListCatalogTypesResponse(){}
    public List<CatalogTypeDTO> CatalogTypes = new();
}