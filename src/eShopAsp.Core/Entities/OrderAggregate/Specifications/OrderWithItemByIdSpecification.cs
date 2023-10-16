using eShopAsp.Core.Extensions;
using eShopAsp.Core.Interfaces;

namespace eShopAsp.Core.Entities.OrderAggregate.Specifications;

public sealed class OrderWithItemByIdSpecification : Specification<Order>, ISingleResultSpecification<Order>
{
    public OrderWithItemByIdSpecification(int orderId)
    {
        Query
            .Where(o => o.Id == orderId)
            .Include(o => o.OrderItems)
            .ThenInclude(i => i.ItemOrdered);
    }
}