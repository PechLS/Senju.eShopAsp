using eShopAsp.Core.Entities.CatalogItemAggregate;
using eShopAsp.Core.Interfaces;
using eShopAsp.Core.Interfaces.Command;
using eShopAsp.Core.Result;

namespace eShopAsp.UseCases.CatalogItems.Create;

public class CreateCatalogItemHandler : ICommandHandler<CreateCatalogItemCommand, Result<int>>
{
    private IRepository<CatalogItem> _repository;
    public CreateCatalogItemHandler(IRepository<CatalogItem> repository) => _repository = repository;
    
    public async Task<Result<int>> Handle(CreateCatalogItemCommand request, CancellationToken cancellationToken)
    {
        var newCatalogItem = new CatalogItem(
            request.CatalogTypeId, request.CatalogBrandId, request.Description,
            request.Name, request.Price, request.PictureUri);
        var createdCatalogItem = await _repository.AddAsync(newCatalogItem, cancellationToken);
        return createdCatalogItem.Id;
    }
}