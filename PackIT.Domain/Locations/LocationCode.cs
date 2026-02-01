using PackIT.Domain.Items.Exceptions;
using PackIT.Domain.Locations.Exceptions;

namespace PackIT.Domain.Locations
{
    public class LocationCode
    {
        public string Value { get; set; }
        public const int MinLenght = 2;
        public const int MaxLenght = 30;

        public LocationCode(string value)
        {
            if (string.IsNullOrEmpty(value)) throw new EmptyItemNameException("Location name cannot be empty.");
            if (value.Length < MinLenght && value.Length > MaxLenght)
                throw new LocationNameLenghtOutOfRangeException($"Item code cannot be shorter than {MinLenght} and longer than {MaxLenght} characters.");

            Value = value;
        }

        public static implicit operator LocationCode(string value) 
        {
            return new LocationCode(value); 
        }

        public static implicit operator string(LocationCode locationCode)
        {
            return locationCode.Value;
        }

    }
}