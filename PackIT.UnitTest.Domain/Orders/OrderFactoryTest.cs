using NSubstitute;
using PackIT.Domain.Orders;
using PackIT.Domain.Orders.Exceptions;
using PackIT.Domain.Orders.Factories;
using PackIT.Domain.Orders.States;
using PackIT.Domain.Orders.ValueObjects;
using PackIT.Domain.Shared;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace PackIT.UnitTest.Domain.Orders
{
    public class OrderFactoryTest
    {
        private OrderFactory _orderFactory;
        public OrderFactoryTest()
        {
            _orderFactory = new OrderFactory();
        }



        [Fact]
        public void CreateOrder_Throws_EmptyOrderException_When_OrderItems_List_is_empty()
        {
            //ARRANGE
            var id = 1;
            var orderDate = DateTime.UtcNow;
            var requestedDeliveryLocation = new LocationVO("Line 1", "L001", "Production Line");
            var requestedDeliveryDate = DateTime.UtcNow.AddHours(3);
            var stateService = Substitute.For<IOrderStateService>();
            var clock = Substitute.For<IDateTimeService>();

            var orderItems = new List<OrderItem>();

            //ACT
            var exception = Record.Exception(() => _orderFactory.CreateOrder(id, requestedDeliveryLocation, orderItems, orderDate, requestedDeliveryDate, stateService, clock));

            //ASSERT
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<EmptyOrderException>();
        }

        [Fact]
        public void CreateOrderTemplate_Throws_EmptyOrderException_When_OrderItems_List_is_empty()
        {
            //ARRANGE
            var id = 1;
            var orderDate = DateTime.UtcNow;
            var requestedDeliveryLocation = new LocationVO("Line 1", "L001", "Production Line");
            var name = "DAF valve components";
            var orderItems = new List<OrderItem>();

            //ACT
            var exception = Record.Exception(() => _orderFactory.CreateOrderTemplate(id, orderDate, requestedDeliveryLocation, orderItems, name));

            //ASSERT
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<EmptyOrderException>();
        }

        [Fact]
        public void Factory_Creates_Order_And_Sets_Initial_Order_State()
        {
            //ARRANGE
            var id = 1;
            var orderDate = DateTime.UtcNow;
            var requestedDeliveryLocation = new LocationVO("Line 1", "L001", "Production Line");
            var requestedDeliveryDate = DateTime.UtcNow.AddHours(3);

            var stateService = Substitute.For<IOrderStateService>();
            var orderInitialState = OrderStateEnum.New;
            stateService.InitialState.Returns(orderInitialState);

            var clock = Substitute.For<IDateTimeService>();

            var itemQuantity = 5;
            var itemVO = new ItemVO("ZX", "ZX001", "Adhesive Tape", "AT001");
            var orderItem = new OrderItem(itemVO, itemQuantity);
            var orderItems = new List<OrderItem>();
            orderItems.Add(orderItem);

            //ACT
            var order = _orderFactory.CreateOrder(id, requestedDeliveryLocation, orderItems, orderDate, requestedDeliveryDate, stateService, clock);

            //ASSERT
            order.ShouldNotBeNull();

            var oItems = order.OrderItems.ToList();
            oItems.ShouldNotBeNull();
            oItems.Count.ShouldBe(1);
            var item = oItems.Single();
            Assert.Equal(item.Quantity, itemQuantity);
            Assert.Equal(item.ItemVO, itemVO);

            Assert.Equal(order.Id, id);
            Assert.Equal(order.CreationDate, orderDate);
            Assert.Equal(order.RequestedDeliveryLocation, requestedDeliveryLocation);
            Assert.Equal(order.RequestedDeliveryTime, requestedDeliveryDate);
            Assert.Equal(order.State, orderInitialState) ;
        }

        [Fact]
        public void Factory_Creates_OrderTemplate()
        {
            //ARRANGE
            var id = 1;
            var orderDate = DateTime.UtcNow;
            var requestedDeliveryLocation = new LocationVO("Line 1", "L001", "Production Line");
            var name = "DAF valve components";

            var itemQuantity = 5;
            var itemVO = new ItemVO("ZX", "ZX001", "Adhesive Tape", "AT001");
            var orderItem = new OrderItem(itemVO, itemQuantity);
            var orderItems = new List<OrderItem>();
            orderItems.Add(orderItem);

            //ACT
            var order = _orderFactory.CreateOrderTemplate(id, orderDate, requestedDeliveryLocation, orderItems, name);

            //ASSERT
            order.ShouldNotBeNull();

            var oItems = order.OrderItems.ToList();
            oItems.ShouldNotBeNull();
            oItems.Count.ShouldBe(1);
            var item = oItems.Single();
            Assert.Equal(item.Quantity, itemQuantity);
            Assert.Equal(item.ItemVO, itemVO);

            Assert.Equal(order.Id, id);
            Assert.Equal(order.CreationDate, orderDate);
            Assert.Equal(order.RequestedDeliveryLocation, requestedDeliveryLocation);
            Assert.Equal(order.Name, name);
        }
    }
}
