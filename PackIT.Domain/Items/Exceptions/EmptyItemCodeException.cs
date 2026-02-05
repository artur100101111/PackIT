using PackIt.Shared.Abstractions.Domain.Exceptions;

namespace PackIT.Domain.Items.Exceptions
{
    internal class EmptyItemCodeException : DomainRuleViolationException
    {
        public EmptyItemCodeException(string? message) : base(message)
        {
        }

    }
}