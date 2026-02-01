
namespace PackIT.Domain.Items
{
    internal class ItemCodeLenghtOutOfRangeException : Exception
    {


        public ItemCodeLenghtOutOfRangeException(string? message) : base(message)
        {
        }

    }
}