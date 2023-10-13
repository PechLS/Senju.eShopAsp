using eShopAsp.Core.Entities.ContributorAggregate.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace eShopAsp.Core.Entities.ContributorAggregate.Handlers;

internal class ContributorDeletedHandler : INotificationHandler<ContributorDeletedEvent>
{
    private readonly ILogger<ContributorDeletedHandler> _logger;

    public ContributorDeletedHandler(ILogger<ContributorDeletedHandler> logger) => _logger = logger;
    
    public async Task Handle(ContributorDeletedEvent domainEvent, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Handling contributor deleted event for {domainEvent.ContributorId}");
        //todo: do meaningful work here.
        await Task.Delay(1);
    }
}