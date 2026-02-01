using PackIT.Domain.Orders.States;
using PackIT.Domain.Orders.ValueObjects;
using PackIT.Domain.Shared;

namespace PackIT.Domain.Orders.Factory
{
    public interface IOrderFactory
    {

        Order CreateOrder(
            OrderId id,
            LocationVO deliveryLocation,
            List<OrderItem> items,
            DateTime orderDate,
            DateTime requestedDeliveryDate,
            IOrderStateService stateService,
            IDateTimeService clock);
        OrderTemplate CreateOrderTemplate(OrderId id, DateTime orderDate, LocationVO deliveryLocationVO, List<OrderItem> items, string orderName);
    }
}
