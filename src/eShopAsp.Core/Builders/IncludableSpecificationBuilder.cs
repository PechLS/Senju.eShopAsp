using eShopAsp.Core.Interfaces.Builders;

namespace eShopAsp.Core.Builders;

public class IncludableSpecificationBuilder<T, TProperty> : IIncludableSpecificationBuilder<T, TProperty> 
    where T : class
{
    public Specification<T> Specification { get; }
    public bool IsChainDiscarded { get; set; }

    public IncludableSpecificationBuilder(Specification<T> specification, bool isChainDiscarded)
    {
        Specification = specification;
        IsChainDiscarded = isChainDiscarded;
    }
    
    public IncludableSpecificationBuilder(Specification<T> specification) 
        : this(specification, false) {}
}

