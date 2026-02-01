using PackIt.Application.Orders.Commands.Requests;
using PackIt.Shared.Abstractions.Commands;

namespace PackIt.Application.Orders.Commands.AddOrderItems
{
    public record  UpdateOrderTemplateItemsCommand(long OrderId, IEnumerable<OrderItemRequest> orderItems) : ICommand;
}
