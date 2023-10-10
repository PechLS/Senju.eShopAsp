using eShopAsp.Core.Interfaces;
using eShopAsp.Core.Interfaces.Evaluators;

namespace eShopAsp.Core.Evaluators;

public class PaginationEvaluator : IEvaluator, IInMemoryEvaluator
{
    private PaginationEvaluator(){}
    public static PaginationEvaluator Instance { get; } = new();
    
    public bool IsCriteriaEvaluator { get; } = false;
    
    public IQueryable<T> GetQuery<T>(IQueryable<T> query, ISpecification<T> specification) where T : class
    {
        // Is skip = 0, avoid adding to the IQueryable. It will generate more optimized SQL that way
        if (specification.Skip != null && specification.Skip != 0) 
            query = query.Skip(specification.Skip.Value);
        if (specification.Take != null) 
            query = query.Take(specification.Take.Value);
        return query;
    }

    public IEnumerable<T> Evaluate<T>(IEnumerable<T> query, ISpecification<T> specification)
    {
        if (specification.Skip != null && specification.Skip != 0)
            query = query.Skip(specification.Skip.Value);
        if (specification.Take != null)
            query = query.Take(specification.Take.Value);
        return query;
    }
}