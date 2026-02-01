using PackIt.Application.Items.DTO;
using PackIt.Shared.Abstractions.Queries;

namespace PackIt.Application.ItemTypes.Queries
{
    public class GetItemTypeByCodeQuery: IQuery<ItemTypeDto>
    {
        public required string Code { get; set; }
    }
}
