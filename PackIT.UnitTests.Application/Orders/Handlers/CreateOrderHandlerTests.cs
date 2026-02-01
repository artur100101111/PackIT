using NSubstitute;
using NSubstitute.ExceptionExtensions;
using PackIt.Application.Items.Exceptions;
using PackIt.Application.Locations.Exceptions;
using PackIt.Application.Orders;
using PackIt.Application.Orders.Commands.CreateOrder;
using PackIt.Application.Orders.Commands.CreateOrder.Handlers;
using PackIt.Application.Orders.Commands.Requests;
using PackIt.Application.Orders.Exceptions;
using PackIt.Application.Orders.Factories;
using PackIt.Application.Services;
using PackIt.Shared.Abstractions.Commands;
using PackIt.Shared.Abstractions.Persistance;
using PackIT.Domain.Orders;
using PackIT.Domain.Orders.Primitives;
using PackIT.Domain.Orders.States;
using Shouldly;

namespace PackIT.UnitTests.Application.Orders.Handlers
{
    public  class CreateOrderHandlerTests
    {
         Task Act(CreateOrderCommand command) 
            =>  _commandHandler.HandleAsync(command, CancellationToken.None);


        [Fact]
        public async Task HandleAsync_Throws_When_OrderFactory_Throws_ItemNotFoundException()
        {
            var order = GetOrder(OrderStateEnum.New);
            CreateOrderCommand command = GetCommand(order);

            _snowflakeIdGenerator.CreateId().Returns(order.Id.Value);
            _orderFactory.CreateOrderAsync(
                Arg.Any<OrderId>(),
                Arg.Any<DateTime>(),
                Arg.Any<long>(),
                Arg.Any<List<OrderItemPrimitive>>(),
                Arg.Any<CancellationToken>())
                .Throws(new ItemNotFoundException(string.Empty));

            //ACT

            var exception = await Record.ExceptionAsync(()=> Act(command));

            //ASSERT

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<ItemNotFoundException>();
        }

        [Fact]
        public async Task  HadleAsync_Throws_When_Order_Factory_Throws_LocationNotFoundException()
        {
            var order = GetOrder(OrderStateEnum.New);
            CreateOrderCommand command = GetCommand(order);

            _snowflakeIdGenerator.CreateId().Returns(order.Id.Value);
            _orderFactory.CreateOrderAsync(
                Arg.Any<OrderId>(),
                Arg.Any<DateTime>(),
                Arg.Any<long>(),
                Arg.Any<List<OrderItemPrimitive>>(),
                Arg.Any<CancellationToken>())
                .Throws(new LocationNotFoundException(string.Empty));

            //ACT
            var exception = await Record.ExceptionAsync(()=> Act(command)); 

            //ASSERT

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<LocationNotFoundException>();
        }

        [Fact]
        public async Task Handle_Async_Throws_OrderAlreadyExistsException_When_Order_With_The_Same_CreationDate_DeliveryLocation_Items_AlreadyExists()
        {
            var order = GetOrder(OrderStateEnum.New);
            CreateOrderCommand command = GetCommand(order);


            _orderFactory.CreateOrderAsync(
                 Arg.Any<OrderId>(),
                 Arg.Any<DateTime>(),
                 Arg.Any<long>(),
                 Arg.Any<List<OrderItemPrimitive>>(),
                 Arg.Any<CancellationToken>())
                 .Returns(order);

            _snowflakeIdGenerator.CreateId().Returns(order.Id.Value);
            _orderReadService.ExistsByLocationStatusItemsAsync(
                Arg.Any<OrderStateEnum>(),
                Arg.Any<string>(),
                Arg.Any<DateTime>(),
                Arg.Any<List<OrderItem>>(),
                Arg.Any<CancellationToken>()
                ).Returns(true);

            //ACT
            var exception = await Record.ExceptionAsync( ()=> Act(command));

            //ASSERT
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<OrderAlreadyExistsException>();
        }

        /// <summary>
        /// Handler persists the order and commmits the transaction
        /// </summary>
        [Fact]
        public async Task HandleAsync_Persists_Order_And_Commits_On_Success()
        {
            var order = GetOrder(OrderStateEnum.New);
            CreateOrderCommand command = GetCommand(order);

            _snowflakeIdGenerator.CreateId().Returns(order.Id.Value);

            _orderFactory.CreateOrderAsync(
                  Arg.Any<OrderId>(),
                  Arg.Any<DateTime>(),
                  Arg.Any<long>(),
                  Arg.Any<List<OrderItemPrimitive>>(),
                  Arg.Any<CancellationToken>())
              .Returns(order);

            _orderReadService.ExistsByLocationStatusItemsAsync(
                  Arg.Any<OrderStateEnum>(),
                  Arg.Any<string>(),
                  Arg.Any<DateTime>(),
                  Arg.Any<List<OrderItem>>(),
                  Arg.Any<CancellationToken>()
                ).Returns(false);

            //ACT
            var exception = await Record.ExceptionAsync(() => Act(command));


            //ASSERT
            exception.ShouldBeNull();

            await _orderRepository.Received(1).AddAsync(order, Arg.Any<CancellationToken>());
            await _unitOfWork.Received(1).SaveAsync();
        }

        private static CreateOrderCommand GetCommand(Order order)
        {
            var orderItemRequest = new List<OrderItemRequest>();
            orderItemRequest.Add(new OrderItemRequest(1, 1));
            var requestLocationId = 1;
            var command = new CreateOrderCommand(order.Id, order.RequestedDeliveryTime, orderItemRequest, requestLocationId);
            return command;
        }

        #region arrange
        private readonly ICommandHandler<CreateOrderCommand> _commandHandler;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderReadService _orderReadService;
        private readonly ISnowflakeIdGenerator _snowflakeIdGenerator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderApplicationFactroy _orderFactory;

        public CreateOrderHandlerTests()
        {
            _orderRepository = Substitute.For<IOrderRepository>();
            _orderReadService= Substitute.For<IOrderReadService>();
            _snowflakeIdGenerator =Substitute.For<ISnowflakeIdGenerator>();
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _orderFactory = Substitute.For<IOrderApplicationFactroy>();

            _commandHandler = new CreateOrderHandler(_orderRepository, _orderReadService, _unitOfWork, _orderFactory, _snowflakeIdGenerator);
        }

        private Order GetOrder(OrderStateEnum state)
        { 
            return OrderFactoryHelper.CreateOrder(state);
        }


        #endregion

    }
}
