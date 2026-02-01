using PackIT.Domain.Common;

namespace PackIt.Application.Items.Exceptions
{
    public class ItemAlreadyExistsException : PackItException
    {

        public ItemAlreadyExistsException(string? message) : base(message)
        {
        }

    }
}