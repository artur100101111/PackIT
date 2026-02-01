using Microsoft.EntityFrameworkCore;
using PackIt.Application.Items;
using PackIt.Persistance.EF.Contexts;
using PackIt.Persistance.EF.Shared;
using PackIt.Shared.Abstractions.Shared;
using PackIT.Domain.Items;

namespace PackIt.Persistance.EF.Items
{
    internal class ItemWriteRepository : WriteRepository<Item, ItemId>, IItemRepository
    {
        private readonly WriteDbContext _writeDbContexty;

        public ItemWriteRepository(WriteDbContext writeDbContext): base(writeDbContext)
        {
            _writeDbContexty = writeDbContext;
        }

        public async Task<bool> CheckIfItemExistsAsync<T>(ISpecyfication<T> existanceSpecyfication, CancellationToken cancellationToken = default) where T : Item
        {
            IQueryable<T> query = _writeDbContext.Set<T>();
            query = query.Where(existanceSpecyfication.Criteria);
            return  await query.AnyAsync(cancellationToken);
        }

        public async Task<T?> GetItemBySpecyfictionAsync<T>(ISpecyfication<T> filterSpecylfication, CancellationToken cancellationToken = default) where T : Item
        {
            IQueryable<T> query = _writeDbContext.Set<T>();
            query = query.Where(filterSpecylfication.Criteria);
            foreach (var include in filterSpecylfication.Includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IEnumerable<T>> GetItemsBySpecyfictionAsync<T>(ISpecyfication<T> filterSpecylfication, CancellationToken cancellationToken = default) where T : Item
        {
            IQueryable<T> query = _writeDbContext.Set<T>();

            query = query.Where(filterSpecylfication.Criteria);

            foreach (var include in filterSpecylfication.Includes)
            {
                query = query.Include(include);
            }
            return await query.ToListAsync(cancellationToken);
        }
    }
}
