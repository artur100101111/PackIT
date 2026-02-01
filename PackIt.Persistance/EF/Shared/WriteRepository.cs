using PackIt.Persistance.EF.Contexts;
using PackIt.Shared.Abstractions.Persistance;
using PackIT.Domain.Common;

namespace PackIt.Persistance.EF.Shared
{
    internal abstract class WriteRepository<TEntity, TId> : IWriteRepository<TEntity, TId> where TEntity:class, IEntity<TId> where TId : IEquatable<TId>
    {
        protected readonly WriteDbContext _writeDbContext;
        public WriteRepository(WriteDbContext writeDbContext)
        {
            _writeDbContext = writeDbContext;
        }
        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        { 
             await _writeDbContext.Set<TEntity>().AddAsync(entity,cancellationToken);
        }
        public  Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
             _writeDbContext.Set<TEntity>().Remove(entity);
            return  Task.CompletedTask;
        }

        public Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            _writeDbContext.Update(entity);
            return Task.CompletedTask;
        }
    }
}