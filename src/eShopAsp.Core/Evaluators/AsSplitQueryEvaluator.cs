using eShopAsp.Core.Interfaces;
using eShopAsp.Core.Interfaces.Evaluators;
using Microsoft.EntityFrameworkCore;

namespace eShopAsp.Core.Evaluators;

public class AsSplitQueryEvaluator : IEvaluator
{
    private AsSplitQueryEvaluator(){}
    public static AsSplitQueryEvaluator Instance { get; } = new();
    public bool IsCriteriaEvaluator { get; } = true;
    public IQueryable<T> GetQuery<T>(IQueryable<T> query, ISpecification<T> specification) where T : class
    {
        if (specification.AsSplitQuery) query = query.AsSplitQuery();
        return query;
    }
}