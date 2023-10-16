using eShopAsp.Core.Entities.CatalogItemAggregate;
using eShopAsp.Core.Interfaces.Command;
using eShopAsp.Core.Result;

namespace eShopAsp.UseCases.CatalogItems.Update;

public record UpdateCatalogItemCommand(
    int CatalogItemId, int CatalogBrandId, int CatalogTypeId, 
    string Description, string Name, string PictureBase64, 
    string PictureUri, string PictureName, decimal Price) : ICommand<Result<CatalogItemDTO>>;