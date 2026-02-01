using Microsoft.EntityFrameworkCore;
using PackIt.Application.Items.DTO;
using PackIt.Application.ItemTypes.Queries;
using PackIt.Persistance.EF.Contexts;
using PackIt.Persistance.EF.ItemTypes.ReadModels;
using PackIt.Shared.Abstractions.Queries;

namespace PackIt.Persistance.EF.ItemTypes.Queries.Handlers
{
    internal class GetItemTypeByCodeHandler : IQueryHandler<GetItemTypeByCodeQuery, ItemTypeDto>
    {
        private DbSet<ItemTypeReadModel> _itemTypes;

        public GetItemTypeByCodeHandler(ReadDbContext readDbContext)
        {
            _itemTypes = readDbContext.ItemTypes;
        }
        public async Task<ItemTypeDto?> HandleAsync(GetItemTypeByCodeQuery query, CancellationToken cancellationToken)
        {
            return await _itemTypes.Where(it => it.Code == query.Code)
                .Select(it => it.AsDto())
                .AsNoTracking()
                .SingleOrDefaultAsync(cancellationToken);
        }
    }
}
