using eShopAsp.UseCases.CatalogItems;

namespace eShopAsp.PublicApi.CatalogItemEndpoints.Update;

public class UpdateCatalogItemResponse : BaseResponse
{
    public UpdateCatalogItemResponse(Guid correlationId) : base(correlationId) {}
    public UpdateCatalogItemResponse() {}
    public CatalogItemDTO CatalogItem { get; set; }
}