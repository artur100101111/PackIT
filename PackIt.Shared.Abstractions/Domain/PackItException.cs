namespace PackIT.Domain.Common
{
    public abstract class PackItException : Exception
    {
        protected PackItException(string? message = null) : base(message)
        {

        }
    }
}
