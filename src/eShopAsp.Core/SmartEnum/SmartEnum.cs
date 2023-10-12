using System.Reflection;
using System.Runtime.CompilerServices;
using eShopAsp.Core.Extensions.SmartEnum;
using eShopAsp.Core.Interfaces;
using eShopAsp.Core.SmartEnum.Core;

namespace eShopAsp.Core.SmartEnum;

public abstract class SmartEnum<TEnum> : SmartEnum<TEnum, int> where TEnum : SmartEnum<TEnum, int>
{
    protected SmartEnum(string name, int value) : base(name, value) {}
}

public abstract class SmartEnum<TEnum, TValue> :
    ISmartEnum,
    IEquatable<SmartEnum<TEnum, TValue>>,
    IComparable<SmartEnum<TEnum, TValue>>
    where TEnum : SmartEnum<TEnum, TValue>
    where TValue : IEquatable<TValue>, IComparable<TValue>
{

    private readonly string _name;
    private readonly TValue _value;

    protected SmartEnum(string name, TValue value)
    {
        if (String.IsNullOrEmpty(name)) 
            ThrowHelper.ThrowArgumentNullOrEmptyException(nameof(name));
        _name = name;
        _value = value;
    }

    public string Name => _name;
    public TValue Value => _value;

    static readonly Lazy<TEnum[]> _enumOptions = new(GetAllOptions, LazyThreadSafetyMode.ExecutionAndPublication);

    static readonly Lazy<Dictionary<string, TEnum>> _fromName = new(() 
        => _enumOptions.Value.ToDictionary(item => item.Name));

    static readonly Lazy<Dictionary<string, TEnum>> _fromNameIgnoreCase = new(()
        => _enumOptions.Value.ToDictionary(item => item.Name, StringComparer.OrdinalIgnoreCase));

    static readonly Lazy<Dictionary<TValue, TEnum>> _fromValue = new(() =>
    {
        var dictionary = new Dictionary<TValue, TEnum>(GetValueComparer());
        foreach (var item in _enumOptions.Value)
        {
            if (item._value != null && !dictionary.ContainsKey(item._value))
                dictionary.Add(item._value, item);
        }

        return dictionary;
    });
    
    private static TEnum[] GetAllOptions()
    {
        Type baseType = typeof(TEnum);
        return Assembly.GetAssembly(baseType)
            .GetTypes()
            .Where(t => baseType.IsAssignableFrom(t))
            .SelectMany(t => t.GetFieldsOfType<TEnum>())
            .OrderBy(t => t.Name)
            .ToArray();
    }

    private static IEqualityComparer<TValue>? GetValueComparer()
    {
        var comparer = typeof(TEnum).GetCustomAttribute<SmartEnumComparerAttribute<TValue>>();
        return comparer?.Comparer;
    }

    public static IReadOnlyCollection<TEnum> List => _fromName.Value.Values.ToList().AsReadOnly();


    public static TEnum FromName(string name, bool ignoreCase = false)
    {
        if (String.IsNullOrEmpty(name)) ThrowHelper.ThrowArgumentNullOrEmptyException(nameof(name));
        if (ignoreCase) return FromName(_fromNameIgnoreCase.Value);
        else return FromName(_fromName.Value);

        TEnum FromName(Dictionary<string, TEnum> dictionary)
        {
            if (!dictionary.TryGetValue(name, out var result))
                ThrowHelper.ThrowNameNotFoundException<TEnum, TValue>(name);
            return result;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryFromName(string name, out TEnum result) => TryFromName(name, false, out result);

    public static bool TryFromName(string name, bool ignoreCase, out TEnum result)
    {
        if (String.IsNullOrEmpty(name))
        {
            result = default;
            return false;
        }

        if (ignoreCase) return _fromNameIgnoreCase.Value.TryGetValue(name, out result);
        else return _fromName.Value.TryGetValue(name, out result);
    }
    
    public static TEnum FromValue(TValue? value)
    {
        TEnum result;
        if (value != null)
        {
            if (!_fromValue.Value.TryGetValue(value, out result))
                ThrowHelper.ThrowValueNotFoundException<TEnum, TValue>(value);
        }
        else
        {
            result = _enumOptions.Value.FirstOrDefault(x => x.Value == null);
            if (result == null) ThrowHelper.ThrowValueNotFoundException<TEnum, TValue>(value);
        }

        return result;
    }
    
    public static TEnum FromValue(TValue? value, TEnum defaultValue)
    {
        if (value == null) ThrowHelper.ThrowArgumentNullException(nameof(value));
        if (!_fromValue.Value.TryGetValue(value, out var result)) return defaultValue;
        return result;
    }

    public static bool TryFromValue(TValue? value, out TEnum result)
    {
        if (value == null)
        {
            result = default;
            return false;
        }

        return _fromValue.Value.TryGetValue(value, out result);
    }
    
    public override string ToString() => _name;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode() => _value.GetHashCode();

    public override bool Equals(object? obj) => (obj is SmartEnum<TEnum, TValue> other) && Equals(other);

    public virtual bool Equals(SmartEnum<TEnum, TValue>? other)
    {
        if (Object.ReferenceEquals(this, other)) return true;
        if (other is null) return false;
        return _value.Equals(other._value);
    }

    public SmartEnumThen<TEnum, TValue> When(SmartEnum<TEnum, TValue> smartEnumWhen)
        => new(this.Equals(smartEnumWhen), false, this);

    public SmartEnumThen<TEnum, TValue> When(params SmartEnum<TEnum, TValue>[] smartEnums)
        => new(smartEnums.Contains(this), false, this);

    public SmartEnumThen<TEnum, TValue> When(IEnumerable<SmartEnum<TEnum, TValue>> smartEnums)
        => new(smartEnums.Contains(this), false, this);

    public static bool operator ==(SmartEnum<TEnum, TValue>? left, SmartEnum<TEnum, TValue>? right)
    {
        if (left is null) return right is null;
        return left.Equals(right);
    }

    public static bool operator !=(SmartEnum<TEnum, TValue> left, SmartEnum<TEnum, TValue> right)
        => !(left == right);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual int CompareTo(SmartEnum<TEnum, TValue> other) => _value.CompareTo(other._value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(SmartEnum<TEnum, TValue> left, SmartEnum<TEnum, TValue> right) => left.CompareTo(right) < 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(SmartEnum<TEnum, TValue> left, SmartEnum<TEnum, TValue> right) => left.CompareTo(right) <= 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(SmartEnum<TEnum, TValue> left, SmartEnum<TEnum, TValue> right) => left.CompareTo(right) > 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(SmartEnum<TEnum, TValue> left, SmartEnum<TEnum, TValue> right) => left.CompareTo(right) >= 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator TValue(SmartEnum<TEnum, TValue> smartEnum)
        => smartEnum is not null
            ? smartEnum._value
            : default;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator SmartEnum<TEnum, TValue>(TValue value) => FromValue(value);
}