using eShopAsp.Core.GuardClauses;
using eShopAsp.Core.Interfaces;

namespace eShopAsp.Core.Entities.OrderAggregate;

public class Order : EntityBase, IAggregateRoot
{
    public string BuyerId { get; private set; }
    public DateTimeOffset OrderDate { get; private set; } = DateTimeOffset.Now;
    public Address ShipToAddress { get; private set; }
    
    // DDD pattern comment
    // Using a private collection field, better for DDD Aggregate's encapsulation.
    // so OrderItems cannot be added from "outside the AggregateRoot" directly to the collection
    // but only through the method Order.AddOrderItem() which includes behavior.
    private readonly List<OrderItem> _orderItems = new();

    // Using List<>.AsReadOnly()
    // this will create a read only wrapper around the private list so is protected against "external updates"
    // It's much cheaper than .ToList() because it will not have to copy all items in a new collection.
    // (Just once heap alloc for the wrapper instance)
    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();


    private Order(){}
    public Order(string buyerId, Address shipToAddress, List<OrderItem> items)
    {
        Guard.Against.NullOrEmpty(buyerId, nameof(buyerId));
        BuyerId = buyerId;
        ShipToAddress = shipToAddress;
        _orderItems = items;
    }

    public decimal Total()
    {
        var total = 0m;
        foreach (var item  in _orderItems)
        {
            total += item.UnitPrice * item.Units;
        }
        return total;
    }
}