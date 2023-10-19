using eShopAsp.UseCases.CatalogItems;

namespace eShopAsp.PublicApi.CatalogItemEndpoints.Create;

public class CreateCatalogItemResponse : BaseResponse
{
    public CreateCatalogItemResponse(Guid correlationId) : base(correlationId){}
    public CreateCatalogItemResponse(){}
    public CatalogItemDTO CatalogItem { get; set; }
}