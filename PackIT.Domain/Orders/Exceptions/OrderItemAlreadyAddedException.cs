using PackIt.Shared.Abstractions.Domain.Exceptions;

namespace PackIT.Domain.Orders.Exceptions
{
    public class OrderItemAlreadyAddedException : DomainRuleViolationException
    {


        public OrderItemAlreadyAddedException(string? message): base(message)
        {

        }
    }
}