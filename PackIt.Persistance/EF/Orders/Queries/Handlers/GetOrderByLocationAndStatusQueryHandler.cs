using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PackIt.Application.Orders.DTO;
using PackIt.Persistance.EF.Contexts;
using PackIt.Persistance.EF.Orders.ReadModels;
using PackIt.Shared.Abstractions.Queries;
using PackIT.Domain.Shared;
using PackIt.Application.Orders.Queries;

namespace PackIt.Persistance.EF.Orders.Queries.Handlers
{
    internal class GetOrderByLocationAndStatusQueryHandler : IQueryHandler<GetOrderByRequestedLocationAndStatusQuery, IEnumerable<OrderDto>>
    {
        private DbSet<OrderReadModel> _orders;
        private IMapper _mapper;
        private IDateTimeService _dateTimeService;

        public GetOrderByLocationAndStatusQueryHandler(ReadDbContext readDbContext, IMapper mapper, IDateTimeService dateTimeService)
        {
            _orders = readDbContext.Orders;
            _mapper = mapper;
            _dateTimeService = dateTimeService;
        }
        public async Task<IEnumerable<OrderDto>> HandleAsync(GetOrderByRequestedLocationAndStatusQuery query, CancellationToken cancellationToken)
        {
            var orders = await _orders.Where(o => o.RequestedDeliveryLocation.Code == query.LocationCode && o.State == query.OrderState)
                .ProjectTo<OrderDto>(_mapper.ConfigurationProvider)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            foreach (var order in orders)
            {
                order.CreationDate = _dateTimeService.ToLocal(order.CreationDate);
                if (order.DeliveryDate != null) order.DeliveryDate = _dateTimeService.ToLocal(order.DeliveryDate.Value);
            }

            return orders;
        }

    }
}
