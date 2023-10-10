namespace eShopAsp.Core.Interfaces;


/// <summary>
/// <para>
/// A <see cref="IRepositoryBase{T}" /> can be used to query instances of <typeparamref name="T" />.
/// An <see cref="ISpecification{T}"/> (or derived) is used to encapsulate the LINQ queries against the database.
/// </para>
/// </summary>
/// <typeparam name="T">The type of entity being operated on by this repository.</typeparam>
public interface IReadRepositoryBase<T> where T : class
{
    Task<T?> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull;

    [Obsolete("Use FirstOrDefaultAsync<T> or SingleOrDefaultAsync<T> instead. " +
              "The SingleOrDefaultAsync<T> can be applied only to SingleResultSpecification<T> specifications.")]
    Task<T?> GetBySpecAsync(ISpecification<T> specification, CancellationToken cancellationToken = default);


    [Obsolete("Use FirstOrDefaultAsync<T> or SingleOrDefaultAsync<T> instead. " +
              "The SingleOrDefaultAsync<T> can be applied only to SingleResultSpecification<T> specifications.")]
    Task<TResult?> GetBySpecAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default);

    Task<T?> FirstOrDefaultAsync(ISpecification<T> specification, CancellationToken cancellationToken = default);

    Task<TResult?> FirstOrDefaultAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default);

    Task<T?> SingleOrDefaultAsync(ISpecification<T> specification, CancellationToken cancellationToken = default);

    Task<TResult?> SingleOrDefaultAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default);

    Task<List<T>> ListAsync(CancellationToken cancellationToken = default);

    Task<List<T>> ListAsync(ISpecification<T> specification, CancellationToken cancellationToken = default);
    
    Task<List<TResult>> ListAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default);

    Task<int> CountAsync(CancellationToken cancellationToken = default);

    Task<int> CountAsync(ISpecification<T> specification, CancellationToken cancellationToken = default);

    Task<bool> AnyAsync(CancellationToken cancellationToken = default);

    Task<bool> AnyAsync(ISpecification<T> specification, CancellationToken cancellationToken = default);

    IAsyncEnumerable<T> AsAsyncEnumerable(ISpecification<T> specification);
}