using PackIt.Shared.Abstractions.Domain.Exceptions;

namespace PackIt.Application.Orders.Exceptions
{
    public class OrderAlreadyExistsException : PackItException
    {
        public OrderAlreadyExistsException(string message) : base(message)
        {

        }

    }
}
