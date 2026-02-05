
using PackIt.Shared.Abstractions.Domain.Exceptions;

namespace PackIT.Domain.Orders
{
    internal class ItemCannotBeAddedException : PackItException
    {
        public ItemCannotBeAddedException(string? message) : base(message)
        {
        }
    }
}