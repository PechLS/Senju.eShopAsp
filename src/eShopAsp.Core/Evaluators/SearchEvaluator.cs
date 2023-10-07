using eShopAsp.Core.Extensions;
using eShopAsp.Core.Interfaces;
using eShopAsp.Core.Interfaces.Evaluators;

namespace eShopAsp.Core.Evaluators;

public class SearchEvaluator : IInMemoryEvaluator
{
    private SearchEvaluator(){}
    public static SearchEvaluator Instance { get; } = new();

    public bool IsCriteriaEvaluator { get; } = false;
    
    public IEnumerable<T> Evaluate<T>(IEnumerable<T> query, ISpecification<T> specification)
    {
        if (specification.SearchCriterias != null)
            foreach (var searchGroup in specification.SearchCriterias.GroupBy(x => x.SearchGroup))
            {
                query = query.Where(x => searchGroup.Any(c => c.SelectorFunc(x).Like(c.SearchTerm)));
            }

        return query;
    }
}