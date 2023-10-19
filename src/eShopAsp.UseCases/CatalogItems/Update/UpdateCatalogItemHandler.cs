using eShopAsp.Core.Entities.CatalogItemAggregate;
using eShopAsp.Core.Interfaces;
using eShopAsp.Core.Interfaces.Command;
using eShopAsp.Core.Result;

namespace eShopAsp.UseCases.CatalogItems.Update;

public class UpdateCatalogItemHandler : ICommandHandler<UpdateCatalogItemCommand, Result<CatalogItemDTO>>
{
    private readonly IRepository<CatalogItem> _repository;
    private readonly IAppLogger<CatalogItem> _logger;

    public UpdateCatalogItemHandler(IRepository<CatalogItem> repository, IAppLogger<CatalogItem> logger)
    {
        _repository = repository;
        _logger = logger;
    }
    
    public async Task<Result<CatalogItemDTO>> Handle(UpdateCatalogItemCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Updating CatalogItem id: {request.CatalogItemId}");
        var existingCatalogItem = await _repository.GetByIdAsync(request.CatalogItemId, cancellationToken);
        if (existingCatalogItem is null) return Result.NotFound();
        CatalogItem.CatalogItemDetails details = new(request.Name, request.Description, request.Price);
        existingCatalogItem.UpdateDetails(details);
        existingCatalogItem.UpdateBrand(request.CatalogBrandId);
        existingCatalogItem.UpdateType(request.CatalogItemId);
        await _repository.UpdateAsync(existingCatalogItem, cancellationToken);
        return new CatalogItemDTO (){
            Id = existingCatalogItem.Id,
            Name = existingCatalogItem.Name,
            Description = existingCatalogItem.Description,
            Price = existingCatalogItem.Price,
            PictureUri = existingCatalogItem.PictureUri,
            CatalogTypeId = existingCatalogItem.CatalogTypeId,
            CatalogBrandId = existingCatalogItem.CatalogBrandId};
    }
}