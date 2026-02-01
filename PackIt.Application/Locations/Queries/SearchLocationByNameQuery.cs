using PackIt.Application.Locations.DTO;
using PackIt.Shared.Abstractions.Queries;

namespace PackIt.Application.Locations.Queries
{
    public class SearchLocationByNameQuery:IQuery<IEnumerable<LocationDto>>
    {
        public required string SearchPhrase { get; set; }
    }
}
