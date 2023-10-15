namespace eShopAsp.Core.Interfaces;

public interface IRepositoryFactory<TRepository>
{
    public TRepository CreateNewRepository();
}