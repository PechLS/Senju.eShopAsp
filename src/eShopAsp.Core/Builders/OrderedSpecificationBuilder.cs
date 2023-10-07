using eShopAsp.Core.Interfaces.Builders;

namespace eShopAsp.Core.Builders;

public class OrderedSpecificationBuilder<T> : IOrderedSpecificationBuilder<T>
{
    public Specification<T> Specification { get; }
    public bool IsChainDiscarded { get; set; }

    public OrderedSpecificationBuilder(Specification<T> specification, bool isChainDiscarded)
    {
        Specification = specification;
        IsChainDiscarded = isChainDiscarded;
    }
    
    public OrderedSpecificationBuilder(Specification<T> specification)
        : this(specification, false) {}
}