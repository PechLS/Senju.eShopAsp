namespace eShopAsp.Core.Interfaces;


/// <summary>
/// Apply this marker interface only to aggregate root entities in our domain model
/// Our repository implementation can use constraints to ensure it only operates on aggregate roots.
/// </summary>
public interface IAggregateRoot
{
    
}