using eShopAsp.Core.Interfaces;
using eShopAsp.Core.Interfaces.Validators;

namespace eShopAsp.Core.Validators;

public class WhereValidator : IValidator
{
    private WhereValidator(){}
    public static WhereValidator Instance { get; } = new();
    
    public bool IsValid<T>(T entity, ISpecification<T> specification)
    {
        foreach (var info in specification.WhereExpressions)
        {
            if (info.FilterFunc(entity) == false) return false;
        }

        return true;
    }
}