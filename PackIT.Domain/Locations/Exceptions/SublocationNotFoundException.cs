using PackIt.Shared.Abstractions.Domain.Exceptions;

namespace PackIT.Domain.Locations.Exceptions
{
    public class SublocationNotFoundException : NotFoundException
    {
        public SublocationNotFoundException(string? message) : base(message)
        {
        }

    }
}