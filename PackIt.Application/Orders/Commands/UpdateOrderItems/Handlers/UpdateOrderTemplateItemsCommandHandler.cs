using PackIt.Application.Orders.Commands.Specyfications;
using PackIt.Application.Orders.Exceptions;
using PackIt.Application.Orders.Factories;
using PackIt.Shared.Abstractions.Commands;
using PackIt.Shared.Abstractions.Persistance;
using PackIT.Domain.Orders.Primitives;

namespace PackIt.Application.Orders.Commands.AddOrderItems.Handlers
{
    internal class UpdateOrderTemplateItemsCommandHandler: ICommandHandler<UpdateOrderTemplateItemsCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderItemsFactory _orderItemsFactory;

        public UpdateOrderTemplateItemsCommandHandler(IOrderRepository orderRepository, IOrderItemsFactory orderItemsFactory, IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
            _orderItemsFactory = orderItemsFactory;
        }
        public async Task HandleAsync(UpdateOrderTemplateItemsCommand command, CancellationToken cancellationToken)
        {
            var (OrderId, OrderItemsRequest) = command;

            var order = await _orderRepository.GetOrderBySpecyfictionAsync(new GetOrderTemplateByIdSpecyfication(OrderId), cancellationToken);
            if (order is null)
                throw new OrderNotFoundException($"OederTemplate with Id: {OrderId} was not found.");

            var orderItemPrimitives = OrderItemsRequest.Select(x => new OrderItemPrimitive(x.ItemId, x.Quantity)).ToList();
            var orderItems = await _orderItemsFactory.CreateOrderItemsAsync(orderItemPrimitives);

            order.UpdateItems(orderItems);

            await _orderRepository.UpdateAsync(order);
            await _unitOfWork.SaveAsync(cancellationToken);
        }
    }
}
