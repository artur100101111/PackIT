using PackIt.Shared.Abstractions.Shared;
using PackIT.Domain.Items.Exceptions;

namespace PackIT.Domain.Items
{
    public class ItemCode : ValueObject
    {
        public string Value { get; }
        public const int MinLenght = 2;
        public const int MaxLenght = 30;

        public ItemCode(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new EmptyItemCodeException("Item code cannot be empty.");
            if (value.Length < MinLenght && value.Length > MaxLenght)
                throw new ItemCodeLenghtOutOfRangeException($"Item code cannot be shorter than {MinLenght} and longer than {MaxLenght} characters.");
            this.Value = value;
        }

        public static implicit operator ItemCode(string value) 
        {
            return new ItemCode(value);
        }
        public static implicit operator string(ItemCode itemCode)
        {
            return itemCode.Value;
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}