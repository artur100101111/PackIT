using PackIt.Application.Items.DTO;
using PackIt.Shared.Abstractions.Queries;

namespace PackIt.Application.ItemTypes.Queries
{
    public class SearchItemTypeByNameQuery : IQuery<IEnumerable<ItemTypeDto>>
    {
        public required string SearchPhrase { get; set; }
    }
}
