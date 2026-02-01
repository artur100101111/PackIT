using PackIt.Application.Locations.DTO;
using PackIT.Shared.DtoTree.DtoTreeBuilder;

namespace PackIt.Application.Locations
{
    /// <summary>
    /// Genarates Tree from flat list of hierarchical data.
    /// </summary>
    internal class LocationDtoTreeBuilder : DtoTreeBuilder<LocationDto, long>
    {
        public LocationDtoTreeBuilder(IEnumerable<LocationDto> nodes) : base()
        {
        }
    }
}
