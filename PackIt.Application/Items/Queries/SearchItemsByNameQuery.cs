using PackIt.Application.Items.DTO;
using PackIt.Shared.Abstractions.Queries;

namespace PackIt.Application.Items.Queries
{
    public class SearchItemsByNameQuery : IQuery<IEnumerable<ItemDto>>
    {
        public required string SearchPhrase { get; set; }
    }
}
