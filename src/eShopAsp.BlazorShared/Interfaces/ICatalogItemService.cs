using eShopAsp.BlazorShared.Models;

namespace eShopAsp.BlazorShared.Interfaces;

public interface ICatalogItemService
{
    Task<CatalogItem> Create(CreateCatalogItemRequest catalogItem);
    Task<CatalogItem> Edit(CatalogItem catalogItem);
    Task<string> Delete(int id);
    Task<CatalogItem> GetById(int id );
    Task<List<CatalogItem>> ListPaged(int pageSize);
    Task<List<CatalogItem>> List();
}