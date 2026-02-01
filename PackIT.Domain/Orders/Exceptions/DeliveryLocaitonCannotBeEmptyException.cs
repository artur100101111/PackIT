using PackIT.Domain.Common;

namespace PackIT.Domain.Orders.Exceptions
{
    internal class DeliveryLocaitonCannotBeEmptyException : PackItException
    {

        public DeliveryLocaitonCannotBeEmptyException(string? message) : base(message)
        {
        }

    }
}