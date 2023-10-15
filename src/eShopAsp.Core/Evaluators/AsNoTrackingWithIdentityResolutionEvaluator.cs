using eShopAsp.Core.Interfaces;
using eShopAsp.Core.Interfaces.Evaluators;
using Microsoft.EntityFrameworkCore;

namespace eShopAsp.Core.Evaluators;

public class AsNoTrackingWithIdentityResolutionEvaluator : IEvaluator
{
    private AsNoTrackingWithIdentityResolutionEvaluator(){}
    public static AsNoTrackingWithIdentityResolutionEvaluator Instance { get; } = new();

    public bool IsCriteriaEvaluator { get; } = true;
    public IQueryable<T> GetQuery<T>(IQueryable<T> query, ISpecification<T> specification) where T : class
    {
        if (specification.AsNoTrackingWithIdentityResolution) query = query.AsNoTrackingWithIdentityResolution();
        return query;
    }
}