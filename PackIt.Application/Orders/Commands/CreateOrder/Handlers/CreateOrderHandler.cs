using PackIt.Application.Orders.Exceptions;
using PackIt.Application.Orders.Factories;
using PackIt.Application.Services;
using PackIt.Shared.Abstractions.Commands;
using PackIt.Shared.Abstractions.Persistance;
using PackIT.Domain.Orders;
using PackIT.Domain.Orders.Primitives;

namespace PackIt.Application.Orders.Commands.CreateOrder.Handlers
{
    internal class CreateOrderHandler : ICommandHandler<CreateOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderApplicationFactroy _orderFactory;
        private readonly ISnowflakeIdGenerator _idGenerator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderReadService _orderReadService;

        public CreateOrderHandler(IOrderRepository orderRepository, IOrderReadService orderReadService,
            IUnitOfWork unitOfWork, IOrderApplicationFactroy orderFactory, ISnowflakeIdGenerator idGenerator)
        {
            _orderRepository = orderRepository;
            _orderFactory = orderFactory;
            _idGenerator = idGenerator;
            _unitOfWork = unitOfWork;
            _orderReadService = orderReadService;
        }

        public async Task HandleAsync(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            //pipeline for Logging, Command Validation, Transaction, Save Steps? 
            command.OrderId = _idGenerator.CreateId();
            var(OrderId, RequestedDeliveryTime, OrderItemsRequest, RequestedLocationId) 
                = (command.OrderId, command.RequestedDeliveryTime, command.OrderItemRequests, command.RequestedLocationId);

            var orderItemPrimitives = OrderItemsRequest.Select(x => new OrderItemPrimitive(x.ItemId, x.Quantity)).ToList();
            Order order = await _orderFactory.CreateOrderAsync(OrderId, RequestedDeliveryTime, RequestedLocationId, orderItemPrimitives, cancellationToken);

            var orderAlreadyExists = await _orderReadService.ExistsByLocationStatusItemsAsync(
                order.State, order.RequestedDeliveryLocation.Code,order.RequestedDeliveryTime, order.OrderItems.ToList(), cancellationToken);

            if (orderAlreadyExists) throw new OrderAlreadyExistsException($"New Order with the same set of Items, and Request Location:{{{order.RequestedDeliveryLocation}}} already exists.");
    
            await _orderRepository.AddAsync(order, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);

   
        }
    }
}
