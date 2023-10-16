using eShopAsp.Core.Interfaces;

namespace eShopAsp.Core.Entities;

public class CatalogBrand : EntityBase, IAggregateRoot
{
    public string Brand { get; private set; }
    public CatalogBrand(string brand) => Brand = brand;
}