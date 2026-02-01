
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
    internal class GetOrderTemplateByIdQueryHandler : IQueryHandler<GetOrderTemplateByIdQuery, OrderTemplateDto>
    {
        private DbSet<OrderTemplateReadModel> _orderTemplates;
        private IMapper _mapper;
        private IDateTimeService _dateTimeService;

        public GetOrderTemplateByIdQueryHandler(ReadDbContext readDbContext, IMapper mapper, IDateTimeService dateTimeService)
        {
            _orderTemplates = readDbContext.OrderTemplates;
            _mapper = mapper;
            _dateTimeService = dateTimeService;
        }
        public async Task<OrderTemplateDto?> HandleAsync(GetOrderTemplateByIdQuery query, CancellationToken cancellationToken)
        {
            var orderTemplate = await _orderTemplates.Where(o => o.Id == query.OrderId)
                .Include(i => i.OrderItems)
                .ProjectTo<OrderTemplateDto>(_mapper.ConfigurationProvider)
                .AsNoTracking()
                .SingleOrDefaultAsync(cancellationToken);
            if (orderTemplate != null)
            {
                orderTemplate.CreationDate = _dateTimeService.ToLocal(orderTemplate.CreationDate);
            }
            return orderTemplate;
        }
    }
}
