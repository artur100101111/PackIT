using PackIT.Domain.ItemTypes;

namespace PackIT.Domain.Items.Factories
{
    public interface IItemFactory
    {
        Item CreateItem(ItemId id, ItemName name, ItemCode code, ItemType itemType);
    }
}