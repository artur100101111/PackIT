using PackIT.Domain.Common;
using PackIT.Domain.ItemTypes;

namespace PackIT.Domain.Items

{
    public class Item : AggregateRoot<ItemId>, IEntity<ItemId>
    {
        public ItemId Id { get; set; }
        public ItemName Name { get;  set; } //item name as value object
        public ItemCode Code { get; set; }
        public ItemType Type { get; set; }
        public ItemTypeId TypeID { get; set; }
        private Item()
        {
            
        }
        internal Item(ItemId id, ItemName name, ItemCode code, ItemType itemType)
        {
            Type = itemType;
            Id = id; 
            Name=  name;
            Code = code;
        }

        public override string ToString()
        {
            return $"Id {Id}, Name: {Name}, Code: {Code} Type:{Type.ToString()}"; 
        }
    }
}
