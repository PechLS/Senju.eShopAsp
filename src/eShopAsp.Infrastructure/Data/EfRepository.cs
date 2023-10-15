using eShopAsp.Core.Interfaces;
using eShopAsp.Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace eShopAsp.Infrastructure.Data;

public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
{
    public EfRepository(DbContext dbContext) : base(dbContext)
    {
    }
}