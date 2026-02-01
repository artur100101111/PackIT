using PackIt.Application.Orders.Commands.Requests;
using PackIt.Shared.Abstractions.Commands;

namespace PackIt.Application.Orders.Commands.CreateOrder
{
    public record CreateOrderTemplateCommand : ICommand
    {
        public CreateOrderTemplateCommand(long? OrderId, List<OrderItemRequest> OrderItemRequests, long RequesteLocationId, string OrderName)
        {
            this.OrderId = OrderId;
            this.OrderItemRequests = OrderItemRequests;
            this.RequesteLocationId = RequesteLocationId;
            this.OrderName = OrderName;
        }

        public long? OrderId { get; set; }
        public List<OrderItemRequest> OrderItemRequests { get; init; }
        public long RequesteLocationId { get; init; }
        public string OrderName { get; init; }
    }

}
