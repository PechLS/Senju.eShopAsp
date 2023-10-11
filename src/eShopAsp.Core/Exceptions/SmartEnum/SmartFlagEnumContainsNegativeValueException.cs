using System.Runtime.Serialization;

namespace eShopAsp.Core.Exceptions.SmartEnum;

[Serializable]
public class SmartFlagEnumContainsNegativeValueException : Exception
{
    public SmartFlagEnumContainsNegativeValueException() : base() {}
    public SmartFlagEnumContainsNegativeValueException(string message) : base(message){}
    public SmartFlagEnumContainsNegativeValueException(string message, Exception innerException) : base(message, innerException) {}
    protected SmartFlagEnumContainsNegativeValueException(SerializationInfo info, StreamingContext context) : base(info, context) {}
}