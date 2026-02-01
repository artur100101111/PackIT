using PackIT.Domain.Locations;

namespace PackIt.Persistance.EF.Locations.ReadModels
{
    internal class LocationReadModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string? Description { get; set; }

        public LocationTypeEnum Type { get; set; }

        public LocationReadModel? Parent { get; set; }
        public long? ParentId { get; set; }
        public List<LocationReadModel> Sublocations { get; set; } = new();
        public int Version { get; set; }


    }
}
