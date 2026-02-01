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
    internal class SearchLocationByNameQueryHandler : IQueryHandler<SearchLocationByNameQuery, IEnumerable<LocationDto>>
    {
        private DbSet<LocationReadModel> _locations;
        private IMapper _mapper;


        public SearchLocationByNameQueryHandler(ReadDbContext readDbContext, IMapper mapper)
        {
            _locations = readDbContext.Locations;
            _mapper = mapper;
        }
        public async Task<IEnumerable<LocationDto>> HandleAsync(SearchLocationByNameQuery query, CancellationToken cancellationToken)
        {
            var locations = await _locations.Where(l=>l.Name.Contains(query.SearchPhrase))
                .ProjectTo<LocationDto>(_mapper.ConfigurationProvider)
                .AsNoTracking()
                .ToListAsync(cancellationToken);


            return locations;
        }
    }
}
