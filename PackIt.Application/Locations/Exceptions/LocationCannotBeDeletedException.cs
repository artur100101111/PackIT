using PackIT.Domain.Common;

namespace PackIt.Application.Locations.Exceptions
{
    internal class LocationCannotBeDeletedException : PackItException
    {


        public LocationCannotBeDeletedException(string? message) : base(message)
        {
        }

    }
}