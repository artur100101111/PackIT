using PackIT.Domain.Common;

namespace PackIT.Domain.Orders.Events
{
    public record OrderItemRemoved(OrderBase Order, OrderItem OrderItem) : IDomainEvent;
}
