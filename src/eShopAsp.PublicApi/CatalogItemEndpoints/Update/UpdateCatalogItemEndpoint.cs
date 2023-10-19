using eShopAsp.Core.Entities.CatalogItemAggregate;
using eShopAsp.Core.Interfaces;
using eShopAsp.Core.Interfaces.Services;
using eShopAsp.UseCases.CatalogItems;
using MinimalApi.Endpoint;

namespace eShopAsp.PublicApi.CatalogItemEndpoints.Update;

public class UpdateCatalogItemEndpoint : IEndpoint<IResult, UpdateCatalogItemRequest, IRepository<CatalogItem>>
{
    private readonly IUriComposer _uriComposer;
    public UpdateCatalogItemEndpoint(IUriComposer uriComposer) => _uriComposer = uriComposer;

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPut("api/catalog-items",
                async (UpdateCatalogItemRequest request, IRepository<CatalogItem> repository)
                    => await HandleAsync(request, repository))
            .Produces<UpdateCatalogItemResponse>()
            .WithTags("CatalogItemEndpoint");
    }

    public async Task<IResult> HandleAsync(UpdateCatalogItemRequest request, IRepository<CatalogItem> repository)
    {
        var response = new UpdateCatalogItemResponse(request.CorrelationId());
        var existingItem = await repository.GetByIdAsync(request.Id);
        if (existingItem == null) return Results.NotFound();

        CatalogItem.CatalogItemDetails details = new(request.Name, request.Description, request.Price);
        existingItem.UpdateDetails(details);
        existingItem.UpdateBrand(request.CatalogBrandId);
        existingItem.UpdateType(request.CatalogTypeId);

        await repository.UpdateAsync(existingItem);

        response.CatalogItem = new CatalogItemDTO
        {
            Id = existingItem.Id,
            CatalogBrandId = existingItem.CatalogBrandId,
            CatalogTypeId = existingItem.CatalogTypeId,
            Description = existingItem.Description,
            Name = existingItem.Name,
            PictureUri = _uriComposer.ComposePicUri(existingItem.PictureUri),
            Price = existingItem.Price
        };
        return Results.Ok(response);
    }
}