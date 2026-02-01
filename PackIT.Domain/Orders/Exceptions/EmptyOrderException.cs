using PackIT.Domain.Common;

namespace PackIT.Domain.Orders.Exceptions
{
    public class EmptyOrderException : PackItException
    {
        public EmptyOrderException(string? message = null) : base(message)
        {
        }
    }
}
