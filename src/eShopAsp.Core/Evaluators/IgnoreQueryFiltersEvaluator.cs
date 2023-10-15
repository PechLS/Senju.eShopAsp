using eShopAsp.Core.Interfaces;
using eShopAsp.Core.Interfaces.Evaluators;
using Microsoft.EntityFrameworkCore;

namespace eShopAsp.Core.Evaluators;

public class IgnoreQueryFiltersEvaluator : IEvaluator
{
    private IgnoreQueryFiltersEvaluator(){}
    public static IgnoreQueryFiltersEvaluator Instance { get; } = new();
    
    public bool IsCriteriaEvaluator { get; } = true;
    public IQueryable<T> GetQuery<T>(IQueryable<T> query, ISpecification<T> specification) where T : class
    {
        if (specification.IgnoreQueryFilters) query = query.IgnoreQueryFilters();
        return query;
    }
}