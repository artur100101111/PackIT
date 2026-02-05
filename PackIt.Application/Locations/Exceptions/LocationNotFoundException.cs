using PackIt.Shared.Abstractions.Domain.Exceptions;

namespace PackIt.Application.Locations.Exceptions
{
    public class LocationNotFoundException : PackItException
    {

        public LocationNotFoundException(string? message) : base(message)
        {
        }

    }
}