using PackIt.Application.Orders.Commands.Specyfications;
using PackIt.Application.Orders.Exceptions;
using PackIt.Shared.Abstractions.Commands;
using PackIt.Shared.Abstractions.Persistance;
using PackIT.Domain.Orders;
using PackIT.Domain.Orders.States;

namespace PackIt.Application.Orders.Commands.DeleteOrder
{
    internal class DeleteOrderHandler : ICommandHandler<DeleteOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork unitOfWork;

        public DeleteOrderHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task HandleAsync(DeleteOrderCommand command, CancellationToken cancellationToken)
        {
            var OrderId = new OrderId(command.OrderId);

            var order = await _orderRepository.GetOrderBySpecyfictionAsync(new GetOrderByIdSpecyfication(OrderId), cancellationToken);
            if (order == null) throw new OrderNotFoundException($"Order with Id: {OrderId} was not found.");

            if (order.State != OrderStateEnum.New)
                throw new OrderCannotBeDeletedException($"Order Id: {order.Id} is in State: {order.State} and cannot be deleted.");

            await _orderRepository.DeleteAsync(order, cancellationToken);
            await unitOfWork.SaveAsync(cancellationToken);
        }
    }
}
