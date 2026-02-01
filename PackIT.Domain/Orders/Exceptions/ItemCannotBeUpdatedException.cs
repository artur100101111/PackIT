using PackIT.Domain.Common;

namespace PackIT.Domain.Orders.Exceptions
{
    internal class ItemCannotBeUpdatedException: PackItException
    {
        public ItemCannotBeUpdatedException(string? message) : base(message)
        {
        }
    }
}
