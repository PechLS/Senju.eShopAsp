using System.Text.Json.Serialization;
using eShopAsp.BlazorShared.Interfaces;

namespace eShopAsp.BlazorShared.Models;

public class CatalogTypeResponse : ILookupDataResponse<CatalogType>
{
    [JsonPropertyName("CatalogTypes")]
    public List<CatalogType> List { get; set; } = new();
}
