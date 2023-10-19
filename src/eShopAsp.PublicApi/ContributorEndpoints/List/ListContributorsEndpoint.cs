using eShopAsp.UseCases.Contributors;
using eShopAsp.UseCases.Contributors.List;
using FastEndpoints;
using MediatR;

namespace eShopAsp.PublicApi.ContributorEndpoints.List;

public class ListContributorsEndpoint : EndpointWithoutRequest<ListContributorsResponse>
{
    private readonly IMediator _mediator;
    public ListContributorsEndpoint(IMediator mediator) => _mediator = mediator;
    public override void Configure()
    {
        Get("/Contributors");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var result = await _mediator.Send(new ListContributorsQuery(null, null));
        if (result.IsSuccess)
        {
            Response = new ListContributorsResponse
            {
                Contributors = result.Value.Select(c => new ContributorDTO(c.Id, c.Name)).ToList()
            };
        }
    }
}