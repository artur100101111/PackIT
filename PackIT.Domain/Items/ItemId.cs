using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PackIT.Domain.Items
{
    public  record ItemId
    {
        public long Value { get; init; }

        public ItemId(long id)
        {
            this.Value = id;
        }

        public static implicit operator long(ItemId id)
            { return id.Value; }

        public static implicit operator ItemId(long id)
        { 
            return new(id);
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
