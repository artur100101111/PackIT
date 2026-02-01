using PackIT.Domain.Common;

namespace PackIt.Application.ItemTypes.Exceptions
{
    public class ItemTypeNotFoundException : PackItException
    {


        public ItemTypeNotFoundException(string? message) : base(message)
        {
        }

    }
}