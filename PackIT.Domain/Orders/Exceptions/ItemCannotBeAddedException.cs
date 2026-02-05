using PackIt.Shared.Abstractions.Domain.Exceptions;

namespace PackIT.Domain.Orders.Exceptions
{
    internal class ItemCannotBeAddedException: DomainRuleViolationException
    {
        public ItemCannotBeAddedException(string? message) : base(message)
        {
        }

    }
}