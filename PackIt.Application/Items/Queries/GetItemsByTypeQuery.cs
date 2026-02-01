using PackIt.Application.Items.DTO;
using PackIt.Shared.Abstractions.Queries;

namespace PackIt.Application.Items.Queries
{
    public class GetItemsByTypeQuery:IQuery<IEnumerable<ItemDto>>
    {
        public required long TypeId { get; set; }
    }
}
