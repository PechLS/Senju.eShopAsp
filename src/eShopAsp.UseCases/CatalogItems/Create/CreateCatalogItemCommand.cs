using eShopAsp.Core.Interfaces.Command;
using eShopAsp.Core.Result;

namespace eShopAsp.UseCases.CatalogItems.Create;

public record CreateCatalogItemCommand(
    int CatalogBrandId, int CatalogTypeId, string Description, 
    string Name, string PictureUri, decimal Price) : ICommand<Result<int>>;