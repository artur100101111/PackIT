using PackIt.Shared.Abstractions.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackIT.Domain.Locations.Exceptions
{
    public class LocationDesctiptionLenghtOutOfRangeException : DomainRuleViolationException
    {
        public LocationDesctiptionLenghtOutOfRangeException(string message) : base(message)
        {
        }
    }
}
