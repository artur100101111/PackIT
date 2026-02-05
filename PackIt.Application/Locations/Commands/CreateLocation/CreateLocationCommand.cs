using PackIt.Shared.Abstractions.Commands;
using PackIT.Domain.Locations;

namespace PackIt.Application.Locations.Commands.CreateLocation
{
    public record CreateLocationCommand : ICommand
    {
        public CreateLocationCommand(long? LocationId, string Name, string Code, string? Description, LocationTypeEnum LocationType,  long? ancestorId)
        {
            this.LocationId = LocationId;
            this.Name = Name;
            this.Code = Code;
            this.ancestorId = ancestorId;
            this.LocationType = LocationType;
            this.Description = Description;

        }

        public long? LocationId { get; set; }
        public string Name { get; init; }
        public string Code { get; init; }
        public string? Description { get; init; }
        public LocationTypeEnum LocationType { get; set; }
        public long? ancestorId { get; init; }
    }
}
