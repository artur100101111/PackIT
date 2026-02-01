namespace PackIT.Domain.Locations.Factories
{
    public interface ILocationFactory
    {
        Location CreateLocation(LocationId Id, LocationName name, LocationCode code, string description, LocationTypeEnum type, Location? ancestor);
    }
}
