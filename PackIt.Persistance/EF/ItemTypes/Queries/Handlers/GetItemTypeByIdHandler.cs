using Microsoft.EntityFrameworkCore;
using PackIt.Application.Items.DTO;
using PackIt.Application.ItemTypes.Queries;
using PackIt.Persistance.EF.Contexts;
using PackIt.Persistance.EF.ItemTypes.ReadModels;
using PackIt.Shared.Abstractions.Queries;

namespace PackIt.Persistance.EF.ItemTypes.Queries.Handlers
{

    internal class GetItemTypeByIdHandler : IQueryHandler<GetItemTypeByIdQuery, ItemTypeDto>
    {
        private DbSet<ItemTypeReadModel> _itemTypes;

        public GetItemTypeByIdHandler(ReadDbContext readDbContext)
        {
            _itemTypes = readDbContext.ItemTypes;
        }

        public async Task<ItemTypeDto?> HandleAsync(GetItemTypeByIdQuery query, CancellationToken cancellationToken)
        {
            return await _itemTypes.Where(it => it.Id == query.ItemTypeId)
                 .Select(it => it.AsDto())
                 .AsNoTracking()
                 .SingleOrDefaultAsync(cancellationToken);
        }
    }
}
