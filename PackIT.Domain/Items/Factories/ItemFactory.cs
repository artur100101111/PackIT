using PackIT.Domain.ItemTypes;

namespace PackIT.Domain.Items.Factories
{

    public class ItemFactory: IItemFactory
    {
        public Item CreateItem(ItemId id, ItemName name, ItemCode code, ItemType itemType)
        { 
          return new Item(id, name, code, itemType);
        }

    }
}
