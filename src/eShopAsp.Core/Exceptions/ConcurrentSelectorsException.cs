namespace eShopAsp.Core.Exceptions;

public class ConcurrentSelectorsException : Exception
{
    private const string _message =
        "Concurrent specification selector transform defined. " +
        "Ensure only one of the Selector() or SelectorMany() transforms is used in the Specification!";
    
    public ConcurrentSelectorsException() : base(_message) {}
    public ConcurrentSelectorsException(Exception innerException) : base(_message, innerException) {}
}