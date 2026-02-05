using PackIt.Shared.Abstractions.Domain.Exceptions;

namespace PackIt.Application.Orders.Exceptions
{
    public class OrderNotFoundException : PackItException
    {
        public OrderNotFoundException(string message) : base(message)
        {

        }
    }
}
