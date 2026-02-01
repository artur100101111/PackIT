using PackIT.Domain.Common;

namespace PackIT.Domain.Orders.Exceptions
{
    internal class ItemCannotBeDeletedException: PackItException
    {
        public ItemCannotBeDeletedException(string? message) : base(message)
        {
        }
    }
}
