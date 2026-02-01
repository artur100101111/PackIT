using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PackIt.Application.Locations.DTO;
using PackIt.Application.Locations.Queries;
using PackIt.Persistance.EF.Contexts;
using PackIt.Persistance.EF.Locations.ReadModels;
using PackIt.Shared.Abstractions.Queries;

namespace PackIt.Persistance.EF.Locations.Queries.Handlers
{
    internal class GetLocationsListWithSublocationsByIdQueryHandler : IQueryHandler<GetLocationsListWithSublocationsByIdQuery, IEnumerable<LocationDto>>
    {
        private DbSet<LocationReadModel> _locations;
        private IMapper _mapper;

        public GetLocationsListWithSublocationsByIdQueryHandler(ReadDbContext readDbContext, IMapper mapper)
        {
            _locations = readDbContext.Locations;
            _mapper = mapper;
        }
        public async Task<IEnumerable<LocationDto>> HandleAsync(GetLocationsListWithSublocationsByIdQuery query, CancellationToken cancellationToken)
        {
            var result = await _locations
                .FromSqlRaw("EXEC [packing].[GetLocationTreeById] @id = {0}", query.LocationId)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
            List<LocationDto> locations = new List<LocationDto>();

            foreach (var location in result)
            {
                locations.Add(_mapper.Map<LocationReadModel, LocationDto>(location));
            }
            return locations ;
        }
    }
}
