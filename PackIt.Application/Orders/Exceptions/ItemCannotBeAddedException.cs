
using PackIT.Domain.Common;

namespace PackIT.Domain.Orders
{
    internal class ItemCannotBeAddedException : PackItException
    {
        public ItemCannotBeAddedException(string? message) : base(message)
        {
        }
    }
}