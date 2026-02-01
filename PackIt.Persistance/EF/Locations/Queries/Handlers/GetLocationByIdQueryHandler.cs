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
    internal class GetLocationByIdQueryHandler : IQueryHandler<GetLocationByIdQuery, LocationDto>
    {

        private DbSet<LocationReadModel> _locations;
        private IMapper _mapper;


        public GetLocationByIdQueryHandler(ReadDbContext readDbContext, IMapper mapper)
        {
            _locations = readDbContext.Locations;
            _mapper = mapper;
        }
        public async Task<LocationDto?> HandleAsync(GetLocationByIdQuery query, CancellationToken cancellationToken)
        {
            var location = await _locations.Where(l=>l.Id == query.LocationId)
                .Include(s=>s.Sublocations)
                .AsNoTracking()
               // .ProjectTo<LocationDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(cancellationToken);



            return _mapper.Map<LocationReadModel,LocationDto>(location);
        }
    }
}
