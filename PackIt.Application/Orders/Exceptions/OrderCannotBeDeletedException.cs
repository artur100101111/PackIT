using PackIT.Domain.Common;

namespace PackIt.Application.Orders.Exceptions
{
    public class OrderCannotBeDeletedException : PackItException
    {

        public OrderCannotBeDeletedException(string? message) : base(message)
        {
        }


    }
}