namespace eShopAsp.Core.Exceptions;

public class DuplicateOrderChainException : Exception
{
    private const string _message =
        "The Specification contains more than one Order chain.";
    
    public DuplicateOrderChainException() : base(_message) {}
    public DuplicateOrderChainException(Exception innerException) : base(_message, innerException) {}
}