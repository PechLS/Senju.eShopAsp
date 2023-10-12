using eShopAsp.Core.Interfaces;

namespace eShopAsp.Core.SmartEnum.Core;

public readonly struct SmartEnumWhen<TEnum, TValue>
    where TEnum : ISmartEnum
    where TValue : IEquatable<TValue>, IComparable<TValue>
{
    private readonly ISmartEnum _smartEnum;
    private readonly bool _stopEvaluating;

    internal SmartEnumWhen(bool stopEvaluating, ISmartEnum smartEnum)
    {
        _smartEnum = smartEnum;
        _stopEvaluating = stopEvaluating;
    }

    /// <summary>
    /// Execute this action if no others calls the When have matched.
    /// </summary>
    /// <param name="action">The Action to call.</param>
    public void Default(Action action)
    {
        if (!_stopEvaluating) action();
    }

    
    public SmartEnumThen<TEnum, TValue> When(ISmartEnum smartEnumWhen)
        => new SmartEnumThen<TEnum, TValue>
            (isMatch: _smartEnum.Equals(smartEnumWhen), stopEvaluating: _stopEvaluating, smartEnum: _smartEnum);

    public SmartEnumThen<TEnum, TValue> When(params ISmartEnum[] smartEnums)
        => new SmartEnumThen<TEnum, TValue>
            (isMatch: smartEnums.Contains(_smartEnum), stopEvaluating: _stopEvaluating, smartEnum: _smartEnum);

    public SmartEnumThen<TEnum, TValue> When(IEnumerable<ISmartEnum> smartEnums)
        => new SmartEnumThen<TEnum, TValue>
            (isMatch: smartEnums.Contains(_smartEnum), stopEvaluating: _stopEvaluating, smartEnum: _smartEnum);
}