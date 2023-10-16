using eShopAsp.Core.Extensions;

namespace eShopAsp.Core.Entities.OrderAggregate.Specifications;

public sealed class CustomerOrdersSpecification : Specification<Order>
{
    public CustomerOrdersSpecification(string buyerId)
    {
        Query.Where(o => o.BuyerId == buyerId)
            .Include(o => o.OrderItems);
    }
}