using System.Runtime.Serialization;

namespace eShopAsp.Core.Exceptions.SmartEnum;

[Serializable]
public class SmartFlagEnumDoesNotContainPowerOfTwoValuesException : Exception
{
    public SmartFlagEnumDoesNotContainPowerOfTwoValuesException() : base() {}
    public SmartFlagEnumDoesNotContainPowerOfTwoValuesException(string message): base(message){}
    public SmartFlagEnumDoesNotContainPowerOfTwoValuesException(string message, Exception innerException) : base(message, innerException) {}
    protected SmartFlagEnumDoesNotContainPowerOfTwoValuesException(SerializationInfo info, StreamingContext context) : base(info, context) {}
}