using PackIT.Domain.Common;
using PackIT.Domain.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackIT.Domain.ItemTypes
{

    /// <summary>
    /// to define type of the item like: Package, Product, Component, Scrap. 
    /// </summary>
    public class ItemType :AggregateRoot<ItemTypeId>, IEntity<ItemTypeId>
    {
        public ItemTypeId Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public ItemType()
        {
            
        }

        public ItemType(ItemTypeId id, string name, string code)
        {
            Id = id;
            Name = name;
            Code = code;
        }
        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}, Code: {Code}";
        }
    }
}

