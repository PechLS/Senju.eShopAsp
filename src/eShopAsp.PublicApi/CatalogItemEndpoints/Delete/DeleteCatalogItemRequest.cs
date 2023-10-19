namespace eShopAsp.PublicApi.CatalogItemEndpoints.Delete;

public class DeleteCatalogItemRequest : BaseRequest
{
    public int CatalogItemId { get; set; }
    public DeleteCatalogItemRequest(int catalogItemId) => CatalogItemId = catalogItemId;
}