using eShopAsp.Core.Interfaces.Query;
using eShopAsp.Core.Result;

namespace eShopAsp.UseCases.CatalogItems.Get;

public record GetCatalogItemQuery(int CatalogItemId) : IQuery<Result<CatalogItemDTO>>;