using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using eShopAsp.Core.Extensions.SmartEnum;
using eShopAsp.Core.Interfaces;
using eShopAsp.Core.SmartEnum.Core;

namespace eShopAsp.Core.SmartEnum;

public abstract class SmartFlagEnum<TEnum> :
    SmartFlagEnum<TEnum, int>
    where TEnum : SmartFlagEnum<TEnum, int>
{
    protected SmartFlagEnum(string name, int value) : base(name, value) {}
}

public abstract class SmartFlagEnum<TEnum, TValue> :
    SmartFlagEngine<TEnum, TValue>,
    ISmartEnum,
    IEquatable<SmartFlagEnum<TEnum, TValue>>,
    IComparable<SmartFlagEnum<TEnum, TValue>>
    where TEnum : SmartFlagEnum<TEnum, TValue>
    where TValue : IEquatable<TValue>, IComparable<TValue>
{
    private readonly string _name;
    private readonly TValue _value;
    public string Name => _name;
    public TValue Value => _value;

    protected SmartFlagEnum(string name, TValue? value)
    {
        if (String.IsNullOrEmpty(name)) ThrowHelper.ThrowArgumentNullOrEmptyException(nameof(name));
        if (value == null) ThrowHelper.ThrowArgumentNullException(nameof(value));
        _name = name;
        _value = value;
    }

    static readonly Lazy<Dictionary<string, TEnum>> _fromName =
        new(() => GetAllOptions().ToDictionary(item => item.Name));

    static readonly Lazy<Dictionary<string, TEnum>> _fromNameIgnoreCase =
        new(() => GetAllOptions().ToDictionary(item => item.Name, StringComparer.OrdinalIgnoreCase));
    
    private static IEnumerable<TEnum> GetAllOptions()
    {
        Type baseType = typeof(TEnum);
        IEnumerable<Type> enumTypes = Assembly.GetAssembly(baseType)
            .GetTypes().Where(t => baseType.IsAssignableFrom(t));
        List<TEnum> options = new();
        foreach (var enumType in enumTypes)
        {
            List<TEnum> typeEnumOptions = enumType.GetFieldsOfType<TEnum>();
            options.AddRange(typeEnumOptions);
        }

        return options.OrderBy(t => t.Value);
    }

    public static IReadOnlyCollection<TEnum> List => _fromName.Value.Values.ToList().AsReadOnly();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryFromName(string names, out IEnumerable<TEnum> result)
        => TryFromName(names, false, out result);
    
    public static bool TryFromName(string names, bool ignoreCase, out IEnumerable<TEnum> result)
    {
        if (String.IsNullOrEmpty(names))
            ThrowHelper.ThrowArgumentNullOrEmptyException(nameof(names));
        if (ignoreCase)
            return _fromNameIgnoreCase.Value.TryGetFlagEnumValuesByName<TEnum, TValue>(names, out result);
        else
            return _fromName.Value.TryGetFlagEnumValuesByName<TEnum, TValue>(names, out result);
    }
    
    public static IEnumerable<TEnum> FromValue(TValue? value)
    {
        if (value == null) 
            ThrowHelper.ThrowArgumentNullException(nameof(value));
        if (GetFlagEnumValues(value, GetAllOptions()) == null)
            ThrowHelper.ThrowValueNotFoundException<TEnum, TValue>(value);
        return GetFlagEnumValues(value, GetAllOptions());
    }

    public static TEnum? DeserializeValue(TValue value)
    {
        var enumList = GetAllOptions();
        foreach (var smartFlagEnum in enumList)
        {
            if (smartFlagEnum.Value.Equals(value))
                return smartFlagEnum;
        }
        ThrowHelper.ThrowValueNotFoundException<TEnum, TValue>(value);
        return null;
    }
    
    
    public static IEnumerable<TEnum> FromValue(TValue? value, IEnumerable<TEnum> defaultValue)
    {
        if (value == null) ThrowHelper.ThrowArgumentNullException(nameof(value));
        return !TryFromValue(value, out var result) ? defaultValue : result;
    }
    
    public static bool TryFromValue(TValue? value, out IEnumerable<TEnum> result)
    {
        if (value == null || !int.TryParse(value.ToString(), out _))
        {
            result = default;
            return false;
        }

        result = GetFlagEnumValues(value, GetAllOptions());
        if (result == null) return false;
        return true;
    }
    
    public static string FromValueToString(TValue value)
    {
        if (!TryFromValue(value, out _))
            ThrowHelper.ThrowValueNotFoundException<TEnum, TValue>(value);
        return FormatEnumListString(GetFlagEnumValues(value, GetAllOptions()));
    }
    
    public static bool TryFromValueToString(TValue value, out string result)
    {
        if (!TryFromValue(value, out var enumResult))
        {
            result = default;
            return false;
        }

        result = FormatEnumListString(GetFlagEnumValues(value, enumResult));
        return true;
    }
    
    private static string FormatEnumListString(IEnumerable<TEnum> enumListInput)
    {
        var enumList = enumListInput.ToList();
        var sb = new StringBuilder();
        foreach (var smartFlagEnum in enumList)
        {
            sb.Append(smartFlagEnum.Name);
            if (enumList.Last().Name != smartFlagEnum.Name && enumList.Count > 1)
                sb.Append(", ");
        }

        return sb.ToString();
    }
    
    public override string ToString() => _name;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode() => _value.GetHashCode();
    
    public override bool Equals(object? obj)
        => (obj is SmartFlagEnum<TEnum, TValue> other) && Equals(other);
    
    public virtual bool Equals(SmartFlagEnum<TEnum, TValue>? other)
    {
        // check if same instance
        if (Object.ReferenceEquals(this, other)) return true;
        if (other is null) return false;
        return _value.Equals(other._value);
    }
    
    public SmartEnumThen<TEnum, TValue> When(SmartFlagEnum<TEnum, TValue> smartFlagEnumWhen)
        => new (this.Equals(smartFlagEnumWhen), false, this);

    public SmartEnumThen<TEnum, TValue> When(SmartFlagEnum<TEnum, TValue>[] smartEnums)
        => new(smartEnums.Contains(this), false, this);

    public SmartEnumThen<TEnum, TValue> When(IEnumerable<SmartFlagEnum<TEnum, TValue>> smartEnums)
        => new(smartEnums.Contains(this), false, this);
    
    public static bool operator ==(SmartFlagEnum<TEnum, TValue>? left, SmartFlagEnum<TEnum, TValue>? right)
    {
        if (left is null) return right is null;
        return left.Equals(right);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(SmartFlagEnum<TEnum, TValue> left, SmartFlagEnum<TEnum, TValue> right)
        => !(left == right);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual int CompareTo(SmartFlagEnum<TEnum, TValue> other) => _value.CompareTo(other.Value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(SmartFlagEnum<TEnum, TValue> left, SmartFlagEnum<TEnum, TValue> right)
        => left.CompareTo(right) < 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(SmartFlagEnum<TEnum, TValue> left, SmartFlagEnum<TEnum, TValue> right)
        => left.CompareTo(right) > 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(SmartFlagEnum<TEnum, TValue> left, SmartFlagEnum<TEnum, TValue> right)
        => left.CompareTo(right) <= 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(SmartFlagEnum<TEnum, TValue> left, SmartFlagEnum<TEnum, TValue> right)
        => left.CompareTo(right) >= 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator TValue(SmartFlagEnum<TEnum, TValue> smartFlagEnum)
        => smartFlagEnum._value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator SmartFlagEnum<TEnum, TValue>(TValue value)
        => FromValue(value).First();
}