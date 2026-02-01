using Microsoft.EntityFrameworkCore;
using PackIt.Application.Locations.DTO;
using PackIt.Application.Services;
using PackIt.Persistance.EF.Contexts;

namespace PackIt.Persistance.EF.Services
{
    internal class LocationReadService : ILocationReadService
    {
        private ReadDbContext _readDbContext;
        private DbSet<LocationDto> _locations;

        public LocationReadService(ReadDbContext readDbContext)
        {
            _readDbContext = readDbContext;
            _locations = _readDbContext.Set<LocationDto>();
        }


        public async Task<IEnumerable<long>> GetParentTreePathAsync(long locationId, CancellationToken cancellationToken)
        {
            var ancestorsList = await _readDbContext.Database.SqlQuery<long>($"[packit].[SPGetLocationAncestorsPath] {locationId}").ToListAsync(cancellationToken);

            return ancestorsList;
        }
    }
}
