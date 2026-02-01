using PackIt.Shared.Abstractions.Shared;
using PackIT.Domain.Locations;
using System.Linq.Expressions;

namespace PackIt.Application.Locations.Commands.Specyfications
{
    public class GetLocationByIdSpecyfication : ISpecyfication<Location>
    {
        public long LocationId;

        public Expression<Func<Location, bool>> Criteria => l => l.Id == LocationId;

        public List<Expression<Func<Location, object>>> Includes => [l=>l.Sublocations];

        public GetLocationByIdSpecyfication(long locationId)
        {
            LocationId = locationId;
        }
    }
}