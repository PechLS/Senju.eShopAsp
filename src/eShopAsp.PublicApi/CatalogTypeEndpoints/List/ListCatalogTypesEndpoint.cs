using AutoMapper;
using eShopAsp.Core.Entities;
using eShopAsp.Core.Interfaces;
using eShopAsp.UseCases.CatalogTypes;
using MinimalApi.Endpoint;

namespace eShopAsp.PublicApi.CatalogTypeEndpoints.List;

public class ListCatalogTypesEndpoint : IEndpoint<IResult, IRepository<CatalogType>>
{
    private readonly IMapper _mapper;
    public ListCatalogTypesEndpoint(IMapper mapper) => _mapper = mapper;

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/catalog-types",
                async (IRepository<CatalogType> catalogTypeRepository) =>
                await HandleAsync(catalogTypeRepository))
            .Produces<ListCatalogTypesResponse>()
            .WithTags("CatalogTypeEndpoints");
    }
    
    public async Task<IResult> HandleAsync(IRepository<CatalogType> catalogTypeRepository)
    {
        var response = new ListCatalogTypesResponse();
        var items = await catalogTypeRepository.ListAsync();
        response.CatalogTypes.AddRange(items.Select(_mapper.Map<CatalogTypeDTO>));
        return Results.Ok(response);
    }
}