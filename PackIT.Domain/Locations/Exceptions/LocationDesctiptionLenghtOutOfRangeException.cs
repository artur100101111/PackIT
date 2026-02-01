using PackIT.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackIT.Domain.Locations.Exceptions
{
    public class LocationDesctiptionLenghtOutOfRangeException : PackItException
    {
        public LocationDesctiptionLenghtOutOfRangeException(string message) : base(message)
        {
        }
    }
}
