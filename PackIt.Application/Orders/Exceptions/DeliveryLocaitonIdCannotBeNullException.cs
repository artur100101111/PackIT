using PackIt.Shared.Abstractions.Domain.Exceptions;

namespace PackIt.Application.Orders.Exceptions
{
    internal class DeliveryLocaitonIdCannotBeNullException : PackItException
    {
        public DeliveryLocaitonIdCannotBeNullException(string? message) : base(message)
        {
        }

    }
}