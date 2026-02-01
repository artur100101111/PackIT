using Microsoft.EntityFrameworkCore;
using PackIt.Application.Items.DTO;
using PackIt.Application.ItemTypes.Queries;
using PackIt.Persistance.EF.Contexts;
using PackIt.Persistance.EF.ItemTypes.ReadModels;
using PackIt.Shared.Abstractions.Queries;

namespace PackIt.Persistance.EF.ItemTypes.Queries.Handlers
{
    internal class SearchItemTypeByNameHandler : IQueryHandler<SearchItemTypeByNameQuery, IEnumerable<ItemTypeDto>>
    {
        private DbSet<ItemTypeReadModel> _itemTypes;

        public SearchItemTypeByNameHandler(ReadDbContext readDbContext)
        {
            _itemTypes = readDbContext.ItemTypes;
        }
        public async Task<IEnumerable<ItemTypeDto>> HandleAsync(SearchItemTypeByNameQuery query, CancellationToken cancellationToken)
        {
            return await _itemTypes.Where(it => it.Name.Contains(query.SearchPhrase))
                .Select(it => it.AsDto())
                .AsNoTracking().
                ToListAsync(cancellationToken);
        }
    }
}
