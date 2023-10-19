using eShopAsp.Core.Result;
using eShopAsp.UseCases.Contributors;
using eShopAsp.UseCases.Contributors.Get;
using FastEndpoints;
using MediatR;

namespace eShopAsp.PublicApi.ContributorEndpoints.GetById;

public class GetContributorByIdEndpoint : Endpoint<GetContributorByIdRequest, ContributorDTO>
{
    private readonly IMediator _mediator;

    public GetContributorByIdEndpoint(IMediator mediator) => _mediator = mediator;

    public override void Configure()
    {
        Get(GetContributorByIdRequest.Route);
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetContributorByIdRequest request, CancellationToken cancellationToken)
    {
        var query = new GetContributorQuery(request.ContributorId);
        var result = await _mediator.Send(query, cancellationToken);
        if (result.Status == ResultStatus.NotFound)
        {
            await SendNotFoundAsync(cancellationToken);
            return;
        }

        if (result.IsSuccess)
        {
            Response = new ContributorDTO(result.Value.Id, result.Value.Name);
        }
    }
}