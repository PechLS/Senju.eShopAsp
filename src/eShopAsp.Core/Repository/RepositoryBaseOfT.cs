using eShopAsp.Core.Evaluators;
using eShopAsp.Core.Interfaces;
using eShopAsp.Core.Interfaces.Evaluators;
using Microsoft.EntityFrameworkCore;

namespace eShopAsp.Core.Repository;

public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    private readonly DbContext _dbContext;
    private readonly ISpecificationEvaluator _specificationEvaluator;

    public RepositoryBase(DbContext dbContext, ISpecificationEvaluator specificationEvaluator)
    {
        _dbContext = dbContext;
        _specificationEvaluator = specificationEvaluator;
    }
    
    public RepositoryBase(DbContext dbContext) : this(dbContext, SpecificationEvaluator.Default){}

    protected virtual IQueryable<T> ApplySpecification(
        ISpecification<T> specification,
        bool evaluateCriterialOnly = false)
        => _specificationEvaluator.GetQuery(_dbContext.Set<T>().AsQueryable(), specification, evaluateCriterialOnly);

    protected virtual IQueryable<TResult> ApplySpecification<TResult>(ISpecification<T, TResult> specification)
        => _specificationEvaluator.GetQuery(_dbContext.Set<T>().AsQueryable(), specification);


    public virtual async Task<T?> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull
        => await _dbContext.Set<T>().FindAsync(new object[] { id }, cancellationToken);

    [Obsolete("Use FirstOrDefaultAsync<T> or SingleOrDefaultAsync<T> instead. The SingleOrDefaultAsync<T> can be applied only to SingleResultSpecification<T> specifications.")]
    public virtual async Task<T?> GetBySpecAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
        => await ApplySpecification(specification).FirstOrDefaultAsync(cancellationToken);

    [Obsolete("Use FirstOrDefaultAsync<T> or SingleOrDefaultAsync<T> instead. The SingleOrDefaultAsync<T> can be applied only to SingleResultSpecification<T> specifications.")]
    public virtual async Task<TResult?> GetBySpecAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default)
        => await ApplySpecification(specification).FirstOrDefaultAsync(cancellationToken);

    public virtual async Task<T?> FirstOrDefaultAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
        => await ApplySpecification(specification).FirstOrDefaultAsync(cancellationToken);

    public virtual async Task<TResult?> FirstOrDefaultAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default)
        => await ApplySpecification(specification).FirstOrDefaultAsync(cancellationToken);

    public virtual async Task<T?> SingleOrDefaultAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
        => await ApplySpecification(specification).SingleOrDefaultAsync(cancellationToken);

    public virtual async Task<TResult?> SingleOrDefaultAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default)
        => await ApplySpecification(specification).SingleOrDefaultAsync(cancellationToken);

    public virtual async Task<List<T>> ListAsync(CancellationToken cancellationToken = default)
        => await _dbContext.Set<T>().ToListAsync(cancellationToken);

    public virtual async Task<List<T>> ListAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
    {
        var queryResult = await ApplySpecification(specification).ToListAsync(cancellationToken);
        return specification.PostProcessingAction == null
            ? queryResult
            : specification.PostProcessingAction(queryResult).ToList();
    }

    public virtual async Task<List<TResult>> ListAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default)
    {
        var queryResult = await ApplySpecification(specification).ToListAsync(cancellationToken);
        return specification.PostProcessingAction == null
            ? queryResult
            : specification.PostProcessingAction(queryResult).ToList();
    }

    public virtual async Task<int> CountAsync(CancellationToken cancellationToken = default)
        => await _dbContext.Set<T>().CountAsync(cancellationToken);

    public virtual async Task<int> CountAsync(ISpecification<T> specification,
        CancellationToken cancellationToken = default)
        => await ApplySpecification(specification, true).CountAsync(cancellationToken);

    public virtual async Task<bool> AnyAsync(CancellationToken cancellationToken = default)
        => await _dbContext.Set<T>().AnyAsync(cancellationToken);

    public virtual async Task<bool> AnyAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
        => await ApplySpecification(specification).AnyAsync(cancellationToken);

    public virtual IAsyncEnumerable<T> AsAsyncEnumerable(ISpecification<T> specification)
        => ApplySpecification(specification).AsAsyncEnumerable();

    public virtual async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        _dbContext.Set<T>().Add(entity);
        await SaveChangesAsync(cancellationToken);
        return entity;
    }

    public virtual async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        _dbContext.Set<T>().AddRange(entities);
        await SaveChangesAsync(cancellationToken);
        return entities;
    }

    public virtual async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        _dbContext.Set<T>().Update(entity);
        await SaveChangesAsync(cancellationToken);
    }

    public virtual async Task UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        _dbContext.Set<T>().UpdateRange(entities);
        await SaveChangesAsync(cancellationToken);
    }

    public virtual async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        _dbContext.Set<T>().Remove(entity);
        await SaveChangesAsync(cancellationToken);
    }

    public virtual async Task DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        _dbContext.Set<T>().RemoveRange(entities);
        await SaveChangesAsync(cancellationToken);
    }

    public virtual async Task DeleteRangeAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
    {
        var query = ApplySpecification(specification);
        _dbContext.Set<T>().RemoveRange(query);
        await SaveChangesAsync(cancellationToken);
    }

    public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => await _dbContext.SaveChangesAsync(cancellationToken);
}