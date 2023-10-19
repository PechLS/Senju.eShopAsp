using eShopAsp.Core.Entities.CatalogItemAggregate;
using eShopAsp.Core.Interfaces;
using eShopAsp.Core.Interfaces.Services;
using eShopAsp.UseCases.CatalogItems;
using MinimalApi.Endpoint;

namespace eShopAsp.PublicApi.CatalogItemEndpoints.GetById;

public class GetCatalogItemByIdEndpoint : IEndpoint<IResult, GetCatalogItemByIdRequest, IRepository<CatalogItem>>
{
    private readonly IUriComposer _uriComposer;
    public GetCatalogItemByIdEndpoint(IUriComposer uriComposer) => _uriComposer = uriComposer;

    public async Task<IResult> HandleAsync(GetCatalogItemByIdRequest request, IRepository<CatalogItem> repository)
    {
        var response = new GetCatalogItemByIdResponse(request.CorrelationId());
        var item = await repository.GetByIdAsync(request.CatalogItemId);
        if (item is null) return Results.NotFound();
        response.CatalogItem = new CatalogItemDTO()
        {
            Id = item.Id,
            CatalogBrandId = item.CatalogBrandId,
            CatalogTypeId = item.CatalogTypeId,
            Description = item.Description,
            Name = item.Name,
            PictureUri = item.PictureUri,
            Price = item.Price
        };
        return Results.Ok(response);
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/catalog-items/{catalogItemId}",
                async (int catalogItemId, IRepository<CatalogItem> itemRepository)
                    => await HandleAsync(new GetCatalogItemByIdRequest(catalogItemId), itemRepository))
            .Produces<GetCatalogItemByIdResponse>()
            .WithTags("CatalogItemEndpoints");
    }
}