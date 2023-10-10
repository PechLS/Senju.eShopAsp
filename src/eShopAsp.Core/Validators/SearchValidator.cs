using eShopAsp.Core.Extensions;
using eShopAsp.Core.Interfaces;
using eShopAsp.Core.Interfaces.Validators;

namespace eShopAsp.Core.Validators;

public class SearchValidator : IValidator
{
    private SearchValidator() {}
    public static SearchValidator Instance { get; } = new();
    
    public bool IsValid<T>(T entity, ISpecification<T> specification)
    {
        foreach (var searchGroup in specification.SearchCriterias.GroupBy(x => x.SearchGroup))
        {
            if (searchGroup.Any(c => c.SelectorFunc(entity).Like(c.SearchTerm)) == false) return false;
        }

        return true;
    }
}