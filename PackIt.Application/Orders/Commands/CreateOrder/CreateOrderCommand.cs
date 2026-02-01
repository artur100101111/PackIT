using PackIt.Application.Orders.Commands.Requests;
using PackIt.Shared.Abstractions.Commands;

namespace PackIt.Application.Orders.Commands.CreateOrder
{
    public record CreateOrderCommand : ICommand
    {
        public CreateOrderCommand(long? OrderId, DateTime RequestedDeliveryTime, List<OrderItemRequest> OrderItemRequests, long RequestedLocationId)
        {
            this.OrderId = OrderId;
            this.RequestedDeliveryTime = RequestedDeliveryTime;
            this.OrderItemRequests = OrderItemRequests;
            this.RequestedLocationId = RequestedLocationId;
        }

        public long? OrderId { get; set; }
        public DateTime RequestedDeliveryTime { get; init; }

        public List<OrderItemRequest> OrderItemRequests { get; init; }
        public long RequestedLocationId { get; init; }
    }

}
   
