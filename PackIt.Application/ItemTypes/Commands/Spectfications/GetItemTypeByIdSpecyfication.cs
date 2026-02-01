using PackIt.Shared.Abstractions.Shared;
using PackIT.Domain.ItemTypes;
using System.Linq.Expressions;

namespace PackIt.Application.ItemTypes.Commands.Spectfications
{
    public class GetItemTypeByIdSpecyfication : ISpecyfication<ItemType>
    {
        public long ItemTypeId { get; }

        public GetItemTypeByIdSpecyfication(long itemTypeId)
        {
            ItemTypeId = itemTypeId;

        }
        public Expression<Func<ItemType, bool>> Criteria => it => it.Id == ItemTypeId;
        public List<Expression<Func<ItemType, object>>> Includes => new();
    }
}
