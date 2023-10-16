namespace eShopAsp.Core.Exceptions;

public class BasketNotFoundException : Exception
{
    public BasketNotFoundException(int basketId) : base($"No basket not found with id: {basketId}"){}
}