using System.Runtime.Serialization;

namespace eShopAsp.Core.Exceptions.SmartEnum;

[Serializable]
public class InvalidFlagEnumValueParseException : Exception
{
    public InvalidFlagEnumValueParseException() : base(){}
    public InvalidFlagEnumValueParseException(string message) : base(message) {}
    public InvalidFlagEnumValueParseException(string message, Exception innerException) : base(message, innerException) {} 
    protected InvalidFlagEnumValueParseException(SerializationInfo info, StreamingContext context) : base(info, context){}
}