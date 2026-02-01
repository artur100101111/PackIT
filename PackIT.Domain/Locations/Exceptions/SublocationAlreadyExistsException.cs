using PackIT.Domain.Common;

namespace PackIT.Domain.Locations.Exceptions
{
    public class SublocationAlreadyExistsException : PackItException
    {

        public SublocationAlreadyExistsException(string? message) : base(message)
        {
        }
    }
}