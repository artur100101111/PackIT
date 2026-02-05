
using PackIt.Shared.Abstractions.Domain.Exceptions;

namespace PackIT.Domain.Items
{
    internal class ItemCodeLenghtOutOfRangeException : DomainRuleViolationException
    {


        public ItemCodeLenghtOutOfRangeException(string? message) : base(message)
        {
        }

    }
}