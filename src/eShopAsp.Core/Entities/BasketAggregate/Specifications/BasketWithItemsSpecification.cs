using eShopAsp.Core.Extensions;
using eShopAsp.Core.Interfaces;

namespace eShopAsp.Core.Entities.BasketAggregate.Specifications;

public sealed class BasketWithItemsSpecification : Specification<Basket>, ISingleResultSpecification<Basket>
{
    public BasketWithItemsSpecification(int basketId)
    {
        Query
            .Where(b => b.Id == basketId)
            .Include(b => b.Items);
    }

    public BasketWithItemsSpecification(string buyerId)
    {
        Query
            .Where(b => b.BuyerId == buyerId)
            .Include(b => b.Items);
    }
}