using PackIT.Domain.Common;

namespace PackIT.Domain.Orders.Exceptions
{

    public class OrderItemNotFoundException : PackItException
    {


        public OrderItemNotFoundException(string? message) : base(message)
        {

        }
    }
}