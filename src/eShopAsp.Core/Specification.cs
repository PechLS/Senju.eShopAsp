using System.Linq.Expressions;
using eShopAsp.Core.Builders;
using eShopAsp.Core.Evaluators;
using eShopAsp.Core.Expressions;
using eShopAsp.Core.Interfaces;
using eShopAsp.Core.Interfaces.Builders;
using eShopAsp.Core.Interfaces.Evaluators;
using eShopAsp.Core.Interfaces.Validators;
using eShopAsp.Core.Validators;

namespace eShopAsp.Core;

public class Specification<T, TResult> : Specification<T> , ISpecification<T, TResult>
{
    public new virtual ISpecificationBuilder<T, TResult> Query { get; }

    protected Specification() : this(InMemorySpecificationEvaluator.Instance){}
    
    protected Specification(IInMemorySpecificationEvaluator inMemorySpecificationEvaluator)
        : base(inMemorySpecificationEvaluator)
        => Query = new SpecificationBuilder<T, TResult>(this);
    
    public new virtual IEnumerable<TResult> Evaluate(IEnumerable<T> entities)
        => Evaluator.Evaluate(entities, this);
    
    public Expression<Func<T, TResult>>? Selector { get; internal set; }
    public Expression<Func<T, IEnumerable<TResult>>>? SelectorMany { get; internal set; }
    public new Func<IEnumerable<TResult>, IEnumerable<TResult>>? PostProcessingAction { get; internal set; } = null;
}

public class Specification<T> : ISpecification<T>
{
    protected IInMemorySpecificationEvaluator Evaluator { get; }
    protected ISpecificationValidator Validator { get; }
    public virtual ISpecificationBuilder<T> Query { get; }

    protected Specification() 
        : this(InMemorySpecificationEvaluator.Instance, SpecificationValidator.Instance){}
    
    protected Specification(IInMemorySpecificationEvaluator inMemorySpecificationEvaluator)
        : this(inMemorySpecificationEvaluator, SpecificationValidator.Instance){}
    
    protected Specification(ISpecificationValidator specificationValidator)
        : this(InMemorySpecificationEvaluator.Instance, specificationValidator){}
    
    protected Specification(IInMemorySpecificationEvaluator inMemorySpecificationEvaluator,
        ISpecificationValidator specificationValidator)
    {
        Evaluator = inMemorySpecificationEvaluator;
        Validator = specificationValidator;
        Query = new SpecificationBuilder<T>(this);
    }

    public IDictionary<string, object> Items { get; set; } = new Dictionary<string, object>();
    public IEnumerable<WhereExpressionInfo<T>> WhereExpressions { get; } = new List<WhereExpressionInfo<T>>();
    public IEnumerable<OrderExpressionInfo<T>> OrderExpressions { get; } = new List<OrderExpressionInfo<T>>();
    public IEnumerable<IncludeExpressionInfo> IncludeExpressions { get; } = new List<IncludeExpressionInfo>();
    public IEnumerable<string> IncludeStrings { get; } = new List<string>();
    public IEnumerable<SearchExpressionInfo<T>> SearchCriterias { get; } = new List<SearchExpressionInfo<T>>();
    public int? Take { get; internal set; } = null;
    public int? Skip { get; internal set; } = null;
    public Func<IEnumerable<T>, IEnumerable<T>>? PostProcessingAction { get; internal set; } = null;
    public bool CacheEnabled { get; internal set; } 
    public string? CacheKey { get; internal set; }
    public bool AsTracking { get; internal set; } = false;
    public bool AsNoTracking { get; internal set; } = false;
    public bool AsSplitQuery { get; internal set; } = false;
    public bool AsNoTrackingWithIdentityResolution { get; internal set; } = false;
    public bool IgnoreQueryFilters { get; internal set; } = false;


    public virtual IEnumerable<T> Evaluate(IEnumerable<T> entities)
        => Evaluator.Evaluate(entities, this);

    public bool IsSatisfiedBy(T entity)
        => Validator.IsValid(entity, this);
}