using PackIt.Application.Locations.DTO;
using PackIt.Shared.Abstractions.Queries;

namespace PackIt.Application.Locations.Queries
{
    public class GetLocationByCodeQuery: IQuery<LocationDto>
    {
        public string Code { get; set; }
    }
}
