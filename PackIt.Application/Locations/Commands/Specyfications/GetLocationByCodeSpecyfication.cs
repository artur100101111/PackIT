using PackIt.Shared.Abstractions.Shared;
using PackIT.Domain.Locations;
using System.Linq.Expressions;

namespace PackIt.Application.Locations.Commands.Specyfications
{
    internal class CheckIfLocationExistsByCodeSpecyfication : ISpecyfication<Location>
    {
        public LocationCode LocationCode;

        public Expression<Func<Location, bool>> Criteria => lo => lo.Code == LocationCode;

        public List<Expression<Func<Location, object>>> Includes => null;

        public CheckIfLocationExistsByCodeSpecyfication(LocationCode locationCode)
        {
            this.LocationCode = locationCode;
        }
    }
}