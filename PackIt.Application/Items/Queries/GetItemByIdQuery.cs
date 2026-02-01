using PackIt.Application.Items.DTO;
using PackIt.Shared.Abstractions.Queries;

namespace PackIt.Application.Items.Queries
{
    public class GetItemByIdQuery: IQuery<ItemDto>
    {
        public required long ItemId { get; set; }
    }
}
