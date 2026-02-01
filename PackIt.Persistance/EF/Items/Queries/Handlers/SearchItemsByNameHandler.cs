using Microsoft.EntityFrameworkCore;
using PackIt.Application.Items.DTO;
using PackIt.Application.Items.Queries;
using PackIt.Persistance.EF.Contexts;
using PackIt.Persistance.EF.Items.ReadModels;
using PackIt.Shared.Abstractions.Queries;

namespace PackIt.Persistance.EF.Items.Queries.Handlers
{
    internal class SearchItemsByNameHandler : IQueryHandler<SearchItemsByNameQuery, IEnumerable<ItemDto>>
    {
        private DbSet<ItemReadModel> _items;

        public SearchItemsByNameHandler(ReadDbContext readDbContext)
        {
            _items = readDbContext.Items;
        }
        public async Task<IEnumerable<ItemDto>> HandleAsync(SearchItemsByNameQuery query, CancellationToken cancellationToken)
        {
            //chech what Query will be generated.with Contains.
            return await _items.Where(it => it.Name.Contains(query.SearchPhrase))
                .Include(it => it.Type)
                .Select(it => it.AsDto())
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
    }
}
