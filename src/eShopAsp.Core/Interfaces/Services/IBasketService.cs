using eShopAsp.Core.Entities.BasketAggregate;
using eShopAsp.Core.Result;

namespace eShopAsp.Core.Interfaces.Services;

public interface IBasketService
{
    Task TransferBasketAsync(string anonymousId, string userName);
    Task<Basket> AddItemToBasket(string userName, int catalogItemId, decimal price, int quantity = 1);
    Task<Result<Basket>> SetQuantities(int basketId, Dictionary<string, int> quantities);
    Task DeleteBasketAsync(int basketId);
}