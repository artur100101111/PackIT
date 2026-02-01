using NSubstitute;
using NSubstitute.ExceptionExtensions;
using PackIt.Application.Items.Exceptions;
using PackIt.Application.Locations.Exceptions;
using PackIt.Application.Orders.Factories;
using PackIt.Shared.Abstractions.Shared;
using PackIT.Domain.Locations;
using PackIT.Domain.Locations.Repository;
using PackIT.Domain.Orders;
using PackIT.Domain.Orders.Factory;
using PackIT.Domain.Orders.Primitives;
using PackIT.Domain.Orders.States;
using PackIT.Domain.Orders.ValueObjects;
using PackIT.Domain.Shared;
using Shouldly;

namespace PackIT.UnitTests.Application.Orders.Factories
{
    public class OrderApplicationFactroyTests
    {

        [Fact]
        public async Task CreateOrderTemplateAsync_Throws_LocationNotFoundException()
        {
            var orderId = new OrderId(1);
            var requestedLocationId = 1;
            _locationRepository.GetLocationBySpecyfictionAsync(Arg.Any<ISpecyfication<Location>>(), Arg.Any<CancellationToken>()).Returns((Location)null);
            var orderItemsPrimitives = GetOrderItemPrimitives();

            var exception =await Record.ExceptionAsync(() => _factory.CreateOrderTemplateAsync(orderId, requestedLocationId, orderItemsPrimitives,
                "Some Order", CancellationToken.None));

            //ASSERT
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<LocationNotFoundException>();
        }

        [Fact]
        public async Task CreateOrderAsync_Throws_LocationNotFoundExceotion()
        {
            var orderId = new OrderId(1);
            var requestedLocationId = 1;
            _locationRepository.GetLocationBySpecyfictionAsync(Arg.Any<ISpecyfication<Location>>(), Arg.Any<CancellationToken>()).Returns((Location)null);
            var requestedDeliveryDate = DateTime.UtcNow.AddHours(3);
            var orderItemsPrimitives = GetOrderItemPrimitives();

            var exception = await Record.ExceptionAsync(() => _factory.CreateOrderAsync(orderId, requestedDeliveryDate, requestedLocationId, 
                orderItemsPrimitives, CancellationToken.None));

            //ASSERT
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<LocationNotFoundException>();
        }


        [Fact]
        public async Task CreataeOrderTemplatAsync_Throws_ItemNotFoundException_when_ItemsFactory_Fails()
        {
            var orderId = new OrderId(1);
            var requestedLocationId = 1;
            var orderItemsPrimitives = GetOrderItemPrimitives();
            _orderItemsFactory.CreateOrderItemsAsync(Arg.Any<List<OrderItemPrimitive>>(), Arg.Any<CancellationToken>()).Throws(new ItemNotFoundException(string.Empty));
            var location = GetLocation();

            _locationRepository.GetLocationBySpecyfictionAsync(Arg.Any<ISpecyfication<Location>>(),Arg.Any<CancellationToken>()).Returns(location);

            var exception = await Record.ExceptionAsync(() => _factory.CreateOrderTemplateAsync(orderId, requestedLocationId, orderItemsPrimitives,
                "Some Order", CancellationToken.None));

            //ASSERT
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<ItemNotFoundException>();
        }

        [Fact]
        public async Task CreateOrderAsync_Throws_ItemNotFoundException_when_ItemsFactory_Fails()
        {
            var orderId = new OrderId(1);
            var requestedLocationId = 1;
            var orderItemsPrimitives = GetOrderItemPrimitives();
            _orderItemsFactory.CreateOrderItemsAsync(Arg.Any<List<OrderItemPrimitive>>(), Arg.Any<CancellationToken>()).Throws(new ItemNotFoundException(string.Empty));
            var location = GetLocation();
            var requestedDeliveryDate = DateTime.UtcNow.AddHours(3);

            _locationRepository.GetLocationBySpecyfictionAsync(Arg.Any<ISpecyfication<Location>>(), Arg.Any<CancellationToken>()).Returns(location);

            var exception = await Record.ExceptionAsync(() => _factory.CreateOrderAsync(orderId, requestedDeliveryDate, requestedLocationId,
         orderItemsPrimitives, CancellationToken.None));

            //ASSERT
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<ItemNotFoundException>();
        }


        [Fact]
        public async Task CreateOrderAsync_Uses_DomanFactory_To_Create_Order()
        {
            var orderId = new OrderId(1);
            var requestedDeliveryDate = DateTime.UtcNow;
            var orderItems = GetOrderItems();
            var orderItemPrimitives = GetOrderItemPrimitives();

            var location = GetLocation();
            _locationRepository.GetLocationBySpecyfictionAsync(Arg.Any<ISpecyfication<Location>>(), Arg.Any<CancellationToken>()).Returns(location);

            _orderItemsFactory.CreateOrderItemsAsync(
                Arg.Any<List<OrderItemPrimitive>>(),
                Arg.Any<CancellationToken>())
                .Returns(orderItems);

            var order = Substitute.For<Order>();
            _domainFactory.CreateOrder(
                Arg.Any<OrderId>(),
                Arg.Any<LocationVO>(),
                Arg.Any<List<OrderItem>>(),
                Arg.Any<DateTime>(),
                Arg.Any<DateTime>(),
                _orderStateService,
                _dateTimeService).Returns(order);

            //ACT
            var result = await _factory.CreateOrderAsync(
                orderId,
                requestedDeliveryDate,
                location.Id.Value,
                orderItemPrimitives,
                CancellationToken.None
                );


            //ASSERT
            _domainFactory.Received(1).CreateOrder(
                Arg.Any<OrderId>(),
                Arg.Any<LocationVO>(),
                Arg.Any<List<OrderItem>>(),
                Arg.Any<DateTime>(),
                Arg.Any<DateTime>(),
                _orderStateService,
                _dateTimeService);
            result.ShouldNotBeNull();
            result.ShouldBe(order);
        }


        [Fact]
        public async Task CreateOrderTemplateAsync_Uses_DomanFactory_To_Create_OrderTemplate()
        {
            var orderId = new OrderId(1);
            var requestedDeliveryDate = DateTime.UtcNow;
            var requestedLocationId = 1;
            var orderItems = GetOrderItems();
            var orderItemPrimitives = GetOrderItemPrimitives();

            var location = GetLocation();
            _locationRepository.GetLocationBySpecyfictionAsync(Arg.Any<ISpecyfication<Location>>(), Arg.Any<CancellationToken>()).Returns(location);

            _orderItemsFactory.CreateOrderItemsAsync(
                Arg.Any<List<OrderItemPrimitive>>(),
                Arg.Any<CancellationToken>())
                .Returns(orderItems);

            var orderTemplate = Substitute.For<OrderTemplate>();//spóbowac zastąpić orderItems
            _domainFactory.CreateOrderTemplate(
                Arg.Any<OrderId>(),
                Arg.Any<DateTime>(),
                Arg.Any<LocationVO>(),
                Arg.Any<List<OrderItem>>(),
                Arg.Any<string>())
                .Returns(orderTemplate);

            //ACT
            var result = await _factory.CreateOrderTemplateAsync(
                orderId,
                requestedLocationId,
                orderItemPrimitives,
                "Some name",
                CancellationToken.None
                );


            //ASSERT
            _domainFactory.Received(1).CreateOrderTemplate(
                Arg.Any<OrderId>(),
               Arg.Any<DateTime>(),
                Arg.Any<LocationVO>(), 
                Arg.Any<List<OrderItem>>(),
                Arg.Any<string>());

            result.ShouldNotBeNull();
            result.ShouldBe(orderTemplate);
        }



        private static Location GetLocation()
        {
            return new Location(1, "Line 1", "L001", string.Empty, LocationTypeEnum.Line,null);
        }

        private static List<OrderItemPrimitive> GetOrderItemPrimitives()
        { 
            var oi = new List<OrderItemPrimitive>();
            oi.Add(new OrderItemPrimitive(1, 2));
            return oi;
        }
        private static List<OrderItem> GetOrderItems()
        {
            var oi = new List<OrderItem>();
            oi.Add(new OrderItem(new ItemVO("Tape", "T001", "Tapes", "T001"),1));
            return oi;
        }

        private readonly IOrderFactory _domainFactory;
        private readonly IOrderStateService _orderStateService;
        private readonly ILocationRepository _locationRepository;
        private readonly IOrderItemsFactory _orderItemsFactory;
        private readonly IDateTimeService _dateTimeService;
        private readonly IOrderApplicationFactroy _factory;
        public OrderApplicationFactroyTests()
        {
            _domainFactory = Substitute.For<IOrderFactory>();
            _orderStateService = Substitute.For<IOrderStateService>();
            _locationRepository = Substitute.For<ILocationRepository>();
            _orderItemsFactory = Substitute.For<IOrderItemsFactory>();
            _dateTimeService = Substitute.For<IDateTimeService>();

            _factory = new OrderApplicationFactroy(_domainFactory, _orderStateService, _locationRepository, _orderItemsFactory, _dateTimeService);
        }

    }
}
