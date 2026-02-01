namespace PackIT.Domain.Locations.Factories
{

    public sealed class LocationFactory : ILocationFactory
    {
        public Location CreateLocation(LocationId Id, LocationName name, LocationCode code, string description, LocationTypeEnum type,  Location? ancestor)
        {
           return new Location(Id, name, code, description, type, ancestor);
        }
    }
}
