namespace eShopAsp.Core.Interfaces.Validators;

public interface IValidator
{
    bool IsValid<T>(T entity, ISpecification<T> specification);
}