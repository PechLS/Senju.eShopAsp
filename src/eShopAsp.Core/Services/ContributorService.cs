using eShopAsp.Core.Entities.ContributorAggregate;
using eShopAsp.Core.Entities.ContributorAggregate.Events;
using eShopAsp.Core.Interfaces;
using eShopAsp.Core.Interfaces.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace eShopAsp.Core.Services;

public class ContributorService : IContributorService
{
    private readonly IRepository<Contributor> _repository;
    private readonly IMediator _mediator;
    private readonly ILogger<ContributorService> _logger;

    public ContributorService(
        IRepository<Contributor> repository,
        IMediator mediator,
        ILogger<ContributorService> logger)
    {
        _repository = repository;
        _mediator = mediator;
        _logger = logger;
    }
    
    public async Task<Result.Result> DeleteContributor(int contributorId)
    {
        _logger.LogInformation($"Deleting Contributor {contributorId}", contributorId);
        var aggregateToDelete = await _repository.GetByIdAsync(contributorId);
        if (aggregateToDelete == null) return Result.Result.NotFound();
        await _repository.DeleteAsync(aggregateToDelete);
        var domainEvent = new ContributorDeletedEvent(contributorId);
        await _mediator.Publish(domainEvent);
        return Result.Result.Success();
    }
}