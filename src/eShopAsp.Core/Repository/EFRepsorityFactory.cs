using eShopAsp.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace eShopAsp.Core.Repository;

public class EFRepositoryFactory<TRepository, TConcreteRepository, TContext> : IRepositoryFactory<TRepository>
    where TConcreteRepository : TRepository
    where TContext : DbContext
{
    private readonly IDbContextFactory<TContext> _dbContextFactory;

    public EFRepositoryFactory(IDbContextFactory<TContext> dbContextFactory) => _dbContextFactory = dbContextFactory;

    public TRepository CreateNewRepository()
    {
        var args = new object[] { _dbContextFactory.CreateDbContext() };
        return (TRepository)Activator.CreateInstance(typeof(TConcreteRepository), args)!;
    }
}