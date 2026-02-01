
using PackIt.Shared.Abstractions.Commands;

namespace PackIt.Application.Orders.Commands.DeleteOrder
{
    public record DeleteOrderTemplateCommand(long OrderId): ICommand;
}
