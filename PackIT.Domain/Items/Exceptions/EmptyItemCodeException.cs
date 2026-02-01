using PackIT.Domain.Common;

namespace PackIT.Domain.Items.Exceptions
{
    internal class EmptyItemCodeException : PackItException
    {
        public EmptyItemCodeException(string? message) : base(message)
        {
        }

    }
}