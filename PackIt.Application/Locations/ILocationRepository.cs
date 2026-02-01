using PackIt.Shared.Abstractions.Persistance;
using PackIt.Shared.Abstractions.Shared;

namespace PackIT.Domain.Locations.Repository
{
    public interface ILocationRepository: IWriteRepository<Location, LocationId>
    {
        Task<T?> GetLocationBySpecyfictionAsync<T>(ISpecyfication<T> filterSpecylfication, CancellationToken cancellationToken = default) where T : Location;
        Task<IEnumerable<T>> GetLocationsBySpecyfictionAsync<T>(ISpecyfication<T> filterSpecylfication, CancellationToken cancellationToken = default) where T : Location;
        Task<bool> CheckIfExistsAsync<T>(ISpecyfication<T> existanceSpecyfication, CancellationToken cancellationToken = default) where T : Location;
    }
}
