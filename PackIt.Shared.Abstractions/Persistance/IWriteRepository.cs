using PackIT.Domain.Common;

namespace PackIt.Shared.Abstractions.Persistance
{
    public interface IWriteRepository<TEntity, TId> where TEntity : class, IEntity<TId> where TId : IEquatable<TId>
    {
        Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);

    }

}

