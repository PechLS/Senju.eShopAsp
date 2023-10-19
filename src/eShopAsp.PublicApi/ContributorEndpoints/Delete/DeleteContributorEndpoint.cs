using eShopAsp.Core.Result;
using eShopAsp.UseCases.Contributors.Delete;
using FastEndpoints;
using MediatR;

namespace eShopAsp.PublicApi.ContributorEndpoints.Delete;

public class DeleteContributorEndpoint : Endpoint<DeleteContributorRequest>
{
    private readonly IMediator _mediator;
    public DeleteContributorEndpoint(IMediator mediator) => _mediator = mediator;

    public override void Configure()
    {
        Delete(DeleteContributorRequest.Route);
        AllowAnonymous();
    }

    public override async Task HandleAsync(DeleteContributorRequest request, CancellationToken cancellationToken)
    {
        var command = new DeleteContributorCommand(request.ContributorId);
        var result = await _mediator.Send(command, cancellationToken);
        if (result.Status == ResultStatus.NotFound)
        {
            await SendNotFoundAsync(cancellationToken);
            return;
        }

        if (result.IsSuccess)
        {
            await SendNoContentAsync(cancellationToken);
        }
        
    }
}