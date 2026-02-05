using PackIt.Shared.Abstractions.Domain.Exceptions;

namespace PackIT.Domain.Items.Exceptions
{
    internal class EmptyItemNameException : DomainRuleViolationException
    {
        public EmptyItemNameException(string? message) : base(message)
        {
        }
    }
}