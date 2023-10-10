using eShopAsp.Core.Interfaces;
using eShopAsp.Core.Interfaces.Validators;

namespace eShopAsp.Core.Validators;

public class SpecificationValidator : ISpecificationValidator
{

    public static SpecificationValidator Instance { get; } = new();

    private readonly List<IValidator> _validators = new();

    public SpecificationValidator()
    {
        _validators.AddRange(new IValidator[]
        {
            WhereValidator.Instance, 
            SearchValidator.Instance
        });
    }

    public SpecificationValidator(IEnumerable<IValidator> validators) => _validators.AddRange(validators);
    
    public bool IsValid<T>(T entity, ISpecification<T> specification)
    {
        foreach (var partialValidator in _validators)
        {
            if (partialValidator.IsValid(entity, specification) == false) return false;
        }

        return true;
    }
}