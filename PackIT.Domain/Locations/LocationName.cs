using PackIt.Shared.Abstractions.Shared;
using PackIT.Domain.Items.Exceptions;
using PackIT.Domain.Locations.Exceptions;

namespace PackIT.Domain.Locations
{
    public class LocationName : ValueObject
    {
        public string Value { get; set; }
        public const int MinLenght = 2;
        public const int MaxLenght = 100;

        public LocationName(string value)
        {
            if (string.IsNullOrEmpty(value)) throw new EmptyItemNameException("Location name cannot be empty.");
            if (value.Length < MinLenght && value.Length > MaxLenght)
                throw new LocationNameLenghtOutOfRangeException($"Location Name cannot be shorter than {MinLenght} and longer than {MaxLenght} characters.");

            Value = value;
        }




        public static implicit operator LocationName(string value)
        {
            return new LocationName(value);
        }

        public static implicit operator string(LocationName locationName)
        {
            return locationName.Value;
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
