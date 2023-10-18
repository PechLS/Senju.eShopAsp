using eShopAsp.BlazorShared.Models;

namespace eShopAsp.BlazorShared.Interfaces;

public interface ILookupDataResponse<TLookupData> where TLookupData : LookupData
{
    List<TLookupData> List { get; set; }
}