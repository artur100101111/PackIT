using PackIT.Domain.Orders;
using PackIT.Domain.Orders.Primitives;

namespace PackIt.Application.Orders.Factories
{
    internal interface IOrderItemsFactory
    {
        Task<List<OrderItem>> CreateOrderItemsAsync(List<OrderItemPrimitive> orderItemPrimitives, 
            CancellationToken cancellationToken = default);
    }
}
