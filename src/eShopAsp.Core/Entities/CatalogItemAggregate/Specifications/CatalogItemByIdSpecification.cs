using eShopAsp.Core.Extensions;

namespace eShopAsp.Core.Entities.CatalogItemAggregate.Specifications;

public sealed class CatalogItemByIdSpecification : Specification<CatalogItem>
{
    public CatalogItemByIdSpecification(int catalogItemId)
    {
        Query.Where(ct => ct.Id == catalogItemId);
    }
}