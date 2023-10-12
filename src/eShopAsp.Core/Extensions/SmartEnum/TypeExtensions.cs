using System.Reflection;

namespace eShopAsp.Core.Extensions.SmartEnum;

internal static class TypeExtensions
{
    public static List<TFieldType> GetFieldsOfType<TFieldType>(this Type type)
    {
        return type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
            .Where(p => type.IsAssignableFrom(p.FieldType))
            .Select(pi => (TFieldType)pi.GetValue(null))
            .ToList();
    }
}