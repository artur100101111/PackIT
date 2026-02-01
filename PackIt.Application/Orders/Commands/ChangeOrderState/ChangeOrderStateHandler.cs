using PackIt.Application.Locations.Commands.Specyfications;
using PackIt.Application.Orders.Commands.Specyfications;
using PackIt.Application.Orders.Exceptions;
using PackIt.Shared.Abstractions.Commands;
using PackIt.Shared.Abstractions.Persistance;
using PackIT.Domain.Locations.Repository;
using PackIT.Domain.Orders;
using PackIT.Domain.Orders.States;
using PackIT.Domain.Orders.ValueObjects;
using PackIT.Domain.Shared;

namespace PackIt.Application.Orders.Commands.ChangeOrderState
{

    /// <summary>
    /// For DDD all status change sould be separate command like "CancelOrder: IOrderStatus", "MarkOrderAsPacked" etc....  for readability, Ubiquitius language, 1 UseCase <-> 1 StateChange(orderStatus)
    /// but in this case for simplicity one handler. // Update -this simplification may be misleading ...
    /// For example .... if there is OrderState->Delivered, additional dependencies would be needed, like -> ILOcationRepository to get deliveryLocation.
    /// and additional conditions/validation of command is required, etc. 
    /// </summary>
    public class ChangeOrderStateHandler : ICommandHandler<ChangeOrderStateCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IDateTimeService _dateTimeService;
        private readonly ILocationRepository _locationRepository;
        private readonly IOrderStateService _orderStateService;

        public ChangeOrderStateHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork, IDateTimeService dateTimeService, IOrderStateService orderStateService, ILocationRepository locationRepository)
        {
            _orderRepository = orderRepository;
            this.unitOfWork = unitOfWork;
            _dateTimeService = dateTimeService;
            _locationRepository = locationRepository;
            _orderStateService = orderStateService;
        }

        public async Task HandleAsync(ChangeOrderStateCommand command, CancellationToken cancellationToken)
        {
            var OrderId = new OrderId(command.OrderId);
            var newOrderState = command.NewOrderState;
            var deliveryLocationId = command.DeliveryLocationId;
            LocationVO? deliveryLocationVO = null;

            var order = await _orderRepository.GetOrderBySpecyfictionAsync(new GetOrderByIdSpecyfication(OrderId), cancellationToken);
            if (order == null) throw new OrderNotFoundException($"Order with Id: {OrderId} was not found.");

            //try to change state 
            //validtor can be used. .. 
            if (newOrderState == OrderStateEnum.Delivered)
            {
                if (deliveryLocationId is long locationId)
                {
                     var location = await _locationRepository.GetLocationBySpecyfictionAsync(new GetLocationByIdSpecyfication(locationId), cancellationToken);
                    if (location == null)
                    {
                        throw new DeliveryLocationNotFoundException($"Delivery Location Id:{locationId} was not found.");
                    }
                    deliveryLocationVO = new LocationVO(location.Name, location.Code, location.Type.ToString());
                }
                else
                {
                    throw new DeliveryLocaitonIdCannotBeNullException($"{nameof(ChangeOrderStateCommand)} " +
                        $"for Order Id: {OrderId} New State: { newOrderState}, has no deliveryLocationId value.");
                }
            }



            order.SetDelivery(deliveryLocationVO!, _dateTimeService);
            order.SetState(_orderStateService, newOrderState, _dateTimeService);

            await _orderRepository.UpdateAsync(order, cancellationToken);
            await unitOfWork.SaveAsync(cancellationToken);
        }
    }
}
