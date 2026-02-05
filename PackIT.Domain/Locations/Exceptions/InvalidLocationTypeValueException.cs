using PackIt.Shared.Abstractions.Domain.Exceptions;

namespace PackIT.Domain.Locations.Exceptions
{
    public class InvalidLocationTypeValueException : DomainRuleViolationException
    {
        public InvalidLocationTypeValueException(string message) : base(message)
        {
        }
    }
}
