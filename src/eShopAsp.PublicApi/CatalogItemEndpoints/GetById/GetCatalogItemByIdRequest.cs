namespace eShopAsp.PublicApi.CatalogItemEndpoints.GetById;

public class GetCatalogItemByIdRequest : BaseRequest
{
    public int CatalogItemId { get; init; }
    public GetCatalogItemByIdRequest(int catalogItemId) => CatalogItemId = catalogItemId;
}