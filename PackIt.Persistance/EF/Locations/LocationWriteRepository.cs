using Microsoft.EntityFrameworkCore;
using PackIt.Persistance.EF.Contexts;
using PackIt.Persistance.EF.Shared;
using PackIt.Shared.Abstractions.Shared;
using PackIT.Domain.Locations;
using PackIT.Domain.Locations.Repository;

namespace PackIt.Persistance.EF.Locations
{
    internal class LocationWriteRepository : WriteRepository<Location, LocationId>, ILocationRepository
    {
        public LocationWriteRepository(WriteDbContext writeDbContext) : base(writeDbContext)
        {
        }

        public async Task<bool> CheckIfExistsAsync<T>(ISpecyfication<T> existanceSpecyfication, CancellationToken cancellationToken = default) where T : Location
        {
            IQueryable<T> query = _writeDbContext.Set<T>();
            query = query.Where(existanceSpecyfication.Criteria);
            return await query.AnyAsync(cancellationToken);
        }

        public async Task<T?> GetLocationBySpecyfictionAsync<T>(ISpecyfication<T> filterSpecylfication, CancellationToken cancellationToken = default) where T : Location
        {
            IQueryable<T> query = _writeDbContext.Set<T>();
            query = query.Where(filterSpecylfication.Criteria);
            foreach (var include in filterSpecylfication.Includes)
            {
                query = query.Include(include);
            }
            return await query.FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IEnumerable<T>> GetLocationsBySpecyfictionAsync<T>(ISpecyfication<T> filterSpecylfication, CancellationToken cancellationToken = default) where T : Location
        {
            IQueryable<T> query = _writeDbContext.Set<T>();
            query = query.Where(filterSpecylfication.Criteria);
            foreach(var include in filterSpecylfication.Includes) 
            {
                query = query.Include(include); 
            }
            return await query.ToListAsync(cancellationToken);
        }
    }
}
