using PackIT.Domain.Common;

namespace PackIT.Domain.Orders.Events
{
    public record OrderItemAdded(OrderBase Order, OrderItem OrderItem) : IDomainEvent;
}