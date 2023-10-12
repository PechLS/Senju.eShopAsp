namespace eShopAsp.Core.SmartEnum;


[AttributeUsage(AttributeTargets.Class)]
public class SmartEnumComparerAttribute<T> : Attribute
{
    private readonly IEqualityComparer<T> _comparer;
    public SmartEnumComparerAttribute(IEqualityComparer<T> comparer) => _comparer = comparer;
    public IEqualityComparer<T> Comparer => _comparer;
    
}

public class SmartEnumStringComparerAttribute : SmartEnumComparerAttribute<string>
{
    public SmartEnumStringComparerAttribute(StringComparison stringComparison) 
        : base(GetComparer(stringComparison)){}

    private static IEqualityComparer<string> GetComparer(StringComparison comparison)
    {
        switch (comparison)
        {
            case StringComparison.Ordinal: return StringComparer.Ordinal;
            case StringComparison.OrdinalIgnoreCase: return StringComparer.OrdinalIgnoreCase;
            case StringComparison.CurrentCulture: return StringComparer.CurrentCulture;
            case StringComparison.CurrentCultureIgnoreCase: return StringComparer.CurrentCultureIgnoreCase;
            case StringComparison.InvariantCultureIgnoreCase: return StringComparer.InvariantCultureIgnoreCase;
            case StringComparison.InvariantCulture: return StringComparer.InvariantCulture;
        }

        throw new ArgumentException($"StringComparison {nameof(comparison)} is not supported.");
    }
}