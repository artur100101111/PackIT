using PackIt.Shared.Abstractions.Shared;
using PackIT.Domain.Items.Exceptions;

namespace PackIT.Domain.Items
{
    public class ItemName : ValueObject
    {
        public string Value { get; }
        public const int MinLenght = 2;
        public const int MaxLenght = 100;
        public ItemName(string value)
        {
            if (string.IsNullOrEmpty(value)) throw new EmptyItemNameException("Item name cannot be empty.");
            if (value.Length < MinLenght && value.Length > MaxLenght)
                throw new ItemNameLenghtOutOfRangeException($"Item code cannot be shorter than {MinLenght} and longer than {MaxLenght} characters.");
            this.Value = value;
        }

        public static implicit operator ItemName(string value)
        {
            return new ItemName(value);
        }

        public static implicit operator string(ItemName itemName)
        {
            return itemName.Value;
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }

    }
}