using eShopAsp.Core.Extensions;

namespace eShopAsp.Core.Entities.CatalogItemAggregate.Specifications;

public sealed class CatalogItemsSpecification : Specification<CatalogItem>
{
    public CatalogItemsSpecification(params int[] ids)
    {
        Query.Where(c => ids.Contains(c.Id));
    }
}