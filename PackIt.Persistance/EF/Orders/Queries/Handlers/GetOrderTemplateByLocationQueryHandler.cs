using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PackIt.Application.Orders.DTO;
using PackIt.Application.Orders.Queries;
using PackIt.Persistance.EF.Contexts;
using PackIt.Persistance.EF.Orders.ReadModels;
using PackIt.Shared.Abstractions.Queries;
using PackIT.Domain.Shared;

namespace PackIt.Persistance.EF.Orders.Queries.Handlers
{
    internal class GetOrderTemplateByLocationQueryHandler : IQueryHandler<GetOrderTemplateByLocationQuery, IEnumerable<OrderTemplateDto>>
    {

        private DbSet<OrderTemplateReadModel> _orderTemplates;
        private IMapper _mapper;
        private IDateTimeService _dateTimeService;

        public GetOrderTemplateByLocationQueryHandler(ReadDbContext readDbContext, IMapper mapper, IDateTimeService dateTimeService)
        {
            _orderTemplates = readDbContext.OrderTemplates;
            _mapper = mapper;
            _dateTimeService = dateTimeService;
        }

        public async Task<IEnumerable<OrderTemplateDto>> HandleAsync(GetOrderTemplateByLocationQuery query, CancellationToken cancellationToken)
        {
            var orderTemplates = await _orderTemplates.Where(o => o.RequestedDeliveryLocation.Code == query.LocationCode)
                .ProjectTo<OrderTemplateDto>(_mapper.ConfigurationProvider)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            foreach (var order in orderTemplates)
            {
                order.CreationDate = _dateTimeService.ToLocal(order.CreationDate);
            }
            return orderTemplates;
        }
    }
}
