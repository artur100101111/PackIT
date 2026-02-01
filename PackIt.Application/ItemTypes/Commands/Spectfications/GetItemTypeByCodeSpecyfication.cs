using PackIt.Shared.Abstractions.Shared;
using PackIT.Domain.ItemTypes;
using System.Linq.Expressions;

namespace PackIt.Application.ItemTypes.Commands.Spectfications
{
    internal class GetItemTypeByCodeSpecyfication : ISpecyfication<ItemType>
    {
        public string Code { get; }
        public Expression<Func<ItemType, bool>> Criteria => t => t.Code == Code;

        public List<Expression<Func<ItemType, object>>> Includes => new();

        public GetItemTypeByCodeSpecyfication(string typeCode)
        {
            Code = typeCode;
        }


    }
}