using eShopAsp.UseCases.CatalogItems;

namespace eShopAsp.PublicApi.CatalogItemEndpoints.List;

public class ListPagedCatalogItemResponse : BaseResponse
{
    public ListPagedCatalogItemResponse(Guid correlationId) : base(correlationId) {}
    public ListPagedCatalogItemResponse() {}
    public int PageCount { get; set; }
    public List<CatalogItemDTO> CatalogItems { get; set; } = new();
}