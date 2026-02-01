using PackIt.Application.Locations.DTO;
using PackIt.Shared.Abstractions.Queries;

namespace PackIt.Application.Locations.Queries
{
    public class GetLocationByIdQuery: IQuery<LocationDto>
    {
        public required long LocationId { get; set; }
    }
}
