using PackIT.Domain.Common;

namespace PackIT.Domain.Orders.Exceptions
{
    public class OrderItemAlreadyAddedException : PackItException
    {


        public OrderItemAlreadyAddedException(string? message): base(message)
        {

        }
    }
}