using eShopAsp.Core.Interfaces;
using eShopAsp.Core.Interfaces.Evaluators;

namespace eShopAsp.Core.Evaluators;

public class WhereEvaluator : IEvaluator, IInMemoryEvaluator
{
    private WhereEvaluator(){}
    public static WhereEvaluator Instance { get; } = new();

    public bool IsCriteriaEvaluator { get; } = true;
    
    public IQueryable<T> GetQuery<T>(IQueryable<T> query, ISpecification<T> specification) where T : class
    {
        if (specification.WhereExpressions != null)
            foreach (var whereExpression in specification.WhereExpressions)
            {
                query = query.Where(whereExpression.Filter);
            }

        return query;
    }

    public IEnumerable<T> Evaluate<T>(IEnumerable<T> query, ISpecification<T> specification)
    {
        if (specification.WhereExpressions != null)
            foreach (var whereExpression in specification.WhereExpressions)
            {
                query = query.Where(whereExpression.FilterFunc);
            }

        return query;
    }
    
}