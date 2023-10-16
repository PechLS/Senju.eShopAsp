using eShopAsp.Core.Extensions;

namespace eShopAsp.Core.Entities.CatalogItemAggregate.Specifications;

public sealed class CatalogFilterSpecification : Specification<CatalogItem>
{
    public CatalogFilterSpecification(int? brandId, int? typeId)
    {
        Query.Where(i => (!brandId.HasValue || i.CatalogBrandId == brandId)
                         && (!typeId.HasValue || i.CatalogTypeId == typeId));
    }
}