namespace eShopAsp.Core.Exceptions;

public class SelectNotFoundException : Exception
{
    private const string _message =
        "The Specification must have a selector transform defined. " +
        "Ensure either Select() or SelectMany() is used in the Specification!";

    public SelectNotFoundException() : base(_message){}
    public SelectNotFoundException(Exception innerException) : base(_message, innerException) {}
}