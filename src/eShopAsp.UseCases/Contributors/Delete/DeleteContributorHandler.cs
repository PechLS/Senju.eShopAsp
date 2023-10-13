using eShopAsp.Core.Interfaces.Command;
using eShopAsp.Core.Interfaces.Services;
using eShopAsp.Core.Result;

namespace eShopAsp.UseCases.Contributors.Delete;

public class DeleteContributorHandler : ICommandHandler<DeleteContributorCommand, Result>
{
    private readonly IContributorService _contributorService;
    
    public DeleteContributorHandler(IContributorService contributorService) => _contributorService = contributorService;
    
    public async Task<Result> Handle(DeleteContributorCommand request, CancellationToken cancellationToken)
    {
        // this approach: keep domain events in the domain model / code project; this becomes a pass-though
        return await _contributorService.DeleteContributor(request.ContributorId);
        
        // Another approach: do the real work here including dispatching domain events - change the event from 
        // internal to public. Using the service so that "domain event" behaviour remains in the domain model /core project 
            // var aggregateToDelete = await _repository.GetByIdAsync(contributorId);
            // if (aggregateToDelete == null) return Result.Result.NotFound();
            // await _repository.DeleteAsync(aggregateToDelete);
            // var domainEvent = new ContributorDeletedEvent(contributorId);
            // await _mediator.Publish(domainEvent);
            // return Result.Success();
    }
}