using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PackIt.Application.Orders.DTO;
using PackIt.Application.Orders.Queries;
using PackIt.Persistance.EF.Contexts;
using PackIt.Persistance.EF.Orders.ReadModels;
using PackIt.Shared.Abstractions.Queries;
using PackIT.Domain.Shared;

namespace PackIt.Persistance.EF.Orders.Queries.Handlers
{
    internal class GetOrderByIdQueryHandler : IQueryHandler<GetOrderByIdQuery, OrderDto>
    {
        private DbSet<OrderReadModel> _orders;
        private IMapper _mapper;
        private IDateTimeService _dateTimeService;

        public GetOrderByIdQueryHandler(ReadDbContext readDbContext, IMapper mapper, IDateTimeService dateTimeService)
        {
            _orders = readDbContext.Orders;
            _mapper = mapper;
            _dateTimeService = dateTimeService;
        }
        public async Task<OrderDto?> HandleAsync(Application.Orders.Queries.GetOrderByIdQuery query, CancellationToken cancellationToken)
        {
            var order = await _orders.Where(o => o.Id == query.OrderId)
                .Include(h=>h.StateChangesHistory)
                .Include(i=>i.OrderItems)
                .AsNoTracking()
                .SingleOrDefaultAsync();


            var orderDto = _mapper.Map<OrderDto>(order);
            if (orderDto != null)
            {
                orderDto.CreationDate = _dateTimeService.ToLocal(orderDto.CreationDate);
                if (orderDto.DeliveryDate != null)
                {
                    orderDto.DeliveryDate = _dateTimeService.ToLocal(orderDto.DeliveryDate.Value);
                }
                foreach(var stateChange in orderDto.StateChangesHistory)
                {
                    stateChange.EventTime = _dateTimeService.ToLocal(stateChange.EventTime);
                }
            }

            return orderDto; 
        }
    }
}
