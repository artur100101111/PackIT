namespace PackIt.Shared.Abstractions.Domain.Exceptions
{
    public class DomainRuleViolationException : PackItException
    {
        public DomainRuleViolationException(string message): base(message) 
        {
            
        }
    }
}
