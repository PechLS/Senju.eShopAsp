namespace eShopAsp.PublicApi;

/// <summary>
/// base class used by api requests and responses
/// </summary>
public abstract class BaseMessage
{
    protected Guid _correlationId = Guid.NewGuid();
    public Guid CorrelationId() => _correlationId;
}