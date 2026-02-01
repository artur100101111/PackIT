using PackIT.Domain.Common;

namespace PackIT.Domain.Locations.Exceptions
{
    public class LocationNameLenghtOutOfRangeException:PackItException
    {
        public LocationNameLenghtOutOfRangeException(string? message) : base(message)
        {
        }
    }
}
