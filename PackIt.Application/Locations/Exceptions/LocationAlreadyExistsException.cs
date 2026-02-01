using PackIT.Domain.Common;

namespace PackIt.Application.Locations.Commands.CreateLocation
{
    public class LocationAlreadyExistsException:PackItException
    {
    
        public LocationAlreadyExistsException(string message):base(message) 
        {
        }
    }
}