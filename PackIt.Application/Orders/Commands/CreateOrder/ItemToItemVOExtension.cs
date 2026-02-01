using PackIT.Domain.Items;
using PackIT.Domain.Orders.ValueObjects;

namespace PackIt.Application.Orders.Commands.CreateOrder
{
    static internal class ItemToItemVOExtension
    {
        internal static ItemVO ToValueObject(this Item item)
        {
            return new ItemVO(item.Name.Value, item.Code.Value, item.Type.Name, item.Type.Code);
        
        }
    }
}
