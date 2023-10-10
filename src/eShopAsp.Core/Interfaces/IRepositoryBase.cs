namespace eShopAsp.Core.Interfaces;


/// <summary>
/// <para>
/// A <see cref="IRepositoryBase{T}"/> can be used to query and save instances of <typeparamref name="T"/>
/// An <see cref="ISpecification{T}"/> (or derived) is used to encapsulate the LINQ queries against the database.
/// </para>
/// </summary>
/// <typeparam name="T">The type of the entity being operated on by this repository</typeparam>
public interface IRepositoryBase<T> : IReadRepositoryBase<T> where T : class
{
    Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);

    Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

    Task UpdateAsync(T entity, CancellationToken cancellationToken = default);

    Task UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

    Task DeleteAsync(T entity, CancellationToken cancellationToken = default);

    Task DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Removes the all entities of <typeparamref name="T"/>, that matches the encapsulated query logic
    /// of the <paramref name="specification"/>, from the database.
    /// </summary>
    Task DeleteRangeAsync(ISpecification<T> specification, CancellationToken cancellationToken = default);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}