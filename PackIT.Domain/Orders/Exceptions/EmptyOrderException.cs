using PackIt.Shared.Abstractions.Domain.Exceptions;

namespace PackIT.Domain.Orders.Exceptions
{
    public class EmptyOrderException : DomainRuleViolationException
    {
        public EmptyOrderException(string? message = null) : base(message)
        {
        }
    }
}
