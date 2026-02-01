using PackIt.Application.Orders.Commands.Specyfications;
using PackIt.Application.Orders.Exceptions;
using PackIt.Application.Orders.Factories;
using PackIt.Application.Services;
using PackIt.Shared.Abstractions.Commands;
using PackIt.Shared.Abstractions.Persistance;
using PackIT.Domain.Orders.Primitives;

namespace PackIt.Application.Orders.Commands.CreateOrder.Handlers
{
    internal class CreateOrderTemplateHandler : ICommandHandler<CreateOrderTemplateCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderApplicationFactroy _orderFactory;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISnowflakeIdGenerator _idGenerator;

        public CreateOrderTemplateHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork, IOrderApplicationFactroy orderFactory, ISnowflakeIdGenerator idGenerator)
        {
            _orderRepository = orderRepository;
            _orderFactory = orderFactory;
            _unitOfWork = unitOfWork;
            _idGenerator = idGenerator;
        }
        public async Task HandleAsync(CreateOrderTemplateCommand command, CancellationToken cancellationToken)
        {
            command.OrderId = _idGenerator.CreateId();
            var (OrderId, OrderItemRequests, RequestedLocationId, OrderName) 
                = (command.OrderId, command.OrderItemRequests, command.RequesteLocationId, command.OrderName);

            var orderItemPrimitives = OrderItemRequests.Select(x => new OrderItemPrimitive(x.ItemId, x.Quantity)).ToList();
            var orderTemplate = await _orderFactory.CreateOrderTemplateAsync(OrderId, RequestedLocationId, orderItemPrimitives, OrderName, cancellationToken);

            var orderExists = await _orderRepository.CheckIfExistsAsync(new CheckIfOrderTemplateExistsByLocationAndName(OrderName, orderTemplate.RequestedDeliveryLocation.Code), cancellationToken) ;
            if (orderExists) throw new OrderTemplateAlreadyExistsException($"Order Template with name: {OrderName} and Requested Location Code: {orderTemplate.RequestedDeliveryLocation.Code} edists already.");

            await _orderRepository.AddAsync(orderTemplate, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);
        }
    }
}
