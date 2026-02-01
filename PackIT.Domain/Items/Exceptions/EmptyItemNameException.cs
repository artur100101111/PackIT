using PackIT.Domain.Common;

namespace PackIT.Domain.Items.Exceptions
{
    internal class EmptyItemNameException : PackItException
    {
        public EmptyItemNameException(string? message) : base(message)
        {
        }
    }
}