using PackIt.Shared.Abstractions.Commands;

namespace PackIt.Application.Orders.Commands.DeleteOrder
{
    public record DeleteOrderCommand(long OrderId): ICommand;

}
