using eShopAsp.UseCases.CatalogItems;

namespace eShopAsp.PublicApi.CatalogItemEndpoints.GetById;

public class GetCatalogItemByIdResponse : BaseResponse
{
    public GetCatalogItemByIdResponse(Guid correlationId) : base(correlationId) {}
    public GetCatalogItemByIdResponse() {}
    public CatalogItemDTO CatalogItem { get; set; } 
}