using PackIt.Shared.Abstractions.Shared;
using PackIT.Domain.Items.Exceptions;
using PackIT.Domain.Locations.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackIT.Domain.Locations
{
    public class LocationType: ValueObject
    {
        public LocationTypeEnum Value { get; set; }

        public LocationType(LocationTypeEnum value)
        {
            if (!Enum.IsDefined(typeof(LocationTypeEnum), value))
            {
                throw new InvalidLocationTypeValueException($"Location Type mus be one of the {nameof(LocationTypeEnum)} value.");
            }
            Value = value;
        }

        public static implicit operator LocationType(LocationTypeEnum value)
        {
            return new LocationType(value);
        }

        public static implicit operator LocationTypeEnum(LocationType locationType)
        {
            return locationType.Value;
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
