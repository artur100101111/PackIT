using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PackIt.Application.Locations.DTO;
using PackIt.Application.Locations.Queries;
using PackIt.Persistance.EF.Contexts;
using PackIt.Persistance.EF.Locations.ReadModels;
using PackIt.Shared.Abstractions.Queries;

namespace PackIt.Persistance.EF.Locations.Queries.Handlers
{
    internal class GetLocationByCodeQueryHandler: IQueryHandler<GetLocationByCodeQuery, LocationDto>
    {

        private DbSet<LocationReadModel> _locations;
        private IMapper _mapper;


        public GetLocationByCodeQueryHandler(ReadDbContext readDbContext, IMapper mapper)
        {
            _locations = readDbContext.Locations;
            _mapper = mapper;
        }
        public async Task<LocationDto?> HandleAsync(GetLocationByCodeQuery query, CancellationToken cancellationToken)
        {
            var location = await _locations.Where(l => l.Code == query.Code)
                .ProjectTo<LocationDto>(_mapper.ConfigurationProvider)
                .AsNoTracking()
                .SingleOrDefaultAsync(cancellationToken);


            return location;
        }
    }
}
