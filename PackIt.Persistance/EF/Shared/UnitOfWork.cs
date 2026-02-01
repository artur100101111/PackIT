using PackIt.Persistance.EF.Contexts;
using PackIt.Shared.Abstractions.Persistance;

namespace PackIt.Persistance.EF.Shared
{
    internal sealed class UnitOfWork : IUnitOfWork
    {
        private readonly WriteDbContext _databaseContext;
        //dodać transakcje w Interfejsie -> Begin, Commit, Rollback z wyborem poziomu izolacji.
        //
        public UnitOfWork(WriteDbContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task SaveAsync(CancellationToken cancellationToken = default)
        {
            await _databaseContext.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _databaseContext.Dispose();
        }
    }
}
