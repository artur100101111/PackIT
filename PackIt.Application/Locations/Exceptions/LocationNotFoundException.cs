using PackIT.Domain.Common;

namespace PackIt.Application.Locations.Exceptions
{
    public class LocationNotFoundException : PackItException
    {

        public LocationNotFoundException(string? message) : base(message)
        {
        }

    }
}