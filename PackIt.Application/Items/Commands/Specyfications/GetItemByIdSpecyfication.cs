using PackIt.Shared.Abstractions.Shared;
using PackIT.Domain.Items;
using System.Linq.Expressions;

namespace PackIt.Application.Items.Commands.Specyfications
{
    internal class GetItemByIdSpecyfication : ISpecyfication<Item>
    {
        public Expression<Func<Item, bool>> Criteria => i => i.Id == ItemId;

        public List<Expression<Func<Item, object>>> Includes => [i =>i.Type];

        public ItemId ItemId { get; }

        public GetItemByIdSpecyfication(ItemId itemId)
        {
            ItemId = itemId;
        }
    }
}