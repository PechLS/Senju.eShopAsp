using eShopAsp.Core.Interfaces.Query;
using eShopAsp.Core.Result;

namespace eShopAsp.UseCases.CatalogItems.List;

public record ListCatalogItemsQuery() : IQuery<Result<IEnumerable<CatalogItemDTO>>>;