namespace eShopAsp.Core;

[Serializable]
public abstract class ValueObject : IComparable, IComparable<ValueObject>
{
    private int? _cachedHashCode;

    protected abstract IEnumerable<object> GetEqualityComponents();

    private static int CompareComponents(object? obj1, object? obj2)
    {
        if (obj1 is null && obj2 is null) return 0;
        if (obj1 is null) return -1;
        if (obj2 is null) return 1;
        if (obj1 is IComparable comparable1 && obj2 is IComparable comparable2)
            return comparable1.CompareTo(comparable2);
        return obj1.Equals(obj2) ? 0 : -1;
    }

    public int CompareTo(ValueObject? other) => CompareTo(other as object);

    public int CompareTo(object? obj)
    {
        if (obj is null) return 1;
        var thisType = GetUnproxiedType(this);
        var otherType = GetUnproxiedType(obj);
        if (thisType != otherType) string.Compare(thisType.ToString(), otherType.ToString(), StringComparison.Ordinal);
        var other = (ValueObject)obj;
        var components = GetEqualityComponents().ToArray();
        var otherComponents = other.GetEqualityComponents().ToArray();
        for (var i = 0; i < components.Length; i++)
        {
            var comparison = CompareComponents(components[i], otherComponents[i]);
            if (comparison != 0) return comparison;
        }

        return 0;
    }
    

    internal static Type GetUnproxiedType(object obj)
    {
        ArgumentNullException.ThrowIfNull(obj);
        const string EFCoreProxyPrefix = "Castle.Proxies.";
        const string NHibernateProxyPostfix = "Proxy";
        var type = obj.GetType();
        var typeString = type.ToString();
        if (typeString.Contains(EFCoreProxyPrefix) || typeString.EndsWith(NHibernateProxyPostfix))
            return type.BaseType!;
        return type;
    }
    
    public override bool Equals(object? obj)
    {
        if (obj == null) return false;
        if (GetUnproxiedType(this) != GetUnproxiedType(obj)) return false;
        var valueObject = (ValueObject)obj;
        return GetEqualityComponents().SequenceEqual(valueObject.GetEqualityComponents());
    }

    public override int GetHashCode()
    {
        if (!_cachedHashCode.HasValue)
        {
            _cachedHashCode = GetEqualityComponents()
                .Aggregate(1, (current, obj) =>
                {
                    unchecked
                    {
                        return current * 23 + (obj?.GetHashCode() ?? 0);
                    }
                });
        }

        return _cachedHashCode.Value;
    }

    public static bool operator ==(ValueObject? a, ValueObject? b)
    {
        if (a is null && b is null) return true;
        if (a is null || b is null) return false;
        return a.Equals(b);
    }
    
    public static bool operator !=(ValueObject a, ValueObject b)
    {
        return !(a == b);
    }
}