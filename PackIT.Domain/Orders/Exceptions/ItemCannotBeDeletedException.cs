using PackIt.Shared.Abstractions.Domain.Exceptions;

namespace PackIT.Domain.Orders.Exceptions
{
    internal class ItemCannotBeDeletedException: DomainRuleViolationException
    {
        public ItemCannotBeDeletedException(string? message) : base(message)
        {
        }
    }
}
