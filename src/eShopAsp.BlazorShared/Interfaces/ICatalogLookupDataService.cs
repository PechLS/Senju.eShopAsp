using eShopAsp.BlazorShared.Models;

namespace eShopAsp.BlazorShared.Interfaces;

public interface ICatalogLookupDataService<TLookupData> where TLookupData : LookupData
{
    Task<List<TLookupData>> List();
}