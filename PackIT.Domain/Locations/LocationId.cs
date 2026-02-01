using PackIT.Domain.Locations.Exceptions;

namespace PackIT.Domain.Locations
{
    public record LocationId
    {
        public long Value { get; private set; }
        public LocationId(long value)
        {

            Value = value;
        }

        public static implicit operator long(LocationId id)
            => id.Value;

        public static implicit operator LocationId(long id)
            => new(id);

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
