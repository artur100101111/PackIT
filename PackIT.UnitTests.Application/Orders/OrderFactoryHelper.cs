using NSubstitute; 
using PackIT.Domain.Orders;
using PackIT.Domain.Orders.States;
using PackIT.Domain.Orders.ValueObjects;
using PackIT.Domain.Shared;

namespace PackIT.UnitTests.Application.Orders
{

    internal static class OrderFactoryHelper
    {
        /// <summary>
        /// Creates Order with one OrderItem and initial State
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public static Order CreateOrder(OrderStateEnum state)
        {
            var item = new OrderItem(new ItemVO("Item1", "001", "Device", "D001"), 1);
            var items = new List<OrderItem>() { item };

            var deliveryLocation = new LocationVO("ZX", "ZX01", "Line");
            var order = new Order(1, DateTime.UtcNow, DateTime.UtcNow.AddDays(1), deliveryLocation, items);
            var clock = Substitute.For<IDateTimeService>();
            clock.UtcNow.Returns(DateTime.UtcNow);

            if (state == OrderStateEnum.Delivered)
            {
                order.SetDelivery(deliveryLocation, clock);
            }
            var stateService = Substitute.For<IOrderStateService>();
            stateService.TryChangeState(Arg.Any<Order>(), state).Returns(state);
            order.SetState(stateService, state, clock);

            return order;

        }
    }
}
