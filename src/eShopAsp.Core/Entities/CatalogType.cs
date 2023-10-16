using eShopAsp.Core.Interfaces;

namespace eShopAsp.Core.Entities;

public class CatalogType : EntityBase, IAggregateRoot
{
    public string Type { get; private set; }
    public CatalogType(string type) => Type = type;
}