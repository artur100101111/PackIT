using PackIt.Shared.Abstractions.Shared;
using PackIT.Domain.Items;
using System.Linq.Expressions;

namespace PackIt.Application.Items.Commands.Specyfications
{
    internal class GetItemsByIDListSpecyfication : ISpecyfication<Item>
    {
        private List<ItemId> _itemIds = new();

        public IReadOnlyList<ItemId> ItemIds => _itemIds.AsReadOnly();

        public Expression<Func<Item, bool>> Criteria => i => _itemIds.Contains(i.Id);

        public List<Expression<Func<Item, object>>> Includes => [i=>i.Type];

        public GetItemsByIDListSpecyfication(List<long> itemIds)
        {
            foreach (var itemId in itemIds)
            {
                _itemIds.Add(new ItemId(itemId));
            }
        }
    }
}
