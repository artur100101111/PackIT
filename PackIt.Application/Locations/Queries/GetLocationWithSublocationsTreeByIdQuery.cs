using PackIt.Application.Locations.DTO;
using PackIt.Shared.Abstractions.Queries;

namespace PackIt.Application.Locations.Queries
{ 
    /// <summary>
    /// Get Root Location with sublocatins tree List data. // by sql stored procedure.
    /// </summary>
    public class GetLocationTreeWithSublocationsByIdQuery: IQuery<LocationDto>
    {
        public long LocationId { get; set; }
    }
}
