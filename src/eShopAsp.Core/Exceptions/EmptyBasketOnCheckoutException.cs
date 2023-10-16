using System.Runtime.Serialization;

namespace eShopAsp.Core.Exceptions;

public class EmptyBasketOnCheckoutException : Exception
{
    public EmptyBasketOnCheckoutException() : base($"Basket cannot have 0 items on checkout."){}
    protected EmptyBasketOnCheckoutException(SerializationInfo info, StreamingContext context) : base(info, context) {}
    public EmptyBasketOnCheckoutException(string msg) : base(msg) {}
    public EmptyBasketOnCheckoutException(string msg, Exception innerException) : base(msg, innerException){}
}