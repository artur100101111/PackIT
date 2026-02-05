using PackIt.Shared.Abstractions.Domain.Exceptions;

namespace PackIt.Application.Locations.Exceptions
{
    internal class InvalidOperationCycleDetectedException : PackItException
    {

        public InvalidOperationCycleDetectedException(string? message) : base(message)
        {
        }

    }
}