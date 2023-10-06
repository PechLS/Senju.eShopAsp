namespace eShopAsp.Core.Interfaces.Validators;

public interface ISpecificationValidator
{
    bool IsValid<T>(T entity, ISpecification<T> specification);
}