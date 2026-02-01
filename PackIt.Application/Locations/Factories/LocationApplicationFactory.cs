using PackIt.Application.Locations.Commands.Specyfications;
using PackIt.Application.Locations.Exceptions;
using PackIT.Domain.Locations;
using PackIT.Domain.Locations.Factories;
using PackIT.Domain.Locations.Repository;

namespace PackIt.Application.Locations.Factories
{
    internal class LocationApplicationFactory
    {
        private ILocationRepository _locationRepository;
        private ILocationFactory _domainFactory;

        public LocationApplicationFactory(ILocationRepository locationRepository, ILocationFactory domainFactory)
        {
            _locationRepository = locationRepository;
            _domainFactory = domainFactory;
        }
        public async Task<Location> CreateLocationAsync(long Id, string name, string code, string? description, LocationTypeEnum locationType,
            long? ancestorId, CancellationToken cancellationToken =default)
        {
            Location? ancestor = null;
            if (ancestorId is long ancestorIdValue)
            {
                ancestor = await _locationRepository.GetLocationBySpecyfictionAsync(new GetLocationByIdSpecyfication(ancestorIdValue), cancellationToken);
                if (ancestor is null) throw new LocationNotFoundException($"Sublocation with Id: {ancestorIdValue} was not found.");
            }
            var location = _domainFactory.CreateLocation(new LocationId(Id), new LocationName(name), new LocationCode(code), new LocationDescription(description), new LocationType(locationType),  ancestor);

            return location;
        }
    }
}
