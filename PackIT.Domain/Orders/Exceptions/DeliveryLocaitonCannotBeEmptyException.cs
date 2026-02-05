using PackIt.Shared.Abstractions.Domain.Exceptions;

namespace PackIT.Domain.Orders.Exceptions
{
    internal class DeliveryLocaitonCannotBeEmptyException : DomainRuleViolationException
    {

        public DeliveryLocaitonCannotBeEmptyException(string? message) : base(message)
        {
        }

    }
}