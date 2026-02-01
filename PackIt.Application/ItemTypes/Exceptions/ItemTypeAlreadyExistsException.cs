using PackIT.Domain.Common;

namespace PackIt.Application.ItemTypes.Exceptions
{
    internal class ItemTypeAlreadyExistsException : PackItException
    {

        public ItemTypeAlreadyExistsException(string? message) : base(message)
        {
        }

    }
}