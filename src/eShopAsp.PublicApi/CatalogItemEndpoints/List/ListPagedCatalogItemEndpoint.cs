using AutoMapper;
using AutoMapper.Internal;
using eShopAsp.Core.Entities.CatalogItemAggregate;
using eShopAsp.Core.Entities.CatalogItemAggregate.Specifications;
using eShopAsp.Core.Interfaces;
using eShopAsp.Core.Interfaces.Services;
using eShopAsp.UseCases.CatalogItems;
using MinimalApi.Endpoint;

namespace eShopAsp.PublicApi.CatalogItemEndpoints.List;

public class ListPagedCatalogItemEndpoint : IEndpoint<IResult, ListPagedCatalogItemRequest, IRepository<CatalogItem>>
{
    private readonly IMapper _mapper;
    private readonly IUriComposer _uriComposer;

    public ListPagedCatalogItemEndpoint(IUriComposer uriComposer, IMapper mapper)
    {
        _mapper = mapper;
        _uriComposer = uriComposer;
    }
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/catalog-items",
                async (int? pageSize, int? pageIndex, int? catalogBrandId,
                        int? catalogTypeId, IRepository<CatalogItem> itemRepository)
                    => await HandleAsync(
                        new ListPagedCatalogItemRequest(pageSize, pageIndex, catalogBrandId, catalogTypeId),
                        itemRepository))
            .Produces<ListPagedCatalogItemResponse>()
            .WithTags("CatalogItemEndpoints");
    }

    public async Task<IResult> HandleAsync(ListPagedCatalogItemRequest request, IRepository<CatalogItem> repository)
    {
        await Task.Delay(1000);
        var response = new ListPagedCatalogItemResponse(request.CorrelationId());
        var filterSpec = new CatalogFilterSpecification(request.CatalogBrandId, request.CatalogTypeId);
        int totalItems = await repository.CountAsync(filterSpec);
        var pagedSpec = new CatalogFilterPaginatedSpecification(
            skip: request.PageIndex * request.PageSize,
            take: request.PageSize,
            brandId: request.CatalogBrandId,
            typeId: request.CatalogTypeId);
        var items = await repository.ListAsync(pagedSpec);
        response.CatalogItems.TryAdd(items.Select(_mapper.Map<CatalogItemDTO>));
        foreach (CatalogItemDTO item in response.CatalogItems)
        {
            item.PictureUri = _uriComposer.ComposePicUri(item.PictureUri);
        }

        if (request.PageSize > 0 )
        {
            response.PageCount = int.Parse(Math.Ceiling((decimal)totalItems / request.PageSize).ToString());
        }
        else
        {
            response.PageCount = totalItems > 0 ? 1 : 0;
        }

        return Results.Ok(response);
    }
}