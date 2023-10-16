using eShopAsp.Core.Extensions;

namespace eShopAsp.Core.Entities.CatalogItemAggregate.Specifications;

public sealed class CatalogFilterPaginatedSpecification : Specification<CatalogItem>
{
    public CatalogFilterPaginatedSpecification(int skip, int take, int? brandId, int? typeId) : base()
    {
        if (take == 0) take = int.MaxValue;
        Query
            .Where(i => (!brandId.HasValue || i.CatalogBrandId == brandId)
                        && (!typeId.HasValue || i.CatalogTypeId == typeId))
            .Skip(skip)
            .Take(take);
    }
}