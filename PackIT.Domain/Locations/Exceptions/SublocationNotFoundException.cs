using PackIT.Domain.Common;

namespace PackIT.Domain.Locations.Exceptions
{
    public class SublocationNotFoundException : PackItException
    {
        public SublocationNotFoundException(string? message) : base(message)
        {
        }

    }
}