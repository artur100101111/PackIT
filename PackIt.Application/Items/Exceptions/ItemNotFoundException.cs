using PackIt.Shared.Abstractions.Domain.Exceptions;

namespace PackIt.Application.Items.Exceptions
{

    public class ItemNotFoundException : PackItException
    {


        public ItemNotFoundException(string? message) : base(message)
        {
        }

    }
}