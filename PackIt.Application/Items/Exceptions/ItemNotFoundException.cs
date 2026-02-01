using PackIT.Domain.Common;

namespace PackIt.Application.Items.Exceptions
{

    public class ItemNotFoundException : PackItException
    {


        public ItemNotFoundException(string? message) : base(message)
        {
        }

    }
}