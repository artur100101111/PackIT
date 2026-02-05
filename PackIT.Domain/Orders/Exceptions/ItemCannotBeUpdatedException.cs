using PackIt.Shared.Abstractions.Domain.Exceptions;

namespace PackIT.Domain.Orders.Exceptions
{
    internal class ItemCannotBeUpdatedException: DomainRuleViolationException
    {
        public ItemCannotBeUpdatedException(string? message) : base(message)
        {
        }
    }
}
