using MediatR;

namespace eShopAsp.Core;

/// <summary>
/// A base type for domain events. Depend on MediatR Notification.
/// Includes DateOccurred which is set on creation.
/// </summary>
public abstract class DomainEventBase : INotification
{
    public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;
}