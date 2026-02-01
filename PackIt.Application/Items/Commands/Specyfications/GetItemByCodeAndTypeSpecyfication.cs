using PackIt.Shared.Abstractions.Shared;
using PackIT.Domain.Items;
using System.Linq.Expressions;

namespace PackIt.Application.Items.Commands.Specyfications
{
    internal class GetItemByCodeSpecyfication : ISpecyfication<Item>
    {
        public Expression<Func<Item, bool>> Criteria => i=>i.Code == ItemCode;// dlacego to nie jest tłumaczone i nie działa

        public List<Expression<Func<Item, object>>> Includes => new();

        public string ItemCode { get; }

        public GetItemByCodeSpecyfication(string itemCode)
        {
            ItemCode = itemCode;
        }
    }
}