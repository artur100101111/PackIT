using PackIt.Shared.Abstractions.Shared;
using PackIT.Domain.Locations.Exceptions;

namespace PackIT.Domain.Locations
{
    public class LocationDescription : ValueObject
    {
        public string? Value { get; set; } 
        public const int MaxLenght = 100;
        public const int MinLenght = 2;

        public LocationDescription(string? value)
        {
            if (value is null)
            {
                Value = null;
                return;
            }

            if (value.Length > MaxLenght)
                throw new LocationDesctiptionLenghtOutOfRangeException($"Location Description cannot be shorter than {MinLenght} and longer than {MaxLenght} characters.");

            Value = value;
        }


        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Value ?? string.Empty;
        }


        public static implicit operator LocationDescription(string value)
        {
            return new LocationDescription(value);
        }

        public static implicit operator string(LocationDescription locationName)
        {
            return locationName.Value!;
        }
    }
}
