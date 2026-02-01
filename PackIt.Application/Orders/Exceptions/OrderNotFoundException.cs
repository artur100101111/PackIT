using PackIT.Domain.Common;

namespace PackIt.Application.Orders.Exceptions
{
    public class OrderNotFoundException : PackItException
    {
        public OrderNotFoundException(string message) : base(message)
        {

        }
    }
}
