using PackIt.Shared.Abstractions.Domain.Exceptions;

namespace PackIt.Application.Orders.Exceptions
{
    public class OrderCannotBeDeletedException : PackItException
    {

        public OrderCannotBeDeletedException(string? message) : base(message)
        {
        }


    }
}