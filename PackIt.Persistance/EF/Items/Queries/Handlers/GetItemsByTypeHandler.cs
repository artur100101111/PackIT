using Microsoft.EntityFrameworkCore;
using PackIt.Application.Items.DTO;
using PackIt.Application.Items.Queries;
using PackIt.Persistance.EF.Contexts;
using PackIt.Persistance.EF.Items.ReadModels;
using PackIt.Shared.Abstractions.Queries;

namespace PackIt.Persistance.EF.Items.Queries.Handlers
{
    internal class GetItemsByTypeHandler : IQueryHandler<GetItemsByTypeQuery, IEnumerable<ItemDto>>
    {
        private DbSet<ItemReadModel> _items;

        public GetItemsByTypeHandler(ReadDbContext readDbContext)
        {
            _items = readDbContext.Items;
        }
        public async Task<IEnumerable<ItemDto>> HandleAsync(GetItemsByTypeQuery query, CancellationToken cancellationToken)
        {
            return await _items.Where(it=>it.TypeId == query.TypeId)
                .Include(t=>t.Type)
                .Select(it=>it.AsDto())
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
    }
}
