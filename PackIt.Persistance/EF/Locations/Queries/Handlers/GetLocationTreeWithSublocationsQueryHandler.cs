using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PackIt.Application.Locations.DTO;
using PackIt.Application.Locations.Queries;
using PackIt.Persistance.EF.Contexts;
using PackIt.Persistance.EF.Locations.ReadModels;
using PackIt.Shared.Abstractions.Queries;

namespace PackIt.Persistance.EF.Locations.Queries.Handlers
{
    internal class GetLocationTreeWithSublocationsQueryHandler: IQueryHandler<GetLocationTreeWithSublocationsByIdQuery, LocationDto>
    {

        private DbSet<LocationReadModel> _locations;
        private IMapper _mapper;


        public GetLocationTreeWithSublocationsQueryHandler(ReadDbContext readDbContext, IMapper mapper)
        {
            _locations = readDbContext.Locations;
            _mapper = mapper;
        }
        public async Task<LocationDto> HandleAsync(GetLocationTreeWithSublocationsByIdQuery query, CancellationToken cancellationToken)
        {
            var result = await _locations
                .FromSqlRaw("EXEC [packing].[GetLocationTreeById] @LocationId = {0}", query.LocationId)
                //.AsNoTracking() -> //and then tree builder from shared, but what is better ? -test the execution speed.
                .ToListAsync(cancellationToken);

            var location = result.SingleOrDefault(x => x.Id == query.LocationId);

            var locationDto =  _mapper.Map<LocationReadModel, LocationDto>(location);

            return locationDto;
        }
    }
}
