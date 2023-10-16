using eShopAsp.Core.Interfaces.Query;
using eShopAsp.Core.Result;

namespace eShopAsp.UseCases.CatalogBrands.List;

public record ListCatalogBrandsQuery(int? Skip, int? Take) : IQuery<Result<IEnumerable<CatalogBrandDTO>>>;