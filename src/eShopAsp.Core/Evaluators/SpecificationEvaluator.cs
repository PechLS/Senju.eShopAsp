using eShopAsp.Core.Exceptions;
using eShopAsp.Core.Interfaces;
using eShopAsp.Core.Interfaces.Evaluators;

namespace eShopAsp.Core.Evaluators;

public class SpecificationEvaluator : ISpecificationEvaluator
{
    public static SpecificationEvaluator Default { get; } = new();
    public static SpecificationEvaluator Cached { get; } = new(true);

    protected List<IEvaluator> Evaluators { get; } = new();

    public SpecificationEvaluator(bool cacheEnabled = false)
    {
        Evaluators.AddRange(new IEvaluator[]
        {
            WhereEvaluator.Instance, 
            SearchEvaluator.Instance, 
            cacheEnabled ? IncludeEvaluator.Cached : IncludeEvaluator.Default,
            OrderEvaluator.Instance, 
            PaginationEvaluator.Instance, 
            AsNoTrackingEvaluator.Instance, 
            AsNoTrackingWithIdentityResolutionEvaluator.Instance, 
            AsTrackingEvaluator.Instance, 
            IgnoreQueryFiltersEvaluator.Instance, 
            AsSplitQueryEvaluator.Instance
        });
    }

    public SpecificationEvaluator(IEnumerable<IEvaluator> evaluators)
    {
        Evaluators.AddRange(evaluators);
    }

    public virtual IQueryable<TResult> GetQuery<T, TResult>(IQueryable<T> query, ISpecification<T, TResult> specification) where T : class
    {
        if (specification is null) throw new ArgumentNullException(nameof(specification));
        if (specification.Selector is null && specification.SelectorMany is null) throw new SelectNotFoundException();
        if (specification.Selector is not null && specification.SelectorMany is not null) throw new ConcurrentSelectorsException();
        query = GetQuery(query, (ISpecification<T>)specification);
        return specification.Selector is not null
            ? query.Select(specification.Selector)
            : query.SelectMany(specification.SelectorMany!);
    }

    public virtual IQueryable<T> GetQuery<T>(IQueryable<T> query, ISpecification<T> specification, bool evaluateCriteriaOnly = false) where T : class
    {
        if (specification is null) throw new ArgumentNullException(nameof(specification));
        var evaluators = evaluateCriteriaOnly ? Evaluators.Where(x => x.IsCriteriaEvaluator) : Evaluators;
        foreach (var evaluator in evaluators)
        {
            query = evaluator.GetQuery(query, specification);
        }

        return query;
    }
}