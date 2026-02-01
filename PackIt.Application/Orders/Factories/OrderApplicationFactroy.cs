using PackIt.Application.Locations.Commands.Specyfications;
using PackIt.Application.Locations.Exceptions;
using PackIT.Domain.Locations;
using PackIT.Domain.Locations.Repository;
using PackIT.Domain.Orders;
using PackIT.Domain.Orders.Factory;
using PackIT.Domain.Orders.Primitives;
using PackIT.Domain.Orders.States;
using PackIT.Domain.Orders.ValueObjects;
using PackIT.Domain.Shared;

namespace PackIt.Application.Orders.Factories
{
    internal class OrderApplicationFactroy: IOrderApplicationFactroy
    {
        private ILocationRepository _locationRepository;
        private IOrderStateService _orderStateService;
        private IOrderItemsFactory _orderItemsFactory;
        private IDateTimeService _dateTimeService;
        private IOrderFactory _domainFactory;

        public OrderApplicationFactroy(IOrderFactory domainFactory, IOrderStateService orderStateService, ILocationRepository locationRepository,
            IOrderItemsFactory orderItemsFactory, IDateTimeService dateTimeService)
        {
            _locationRepository = locationRepository;
            _orderStateService = orderStateService;
            _orderItemsFactory = orderItemsFactory;
            _dateTimeService = dateTimeService;
            _domainFactory = domainFactory;
        }
        public async Task<Order> CreateOrderAsync(OrderId id, DateTime requestedDeliveryDate,
            long requestedLocationId, List<OrderItemPrimitive> orderItemPrimitives, CancellationToken cancellationToken)
        {
            var requestedLocation = await GetRequestedDeliveryLocationAsync(requestedLocationId, cancellationToken);

            var orderItems = await _orderItemsFactory.CreateOrderItemsAsync(orderItemPrimitives, cancellationToken);

            var locationVO = new LocationVO(requestedLocation.Name, requestedLocation.Code, requestedLocation.Type.ToString());
            var order = _domainFactory.CreateOrder(id, locationVO,orderItems,_dateTimeService.UtcNow, requestedDeliveryDate,_orderStateService,_dateTimeService);

            return order;
        }

        public async Task<OrderTemplate> CreateOrderTemplateAsync(OrderId id, long requestedLocationId, 
            List<OrderItemPrimitive> orderItemPrimitives, string orderName, CancellationToken cancellationToken)
        {
            var requestedLocation = await GetRequestedDeliveryLocationAsync(requestedLocationId, cancellationToken);

            var orderItems = await _orderItemsFactory.CreateOrderItemsAsync(orderItemPrimitives, cancellationToken);

            var requestedLocationVO = new LocationVO(requestedLocation.Name, requestedLocation.Code, requestedLocation.Type.ToString());
            var orderTemplate = _domainFactory.CreateOrderTemplate(id, _dateTimeService.UtcNow, requestedLocationVO, orderItems, orderName);

            return orderTemplate;
        }

        private async Task<Location> GetRequestedDeliveryLocationAsync(long locationId, CancellationToken cancellationToken)
        { 
            LocationId id = new LocationId(locationId);
            var requestedDeliveryLocation = await _locationRepository.GetLocationBySpecyfictionAsync(new GetLocationByIdSpecyfication(id), 
                cancellationToken);
            if (requestedDeliveryLocation == null) throw new LocationNotFoundException($"Location with Id: {id} was not found.");
            return requestedDeliveryLocation;
        }
    }
}
