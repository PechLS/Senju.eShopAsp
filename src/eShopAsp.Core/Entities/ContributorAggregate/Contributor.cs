using eShopAsp.Core.Interfaces;

namespace eShopAsp.Core.Entities.ContributorAggregate;

public class Contributor : EntityBase, IAggregateRoot
{
    public string Name { get; private set; }
    
}