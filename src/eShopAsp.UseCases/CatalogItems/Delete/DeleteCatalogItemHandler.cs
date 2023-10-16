using eShopAsp.Core.Entities.CatalogItemAggregate;
using eShopAsp.Core.Interfaces;
using eShopAsp.Core.Interfaces.Command;
using eShopAsp.Core.Result;

namespace eShopAsp.UseCases.CatalogItems.Delete;

public class DeleteCatalogItemHandler : ICommandHandler<DeleteCatalogItemCommand, Result>
{
    private IRepository<CatalogItem> _repository;
    private IAppLogger<CatalogItem> _logger;

    public DeleteCatalogItemHandler(IRepository<CatalogItem> repository, IAppLogger<CatalogItem> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<Result> Handle(DeleteCatalogItemCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Deleting CatalogItem id: {request.CatalogItemId}");
        var aggregateToDelete = await _repository.GetByIdAsync(request.CatalogItemId);
        if (aggregateToDelete is null) return Result.NotFound();
        await _repository.DeleteAsync(aggregateToDelete);
        return Result.Success();
    }
}