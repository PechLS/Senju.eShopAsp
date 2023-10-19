using eShopAsp.Core.Entities.CatalogItemAggregate;
using eShopAsp.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using MinimalApi.Endpoint;

namespace eShopAsp.PublicApi.CatalogItemEndpoints.Delete;

public class DeleteCatalogItemEndpoint : IEndpoint<IResult, DeleteCatalogItemRequest, IRepository<CatalogItem>>
{
    
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapDelete("api/catalog-items/{catalogItemId}",
                [Authorize] async (int catalogItemId, IRepository<CatalogItem> repository)
                    => await HandleAsync(new DeleteCatalogItemRequest(catalogItemId), repository))
            .Produces<DeleteCatalogItemResponse>()
            .WithTags("CatalogItemEndpoints");
    }

    public async Task<IResult> HandleAsync(DeleteCatalogItemRequest request, IRepository<CatalogItem> repository)
    {
        var response = new DeleteCatalogItemResponse(request.CorrelationId());
        var itemToDelete = await repository.GetByIdAsync(request.CatalogItemId);
        if (itemToDelete is null) return Results.NotFound();
        await repository.DeleteAsync(itemToDelete);
        return Results.Ok(response);
    }
}