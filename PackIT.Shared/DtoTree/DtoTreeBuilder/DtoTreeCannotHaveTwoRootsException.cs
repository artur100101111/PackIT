using PackIt.Shared.Abstractions.Domain.Exceptions;

namespace PackIT.Shared.DtoTree.DtoTreeBuilder
{
    internal class TwoRootsInTheNodeListException : PackItException
    {

        public TwoRootsInTheNodeListException(string? message) : base(message)
        {
        }

    }
}