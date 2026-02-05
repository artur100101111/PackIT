namespace PackIt.Shared.Abstractions.Domain.Exceptions
{
    public class AlreadyExistsException:PackItException
    {
        public AlreadyExistsException(string message) : base(message)
        {
            
        }
    }
}
