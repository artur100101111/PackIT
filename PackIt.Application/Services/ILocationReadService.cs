namespace PackIt.Application.Services
{
    public interface ILocationReadService
    {
        /// <summary>
        /// Used to check cycle when changing Location Parent property.
        /// </summary>
        /// <param name="locationId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<long>> GetParentTreePathAsync(long locationId, CancellationToken cancellationToken);

        ///// <summary>
        ///// Get all sublocatoins tree List.
        ///// It Can be filtered to RootLocation by Id.
        ///// </summary>
        ///// <param name="locationId"></param>
        ///// <param name="cancellationToken"></param>
        ///// <returns></returns>
        //Task<IEnumerable<LocationDto>> GetDescendantsTreeAsync(long locationId, CancellationToken cancellationToken);
    }
}
