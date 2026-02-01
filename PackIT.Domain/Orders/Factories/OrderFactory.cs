using PackIT.Domain.Orders.Exceptions;
using PackIT.Domain.Orders.Factory;
using PackIT.Domain.Orders.States;
using PackIT.Domain.Orders.ValueObjects;
using PackIT.Domain.Shared;

namespace PackIT.Domain.Orders.Factories
{
    public sealed class OrderFactory : IOrderFactory
    {
        public Order CreateOrder(
            OrderId id,
            LocationVO requestedDeliveryLocation,
            List<OrderItem> items,
            DateTime orderDate,
            DateTime requestedDeliveryDate,
            IOrderStateService stateService,
            IDateTimeService clock)
        {
            if (!items.Any())
                throw new EmptyOrderException("Order must contain at least one Item");

            var order = new Order(
                id,
                orderDate,
                requestedDeliveryDate,
                requestedDeliveryLocation,
                items.ToList());

            order.SetState(stateService, stateService.InitialState, clock);

            return order;
        }

        public OrderTemplate CreateOrderTemplate(OrderId id, DateTime orderDate, LocationVO requestedDeliveryLocationVO, List<OrderItem> items, string orderName)
        {
            if (!items.Any())
                throw new EmptyOrderException("Order must contain at least one Item");

            var order = new OrderTemplate(id, orderDate, requestedDeliveryLocationVO, items, orderName);

            return order;
        }

    }
}
