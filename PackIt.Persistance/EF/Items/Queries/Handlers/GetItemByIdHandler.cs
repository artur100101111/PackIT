using Microsoft.EntityFrameworkCore;
using PackIt.Application.Items.DTO;
using PackIt.Application.Items.Queries;
using PackIt.Persistance.EF.Contexts;
using PackIt.Persistance.EF.Items.ReadModels;
using PackIt.Shared.Abstractions.Queries;

namespace PackIt.Persistance.EF.Items.Queries.Handlers
{
    internal class GetItemByIdHandler : IQueryHandler<GetItemByIdQuery, ItemDto>
    {
        private DbSet<ItemReadModel> _items;

        public GetItemByIdHandler(ReadDbContext readDbContext)
        {
            _items = readDbContext.Items;
        }

        public async Task<ItemDto?> HandleAsync(GetItemByIdQuery query , CancellationToken cancellationToken)
        {
            return await _items.Where(it => it.Id == query.ItemId)
           .Include(t => t.Type)
           .Select(it => it.AsDto())
           .AsNoTracking()
           .SingleOrDefaultAsync(cancellationToken);
        }
    }
}
