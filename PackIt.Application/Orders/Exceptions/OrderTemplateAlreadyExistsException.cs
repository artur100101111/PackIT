using PackIT.Domain.Common;

namespace PackIt.Application.Orders.Exceptions
{
    public class OrderTemplateAlreadyExistsException : PackItException
    {
        public OrderTemplateAlreadyExistsException(string? message) : base(message)
        {
        }
    }
}