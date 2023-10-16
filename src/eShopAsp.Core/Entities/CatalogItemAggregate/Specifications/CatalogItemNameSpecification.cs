using eShopAsp.Core.Extensions;

namespace eShopAsp.Core.Entities.CatalogItemAggregate.Specifications;

public sealed class CatalogItemNameSpecification : Specification<CatalogItem>
{
    public CatalogItemNameSpecification(string catalogItemName)
    {
        Query.Where(ci => ci.Name == catalogItemName);
    }
}