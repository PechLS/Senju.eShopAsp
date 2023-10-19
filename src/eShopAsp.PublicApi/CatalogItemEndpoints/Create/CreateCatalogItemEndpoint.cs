using eShopAsp.Core.Entities.CatalogItemAggregate;
using eShopAsp.Core.Interfaces;
using eShopAsp.Core.Interfaces.Services;
using MinimalApi.Endpoint;
using Microsoft.AspNetCore.Authorization;
using eShopAsp.Core.Entities.CatalogItemAggregate.Specifications;
using eShopAsp.Core.Exceptions;
using eShopAsp.UseCases.CatalogItems;

namespace eShopAsp.PublicApi.CatalogItemEndpoints.Create;

public class CreateCatalogItemEndpoint : IEndpoint<IResult, CreateCatalogItemRequest, IRepository<CatalogItem>>
{
    private readonly IUriComposer _uriComposer;
    public CreateCatalogItemEndpoint(IUriComposer uriComposer) => _uriComposer = uriComposer;
    
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("api/catalog-items",
             [Authorize]   
             async (CreateCatalogItemRequest request, IRepository<CatalogItem> itemRepository) 
             => await  HandleAsync(request, itemRepository))
            .Produces<CreateCatalogItemRequest>()
            .WithTags("CreateCatalogItemEndpoints");
    }

    public async Task<IResult> HandleAsync(CreateCatalogItemRequest request, IRepository<CatalogItem> repository)
    {
        var response = new CreateCatalogItemResponse(request.CorrelationId());
        var catalogItemNameSpecification = new CatalogItemNameSpecification(request.Name);
        var existingCatalogItem = await repository.CountAsync(catalogItemNameSpecification);
        if (existingCatalogItem > 0) throw new DuplicateException($"A catalogItem with name {request.Name} already exist");
        var newItem = new CatalogItem(request.CatalogTypeId, request.CatalogBrandId, request.Description, request.Name, request.Price, request.PictureUri);
        newItem = await repository.AddAsync(newItem);
        if (newItem.Id != 0)
        {
            // We disabled the upload functionality and added the default/placeholder image to this sample due to a potential security risk
            //pointed out by the community. 
            // In the production, we recommend the uploading to a blob storage and deliver the image via CDN
            // after a verification process.
            newItem.UpdatePictureUri("eCatalog-item-default.png");
            await repository.UpdateAsync(newItem);
        }
        var dto = new CatalogItemDTO
        {
            Id = newItem.Id,
            CatalogBrandId = newItem.CatalogBrandId,
            CatalogTypeId = newItem.CatalogTypeId,
            Name = newItem.Name,
            PictureUri = _uriComposer.ComposePicUri(newItem.PictureUri),
            Price = newItem.Price
        };
        response.CatalogItem = dto;
        return Results.Created($"api/catalog-items/{dto.Id}", response);
    }
}