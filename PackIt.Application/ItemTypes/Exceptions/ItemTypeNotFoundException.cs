using PackIt.Shared.Abstractions.Domain.Exceptions;

namespace PackIt.Application.ItemTypes.Exceptions
{
    public class ItemTypeNotFoundException : NotFoundException
    {


        public ItemTypeNotFoundException(string? message) : base(message)
        {
        }

    }
}