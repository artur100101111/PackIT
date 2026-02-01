using Microsoft.EntityFrameworkCore;
using PackIt.Application.ItemTypes;
using PackIt.Persistance.EF.Contexts;
using PackIt.Persistance.EF.Shared;
using PackIt.Shared.Abstractions.Shared;
using PackIT.Domain.ItemTypes;

namespace PackIt.Persistance.EF.ItemTypes
{
    internal class ItemTypeWriteRepository : WriteRepository<ItemType, ItemTypeId>, IItemTypeRepository
    {
        public ItemTypeWriteRepository(WriteDbContext writeDbContext): base(writeDbContext) 
        {
        }
        public async Task<bool> CheckIfItemTypeExistsAsync<T>(ISpecyfication<T> existanceSpecyfication, CancellationToken cancellationToken = default) where T : ItemType
        {
            IQueryable<T> query = _writeDbContext.Set<T>();
            query = query.Where(existanceSpecyfication.Criteria);
            var result = await query.AnyAsync(cancellationToken);
            return result;
        }

        public async Task<IEnumerable<T>> GetItemsTypesBySpecyfictionAsync<T>(ISpecyfication<T> filterSpecylfication, CancellationToken cancellationToken = default) where T : ItemType
        {
            IQueryable<T> query = _writeDbContext.Set<T>();
            query = query.Where(filterSpecylfication.Criteria);
            foreach (var include in filterSpecylfication.Includes)
            { 
               query =  query.Include(include);
            }

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<T?> GetItemTypeBySpecyfictionAsync<T>(ISpecyfication<T> filterSpecylfication, CancellationToken cancellationToken = default) where T : ItemType
        {
            IQueryable<T> query = _writeDbContext.Set<T>();
            query = query.Where(filterSpecylfication.Criteria);

            foreach (var include in filterSpecylfication.Includes)
            {
                query = query.Include(include);
            }
            return await query.FirstOrDefaultAsync(cancellationToken);
        }
    }
}
