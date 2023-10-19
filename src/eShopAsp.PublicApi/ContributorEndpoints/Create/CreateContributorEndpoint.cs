using eShopAsp.Core.Entities.ContributorAggregate;
using eShopAsp.Core.Interfaces;
using eShopAsp.UseCases.Contributors.Create;
using FastEndpoints;
using MediatR;

namespace eShopAsp.PublicApi.ContributorEndpoints.Create;

public class CreateContributorEndpoint : Endpoint<CreateContributorRequest, CreateContributorResponse>
{

    private readonly IRepository<Contributor> _repository;
    private readonly IMediator _mediator;

    public CreateContributorEndpoint(IRepository<Contributor> repository, IMediator mediator)
    {
        _repository = repository;
        _mediator = mediator;
    }

    public override void Configure()
    {
        Post(CreateContributorRequest.Route);
        AllowAnonymous();
        Summary(s =>
        {
            // XML Docs are used by default but are overridden by these properties.
            s.Summary = "Create a new Contributor";
            s.Description = "Create a new Contributor, a valid name is required.";
            s.ExampleRequest = new CreateContributorRequest { Name = "Contributor Name" };
        });
    }
    
    public override async Task HandleAsync(CreateContributorRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new CreateContributorCommand(request.Name!), cancellationToken);
        if (result.IsSuccess)
        {
            Response = new CreateContributorResponse(result.Value, request.Name!);
            return;
        }
        // todo: handle other case as necessary.
    }
    
}