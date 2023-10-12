using eShopAsp.Core.Interfaces;

namespace eShopAsp.Core.SmartEnum.Core;

public readonly struct SmartEnumThen<TEnum, TValue>
    where TEnum : ISmartEnum
    where TValue : IEquatable<TValue>, IComparable<TValue>
{
    private readonly bool _isMatch;
    private readonly ISmartEnum _smartEnum;
    private readonly bool _stopEvaluating;

    internal SmartEnumThen(bool isMatch, bool stopEvaluating, ISmartEnum smartEnum)
    {
        _isMatch = isMatch;
        _stopEvaluating = stopEvaluating;
        _smartEnum = smartEnum;
    }

    public SmartEnumWhen<TEnum, TValue> Then(Action doThis)
    {
        if (!_stopEvaluating && _isMatch) doThis();
        return new SmartEnumWhen<TEnum, TValue>(_stopEvaluating || _isMatch, _smartEnum);
    }
}