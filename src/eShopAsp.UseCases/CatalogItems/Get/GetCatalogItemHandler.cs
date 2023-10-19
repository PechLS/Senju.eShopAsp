using eShopAsp.Core.Entities.CatalogItemAggregate;
using eShopAsp.Core.Entities.CatalogItemAggregate.Specifications;
using eShopAsp.Core.Interfaces;
using eShopAsp.Core.Interfaces.Query;
using eShopAsp.Core.Result;

namespace eShopAsp.UseCases.CatalogItems.Get;

public class GetCatalogItemHandler : IQueryHandler<GetCatalogItemQuery, Result<CatalogItemDTO>>
{
    private readonly IRepository<CatalogItem> _repository;

    public GetCatalogItemHandler(IRepository<CatalogItem> repository) => _repository = repository;
    
    public async Task<Result<CatalogItemDTO>> Handle(GetCatalogItemQuery request, CancellationToken cancellationToken)
    {
        var specification = new CatalogItemByIdSpecification(request.CatalogItemId);
        var entity = await _repository.GetByIdAsync(specification);
        if (entity is null) return Result.NotFound();
        return new CatalogItemDTO()
        {
            Id = entity.Id, 
            Name = entity.Name,
            Description = entity.Description, 
            Price = entity.Price, 
            PictureUri = entity.PictureUri,
            CatalogTypeId = entity.CatalogTypeId, 
            CatalogBrandId = entity.CatalogBrandId
            
        };
    }
}