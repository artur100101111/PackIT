namespace PackIt.Shared.Abstractions.Domain.Exceptions
{
    public class NotFoundException:PackItException
    {
        public NotFoundException(string message) : base(message)
        {
            
        }
    }
}
