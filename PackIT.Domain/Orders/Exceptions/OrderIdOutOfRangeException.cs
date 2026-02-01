using PackIT.Domain.Common;

namespace PackIT.Domain.Orders.Exceptions
{
    internal class OrderIdOutOfRangeException : PackItException
    {


        public OrderIdOutOfRangeException(string? message) : base(message)
        {
        }

    }
}