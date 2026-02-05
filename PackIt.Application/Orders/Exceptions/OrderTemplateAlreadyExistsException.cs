using PackIt.Shared.Abstractions.Domain.Exceptions;

namespace PackIt.Application.Orders.Exceptions
{
    public class OrderTemplateAlreadyExistsException : PackItException
    {
        public OrderTemplateAlreadyExistsException(string? message) : base(message)
        {
        }
    }
}