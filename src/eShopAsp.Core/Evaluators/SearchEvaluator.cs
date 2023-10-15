using eShopAsp.Core.Extensions;
using eShopAsp.Core.Interfaces;
using eShopAsp.Core.Interfaces.Evaluators;

namespace eShopAsp.Core.Evaluators;

public class SearchEvaluator : IInMemoryEvaluator, IEvaluator
{
    private SearchEvaluator(){}
    public static SearchEvaluator Instance { get; } = new();

    public bool IsCriteriaEvaluator { get; } = true;
    
    public IQueryable<T> GetQuery<T>(IQueryable<T> query, ISpecification<T> specification) where T : class
    {
        foreach (var searchCriteria in specification.SearchCriterias.GroupBy(x => x.SearchGroup))
        {
            query = query.Search(searchCriteria);
        }

        return query;
    }

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