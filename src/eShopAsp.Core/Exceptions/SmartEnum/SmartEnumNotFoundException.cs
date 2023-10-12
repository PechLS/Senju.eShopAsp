using System.Runtime.Serialization;

namespace eShopAsp.Core.Exceptions.SmartEnum;

[Serializable]
public class SmartEnumNotFoundException : Exception
{
    public SmartEnumNotFoundException() : base(){}
    public SmartEnumNotFoundException(string message) : base(message){}
    public SmartEnumNotFoundException(string message, Exception innerException) : base(message, innerException) {}
    protected SmartEnumNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) {}
}