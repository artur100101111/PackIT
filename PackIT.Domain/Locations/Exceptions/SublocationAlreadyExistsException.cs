using PackIt.Shared.Abstractions.Domain.Exceptions;

namespace PackIT.Domain.Locations.Exceptions
{
    public class SublocationAlreadyExistsException : AlreadyExistsException
    {

        public SublocationAlreadyExistsException(string? message) : base(message)
        {
        }
    }
}