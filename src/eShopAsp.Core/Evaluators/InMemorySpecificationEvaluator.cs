using eShopAsp.Core.Interfaces;
using eShopAsp.Core.Interfaces.Evaluators;

namespace eShopAsp.Core.Evaluators;

public class InMemorySpecificationEvaluator : IInMemorySpecificationEvaluator
{
    public static InMemorySpecificationEvaluator Instance { get; } = new();

    protected List<IInMemoryEvaluator> Evaluators { get; } = new();

    public InMemorySpecificationEvaluator(IEnumerable<IInMemoryEvaluator> evaluators) 
        => Evaluators.AddRange(evaluators);

    public InMemorySpecificationEvaluator()
    {
        Evaluators.AddRange(new IInMemoryEvaluator[]
        {
            
        });
    }
    
    public IEnumerable<TResult> Evaluate<T, TResult>(IEnumerable<T> source, ISpecification<T, TResult> specification)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<T> Evaluate<T>(IEnumerable<T> source, ISpecification<T> specification)
    {
        throw new NotImplementedException();
    }
}