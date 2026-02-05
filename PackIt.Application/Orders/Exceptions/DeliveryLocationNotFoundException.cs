using PackIt.Shared.Abstractions.Domain.Exceptions;

namespace PackIt.Application.Orders.Exceptions
{ 
    internal class DeliveryLocationNotFoundException : PackItException
    {

        public DeliveryLocationNotFoundException(string? message) : base(message)
        {
        }
    }
}