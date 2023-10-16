using eShopAsp.Core.Entities.BasketAggregate;
using eShopAsp.Core.Exceptions;

namespace eShopAsp.Core.GuardClauses;

public static class BasketGuards
{
    public static void EmptyBasketOnCheckout(this IGuardClause guardClause, IReadOnlyCollection<BasketItem> basketItems)
    {
        if (!basketItems.Any()) throw new EmptyBasketOnCheckoutException();
    }
}