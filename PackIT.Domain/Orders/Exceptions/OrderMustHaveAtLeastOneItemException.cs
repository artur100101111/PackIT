using PackIT.Domain.Common;

namespace PackIT.Domain.Orders.Exceptions
{
    public class OrderMustHaveAtLeastOneItemException : PackItException
    {
        public OrderMustHaveAtLeastOneItemException(string message) : base(message)
        {
        }
    }
}
