using PackIT.Domain.Common;

namespace PackIT.Domain.Orders.Events
{
    public record OrderItemUpdated(OrderBase Order, OrderItem OrderItem) : IDomainEvent;

}
