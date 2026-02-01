using PackIt.Application.Items.DTO;
using PackIt.Shared.Abstractions.Queries;

namespace PackIt.Application.ItemTypes.Queries
{
    public record GetItemTypeByIdQuery(long ItemTypeId) : IQuery<ItemTypeDto>;

}
