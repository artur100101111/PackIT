using PackIt.Shared.Abstractions.Domain.Exceptions;

namespace PackIT.Domain.Items.Exceptions
{
    internal class ItemNameLenghtOutOfRangeException : DomainRuleViolationException
    {
        public ItemNameLenghtOutOfRangeException(string? message) : base(message)
        {
        }
    }
}