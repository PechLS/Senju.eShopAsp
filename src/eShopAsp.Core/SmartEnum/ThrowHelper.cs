using eShopAsp.Core.Exceptions.SmartEnum;
using eShopAsp.Core.Interfaces;

namespace eShopAsp.Core.SmartEnum;

static class ThrowHelper
{
    public static void ThrowArgumentNullException(string paramName)
        => throw new ArgumentNullException(paramName);

    public static void ThrowArgumentNullOrEmptyException(string paramName)
        => throw new ArgumentException("Argument cannot be null or empty", paramName);

    public static void ThrowNameNotFoundException<TEnum, TValue>(string name)
        where TEnum : ISmartEnum
        where TValue : IEquatable<TValue>, IComparable<TValue>
        => throw new SmartEnumNotFoundException($"No {typeof(TEnum).Name} with Name \"{name}\" not found.");

    public static void ThrowValueNotFoundException<TEnum, TValue>(TValue value)
        where TEnum : ISmartEnum
        where TValue : IEquatable<TValue>, IComparable<TValue>
        => throw new SmartEnumNotFoundException($"No {typeof(TEnum).Name} with Value \"{value}\" not found.");

    public static void ThrowContainsNegativeValueException<TEnum, TValue>()
        where TEnum : ISmartEnum
        where TValue : IEquatable<TValue>, IComparable<TValue>
        => throw new SmartFlagEnumContainsNegativeValueException($"The {typeof(TEnum).Name} contains negative values other than (-1).");

    public static void ThrowInvalidValueCastException<TEnum, TValue>(TValue value)
        where TEnum : ISmartEnum
        where TValue : IEquatable<TValue>, IComparable<TValue>
        => throw new InvalidFlagEnumValueParseException(
            $"The value: {value} input to {typeof(TEnum).Name} could not be parsed into an integer value.");

    public static void ThrowNegativeValueArgumentException<TEnum, TValue>(TValue value)
        where TEnum : ISmartEnum
        where TValue : IEquatable<TValue>, IComparable<TValue>
        => throw new NegativeValueArgumentException(
            $"The value: {value} input to {typeof(TEnum).Name} was a negative number other than (-1).");

    public static void ThrowNegativeValueArgumentException<TEnum, TValue>(int value)
        where TEnum : ISmartEnum
        where TValue : IEquatable<TValue>, IComparable<TValue>
        => throw new NegativeValueArgumentException(
            $"The value: {value} input to {typeof(TEnum).Name} was a negative number other than (-1).");

    public static void ThrowDoesNotContainPowerOfTwoValueException<TEnum, TValue>()
        where TEnum : ISmartEnum
        where TValue : IEquatable<TValue>, IComparable<TValue>
        => throw new SmartFlagEnumDoesNotContainPowerOfTwoValuesException(
            $"The {typeof(TEnum).Name} does not contain consecutive power of two values.");
}