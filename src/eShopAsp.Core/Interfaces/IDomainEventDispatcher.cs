using eShopAsp.Core.Entities;

namespace eShopAsp.Core.Interfaces;

/// <summary>
/// A simple interface for sending domain events. Can use MediatR or any other implementation.
/// </summary>
public interface IDomainEventDispatcher
{
    Task DispatchAndClearEvents(IEnumerable<EntityBase> entitiesWithEvents);
}