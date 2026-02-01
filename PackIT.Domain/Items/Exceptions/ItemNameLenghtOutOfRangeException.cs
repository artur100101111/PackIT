namespace PackIT.Domain.Items.Exceptions
{
    internal class ItemNameLenghtOutOfRangeException : Exception
    {
        public ItemNameLenghtOutOfRangeException(string? message) : base(message)
        {
        }
    }
}