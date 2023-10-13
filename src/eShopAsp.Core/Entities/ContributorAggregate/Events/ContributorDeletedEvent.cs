namespace eShopAsp.Core.Entities.ContributorAggregate.Events;

internal class ContributorDeletedEvent : DomainEventBase
{
    public int ContributorId { get; set; }

    public ContributorDeletedEvent(int contributorId) => ContributorId = contributorId;
}