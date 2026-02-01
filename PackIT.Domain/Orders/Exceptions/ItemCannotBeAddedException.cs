using PackIT.Domain.Common;

namespace PackIT.Domain.Orders.Exceptions
{
    internal class ItemCannotBeAddedException : PackItException
    {
        public ItemCannotBeAddedException(string? message) : base(message)
        {
        }

    }
}