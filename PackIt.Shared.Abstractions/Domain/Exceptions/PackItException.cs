namespace PackIt.Shared.Abstractions.Domain.Exceptions
{
    public abstract class PackItException : Exception
    {
        protected PackItException(string? message = null) : base(message)
        {

        }
    }
}
