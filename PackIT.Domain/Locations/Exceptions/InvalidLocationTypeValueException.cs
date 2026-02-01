using PackIT.Domain.Common;

namespace PackIT.Domain.Locations.Exceptions
{
    public class InvalidLocationTypeValueException : PackItException
    {
        public InvalidLocationTypeValueException(string message) : base(message)
        {
        }
    }
}
