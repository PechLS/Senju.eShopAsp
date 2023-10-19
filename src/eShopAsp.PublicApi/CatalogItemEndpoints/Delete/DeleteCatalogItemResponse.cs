namespace eShopAsp.PublicApi.CatalogItemEndpoints.Delete;

public class DeleteCatalogItemResponse : BaseResponse
{
    public DeleteCatalogItemResponse(Guid correlationId): base(correlationId){}
    public DeleteCatalogItemResponse(){}
    public string Status { get; set; } = "Deleted";
}