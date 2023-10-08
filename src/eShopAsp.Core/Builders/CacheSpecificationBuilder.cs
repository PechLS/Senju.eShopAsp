using eShopAsp.Core.Interfaces.Builders;

namespace eShopAsp.Core.Builders;

public class CacheSpecificationBuilder<T> : ICacheSpecificationBuilder<T> where T : class
{
    public Specification<T> Specification { get; }
    public bool IsChainDiscarded { get; set; }

    public CacheSpecificationBuilder(Specification<T> specification, bool isChainDiscarded)
    {
        Specification = specification;
        IsChainDiscarded = isChainDiscarded;
    }
    
    public CacheSpecificationBuilder(Specification<T> specification) : this(specification, false){}
}