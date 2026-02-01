using PackIt.Shared.Abstractions.Commands;
using PackIT.Domain.Orders.States;

namespace PackIt.Application.Orders.Commands.ChangeOrderState
{
    public record ChangeOrderStateCommand(long OrderId, OrderStateEnum NewOrderState, long? DeliveryLocationId ) : ICommand;
}
