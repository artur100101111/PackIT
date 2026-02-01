using PackIT.Domain.Orders.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackIT.Domain.ItemTypes
{
    public record ItemTypeId
    {
        public long Value { get; init; }

        public ItemTypeId(long value)
        {
            if (value < 1)
            {
                throw new OrderIdOutOfRangeException("ItemType Id value must be greater then 0.");
            }
            Value = value;
        }

        public static implicit operator ItemTypeId(long id)
        {
            return new ItemTypeId(id);
        }

        public static implicit operator long(ItemTypeId id)
        {
            return id.Value;
        }
    }

}
