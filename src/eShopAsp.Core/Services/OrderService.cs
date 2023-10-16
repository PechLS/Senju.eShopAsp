using eShopAsp.Core.Entities.BasketAggregate;
using eShopAsp.Core.Entities.BasketAggregate.Specifications;
using eShopAsp.Core.Entities.CatalogItemAggregate;
using eShopAsp.Core.Entities.CatalogItemAggregate.Specifications;
using eShopAsp.Core.Entities.OrderAggregate;
using eShopAsp.Core.GuardClauses;
using eShopAsp.Core.Interfaces;
using eShopAsp.Core.Interfaces.Services;

namespace eShopAsp.Core.Services;

public class OrderService : IOrderService
{
    private readonly IRepository<Order> _orderRepository;
    private readonly IUriComposer _uriComposer;
    private readonly IRepository<Basket> _basketRepository;
    private readonly IRepository<CatalogItem> _catalogItemRepository;

    public OrderService(
        IRepository<Order> orderRepository,
        IRepository<CatalogItem> catalogItemRepository,
        IRepository<Basket> basketRepository,
        IUriComposer uriComposer)
    {
        _orderRepository = orderRepository;
        _basketRepository = basketRepository;
        _catalogItemRepository = catalogItemRepository;
        _uriComposer = uriComposer;
    }

    public async Task CreatOrderAsync(int basketId, Address shippingToAddress)
    {
        var basketSpec = new BasketWithItemsSpecification(basketId);
        var basket = await _basketRepository.FirstOrDefaultAsync(basketSpec);

        Guard.Against.Null(basket, nameof(basket));
        Guard.Against.EmptyBasketOnCheckout(basket.Items);

        var catalogItemsSpec = new CatalogItemsSpecification(basket.Items.Select(item => item.CatalogItemId).ToArray());
        var catalogItems = await _catalogItemRepository.ListAsync(catalogItemsSpec);
        var items = basket.Items.Select(basketItem =>
        {
            var catalogItem = catalogItems.First(c => c.Id == basketItem.CatalogItemId);
            var itemOrdered = new CatalogItemOrdered(catalogItem.Id, catalogItem.Name,
                _uriComposer.ComposePicUri(catalogItem.PictureUri));
            var orderItem = new OrderItem(itemOrdered, basketItem.UnitPrice, basketItem.Quantity);
            return orderItem;
        }).ToList();
        var order = new Order(basket.BuyerId, shippingToAddress, items);
        await _orderRepository.AddAsync(order);
    }
}