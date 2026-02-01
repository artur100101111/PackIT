using PackIT.Domain.Common;
using PackIT.Domain.Locations.Exceptions;

namespace PackIT.Domain.Locations
{
    public class Location : AggregateRoot<LocationId>,  IEntity<LocationId>
    {
        public  LocationName Name { get; private set; }
        public LocationCode Code { get; private set; }

        public LocationDescription Description { get; set; }


        /// <summary>
        /// Enum for simplicity.
        /// </summary>
        public LocationType Type { get; private set; }
        public Location? ParentLocation
        { get; private set; }
        public LocationId? ParentId { get; private set; }

        public IReadOnlyCollection<Location> Sublocations { get; private set; }
        private Location(): base() 
        { }
        public Location(LocationId locationId, LocationName name, LocationCode code, string description, LocationTypeEnum type,  Location? parentLocation)
        {
            Id = locationId;
            Code = code;
            Name = name;
            ParentLocation = parentLocation;
            Description = description;
            Type = type;
        }

        public void SetParentLocation(long? parentLocationId)
        { 
            this.ParentId = parentLocationId;
            this.IncrementVersion();
        }



        public override string ToString()
        {
            return $"Name: {this.Name}, Code: {Code}";
        }


    }



}
