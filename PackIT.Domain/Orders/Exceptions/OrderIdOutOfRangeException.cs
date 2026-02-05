using PackIt.Shared.Abstractions.Domain.Exceptions;

namespace PackIT.Domain.Orders.Exceptions
{
    internal class OrderIdOutOfRangeException : DomainRuleViolationException
    {


        public OrderIdOutOfRangeException(string? message) : base(message)
        {
        }

    }
}