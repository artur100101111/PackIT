using PackIT.Domain.Orders;
using PackIT.Domain.Orders.Primitives;

namespace PackIt.Application.Orders.Factories
{
    internal interface IOrderApplicationFactroy
    {
        Task<Order> CreateOrderAsync(OrderId id, DateTime requestedDeliveryDate,
                   long requestedLocationId, List<OrderItemPrimitive> orderItemPrimitives, CancellationToken cancellationToken);
        Task<OrderTemplate> CreateOrderTemplateAsync(OrderId id, long requestedLocationId,
            List<OrderItemPrimitive> orderItemPrimitives, string orderName, CancellationToken cancellationToken);
    }
}
