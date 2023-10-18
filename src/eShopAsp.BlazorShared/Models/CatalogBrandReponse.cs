using eShopAsp.BlazorShared.Interfaces;

namespace eShopAsp.BlazorShared.Models;

public class CatalogBrandResponse : ILookupDataResponse<CatalogBrand>
{
    public List<CatalogBrand> List { get; set; } = new();

}