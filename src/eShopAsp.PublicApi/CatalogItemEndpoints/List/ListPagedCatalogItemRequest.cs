namespace eShopAsp.PublicApi.CatalogItemEndpoints.List;

public class ListPagedCatalogItemRequest : BaseRequest
{
    public int PageSize { get; set; }
    public int PageIndex { get; set; }
    public int? CatalogBrandId { get; init; }
    public int? CatalogTypeId { get; init; }

    public ListPagedCatalogItemRequest(int? pageSize, int? pageIndex, int? catalogBrandId, int? catalogTypeId)
    {
        PageSize = pageSize ?? 0;
        PageIndex = pageIndex ?? 0;
        CatalogBrandId = catalogBrandId;
        CatalogTypeId = catalogTypeId;
    }
}