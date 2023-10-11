using System.Runtime.Serialization;

namespace eShopAsp.Core.Exceptions.SmartEnum;

[Serializable]
public class NegativeValueArgumentException : ArgumentException
{
    public NegativeValueArgumentException() : base() {}
    public NegativeValueArgumentException(string message) : base(message){}
    public NegativeValueArgumentException(string message, Exception innerException) : base(message, innerException) {}
    protected NegativeValueArgumentException(SerializationInfo info, StreamingContext context) : base(info, context) {}
}