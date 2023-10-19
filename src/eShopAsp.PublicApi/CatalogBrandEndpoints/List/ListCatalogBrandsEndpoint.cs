using AutoMapper;
using eShopAsp.Core.Entities;
using eShopAsp.Core.Interfaces;
using eShopAsp.UseCases.CatalogBrands;
using MinimalApi.Endpoint;

namespace eShopAsp.PublicApi.CatalogBrandEndpoints.List;

public class ListCatalogBrandsEndpoint : IEndpoint<IResult, IRepository<CatalogBrand>>
{
    private readonly IMapper _mapper;
    public ListCatalogBrandsEndpoint(IMapper mapper) => _mapper = mapper;

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/catalog-brands",
                async (IRepository<CatalogBrand> catalogBrandRepository) =>
                {
                    return await HandleAsync(catalogBrandRepository);
                })
            .Produces<ListCatalogBrandsResponse>()
            .WithTags("CatalogBrandsEndpoints");
    }

    public async Task<IResult> HandleAsync(IRepository<CatalogBrand> catalogBrandRepository)
    {
        var response = new ListCatalogBrandsResponse();
        var items = await catalogBrandRepository.ListAsync();
        response.CatalogBrands.AddRange(items.Select(_mapper.Map<CatalogBrandDTO>));
        return Results.Ok(response);
    }
}