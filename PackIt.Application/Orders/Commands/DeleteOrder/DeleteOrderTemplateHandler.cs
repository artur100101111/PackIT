using PackIt.Application.Orders.Commands.Specyfications;
using PackIt.Application.Orders.Exceptions;
using PackIt.Shared.Abstractions.Commands;
using PackIt.Shared.Abstractions.Persistance;
using PackIT.Domain.Orders;


namespace PackIt.Application.Orders.Commands.DeleteOrder
{
    internal class DeleteOrderTemplateHandler: ICommandHandler<DeleteOrderTemplateCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork unitOfWork;

        public DeleteOrderTemplateHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task HandleAsync(DeleteOrderTemplateCommand command, CancellationToken cancellationToken)
        {
            var OrderId = new OrderId(command.OrderId);

            var orderTemplate = await _orderRepository.GetOrderBySpecyfictionAsync(new GetOrderTemplateByIdSpecyfication(OrderId), cancellationToken);
            if (orderTemplate == null) throw new OrderNotFoundException($"Order with Id: {OrderId} was not found.");

            await _orderRepository.DeleteAsync(orderTemplate, cancellationToken);
            await unitOfWork.SaveAsync(cancellationToken);
        }
    }
}
