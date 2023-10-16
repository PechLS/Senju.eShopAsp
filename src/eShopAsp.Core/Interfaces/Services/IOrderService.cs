using eShopAsp.Core.Entities.OrderAggregate;

namespace eShopAsp.Core.Interfaces.Services;

public interface IOrderService
{
    Task CreatOrderAsync(int basketId, Address shippingToAddress);
}