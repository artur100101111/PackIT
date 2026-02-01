using NSubstitute;
using PackIT.Domain.Orders;
using PackIT.Domain.Orders.Events;
using PackIT.Domain.Orders.Exceptions;
using PackIT.Domain.Orders.States;
using PackIT.Domain.Orders.ValueObjects;
using PackIT.Domain.Shared;
using Shouldly;

namespace PackIT.UnitTest.Domain.Orders
{
    public class OrderTests
    {
        private Order GetOrder()
        {
            return OrderFactoryHelper.CreateOrder(OrderStateEnum.New);
        }

        [Fact]
        public void AddItem_Throws_ItemCannotBeAddedException_When_Order_State_Is_Different_Than_New()
        {
            //ARRANGE
            var order = GetOrder();
            var orderStateSrv = Substitute.For<IOrderStateService>();
            orderStateSrv.TryChangeState(Arg.Any<Order>(), OrderStateEnum.InPacking).Returns(OrderStateEnum.InPacking);

            var clock = Substitute.For<IDateTimeService>();
            clock.UtcNow.Returns(DateTime.UtcNow);

            order.SetState(orderStateSrv, OrderStateEnum.InPacking, clock);
            var item = new OrderItem(new ItemVO("Item", "001", "Device", "D001"), 1);

            //ACT
            var exception = Record.Exception(() => order.AddItem(item));

            //ASSERT
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<ItemCannotBeAddedException>();
        }

        [Fact]
        public void RemoveItem_Throws_ItemCannotBeDeletedException_When_Order_State_Is_Different_Than_New()
        {
            //ARRANGE
            var order = GetOrder();

            var orderStateSrv = Substitute.For<IOrderStateService>();
            orderStateSrv.TryChangeState(Arg.Any<Order>(), OrderStateEnum.InPacking).Returns(OrderStateEnum.InPacking);

            var clock = Substitute.For<IDateTimeService>();
            clock.UtcNow.Returns(DateTime.UtcNow);

            order.SetState(orderStateSrv, OrderStateEnum.InPacking, clock);
            var item = new OrderItem(new ItemVO("Item", "001", "Device", "D001"), 1);

            //ACT
            var exception = Record.Exception(() => order.RemoveItem(item.ItemVO.Code));

            //ASSERT
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<ItemCannotBeDeletedException>();
        }

        [Fact]
        public void RemoveItem_Removes_Item_From_OrderItems_and_Generates_DomainEvent()
        {
            //ARRANGE
            var order = GetOrder();
            order.RemoveItem("001");
            order.ClearEvents();

            var orderStateSrv = Substitute.For<IOrderStateService>();
            orderStateSrv.TryChangeState(Arg.Any<Order>(), OrderStateEnum.New).Returns(OrderStateEnum.New);

            var clock = Substitute.For<IDateTimeService>();
            clock.UtcNow.Returns(DateTime.UtcNow);

            order.SetState(orderStateSrv, OrderStateEnum.New, clock);
            var item = new OrderItem(new ItemVO("Item", "001", "Device", "D001"), 1);
            order.AddItem(item);

            //ACT
            var exception = Record.Exception(() => order.RemoveItem(item.ItemVO.Code));

            //ASSERT
            exception.ShouldBeNull();
            var orderItems = order.OrderItems.FirstOrDefault(oi => oi.ItemVO.Code == "001");
            orderItems.ShouldBeNull();

            var @event = order.Events.FirstOrDefault(e => e.GetType() == typeof(OrderItemRemoved));
            @event.ShouldNotBeNull();
        }


        [Fact]
        public void AddItem_Adds_Items_To_OrderItems_And_Generates_Domain_Events_On_Success()
        {
            //ARRANGE
            var order = GetOrder();
            order.ClearEvents();
            var orderStateSrv = Substitute.For<IOrderStateService>();
            orderStateSrv.TryChangeState(Arg.Any<Order>(), OrderStateEnum.New).Returns(OrderStateEnum.New);

            var clock = Substitute.For<IDateTimeService>();
            clock.UtcNow.Returns(DateTime.UtcNow);

            order.SetState(orderStateSrv, OrderStateEnum.InPacking, clock);
            var item2 = new OrderItem(new ItemVO("Item2", "002", "Device", "D002"), 1);//to add  item with other code

            //ACT
            var exception2 = Record.Exception(() => order.AddItem(item2));

            //ASSERT
            exception2.ShouldBeNull();

            var orderItem2 = order.OrderItems.FirstOrDefault(oi => oi.ItemVO.Code == item2.ItemVO.Code);

            orderItem2.ShouldNotBeNull();
            orderItem2.Quantity.ShouldBe(1);

            var @orderItemAddedEvent = order.Events.Where(e => e.GetType() == typeof(OrderItemAdded));
            @orderItemAddedEvent.ShouldNotBeNull();
            @orderItemAddedEvent.Count().ShouldBe(1);
        }

        [Fact]
        public void UpdateItems_Adds_Updates_and_Deletes_Items_from_OrderItems_And_Generates_Domain_Events_On_Success()
        {
            //ARRANGE
            var order = GetOrder();
            order.RemoveItem("001");
            order.ClearEvents();

            var orderStateSrv = Substitute.For<IOrderStateService>();
            orderStateSrv.TryChangeState(Arg.Any<Order>(), OrderStateEnum.New).Returns(OrderStateEnum.New);

            var clock = Substitute.For<IDateTimeService>();
            clock.UtcNow.Returns(DateTime.UtcNow);

            order.SetState(orderStateSrv, OrderStateEnum.InPacking, clock);

            #region initial items
            var orderItem0 = new OrderItem(new ItemVO("Item1", "001", "Device", "D001"), 1);//it should be removed by Update()
            order.AddItem(orderItem0);

            var orderItem1 = new OrderItem(new ItemVO("Item2", "002", "Device", "D002"), 1);//it should be updated to 2 pcs.
            order.AddItem(orderItem1);
            #endregion

            #region new set of items to update
            var orderItem2 = new OrderItem(new ItemVO("Item2", "002", "Device", "D002"), 2); ;//shouldl be updated
            var orderItem3 = new OrderItem(new ItemVO("Item3", "003", "Device", "D003"), 1);//it shpuld be added
            var items = new List<OrderItem>() { orderItem2, orderItem3 };
            #endregion

            //ACT
            var exception1 = Record.Exception(() => order.UpdateItems(items));

            //ASSERT
            exception1.ShouldBeNull();

            #region result orderItems set
            var orderItem2result = order.OrderItems.FirstOrDefault(oi => oi.ItemVO.Code == orderItem2.ItemVO.Code);
            var orderItem3result = order.OrderItems.FirstOrDefault(oi => oi.ItemVO.Code == orderItem3.ItemVO.Code);

            orderItem2result.ShouldNotBeNull();
            orderItem3result.ShouldNotBeNull();

            orderItem2result.Quantity.ShouldBe(2);
            orderItem3result.Quantity.ShouldBe(1);
            #endregion

            #region events
            var @orderItemAddedEvent = order.Events.Where(e => e.GetType() == typeof(OrderItemAdded));
            @orderItemAddedEvent.ShouldNotBeNull();
            @orderItemAddedEvent.Count().ShouldBe(3);

            var @orderItemUpdatedEvent = order.Events.Where(e => e.GetType() == typeof(OrderItemUpdated));
            @orderItemUpdatedEvent.ShouldNotBeNull();
            @orderItemUpdatedEvent.Count().ShouldBe(1);

            var @orderItemRemovedEvent = order.Events.Where(e => e.GetType() == typeof(OrderItemRemoved));
            @orderItemUpdatedEvent.ShouldNotBeNull();
            @orderItemUpdatedEvent.Count().ShouldBe(1);
            #endregion
        }

        [Fact]
        public void SetDelivery_Sets_DeliveryTime_and_DeliveryLocation_Properties()
        {
            //ARRANGE
            var order = GetOrder();
            var deliveryLocation = new LocationVO("Line1", "L001", "Production Line");
            var eventTime = DateTime.UtcNow;
            var clock = Substitute.For<IDateTimeService>();
            clock.UtcNow.Returns(eventTime);

            //ACT
            var exception = Record.Exception(() => order.SetDelivery(deliveryLocation,clock));

            //ASSTER
            exception.ShouldBeNull();

            order.DeliveryLocation.ShouldNotBeNull();
            Assert.Equal(order.DeliveryLocation,  deliveryLocation);
            Assert.Equal(order.DeliveryTime, eventTime);
        }

        [Fact]
        public void SetState_Sets_Order_State_and_Generate_DomaiEvent()
        {
            //ARRANGE
            var order = GetOrder();
            order.ClearEvents();

            var clock = Substitute.For<IDateTimeService>();
            var eventDate  = DateTime.UtcNow;
            clock.UtcNow.Returns(eventDate);
            var stateService = Substitute.For<IOrderStateService>();
            var stateToSet = OrderStateEnum.InPacking;
            stateService.TryChangeState(Arg.Any<Order>(), OrderStateEnum.InPacking).Returns(stateToSet);

            //ACT
            var exception = Record.Exception(()=> order.SetState(stateService, stateToSet, clock));


            //ASSERT
            exception.ShouldBeNull();
            Assert.Equal(order.State, stateToSet);

            var @event = order.Events.Single(st=> st.GetType() == typeof(OrderStateChangedEvent)) as OrderStateChangedEvent;
            @event.ShouldNotBeNull();
            
            Assert.Equal(@event.CurrentState, stateToSet);
            Assert.Equal(@event.EventTime, eventDate);
        }
        [Fact]
        public void SerState_Throws_Exception_When_DeliveryLocation_Or_DeliveryTime_Is_Not_Settted()
        {
            //ARRANGE
            var order = GetOrder();
            var clock = Substitute.For<IDateTimeService>();
            var eventDate = DateTime.UtcNow;
            clock.UtcNow.Returns(eventDate);
            var stateService = Substitute.For<IOrderStateService>();
            var stateToSet = OrderStateEnum.Delivered;
            stateService.TryChangeState(Arg.Any<Order>(), OrderStateEnum.Delivered).Returns(stateToSet);

            //ACT
            var exception = Record.Exception(() => order.SetState(stateService, stateToSet, clock));

            //ASSERT
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<DeliveryLocaitonCannotBeEmptyException>();
            order.DeliveryLocation.ShouldBeNull();
            order.DeliveryTime.ShouldBeNull();
        }

    }
}
