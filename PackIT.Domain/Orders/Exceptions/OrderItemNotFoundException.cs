using PackIt.Shared.Abstractions.Domain.Exceptions;

namespace PackIT.Domain.Orders.Exceptions
{

    public class OrderItemNotFoundException : NotFoundException
    {


        public OrderItemNotFoundException(string? message) : base(message)
        {

        }
    }
}