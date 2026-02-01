using PackIT.Domain.Common;

namespace PackIt.Application.Locations.Exceptions
{
    internal class InvalidOperationCycleDetectedException : PackItException
    {

        public InvalidOperationCycleDetectedException(string? message) : base(message)
        {
        }

    }
}