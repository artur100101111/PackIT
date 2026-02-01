using PackIt.Application.Locations.DTO;
using PackIt.Shared.Abstractions.Queries;

namespace PackIt.Application.Locations.Queries
{
    public class GetLocationsListWithSublocationsByIdQuery: IQuery<IEnumerable<LocationDto>>
    {
        public long LocationId { get; set; }
    }
}
