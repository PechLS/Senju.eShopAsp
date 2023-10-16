namespace eShopAsp.UseCases.CatalogItems;

public record CatalogItemDTO(
    int Id,
    string Name,
    string Description,
    decimal Price,
    string PictureUri,
    int CatalogTypeId,
    int CatalogBrandId);