using PackIT.Domain.Common;

namespace PackIT.Domain.Orders.Exceptions
{
    public class OrderIdLessThanZeroException : PackItException
    {
        public OrderIdLessThanZeroException(string? message):base(message)
        {
        }
    }
}