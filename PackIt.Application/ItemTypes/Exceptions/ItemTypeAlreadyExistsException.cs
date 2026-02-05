using PackIt.Shared.Abstractions.Domain.Exceptions;

namespace PackIt.Application.ItemTypes.Exceptions
{
    internal class ItemTypeAlreadyExistsException : AlreadyExistsException
    {

        public ItemTypeAlreadyExistsException(string? message) : base(message)
        {
        }

    }
}