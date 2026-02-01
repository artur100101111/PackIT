using PackIT.Domain.Common;

namespace PackIT.Shared.DtoTree.DtoTreeBuilder
{
    internal class TwoRootsInTheNodeListException : PackItException
    {

        public TwoRootsInTheNodeListException(string? message) : base(message)
        {
        }

    }
}