using eShopAsp.Core.Entities.ContributorAggregate;
using eShopAsp.Core.Interfaces;
using eShopAsp.Core.Result;
using eShopAsp.UseCases.Contributors.Update;
using FastEndpoints;
using MediatR;

namespace eShopAsp.PublicApi.ContributorEndpoints.Update;

public class UpdateContributorEndpoint : Endpoint<UpdateContributorRequest, UpdateContributorResponse>
{
    private readonly IRepository<Contributor> _repository;
    private readonly IMediator _mediator;

    public UpdateContributorEndpoint(IRepository<Contributor> repository, IMediator mediator)
    {
        _repository = repository;
        _mediator = mediator;
    }

    public override void Configure()
    {
        Put(UpdateContributorRequest.Route);
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateContributorRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateContributorCommand(request.Id, request.Name);
        var result = await _mediator.Send(command, cancellationToken);
        if (result.Status == ResultStatus.NotFound)
        {
            await SendNotFoundAsync(cancellationToken);
            return;
        }

        // todo: use Mediator
        var existingContributor = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (existingContributor == null)
        {
            await SendNotFoundAsync(cancellationToken);
            return;
        }
        if (result.IsSuccess)
        {
            Response = new UpdateContributorResponse(result.Value);
        }
    }
}