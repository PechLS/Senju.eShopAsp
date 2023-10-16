using eShopAsp.Core.Interfaces.Query;
using eShopAsp.Core.Result;

namespace eShopAsp.UseCases.CatalogTypes.List;

public record ListCatalogItemsQuery(int? Skip, int? Take) : IQuery<Result<IEnumerable<CatalogTypeDTO>>>;