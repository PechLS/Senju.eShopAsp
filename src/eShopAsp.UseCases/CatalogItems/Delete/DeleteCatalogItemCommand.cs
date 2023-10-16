using eShopAsp.Core.Interfaces.Command;
using eShopAsp.Core.Result;

namespace eShopAsp.UseCases.CatalogItems.Delete;

public record DeleteCatalogItemCommand(int CatalogItemId) : ICommand<Result>;
