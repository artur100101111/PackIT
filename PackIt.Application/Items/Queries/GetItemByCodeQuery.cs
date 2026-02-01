using PackIt.Application.Items.DTO;
using PackIt.Shared.Abstractions.Queries;

namespace PackIt.Application.Items.Queries
{
    public class GetItemByCodeQuery: IQuery<ItemDto>
    {
        public required string Code { get; set; }
    }
}
