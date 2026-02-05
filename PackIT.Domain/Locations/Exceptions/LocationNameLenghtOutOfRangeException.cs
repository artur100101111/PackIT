using PackIt.Shared.Abstractions.Domain.Exceptions;

namespace PackIT.Domain.Locations.Exceptions
{
    public class LocationNameLenghtOutOfRangeException: DomainRuleViolationException
    {
        public LocationNameLenghtOutOfRangeException(string? message) : base(message)
        {
        }
    }
}
